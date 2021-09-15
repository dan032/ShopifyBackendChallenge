using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopifyBackendChallenge.Core.Utils;
using System;

namespace ShopifyBackendChallenge.Web.ExtensionMethods
{
    public static class ExtensionMethods2
    {
        public static bool PredicateTest(Func<string, string, string, bool> check, string password, string hash, string salt)
        {
            return check(password, hash, salt);
        }
    }

}
