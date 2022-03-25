USE [OrsnaDatabase]
GO

INSERT INTO [dbo].[LibranzasEstado]
           ([Nombre]
           ,[Estado])
     VALUES
           ('En elaboración'
           ,1),('Emitida (GAP)'
           ,1),('Pendiente de firma USG / DIR'
           ,1),('En el Banco'
           ,1),('En el Ministerio'
           ,1),('Rechazada por el ministerio'
           ,1),('Pagada'
           ,1),('Anulada'
           ,1)
GO