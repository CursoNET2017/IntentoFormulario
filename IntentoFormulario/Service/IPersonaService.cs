using System.Linq;

namespace IntentoFormulario.Service
{
    public interface IPersonaService
    {
        Persona Create(Persona persona);
        Persona Delete(long id);
        Persona Get(long id);
        IQueryable<Persona> GetPersonas();
        void Put(Persona persona);
    }
}