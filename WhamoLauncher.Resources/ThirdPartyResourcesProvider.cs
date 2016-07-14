using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WhamoLauncher.Resources
{
    public static class ThirdPartyResourcesProvider
    {
        public static void CopyXlsTemplateTo(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhamoLauncher.Resources.xlsTemplate.xls"))
            {
                copyToFile(stream, path);
            }
        }

        public static void CopyWhamoExecutableTo(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhamoLauncher.Resources.Whamo.exe"))
            {
                copyToFile(stream, path);
            }
        }

        private static void copyToFile(Stream stream, string path)
        {
            Debug.Assert(path != null);

            using (var file = File.Create(path))
            {
                stream.CopyTo(file);
            }
        }
    }
}
