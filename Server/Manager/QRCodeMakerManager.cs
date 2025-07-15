using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using GIBS.Module.QRCodeMaker.Repository;
using System.Threading.Tasks;

namespace GIBS.Module.QRCodeMaker.Manager
{
    public class QRCodeMakerManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IQRCodeMakerRepository _QRCodeMakerRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public QRCodeMakerManager(IQRCodeMakerRepository QRCodeMakerRepository, IDBContextDependencies DBContextDependencies)
        {
            _QRCodeMakerRepository = QRCodeMakerRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new QRCodeMakerContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new QRCodeMakerContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.QRCodeMaker> QRCodeMakers = _QRCodeMakerRepository.GetQRCodeMakers(module.ModuleId).ToList();
            if (QRCodeMakers != null)
            {
                content = JsonSerializer.Serialize(QRCodeMakers);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.QRCodeMaker> QRCodeMakers = null;
            if (!string.IsNullOrEmpty(content))
            {
                QRCodeMakers = JsonSerializer.Deserialize<List<Models.QRCodeMaker>>(content);
            }
            if (QRCodeMakers != null)
            {
                foreach(var QRCodeMaker in QRCodeMakers)
                {
                    _QRCodeMakerRepository.AddQRCodeMaker(new Models.QRCodeMaker { ModuleId = module.ModuleId, Name = QRCodeMaker.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var QRCodeMaker in _QRCodeMakerRepository.GetQRCodeMakers(pageModule.ModuleId))
           {
               if (QRCodeMaker.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "GIBSQRCodeMaker",
                       EntityId = QRCodeMaker.QRCodeMakerId.ToString(),
                       Title = QRCodeMaker.Name,
                       Body = QRCodeMaker.Name,
                       ContentModifiedBy = QRCodeMaker.ModifiedBy,
                       ContentModifiedOn = QRCodeMaker.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
