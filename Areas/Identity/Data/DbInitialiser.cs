using BloodProject3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

                //Nurses
                if (!context.Nurse.Any())
                {
                    var nurses = new Nurse[]
                    {
                        new Nurse{FirstName="William", LastName="Anderson", Phone = "555-807-2513", JobRole = "Senior Nurse", EmployedStartDate = DateTime.Parse("2020-01-10"), LicenseNumber = "RN123456"},
                        new Nurse{FirstName="Lauren", LastName="Thomas", Phone = "555-918-3624", JobRole="Clinic Lead", EmployedStartDate=DateTime.Parse("2021-05-15"), LicenseNumber="RN678901"},
                        new Nurse{FirstName="Robert", LastName="Moore", Phone = "555-685-0391", JobRole = "Staff Nurse", EmployedStartDate = DateTime.Parse("2023-08-24"), LicenseNumber = "RN456123"},
                        new Nurse{FirstName="Amanda", LastName="Taylor", Phone = "555-796-1402", JobRole = "Phlebotomist", EmployedStartDate = DateTime.Parse("2024-11-02"), LicenseNumber = "RN789321"}
                    };
                    context.Nurse.AddRange(nurses);
                    context.SaveChanges();
                }

                //Donors
                if (!context.Donor.Any())
                {
                    var donors = new Donor[]
                    {
                        new Donor{FirstName="John", LastName="Doe", Phone="123-456-7890", DateOfBirth=DateTime.Parse("1990-01-01"), BloodTypeID=1},
                        new Donor{FirstName="Jane", LastName="Smith", Phone="098-765-4321", DateOfBirth=DateTime.Parse("1992-02-02"), BloodTypeID=2},
                        new Donor{FirstName="Michael", LastName="Johnson", Phone="555-123-4567", DateOfBirth=DateTime.Parse("1988-03-15"), BloodTypeID=3},
                        new Donor{FirstName="Emily", LastName="Williams", Phone="555-987-6543", DateOfBirth=DateTime.Parse("1995-04-22"), BloodTypeID=4},
                        new Donor{FirstName="David", LastName="Brown", Phone="555-246-8135", DateOfBirth=DateTime.Parse("1987-05-10"), BloodTypeID=1},
                        new Donor{FirstName="Sarah", LastName="Davis", Phone="555-369-1357", DateOfBirth=DateTime.Parse("1993-06-18"), BloodTypeID=2},
                        new Donor{FirstName="James", LastName="Miller", Phone="555-482-7160", DateOfBirth=DateTime.Parse("1991-07-25"), BloodTypeID=5},
                        new Donor{FirstName="Jessica", LastName="Wilson", Phone="555-573-9284", DateOfBirth=DateTime.Parse("1994-08-12"), BloodTypeID=6}
                    };

                    context.Donor.AddRange(donors);
                    context.SaveChanges();
                }

                //Appointments
                if (!context.Appointment.Any())
                {
                    var appointments = new Appointment[]
                    {
                       new Appointment{ DonorID = 1, NurseID = 1, AppointmentDateTime = DateTime.Now.AddDays(1), Location = "Auckland City Center", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                       new Appointment{ DonorID = 2, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(2), Location = "North Shore Clinic", TypeOfAppointment = Appointment.AppointmentType.Consulting, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 15}
                    };

                    context.Appointment.AddRange(appointments);
                    context.SaveChanges();
                }

                //MedicalForms
                if (!context.MedicalForm.Any())
                {
                    var medicalForms = new MedicalForm[]
                    {
                        new MedicalForm{NurseID=1, AppointmentID=1, FormDate=DateTime.Now.AddDays(-2)},
                        new MedicalForm{NurseID=2, AppointmentID=2, FormDate=DateTime.Now.AddDays(-1)}
                    };
                    context.MedicalForm.AddRange(medicalForms);
                    context.SaveChanges();
                }

                //DonatedBlood
                if (!context.DonatedBlood.Any())
                {
                    var donations = new DonatedBlood[]
                    {
                        new DonatedBlood{AppointmentID=1, DonorID=1, BloodTypeID=1, CollectionDate=DateTime.Now.AddDays(-5), ExpiryDate=DateTime.Now.AddDays(37), VolumeML=450.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=2, DonorID=2, BloodTypeID=2, CollectionDate=DateTime.Now.AddDays(-4), ExpiryDate=DateTime.Now.AddDays(38), VolumeML=500.00m, BloodStatus=DonatedBlood.Status.Approved}
                    };
                    context.DonatedBlood.AddRange(donations);
                    context.SaveChanges();
                }

                //Inventory
                if (!context.Inventory.Any())
                {
                    var inventoryItems = new Inventory[]
                    {
                        new Inventory{DonationID=1, BloodTypeID=1, CurrentVolumeML=450.00m, StorageLocation="Fridge-A1", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=2, BloodTypeID=2, CurrentVolumeML=500.00m, StorageLocation="Shelf-04", BloodStatus=Inventory.Status.Available}
                    };

                    context.Inventory.AddRange(inventoryItems);
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
                        new Questions{FormQuestions="Are you currently taking any prescription medications including iron?"},
                        new Questions{FormQuestions="Have you travelled outside of New Zealand in the last 12 months?"}
                    };
                    context.Questions.AddRange(questions);
                    context.SaveChanges();
                }

                //Answers
                if (!context.Answers.Any())
                {
                    var answers = new Answers[]
                    {
                        new Answers{FormID=1, HealthQID=1, DonorID=1, AnswersBool=true, AnswerDate=DateTime.Now.AddDays(-2)},
                        new Answers{FormID=2, HealthQID=1, DonorID=2, AnswersBool=false, AnswerDate=DateTime.Now.AddDays(-1)}
                    };

                    context.Answers.AddRange(answers);
                    context.SaveChanges();
                }
            }
        }
    }
}