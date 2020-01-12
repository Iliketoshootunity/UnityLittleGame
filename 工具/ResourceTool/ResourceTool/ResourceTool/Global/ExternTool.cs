using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckResourceTool
{
    class ExternTool
    {
        private static ExternTool _Instance;
        public static ExternTool Instance { get { _Instance = _Instance ?? new ExternTool(); return _Instance; } }

        private string path_7z;
        public string _7zPath { get { path_7z = path_7z ?? Global.Instance.FixedDirectory + @"7-Zip/7z.exe"; return path_7z; } }//7-zip.exe路径

        private string path_autozippath;
        public string _AutoGenerateResourceZIPsPath { get { path_autozippath = path_autozippath ?? Global.Instance.FixedDirectory + @"AutoGenerateResourceZIPs/AutoGenerateResourceZIPs.exe"; return path_autozippath; } }//自动打包资源工具路径
    }
}
