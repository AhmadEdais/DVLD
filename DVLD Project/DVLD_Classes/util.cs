using Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Classes
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {

            // Generate a new GUID
            Guid newGuid = Guid.NewGuid();

            // convert the GUID to a string
            return newGuid.ToString();

        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    clsUtil.LogEvent(ex);
                }

            }

                return true;

        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            // Full file name. Change your file name   
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;

        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            // this funciton will copy the image to the
            // project images foldr after renaming it
            // with GUID with the same extention, then it will update the sourceFileName with the new name.

            string DestinationFolder = @"C:\DVLD-People-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationFile, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsSettings.LogEvent(iox.Message);
                return false;
            }

            sourceFile = destinationFile;
            return true;
        }
        public static void SaveCredentialsToReg(string username, string password)
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD";
            string dataToSave = username + '#' + password;
            try
            {
                Microsoft.Win32.Registry.SetValue(keyPath, "Credentials", dataToSave);
            }
            catch (Exception ex)
            {
                clsSettings.LogEvent(ex);
            }
        }
        public static void _SaveCredentialsToFile(string username, string password)
        {
            // Build the file path (it will be created next to your .exe)
            string filePath = "login_info.txt";

            // Create a simple string format: "username#password"
            // We use '#' as a separator.
            string dataToSave = username + '#' + password;

            try
            {
                // This writes the text to the file (overwriting it if it exists)
                File.WriteAllText(filePath, dataToSave);
            }
            catch (Exception ex)
            {
                clsSettings.LogEvent(ex);
                MessageBox.Show("Error saving credentials: " + ex.Message);
            }
        }
        private static string _sourceName = "DVLD_App"; // Define your source name once

        public static void LogEvent(string message, EventLogEntryType type = EventLogEntryType.Information)
        {
            try
            {
                // Check if the source exists. 
                // NOTE: Creating a source requires Admin privileges. 
                // If the app runs as standard user, this must be created beforehand (e.g. by an installer).
                if (!EventLog.SourceExists(_sourceName))
                {
                    EventLog.CreateEventSource(_sourceName, "Application");
                }

                EventLog.WriteEntry(_sourceName, message, type);
            }
            catch (Exception ex)
            {
                clsUtil.LogEvent(ex);
            }
        }

        // Overload for easy Exception logging
        public static void LogEvent(Exception ex)
        {
            // Log the message + the location (StackTrace)
            string errorMessage = $"Message: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}";
            LogEvent(errorMessage, EventLogEntryType.Error);
        }
    }
}
