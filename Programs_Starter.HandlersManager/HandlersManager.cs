using Programs_Starter.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.HandlersManaging
{
    public static class HandlersManager
    {
        public static XMLConfigHandler XMLConfigHandler = new XMLConfigHandler();
        public static StartingProgramsHandler StartingProgramsHandler = new StartingProgramsHandler();

        public static void LoadProgramsToStartFromConfig()
        {

        }
    }
}
