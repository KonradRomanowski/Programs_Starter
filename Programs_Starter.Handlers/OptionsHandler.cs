using Programs_Starter.Handlers.Base;
using Programs_Starter.Models;
using Programs_Starter.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        /// <summary>
        /// Updates multiple options on Options list
        /// </summary>
        /// <param name="optionsToUpdate">Dictionary of key value pairs with new values for options</param>
        public void UpdateMultipleOptionsOnOptionsList(Dictionary<string, string> optionsToUpdate)
        {
            if (optionsToUpdate == null)
            {
                Logger.DoErrorLogKV("UpdateOptionsListWithUserValues called with null - using default options values");
                return;
            }

            foreach (var option in optionsToUpdate)
            {
                TryToUpdateOptionValue(option.Key, option.Value);
            }
        }

        /// <summary>
        /// Tries to update option value depending on its type
        /// </summary>
        /// <param name="optionName">Name of option to update</param>
        /// <param name="optionValue">New value for option, will be parsed to proper type</param>
        public void TryToUpdateOptionValue(string optionName, string optionValue)
        {
            if (string.IsNullOrWhiteSpace(optionName) || string.IsNullOrWhiteSpace(optionValue))
            {
                Logger.DoErrorLogKV("TryToUpdateOptionValue called with null, empty or white space parameter!", 
                    "Name", optionName,"Value", optionValue);
                return;
            }

            if (Options.Exists(x => x.Name == optionName))
            {
                BaseOption option = Options.First(x => x.Name == optionName);

                switch (option)
                {
                    case Option<bool> boolOption:
                        TryToChangeValueForBoolOption(optionName, optionValue, boolOption);
                        break;
                    case Option<int> intOption:
                        TryToChangeValueForIntOption(optionName, optionValue, intOption);
                        break;
                    default:
                        Logger.DoErrorLogKV("Trying to update option with not supported type!", "Name", optionName,
                            "Value", optionValue);
                        break;
                }                
            }
            else
            {
                Logger.DoWarningLogKV("Trying to update option which dont exists in Options list!", "Name", optionName,
                    "Value", optionValue);
            }
        }

        private void TryToChangeValueForIntOption(string optionName, string optionValue, Option<int> intOption)
        {
            int newValue = 0;

            if (int.TryParse(optionValue, out newValue))
                intOption.SetValue(newValue);
            else
                Logger.DoErrorLogKV("Cannot parse new value for option to int!",
                     "Name", optionName, "Value", optionValue);
        }

        private void TryToChangeValueForBoolOption(string optionName, string optionValue, Option<bool> boolOption)
        {
            bool newValue = false;

            if (bool.TryParse(optionValue, out newValue))
                boolOption.SetValue(newValue);
            else
                Logger.DoErrorLogKV("Cannot parse new value for option to bool!",
                     "Name", optionName, "Value", optionValue);
        }
    }
}
