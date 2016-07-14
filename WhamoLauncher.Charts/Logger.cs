using System;
using System.IO;
using System.Reflection;

namespace WhamoLauncher.Charts
{
    internal interface ILogger
    {
        void Log(Exception exception);
        void SetLoggingPath(string path);
        string LoggingPath { get; }
    }

    internal static class Logger
    {
        private static readonly Lazy<ILogger> logger;
        public static ILogger Default => logger.Value;

        static Logger()
        {
            logger = new Lazy<ILogger>(() => new _Logger(), true);
        }

        private class _Logger : ILogger, IDisposable
        {
            private bool disposed;
            private StreamWriter writer;
            private string logPath;

            public _Logger()
            {
                logPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Strings.LogFileName);
            }

            public string LoggingPath => logPath;

            public void SetLoggingPath(string path)
            {
                if (path == null)
                {
                    throw new ArgumentNullException(nameof(path));
                }

                logPath = path;
            }

            public void Log(Exception exc)
            {
                if (exc == null)
                {
                    throw new ArgumentNullException(nameof(exc));
                }

                if (disposed)
                {
                    throw new ObjectDisposedException(nameof(writer));
                }

                try
                {
                    if (writer == null)
                    {
                        writer = new StreamWriter(logPath, true);
                    }

                    int indentationLevel = 0;
                    var e = exc;

                    do
                    {
                        var log = getExceptionLog(e, indentationLevel);
                        writer.WriteLine(log);
                        writer.Flush();
                        e = e.InnerException;
                        indentationLevel++;
                    } while (e != null);
                    
                }
                catch
                {
                    //fail silently;
                    return;
                }
            }

            private static string getExceptionLog(Exception exc, int indentationLevel)
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName();
                var indent = string.Empty;

                for (var i = 0; i < indentationLevel; i++)
                {
                    indent = string.Concat(indent, '\t');
                }

                return $"{indent}{DateTime.Now}: {assemblyName.Name}-{assemblyName.Version}{Environment.NewLine}" +
                       $"{indent}- Type: {exc.GetType().Name}{Environment.NewLine}" +
                       $"{indent}- Message: {(exc.Message != null ? exc.Message : "None.")}{Environment.NewLine}" +
                       $"{indent}- StackTrace: {(exc.StackTrace != null ? exc.StackTrace.Trim() : "Not available.")}{Environment.NewLine}" +
                       $"{indent}- Inner exception: {(exc.InnerException != null ? string.Empty : "None.")}";
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool isSafe)
            {
                if (disposed)
                {
                    disposed = true;
                }

                if (isSafe)
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }

                disposed = true;
            }

            ~_Logger()
            {
                Dispose(false);
            }
        }
    }
}
