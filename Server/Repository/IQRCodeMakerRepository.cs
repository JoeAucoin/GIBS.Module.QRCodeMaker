using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.QRCodeMaker.Repository
{
    public interface IQRCodeMakerRepository
    {
        IEnumerable<Models.QRCodeMaker> GetQRCodeMakers(int ModuleId);
        Models.QRCodeMaker GetQRCodeMaker(int QRCodeMakerId);
        Models.QRCodeMaker GetQRCodeMaker(int QRCodeMakerId, bool tracking);
        Models.QRCodeMaker AddQRCodeMaker(Models.QRCodeMaker QRCodeMaker);
        Models.QRCodeMaker UpdateQRCodeMaker(Models.QRCodeMaker QRCodeMaker);
        void DeleteQRCodeMaker(int QRCodeMakerId);
    }
}
