using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}
