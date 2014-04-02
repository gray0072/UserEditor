using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Xml.Serialization;
using UserEditor.Models.ViewModels;

namespace UserEditor.Models
{
    using UserEditor.Models.XML;

    public class UsersXmlService : IUsersService
    {
        private string XmlPath;
        private List<User> users;
        private bool IsChanged = false;

        public UsersXmlService(string xmlPath)
        {
            this.XmlPath = xmlPath;
            FileStream stream = null;
            try
            {
                var serializer = new XmlSerializer(typeof(List<User>));
                stream = new FileStream(this.XmlPath, FileMode.Open);
                this.users = (List<User>)serializer.Deserialize(stream);
            }
            catch (Exception)
            {
                this.users = new List<User>();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public IEnumerable<UserDto> Get()
        {
            return this.users.Select(user => new UserDto()
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
            var user = this.users.FirstOrDefault(u => u.Id == id);

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
            int newId = 1;
            if (this.users.Count != 0)
            {
                newId = this.users.Max(t => t.Id) + 1;
            }

            var user = new User
            {
                Id = newId,
                Firstname = value.Firstname,
                Lastname = value.Lastname,
                Status = value.Status,
                Pages = value.Pages,
                IsAdmin = value.IsAdmin
            };
            this.users.Add(user);
            this.IsChanged = true;
            return newId;
        }

        public void Put(int id, UserDto value)
        {
            var user = this.users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Firstname = value.Firstname;
                user.Lastname = value.Lastname;
                user.Status = value.Status;
                user.Pages = value.Pages;
                user.IsAdmin = value.IsAdmin;
            }

            this.IsChanged = true;
        }

        public void Delete(int id)
        {
            var user = this.users.FirstOrDefault(u => u.Id == id);
            this.users.Remove(user);
            this.IsChanged = true;
        }

        public void Dispose()
        {
            if (this.IsChanged)
            {
                FileStream stream = null;
                try
                {
                    var serializer = new XmlSerializer(typeof(List<User>));
                    stream = new FileStream(this.XmlPath, FileMode.Create);
                    serializer.Serialize(stream, this.users);
                    stream.Close();
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
            }
        }
    }
}