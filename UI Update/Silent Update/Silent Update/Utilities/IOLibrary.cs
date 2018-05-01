using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Silent_Update.Utilities
{
    public class IOLibrary
    {
        // <summary>
        /// This is to calculate the memory space
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
           out long lpFreeBytesAvailable,
           out long lpTotalNumberOfBytes,
           out long lpTotalNumberOfFreeBytes);

        long FreeBytesAvailable;
        long TotalNumberOfBytes;
        long TotalNumberOfFreeBytes;

        #region Calculate Memory Space
        /// <summary>
        /// Check if the back up folder have enough space to back up all the file for updating to run
        /// </summary>
        /// <param name="UpdatePath"></param>
        /// <param name="BkPath"></param>
        /// <returns></returns>
        public bool CheckForSufficientMemory(string UpdatePath, string BkPath)
        {
            bool bRetValue = true;
            long DirectorySize = GetDirectorySize(UpdatePath);
            if (DirectorySize < 0)
            {
                return false;
            }
            long SpaceAvailable = CalculateTheMemoryAvailable(BkPath);
            if (DirectorySize >= SpaceAvailable)
            {
                bRetValue = false;
            }
            else
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        /// <summary>
        /// Calculate the availble memory
        /// </summary>
        /// <param name="FolderPath"></param>
        /// <returns></returns>
        private long CalculateTheMemoryAvailable(string FolderPath)
        {
            bool success = GetDiskFreeSpaceEx(FolderPath,
                                  out FreeBytesAvailable,
                                  out TotalNumberOfBytes,
                                  out TotalNumberOfFreeBytes);
            if (!success)
            {
                // Not calculate
            }
            return FreeBytesAvailable;
        }

        /// <summary>
        /// Get the directory size
        /// </summary>
        /// <param name="parentDirectory"></param>
        /// <returns></returns>
        private long GetDirectorySize(string parentDirectory)
        {
            try
            {
                return new System.IO.DirectoryInfo(parentDirectory).GetFiles("*.*", System.IO.SearchOption.AllDirectories).Sum(file => file.Length);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Return true when the time exceeding the limit time set in Environment for updating/roll forwarding
        /// </summary>
        /// <param name="Hours"></param>
        /// <param name="StartTime"></param>
        /// <returns></returns>
        public bool DoesExceedAllowingTime(int Hours, DateTime StartTime)
        {
            if (Hours < 1)
            {
                Hours = 8;
            }
            bool isSuccess = false;
            DateTime EndTime = DateTime.Now;
            TimeSpan ts = EndTime.Subtract(StartTime);
            int HourSpend = (ts.Days * 24) + ts.Hours;
            if (HourSpend < Hours)
            {
                isSuccess = false;
            }
            else if (ts.Minutes == 0)
            {
                isSuccess = false;
            }
            else
            {
                isSuccess = true;
            }
            return isSuccess;
        }
        #endregion

        #region File/Folder utilities
        /// <summary>
        /// Copy the folder from source to destination
        /// </summary>
        /// <param name="Source">C:\Caseware\TEST</param>
        /// <param name="Destination">C:\ABC, the result would be C:\ABC\TEST</param>
        /// <returns></returns>
        public bool CopyFolder(string Source, string Destination)
        {
            bool isSuccess = false;
            string NewFolderPath;
            try
            {
                if (Directory.Exists(Source))
                {
                    DirectoryInfo SourceFolder = new DirectoryInfo(Source);
                    NewFolderPath = Path.Combine(Destination, SourceFolder.Name);
                    if (Directory.Exists(NewFolderPath))
                    {
                        // Deleting the current folder if it exists
                        try
                        {
                            Directory.Delete(NewFolderPath, true);
                        }
                        catch { }
                    }
                    isSuccess = CopyFolderToLocation(Source, NewFolderPath);
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch { isSuccess = false; }
            return isSuccess;
        }

        /// <summary>
        /// Copy all files and sub-folders from source to destination
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Destination"></param>
        /// <returns></returns>
        private bool CopyFolderToLocation(string Source, string Destination)
        {
            if (!Directory.Exists(Destination))
            {
                Directory.CreateDirectory(Destination);
            }
            foreach (string files in Directory.GetFiles(Source))
            {
                FileInfo fileInfo = new FileInfo(files);
                fileInfo.CopyTo(string.Format(@"{0}\{1}", Destination, fileInfo.Name), true);
            }
            foreach (string dirs in Directory.GetDirectories(Source))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirs);
                if (CopyFolderToLocation(dirs, Path.Combine(Destination, dirInfo.Name)) == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get the parent folder of the current file
        /// </summary>
        /// <param name="targetFilePath"></param>
        /// <returns></returns>
        public string GetParentFolderFromFilePath(string targetFilePath)
        {
            string FileName = Path.GetFileName(targetFilePath);
            string ClientFolderPath = Path.GetFullPath(targetFilePath).Replace(FileName, "");
            return ClientFolderPath;
        }

        /// <summary>
        /// Get the second level of the folder from the file
        /// </summary>
        /// <param name="oldPath"></param>
        /// <returns></returns>
        public string GetGrandparentFolder(string oldPath)
        {
            string FolderPath = "";
            int LastIndex = oldPath.LastIndexOf('\\');
            string ParentPath = oldPath.Substring(0, LastIndex);
            LastIndex = ParentPath.LastIndexOf('\\');
            FolderPath = ParentPath.Substring(0, LastIndex);
            return FolderPath;
        }

        /// <summary>
        /// Copy the file to the destination Folder Location
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="Destination">Folder Location</param>
        public bool CopyFile(string FilePath, string Destination)
        {
            bool isSuccess = false;
            if (File.Exists(FilePath))
            {
                string FileName = Path.GetFileName(FilePath);
                string NewFilePath = Path.Combine(Destination, FileName);
                File.Copy(FilePath, NewFilePath, true);
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        #endregion

        #region Set Environment for wizard run update
        /// <summary>
        /// Set Window environment flag for silent update wizard
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool SetEnvironment(string Value)
        {
            bool isSuccess = false;
            try
            {
                string EnviromentValue = System.Environment.GetEnvironmentVariable("CWISILENTPATCH", EnvironmentVariableTarget.User);
                if (EnviromentValue == null)
                {
                    System.Environment.SetEnvironmentVariable("CWISILENTPATCH", Value, EnvironmentVariableTarget.User);
                }
                else
                {
                    System.Environment.SetEnvironmentVariable("CWISILENTPATCH", Value, EnvironmentVariableTarget.User);
                }
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        #endregion

        #region Open File By Default program
        public void OpenFile(string filePath)
        {
            string command;
            if (filePath != null && filePath != "")
            {
                if (System.IO.File.Exists(filePath) == false && filePath.IndexOf(".ac_") > 0)
                {
                    filePath = filePath.Replace(".ac_", ".ac");
                }
                command = "cw:" + filePath + ":";
                System.Diagnostics.Process.Start(command);
            }
        }
        #endregion
    }
}
