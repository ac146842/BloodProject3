SELECT FormID, NurseID, AppointmentID, FormDate
FROM MedicalForm
WHERE NurseID = 2 AND FormDate > '2026-01-01'
ORDER BY FormDate DESC;