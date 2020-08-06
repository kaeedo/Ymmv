using System;
using System.IO;

namespace Ymmv
{
    public static class Constants
    {
        public static string DatabaseFilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ymmv.db3");
    }
}
