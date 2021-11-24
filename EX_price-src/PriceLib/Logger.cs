using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PriceLib
{
    public class Logger
    {
        private static readonly ILog logger = InitLogger();

        private static ILog InitLogger()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static void Info(string msg)
        {
            logger.Info(msg);
        }

        public static void Info(string formatString, params object[] parameters)
        {
            logger.InfoFormat(formatString, parameters);
        }

        public static void Warn(string msg)
        {
            logger.Warn(msg);
        }

        public static void Warn(string formatString, params object[] parameters)
        {
            logger.WarnFormat(formatString, parameters);
        }

        public static void Error(string msg)
        {
            logger.Error(msg);
        }

        public static void Error(string formatString, params object[] parameters)
        {
            logger.ErrorFormat(formatString, parameters);
        }

        public static void Debug(string msg)
        {
            logger.Debug(msg);
        }

        public static void Debug(string formatString, params object[] parameters)
        {
            logger.DebugFormat(formatString, parameters);
        }

    }
}
