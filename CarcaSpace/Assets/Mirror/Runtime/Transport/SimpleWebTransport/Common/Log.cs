using System;
using UnityEngine;
using Conditional = System.Diagnostics.ConditionalAttribute;

namespace Mirror.SimpleWeb
{
    public static class Log
    {
        // used for Conditional
        const string SIMPLEWEB_LOG_ENABLED = nameof(SIMPLEWEB_LOG_ENABLED);
        const string DEBUG = nameof(DEBUG);

        public enum Levels
        {
            none = 0,
            error = 1,
            warn = 2,
            info = 3,
            verbose = 4,
        }

<<<<<<< HEAD
        public static ILogger logger = Debug.unityLogger;
=======
>>>>>>> origin/alpha_merge
        public static Levels level = Levels.none;

        public static string BufferToString(byte[] buffer, int offset = 0, int? length = null)
        {
            return BitConverter.ToString(buffer, offset, length ?? buffer.Length);
        }

        [Conditional(SIMPLEWEB_LOG_ENABLED)]
        public static void DumpBuffer(string label, byte[] buffer, int offset, int length)
        {
            if (level < Levels.verbose)
                return;

<<<<<<< HEAD
            logger.Log(LogType.Log, $"VERBOSE: <color=cyan>{label}: {BufferToString(buffer, offset, length)}</color>");
=======
            Debug.Log($"VERBOSE: <color=blue>{label}: {BufferToString(buffer, offset, length)}</color>");
>>>>>>> origin/alpha_merge
        }

        [Conditional(SIMPLEWEB_LOG_ENABLED)]
        public static void DumpBuffer(string label, ArrayBuffer arrayBuffer)
        {
            if (level < Levels.verbose)
                return;

<<<<<<< HEAD
            logger.Log(LogType.Log, $"VERBOSE: <color=cyan>{label}: {BufferToString(arrayBuffer.array, 0, arrayBuffer.count)}</color>");
=======
            Debug.Log($"VERBOSE: <color=blue>{label}: {BufferToString(arrayBuffer.array, 0, arrayBuffer.count)}</color>");
>>>>>>> origin/alpha_merge
        }

        [Conditional(SIMPLEWEB_LOG_ENABLED)]
        public static void Verbose(string msg, bool showColor = true)
        {
            if (level < Levels.verbose)
                return;

            if (showColor)
<<<<<<< HEAD
                logger.Log(LogType.Log, $"VERBOSE: <color=cyan>{msg}</color>");
            else
                logger.Log(LogType.Log, $"VERBOSE: {msg}");
=======
                Debug.Log($"VERBOSE: <color=blue>{msg}</color>");
            else
                Debug.Log($"VERBOSE: {msg}");
>>>>>>> origin/alpha_merge
        }

        [Conditional(SIMPLEWEB_LOG_ENABLED)]
        public static void Info(string msg, bool showColor = true)
        {
            if (level < Levels.info)
                return;

            if (showColor)
<<<<<<< HEAD
                logger.Log(LogType.Log, $"INFO: <color=cyan>{msg}</color>");
            else
                logger.Log(LogType.Log, $"INFO: {msg}");
=======
                Debug.Log($"INFO: <color=blue>{msg}</color>");
            else
                Debug.Log($"INFO: {msg}");
>>>>>>> origin/alpha_merge
        }

        /// <summary>
        /// An expected Exception was caught, useful for debugging but not important
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="showColor"></param>
        [Conditional(SIMPLEWEB_LOG_ENABLED)]
        public static void InfoException(Exception e)
        {
            if (level < Levels.info)
                return;

<<<<<<< HEAD
            logger.Log(LogType.Log, $"INFO_EXCEPTION: <color=cyan>{e.GetType().Name}</color> Message: {e.Message}\n{e.StackTrace}\n\n");
=======
            Debug.Log($"INFO_EXCEPTION: <color=blue>{e.GetType().Name}</color> Message: {e.Message}");
>>>>>>> origin/alpha_merge
        }

        [Conditional(SIMPLEWEB_LOG_ENABLED), Conditional(DEBUG)]
        public static void Warn(string msg, bool showColor = true)
        {
            if (level < Levels.warn)
                return;

            if (showColor)
<<<<<<< HEAD
                logger.Log(LogType.Warning, $"WARN: <color=orange>{msg}</color>");
            else
                logger.Log(LogType.Warning, $"WARN: {msg}");
=======
                Debug.LogWarning($"WARN: <color=orange>{msg}</color>");
            else
                Debug.LogWarning($"WARN: {msg}");
>>>>>>> origin/alpha_merge
        }

        [Conditional(SIMPLEWEB_LOG_ENABLED), Conditional(DEBUG)]
        public static void Error(string msg, bool showColor = true)
        {
            if (level < Levels.error)
                return;

            if (showColor)
<<<<<<< HEAD
                logger.Log(LogType.Error, $"ERROR: <color=red>{msg}</color>");
            else
                logger.Log(LogType.Error, $"ERROR: {msg}");
=======
                Debug.LogError($"ERROR: <color=red>{msg}</color>");
            else
                Debug.LogError($"ERROR: {msg}");
>>>>>>> origin/alpha_merge
        }

        public static void Exception(Exception e)
        {
            // always log Exceptions
<<<<<<< HEAD
            logger.Log(LogType.Error, $"EXCEPTION: <color=red>{e.GetType().Name}</color> Message: {e.Message}\n{e.StackTrace}\n\n");
=======
            Debug.LogError($"EXCEPTION: <color=red>{e.GetType().Name}</color> Message: {e.Message}");
>>>>>>> origin/alpha_merge
        }
    }
}
