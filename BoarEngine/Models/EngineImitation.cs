using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace BoarEngine.Models
{
    class EngineImitation
    {
        private bool running = false;

        public void run()
        {
            running = true;
            Debug.WriteLine("Started the engine");
        }

        public bool runnable()
        {
            return !running;
        }

        public void stop() 
        {
            running = false;
        }
    }
}
