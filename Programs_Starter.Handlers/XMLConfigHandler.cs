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
                XMLPath = Path.Combine(XMLPath, "Data\\configuration.xml");
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

                XmlNode mainNode = doc.CreateElement("Programs_Starter");
                doc.AppendChild(mainNode);

                XmlNode settingsNode = doc.CreateElement("ProgramSettings");
                mainNode.AppendChild(settingsNode);

                XmlNode programsNode = doc.CreateElement("ProgramsToStart");
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
                Directory.CreateDirectory(XMLPath.Remove(XMLPath.Length - 18)); //remove "\\configuration.xml" from XMLPath
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

                    XmlNodeList programsToStartNodes = doc.DocumentElement.SelectNodes("/Programs_Starter/ProgramsToStart/Program");

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
    }
}
