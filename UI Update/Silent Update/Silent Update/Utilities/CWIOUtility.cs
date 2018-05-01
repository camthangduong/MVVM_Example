using CaseViewGateway;
using CaseWare;
using Silent_Update.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace Silent_Update.Utilities
{
    public class CWIOUtility
    {
        #region Property
        public CWApplication CWApp { get; }
        public CvGtwyApp GWApp { get; }
        public string CWAppPath { get { return GWApp.ProgramPath; } }
        public string CWBKPath
        {
            get { return CWApp.Options.DefaultPath[CaseWare.CWPathType.ptBackup]; }
        }
        public short MajorVersion { get { return GWApp.MajorVersion; } }
        public short MinorVersion { get { return GWApp.MinorVersion; } }
        public short BuildVersion { get { return GWApp.BuildVersion; } }
        #endregion

        public CWIOUtility()
        {
            CWApp = new CWApplication();
            GWApp = new CvGtwyApp();
        }

        #region Template Method
        /// <summary>
        /// Install the template file
        /// </summary>
        /// <param name="cwpFile"></param>
        public void Install(string cwpFile)
        {
            //Create a new process info structure.
            Process pInfo = new Process();
            pInfo.StartInfo.FileName = Path.Combine(CWAppPath, "cwpackager64.exe");
            pInfo.StartInfo.Arguments = "-u \"" + cwpFile + "\"";
            //Start the process.
            pInfo.Start();
            //Wait for the window to finish loading.
            pInfo.WaitForInputIdle();
            //Wait for the process to end.
            pInfo.WaitForExit();
        }

        /// <summary>
        /// Uninstall the template
        /// </summary>
        /// <param name="acFilePath"></param>
        public void Uninstall(string acFilePath)
        {

        }

        /// <summary>
        /// Get the collection of installed master template on the machine
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<TemplateInfo> GetInstalledMaster()
        {
            ObservableCollection<TemplateInfo> installedMasterList = new ObservableCollection<TemplateInfo>();
            CWTemplateInfos templateList = CWApp.TemplateList;
            int numberInstalledTemplate = templateList.Count;
            int index = 0;
            if (numberInstalledTemplate > 0)
            {
                foreach (CWTemplateInfo template in templateList)
                {
                    string templateVersion = "", templateName = template.Name, templateMinVersion = "", templateDisplayName = "";
                    // DBF File
                    string dbfFile = Path.Combine(template.FilePath + "CV.dbf");
                    // At Build value in CV DB - For audit
                    CWCaseViewData atBuildEntry = CWApp.SystemCaseViewDataSet(dbfFile, "", "", "WPGTEMPDIR_AT_BUILD");
                    if (atBuildEntry != null)
                    {
                        templateName = System.Convert.ToString(atBuildEntry.DataGroupFormId["", "", "WPGTEMPDIR_AT_BUILD"]);
                        templateVersion = System.Convert.ToString(atBuildEntry.DataGroupFormId["WPG_TEM_DATA", templateName, "WPGVER"]);
                        templateMinVersion = System.Convert.ToString(atBuildEntry.DataGroupFormId["WPG_TEM_DATA", templateName, "WPGVERMINWZ"]);
                        templateDisplayName = templateName;
                        if (templateVersion == null || templateVersion == "" || templateName == null || templateName == "")
                        {
                            // Could be FIN
                            atBuildEntry = null;
                            atBuildEntry = CWApp.SystemCaseViewDataSet(dbfFile, "", "", "CQCOUNTRY");
                            string sCountry = System.Convert.ToString(atBuildEntry.DataGroupFormId["", "", "CQCOUNTRY"]);
                            string sCode = "";
                            if (sCountry != null && sCountry != "")
                            {
                                if (sCountry == "1")
                                {
                                    sCode = "0100000000";
                                }
                                else if (sCountry == "2")
                                {
                                    sCode = "0000000000";
                                }
                                else if (sCountry == "3")
                                {
                                    sCode = "9900020000";
                                }
                                if (sCode != "")
                                {
                                    templateVersion = System.Convert.ToString(atBuildEntry.DataGroupFormId["TEMPLATEINFO", sCode, "VERSION"]);
                                    templateName = System.Convert.ToString(atBuildEntry.DataGroupFormId["TEMPLATEINFO", sCode, "DISPLAYNAME"]);
                                    templateMinVersion = System.Convert.ToString(atBuildEntry.DataGroupFormId["TEMPLATEINFO", sCode, "VERSIONMIN"]);
                                }
                            }
                        }
                        else
                        {
                            if (templateName.Contains("REVIEW") == true)
                            {
                                if (System.Convert.ToString(atBuildEntry.DataGroupFormId["WPG_TEM_DATA", templateName, "WPGVER"]) != null && System.Convert.ToString(atBuildEntry.DataGroupFormId["WPG_TEM_DATA", templateName, "HASCOMPILATION"]) == "1")
                                {
                                    templateDisplayName = System.Convert.ToString(atBuildEntry.DataGroupFormId["WPG_TEM_DATA", templateName, "CVDISPNAME"]);
                                }
                            }
                        }
                    }
                    else { continue; }
                    if (templateName != null && templateVersion != null && templateName != "" && templateVersion != "" && templateName.Contains("PCUTIL") == false)
                    {
                        templateMinVersion = templateMinVersion ?? "";
                        installedMasterList.Add(new TemplateInfo(index, true, templateName, templateVersion, templateMinVersion));
                        index++;
                    }
                }
            }
            return installedMasterList;
        }



        /// <summary>
        /// Unzip the CWP file
        /// </summary>
        /// <param name="cwpFile"></param>
        /// <param name="targetFolder"></param>
        public void Unzip(string cwpFile, string targetFolder)
        {
            GWApp.UnzipFiles(cwpFile, targetFolder);
            DirectoryInfo TempDir = new DirectoryInfo(targetFolder);
            /* Unzip in After folder */
            foreach (DirectoryInfo subDir in TempDir.GetDirectories())
            {
                if (subDir != null)
                {
                    foreach (FileInfo file in subDir.GetFiles())
                    {
                        if (file.Extension.ToUpper() == ".CWP")
                        {
                            string sPath = file.FullName;
                            int positionLast = sPath.LastIndexOf('.');
                            sPath = sPath.Substring(0, positionLast);
                            if (!Directory.Exists(sPath))
                            {
                                Directory.CreateDirectory(sPath);
                            }
                            GWApp.UnzipFiles(file.FullName, sPath);
                            // Delete .cwp package after unzip
                            file.Delete();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            // Find all the .cwp in the folder to unzip the package
            foreach (FileInfo file in TempDir.GetFiles())
            {
                if (file.Extension.ToUpper() == ".CWP")
                {
                    string sPath = file.FullName;
                    int positionLast = sPath.LastIndexOf('.');
                    sPath = sPath.Substring(0, positionLast);
                    if (!Directory.Exists(sPath))
                    {
                        Directory.CreateDirectory(sPath);
                    }
                    GWApp.UnzipFiles(file.FullName, sPath);
                    // Delete .cwp package after unzip
                    file.Delete();
                }
                else
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Repackage the template
        /// </summary>
        /// <param name="acFile"></param>
        /// <returns></returns>
        public string Repackage(string acFile)
        {
            string cwpFile = "";
            return cwpFile;
        }


        #endregion

        #region Client Method
        /// <summary>
        /// Convert the file to the latest version of Working Papers version
        /// </summary>
        /// <param name="acFilePath">Path to ac file</param>
        /// <returns>True if the conversion successful, false if any error</returns>
        public bool ConvertFile(string acFilePath)
        {
            try
            {
                CWApp.Clients.Convert(acFilePath);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message); // For debug purpose
                return false;
            }
        }

        /// <summary>
        /// Open the client file with provided username and password
        /// </summary>
        /// <param name="acFilePath">Client file path</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>CWClient instance if success, null otherwise</returns>
        public CWClient Open(string acFilePath, string username, string password)
        {
            CWClient client = null;
            try
            {
                if (ConvertFile(acFilePath))
                {
                    client = CWApp.Clients.Open2(acFilePath, username, password, CaseWare.CWOpenFlags.ofNone);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                client = null;
            }
            return client;
        }

        /// <summary>
        /// Close the current client
        /// </summary>
        /// <param name="client"></param>
        public void Close(CWClient client)
        {
            client.Close();
        }


        /// <summary>
        /// Compress and close file
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <returns>True/False</returns>
        public bool Compress(CWClient client)
        {
            try
            {
                client.CloseCompressed2(CaseWare.CWCompressFlags.cCompressAllSubFolders);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Compress the file based on file path
        /// </summary>
        /// <param name="acFilePath">Current File Path</param>
        /// <returns>True/False</returns>
        public bool Compress(string acFilePath)
        {
            try
            {
                CWApp.Clients.Compress2(acFilePath, CaseWare.CWCompressFlags.cCompressAllSubFolders);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Uncompress the _ac File
        /// </summary>
        /// <param name="_acFilePath">_ac File Path</param>
        /// <returns>True/False</returns>
        public bool UnCompress(string _acFilePath)
        {
            try
            {
                CWApp.Clients.Uncompress(_acFilePath);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Toggle protetion mode on the file
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="protection">True/False - Turn on/off protection</param>
        /// <returns>True success, false otherwise</returns>
        public bool ToggleProtection(CWClient client, string username, string password, bool protection)
        {
            CWSecurity security = client.Security;
            try
            {
                security.SetProtection(protection, username, password);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Check if the file has protection on
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <returns>True/False</returns>
        public bool IsProtected(CWClient client)
        {
            CWSecurity security = client.Security;
            return security.Protection;
        }

        /// <summary>
        /// Is the file check out (any document check out consider the file is checked out)
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <returns>True/False</returns>
        public bool IsCheckOut(CWClient client)
        {
            bool ischeckout = false;
            ischeckout = client.IsCheckedOut;
            if (!ischeckout)
            {
                // Loop throught document manager
                CWDocuments docMng = client.Documents;
                foreach (ICWDocument document in docMng)
                {
                    ischeckout = document.IsCheckedOut;
                    if (ischeckout)
                    {
                        break;
                    }
                }
            }
            return ischeckout;
        }

        /// <summary>
        /// Is File Lock Down
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <returns>True/False</returns>
        public bool IsLockedDown(CWClient client)
        {
            return client.IsLockedDown;
        }

        /// <summary>
        /// Is File Sign Out
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <returns>True/False</returns>
        public bool IsSignOut(CWClient client)
        {
            return client.IsSignedOut;
        }

        /// <summary>
        /// True if all childrent have sync 100% and false otherwise
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <returns>True/False</returns>
        public bool SmartSync(CWClient client)
        {
            bool IsSmartSync = true;
            int iLevel = -1;
            int hasSyncInfo = client.HasSynchronizationInformation;
            /* File need to be open to check if there is Sync Child */
            CWSynchronizationClientEntries oSyncClientEntries = client.SynchronizationTree;
            int nChilds = oSyncClientEntries.Count;

            /**************************************************************
            * Recursively traverse the synchronization tree, printing entries
            *   to the document
            *   E.g:
            *   C:\Master\Master.ac  100%
            *   C:\Synchronized Child1\Synchronized Child1.ac      50%
            *   C:\Synchronized Child2\Synchronized Child2.ac      100%
            *************************************************************/
            if (oSyncClientEntries != null)
            {
                iLevel++;
                foreach (CWSynchronizationClientEntry oSyncClientEntry in oSyncClientEntries)
                {
                    if (oSyncClientEntry.Current)
                    {
                        // Returns whether or not the client entry corresponds to the current client.
                        continue;
                    }
                    if (oSyncClientEntry.Root)
                    {
                        // This is Master in the tree
                        continue;
                    }
                    else
                    {
                        int Percentage = oSyncClientEntry.Completed;
                        if (Percentage < 100)
                        {
                            // There is a Child not sync to 100%, should not run the update
                            IsSmartSync = false;
                            break;
                        }
                    }
                }
            }
            return IsSmartSync;
        }

        /// <summary>
        /// Get the engagement setting on the file for Operating name
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <returns></returns>
        public string GetOperatingName(CWClient client)
        {
            return client.ClientProfile.Name;
        }

        /// <summary>
        /// Get the engagement setting on the file for client name
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <returns></returns>
        public string GetClientName(CWClient client)
        {
            return client.ClientProfile.ClientNumber;
        }

        /// <summary>
        /// Launching CaseWare Folder chooser
        /// </summary>
        /// <param name="srcPath">Source Folder Path</param>
        /// <returns>Seleted Folder Path</returns>
        public string LaunchFolderDialog(string srcPath)
        {
            string folderPath = "";
            CaseWare.CWUtilities CWUTIL = new CWUtilities();
            string ClientPath = srcPath;
            if (ClientPath == "")
            {
                ClientPath = CWBKPath;
            }
            folderPath = CWUTIL.FolderDialog("", ClientPath, true, true);
            return folderPath;
        }

        /// <summary>
        /// Clean up history from client
        /// </summary>
        /// <param name="client"></param>
        public void CleanUpHistory(CWClient client)
        {
            // Remove users
            client.Security.Users.RemoveAll();
            // Remove doc history
            client.ClientDocHistory.RemoveAll();
            // Remove client history
            client.ClientHistory.RemoveAll();
        }
        #endregion

        #region Database method
        /// <summary>
        /// Set value to CV database by Active Client
        /// </summary>
        /// <param name="client">Current Client</param>
        /// <param name="group">Group</param>
        /// <param name="form">Form</param>
        /// <param name="id">Id</param>
        /// <param name="value">Value</param>
        public void SetValue(CWClient client, string group, string form, string id, string value)
        {
            CWCaseViewData CVDB = client.CaseViewData;
            CVDB.SetGroupFormIdData(group, form, id, CaseWare.CvDataType.CvDataTypeAuto, CaseWare.CvDataOvrFlag.CvDataOvrAuto, value);
        }

        /// <summary>
        /// Set value to CV database by ac File Path
        /// </summary>
        /// <param name="acFilePath">AC File Path</param>        
        /// <param name="group">Group</param>
        /// <param name="form">Form</param>
        /// <param name="id">Id</param>
        /// <param name="value">Value</param>
        public void SetValue(string acFilePath, string group, string form, string id, string value)
        {
            string dbfFile = System.IO.Path.Combine(acFilePath.ToUpper().Replace(".AC", "CV.DBF"));
            if (System.IO.File.Exists(dbfFile))
            {
                CWCaseViewData DBEntry = CWApp.SystemCaseViewDataSet(dbfFile, group, form, id);
                DBEntry.DataGroupFormId[group, form, id] = value;
            }
        }

        /// <summary>
        /// Read value from CV database
        /// </summary>
        /// <param name="client"></param>
        /// <param name="group"></param>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ReadValue(CWClient client, string group, string form, string id)
        {
            CWCaseViewData CVDB = client.CaseViewData;
            if (CVDB.ExistsGroupFormId[group, form, id])
            {
                return System.Convert.ToString(CVDB.DataGroupFormId[group, form, id]);
            }
            return "";
        }

        /// <summary>
        /// Read value from CV database
        /// </summary>
        /// <param name="acFilePath"></param>
        /// <param name="client"></param>
        /// <param name="group"></param>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ReadValue(string acFilePath, string group, string form, string id)
        {
            string dbfFile = System.IO.Path.Combine(acFilePath.ToUpper().Replace(".AC", "CV.DBF"));
            if (System.IO.File.Exists(dbfFile))
            {
                CWCaseViewData DBEntry = CWApp.SystemCaseViewDataSet(dbfFile, group, form, id);
                var value = DBEntry.DataGroupFormId[group, form, id];
                return value.ToString();
            }
            return "";
        }

        /// <summary>
        /// Check the entry in CV database
        /// </summary>
        /// <param name="client"></param>
        /// <param name="group"></param>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(CWClient client, string group, string form, string id)
        {
            CWCaseViewData CVDB = client.CaseViewData;
            return CVDB.ExistsGroupFormId[group, form, id];
        }

        /// <summary>
        /// Check the entry in CV database
        /// </summary>
        /// <param name="acFilePath"></param>
        /// <param name="group"></param>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(string acFilePath, string group, string form, string id)
        {
            string dbfFile = System.IO.Path.Combine(acFilePath.ToUpper().Replace(".AC", "CV.DBF"));
            if (System.IO.File.Exists(dbfFile))
            {
                CWCaseViewData DBEntry = CWApp.SystemCaseViewDataSet(dbfFile, group, form, id);
                return DBEntry.ExistsGroupFormId[group, form, id];
            }
            return false;
        }

        /// <summary>
        /// Delete entry in CV database
        /// </summary>
        /// <param name="client"></param>
        /// <param name="group"></param>
        /// <param name="form"></param>
        /// <param name="id"></param>
        public void Delete(CWClient client, string group, string form, string id)
        {
            CWCaseViewData CVDB = client.CaseViewData;
            if (CVDB.ExistsGroupFormId[group, form, id])
            {
                CVDB.DeleteGroupFormId(group, form, id);
            }
        }

        /// <summary>
        /// Delete entry in CV database
        /// </summary>
        /// <param name="acFilePath"></param>
        /// <param name="group"></param>
        /// <param name="form"></param>
        /// <param name="id"></param>
        public void Delete(string acFilePath, string group, string form, string id)
        {
            string dbfFile = System.IO.Path.Combine(acFilePath.ToUpper().Replace(".AC", "CV.DBF"));
            if (System.IO.File.Exists(dbfFile))
            {
                CWCaseViewData DBEntry = CWApp.SystemCaseViewDataSet(dbfFile, group, form, id);
                DBEntry.DeleteGroupFormId(group, form, id);
            }
        }

        #endregion

        #region Script Method

        /// <summary>
        /// Run Script when opening document [document path, script name, script file path, script function, arguments]
        /// </summary>
        /// <param name="sDocPath"></param>
        /// <param name="scriptName"></param>
        /// <param name="scriptPath"></param>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool RunScriptOnDocument(string sDocPath, string scriptName, string scriptPath, string funcName, string[] args)
        {
            try
            {
                GC.Collect();
                /*Open the document */
                CvGtwyDocument oCurDoc = GWApp.Documents.Open(sDocPath, 0);
                // Run the script                                                
                oCurDoc.RunScriptVariant(scriptName, scriptPath, funcName, args);
                // Close the document                
                oCurDoc.Save();
                oCurDoc.Close(1);
                oCurDoc = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public CvGtwyDocument OpenDocumentWithoutScript(string docPath)
        {
            CvGtwyDocument oRetDoc = null;
            if (File.Exists(docPath))
            {
                // try to open the document
                while (oRetDoc == null)
                {
                    try
                    {
                        oRetDoc = GWApp.Documents.Open(docPath, 4);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("================== ErrorEvent ====================");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("================== ErrorEvent ====================");
                        Console.WriteLine();
                    }
                }
            }
            return oRetDoc;
        }

        public List<ScriptInfo> GetScriptList(CvGtwyDocument CVDoc)
        {
            List<ScriptInfo> scriptList = new List<ScriptInfo>();
            if (CVDoc != null)
            {
                CvGtwyScripts CVScripts = CVDoc.Scripts;
                int numberScript = CVScripts.Count;
                for (int index = 0; index <= numberScript; index++)
                {
                    // Get the script name by index
                    ScriptInfo script = GetScriptNameByIndex(CVScripts, index);
                    if (script != null)
                    {
                        scriptList.Add(script);
                    }
                }
            }
            return scriptList;
        }

        private ScriptInfo GetScriptNameByIndex(CvGtwyScripts cVScripts, int index)
        {
            ScriptInfo script = null;
            string[] passList = { "Blu3b1rd", "5AlivePie", "" };
            foreach (string pass in passList)
            {
                try
                {
                    CvGtwyScript currentScript = cVScripts[index, pass];
                    if (currentScript != null && currentScript.Type == 2)
                    {
                        if (currentScript.Name.Contains("UTILI"))
                        {
                            string test = currentScript.Name;
                        }
                        script = new ScriptInfo(currentScript.Name, currentScript.FLibrary);
                        break;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return script;
        }

        public void DeleteScriptInDoc(CvGtwyDocument CVDoc, List<ScriptInfo> scriptList)
        {
            if (CVDoc != null)
            {
                CvGtwyScripts CVScripts = CVDoc.Scripts;
                int numberScript = CVScripts.Count;
                for (int index = 0; index < scriptList.Count; index++)
                {
                    // Get the script name by index
                    DeleteScriptByName(CVScripts, scriptList[index].Name);
                }
                // Save the document
                CVDoc.Save();
            }
        }

        private void DeleteScriptByName(CvGtwyScripts CVScripts, string name)
        {
            string[] passList = { "Blu3b1rd", "5AlivePie", "" };
            foreach (string pass in passList)
            {
                try
                {
                    if (CVScripts.DeleteScript(name, pass) > 0)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public void SetScriptToGlobal(string docPath, List<ScriptInfo> listScripts, string newScriptPath)
        {
            int numberScript = listScripts.Count;
            CvGtwyDocument CVDoc = OpenDocumentWithoutScript(docPath);
            if (CVDoc != null)
            {
                CvGtwyScripts CVScripts = CVDoc.Scripts;
                for (int index = 0; index < numberScript; index++)
                {
                    ScriptInfo script = listScripts[index];
                    string scriptPath = Path.Combine(newScriptPath, script.Name + ".scp");
                    if (!File.Exists(scriptPath))
                    {
                        scriptPath = Path.Combine(newScriptPath, "Extracted Classes", script.Name + ".scp");
                    }
                    if (!File.Exists(scriptPath))
                    {
                        continue;
                    }
                    CvGtwyScript newScript = CVScripts.InsertScript(script.Name, 3, scriptPath);
                    if (newScript != null)
                    {
                        newScript.FLibrary = script.Library;
                    }
                }
                try
                {
                    CVDoc.Save();
                    CVDoc.Close(1);
                }
                catch (Exception) { }
            }
        }
        #endregion

        #region Dialog
        public string FolderDialog(string initialPath, string title)
        {
            string defaultPath = initialPath != "" && initialPath != null && Directory.Exists(initialPath) ? initialPath : CWAppPath;
            CWUtilities util = new CWUtilities();
            return util.FolderDialog(title, initialPath, true, true);
        }

        public string FileDialog(string initialPath, string ext, string title)
        {
            string defaultPath = initialPath != "" && initialPath != null && Directory.Exists(initialPath) ? initialPath : CWAppPath;
            CWUtilities util = new CWUtilities();
            return util.FileDialog(1, title, initialPath, ext, ext);
        }
        #endregion
    }

    public class ScriptInfo
    {
        public ScriptInfo(string name, int lib)
        {
            Name = name;
            Library = lib;
        }

        public string Name { get; set; }
        public int Library { get; set; }
    }
}
