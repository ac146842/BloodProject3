SELECT n.NurseID, n.FirstName, n.LastName, COUNT(a.AppointmentID) AS AppointmentCount
FROM Nurse n
INNER JOIN Appointment a ON n.NurseID = a.NurseID
GROUP BY n.NurseID, n.FirstName, n.LastName
ORDER BY AppointmentCount DESC;