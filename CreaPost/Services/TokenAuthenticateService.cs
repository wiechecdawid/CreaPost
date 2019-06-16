using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreaPost.Models.TokenValidationModels;

namespace CreaPost.Services
{
    public class TokenAuthenticateService : IAuthenticateService
    {
        public bool IsAuthenticated(TokenRequest request, out string token)
        {
            throw new NotImplementedException();
        }
    }
}
