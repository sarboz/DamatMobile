using System;
using System.IO;
using DamatMobile.Core.Abstractions;
using SQLitePCL;
using Xamarin.Forms;

namespace DamatMobile.Ui.Facades
{
    public class DatabasePathProvider:IDatabasePathProvider
    {
        private const string DataBaseFileName = "database.db";
        public string GetDatabasePath()
        {
            string directoryPath;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Batteries_V2.Init();
                    directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..",
                        "Library");
                    break;
                case Device.Android:
                    Batteries_V2.Init();
                    directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    break;

                default:
                    directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    break;
            }
            var dataBasePath = Path.Combine(directoryPath, DataBaseFileName);
            return dataBasePath;
        }
    }
}