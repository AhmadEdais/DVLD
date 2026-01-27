using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Settings
{
    public class clsSettings
    {
        public enum enPeopleFilterOptions
        {
            None,
            PersonID,
            NationalNo,
            FirstName,
            SecondName,
            ThirdName,
            LastName,
            Nationality,
            Gendor,
            Phone,
            Email
        }
        private static string _sourceName = "DVLD"; // Define your source name once

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
                clsSettings.LogEvent(ex);
            }
        }

        // Overload for easy Exception logging
        public static void LogEvent(Exception ex)
        {
            // Log the message + the location (StackTrace)
            string errorMessage = $"Message: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}";
            LogEvent(errorMessage, EventLogEntryType.Error);
        }
        public static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }

}
