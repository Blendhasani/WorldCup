
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WorldCup.Data;
using WorldCup.Models;
using WorldCup.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using WorldCup.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WorldCup.Data.Static;

namespace WorldCup.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Area("Admin")]
        public class StatisticsController : Controller
        {
            private AppDbContext _context;

            public StatisticsController(AppDbContext context)
            {
            _context = context;
            }

     
        [Route("Admin/Statistics/Index")]

        public ActionResult Index()
        {
            var orderItems = _context.OrderItems
                .Include(oi => oi.Order)
                .ToList();

            double sum = orderItems.Sum(oi => oi.Price * oi.Amount);
            double sasia = orderItems.Sum(oi => oi.Amount);
            int totalCount = _context.Highlights.Sum(x => x.Count);

            var model = new OrderStatisticsViewModel
            {
                Total = sum,
                Views = totalCount,
                Sasia = sasia
            };

            return View(model);
        }
    }

    }

