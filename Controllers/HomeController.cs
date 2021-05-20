using AspaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspaApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
            if (db.Categories.Count() == 0)
            {
                Сategory food1 = new Сategory { СategoryName = "Пик фурманова" };
                Сategory food2= new Сategory { СategoryName = "Чарынский каньон" };

                Сategory food3 = new Сategory { СategoryName = "БАО" };

                Сategory food4 = new Сategory { СategoryName = "Бутаковский водопад" };
                Сategory food5 = new Сategory { СategoryName = "Поющий бархан" };


                db.Categories.AddRange(food1, food2 , food3 , food4 , food5);
                db.SaveChanges();
            }

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "admin, user")]
        public IActionResult Userpage()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            if (role == "admin")
            {
                return RedirectToAction("Adminpage", "Home");
            }
            return View(db.Users.ToList()); ;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Adminpage()
        {
            return View(db.Users.ToList());
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }


        public ActionResult Product(int? company, string name)

        {
            IQueryable<Product> products = db.Products.Include(p => p.Сategory);
            if (company != null && company != 0)
            {
                products = products.Where(p => p.СategoryId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.ProductName.Contains(name));
            }

            List<Сategory> сategories = db.Categories.ToList();
            сategories.Insert(0, new Сategory { СategoryName = "Все", СategoryId = 0 });

            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = products.ToList(),
                Categories = new SelectList(сategories, "Id", "Name"),
                Name = name
            };
            return View(viewModel);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Adminproduct(int? company, string name)

        {
            IQueryable<Product> products = db.Products.Include(p => p.Сategory);
            if (company != null && company != 0)
            {
                products = products.Where(p => p.СategoryId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.ProductName.Contains(name));
            }

            List<Сategory> сategories = db.Categories.ToList();
            сategories.Insert(0, new Сategory { СategoryName = "Все", СategoryId = 0 });

            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = products.ToList(),
                Categories = new SelectList(сategories, "Id", "Name"),
                Name = name
            };
            return View(viewModel);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create2(ProductViewModel pvm)
        {
            Product product = new Product { ProductName = pvm.ProductName, Description = pvm.Description, Company = pvm.Company, Place = pvm.Place , Start = pvm.Start, ProductCost = pvm.ProductCost,Numberoftickets = pvm.Numberoftickets ,СategoryId = pvm.СategoryId };
            if (pvm.ProductImage != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(pvm.ProductImage.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)pvm.ProductImage.Length);
                }

                product.ProductImage = imageData;
            }
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Create");
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Adminproduct");
            }
            return NotFound();
        }
        [Authorize(Roles = "admin, user")]
        public IActionResult Add(int id)
        {

            if (id != 0)
            {

                var user = db.Users.Where(p => p.Email == User.Identity.Name).FirstOrDefault();
                Basket basket = new Basket { ProductId = id, UserId = user.Id };
                db.Baskets.Add(
                   basket
                    );

                db.SaveChanges();
            }

            return RedirectToAction("Product");


        }
        
        public IActionResult Full(int id)
        {

         return View(db.Products.Where(p=> p.ProductId == id));


        }
        [Authorize(Roles = "admin, user")]
        public IActionResult Baskethis()
       {
            var select1 = from product in db.Products
                        join bas in db.Baskets on product.ProductId equals bas.ProductId
                          join user in db.Users on bas.UserId equals user.Id
                          select new Baskethis1 
                        {
                            BasketId = bas.BasketId,
                            ProductName = product.ProductName,
                          
                            ProductCost = product.ProductCost,
                            ProductImage = product.ProductImage,
                            Email = user.Email
                            
                            
                        };

            return View(select1);
            }


        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Delete2(int? id)
        {

            if (id != null)
            {
                Basket b1 = await db.Baskets.FirstOrDefaultAsync(p => p.BasketId == id);
                db.Baskets.Remove(b1);

                await db.SaveChangesAsync();
                return RedirectToAction("Baskethis");
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Userlist()
        {
            return View(db.Users);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete3(int? id)
        {

            if (id != null)
            {
                User b2 = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                db.Users.Remove(b2);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.PhoneId = id;
            return View();
        }
        [Authorize(Roles = "admin, user")]
        [HttpPost]
        public string Buy(Order order)
        {
            db.Orders.Add(order);
            
            db.SaveChanges();
            
            return "Спасибо, " + order.User + ", за покупку!";
        }
        

    }













}

