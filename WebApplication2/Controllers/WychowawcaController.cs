using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBAccess;

namespace WebApplication2.Controllers
{
    public class WychowawcaController : ApiController
    {
        [HttpGet]
        public List<Wychowawca> GetWychowawca()
        {
            using(SchoolEntities entities=new SchoolEntities())
            {
                return entities.Wychowawcas.ToList();
            }
        }
        [HttpPost]
        public HttpResponseMessage AddWychowawca([FromBody]Wychowawca wychowawca)
        {
            string i = wychowawca.Imie.Trim();
            string j = wychowawca.Nazwisko.Trim();
                using (SchoolEntities entities = new SchoolEntities())
                {
                if (wychowawca.Imie.Trim()==null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "incomplete data");
                }
                else
                {
                    entities.Wychowawcas.Add(wychowawca);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Wychowawcas);
                }
                    
                }
        }
        [HttpPut]
        public HttpResponseMessage UpdateWychowawca(int id,[FromBody]Wychowawca wychowawca)
        {
            using (SchoolEntities entities = new SchoolEntities())
            {

                var entity = entities.Wychowawcas.FirstOrDefault(w => w.id == id);
                if(wychowawca.Imie==null || wychowawca.Nazwisko == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"incomplete data");
                }
                else
                {
                    entity.Imie = wychowawca.Imie;
                    entity.Nazwisko = wychowawca.Nazwisko;
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                
            }
        }
        [HttpDelete]
        public void DeleteWychowawca(int id)
        {
            using(SchoolEntities entities=new SchoolEntities())
            {
                //int idK = (entities.Klasas.Where(k => k.Id_Wychowawca == id).First()).id;
                entities.Wychowawcas.Remove(entities.Wychowawcas.FirstOrDefault(w => w.id == id));
                IEnumerable<Klasa> kl = new List<Klasa>();
                kl = (entities.Klasas.Where(k => k.Id_Wychowawca == id)).ToList();
                entities.Klasas.RemoveRange(kl);
                //entities.Uczniowies.Remove(entities.Uczniowies.Where(u => u.id == idK).First());
                entities.SaveChanges();
            }
        }
    }
}
