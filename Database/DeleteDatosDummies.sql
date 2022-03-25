  /*Delete libranzas*/
  delete from dbo.LibranzaDetalleWorkflow where IdLibranza IN (select id from dbo.Libranzas)
  delete from dbo.LibranzaBeneficiarios where IdLibranzas IN (select id from dbo.Libranzas)
  delete from dbo.LibranzaFacturas where IdLibranza IN (select id from dbo.Libranzas)
  delete from dbo.LibranzaAdjuntos where IdLibranza IN (select id from dbo.Libranzas)
  delete from dbo.Libranzas
  
  /*Delete Areas dummies*/
  delete from ProyectoAeropuertos where IdProyecto IN (select id from Proyectos where IdArea IN (select id from dbo.areas where nombre IN ('a-Area001','a-Area001','Area de Prueba','a-001','a-Area001','a-Area002','a-Area002')))
  GO
  delete from ProyectoAdjuntos where IdProyecto IN (select id from Proyectos where IdArea IN (select id from dbo.areas where nombre IN ('a-Area001','a-Area001','Area de Prueba','a-001','a-Area001','a-Area002','a-Area002')))
  GO
  delete from Proyectos where IdArea IN (select id from dbo.areas where nombre IN ('a-Area001','a-Area001','Area de Prueba','a-001','a-Area001','a-Area002','a-Area002'))
  GO
  delete from dbo.Areas where nombre IN ('a-Area001','a-Area001','Area de Prueba','a-001','a-Area001','a-Area002','a-Area002')
  

  /*delete proyectos*/
  delete from dbo.Proyectos where IdProyecto IN ('ID12','ID11','ID13','10')

  /*delete cuentas*/
  delete from dbo.Cuentas where Nombre IN ('NumeroCuenta10','NuevaCuenta12','NombreCuenta 11','NombreCuenta13','Cuenta001-e','Cuenta002-e','cuenta001')

  /*delete Beneficiarios*/
  delete from BeneficiarioBancos where IdBeneficiario IN (select IdBeneficiario from Beneficiarios where RazonSocial IN ('aaa-e','a-001','Razon social Nacional','p-RazónSocial11 (Nacional)'))
  delete from BeneficiarioAdjuntos where IdBeneficiario IN (select IdBeneficiario from Beneficiarios where RazonSocial IN ('aaa-e','a-001','Razon social Nacional','p-RazónSocial11 (Nacional)'))
  delete from Beneficiarios where RazonSocial IN ('aaa-e','a-001','Razon social Nacional','p-RazónSocial11 (Nacional)')

  delete from BeneficiarioBancos where IdBeneficiario IN (select IdBeneficiario from Beneficiarios )
  delete from BeneficiarioAdjuntos where IdBeneficiario IN (select IdBeneficiario from Beneficiarios )
  delete from Beneficiarios
  --delete from Beneficiarios where RazonSocial IN ('aaa-e','a-001','Razon social Nacional','p-RazónSocial11 (Nacional)')
