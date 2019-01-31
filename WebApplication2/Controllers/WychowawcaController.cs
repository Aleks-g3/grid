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
        public void AddWychowawca([FromBody]Wychowawca wychowawca)
        {
           
                using (SchoolEntities entities = new SchoolEntities())
                {
                if (wychowawca.Imie.Trim() != string.Empty && wychowawca.Nazwisko.Trim() != string.Empty)
                {
                    entities.Wychowawcas.Add(wychowawca);
                    entities.SaveChanges();
                }
                    
                }
        }
        [HttpPut]
        public void UpdateWychowawca(int id,[FromBody]Wychowawca wychowawca)
        {
            using (SchoolEntities entities = new SchoolEntities())
            {

                var entity = entities.Wychowawcas.FirstOrDefault(w => w.id == id);
                if(wychowawca.Imie!=string.Empty && wychowawca.Nazwisko != string.Empty)
                {
                    entity.Imie = wychowawca.Imie;
                    entity.Nazwisko = wychowawca.Nazwisko;
                    entities.SaveChanges();
                }
                
            }
        }
        [HttpDelete]
        public void DeleteWychowawca(int id)
        {
            using(SchoolEntities entities=new SchoolEntities())
            {
                
                int idK = entities.Klasas.FirstOrDefault(k => k.Id_Wychowawca == id).id;
                var obj = entities.Uczniowies.Where(u => u.Id_Klasy == idK).ToList();
                entities.Uczniowies.RemoveRange(obj);
                entities.Klasas.Remove(entities.Klasas.FirstOrDefault(k=>k.Id_Wychowawca==id));
                entities.Wychowawcas.Remove(entities.Wychowawcas.FirstOrDefault(w => w.id == id));
                
                entities.SaveChanges();
                

            }
        }
    }
}
