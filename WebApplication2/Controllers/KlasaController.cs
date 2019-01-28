﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBAccess;

namespace WebApplication2.Controllers
{
    public class KlasaController : ApiController
    {
        [HttpGet]
        public List<Klasa> GetKlasa()
        {
            using (SchoolEntities entities = new SchoolEntities())
            {
                return entities.Klasas.ToList();
            }
        }
        [HttpGet]
        public List<Klasa> GetKlasaId(int id)
        {    
            using(SchoolEntities entities=new SchoolEntities())
            {
               return entities.Klasas.Where(k => k.Id_Wychowawca == id).ToList();
            }
        }
        [HttpPost]
        public HttpResponseMessage AddKlasa([FromBody]Klasa klasa)
        {
            using (SchoolEntities entities = new SchoolEntities())
            {
                
                if (klasa.Nazwa.Trim() == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "incomplete data");
                }
                else
                {
                    klasa.Id_Wychowawca = entities.Wychowawcas.OrderByDescending(w=>w.id).FirstOrDefault().id;
                    entities.Klasas.Add(klasa);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Klasas);
                }

            }
        }
    }
}
