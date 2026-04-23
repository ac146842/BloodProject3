using BloodProject3.Models;
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

                //Profile
                if (!context.Profile.Any())
                {
                    var Profiles = new Profile[]
                    {
                        new Profile{UserID=1, FirstName="John", LastName="Doe", Phone="1234567890", DateOfBirth=DateTime.Parse("1990-01-01")},
                        new Profile{UserID=2, FirstName="Jane", LastName="Smith", Phone="0987654321", DateOfBirth=DateTime.Parse("1992-02-02")},
                        new Profile{UserID=3, FirstName="Michael", LastName="Johnson", Phone="5551234567", DateOfBirth=DateTime.Parse("1988-03-15")},
                        new Profile{UserID=4, FirstName="Emily", LastName="Williams", Phone="5559876543", DateOfBirth=DateTime.Parse("1995-04-22")},
                        new Profile{UserID=5, FirstName="David", LastName="Brown", Phone="5552468135", DateOfBirth=DateTime.Parse("1987-05-10")},
                        new Profile{UserID=6, FirstName="Sarah", LastName="Davis", Phone="5553691357", DateOfBirth=DateTime.Parse("1993-06-18")},
                        new Profile{UserID=7, FirstName="James", LastName="Miller", Phone="5554827160", DateOfBirth=DateTime.Parse("1991-07-25")},
                        new Profile{UserID=8, FirstName="Jessica", LastName="Wilson", Phone="5555739284", DateOfBirth=DateTime.Parse("1994-08-12")},
                        new Profile{UserID=9, FirstName="Robert", LastName="Moore", Phone="5556850391", DateOfBirth=DateTime.Parse("1989-09-30")},
                        new Profile{UserID=10, FirstName="Amanda", LastName="Taylor", Phone="5557961402", DateOfBirth=DateTime.Parse("1996-10-14")},
                        new Profile{UserID=11, FirstName="William", LastName="Anderson", Phone="5558072513", DateOfBirth=DateTime.Parse("1986-11-07")},
                        new Profile{UserID=12, FirstName="Lauren", LastName="Thomas", Phone="5559183624", DateOfBirth=DateTime.Parse("1997-12-20")},
                        new Profile{UserID=13, FirstName="Christopher", LastName="Jackson", Phone="5550294735", DateOfBirth=DateTime.Parse("1990-01-28")},
                        new Profile{UserID=14, FirstName="Megan", LastName="White", Phone="5551405846", DateOfBirth=DateTime.Parse("1998-02-11")},
                        new Profile{UserID=15, FirstName="Daniel", LastName="Harris", Phone="5552516957", DateOfBirth=DateTime.Parse("1985-03-19")},
                        new Profile{UserID=16, FirstName="Ashley", LastName="Martin", Phone="5553627068", DateOfBirth=DateTime.Parse("1999-04-05")},
                        new Profile{UserID=17, FirstName="Matthew", LastName="Thompson", Phone="5554738179", DateOfBirth=DateTime.Parse("1992-05-22")},
                        new Profile{UserID=18, FirstName="Natalie", LastName="Garcia", Phone="5555849280", DateOfBirth=DateTime.Parse("2000-06-09")},
                        new Profile{UserID=19, FirstName="Anthony", LastName="Martinez", Phone="5556950391", DateOfBirth=DateTime.Parse("1988-07-16")},
                        new Profile{UserID=20, FirstName="Olivia", LastName="Robinson", Phone="5557061402", DateOfBirth=DateTime.Parse("1994-08-03")},
                        new Profile{UserID=21, FirstName="Mark", LastName="Clark", Phone="5558172513", DateOfBirth=DateTime.Parse("1991-09-27")},
                        new Profile{UserID=22, FirstName="Sophia", LastName="Rodriguez", Phone="5559283624", DateOfBirth=DateTime.Parse("1997-10-14")},
                        new Profile{UserID=23, FirstName="Steven", LastName="Lewis", Phone="5550394735", DateOfBirth=DateTime.Parse("1989-11-21")},
                        new Profile{UserID=24, FirstName="Emma", LastName="Lee", Phone="5551405846", DateOfBirth=DateTime.Parse("1996-12-08")},
                        new Profile{UserID=25, FirstName="Paul", LastName="Walker", Phone="5552516957", DateOfBirth=DateTime.Parse("1987-01-30")}
                    }; 

                    context.Profile.AddRange(Profiles);
                    context.SaveChanges();
                }

                //Donor
                if (!context.Donor.Any())
                {
                    var Donors = new Donor[]
                    {
                                new Donor{DonorID=1, UserID=1, BloodTypeID=1, LastDonationDate=DateTime.Parse("2025-12-15")},
                                new Donor{DonorID=2, UserID=2, BloodTypeID=2, LastDonationDate=DateTime.Parse("2025-11-20")},
                                new Donor{DonorID=3, UserID=3, BloodTypeID=3, LastDonationDate=DateTime.Parse("2025-10-10")},
                                new Donor{DonorID=4, UserID=4, BloodTypeID=4, LastDonationDate=DateTime.Parse("2025-09-05")},
                                new Donor{DonorID=5, UserID=5, BloodTypeID=1, LastDonationDate=DateTime.Parse("2025-08-12")},
                                new Donor{DonorID=6, UserID=6, BloodTypeID=2, LastDonationDate=DateTime.Parse("2025-07-18")},
                                new Donor{DonorID=7, UserID=7, BloodTypeID=3, LastDonationDate=DateTime.Parse("2025-06-25")},
                                new Donor{DonorID=8, UserID=8, BloodTypeID=4, LastDonationDate=DateTime.Parse("2025-05-30")},
                                new Donor{DonorID=9, UserID=9, BloodTypeID=1, LastDonationDate=DateTime.Parse("2025-04-14")},
                                new Donor{DonorID=10, UserID=10, BloodTypeID=2, LastDonationDate=null}
                    };

                    context.Donor.AddRange(Donors);
                    context.SaveChanges();
                }

                //Inventory
                if (!context.Inventory.Any())
                {
                    var Inventory = new Inventory[]
                    {

                    };

                    context.Inventory.AddRange(Inventory);
                    context.SaveChanges();
                }

            }
        }
    }
}