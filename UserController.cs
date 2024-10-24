using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using szamonkeresapi.Models;
using static szamonkeresapi.Models.Dto;

namespace szamonkeresapi.Migrations
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<User> Get()
        {
            using (var context = new UserDbContext())
            {
                return Ok(context.NewUsers.ToList());
            }

        }
        [HttpPost]
        public ActionResult<User> Post([FromBody] CreateUserDto craeteUserDto)
        {
            using (var context = new UserDbContext())
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = craeteUserDto.Name,
                    Age = craeteUserDto.Age,
                    License = craeteUserDto.License,
                };

                if (user != null)
                {
                    context.NewUsers.Add(user);
                    context.SaveChanges();
                    return StatusCode(201, user);
                }
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult<User> Put(Guid id, UpdateUserDto updateUserDto)
        {
            using (var context = new UserDbContext())
            {
                var existingUser = context.NewUsers.FirstOrDefault(x => x.Id == id);
                if (existingUser != null)
                {
                    existingUser.Name = updateUserDto.Name;
                    existingUser.Age = updateUserDto.Age;
                    existingUser.License = updateUserDto.License;

                    context.NewUsers.Update(existingUser);
                    context.SaveChanges();

                    return StatusCode(200, existingUser);

                }
                return NotFound();
            }
        }
        [HttpDelete]
        public ActionResult<object> Delete(Guid id)
        {
            using (var context = new UserDbContext())
            {
                var user = context.NewUsers.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    context.NewUsers.Remove(user);
                    context.SaveChanges();

                    return StatusCode(204, new { message = "Sikeres törlés!" });
                }
                return StatusCode(404, new { message = "Nincs mit törölni!" });
            }
        }
    }
}