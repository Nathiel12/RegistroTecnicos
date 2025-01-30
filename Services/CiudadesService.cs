using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class CiudadesService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Ciudades ciudad)
        {
            if (!await Existe(ciudad.CiudadId))
            {
                return await Insertar(ciudad);
            }
            else
            {
                return await Modificar(ciudad);
            }
        }

        public async Task<bool> Existe(int CiudadId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades.AnyAsync(c => c.CiudadId == CiudadId);
        }

        public async Task<bool> ExisteNombre(string Nombre)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades.AnyAsync(c => c.Nombre.ToLower() == Nombre.ToLower());
        }

        private async Task<bool> Insertar(Ciudades ciudad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Ciudades.Add(ciudad);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Ciudades ciudad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(ciudad);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Ciudades?> Buscar(int CiudadId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades.FirstOrDefaultAsync(c => c.CiudadId == CiudadId);
        }

        public async Task<bool> Eliminar(int CiudadId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades.AsNoTracking().Where(c => c.CiudadId == CiudadId).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Ciudades.Where(criterio).AsNoTracking().ToListAsync();
        }

    }
}

