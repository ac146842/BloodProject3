SELECT BloodBankID, DonationID, BloodStatus, StorageLocation 
FROM Inventory
WHERE BloodStatus = 4
ORDER BY StorageLocation;