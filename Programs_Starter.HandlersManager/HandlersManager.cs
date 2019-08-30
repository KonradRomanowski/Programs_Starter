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
        public static OptionsHandler OptionsHandler = new OptionsHandler();

        public static void LoadProgramsToStartFromConfig()
        {
            StartingProgramsHandler.InitializeProgramsToStartDictionary(XMLConfigHandler.ReadProgramsToStartFromConfig());
        }

        public static void SaveProgramsToStartToConfig()
        {
            XMLConfigHandler.SaveProgramsToStartDict(StartingProgramsHandler.ProgramsToStart);
        }
    }
}
