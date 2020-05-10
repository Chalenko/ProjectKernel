using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelTest
{
    class DebugException : Exception
    {
        public DebugException(string text) : base(text) { }

        public static void throwDebugException(string exMess)
        {
            throw new DebugException(exMess);
        }
    }
}
