using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UserEditor.Controllers
{
    using UserEditor.Models;
    using UserEditor.Models.ViewModels;

    public class UsersController : ApiController
    {
        private UsersDB context = new UsersDB();

        // GET api/users
        public IEnumerable<UserDto> Get()
        {
            return context.Users.ToList().Select(t => new UserDto()
            {
                Id = t.Id,
                Firstname = t.Firstname,
                Lastname = t.Lastname,
                Status = t.Status,
                Pages = string.IsNullOrEmpty(t.Pages) ? null : t.Pages.Split(';'),
                IsAdmin = t.IsAdmin
            }).ToList();
        }

        // GET api/users/5
        public UserDto Get(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                return new UserDto
                           {
                               Id = user.Id,
                               Firstname = user.Firstname,
                               Lastname = user.Lastname,
                               Status = user.Status,
                               Pages = string.IsNullOrEmpty(user.Pages) ? null : user.Pages.Split(';'),
                               IsAdmin = user.IsAdmin
                           };
            }

            return null;
        }

        // POST api/users
        public int Post([FromBody]UserDto value)
        {
            var user = new User
                           {
                               Id = value.Id,
                               Firstname = value.Firstname,
                               Lastname = value.Lastname,
                               Status = value.Status,
                               Pages = value.Pages == null ? null : string.Join(";", value.Pages),
                               IsAdmin = value.IsAdmin
                           };
            context.Users.Add(user);
            context.SaveChanges();
            return user.Id;
        }

        // PUT api/users/5
        public void Put(int id, [FromBody]UserDto value)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Firstname = value.Firstname;
                user.Lastname = value.Lastname;
                user.Status = value.Status;
                user.Pages = value.Pages == null ? null : string.Join(";", value.Pages);
                user.IsAdmin = value.IsAdmin;
            }

            context.SaveChanges();
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
