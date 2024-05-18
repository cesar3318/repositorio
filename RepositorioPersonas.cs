using Catalogo.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Repositorio
{
	public class RepositorioPersonas : IRepositorioPersonas
	{
		private readonly CatalogoDBContext _context;

		public RepositorioPersonas(CatalogoDBContext context)
		{
			_context = context;
		}
		public async Task<Persona> Add(Persona persona)
		{
			await _context.Personas.AddAsync(persona);
			await _context.SaveChangesAsync();
			return persona;
		}
		public async Task Delete(int id)
		{
			var persona = await _context.Personas.FindAsync(id);
			if (persona != null)
			{
				_context.Personas.Remove(persona);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<Persona?> Get(int id)
		{

			return await _context.Personas.Include(h => h.Habitos).Where(r => r.Id == id).FirstOrDefaultAsync();
		}
		public async Task<List<Persona>> GetAll()
		{
			return await _context.Personas.ToListAsync();
		}

		public async Task<List<Visual>> GetVisual()
		{
			return await _context.Visualizaciones.ToListAsync();
		}

        public async Task<List<Contenido>> GetContenido()
        {
            return await _context.Contenidos.ToListAsync();
        }
        public async Task Update(int id, Persona persona)
		{
			var personaactual = await _context.Personas.FindAsync(id);
			if(personaactual != null)
			{
				personaactual.Nombre = persona.Nombre;
				personaactual.Correo = persona.Correo;
				personaactual.Telefono = persona.Telefono;
				personaactual.Edad = persona.Edad;
				personaactual.Sexo = persona.Sexo;
				await _context.SaveChangesAsync();
			}

		}



	}
}

