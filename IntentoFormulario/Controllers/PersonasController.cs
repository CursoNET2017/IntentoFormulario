using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using IntentoFormulario;
using IntentoFormulario.Models;
using IntentoFormulario.Service;

namespace IntentoFormulario.Controllers
{
    public class PersonasController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IPersonaService personaService;

        public PersonasController(IPersonaService _personaService)
        {
            this.personaService = _personaService;
        }

        // GET: api/Personas
        public IQueryable<Persona> GetPersonas()
        {
            return personaService.GetPersonas();
        }

        // GET: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(long id)
        {
            Persona persona = personaService.Get(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/Personas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersona(long id, Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Id)
            {
                return BadRequest();
            }            
            try
            {
                personaService.Put(persona);
            }
            catch (NoEncontradoException)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Personas
        [ResponseType(typeof(Persona))]
        public IHttpActionResult PostPersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            persona = personaService.Create(persona);

            return CreatedAtRoute("DefaultApi", new { id = persona.Id }, persona);
        }

        // DELETE: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(long id)
        {
            Persona persona;
            try
            {
                persona = personaService.Delete(id);
            } catch (NoEncontradoException)
            {
                return NotFound();
            }

            return Ok(persona);
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(long id)
        {
            return db.Personas.Count(e => e.Id == id) > 0;
        }
        */
    }
}