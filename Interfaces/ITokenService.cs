using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peace_api.Models;

namespace peace_api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}