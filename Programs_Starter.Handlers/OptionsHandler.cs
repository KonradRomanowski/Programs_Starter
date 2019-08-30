using Programs_Starter.Handlers.Base;
using Programs_Starter.Models;
using Programs_Starter.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Handlers
{
    public class OptionsHandler : BaseLoggingHandler
    {
        private const string NAME = "OptionsHandler";

        public static Option<bool> AutoStart { get; private set; }

        public static Option<int> SecondsToStartPrograms { get; private set; }

        public static Option<int> GapBetweenStartingPrograms { get; private set; }

        public List<BaseOption> Options { get; private set; }

        public OptionsHandler() : base(NAME)
        {
            Options = new List<BaseOption>();

            AutoStart = new Option<bool>("AutoStart", false);
            Options.Add(AutoStart);

            SecondsToStartPrograms = new Option<int>("SecondsToStart", 4);
            Options.Add(SecondsToStartPrograms);

            GapBetweenStartingPrograms = new Option<int>("GapBetweenStartingPrograms", 2);
            Options.Add(GapBetweenStartingPrograms);

            foreach (var item in Options)
            {
                if (item.Name == "AutoStart")
                {
                    Option<bool> option = (Option<bool>)item;
                    option.SetValue(true);
                }
            }
        }
    }
}
