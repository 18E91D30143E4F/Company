using Data.Repositories.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        public bool Add(PositionEntity entity)
        {
            var command = new SqlCommand($"INSERT INTO position (p_name) VALUES ('{entity.Name}')");

            return DbHelper.ExecuteNoQuery(command) > 0;
        }

        public bool Update(PositionEntity entity)
        {
            var command = new SqlCommand($"UPDATE position SET p_name = '{entity.Name}' WHERE p_id = '{entity.Id}'");

            return DbHelper.ExecuteNoQuery(command) > 0;
        }

        public bool Delete(int id)
        {
            var command = new SqlCommand($"DELETE FROM position WHERE p_id = {id}");

            return DbHelper.ExecuteNoQuery(command) > 0;
        }

        public PositionEntity Get(int id)
        {
            var command = new SqlCommand($"SELECT p_id, p_name " +
                                         $"FROM position WHERE p_id = '{id}'");
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

        public IEnumerable<PositionEntity> GetAll()
        {
            const string cmdText = "SELECT * FROM position";
            var command = new SqlCommand(cmdText);
            var dataTable = DbHelper.ExecuteToDataTable(command);

            return from DataRow item in dataTable.Rows
                   select ParseDataRow(item);
        }

        private static PositionEntity ParseDataRow(DataRow dataRow)
        {
            return new PositionEntity
            {
                Id = int.Parse(dataRow["p_id"].ToString()),
                Name = dataRow["p_name"].ToString(),
            };
        }
    }
}