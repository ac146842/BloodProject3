SELECT i.StorageLocation, I.CurrentVolumeML, p.LastName AS DonorLastName 
FROM Inventory i, DonatedBlood db, Profile p 
WHERE i.DonationID = db.DonationID 
AND db.DonorID = ProfileID; 