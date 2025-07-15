using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.QRCodeMaker.Services
{
    public interface IQRCodeMakerService 
    {
        Task<List<Models.QRCodeMaker>> GetQRCodeMakersAsync(int ModuleId);

        Task<Models.QRCodeMaker> GetQRCodeMakerAsync(int QRCodeMakerId, int ModuleId);

        Task<Models.QRCodeMaker> AddQRCodeMakerAsync(Models.QRCodeMaker QRCodeMaker);

        Task<Models.QRCodeMaker> UpdateQRCodeMakerAsync(Models.QRCodeMaker QRCodeMaker);

        Task DeleteQRCodeMakerAsync(int QRCodeMakerId, int ModuleId);
    }
}
