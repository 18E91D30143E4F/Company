using Data.Repositories.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public bool Add(EmployeeEntity entity)
        {
            var command = new SqlCommand(
                "INSERT INTO employee(e_name, e_surname, e_birthday, e_salary, e_position_id)" +
                       $" VALUES('{entity.Name}', '{entity.Surname}', '{entity.Birthday:yyyy-MM-dd}', '{entity.Salary}'," +
                       $"(SELECT p_id FROM position WHERE p_name = '{entity.PositionName}'))");

            return DbHelper.ExecuteNoQuery(command) > 0;
        }

        public bool Update(EmployeeEntity entity)
        {
            var selCulture = entity.Salary.ToString(CultureInfo.InvariantCulture);
            selCulture = selCulture.Replace(',', '.');

            var command = new SqlCommand($"UPDATE employee " +
                                         $"SET e_name = '{entity.Name}'," +
                                         $"e_surname = '{entity.Surname}'," +
                                         $"e_birthday = '{entity.Birthday:yyyy.MM.dd}'," +
                                         $"e_salary = '{selCulture}'," +
                                         $"e_position_id = " +
                                         $"(SELECT p_id FROM position " +
                                         $"WHERE p_name = '{entity.PositionName}') WHERE e_id = '{entity.Id}'");

            return DbHelper.ExecuteNoQuery(command) > 0;
        }

        public bool Delete(int id)
        {
            var command = new SqlCommand($"DELETE FROM employee WHERE e_id = {id}");

            return DbHelper.ExecuteNoQuery(command) > 0;
        }

        public EmployeeEntity Get(int id)
        {
            var cmdText = $"SELECT * FROM employee INNER JOIN position ON e_position_id = p_id WHERE e_id = {id}";
            var command = new SqlCommand(cmdText);
            var dataTable = DbHelper.ExecuteToDataTable(command);

            try
            {
                var item = dataTable.Rows[0];

                return ParseDataRow(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return null;
            }
        }

        public IEnumerable<EmployeeEntity> GetAll()
        {
            const string cmdText = "SELECT * FROM employee INNER JOIN position ON e_position_id = p_id";
            var command = new SqlCommand(cmdText);
            var dataTable = DbHelper.ExecuteToDataTable(command);

            foreach (DataRow item in dataTable.Rows)
                yield return ParseDataRow(item);
        }

        private static EmployeeEntity ParseDataRow(DataRow dataRow)
        {
            return new EmployeeEntity
            {
                Id = int.Parse(dataRow["e_id"].ToString()),
                Name = dataRow["e_name"].ToString(),
                Surname = dataRow["e_surname"].ToString(),
                PositionId = int.Parse(dataRow["e_position_id"].ToString()),
                PositionName = dataRow["p_name"].ToString(),
                Birthday = DateTime.Parse(dataRow["e_birthday"].ToString()),
                Salary = decimal.Parse(dataRow["e_salary"].ToString())
            };
        }
    }
}