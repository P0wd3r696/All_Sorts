﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using All_Sorts.Data;
using Microsoft.AspNetCore.Mvc;

namespace All_Sorts.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var users = _db.Users.ToList();

            return View(users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}