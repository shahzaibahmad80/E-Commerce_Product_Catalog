USE [ECommerceDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('dbo.sp_AddProductImage', 'P') IS NOT NULL
 DROP PROCEDURE dbo.sp_AddProductImage
GO
CREATE PROCEDURE dbo.sp_AddProductImage
 (
 @ProductId INT,
 @ImageUrl NVARCHAR(500)
 )
AS
BEGIN
 SET NOCOUNT ON;
 INSERT INTO ProductImages (ProductId, ImageUrl)
 VALUES (@ProductId, @ImageUrl);
 SELECT SCOPE_IDENTITY() AS ImageId;
END
GO