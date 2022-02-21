#region Usings

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain;
using Services;
using Services.Abstract;

#endregion

namespace AbstractCompany.Web.Controllers
{
    public class PositionController : Controller
    {
        #region Fields

        public IPositionService _PositionService;

        #endregion

        #region Constructors

        public PositionController()
        {
            _PositionService = new PositionService();
        }

        public PositionController(IPositionService positionService)
        {
            _PositionService = positionService;
        }

        #endregion

        #region Index

        public ActionResult Index()
        {
            IEnumerable<Position> positions;

            try
            {
                positions = _PositionService.GetAll();
            }
            catch
            {
                return new HttpStatusCodeResult(500);
            }

            return View(positions);
        }

        #endregion

        #region Add position

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Создать должность";
            ViewBag.Action = "Создать";

            return View("CreateOrEdit");
        }

        [HttpPost]
        public ActionResult Create(Position position)
        {
            try
            {
                var res = _PositionService.Add(position);
                if (res)
                    return RedirectToAction("Index");
                return new HttpStatusCodeResult(500);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(500);
            }
        }

        #endregion

        #region Edit position

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var position = _PositionService.Get(id);

                if (position != null && position.Id == id)
                    return View("CreateOrEdit", position);
                return new HttpStatusCodeResult(500);
            }
            catch
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpPost]
        public ActionResult Edit(Position position)
        {
            try
            {
                var res = _PositionService.Update(position);

                if (res)
                    return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(500);
            }

            return new HttpStatusCodeResult(500);
        }

        #endregion

        #region Delete position

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var employee = _PositionService.Get(id);

                if (employee != null)
                    return View(employee);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            try
            {
                var res = _PositionService.Delete(employee.Id);

                if (res)
                    return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new HttpStatusCodeResult(500);
        }

        #endregion
    }
}