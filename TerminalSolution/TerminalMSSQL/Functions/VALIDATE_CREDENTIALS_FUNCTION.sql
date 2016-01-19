CREATE FUNCTION [dbo].[VALIDATE_CREDENTIALS_FUNCTION]
(
	@login varchar(16),
	@hash varchar(32)
)
RETURNS INT
AS
BEGIN
	DECLARE @exist int;
	DECLARE @permission int;
	SELECT @exist = COUNT([ACCOUNT_ID]) 
	FROM [dbo].[ACCOUNTS] 
	WHERE [LOGIN] = @login AND [PASSWORD] = @hash AND [STATUS] = 0;
	IF @exist = 1 
		BEGIN
			SELECT @permission = [PERMISSIONS] 
			FROM [dbo].[ACCOUNTS] 
			WHERE [LOGIN] = @login AND [PASSWORD] = @hash AND [STATUS] = 0;
			RETURN @permission;
		END
	RETURN NULL;
END