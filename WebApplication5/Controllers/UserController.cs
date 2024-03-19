using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Controllers.Data;
using WebApplication5.DTO;

namespace WebApplication5.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserContext userContext;

        public UserController(UserContext user)
        {
            userContext = user;
        }

        public IActionResult GetAllUsers()
        {
            var usersWithRoles = userContext.userList
                .FromSqlRaw("SELECT u.Id, u.firstname, u.lastname, u.email, r.Name AS Role " +
                            "FROM aspnetusers u " +
                            "JOIN aspnetuserroles ur ON u.Id = ur.UserId " +
                            "JOIN aspnetroles r ON ur.RoleId = r.Id"
                )
                .Select(u => new UserList
                {
                    Id = (u.Id as string) ?? null,
                    firstname = (u.firstname as string) ?? null,
                    lastname = (u.lastname as string) ?? null,
                    email = (u.email as string) ?? null,
                    Role = (u.Role as string) ?? null,
                })
                .ToList();

            return View(usersWithRoles);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserList newUser)
        {
            if (ModelState.IsValid)
            {
                // Perform any additional validation if needed

                userContext.userList.Add(newUser);
                userContext.SaveChanges();

                return RedirectToAction("GetAllUsers");
            }

            return View("Error");
        }

        public IActionResult GetUserDetails(string userId)
        {
            var user = userContext.userList.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return View(user);
            }

            return View("Error");
        }
     
        public IActionResult EditUser(string userId)
        {
            var user = userContext.userList.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return View(user);
            }

            return View("Error");
        }

        [HttpPost]
        public IActionResult EditUser(UserList updatedUser)
        {
            if (ModelState.IsValid)
            {
                // Perform any additional validation if needed

                userContext.Entry(updatedUser).State = EntityState.Modified;
                userContext.SaveChanges();

                return RedirectToAction("GetAllUsers");
            }

            return View("Error");
        }

        // Delete user
        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            var user = userContext.userList
                .FromSqlRaw("SELECT Id, FirstName, LastName, Email FROM aspnetusers WHERE Id = {0}", id)
                .Select(u => new UserList
                {
                    Id = (u.Id as string) ?? null,
                    firstname = (u.firstname as string) ?? null,
                    lastname = (u.lastname as string) ?? null,
                    email = (u.email as string) ?? null
                  
                })
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public IActionResult ConfirmDeleteUser(string id)
        {
            // Fetch user details for display purposes (similar to DeleteUser action)
            var userToDelete = userContext.userList
                .FromSqlRaw("SELECT Id, FirstName, LastName, Email FROM aspnetusers WHERE Id = {0}", id)
                .Select(u => new UserList
                {
                    Id = (u.Id as string) ?? null,
                    firstname = (u.firstname as string) ?? null,
                    lastname = (u.lastname as string) ?? null,
                    email = (u.email as string) ?? null
                })
                .FirstOrDefault();

            if (userToDelete == null)
            {
                return NotFound();
            }

            // Perform necessary validations (if any)

            // Raw SQL query to delete the user from the database
            var deleteSql = "DELETE FROM aspnetusers WHERE Id = {0}";
            userContext.Database.ExecuteSqlRaw(deleteSql, id);

            return RedirectToAction(nameof(GetAllUsers));
        }
    }
}
