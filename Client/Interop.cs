using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace GIBS.Module.QRCodeMaker
{
    public class Interop
    {
        private readonly IJSRuntime _jsRuntime;

        public Interop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
    }
}
