using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBAccess;

namespace WebApplication2.Controllers
{
    public class UczniowieController : ApiController
    {
        [HttpGet]
        public List<Uczniowie> GetUczniowieId(int id)
        {
            
            using (SchoolEntities entities= new SchoolEntities())
            {
               return entities.Uczniowies.Where(u => u.Id_Klasy == id).ToList();
            }
        }
    }
}
