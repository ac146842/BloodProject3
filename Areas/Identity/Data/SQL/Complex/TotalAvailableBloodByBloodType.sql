SELECT bt.SelectedBloodType, SUM(i.CurrentVolumeML) AS TotalAvailableVolume
FROM Inventory i
INNER JOIN BloodType bt ON i.BloodTypeID = bt.BloodTypeID
WHERE i.BloodStatus = 1
GROUP BY bt.SelectedBloodType;