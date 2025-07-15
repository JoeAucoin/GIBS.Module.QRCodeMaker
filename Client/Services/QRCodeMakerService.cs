using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace GIBS.Module.QRCodeMaker.Services
{
    public class QRCodeMakerService : ServiceBase, IQRCodeMakerService
    {
        public QRCodeMakerService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("QRCodeMaker");

        public async Task<List<Models.QRCodeMaker>> GetQRCodeMakersAsync(int ModuleId)
        {
            List<Models.QRCodeMaker> QRCodeMakers = await GetJsonAsync<List<Models.QRCodeMaker>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.QRCodeMaker>().ToList());
            return QRCodeMakers.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.QRCodeMaker> GetQRCodeMakerAsync(int QRCodeMakerId, int ModuleId)
        {
            return await GetJsonAsync<Models.QRCodeMaker>(CreateAuthorizationPolicyUrl($"{Apiurl}/{QRCodeMakerId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.QRCodeMaker> AddQRCodeMakerAsync(Models.QRCodeMaker QRCodeMaker)
        {
            return await PostJsonAsync<Models.QRCodeMaker>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, QRCodeMaker.ModuleId), QRCodeMaker);
        }

        public async Task<Models.QRCodeMaker> UpdateQRCodeMakerAsync(Models.QRCodeMaker QRCodeMaker)
        {
            return await PutJsonAsync<Models.QRCodeMaker>(CreateAuthorizationPolicyUrl($"{Apiurl}/{QRCodeMaker.QRCodeMakerId}", EntityNames.Module, QRCodeMaker.ModuleId), QRCodeMaker);
        }

        public async Task DeleteQRCodeMakerAsync(int QRCodeMakerId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{QRCodeMakerId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
