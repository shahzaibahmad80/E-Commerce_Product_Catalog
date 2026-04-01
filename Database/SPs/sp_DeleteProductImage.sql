USE [ECommerceDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('dbo.sp_DeleteProductImage', 'P') IS NOT NULL
 DROP PROCEDURE dbo.sp_DeleteProductImage
GO
CREATE PROCEDURE dbo.sp_DeleteProductImage
 (
 @ImageId INT,
 @DeletedUrl NVARCHAR(500) OUTPUT
 )
AS
BEGIN
 SET NOCOUNT ON;
 SELECT @DeletedUrl = ImageUrl FROM ProductImages WHERE ImageId = @ImageId;
 DELETE FROM ProductImages WHERE ImageId = @ImageId;
END
GO