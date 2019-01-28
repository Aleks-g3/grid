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
        public HttpResponseMessage AddKUczniowie([FromBody]Uczniowie uczniowie)
        {
            using (SchoolEntities entities = new SchoolEntities())
            {
                if (uczniowie.Imie.Trim() == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "incomplete data");
                }
                else
                {
                    uczniowie.Id_Klasy = (entities.Klasas.Last() as Klasa).id;
                    entities.Uczniowies.Add(uczniowie);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Uczniowies);
                }

            }
        }
    }
}
