/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.5.278
 * Time: 04.08.2016 18:14:27
 ************************************************************/

/*******************************************
 * clear html tags
 *******************************************/
IF OBJECT_ID(N'dbo.func_strip_html', N'FN') IS NOT NULL
    DROP FUNCTION dbo.func_strip_html;
GO 
CREATE FUNCTION dbo.func_strip_html
(
	@HTMLText NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @Start INT
	DECLARE @End INT
	DECLARE @Length INT
	
	-- Replace the HTML entity &amp; with the '&' character (this needs to be done first, as
	-- '&' might be double encoded as '&amp;amp;')
	SET @Start = CHARINDEX('&amp;', @HTMLText)
	SET @End = @Start + 4
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '&')
	    SET @Start = CHARINDEX('&amp;', @HTMLText)
	    SET @End = @Start + 4
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace the HTML entity &ndash; with the ' - ' character
	SET @Start = CHARINDEX('&ndash;', @HTMLText)
	SET @End = @Start + 6
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, ' - ')
	    SET @Start = CHARINDEX('&ndash;', @HTMLText)
	    SET @End = @Start + 6
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace the HTML entity &laquo; with the ' " ' character
	SET @Start = CHARINDEX('&laquo;', @HTMLText)
	SET @End = @Start + 6
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '"')
	    SET @Start = CHARINDEX('&laquo;', @HTMLText)
	    SET @End = @Start + 6
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace the HTML entity &raquo; with the ' " ' character
	SET @Start = CHARINDEX('&raquo;', @HTMLText)
	SET @End = @Start + 6
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '"')
	    SET @Start = CHARINDEX('&raquo;', @HTMLText)
	    SET @End = @Start + 6
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace the HTML entity &lt; with the '<' character
	SET @Start = CHARINDEX('&lt;', @HTMLText)
	SET @End = @Start + 3
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '<')
	    SET @Start = CHARINDEX('&lt;', @HTMLText)
	    SET @End = @Start + 3
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace the HTML entity &gt; with the '>' character
	SET @Start = CHARINDEX('&gt;', @HTMLText)
	SET @End = @Start + 3
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '>')
	    SET @Start = CHARINDEX('&gt;', @HTMLText)
	    SET @End = @Start + 3
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace the HTML entity &amp; with the '&' character
	SET @Start = CHARINDEX('&amp;amp;', @HTMLText)
	SET @End = @Start + 4
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '&')
	    SET @Start = CHARINDEX('&amp;amp;', @HTMLText)
	    SET @End = @Start + 4
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace the HTML entity &nbsp; with the ' ' character
	SET @Start = CHARINDEX('&nbsp;', @HTMLText)
	SET @End = @Start + 5
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, ' ')
	    SET @Start = CHARINDEX('&nbsp;', @HTMLText)
	    SET @End = @Start + 5
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace the HTML entity &quot; with the '"' character
	SET @Start = CHARINDEX('&quot;', @HTMLText)
	SET @End = @Start + 5
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '"')
	    SET @Start = CHARINDEX('&quot;', @HTMLText)
	    SET @End = @Start + 5
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace any <br> tags with a newline
	SET @Start = CHARINDEX('<br>', @HTMLText)
	SET @End = @Start + 3
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, CHAR(13) + CHAR(10))
	    --CHAR(13) + CHAR(10)
	    SET @Start = CHARINDEX('<br>', @HTMLText)
	    SET @End = @Start + 3
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace any <br/> tags with a newline
	SET @Start = CHARINDEX('<br/>', @HTMLText)
	SET @End = @Start + 4
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, CHAR(13) + CHAR(10))
	    --CHAR(13) + CHAR(10)
	    SET @Start = CHARINDEX('<br/>', @HTMLText)
	    SET @End = @Start + 4
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Replace any <br /> tags with a newline
	SET @Start = CHARINDEX('<br />', @HTMLText)
	SET @End = @Start + 5
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, CHAR(13) + CHAR(10))
	    --CHAR(13) + CHAR(10)
	    SET @Start = CHARINDEX('<br />', @HTMLText)
	    SET @End = @Start + 5
	    SET @Length = (@End - @Start) + 1
	END
	
	-- Remove anything between <whatever> tags
	SET @Start = CHARINDEX('<', @HTMLText)
	SET @End = CHARINDEX('>', @HTMLText, CHARINDEX('<', @HTMLText))
	SET @Length = (@End - @Start) + 1
	
	WHILE (@Start > 0 AND @End > 0 AND @Length > 0)
	BEGIN
	    SET @HTMLText = STUFF(@HTMLText, @Start, @Length, '')
	    SET @Start = CHARINDEX('<', @HTMLText)
	    SET @End = CHARINDEX('>', @HTMLText, CHARINDEX('<', @HTMLText))
	    SET @Length = (@End - @Start) + 1
	END
	
	RETURN LTRIM(RTRIM(@HTMLText))
END
GO









/*******************************************
 * Получить услугу
 *******************************************/
IF OBJECT_ID(N'dbo.get_service_by_id', N'P') IS NOT NULL
    DROP PROCEDURE dbo.get_service_by_id;
GO
CREATE PROCEDURE dbo.get_service_by_id
	@serviceId INT
AS
BEGIN
	SELECT *
	FROM   Services AS s
	WHERE  s.Id = @serviceId
END
GO

/*******************************************
 * Добавить услугу
 *******************************************/
IF OBJECT_ID(N'dbo.add_service', N'P') IS NOT NULL
    DROP PROCEDURE dbo.add_service;
GO
CREATE PROCEDURE dbo.add_service
	@title NVARCHAR(255),
	@titleUrl NVARCHAR(255),
	@html NVARCHAR(MAX),
	@duration NVARCHAR(100),
	@cost NVARCHAR(100)
AS
BEGIN
	DECLARE @date     DATETIME = GETDATE(),
	        @id       INT = 0
	
	IF NOT EXISTS (
	       SELECT TOP 1 s.Id
	       FROM   Services AS s
	       WHERE  s.TitleUrl = @titleUrl
	   )
	BEGIN
	    INSERT INTO Services
	      (
	        -- Id -- this column value is auto-generated
	        Html,
	        Duration,
	        Cost,
	        DateUpdate,
	        DateCreate,
	        TitleUrl,
	        Title
	      )
	    VALUES
	      (
	        @html,
	        @duration,
	        @cost,
	        @date,
	        @date,
	        @titleUrl,
	        @title
	      )
	    
	    SELECT @id = @@identity
	END
	
	EXEC dbo.get_service_by_id @id
END
GO

/*******************************************
 * Удалить услугу
 *******************************************/
IF OBJECT_ID(N'dbo.del_service', N'P') IS NOT NULL
    DROP PROCEDURE dbo.del_service;
GO
CREATE PROCEDURE dbo.del_service
	@serviceId INT
AS
BEGIN
	DELETE 
	FROM   Services
	WHERE  Id = @serviceId
END
GO

/*******************************************
 * Обновить услугу
 *******************************************/
IF OBJECT_ID(N'dbo.update_service', N'P') IS NOT NULL
    DROP PROCEDURE dbo.update_service;
GO
CREATE PROCEDURE dbo.update_service
	@serviceId INT,
	@title NVARCHAR(255),
	@titleUrl NVARCHAR(255),
	@html NVARCHAR(MAX),
	@duration NVARCHAR(100),
	@cost NVARCHAR(100)
AS
BEGIN
	DECLARE @date DATETIME = GETDATE()
	
	UPDATE Services
	SET    Html = @html,
	       Duration = @duration,
	       Cost = @cost,
	       DateUpdate = @date,
	       TitleUrl = @titleUrl,
	       Title = @title
	WHERE  Id = @serviceId
	
	EXEC dbo.get_service_by_id @serviceId
END
GO

