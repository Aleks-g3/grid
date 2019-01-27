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

                    entities.Wychowawcas.Add(wychowawca);
                    entities.SaveChanges();
                }
        }
    }
}
