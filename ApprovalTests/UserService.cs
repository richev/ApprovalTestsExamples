using Example.Services.Models;
using System.Collections.Generic;
using System;

namespace Example.Services
{
    public class UserService
    {
        private readonly IEnumerable<UserModel> _users = new List<UserModel>
        {
            new UserModel
            {
                Key = Guid.Parse("0bf6029f-31e5-467c-9a0f-e982482a0ede"),
                Age = 20,
                Name = new NameModel
                {
                    First = "Jack",
                    Last = "Black"
                },
                Addresses = new List<AddressModel>
                {
                    new AddressModel
                    {
                        Type = AddressType.Business,
                        Street = "Work Road",
                        City = "Sydney",
                        State = "NSW",
                        Postcode = "2000"
                    },
                    new AddressModel
                    {
                        Type = AddressType.Home,
                        Street = "Home Road",
                        City = "Fairlight",
                        State = "NSW",
                        Postcode = "2094"
                    }
                }
            },
            new UserModel
            {
                Key = Guid.Parse("cda51980-0b41-43db-9135-bf44471bd07f"),
                Name = new NameModel
                {
                    First = "Jill",
                    Last = "Blue"
                }
            }
        };

        public IEnumerable<UserModel> GetUsers()
        {
            return _users;
        }
    }
}
