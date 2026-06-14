SELECT d.DonorID, d.FirstName, d.LastName, COUNT(db.DonationID) AS TotalDonations
FROM Donor d
INNER JOIN DonatedBlood db ON d.DonorID = db.DonorID
GROUP BY d.DonorID, d.FirstName, d.LastName
ORDER BY d.LastName ASC;