using IntentoFormulario.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntentoFormulario.Service
{
    public class PersonaService : IPersonaService
    {
        //[ThreadStatic] public static ApplicationDbContext applicationDbContext;// Para poder usarlo en el repository

        private IPersonaRepository personaRepository;
        public PersonaService(IPersonaRepository _personaRepository)
        {
            this.personaRepository = _personaRepository;
        }

        public Persona Get(long id)
        {            
            return personaRepository.Get(id);
        }

        public Persona Create(Persona persona)
        {
            return personaRepository.Create(persona);
        }


        public IQueryable<Persona> GetPersonas()
        {
            return personaRepository.GetPersonas();
        }

        public Persona Delete(long id)
        {
            
            return personaRepository.Delete(id);
        }

        public void Put(Persona persona)
        {
            personaRepository.Put(persona);
        }
    }
}