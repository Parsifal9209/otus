using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.PromoCodeFactory.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Extensions
{
    public static class PromoCodeDbExtensions
    {
        public static void SeedingDataPromoCodeDb(this IServiceCollection services)
        {
            //var scope = services.CreateScope();
            //var db = scope.ServiceProvider.GetService<PromoCodeDbContext>();


            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();



            //db.SaveChanges();
        }
    }
}
