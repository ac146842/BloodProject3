using BloodProject3.Migrations;
using BloodProject3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Server;
using System;
using System.Linq;

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
                        new Nurse{FirstName="Amanda", LastName="Taylor", Phone = "555-796-1402", JobRole = "Phlebotomist", EmployedStartDate = DateTime.Parse("2024-11-02"), LicenseNumber = "RN789321"},
                        new Nurse{FirstName="Charles", LastName="Harris", Phone = "555-111-2222", JobRole = "Donor Care Specialist", EmployedStartDate = DateTime.Parse("2019-03-14"), LicenseNumber = "RN102938"},
                        new Nurse{FirstName="Patricia", LastName="Clark", Phone = "555-333-4444", JobRole = "Apheresis Specialist", EmployedStartDate = DateTime.Parse("2022-07-19"), LicenseNumber = "RN564738"},
                        new Nurse{FirstName="Matthew", LastName="Lewis", Phone = "555-555-6666", JobRole = "Clinical Supervisor", EmployedStartDate = DateTime.Parse("2018-11-30"), LicenseNumber = "RN928134"},
                        new Nurse{FirstName="Jennifer", LastName="Walker", Phone = "555-777-8888", JobRole = "Mobile Unit Coordinator", EmployedStartDate = DateTime.Parse("2021-02-14"), LicenseNumber = "RN384729"},
                        new Nurse{FirstName="Christopher", LastName="Hall", Phone = "555-999-0000", JobRole = "Quality Assurance Nurse", EmployedStartDate = DateTime.Parse("2020-06-05"), LicenseNumber = "RN728394"},
                        new Nurse{FirstName="Elizabeth", LastName="Allen", Phone = "555-123-9876", JobRole = "Triage Nurse", EmployedStartDate = DateTime.Parse("2023-01-15"), LicenseNumber = "RN463728"},
                        new Nurse{FirstName="Daniel", LastName="Young", Phone = "555-456-1122", JobRole = "Hematology Nurse", EmployedStartDate = DateTime.Parse("2024-05-20"), LicenseNumber = "RN293847"},
                        new Nurse{FirstName="Megan", LastName="King", Phone = "555-789-3344", JobRole = "Research Nurse", EmployedStartDate = DateTime.Parse("2022-10-10"), LicenseNumber = "RN583920"}
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
                        new Donor{FirstName="Emily", LastName="Williams", Phone="555-987-6543", DateOfBirth=DateTime.Parse("1995-04-22"), BloodTypeID=3},
                        new Donor{FirstName="David", LastName="Brown", Phone="555-246-8135", DateOfBirth=DateTime.Parse("1987-05-10"), BloodTypeID=3},
                        new Donor{FirstName="Sarah", LastName="Davis", Phone="555-369-1357", DateOfBirth=DateTime.Parse("1993-06-18"), BloodTypeID=3},
                        new Donor{FirstName="James", LastName="Miller", Phone="555-482-7160", DateOfBirth=DateTime.Parse("1991-07-25"), BloodTypeID=3},
                        new Donor{FirstName="Jessica", LastName="Wilson", Phone="555-573-9284", DateOfBirth=DateTime.Parse("1994-08-12"), BloodTypeID=3},
                        new Donor{FirstName="Kevin", LastName="Martinez", Phone="555-612-3456", DateOfBirth=DateTime.Parse("1985-09-05"), BloodTypeID=3},
                        new Donor{FirstName="Lisa", LastName="Anderson", Phone="555-714-5678", DateOfBirth=DateTime.Parse("1989-12-30"), BloodTypeID=3},
                        new Donor{FirstName="Brian", LastName="Thomas", Phone="555-816-7890", DateOfBirth=DateTime.Parse("1996-05-14"), BloodTypeID=3},
                        new Donor{FirstName="Michelle", LastName="White", Phone="555-918-9012", DateOfBirth=DateTime.Parse("1990-11-21"), BloodTypeID=3},
                        new Donor{FirstName="Liam", LastName="Baker", Phone="021-111-2222", DateOfBirth=DateTime.Parse("1994-01-11"), BloodTypeID=1},
                        new Donor{FirstName="Noah", LastName="Carter", Phone="021-222-3333", DateOfBirth=DateTime.Parse("1991-02-12"), BloodTypeID=2},
                        new Donor{FirstName="Oliver", LastName="Evans", Phone="021-333-4444", DateOfBirth=DateTime.Parse("1989-03-13"), BloodTypeID=3},
                        new Donor{FirstName="Elijah", LastName="Foster", Phone="021-444-5555", DateOfBirth=DateTime.Parse("1995-04-14"), BloodTypeID=4},
                        new Donor{FirstName="Lucas", LastName="Green", Phone="021-555-6666", DateOfBirth=DateTime.Parse("1992-05-15"), BloodTypeID=5},
                        new Donor{FirstName="Mason", LastName="Harris", Phone="021-666-7777", DateOfBirth=DateTime.Parse("1987-06-16"), BloodTypeID=6},
                        new Donor{FirstName="Logan", LastName="Jackson", Phone="021-777-8888", DateOfBirth=DateTime.Parse("1993-07-17"), BloodTypeID=7},
                        new Donor{FirstName="Ethan", LastName="Kelly", Phone="021-888-9999", DateOfBirth=DateTime.Parse("1990-08-18"), BloodTypeID=8},
                        new Donor{FirstName="Aiden", LastName="Lane", Phone="021-999-0000", DateOfBirth=DateTime.Parse("1996-09-19"), BloodTypeID=1},
                        new Donor{FirstName="James", LastName="Adams", Phone="021-123-4567", DateOfBirth=DateTime.Parse("1994-10-20"), BloodTypeID=2}
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
                        new Appointment{ DonorID = 2, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(2), Location = "North Shore Clinic", TypeOfAppointment = Appointment.AppointmentType.Consulting, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 15},
                        new Appointment{ DonorID = 3, NurseID = 3, AppointmentDateTime = DateTime.Now.AddDays(3), Location = "Auckland City Center", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 4, NurseID = 4, AppointmentDateTime = DateTime.Now.AddDays(4), Location = "Manukau Clinic", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 5, NurseID = 5, AppointmentDateTime = DateTime.Now.AddDays(5), Location = "Hamilton Donor Hub", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 6, NurseID = 6, AppointmentDateTime = DateTime.Now.AddDays(6), Location = "Wellington Central", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 7, NurseID = 7, AppointmentDateTime = DateTime.Now.AddDays(7), Location = "Christchurch East", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 8, NurseID = 8, AppointmentDateTime = DateTime.Now.AddDays(8), Location = "North Shore Clinic", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 9, NurseID = 9, AppointmentDateTime = DateTime.Now.AddDays(9), Location = "Auckland City Center", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 10, NurseID = 10, AppointmentDateTime = DateTime.Now.AddDays(10), Location = "Dunedin Hospital", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 11, NurseID = 11, AppointmentDateTime = DateTime.Now.AddDays(11), Location = "Manukau Clinic", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 12, NurseID = 12, AppointmentDateTime = DateTime.Now.AddDays(12), Location = "Hamilton Donor Hub", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 13, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(13), Location = "Auckland City Center", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 14, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(14), Location = "North Shore Clinic, Auckland", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 15, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(15), Location = "Auckland City Center", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 16, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(16), Location = "Auckland West Hub", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 17, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(17), Location = "North Shore Clinic, Auckland", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 18, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(18), Location = "Auckland South Clinic", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 19, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(19), Location = "Auckland City Center", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 20, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(20), Location = "Auckland City Center", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30},
                        new Appointment{ DonorID = 21, NurseID = 2, AppointmentDateTime = DateTime.Now.AddDays(21), Location = "Auckland City Center", TypeOfAppointment = Appointment.AppointmentType.Donation, AppointmentStatus = Appointment.Status.Scheduled, DurationEndTime = 30}
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
                        new MedicalForm{NurseID=2, AppointmentID=2, FormDate=DateTime.Now.AddDays(-1)},
                        new MedicalForm{NurseID=3, AppointmentID=3, FormDate=DateTime.Now.AddDays(-1)},
                        new MedicalForm{NurseID=4, AppointmentID=4, FormDate=DateTime.Now.AddDays(-3)},
                        new MedicalForm{NurseID=5, AppointmentID=5, FormDate=DateTime.Now.AddDays(-4)},
                        new MedicalForm{NurseID=6, AppointmentID=6, FormDate=DateTime.Now.AddDays(-2)},
                        new MedicalForm{NurseID=7, AppointmentID=7, FormDate=DateTime.Now.AddDays(-5)},
                        new MedicalForm{NurseID=8, AppointmentID=8, FormDate=DateTime.Now.AddDays(-6)},
                        new MedicalForm{NurseID=9, AppointmentID=9, FormDate=DateTime.Now.AddDays(-1)},
                        new MedicalForm{NurseID=10, AppointmentID=10, FormDate=DateTime.Now.AddDays(-2)},
                        new MedicalForm{NurseID=11, AppointmentID=11, FormDate=DateTime.Now.AddDays(-3)},
                        new MedicalForm{NurseID=12, AppointmentID=12, FormDate=DateTime.Now.AddDays(-4)},
                        new MedicalForm{NurseID=2, AppointmentID=13, FormDate=DateTime.Parse("2026-02-10")},
                        new MedicalForm{NurseID=2, AppointmentID=14, FormDate=DateTime.Parse("2026-02-15")},
                        new MedicalForm{NurseID=2, AppointmentID=15, FormDate=DateTime.Parse("2026-03-01")},
                        new MedicalForm{NurseID=2, AppointmentID=16, FormDate=DateTime.Parse("2026-03-05")},
                        new MedicalForm{NurseID=2, AppointmentID=17, FormDate=DateTime.Parse("2026-03-20")},
                        new MedicalForm{NurseID=2, AppointmentID=18, FormDate=DateTime.Parse("2026-04-02")},
                        new MedicalForm{NurseID=2, AppointmentID=19, FormDate=DateTime.Parse("2026-04-15")},
                        new MedicalForm{NurseID=2, AppointmentID=20, FormDate=DateTime.Parse("2026-05-01")},
                        new MedicalForm{NurseID=2, AppointmentID=21, FormDate=DateTime.Parse("2026-05-10")},
                        new MedicalForm{NurseID=2, AppointmentID=2, FormDate=DateTime.Parse("2026-06-01")}
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
                        new DonatedBlood{AppointmentID=2, DonorID=2, BloodTypeID=2, CollectionDate=DateTime.Now.AddDays(-4), ExpiryDate=DateTime.Now.AddDays(38), VolumeML=500.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=3, DonorID=3, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-3), ExpiryDate=DateTime.Now.AddDays(39), VolumeML=470.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=4, DonorID=4, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-2), ExpiryDate=DateTime.Now.AddDays(40), VolumeML=450.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=5, DonorID=5, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-6), ExpiryDate=DateTime.Now.AddDays(36), VolumeML=490.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=6, DonorID=6, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-7), ExpiryDate=DateTime.Now.AddDays(35), VolumeML=510.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=7, DonorID=7, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-1), ExpiryDate=DateTime.Now.AddDays(41), VolumeML=460.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=8, DonorID=8, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-8), ExpiryDate=DateTime.Now.AddDays(34), VolumeML=450.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=9, DonorID=9, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-2), ExpiryDate=DateTime.Now.AddDays(40), VolumeML=480.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=10, DonorID=10, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-3), ExpiryDate=DateTime.Now.AddDays(39), VolumeML=500.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=11, DonorID=11, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-4), ExpiryDate=DateTime.Now.AddDays(38), VolumeML=470.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=12, DonorID=12, BloodTypeID=3, CollectionDate=DateTime.Now.AddDays(-5), ExpiryDate=DateTime.Now.AddDays(37), VolumeML=465.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=13, DonorID=13, BloodTypeID=1, CollectionDate=DateTime.Now.AddDays(-1), ExpiryDate=DateTime.Now.AddDays(41), VolumeML=485.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=14, DonorID=14, BloodTypeID=2, CollectionDate=DateTime.Now.AddDays(-2), ExpiryDate=DateTime.Now.AddDays(40), VolumeML=495.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=15, DonorID=16, BloodTypeID=4, CollectionDate=DateTime.Now.AddDays(-1), ExpiryDate=DateTime.Now.AddDays(41), VolumeML=460.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=16, DonorID=17, BloodTypeID=5, CollectionDate=DateTime.Now.AddDays(-2), ExpiryDate=DateTime.Now.AddDays(40), VolumeML=480.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=17, DonorID=18, BloodTypeID=6, CollectionDate=DateTime.Now.AddDays(-3), ExpiryDate=DateTime.Now.AddDays(39), VolumeML=450.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=18, DonorID=19, BloodTypeID=7, CollectionDate=DateTime.Now.AddDays(-4), ExpiryDate=DateTime.Now.AddDays(38), VolumeML=490.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=19, DonorID=20, BloodTypeID=8, CollectionDate=DateTime.Now.AddDays(-5), ExpiryDate=DateTime.Now.AddDays(37), VolumeML=470.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=20, DonorID=21, BloodTypeID=1, CollectionDate=DateTime.Now.AddDays(-6), ExpiryDate=DateTime.Now.AddDays(36), VolumeML=510.00m, BloodStatus=DonatedBlood.Status.Approved},
                        new DonatedBlood{AppointmentID=21, DonorID=22, BloodTypeID=2, CollectionDate=DateTime.Now.AddDays(-7), ExpiryDate=DateTime.Now.AddDays(35), VolumeML=505.00m, BloodStatus=DonatedBlood.Status.Approved}
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
                        new Inventory{DonationID=2, BloodTypeID=2, CurrentVolumeML=500.00m, StorageLocation="Shelf-04", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=3, BloodTypeID=3, CurrentVolumeML=470.00m, StorageLocation="Fridge-B2", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=4, BloodTypeID=3, CurrentVolumeML=450.00m, StorageLocation="Fridge-B2", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=5, BloodTypeID=3, CurrentVolumeML=490.00m, StorageLocation="Fridge-C1", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=6, BloodTypeID=3, CurrentVolumeML=510.00m, StorageLocation="Fridge-C2", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=7, BloodTypeID=3, CurrentVolumeML=460.00m, StorageLocation="Shelf-01", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=8, BloodTypeID=3, CurrentVolumeML=450.00m, StorageLocation="Shelf-02", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=9, BloodTypeID=3, CurrentVolumeML=480.00m, StorageLocation="Fridge-A2", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=10, BloodTypeID=3, CurrentVolumeML=500.00m, StorageLocation="Fridge-B1", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=11, BloodTypeID=3, CurrentVolumeML=470.00m, StorageLocation="Shelf-03", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=12, BloodTypeID=3, CurrentVolumeML=465.00m, StorageLocation="Fridge-C3", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=13, BloodTypeID=1, CurrentVolumeML=485.00m, StorageLocation="Fridge-A3", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=14, BloodTypeID=2, CurrentVolumeML=495.00m, StorageLocation="Fridge-A4", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=15, BloodTypeID=4, CurrentVolumeML=460.00m, StorageLocation="Fridge-D1", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=16, BloodTypeID=5, CurrentVolumeML=480.00m, StorageLocation="Fridge-D2", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=17, BloodTypeID=6, CurrentVolumeML=450.00m, StorageLocation="Fridge-D3", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=18, BloodTypeID=7, CurrentVolumeML=490.00m, StorageLocation="Fridge-D4", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=19, BloodTypeID=8, CurrentVolumeML=470.00m, StorageLocation="Fridge-D5", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=20, BloodTypeID=1, CurrentVolumeML=510.00m, StorageLocation="Fridge-A5", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=21, BloodTypeID=2, CurrentVolumeML=505.00m, StorageLocation="Fridge-A6", BloodStatus=Inventory.Status.Available},
                        new Inventory{DonationID=1, BloodTypeID=1, CurrentVolumeML=450.00m, StorageLocation="Fridge-X1", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=2, BloodTypeID=2, CurrentVolumeML=500.00m, StorageLocation="Fridge-X2", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=3, BloodTypeID=3, CurrentVolumeML=470.00m, StorageLocation="Fridge-X3", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=4, BloodTypeID=3, CurrentVolumeML=450.00m, StorageLocation="Fridge-X4", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=5, BloodTypeID=3, CurrentVolumeML=490.00m, StorageLocation="Fridge-X5", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=6, BloodTypeID=3, CurrentVolumeML=510.00m, StorageLocation="Fridge-X6", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=7, BloodTypeID=3, CurrentVolumeML=460.00m, StorageLocation="Fridge-X7", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=8, BloodTypeID=3, CurrentVolumeML=450.00m, StorageLocation="Fridge-X8", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=9, BloodTypeID=3, CurrentVolumeML=480.00m, StorageLocation="Fridge-X9", BloodStatus=(Inventory.Status)4},
                        new Inventory{DonationID=10, BloodTypeID=3, CurrentVolumeML=500.00m, StorageLocation="Fridge-Y1", BloodStatus=(Inventory.Status)4}
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
                        new Answers{FormID=1, HealthQID=1, DonorID=1, AnswersText="25", AnswerDate=DateTime.Now.AddDays(-2)},
                        new Answers{FormID=2, HealthQID=1, DonorID=2, AnswersText="34", AnswerDate=DateTime.Now.AddDays(-1)},
                        new Answers{FormID=3, HealthQID=1, DonorID=3, AnswersText="38", AnswerDate=DateTime.Now.AddDays(-1)},
                        new Answers{FormID=4, HealthQID=1, DonorID=4, AnswersText="31", AnswerDate=DateTime.Now.AddDays(-3)},
                        new Answers{FormID=5, HealthQID=1, DonorID=5, AnswersText="39", AnswerDate=DateTime.Now.AddDays(-4)},
                        new Answers{FormID=6, HealthQID=1, DonorID=6, AnswersText="33", AnswerDate=DateTime.Now.AddDays(-2)},
                        new Answers{FormID=7, HealthQID=1, DonorID=7, AnswersText="35", AnswerDate=DateTime.Now.AddDays(-5)},
                        new Answers{FormID=8, HealthQID=1, DonorID=8, AnswersText="32", AnswerDate=DateTime.Now.AddDays(-6)},
                        new Answers{FormID=9, HealthQID=1, DonorID=9, AnswersText="41", AnswerDate=DateTime.Now.AddDays(-1)},
                        new Answers{FormID=10, HealthQID=1, DonorID=10, AnswersText="37", AnswerDate=DateTime.Now.AddDays(-2)},
                        new Answers{FormID=11, HealthQID=1, DonorID=11, AnswersText="30", AnswerDate=DateTime.Now.AddDays(-3)},
                        new Answers{FormID=12, HealthQID=1, DonorID=12, AnswersText="36", AnswerDate=DateTime.Now.AddDays(-4)}
                    };

                    context.Answers.AddRange(answers);
                    context.SaveChanges();
                }
            }
        }
    }
}