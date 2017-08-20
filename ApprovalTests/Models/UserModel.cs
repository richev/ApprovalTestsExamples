using System;
using System.Collections.Generic;

namespace Example.Services.Models
{
    public class UserModel
    {
        public Guid Key { get; set; }

        public int? Age { get; set; }

        public NameModel Name { get; set; }

        public IEnumerable<AddressModel> Addresses { get; set; }

        public IEnumerable<string> SomeCodes { get; set; }
    }
}
