using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserEditor.Models
{
    using UserEditor.Models.ViewModels;

    public interface IUsersService : IDisposable
    {
        IEnumerable<UserDto> Get();

        UserDto Get(int id);

        int Post(UserDto value);

        void Put(int id, UserDto value);

        void Delete(int id);
    }
}
