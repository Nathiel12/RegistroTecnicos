using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class TecnicosService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Tecnicos tecnico)
        {
            if (!await Existe(tecnico.TecnicoId))
            {
                return await Insertar(tecnico);
            }
            else
            {
                return await Modificar(tecnico);
            }
        }

        public async Task<bool> Existe(int TecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos.AnyAsync(t => t.TecnicoId == TecnicoId);
        }

        public async Task<bool> ExisteNombre(string Nombres)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos.AnyAsync(t => t.Nombres.ToLower() == Nombres.ToLower());
        }

        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tecnicos.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            await using var contexto= await DbFactory.CreateDbContextAsync();
            contexto.Update(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Tecnicos?> Buscar(int TecnicoId)
        {
            await using var contexto= await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos.FirstOrDefaultAsync(t => t.TecnicoId == TecnicoId);
        }

        public async Task<bool> Eliminar(int TecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos.AsNoTracking().Where(t => t.TecnicoId == TecnicoId).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos.Where(criterio).AsNoTracking().ToListAsync();
        }

    }
}
