﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasteAndRate.DTO
{
    public class LoginDTO
    {
        //public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }

        public LoginDTO() { }
        public LoginDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }

       
    }
}
