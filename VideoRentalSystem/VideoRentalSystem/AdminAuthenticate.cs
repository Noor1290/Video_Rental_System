﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystem
{
    internal class AdminAuthenticate
    {
        public class AdminAuthenticator : IAuthenticator
        {
            public bool Authenticate(string username, string password)
            {
                string correctUsername = "admin";
                string correctPassword = "admin123";

                return username == correctUsername && password == correctPassword;
            }
        }

    }
}
