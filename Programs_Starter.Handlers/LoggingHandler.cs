using Programs_Starter.Handlers.Base;
using Programs_Starter.Models;
using Programs_Starter.Models.Base;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Programs_Starter.Handlers
{
    public class LoggingHandler : BaseHandler
    {
        private const string NAME = "LoggingHandler";

        private const string LOG_FILE_NAME = "log.txt";
        private const string LOG_FILE_PATH = "Log\\" + LOG_FILE_NAME;

        private string parentClassName;

        private string logFilePath;

        public List<BaseLog> Logs { get; private set; }

        public LoggingHandler(string _parentClassName) : base(NAME)
        {
            parentClassName = _parentClassName;
            Logs = new List<BaseLog>();

            ObtainLogFilePath();

            if (!LogFileExist())
                CreateEmptyLogFile();
        }

        /// <summary>
        /// Creates Warning Log
        /// </summary>
        /// <param name="logContent"></param>
        public void DoWarningLog(string logContent)
        {
            WarningLog log = new WarningLog(DateTime.Now, parentClassName, logContent);

            StoreLog(log);
        }

        /// <summary>
        /// Creates Warning Log with key values
        /// </summary>
        /// <param name="logContent"></param>
        /// <param name="values"></param>
        public void DoWarningLogKV(string logContent, params string[] values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(logContent + " ");

            for (int i = 0; i < values.Length; i++)
            {
                if (i.IsEven())
                    stringBuilder.Append(values[i]);
                else
                    stringBuilder.Append($"=<{values[i]}> ");
            }

            WarningLog log = new WarningLog(DateTime.Now, parentClassName, stringBuilder.ToString());

            StoreLog(log);
        }

        /// <summary>
        /// Creates Error Log
        /// </summary>
        /// <param name="logContent"></param>
        public void DoErrorLog(string logContent)
        {
            ErrorLog log = new ErrorLog(DateTime.Now, parentClassName, logContent);

            StoreLog(log);            
        }
        
        /// <summary>
        /// Creates Error Log with key values
        /// </summary>
        /// <param name="logContent"></param>
        /// <param name="values"></param>
        public void DoErrorLogKV(string logContent, params string[] values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(logContent + " ");

            for (int i = 0; i < values.Length; i++)
            {
                if (i.IsEven())
                    stringBuilder.Append(values[i]);
                else
                    stringBuilder.Append($"=<{values[i]}> ");
            }

            ErrorLog log = new ErrorLog(DateTime.Now, parentClassName, stringBuilder.ToString());

            StoreLog(log);
        }

        /// <summary>
        /// This method is storing the log
        /// </summary>
        /// <param name="log"></param>
        private void StoreLog(BaseLog log)
        {
            Logs.Add(log);

            try
            {
                File.AppendAllText(logFilePath, log.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in LoggingHandler.StoreLog: " + ex.Message);
            }
        }

        /// <summary>
        /// This method is obtaining LogFilePath for LoggingHandler - must be used before all other methods
        /// </summary>
        private void ObtainLogFilePath()
        {
            //if 'try catch' fails then logFilePath will be null
            logFilePath = null;

            try
            {
                logFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                logFilePath = Path.Combine(logFilePath, LOG_FILE_PATH);
                logFilePath = new Uri(logFilePath).LocalPath;  //this will cut 'file:///' at the beginning of path from .CodeBase method
            }
            catch (Exception ex)
            {
                throw new Exception("Error in LoggingHandler.ObtainLogFilePath: " + ex.Message);
            }
        }

        /// <summary>
        /// This method is checking log file exists at LogFilePath
        /// </summary>
        private bool LogFileExist()
        {
            if (!string.IsNullOrWhiteSpace(logFilePath) && File.Exists(logFilePath))
                return true;
            
            return false;
        }

        /// <summary>
        /// This method creates new empty log file at LogFilePath
        /// </summary>
        private void CreateEmptyLogFile()
        {
            if (string.IsNullOrWhiteSpace(logFilePath))
            {
                throw new Exception("Cannot create empty log file, because logFilePath is null, empty or whitespace: " + logFilePath);
            }

            if (!Directory.Exists(logFilePath) && !TryToCreateLogDirectory())
            {
                throw new Exception("Cannot create empty log file, because Log folder didn't exists");
            }

            try
            {
                File.Create(logFilePath).Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating new empty log file at logFilePath" + logFilePath +
                    " Error: " + ex.Message);
            }
        }

        /// <summary>
        /// This method tries to create Log directory
        /// </summary>
        /// <returns></returns>
        private bool TryToCreateLogDirectory()
        {
            try
            {
                Directory.CreateDirectory(logFilePath.Remove(logFilePath.Length - LOG_FILE_NAME.Length - 1)); //remove "\\log.txt" from logFilePath
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating Log directory for logFilePath: " + logFilePath +
                    " Error: " + ex.Message);
            }
        }
    }
}
