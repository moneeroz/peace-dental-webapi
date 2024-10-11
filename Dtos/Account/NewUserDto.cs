using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peace_api.Dtos.Account
{
    public class NewUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }

    }
}