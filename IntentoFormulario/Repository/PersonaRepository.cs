using IntentoFormulario.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IntentoFormulario.Repository
{
    public class PersonaRepository : IPersonaRepository
    {
        public Persona Get(long id)
        {
            return ApplicationDbContext.applicationDbContext.Personas.Find(id);
        }

        public Persona Create(Persona persona)
        {
            return ApplicationDbContext.applicationDbContext.Personas.Add(persona);
        }

        public IQueryable<Persona> GetPersonas()
        {
            IList<Persona> lista = new List<Persona>(ApplicationDbContext.applicationDbContext.Personas);
            return lista.AsQueryable();//Si devuelves el IQueryable casca en el lado del cliente.
        }

        public Persona Delete(long id)
        {
            Persona persona = ApplicationDbContext.applicationDbContext.Personas.Find(id);
            if (persona == null)
            {
                throw new NoEncontradoException("No he encontrado la entidad");
            }
            ApplicationDbContext.applicationDbContext.Personas.Remove(persona);
            return persona;
        }

        public void Put(Persona persona)
        {
            if (ApplicationDbContext.applicationDbContext.Personas.Count(e => e.Id == persona.Id) == 0)// El private bool PersonaExists(long id) del anterior controlador
            {
                throw new NoEncontradoException("No he encontrado la entidad");
            }
            ApplicationDbContext.applicationDbContext.Entry(persona).State = EntityState.Modified;
        }
    }
}