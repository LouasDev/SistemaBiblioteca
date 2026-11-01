using System;
using System.IO;
using System.Threading;

namespace SUB.Tests.Infrastructure
{
    internal sealed class TestTempWorkspace : IDisposable
    {
        public string Root { get; }

        public TestTempWorkspace()
        {
            Root = Path.Combine(Path.GetTempPath(), "SUB.Tests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(Root);
        }

        public string CreateFolder(string name)
        {
            string path = Path.Combine(Root, name);
            Directory.CreateDirectory(path);
            return path;
        }

        public string CreateFile(string name, string content)
        {
            string path = Path.Combine(Root, name);
            File.WriteAllText(path, content);
            return path;
        }

        public void Dispose()
        {
            TryDeleteDirectory(Root);
        }

        internal static void TryDeleteDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                return;

            for (int tentativa = 0; tentativa < 8; tentativa++)
            {
                try
                {
                    Directory.Delete(path, true);
                    return;
                }
                catch (IOException)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Thread.Sleep(100);
                }
                catch (UnauthorizedAccessException)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Thread.Sleep(100);
                }
            }
        }
    }
}