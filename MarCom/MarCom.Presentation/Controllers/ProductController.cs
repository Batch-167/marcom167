﻿using MarCom.Repository;
using MarCom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarCom.Presentation.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", ProductRepo.Get());
        }

        //GET : New Product
        public ActionResult Create()
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            return View("_Create", new ProductViewModel());
        }

        //POS
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            ResultResponse result = ProductRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Product = new SelectList(ProductRepo.Get(), "Id", "Name");
            ProductViewModel model = ProductRepo.GetById(id);
            return View("_Edit", model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            ResultResponse result = ProductRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                entity = model,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }


        //DELETE
        public ActionResult Delete(int id)
        {

            ProductViewModel model = ProductRepo.GetById(id);
            return View("_Delete", model);
        }

        //POST
        [HttpPost]
        public ActionResult Delete(ProductViewModel model)
        {
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            if (ProductRepo.Delete(id))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}