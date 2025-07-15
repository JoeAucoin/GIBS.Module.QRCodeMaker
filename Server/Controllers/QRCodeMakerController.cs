using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.QRCodeMaker.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace GIBS.Module.QRCodeMaker.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class QRCodeMakerController : ModuleControllerBase
    {
        private readonly IQRCodeMakerService _QRCodeMakerService;

        public QRCodeMakerController(IQRCodeMakerService QRCodeMakerService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _QRCodeMakerService = QRCodeMakerService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.QRCodeMaker>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _QRCodeMakerService.GetQRCodeMakersAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.QRCodeMaker> Get(int id, int moduleid)
        {
            Models.QRCodeMaker QRCodeMaker = await _QRCodeMakerService.GetQRCodeMakerAsync(id, moduleid);
            if (QRCodeMaker != null && IsAuthorizedEntityId(EntityNames.Module, QRCodeMaker.ModuleId))
            {
                return QRCodeMaker;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Get Attempt {QRCodeMakerId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.QRCodeMaker> Post([FromBody] Models.QRCodeMaker QRCodeMaker)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, QRCodeMaker.ModuleId))
            {
                QRCodeMaker = await _QRCodeMakerService.AddQRCodeMakerAsync(QRCodeMaker);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Post Attempt {QRCodeMaker}", QRCodeMaker);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                QRCodeMaker = null;
            }
            return QRCodeMaker;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.QRCodeMaker> Put(int id, [FromBody] Models.QRCodeMaker QRCodeMaker)
        {
            if (ModelState.IsValid && QRCodeMaker.QRCodeMakerId == id && IsAuthorizedEntityId(EntityNames.Module, QRCodeMaker.ModuleId))
            {
                QRCodeMaker = await _QRCodeMakerService.UpdateQRCodeMakerAsync(QRCodeMaker);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Put Attempt {QRCodeMaker}", QRCodeMaker);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                QRCodeMaker = null;
            }
            return QRCodeMaker;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.QRCodeMaker QRCodeMaker = await _QRCodeMakerService.GetQRCodeMakerAsync(id, moduleid);
            if (QRCodeMaker != null && IsAuthorizedEntityId(EntityNames.Module, QRCodeMaker.ModuleId))
            {
                await _QRCodeMakerService.DeleteQRCodeMakerAsync(id, QRCodeMaker.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized QRCodeMaker Delete Attempt {QRCodeMakerId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
