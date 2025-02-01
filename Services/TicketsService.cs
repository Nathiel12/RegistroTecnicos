using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services
{
    public class TicketsService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Tickets ticket)
        {
            if (!await Existe(ticket.TicketId))
            {
                return await Insertar(ticket);
            }
            else
            {
                return await Modificar(ticket);
            }
        }

        public async Task<bool> Existe(int TicketId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets.AnyAsync(t => t.TicketId == TicketId);
        }

        private async Task<bool> Insertar(Tickets ticket)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tickets.Add(ticket);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tickets ticket)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(ticket);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Tickets?> Buscar(int TicketId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets.FirstOrDefaultAsync(t => t.TicketId == TicketId);
        }

        public async Task<bool> Eliminar(int TicketId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets.AsNoTracking().Where(t => t.TicketId == TicketId).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets.Where(criterio).Include(c => c.Cliente).Include(t => t.Tecnico).AsNoTracking().ToListAsync();
        }

    }

}
