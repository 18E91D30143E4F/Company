using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using ClosedXML.Excel;
using Data.Repositories;
using Data.Repositories.Abstract;
using Microsoft.Office.Interop.Excel;
using _excel = Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace Services
{
    public class ReportService
    {
        #region Fields

        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IPositionRepository _PositionRepository;

        #endregion

        #region Contructors

        public ReportService(IEmployeeRepository employeeRepository, IPositionRepository positionRepository)
        {
            _EmployeeRepository = employeeRepository;
            _PositionRepository = positionRepository;
        }

        public ReportService()
        {
            _EmployeeRepository = new EmployeeRepository();
            _PositionRepository = new PositionRepository();
        }

        #endregion

        public byte[] GenerateReport()
        {
            var employees = _EmployeeRepository.GetAll()
                .ToList();
            var positions = _PositionRepository.GetAll()
                .ToList();

            var dt = new DataTable { TableName = "Отчет" };
            dt.Columns.Add("Должность", typeof(string));
            dt.Columns.Add("Средняя зарплата", typeof(string));

            var reports =
                (from position in positions
                 let unionEmployees = employees
                     .Where(emp => emp.PositionId == position.Id)
                     .Select(obj => new { obj.Salary, obj.PositionName })
                 select new PositionReport
                 {
                     AverageSalary = unionEmployees.Count() != 0
                         ? unionEmployees
                             .Select(salary => salary.Salary)
                             .Average()
                         : 0,
                     PositionName = position.Name
                 });

            foreach (var report in reports)
                dt.Rows.Add(report.PositionName, report.AverageSalary);

            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public class PositionReport
        {
            public string PositionName { get; set; }
            public decimal AverageSalary { get; set; }
        }
    }
}