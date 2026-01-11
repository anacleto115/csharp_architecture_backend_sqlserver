IF NOT EXISTS (SELECT 1 FROM [person_types] WHERE [name] = 'Normal')
BEGIN
	INSERT INTO [person_types] ([name])
	VALUES ('Normal');
END