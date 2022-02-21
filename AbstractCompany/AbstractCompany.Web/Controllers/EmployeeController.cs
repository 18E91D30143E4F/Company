#region Usings

using Services;
using Services.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using AbstractCompany.Web.Models;
using Domain;

#endregion

namespace AbstractCompany.Web.Controllers
{
    public class EmployeeController : Controller
    {
        #region Fields

        private readonly IEmployeeService _EmployeeService;
        private readonly IPositionService _PositionService;
        private readonly ReportService _ReportService;

        #endregion

        #region Constructors

        public EmployeeController()
        {
            _EmployeeService = new EmployeeService();
            _PositionService = new PositionService();
            _ReportService = new ReportService();
        }

        #endregion

        #region Index

        public ActionResult Index() => View();

        #endregion

        #region Create employee

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Добавить сотрудника";
            ViewBag.Action = "Добавить";

            try
            {
                var positions = _PositionService.GetAll();

                var empViewModel = new EmployeeViewModel
                {
                    Positions = positions
                };


                return View("CreateOrEdit", empViewModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new HttpStatusCodeResult(500);
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            ViewBag.Title = "Добавить сотрудника";
            ViewBag.Action = "Добавить";

            if (ModelState.IsValid)
            {
                try
                {
                    var addedResult = _EmployeeService.Add(employee);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                var empViewModel = new EmployeeViewModel
                {
                    Positions = _PositionService.GetAll()
                };

                return View("CreateOrEdit", empViewModel);
            }

            return RedirectToAction("Index", "Employee");
        }


        #endregion

        #region Edit employee

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Редактировать сотрудника";
            ViewBag.Action = "Редактировать";

            try
            {
                var empViewModel = GetFilledEmployeeViewModel(id);

                return View("CreateOrEdit", empViewModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new HttpStatusCodeResult(500);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel model)
        {
            ViewBag.Title = "Редактировать сотрудника";
            ViewBag.Action = "Редактировать";

            if (ModelState.IsValid)
            {

                try
                {
                    model.Employee.Id = model.EmployeeId;
                    var res = _EmployeeService.Update(model.Employee);

                    return View("Index");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                return View("CreateOrEdit", GetFilledEmployeeViewModel(model.EmployeeId));
            }

            return new HttpStatusCodeResult(500);
        }

        #endregion

        #region Delete employee

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Employee employee = null;

            try
            {
                employee = _EmployeeService.Get(id);

                if (employee is null)
                    return new HttpStatusCodeResult(404);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                    return new HttpStatusCodeResult(500);

                var res = _EmployeeService.Delete(id);
                if (res)
                    return RedirectToAction("Index");

                return new HttpStatusCodeResult(500);
            }
            catch
            {
                return new HttpStatusCodeResult(500);
            }
        }

        #endregion

        #region Export excel

        public ActionResult GetPositionsReport()
        {
            var content = _ReportService.GenerateReport();
            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Report.xlsx"
            );
        }

        #endregion

        #region Help methods

        public PartialViewResult SearchByPosition(string text)
        {
            var employees = _EmployeeService.GetAll()
                .Where(emp => emp.PositionName.ToLower()
                    .Contains(text.ToLower()));

            return PartialView("_GridView", employees);
        }

        private EmployeeViewModel GetFilledEmployeeViewModel(int id)
        {
            var employee = _EmployeeService.Get(id);

            return new EmployeeViewModel
            {
                EmployeeId = id,
                Employee = employee,
                Positions = _PositionService.GetAll()
            };
        }

        #endregion
    }
}