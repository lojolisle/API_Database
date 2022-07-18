using ApiDatabaseProject2.Data;
using ApiDatabaseProject2.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ApiDatabaseProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DemoDbContext _dbContext;

        public UserController(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    
        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
 
            try
            {
                var users = _dbContext.tblUsers.ToList();
                if (users.Count == 0)
                {
                    return StatusCode(404, "No User found");
                }
                return new JsonResult(users);
                //return Ok(users);

            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong!");
            }

        }


        [HttpGet("GetUser/{Id}")]
        public IActionResult GetUser([FromRoute] int Id)
        {
            try
            {
                var user = _dbContext.tblUsers.FirstOrDefault(x => x.Id == Id);
                if (user == null)
                {
                    return StatusCode(404, "No User found");
                }

                return new JsonResult(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }


        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] UserRequest request)
        {


            tblUser user = new tblUser();
            //user.Id = request.Id;
            user.UserName = request.UserName;
            user.Phonenumber = request.Phonenumber;
            user.City = request.City;

            // get last id
           
            var lastUserId = _dbContext.tblUsers.Max(x => x.Id);
            Debug.WriteLine(lastUserId);

            user.Id = lastUserId + 1;
            Debug.WriteLine(user.Id);

            try
            {
                _dbContext.tblUsers.Add(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong!");
            }

            //get all users
            var users = _dbContext.tblUsers.ToList();
            return Ok(users);


        }

        [HttpPut("UpdateUser")]
        public IActionResult Update([FromBody] UserRequest request)
        {
            try
            {
                var user = _dbContext.tblUsers.FirstOrDefault(x => x.Id == request.Id);
                if (user == null)
                {
                    return StatusCode(404, "No User found");
                }
                user.UserName = request.UserName;
                user.Phonenumber = request.Phonenumber;
                user.City = request.City;

                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong!");
            }

            //get all users
            var users = _dbContext.tblUsers.ToList();
            return Ok(users);
        }

        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            try
            {
                var user = _dbContext.tblUsers.FirstOrDefault(x => x.Id == Id);
                if (user == null)
                {
                    return StatusCode(404, "No User found");
                }

                _dbContext.Entry(user).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong!");
            }

            //get all users
            var users = _dbContext.tblUsers.ToList();
           
            return Ok(users);
        }

        /* private List<UserRequest> GetUsers()
         {
             return new List<UserRequest> { new UserRequest { UserName = "ABC" } };
         }*/


    }
}
