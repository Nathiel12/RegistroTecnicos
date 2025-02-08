using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class SistemasService(IDbContextFactory<Contexto> DbFactory)
    {
        /// <summary>
        /// Guarda un sistema en la base de datos. Si el sistema ya existe, lo modifica; si no, lo inserta.
        /// </summary>
        /// <param name="sistema">Instancia del sistema a guardar.</param>
        /// <returns>Devuelve true si la operación fue exitosa, false en caso contrario.</returns>
        public async Task<bool> Guardar(Sistemas sistema)
        {
            if(!await Existe(sistema.SistemaId))
            {
                return await Insertar(sistema);
            }
            else
            {
                return await Modificar(sistema);
            }
        }
        /// <summary>
        /// Verifica si un sistema con el ID especificado ya existe en la base de datos.
        /// </summary>
        /// <param name="SistemaId">ID del sistema a verificar.</param>
        /// <returns>Devuelve true si el sistema existe, false en caso contrario.</returns>
        public async Task<bool> Existe(int SistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas.AnyAsync(s => s.SistemaId == SistemaId);
        }
        /// <summary>
        /// Inserta un nuevo sistema en la base de datos.
        /// </summary>
        /// <param name="sistema">Instancia del sistema a insertar.</param>
        /// <returns>Devuelve true si la operación fue exitosa, false en caso contrario.</returns>
        private async Task<bool> Insertar(Sistemas sistema)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Sistemas.Add(sistema);
            return await contexto.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Modifica un sistema existente en la base de datos.
        /// </summary>
        /// <param name="sistema">Instancia del sistema con los datos actualizados.</param>
        /// <returns>Devuelve true si la modificación fue exitosa, false en caso contrario.</returns>
        private async Task<bool> Modificar(Sistemas sistema)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(sistema);
            return await contexto.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Busca un sistema en la base de datos por su ID.
        /// </summary>
        /// <param name="SistemaId">ID del sistema a buscar.</param>
        /// <returns>Devuelve la instancia del sistema si se encuentra, null en caso contrario.</returns>
        public async Task<Sistemas?> Buscar(int SistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas.FirstOrDefaultAsync(s => s.SistemaId == SistemaId);
        }
        /// <summary>
        /// Elimina un sistema de la base de datos por su ID.
        /// </summary>
        /// <param name="SistemaId">ID del sistema a eliminar.</param>
        /// <returns>Devuelve true si la eliminación fue exitosa, false en caso contrario.</returns>
        public async Task<bool> Eliminar(int SistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas.AsNoTracking().Where(s => s.SistemaId == SistemaId).ExecuteDeleteAsync() > 0;
        }
        /// <summary>
        /// Lista los sistemas que cumplen con un criterio específico.
        /// </summary>
        /// <param name="criterio">Expresión lambda que define el filtro de búsqueda.</param>
        /// <returns>Devuelve una lista de sistemas que cumplen con el criterio.</returns>
        public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistemas.AsNoTracking().Where(criterio).ToListAsync();
        }

    }
}
