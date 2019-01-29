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
        public List<Uczniowie> GetKlasa()
        {
            using (SchoolEntities entities = new SchoolEntities())
            {
                return entities.Uczniowies.ToList();
            }
        }
        [HttpGet]
        public List<Uczniowie> GetUczniowieId(int id)
        {
            
            using (SchoolEntities entities= new SchoolEntities())
            {
               return entities.Uczniowies.Where(u => u.Id_Klasy == id).ToList();
            }
        }
        [HttpPost]
        public void AddKUczniowie([FromBody]Uczniowie uczniowie)
        {
            using (SchoolEntities entities = new SchoolEntities())
            {
                if (uczniowie.Imie.Trim() != string.Empty && uczniowie.Nazwisko.Trim() != string.Empty)
                {
                    uczniowie.Id_Klasy = entities.Klasas.OrderByDescending(k => k.id).FirstOrDefault().id;
                    entities.Uczniowies.Add(uczniowie);
                    entities.SaveChanges();
                }

            }
        }
    }
}
