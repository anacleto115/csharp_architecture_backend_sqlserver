CREATE PROCEDURE [dbo].[sp_select_per_types]
    @create_by NVARCHAR(25),
    @ip NVARCHAR(15),
    @result INT OUTPUT
AS
    SELECT  [id],
            [name]
    FROM [dbo].[person_types]

    SET @result = 1;
RETURN 0