USE [OrsnaDatabase]
GO

UPDATE [dbo].[LibranzasEstado]
   SET [Nombre] = 'En elaboración'
      ,[Estado] = 1
 WHERE Id=1
GO
UPDATE [dbo].[LibranzasEstado]
   SET [Nombre] = 'Emitida (GAP)'
      ,[Estado] = 1
 WHERE Id=1
GO
UPDATE [dbo].[LibranzasEstado]
   SET [Nombre] = 'Pendiente de firma USG / DIR'
      ,[Estado] = 1
 WHERE Id=1
GO
UPDATE [dbo].[LibranzasEstado]
   SET [Nombre] = 'En el Ministerio'
      ,[Estado] = 1
 WHERE Id=1
GO
UPDATE [dbo].[LibranzasEstado]
   SET [Nombre] = 'Rechazada por el ministerio'
      ,[Estado] = 1
 WHERE Id=1
GO
UPDATE [dbo].[LibranzasEstado]
   SET [Nombre] = 'En el Banco'
      ,[Estado] = 1
 WHERE Id=1
GO
UPDATE [dbo].[LibranzasEstado]
   SET [Nombre] = 'Pagada'
      ,[Estado] = 1
 WHERE Id=1
GO
UPDATE [dbo].[LibranzasEstado]
   SET [Nombre] = 'Anulada'
      ,[Estado] = 1
 WHERE Id=1
GO
