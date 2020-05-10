using ProjectKernel.Classes.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FinPlanUZD.Classes.Exporter
{
    abstract class AFileExporter : AExporter
    {
        public string FileName { get; private set; }
        
        //private int totalRowCount;

        public AFileExporter(string fileName) : base()
        {
            FileName = fileName;
        }

        public override bool Export(IDataAccessor accessor, System.ComponentModel.BackgroundWorker bw)
        {
            bool error = false;

            bgw = bw;

            try
            {
                OpenApp();

                PrepareApp();

                PrintData(accessor);

                SaveFile();

                ShowApp();
            }
            catch (Exception ex)
            {
                FileLogger.Instance.error("Ошибка при экспорте. Нет доступа к файлу. " + ex.ToString());
                System.IO.File.Delete(FileName);
                error = true;
            }

            return !error;
        }

        protected abstract void OpenApp();
        protected abstract void PrepareApp();
        protected abstract void ShowApp();
        protected abstract void PrintData(IDataAccessor accessor);
        protected abstract void SaveFile();
        protected abstract void CloseApp();
    }
}
