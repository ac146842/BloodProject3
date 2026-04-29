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

                //Roles
                {
                    var roles = new IdentityRole[]
                    {
                        new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                    };
                    context.Roles.AddRange(roles);
                    context.SaveChanges();
                }

                // 4. Link the user (this must stay INSIDE these braces)
                var adminMapping = new IdentityUserRole<string>
                {
                    UserId = "7f9b50ae-6022-4896-bfee-b5b346f9bbf2",
                    RoleId = roles[0].Id // roles[0] is "Admin"
                };

                context.UserRoles.Add(adminMapping);
                context.SaveChanges();

 

                //BloodType
                if (!context.BloodType.Any())
                {
                    var bloodTypes = new BloodType[]
                    {
                        new BloodType{SelectedBloodType = BloodType.BloodGroup.APositive},
                        new BloodType{SelectedBloodType = BloodType.BloodGroup.OPositive},
                        new BloodType{SelectedBloodType = BloodType.BloodGroup.BPositive},
                        new BloodType{SelectedBloodType = BloodType.BloodGroup.ABPositive},
                        new BloodType{SelectedBloodType = BloodType.BloodGroup.ANegative},
                        new BloodType{SelectedBloodType = BloodType.BloodGroup.ONegative},
                        new BloodType{SelectedBloodType = BloodType.BloodGroup.BNegative},
                        new BloodType{SelectedBloodType = BloodType.BloodGroup.ABNegative}
                    };
                    context.BloodType.AddRange(bloodTypes);
                    context.SaveChanges();
                }

                //Profile
                if (!context.Profile.Any())
                {
                    var profiles = new Profile[]
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

                    context.Profile.AddRange(profiles);
                    context.SaveChanges();
                }

                //Donor
                if (!context.Donor.Any())
                {
                    var donors = new Donor[]
                    {
                        new Donor{UserID=1, BloodTypeID=1},
                        new Donor{UserID=2, BloodTypeID=2},
                        new Donor{UserID=3, BloodTypeID=3}
                    };
                    context.Donor.AddRange(donors);
                    context.SaveChanges();
                }

                //Nurse
                if (!context.Nurse.Any())
                {
                    var nurses = new Nurse[]
                    {
                        new Nurse{UserID=11, JobRole="Senior Nurse", EmployedStartDate=DateTime.Parse("2020-01-10"), LicenseNumber="RN12345"},
                        new Nurse{UserID=12, JobRole="Clinic Lead", EmployedStartDate=DateTime.Parse("2021-05-15"), LicenseNumber="RN67890"}
                    };
                    context.Nurse.AddRange(nurses);
                    context.SaveChanges();
                }

                //Questions
                if (!context.Questions.Any())
                {
                    var questions = new Questions[]
                    {
                        new Questions{FormQuestions="How old are you?"},
                        new Questions{FormQuestions="To donate plasma you must weigh 50 kg or more and be 150 cm or taller. To donate blood you must weigh 50kg or more. Do you match these requirements?"},
                        new Questions{FormQuestions="Have you ever received a blood transfusion in the UK, Republic of Ireland or France after 1st January 1980?"},
                        new Questions{FormQuestions="Have you ever had a stroke, heart attack, or cardiac stent?"},
                        new Questions{FormQuestions="Are you under any health investigations and/or awaiting surgery?"},
                        new Questions{FormQuestions="Do you have cough or cold symptoms?"},
                        new Questions{FormQuestions="Have you had COVID in the last 7 days?"},
                        new Questions{FormQuestions="Have you had the Flu in the last 4 weeks?"},
                        new Questions{FormQuestions="Have you or anyone in your household had diarrhoea and/or vomiting in the last 12 weeks?"},
                        new Questions{FormQuestions="Have you had a tattoo or piercing in the last 3 months?"},
                        new Questions{FormQuestions="Are you currently pregnant or have you given birth recently?"},
                        new Questions{FormQuestions="Are you taking any prescription medications including iron?"},
                        new Questions{FormQuestions="Have you travelled outside of New Zealand in the last 12 months?"}
                    };
                    context.Questions.AddRange(questions);
                    context.SaveChanges();
                }

                //MedicalForm
                if (!context.MedicalForm.Any())
                {
                    var medicalForms = new MedicalForm[]
                    {
                        new MedicalForm{NurseID=1, AppointmentID=101, FormDate=DateTime.Now}
                    };
                    context.MedicalForm.AddRange(medicalForms);
                    context.SaveChanges();
                }

                // Answers
                if (!context.Answers.Any())
                {
                    var Answers = new Answers[]
                    {
                        new Answers{HealthQID=1, AppointmentID=1, QuestionAnswers="25", AnswerDate=DateTime.Now},
                        new Answers{HealthQID=2, AppointmentID=1, QuestionAnswers="Yes", AnswerDate=DateTime.Now},
                        new Answers{HealthQID=13, AppointmentID=1, QuestionAnswers="No", AnswerDate=DateTime.Now}
                    };

                    context.Answers.AddRange(Answers);
                    context.SaveChanges();
                }

                //DonatedBlood
                if (!context.DonatedBlood.Any())
                {
                    var donations = new DonatedBlood[]
                    {
                       new DonatedBlood{AppointmentID=1, DonorID=1, BloodTypeID=1, CollectionDate=DateTime.Now.AddDays(-1), ExpiryDate=DateTime.Now.AddDays(41), VolumeML=450.00m, BloodStatus=DonatedBlood.Status.Approved},
                       new DonatedBlood{AppointmentID=2, DonorID=2, BloodTypeID=2, CollectionDate=DateTime.Now.AddDays(-2), ExpiryDate=DateTime.Now.AddDays(40), VolumeML=500.00m, BloodStatus=DonatedBlood.Status.Approved}
                    };
                    context.DonatedBlood.AddRange(donations);
                    context.SaveChanges();
                }

                // Inventory
                if (!context.Inventory.Any())
                {
                    var inventoryItems = new Inventory[]
                    {
                        new Inventory{DonationID=1, BloodTypeID=1, CurrentVolumeML=4.50m, StorageLocation="Fridge-A1", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=2, BloodTypeID=2, CurrentVolumeML=5.00m, StorageLocation="Shelf-04", BloodStatus=Inventory.Status.Available}
                    };

                    context.Inventory.AddRange(inventoryItems);
                    context.SaveChanges();
                }
            }
        }
    }
}