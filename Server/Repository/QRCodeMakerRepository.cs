using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace GIBS.Module.QRCodeMaker.Repository
{
    public class QRCodeMakerRepository : IQRCodeMakerRepository, ITransientService
    {
        private readonly IDbContextFactory<QRCodeMakerContext> _factory;

        public QRCodeMakerRepository(IDbContextFactory<QRCodeMakerContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.QRCodeMaker> GetQRCodeMakers(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.QRCodeMaker.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.QRCodeMaker GetQRCodeMaker(int QRCodeMakerId)
        {
            return GetQRCodeMaker(QRCodeMakerId, true);
        }

        public Models.QRCodeMaker GetQRCodeMaker(int QRCodeMakerId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.QRCodeMaker.Find(QRCodeMakerId);
            }
            else
            {
                return db.QRCodeMaker.AsNoTracking().FirstOrDefault(item => item.QRCodeMakerId == QRCodeMakerId);
            }
        }

        public Models.QRCodeMaker AddQRCodeMaker(Models.QRCodeMaker QRCodeMaker)
        {
            using var db = _factory.CreateDbContext();
            db.QRCodeMaker.Add(QRCodeMaker);
            db.SaveChanges();
            return QRCodeMaker;
        }

        public Models.QRCodeMaker UpdateQRCodeMaker(Models.QRCodeMaker QRCodeMaker)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(QRCodeMaker).State = EntityState.Modified;
            db.SaveChanges();
            return QRCodeMaker;
        }

        public void DeleteQRCodeMaker(int QRCodeMakerId)
        {
            using var db = _factory.CreateDbContext();
            Models.QRCodeMaker QRCodeMaker = db.QRCodeMaker.Find(QRCodeMakerId);
            db.QRCodeMaker.Remove(QRCodeMaker);
            db.SaveChanges();
        }
    }
}
