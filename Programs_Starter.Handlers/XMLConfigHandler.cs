using Programs_Starter.Handlers.Base;
using Programs_Starter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Programs_Starter.Handlers
{
    public class XMLConfigHandler : BaseLoggingHandler
    {
        private const string NAME = "XMLConfigHandler";
                
        private const string CONFIG_FILE_NAME = "configuration.xml";
        private const string CONFIG_FILE_PATH = "Data\\" + CONFIG_FILE_NAME;

        private const string XML_MAIN_NODE_NAME = "Programs_Starter";

        private const string XML_SETTINGS_NODE_NAME = "ProgramSettings";
        private const string XML_PROGRAMS_NODE_NAME = "ProgramsToStart";

        private const string PROGRAMS_TO_START_XML_PATH = "/" + XML_MAIN_NODE_NAME + "/" + XML_PROGRAMS_NODE_NAME + "/Program";
        private const string OPTIONS_XML_PATH = "/" + XML_MAIN_NODE_NAME + "/" + XML_SETTINGS_NODE_NAME + "/Setting";

        public string XMLPath { get; private set; }

        public delegate void NoProgramsToStartFoundDelegate();
        public NoProgramsToStartFoundDelegate NoProgramsToStartFound;

        public XMLConfigHandler() : base(NAME)
        {
            ObtainXMLPath();

            if (!ConfigFileExist())
                CreateEmptyConfigFile();                
        }

        /// <summary>
        /// This method is obtaining XMLPath for XMLHandler - must be used before all other methods
        /// </summary>
        private void ObtainXMLPath()
        {
            //if 'try catch' fails then XMLPath will be null
            XMLPath = null;

            try
            {
                XMLPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                XMLPath = Path.Combine(XMLPath, CONFIG_FILE_PATH);
                XMLPath = new Uri(XMLPath).LocalPath;  //this will cut 'file:///' at the beginning of path from .CodeBase method
            }
            catch (Exception ex)
            {
                Logger.DoErrorLog("Unexpected exception with XMLPath (XMLPath set to null): " + ex.Message);
            }
        }

        /// <summary>
        /// This method is checking config file exists at XMLPath
        /// </summary>
        private bool ConfigFileExist()
        {
            if (!string.IsNullOrWhiteSpace(XMLPath) && File.Exists(XMLPath))
                return true;

            Logger.DoWarningLog("Config file not exists at path: " + XMLPath);
            return false;
        }

        /// <summary>
        /// This method creates new empty config file at XMLPath
        /// </summary>
        private void CreateEmptyConfigFile()
        {
            if (string.IsNullOrWhiteSpace(XMLPath))
            {
                Logger.DoErrorLogKV("Cannot create empty config file, because XMLPath is null, empty or whitespace", "XMLPath", XMLPath);
                return;
            }

            if (!Directory.Exists(XMLPath) && !TryToCreateDataDirectory())
            {
                Logger.DoErrorLog("Cannot create empty config file, because Data folder didn't exists");
                return;
            }

            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                XmlNode mainNode = doc.CreateElement(XML_MAIN_NODE_NAME);
                doc.AppendChild(mainNode);

                XmlNode settingsNode = doc.CreateElement(XML_SETTINGS_NODE_NAME);
                mainNode.AppendChild(settingsNode);

                XmlNode programsNode = doc.CreateElement(XML_PROGRAMS_NODE_NAME);
                mainNode.AppendChild(programsNode);

                doc.Save(XMLPath);
            }
            catch (Exception ex)
            {
                Logger.DoErrorLogKV("Error while creating new empty config file: ", "XMLPath", XMLPath,
                    "Error", ex.Message);
            }    
        }

        /// <summary>
        /// This method tries to create Data directory
        /// </summary>
        /// <returns></returns>
        private bool TryToCreateDataDirectory()
        {
            try
            {
                Directory.CreateDirectory(XMLPath.Remove(XMLPath.Length - CONFIG_FILE_NAME.Length - 1)); //remove "\\configuration.xml" from XMLPath
                return true;
            }
            catch (Exception ex)
            {
                Logger.DoErrorLogKV("Error while creating Data directory for XMLPath: ", "XMLPath", XMLPath,
                    "Error", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// This method is reading all programs from xml file and returns them as Dictionary
        /// </summary>
        /// <returns>Dictionary with programs to start, empty if no programs found</returns>
        public Dictionary<int, ProgramToStart> ReadProgramsToStartFromConfig()
        {
            Dictionary<int, ProgramToStart> programsDict = new Dictionary<int, ProgramToStart>();
            XmlDocument doc = new XmlDocument();
            int temp = 0;

            if (File.Exists(XMLPath))
            {
                try
                {
                    doc.Load(XMLPath);

                    XmlNodeList programsToStartNodes = doc.DocumentElement.SelectNodes(PROGRAMS_TO_START_XML_PATH);

                    if (programsToStartNodes != null && programsToStartNodes.Count > 0)
                    {
                        foreach (XmlNode programNode in programsToStartNodes)
                        {
                            ProgramToStart _program = new ProgramToStart(programNode.Attributes["name"].Value, programNode.Attributes["path"].Value);
                            if (int.TryParse(programNode.Attributes["order"].Value, out temp))
                                programsDict.Add(temp, _program);
                            else
                                Logger.DoErrorLogKV("Starting Order could not be parsed into int: ", "Program", _program.Name);
                        }
                    }
                    else
                    {
                        Logger.DoWarningLog("No programs to start found in xml file");
                        NoProgramsToStartFound?.Invoke();
                    }                    
                }
                catch (Exception ex)
                {
                    Logger.DoErrorLogKV("Error while reading ProgramsToStart from xml: ", "XMLPath", XMLPath,
                        "Error", ex.Message);
                }
                
            }
            else
            {
                Logger.DoErrorLog("Error while reading ProgramsToStart from xml - XML file not found at path: " + XMLPath);
            }

            return programsDict;
        }

        /// <summary>
        /// This method is saving ProgramsToStart into the XMLFile
        /// </summary>
        /// <param name="newList">Dict of ProgramToStart which you want to save into xml</param>
        public void SaveProgramsToStartDict(Dictionary<int, ProgramToStart> newDictionary)
        {
            XmlDocument doc = new XmlDocument();
            
            if (XMLPath == null)
            {
                Logger.DoErrorLog("Error in SaveProgramsToStartDict: XMLPath is null");
            }

            if (File.Exists(XMLPath))
            {
                try
                {
                    doc.Load(XMLPath);

                    XmlNodeList programsToStartNodes = doc.DocumentElement.SelectNodes(PROGRAMS_TO_START_XML_PATH);

                    //clear whole list
                    for (int i = programsToStartNodes.Count - 1; i >= 0; i--)
                    {
                        programsToStartNodes[i].ParentNode.RemoveChild(programsToStartNodes[i]);
                    }

                    //create a new one
                    foreach (var item in newDictionary)
                    {
                        XmlElement childElement = doc.CreateElement("Program");
                        childElement.SetAttribute("order", item.Key.ToString());
                        childElement.SetAttribute("name", item.Value.Name);
                        childElement.SetAttribute("path", item.Value.Path);
                        XmlNode parentNode = doc.DocumentElement.SelectSingleNode("/" + XML_MAIN_NODE_NAME + "/" + XML_PROGRAMS_NODE_NAME);
                        parentNode.InsertAfter(childElement, parentNode.LastChild);
                    }

                    //save the file
                    doc.Save(XMLPath);
                }
                catch (Exception ex)
                {
                    Logger.DoErrorLogKV("Error in SaveProgramsToStartDict: ", "XMLPath", XMLPath,
                        "Error", ex.Message);
                }
            }
            else
            {
                Logger.DoErrorLog("Error in SaveProgramsToStartDict XML file not found at path: " + XMLPath);
            }
        }

        /// <summary>
        /// This method is reading all options from xml file and returns them as Dictionary
        /// </summary>
        /// <returns>Dictionary with options and their values as KeyValuePair string/string, empty if no options found</returns>
        public Dictionary<string, string> ReadOptionsFromConfig()
        {
            Dictionary<string, string> optionsDict = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();

            if (File.Exists(XMLPath))
            {
                try
                {
                    doc.Load(XMLPath);

                    XmlNodeList optionsNodes = doc.DocumentElement.SelectNodes(OPTIONS_XML_PATH);

                    if (optionsNodes != null && optionsNodes.Count > 0)
                    {
                        foreach (XmlNode optionNode in optionsNodes)
                        {
                            optionsDict.Add(optionNode.Attributes["name"].Value, optionNode.Attributes["value"].Value);
                        }
                    }
                    else
                    {
                        Logger.DoWarningLog("No options found in xml file, default values will be used");
                    }
                }
                catch (Exception ex)
                {
                    Logger.DoErrorLogKV("Error while reading options from xml, default values will be used: ", "XMLPath", XMLPath,
                        "Error", ex.Message);
                }
            }
            else
            {
                Logger.DoErrorLog("Error while reading Options from xml - XML file not found at path: " + XMLPath);
            }

            return optionsDict;
        }
    }
}
