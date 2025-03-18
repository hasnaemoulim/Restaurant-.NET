using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resto4.Models;
using Resto4.Repositories;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Resto4.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AdminController(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View(_db.Ctegories.ToList());
        }

        public ActionResult Plats()
        {
            var plats = _db.Plats
                .Include(p => p.Category)
                .ToList();

            return View(plats);
        }


        public ActionResult Clients()
        {
            return View();
        }

        public ActionResult Orders()
        {
            var orders = _db.Orders
                .Include(o => o.OrderStatus)
                .ToList();

            // Fetch user-related information using a separate query
            var userIds = orders.Select(o => o.UserId).Distinct().ToList();
            var userDict = _db.Users.Where(u => userIds.Contains(u.Id)).ToDictionary(u => u.Id);

            // Assign user-related information to each order
            foreach (var order in orders)
            {
                if (userDict.TryGetValue(order.UserId, out var user))
                {
                    order.UserName = user.UserName;
                }
            }

            return View(orders);
        }


        public ActionResult CreateCategorie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategorie([Bind("CategoryId,CategoryName")] Category categorie)
        {
            if (ModelState.IsValid)
            {
                // Activer IDENTITY_INSERT
                _db.Database.OpenConnection();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Category ON");

                // Ajouter l'entité
                _db.Ctegories.Add(categorie);
                _db.SaveChanges();

                // Désactiver IDENTITY_INSERT
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Category OFF");
                _db.Database.CloseConnection();

                return RedirectToAction("Categories");
            }

            return View(categorie);
        }

        public ActionResult EditCategorie(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category categorie = _db.Ctegories.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }
            return View(categorie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategorie([Bind("CategoryId,CategoryName")] Category categorie)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(categorie).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(categorie);
        }

        [HttpGet]
        public ActionResult DeleteCategorie(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Category categorie = _db.Ctegories.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategorie(int id)
        {
            Category categorie = _db.Ctegories.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }

            _db.Ctegories.Remove(categorie);
            _db.SaveChanges();

            return RedirectToAction("Categories");
        }

        [HttpGet]
        public ActionResult CreatePlat()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlat(Plat plat)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("BEFORE ModelState.IsValid check");

                if (ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("INSIDE ModelState.IsValid block");

                    // Activer IDENTITY_INSERT
                    _db.Database.OpenConnection();
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Plat ON");

                    // Ajouter l'entité
                    _db.Plats.Add(plat);
                    _db.SaveChanges();

                    // Désactiver IDENTITY_INSERT
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Plat OFF");
                    _db.Database.CloseConnection();

                    return RedirectToAction("Plats");
                   
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("INSIDE ModelState.IsValid ELSE block");

                    // Log ModelState errors to Output window
                    System.Diagnostics.Debug.WriteLine($"ModelState IsValid: {ModelState.IsValid}");
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            System.Diagnostics.Debug.WriteLine($"ModelState Error: {error.ErrorMessage}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception to Output window
                System.Diagnostics.Debug.WriteLine($"Exception: {ex}");

                ModelState.AddModelError("", "An error occurred while saving the Plat.");
            }

            // Return the original view with validation errors
            return View(plat);
        }




        public ActionResult EditPlat(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Plat plat = _db.Plats.Find(id);

            if (plat == null)
            {
                return NotFound();
            }

            // Populate the ViewBag with the list of categories for the dropdown
            ViewBag.Categories = _db.Ctegories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();

            return View(plat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPlat(Plat plat)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(plat).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Plats");
            }

            // If ModelState is not valid, repopulate the ViewBag with the list of categories
            ViewBag.Categories = _db.Ctegories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();

            return View(plat);
        }


        [HttpGet]
        public ActionResult DeletePlat(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Plat plat = _db.Plats.Find(id);
            if (plat == null)
            {
                return NotFound();
            }

            return View(plat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlat(int id)
        {
            Plat plat = _db.Plats.Find(id);
            if (plat == null)
            {
                return NotFound();
            }

            _db.Plats.Remove(plat);
            _db.SaveChanges();

            return RedirectToAction("Plats");
        }

        public ActionResult EditOrders(int id)
        {
            // Fetch the order from your data source
            Order order = _db.Orders.Find(id);

            if (order == null)
            {
                // If order with the specified id is not found, return a 404 Not Found
                return NotFound();
            }

            // Populate the OrderStatusList
            ViewBag.OrderStatusList = _db.orderStatuses
                .Select(os => new SelectListItem
                {
                    Value = os.OrderStatusId.ToString(),
                    Text = os.StatusName
                })
                .ToList();

            return View(order);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrders(Order order)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the selected OrderStatus based on the OrderStatusId
                OrderStatus selectedOrderStatus = _db.orderStatuses.Find(order.OrderStatusId);

                // Update the OrderStatus property in the order object
                order.OrderStatus = selectedOrderStatus;

                // Set the state of the order entity to Modified
                _db.Entry(order).State = EntityState.Modified;

                // Save changes to the database
                _db.SaveChanges();

                return RedirectToAction("Orders");
            }

            // If ModelState is not valid, return to the view with the existing order object
            return View(order);
        }

        public ActionResult DeleteClient(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClientConfirmed(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user != null)
            {
                var result = _userManager.DeleteAsync(user).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Clients");
                }
                else
                {
                    // Handle errors, maybe return a view with error messages
                }
            }

            return NotFound();
        }

        public ActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateClient(IdentityUser client, string role)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser
                {
                    UserName = client.UserName,
                    Email = client.Email
                    // Add other properties as needed
                };

                var result = await _userManager.CreateAsync(newUser, client.PasswordHash);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(role) && await _roleManager.RoleExistsAsync(role))
                    {
                        await _userManager.AddToRoleAsync(newUser, role);
                    }

                    return RedirectToAction("clients");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(client);
        }

        public ActionResult EditClient(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditClient(IdentityUser editedUser, string role)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing user
                var existingUser = await _userManager.FindByIdAsync(editedUser.Id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing user
                existingUser.UserName = editedUser.UserName;
                existingUser.Email = editedUser.Email;
                // Update other properties as needed

                // Update the role for the user
                var currentRoles = await _userManager.GetRolesAsync(existingUser);
                await _userManager.RemoveFromRolesAsync(existingUser, currentRoles.ToArray());
                await _userManager.AddToRoleAsync(existingUser, role);

                // Update the user using UserManager
                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("clients");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(editedUser);
        }


    }



}

