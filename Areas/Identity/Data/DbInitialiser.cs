using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloodProject3.Areas.Identity.Data
{
    public class DbInitialiser
    {
        public static void AddData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BloodProject3DbContext>();

                context.Database.Migrate();
            }
        }
    }
}