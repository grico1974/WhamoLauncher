using System;
using System.Diagnostics;
using System.IO;
using WhamoLauncher.Resources;

namespace WhamoLauncher
{
    internal static class Launcher
    {
        public static void Launch(string whamoInputFilePath)
        {
            if (whamoInputFilePath == null)
            {
                throw new ArgumentNullException("inputFilePath");
            }

            if (!File.Exists(whamoInputFilePath))
            {
                throw new ArgumentException(string.Format(Strings.InputFileNotFoundError, whamoInputFilePath), "inputFilePath");
            }

            var canDeleteSafeCopies = false;
            var localWhamoExecutableCopied = false;
            var filesInfo = new WhamoFilesInformation(whamoInputFilePath);

            try
            {
                localWhamoExecutableCopied = copyLocalWhamoExecutable(filesInfo);
                backupAllFiles(filesInfo);
                executeWhamo(filesInfo);
                canDeleteSafeCopies = true;
                processWhamoOutFile(filesInfo);
            }
            catch
            {
                rollBackAllFileChanges(filesInfo);
                canDeleteSafeCopies = true;
                throw;
            }
            finally
            {
                if (canDeleteSafeCopies)
                {
                    deleteAllBackupFiles(filesInfo);
                }

                if (localWhamoExecutableCopied)
                {
                    deleteLocalWhamoExecutable(filesInfo);
                }
            }
        }

        private static void backupAllFiles(WhamoFilesInformation filesInfo)
        {
            if (File.Exists(filesInfo.OutFilePath))
            {
                File.Copy(filesInfo.OutFilePath, filesInfo.SafeCopyOutFilePath, true);
                File.Delete(filesInfo.OutFilePath);
            }

            if (File.Exists(filesInfo.TabFilePath))
            {
                File.Copy(filesInfo.TabFilePath, filesInfo.SafeCopyTabFilePath, true);
                File.Delete(filesInfo.TabFilePath);
            }

            if (File.Exists(filesInfo.PltFilePath))
            {
                File.Copy(filesInfo.PltFilePath, filesInfo.SafeCopyPltFilePath, true);
                File.Delete(filesInfo.PltFilePath);
            }

            if (File.Exists(filesInfo.ProcessedOutFilePath))
            {
                File.Copy(filesInfo.ProcessedOutFilePath, filesInfo.SafeProcessedOutFilePath, true);
                File.Delete(filesInfo.ProcessedOutFilePath);
            }
        }

        private static bool copyLocalWhamoExecutable(WhamoFilesInformation filesInfo)
        {
            if (!File.Exists(filesInfo.LocalWhamoPath))
            {
                ThirdPartyResourcesProvider.CopyWhamoExecutableTo(filesInfo.LocalWhamoPath);
                return true;
            }

            return false;
        }

        private static void executeWhamo(WhamoFilesInformation filesInfo)
        {
            var startInfo = new ProcessStartInfo(filesInfo.LocalWhamoPath);
            startInfo.WorkingDirectory = Path.GetDirectoryName(filesInfo.WhamoInputFilePath);
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            var whamo = Process.Start(startInfo);
            whamo.StandardInput.WriteLine(Path.GetFileNameWithoutExtension(filesInfo.WhamoInputFilePath));
            whamo.StandardInput.WriteLine(); //Confirm .out file
            whamo.StandardInput.WriteLine(); //Confirm .plt file
            whamo.StandardInput.WriteLine(); //Confirm .tab file
            whamo.CloseMainWindow();
            whamo.WaitForExit();
        }

        private static void rollBackAllFileChanges(WhamoFilesInformation filesInfo)
        {
            if (File.Exists(filesInfo.SafeCopyPltFilePath))
            {
                File.Copy(filesInfo.SafeCopyPltFilePath, filesInfo.PltFilePath, true);
            }

            if (File.Exists(filesInfo.SafeCopyTabFilePath))
            {
                File.Copy(filesInfo.SafeCopyTabFilePath, filesInfo.TabFilePath, true);
            }

            if (File.Exists(filesInfo.SafeCopyOutFilePath))
            {
                File.Copy(filesInfo.SafeCopyOutFilePath, filesInfo.OutFilePath, true);
            }

            if (File.Exists(filesInfo.SafeProcessedOutFilePath))
            {
                File.Copy(filesInfo.SafeProcessedOutFilePath, filesInfo.ProcessedOutFilePath, true);
            }
        }

        private static void deleteAllBackupFiles(WhamoFilesInformation filesInfo)
        {
            if (File.Exists(filesInfo.SafeCopyOutFilePath))
            {
                File.Delete(filesInfo.SafeCopyOutFilePath);
            }

            if (File.Exists(filesInfo.SafeCopyTabFilePath))
            {
                File.Delete(filesInfo.SafeCopyTabFilePath);
            }

            if (File.Exists(filesInfo.SafeCopyPltFilePath))
            {
                File.Delete(filesInfo.SafeCopyPltFilePath);
            }

            if (File.Exists(filesInfo.SafeCopyOutFilePath))
            {
                File.Delete(filesInfo.SafeCopyOutFilePath);
            }

            if (File.Exists(filesInfo.SafeProcessedOutFilePath))
            {
                File.Delete(filesInfo.SafeProcessedOutFilePath);
            }
        }

        private static void deleteLocalWhamoExecutable(WhamoFilesInformation filesInfo)
        {
            if (File.Exists(filesInfo.LocalWhamoPath))
            {
                File.Delete(filesInfo.LocalWhamoPath);
            }
        }

        private static void processWhamoOutFile(WhamoFilesInformation filesInfo)
        {
            using (var reader = new OutFileReader(filesInfo.OutFilePath))
            {
                using (var writer = new ProcessedDataFileWriter(filesInfo.ProcessedOutFilePath))
                {
                    writer.Write(reader.ProcessOutputData());
                }
            }
        }

        private class WhamoFilesInformation
        {
            public WhamoFilesInformation(string whamoInputFile)
            {
                WhamoInputFilePath = whamoInputFile;
                LocalWhamoPath = Path.Combine(Path.GetDirectoryName(whamoInputFile), Strings.WhamoExecutableFileName);
                OutFilePath = Path.ChangeExtension(whamoInputFile, Strings.WhamoOutFileExtension);
                PltFilePath = Path.ChangeExtension(whamoInputFile, Strings.WhamoPlotFileExtension);
                TabFilePath = Path.ChangeExtension(whamoInputFile, Strings.WhamoTabFileExtension);
                ProcessedOutFilePath = Path.ChangeExtension(whamoInputFile, Strings.CsvFileExtension);
                SafeCopyOutFilePath = Path.ChangeExtension(OutFilePath, Strings.WhamoOutSafeCopyFileExtension);
                SafeCopyPltFilePath = Path.ChangeExtension(OutFilePath, Strings.WhamoPlotSafeCopyFileExtension);
                SafeCopyTabFilePath = Path.ChangeExtension(OutFilePath, Strings.WhamoTabSafeCopyFileExtension);
                SafeProcessedOutFilePath = Path.ChangeExtension(ProcessedOutFilePath, Strings.CsvSafeCopyFileExtension);
            }

            public readonly string LocalWhamoPath;
            public readonly string OutFilePath;
            public readonly string PltFilePath;
            public readonly string ProcessedOutFilePath;
            public readonly string SafeCopyOutFilePath;
            public readonly string SafeCopyPltFilePath;
            public readonly string SafeCopyTabFilePath;
            public readonly string SafeProcessedOutFilePath;
            //public readonly string WhamoExecutableRepositoryPath;
            public readonly string TabFilePath;
            public readonly string WhamoInputFilePath;
        }
    }
}
