using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using GIBS.Module.QRCodeMaker.Repository;

namespace GIBS.Module.QRCodeMaker.Services
{
    public class ServerQRCodeMakerService : IQRCodeMakerService
    {
        private readonly IQRCodeMakerRepository _QRCodeMakerRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerQRCodeMakerService(IQRCodeMakerRepository QRCodeMakerRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _QRCodeMakerRepository = QRCodeMakerRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.QRCodeMaker>> GetQRCodeMakersAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_QRCodeMakerRepository.GetQRCodeMakers(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.QRCodeMaker> GetQRCodeMakerAsync(int QRCodeMakerId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_QRCodeMakerRepository.GetQRCodeMaker(QRCodeMakerId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Get Attempt {QRCodeMakerId} {ModuleId}", QRCodeMakerId, ModuleId);
                return null;
            }
        }

        public Task<Models.QRCodeMaker> AddQRCodeMakerAsync(Models.QRCodeMaker QRCodeMaker)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, QRCodeMaker.ModuleId, PermissionNames.Edit))
            {
                QRCodeMaker = _QRCodeMakerRepository.AddQRCodeMaker(QRCodeMaker);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "QRCodeMaker Added {QRCodeMaker}", QRCodeMaker);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Add Attempt {QRCodeMaker}", QRCodeMaker);
                QRCodeMaker = null;
            }
            return Task.FromResult(QRCodeMaker);
        }

        public Task<Models.QRCodeMaker> UpdateQRCodeMakerAsync(Models.QRCodeMaker QRCodeMaker)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, QRCodeMaker.ModuleId, PermissionNames.Edit))
            {
                QRCodeMaker = _QRCodeMakerRepository.UpdateQRCodeMaker(QRCodeMaker);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "QRCodeMaker Updated {QRCodeMaker}", QRCodeMaker);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Update Attempt {QRCodeMaker}", QRCodeMaker);
                QRCodeMaker = null;
            }
            return Task.FromResult(QRCodeMaker);
        }

        public Task DeleteQRCodeMakerAsync(int QRCodeMakerId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _QRCodeMakerRepository.DeleteQRCodeMaker(QRCodeMakerId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "QRCodeMaker Deleted {QRCodeMakerId}", QRCodeMakerId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Delete Attempt {QRCodeMakerId} {ModuleId}", QRCodeMakerId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
