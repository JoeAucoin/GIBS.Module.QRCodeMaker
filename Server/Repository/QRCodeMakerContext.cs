using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace GIBS.Module.QRCodeMaker.Repository
{
    public class QRCodeMakerContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.QRCodeMaker> QRCodeMaker { get; set; }

        public QRCodeMakerContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.QRCodeMaker>().ToTable(ActiveDatabase.RewriteName("GIBSQRCodeMaker"));
        }
    }
}
