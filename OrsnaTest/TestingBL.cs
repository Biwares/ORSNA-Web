using BD.Models;
using BD.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace OrsnaTest
{
    [TestClass]
    public class TestingBL
    {
        [TestMethod]
        public void TestProyectosGetAll()
        {
            BL.Proyecto.BLProyecto blproyecto = new BL.Proyecto.BLProyecto("data source=10.230.20.21;initial catalog=OrsnaDatabase;user id=rsaadmin;password=rsa123;MultipleActiveResultSets=True;", "UserTest");
            ICollection<VMProyecto> proyectos = blproyecto.GetAll();
            Equals(proyectos);
        }
        [TestMethod]
        public void TestProyectosGetAllFilter()
        {
            BL.Proyecto.BLProyecto blproyecto = new BL.Proyecto.BLProyecto("data source=10.230.20.21;initial catalog=OrsnaDatabase;user id=rsaadmin;password=rsa123;MultipleActiveResultSets=True;", "UserTest");
            ICollection<VMProyecto> proyectos = blproyecto.GetAll(1, "", "", ",20", null, 0, "", null, "asc", "Aeropuerto", "", 0);
            Equals(proyectos);
        }
        [TestMethod]
        public void TestGetSiguientesEstados()
        {
            BL.Libranza.BLLibranza le = new BL.Libranza.BLLibranza("data source=10.230.20.21;initial catalog=OrsnaDatabase;user id=rsaadmin;password=rsa123;MultipleActiveResultSets=True;", "UserTest");
            ICollection<VMLibranzasEstado> estados = le.GetSiguientesEstado(3);
            Equals(estados);
        }

        [TestMethod]
        public void TestAuthUser()
        {
            string path = "LDAP://tamashi.orsna.gov.ar/CN=Users,DC=orsna,DC=gov,DC=ar";
            string strAccountId = "hypusrtest";
            string strPassword = "sarasa"; //73stHy93R!0n 
            string strError = "";
            bool bSucceeded = false;

            using (DirectoryEntry adsEntry = new DirectoryEntry(path, strAccountId, strPassword))
            {
                using (DirectorySearcher adsSearcher = new DirectorySearcher(adsEntry))
                {
                    adsSearcher.Filter = "(sAMAccountName=" + strAccountId + ")";

                    try
                    {
                        SearchResult adsSearchResult = adsSearcher.FindOne();
                        bSucceeded = true;
                    }
                    catch (Exception ex)
                    {
                        strError = ex.Message;
                    }
                    finally
                    {
                        adsEntry.Close();
                    }
                }
            }
        }

    }
}
