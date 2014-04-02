using System;
using System.Collections.Generic;
using UserEditor.Models.ViewModels;

namespace UserEditor.Models
{
    public interface IUsersService : IDisposable
    {
        IEnumerable<UserDto> Get();

        UserDto Get(int id);

        int Post(UserDto value);

        void Put(int id, UserDto value);

        void Delete(int id);
    }
}
