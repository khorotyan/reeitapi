using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Enums
{
    public enum ErrorCode
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        TooManyRequests = 429
    }
}
