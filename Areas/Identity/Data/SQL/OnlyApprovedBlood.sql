SELECT db.DonationID, i.StorageLocation, db.CollectionDate 
FROM dbo.DonatedBlood db
JOIN dbo.Inventory i ON db.DonationID = i.DonationID 
WHERE i.BloodStatus = 3;