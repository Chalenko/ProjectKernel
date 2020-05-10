using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Exporter
{
    public interface IExporter
    {
        bool Export(IDataAccessor accessor, System.ComponentModel.BackgroundWorker bw = null);
    }

    public abstract class AExporter : IExporter
    {
        public System.ComponentModel.BackgroundWorker bgw;
        public abstract bool Export(IDataAccessor accessor, System.ComponentModel.BackgroundWorker bw);
    }
}
