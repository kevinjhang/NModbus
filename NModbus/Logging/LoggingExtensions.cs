﻿using System;
using System.Linq;
using NModbus.Extensions;

namespace NModbus.Logging
{
    public static class LoggingExtensions
    {
        #region Standard level-based logging

        public static void Trace(this IModbusLogger logger, string message)
        {
            logger.Log(LoggingLevel.Trace, message);
        }

        public static void Debug(this IModbusLogger logger, string message)
        {
            logger.Log(LoggingLevel.Debug, message);
        }

        public static void Information(this IModbusLogger logger, string message)
        {
            logger.Log(LoggingLevel.Information, message);
        }

        public static void Warning(this IModbusLogger logger, string message)
        {
            logger.Log(LoggingLevel.Warning, message);
        }

        public static void Error(this IModbusLogger logger, string message)
        {
            logger.Log(LoggingLevel.Error, message);
        }

        public static void Critical(this IModbusLogger logger, string message)
        {
            logger.Log(LoggingLevel.Critical, message);
        }

        #endregion

        #region Func Logging
        public static void Log(this IModbusLogger logger, LoggingLevel level, Func<string> messageFactory)
        {
            if (logger.ShouldLog(level))
            {
                string message = messageFactory();

                logger.Log(level, message);
            }
        }

        public static void Trace(this IModbusLogger logger, Func<string> messageFactory)
        {
            logger.Log(LoggingLevel.Trace, messageFactory);
        }

        public static void Debug(this IModbusLogger logger, Func<string> messageFactory)
        {
            logger.Log(LoggingLevel.Debug, messageFactory);
        }

        public static void Information(this IModbusLogger logger, Func<string> messageFactory)
        {
            logger.Log(LoggingLevel.Information, messageFactory);
        }

        public static void Warning(this IModbusLogger logger, Func<string> messageFactory)
        {
            logger.Log(LoggingLevel.Warning, messageFactory);
        }

        public static void Error(this IModbusLogger logger, Func<string> messageFactory)
        {
            logger.Log(LoggingLevel.Error, messageFactory);
        }

        public static void Critical(this IModbusLogger logger, Func<string> messageFactory)
        {
            logger.Log(LoggingLevel.Critical, messageFactory);
        }

        #endregion

        #region Frame logging

        private static void LogFrame(this IModbusLogger logger, string validPrefix, string invalidPrefix, string mode, byte[] frame, string endPoint = null)
        {
            if (logger.ShouldLog(LoggingLevel.Frame))
            {
                string prefix = validPrefix;

                if (mode == "RTU")
                {
                    prefix = frame.DoesCrcMatch() ? validPrefix : invalidPrefix;
                }

                string message = message = $"{prefix},{mode},{string.Join(" ", frame.Select(b => b.ToString("X2")))}";

                if (endPoint != null)
                {
                    message = $"{endPoint}," + message;
                }

                logger.Log(LoggingLevel.Frame, message);
            }
        }

        internal static void LogFrameTx(this IModbusLogger logger, string mode, byte[] frame, string endPoint = null)
        {
            logger.LogFrame("TX", "tx", mode, frame, endPoint);
        }

        internal static void LogFrameRx(this IModbusLogger logger, string mode, byte[] frame, string endPoint = null)
        {
            logger.LogFrame("RX", "rx", mode, frame, endPoint);
        }

        internal static void LogFrameIgnoreRx(this IModbusLogger logger, string mode, byte[] frame)
        {
            logger.LogFrame("IR", "ir", mode, frame);
        }
        #endregion  
    }
}