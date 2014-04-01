using System.Collections.Generic;
using System.Web.Http;

namespace UserEditor.Controllers
{
    using UserEditor.Models;
    using UserEditor.Models.ViewModels;

    public class UsersController : ApiController
    {
        private IUsersService _usersService;


        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET api/users
        public IEnumerable<UserDto> Get()
        {
            return _usersService.Get();
        }

        // GET api/users/5
        public UserDto Get(int id)
        {
            return _usersService.Get(id);
        }

        // POST api/users
        public int Post([FromBody]UserDto value)
        {
            return _usersService.Post(value);
        }

        // PUT api/users/5
        public void Put(int id, [FromBody]UserDto value)
        {
            _usersService.Put(id, value);
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
            _usersService.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            this._usersService.Dispose();
            base.Dispose(disposing);
        }
    }
}
