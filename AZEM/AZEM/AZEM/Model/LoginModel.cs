using System;
using System.Collections.Generic;
using System.Text;

namespace AZEM.Model
{
    class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RemainLoggedIn { get; set; }
    }
}
