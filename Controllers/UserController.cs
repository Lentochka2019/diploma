using Lesson35.WebAPI.Data;
using Lesson35.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson35.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private PersonsDbContext _dbContext;
        public UserController(PersonsDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            //var users = GetUsers();
            try
            {
                var users = _dbContext.Persons.ToList(); 
                if (users.Count == 0)              
                {
                    return StatusCode(404, "No data");
                }
                return View(users);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Error 500"+ex.Message);
            }
        }
        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] Persons request)
        {
            Persons person = new Persons();
            person.LastName = request.LastName;
            person.FirstName = request.FirstName;
            person.Gender = request.Gender;
            person.Address = request.Address;
            try
            {
                _dbContext.Persons.Add(person);
                _dbContext.SaveChanges();
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Error 500" + ex.Message);
            }
            var users = _dbContext.Persons.ToList();
            return Ok(users);
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] Persons request)
        {            
            try
            {
                var user = _dbContext.Persons.FirstOrDefault(x => x.Id == request.Id);
                if(user==null)
                {
                    return StatusCode(404, "User not found");
                }
                user.LastName = request.LastName;
                user.FirstName = request.FirstName;
                user.Gender = request.Gender;
                user.Address = request.Address;

                _dbContext.Entry(user).State=EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Error 500" + ex.Message);
            }
            var users = _dbContext.Persons.ToList();
            return Ok(users);
        }
        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult Delete([FromRoute]int Id)
        {
            try
            {
                var user = _dbContext.Persons.FirstOrDefault(x => x.Id == Id);
                if (user == null)
                {
                    return StatusCode(404, "User not found");
                }               

                _dbContext.Entry(user).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Error 500" + ex.Message);
            }
            var users = _dbContext.Persons.ToList();
            return Ok(users);
        }
        //private List<UserRequest> GetUsers()
        //{
        //    return new List<UserRequest>
        //        {
        //        new UserRequest { UserName="Olena", UserAge=35},
        //        new UserRequest { UserName="Elena", UserAge=35},
        //        new UserRequest { UserName="Alena", UserAge=35}
        //    };
        //}

    }
}
