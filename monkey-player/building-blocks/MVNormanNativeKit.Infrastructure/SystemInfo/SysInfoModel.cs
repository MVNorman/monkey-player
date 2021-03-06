using System.Collections.Generic;

namespace MVNormanNativeKit.Infrastructure.SystemInfo
{
    public class SysInfoModel
    {
        public string OSArchitecture { get; set; }
        public string OSDescription { get; set; }
        public string ProcessArchitecture { get; set; }
        public string BasePath { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public string AssemblyVersion { get; set; }
        public string RuntimeFramework { get; set; }
        public string FrameworkDescription { get; set; }
        public Dictionary<string, object> Envs { get; set; }
    }
}
