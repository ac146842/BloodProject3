SELECT AppointmentID, DonorID, NurseID, AppointmentDateTime, Location
FROM Appointment
WHERE Location LIKE '%Auckland%'
ORDER BY AppointmentDateTime ASC;