using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShayanShop.Data;
using ShayanShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZarinpalSandbox;

namespace ShayanShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ShayanShopContext _Context;

        public HomeController(ILogger<HomeController> logger, ShayanShopContext context)
        {
            _logger = logger;
            _Context = context;
        }

        public IActionResult Index()
        {
            var products = _Context.Products.ToList();
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = _Context.Products
                .Include(p => p.Item)
                .SingleOrDefault(s => s.Id == id);
            if (product == null) 
            {
                return NotFound();
            }

            var categories = _Context.Categories
                .Where(x => x.Id == id)
                .SelectMany(c => c.CategoryToProducts)
                .Select(ca => ca.Category)
                .ToList();

            var vm = new DetailsViewModel()
            {
                Product = product,
                Categories = categories
            };


            return View(vm);
        }


        [Authorize]
        public IActionResult AddToCart(int itemId)
        {
            var product = _Context.Products
                .Include(i => i.Item)
                .SingleOrDefault(s => s.ItemId == itemId);
            if (product != null) 
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _Context.Orders
                    .FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
                if (order != null)
                {
                    var orderdetails = _Context.OrderDetails
                        .FirstOrDefault(d => d.OrderId == order.OrderId && d.ProductId == product.Id);
                    if(orderdetails != null)
                    {
                        orderdetails.Count += 1;
                    }
                    else
                    {
                        _Context.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.Id,
                            Price = product.Item.Price,
                            Count = 1
                        });
                    }
                }
                else
                {
                    order = new Order()
                    {
                        IsFinally = false,
                        CreateDate = DateTime.Now,
                        UserId = userId
                    };
                    _Context.Orders.Add(order);
                    _Context.SaveChanges();
                    _Context.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.Id,
                        Price = product.Item.Price,
                        Count = 1
                    });
                }
                _Context.SaveChanges();
            }
            return RedirectToAction("ShowCart");
        }


        [Authorize]
        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _Context.Orders
                .Where(x => x.UserId == userId && !x.IsFinally)
                .Include(o => o.OrderDetails)
                .ThenInclude(c => c.Product)
                .FirstOrDefault();
            return View(order);
        }

        [Authorize]
        public IActionResult RemoveCart(int orderDetailId)
        {
            var orderDetails = _Context.OrderDetails.Find(orderDetailId);
            _Context.OrderDetails.Remove(orderDetails);
            _Context.SaveChanges();

            return RedirectToAction("ShowCart");
        }



        [Route("/Group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id, string name)
        {
            ViewData["GroupName"] = name;
            var product = _Context.CategoryToProducts
                .Where(i => i.CategoryId == id)
                .Include(p => p.Product)
                .Select(c => c.Product)
                .ToList();
            return View(product);
        }



        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult ContactSend()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region Payment

        [Authorize]
        public IActionResult Payment()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = _Context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
            if (order == null)
                return NotFound();

            var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
                "http://localhost:1635/Home/OnlinePayment/" + order.OrderId, "Iman@Madaeny.com", "09197070750");
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }

        }

        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var order = _Context.Orders.Include(o => o.OrderDetails)
                    .FirstOrDefault(o => o.OrderId == id);
                var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    order.IsFinally = true;
                    _Context.Orders.Update(order);
                    _Context.SaveChanges();
                    ViewBag.code = res.RefId;
                    return View();
                }
            }

            return NotFound();
        }
        #endregion
    }
}
