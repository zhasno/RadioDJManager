using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RadioDJManager.Data;
using RadioDJManager.Models;

namespace RadioDJManager
{
    public static class Extensions
    {
        /// <summary>
        /// Gets the folder from where the song is played
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public static  string GetSongFolder(this EventAction track, RadioDbContext Db)
        {

            var folderName = "";

            if (track == null)
                return folderName;
            if (track.ActionValue == null)
                return folderName;

            var id = track.GetSongId();
            if ((id[0] != -1) && (id[1] != -1))
            {
                var song = Db.songs.Find(id[1]);
                folderName = Path.GetDirectoryName(song.path);
            }

            return folderName;
        }


        public static IEnumerable<Song> GetSongsFromFolder(this string folderName, RadioDbContext Db)
        {

            return Db.songs.Where(s => s.path.StartsWith(folderName)).ToList();

            //RefreshDb();
            var songList = Db.songs.ToList();

            var list = new List<Song>();
            foreach (var song in songList)
            {
                if (song.path.StartsWith(folderName))
                {
                    list.Add(song);
                }
            }

            return list;
        }

        public static Exception GetLastInnerException(this Exception exception)
        {
            if (exception.InnerException == null) return exception;

            Exception innerEx = exception;
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }

            return innerEx;
        }

        public static string GetLastErrorMessage(this Exception exception)
        {
            if (exception == null) return "";

            string message = exception.Message;
            Exception inner = exception.InnerException;
            while (inner != null && !string.IsNullOrEmpty(inner.Message))
            {
                message = inner.Message;
                inner = inner.InnerException;
            }

            return message;
        }

        /// <summary>
        /// Calculates when the event will finish and its length  (i.e:the sum of lengths of all its tracks (or actions))
        /// </summary>
        /// <param name="inputEvent"></param>
        /// <returns>Returns the time when the event will finish converted to seconds</returns>
        public static double[] CalculateEventEndTime(this RadioEvent radioEvent, RadioDbContext dbContext)
        {
            double[] timeDouble = { 0, 0 };

            if (radioEvent == null)
            {
                //ModernDialog.ShowMessage("Model is null", "Error Calculating event duration", MessageBoxButton.OK);
                return timeDouble;
            }
            if (radioEvent.actions == null)
            {
                //ModernDialog.ShowMessage("Model.actions is null", "Error Calculating event duration", MessageBoxButton.OK);
                return timeDouble;
            }
            if (radioEvent.time == null)
            {
                //ModernDialog.ShowMessage("Model.time is null", "Error Calculating event duration", MessageBoxButton.OK);
                return timeDouble;
            }

            double totalDuration = 0;

            foreach (var track in radioEvent.actions)
            {
                if (track.ActionValue.Trim().StartsWith("Load Track By ID"))
                {
                    var id = track.GetSongId();
                    if ((id[0] != -1) && (id[1] != -1))
                    {
                        var song = dbContext.songs.Find(id[1]);
                        totalDuration += Convert.ToDouble(song.duration);//in seconds
                    }
                }

            }

            timeDouble[0] = totalDuration;//duration of all tracks on the event in seconds
            timeDouble[1] = radioEvent.time.ConvertTimeStringToSeconds();
            timeDouble[1] += totalDuration;//the end time of the event in seconds

            return timeDouble;
        }

        /// <summary>
        /// Converts a string to time structure
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns>time in seconds</returns>
        public static double ConvertTimeStringToSeconds(this string timeString)
        {
            if (string.IsNullOrEmpty(timeString))
                return 0;

            //hh:mm:ss
            var parts = timeString.Split(':');
            //switch (parts.Length)
            //{
            //    case 2:
            //        return ((Convert.ToDouble(parts[0]) * 60) + (Convert.ToDouble(parts[1]) ));
            //    case 3:
            //        return ((Convert.ToDouble(parts[0]) * 3600) + (Convert.ToDouble(parts[1]) * 60) + (Convert.ToDouble(parts[2])));
            //    default:
            //        return 0;
            //}
            return ((Convert.ToDouble(parts[0]) * 3600) + (Convert.ToDouble(parts[1]) * 60));
        }

        /// <summary>
        /// Converts a time structure to string
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertTimeToString(this double time)
        {
            var hour = Math.Truncate(time / 3600);
            var minute = Math.Truncate((time - (hour * 3600)) / 60);
            var second = Math.Truncate(time - (hour * 3600) - (minute * 60));
            //////return TimeSpan.FromSeconds(time).ToString();
            //return string.Format("{0}:{1}:{2}",hour,minute,second);
            return $"{hour.ToString("00")}:{minute.ToString("00")}:{second.ToString("00")}";
        }

        /// <summary>
        /// Gets the song ID
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static int[] GetSongId(this EventAction action)
        {
            int[] id = { -1, -1 };

            //Load Track By ID|0|36|the daylights - black dove mp3 download.mp3|Top
            //Load Track From Category|0|4|0|0|1|True|2000s|Any Genre|Least Recently Played|Top
            var parts = action.ActionValue.Split('|');
            if (parts.Length >= 4)
            {
                id[0] = Int32.Parse(parts[1]);
                id[1] = Int32.Parse(parts[2]);
            }

            return id;
        }

        /// <summary>
        /// Parses the actions from the data field of the event
        /// </summary>
        /// <param name="radioEvent"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetActions(this RadioEvent radioEvent)
        {
            /* '\r\n' */
            return radioEvent.data
                            .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                            .ToList();
        }

        public static DayElement GetDayElementForDateString(this string dateString)
        {
            return Utils
                        .Instance
                        .CsvContent
                        //.Select(d => DateTime.ParseExact(d.Date, "yyyy-MM-dd", CultureInfo.CurrentCulture))
                        .FirstOrDefault(d => d.Date.Trim().Equals(dateString.Trim()));
        }
    }

}
