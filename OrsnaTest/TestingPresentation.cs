using BD.Models;
using BD.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrsnaTest
{
    [TestClass]
    public class TestingPresentation
    {
        [TestMethod]
        public void TestProyectosGetAllFilter()
        {
                //page = 1 & FilterAeropuerto = &FilterIdProyecto = &FilterArea =,20 & FilterEstado = &FilterFecha = &Order = desc & ColumnOrder = Id
            BL.Proyecto.BLProyecto blproyecto = new BL.Proyecto.BLProyecto("data source=10.230.20.21;initial catalog=OrsnaDatabase;user id=rsaadmin;password=rsa123;MultipleActiveResultSets=True;", "UserTest");
            var proyectos = blproyecto.GetAll(1, "", "", ",18", null, 0,"", null, "asc", "Aeropuerto","", 0);
            Equals(proyectos);
        }
    }
}
