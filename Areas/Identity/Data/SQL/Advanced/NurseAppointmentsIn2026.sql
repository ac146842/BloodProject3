SELECT n.LicenseNumber, p.FirstName, p.LastName, a.AppointmentDateTime   
FROM Nurse n, Profile p, Appointment a   
WHERE n.UserID = p.UserID   
AND a.NurseID = n.UserID  
AND a.AppointmentDateTime >= '2026-01-01' 
AND a.AppointmentDateTime <= '2026-12-31';