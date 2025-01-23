using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) {
        }
        public virtual DbSet<Tecnicos> Tecnicos { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
    }
}
