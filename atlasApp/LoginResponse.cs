using System;
using System.Collections.Generic;
using System.Text;

namespace atlasApp
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
    }
}
