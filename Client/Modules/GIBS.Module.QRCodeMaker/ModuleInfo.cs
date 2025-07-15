using Oqtane.Models;
using Oqtane.Modules;

namespace GIBS.Module.QRCodeMaker
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "QRCodeMaker",
            Description = "QR Code Maker Module for Oqtane",
            Version = "1.0.0",
            ServerManagerType = "GIBS.Module.QRCodeMaker.Manager.QRCodeMakerManager, GIBS.Module.QRCodeMaker.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "GIBS.Module.QRCodeMaker.Shared.Oqtane,QRCoder",
            PackageName = "GIBS.Module.QRCodeMaker" 
        };
    }
}

