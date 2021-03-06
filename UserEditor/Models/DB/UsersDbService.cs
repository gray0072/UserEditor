﻿using System.Collections.Generic;
using System.Linq;
using UserEditor.Models.DB;
using UserEditor.Models.ViewModels;

namespace UserEditor.Models
{
    public class UsersDbService : IUsersService
    {
        private UsersDB _context = new UsersDB();

        public IEnumerable<UserDto> Get()
        {
            return this._context.Users.AsEnumerable().Select(user => new UserDto()
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Status = user.Status,
                Pages = user.Pages,
                IsAdmin = user.IsAdmin
            });
        }

        public UserDto Get(int id)
        {
            var user = this._context.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Status = user.Status,
                    Pages = user.Pages,
                    IsAdmin = user.IsAdmin
                };
            }

            return null;
        }

        public int Post(UserDto value)
        {
            var user = new User
            {
                Id = value.Id,
                Firstname = value.Firstname,
                Lastname = value.Lastname,
                Status = value.Status,
                Pages = value.Pages,
                IsAdmin = value.IsAdmin
            };
            this._context.Users.Add(user);
            this._context.SaveChanges();
            return user.Id;
        }

        public void Put(int id, UserDto value)
        {
            var user = this._context.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Firstname = value.Firstname;
                user.Lastname = value.Lastname;
                user.Status = value.Status;
                user.Pages = value.Pages;
                user.IsAdmin = value.IsAdmin;
            }

            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = this._context.Users.FirstOrDefault(u => u.Id == id);
            this._context.Users.Remove(user);
            this._context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}