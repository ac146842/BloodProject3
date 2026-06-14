SELECT bt.BloodTypeID, bt.SelectedBloodType, AVG(db.VolumeML) AS AvgVolumeML
FROM BloodType bt
INNER JOIN DonatedBlood db ON bt.BloodTypeID = db.BloodTypeID
GROUP BY bt.BloodTypeID, bt.SelectedBloodType
ORDER BY AvgVolumeML DESC;