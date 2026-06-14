SELECT d.DonorID, d.FirstName, d.LastName, COUNT(mf.FormID) AS FormsCompleted
FROM Donor d
INNER JOIN Appointment a ON d.DonorID = a.DonorID
INNER JOIN MedicalForm mf ON a.AppointmentID = mf.AppointmentID
GROUP BY d.DonorID, d.FirstName, d.LastName
ORDER BY FormsCompleted DESC;