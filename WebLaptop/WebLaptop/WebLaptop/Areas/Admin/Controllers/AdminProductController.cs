﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AdminProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetList()
         {
            return PartialView();
        }
        public IActionResult Create()
        {
            return PartialView();
        }
        public IActionResult Edit(int id)
        {
           
            if (id != null)
            {
                ViewData["id"] = id;
                return PartialView();
            }
            else
            {
                return View("Index");
            }
        }
    }
}
