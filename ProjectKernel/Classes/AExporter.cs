using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinPlanUZD.Classes.Exporter
{
    interface IExporter
    {
        bool Export(IDataAccessor accessor, System.ComponentModel.BackgroundWorker bw = null);
    }

    abstract class AExporter : IExporter
    {
        public System.ComponentModel.BackgroundWorker bgw;
        public abstract bool Export(IDataAccessor accessor, System.ComponentModel.BackgroundWorker bw);
    }
}
