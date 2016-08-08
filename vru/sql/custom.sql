/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.5.278
 * Time: 08.08.2016 3:29:38
 ************************************************************/

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
	FROM   D_SERVICE AS s
	WHERE  s.Id = @serviceId
END
GO

/*******************************************
 * Получить услугу по titleUrl
 *******************************************/
IF OBJECT_ID(N'dbo.get_service_by_title_url', N'P') IS NOT NULL
    DROP PROCEDURE dbo.get_service_by_title_url;
GO
CREATE PROCEDURE dbo.get_service_by_title_url
	@titleUrl NVARCHAR(255)
AS
BEGIN
	SELECT *
	FROM   D_SERVICE AS s
	WHERE  s.TitleUrl = @titleUrl
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
	       FROM   D_SERVICE AS s
	       WHERE  s.TitleUrl = @titleUrl
	   )
	BEGIN
	    INSERT INTO D_SERVICE
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
	FROM   D_SERVICE
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
	
	UPDATE D_SERVICE
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

/*******************************************
 * Получить вопрос
 *******************************************/
IF OBJECT_ID(N'dbo.get_question_by_id', N'P') IS NOT NULL
    DROP PROCEDURE dbo.get_question_by_id;
GO
CREATE PROCEDURE dbo.get_question_by_id
	@questionId INT
AS
BEGIN
	SELECT *
	FROM   D_QUESTION AS q
	WHERE  q.Id = @questionId
END
GO

/*******************************************
 * Обновить статус прочтения вопроса
 *******************************************/
IF OBJECT_ID(N'dbo.update_question_read', N'P') IS NOT NULL
    DROP PROCEDURE dbo.update_question_read;
GO
CREATE PROCEDURE dbo.update_question_read
	@questionId INT,
	@read BIT
AS
BEGIN
	UPDATE D_QUESTION
	SET    IsReaded     = @read
	WHERE  Id           = @questionId
END
GO

/*******************************************
 * Добавить вопрос
 *******************************************/
IF OBJECT_ID(N'dbo.add_question', N'P') IS NOT NULL
    DROP PROCEDURE dbo.add_question;
GO
CREATE PROCEDURE dbo.add_question
	@name NVARCHAR(100),
	@email NVARCHAR(100),
	@phone NVARCHAR(100),
	@text NVARCHAR(600)
AS
BEGIN
	INSERT INTO D_QUESTION
	  (
	    -- Id -- this column value is auto-generated
	    NAME,
	    Email,
	    Phone,
	    [Text],
	    IsReaded,
	    DateCreate
	  )
	VALUES
	  (
	    @name,
	    @email,
	    @phone,
	    @text,
	    0,
	    GETDATE()
	  )
	
	DECLARE @id INT
	SELECT @id = @@identity
	
	EXEC dbo.get_question_by_id @id
END
GO

/*******************************************
 * Обновить вопрос
 *******************************************/
IF OBJECT_ID(N'dbo.update_question', N'P') IS NOT NULL
    DROP PROCEDURE dbo.update_question;
GO
CREATE PROCEDURE dbo.update_question
	@questionId INT,
	@name NVARCHAR(100),
	@email NVARCHAR(100),
	@phone NVARCHAR(100),
	@text NVARCHAR(600),
	@isReaded BIT
AS
BEGIN
	UPDATE D_QUESTION
	SET    NAME = @name,
	       Email = @email,
	       Phone = @phone,
	       [Text] = @text,
	       IsReaded = @isReaded,
	       DateCreate = GETDATE()
	WHERE  Id = @questionId
	
	EXEC dbo.get_question_by_id @questionId
END
GO

/*******************************************
 * Удалить вопрос
 *******************************************/
IF OBJECT_ID(N'dbo.del_question', N'P') IS NOT NULL
    DROP PROCEDURE dbo.del_question;
GO
CREATE PROCEDURE dbo.del_question
	@questionId INT
AS
BEGIN
	DELETE 
	FROM   D_QUESTION
	WHERE  Id = @questionId
END
GO

/*******************************************
 * Получить образование
 *******************************************/
IF OBJECT_ID(N'dbo.get_education_by_key', N'P') IS NOT NULL
    DROP PROCEDURE dbo.get_education_by_key;
GO
CREATE PROCEDURE dbo.get_education_by_key
	@educationId INT
AS
BEGIN
	SELECT *
	FROM   D_EDUCATION          AS de
	       LEFT JOIN D_PICTURE  AS dp
	            ON  dp.Id = de.PictureId
	WHERE  de.Id = @educationId
END
GO

/*******************************************
 * Добавить образование
 *******************************************/
IF OBJECT_ID(N'dbo.add_education', N'P') IS NOT NULL
    DROP PROCEDURE dbo.add_education;
GO
CREATE PROCEDURE dbo.add_education
	@year INT,
	@month INT,
	@groupName NVARCHAR(MAX),
	@html NVARCHAR(MAX),
	@pictureId UNIQUEIDENTIFIER,
	@order INT
AS
BEGIN
	DECLARE @date DATETIME = GETDATE()
	INSERT INTO D_EDUCATION
	  (
	    -- Id -- this column value is auto-generated
	    [Year],
	    [Month],
	    GroupName,
	    Html,
	    PictureId,
	    DateUpdate,
	    DateCreate,
	    [Order]
	  )
	VALUES
	  (
	    @year,
	    @month,
	    @groupName,
	    @html,
	    @pictureId,
	    @date,
	    @date,
	    @order
	  )
	
	DECLARE @id INT
	SELECT @id = @@identity
	
	EXEC dbo.get_education_by_key @id
END
GO

/*******************************************
 * Удалить образование
 *******************************************/
IF OBJECT_ID(N'dbo.del_education', N'P') IS NOT NULL
    DROP PROCEDURE dbo.del_education;
GO
CREATE PROCEDURE dbo.del_education
	@educationId INT
AS
BEGIN
	DELETE 
	FROM   D_EDUCATION
	WHERE  Id = @educationId
END
GO

/*******************************************
 * Редактировать образование
 *******************************************/
IF OBJECT_ID(N'dbo.update_education', N'P') IS NOT NULL
    DROP PROCEDURE dbo.update_education;
GO
CREATE PROCEDURE dbo.update_education
	@educationId INT,
	@year INT,
	@month INT,
	@groupName NVARCHAR(MAX),
	@html NVARCHAR(MAX),
	@pictureId UNIQUEIDENTIFIER,
	@order INT
AS
BEGIN
	UPDATE D_EDUCATION
	SET    [Year] = @year,
	       [Month] = @month,
	       GroupName = @groupName,
	       Html = @html,
	       PictureId = @pictureId,
	       DateUpdate = GETDATE(),
	       [Order] = @order
	WHERE  Id = @educationId
	
	EXEC dbo.get_education_by_key @educationId
END
GO

/*******************************************
 * Получить ситуацию
 *******************************************/
IF OBJECT_ID(N'dbo.get_situation_by_id', N'P') IS NOT NULL
    DROP PROCEDURE dbo.get_situation_by_id;
GO
CREATE PROCEDURE dbo.get_situation_by_id
	@situationId INT
AS
BEGIN
	SELECT *
	FROM   D_SITUATION AS ds
	WHERE  ds.Id = @situationId
END
GO

/*******************************************
 * Добавить ситуацию
 *******************************************/
IF OBJECT_ID(N'dbo.add_situation', N'P') IS NOT NULL
    DROP PROCEDURE dbo.add_situation;
GO
CREATE PROCEDURE dbo.add_situation
	@text NVARCHAR(MAX)
AS
BEGIN
	DECLARE @date DATETIME = GETDATE()
	
	INSERT INTO D_SITUATION
	  (
	    [Text],
	    DateUpdate,
	    DateCreate
	  )
	VALUES
	  (
	    @text,
	    @date,
	    @date
	  )
	
	DECLARE @id INT
	SELECT @id = @@identity
	
	EXEC dbo.get_situation_by_id @id
END
GO

/*******************************************
 * Обновить ситуацию
 *******************************************/
IF OBJECT_ID(N'dbo.update_situation', N'P') IS NOT NULL
    DROP PROCEDURE dbo.update_situation;
GO
CREATE PROCEDURE dbo.update_situation
	@situationId INT,
	@text NVARCHAR(MAX)
AS
BEGIN
	DECLARE @date DATETIME = GETDATE()
	
	UPDATE D_SITUATION
	SET    [Text] = @text,
	       DateUpdate = @date
	WHERE  Id = @situationId
	
	EXEC dbo.get_situation_by_id @situationId
END
GO

/*******************************************
 * Удалить ситуацию
 *******************************************/
IF OBJECT_ID(N'dbo.del_situation', N'P') IS NOT NULL
    DROP PROCEDURE dbo.del_situation;
GO
CREATE PROCEDURE dbo.del_situation
	@situationId INT
AS
BEGIN
	DELETE 
	FROM   D_SITUATION
	WHERE  Id = @situationId
END
GO

/*******************************************
 * sitemap
 *******************************************/
IF OBJECT_ID(N'dbo.get_valliulina_site_map', N'P') IS NOT NULL
    DROP PROCEDURE dbo.get_valliulina_site_map;
GO
CREATE PROCEDURE dbo.get_valliulina_site_map
AS
BEGIN
	SELECT dm.DateCreate,
	       dm.TitleUrl,
	       dm.ModelCoreType
	FROM   DV_MATERIAL AS dm
END
GO