using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Messaging;
using RadioDJManager.Data;
using RadioDJManager.Events;
using RadioDJManager.Models;
using RadioDJManager.ViewModels;

namespace RadioDJManager
{
    public class Utils : ISubscriber<CsvPathChangedMsg>
    {
        #region Variables

        private static readonly object _padlock = new object();
        private static Utils _instance = null;
        public ObservableCollection<DayElement> CsvContent { get; set; }


        public static Utils Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                        _instance = new Utils();
                }

                return _instance;
            }
        } 

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public Utils()
        {
            CsvContent = new ObservableCollection<DayElement>();

            LoadCSV(Properties.Settings.Default.CSVPath);
        }



        /// <summary>
        /// Loads the data from the csv file
        /// </summary>
        private void LoadCSV(string path)
        {
            CsvContent.Clear();

            if (!File.Exists(path))
            {
                //throw new FileNotFoundException($"The file {path} was not found!\n\nThe program will close");
                return;
            }

            using (var sr = new StreamReader(path))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 7)
                        CsvContent.Add(new DayElement
                        {
                            Date = parts[0],
                            Subh = parts[1],
                            Dhuhr = parts[3],
                            Asr = parts[4],
                            Maghrib = parts[5],
                            Isha = parts[6]
                        });
                }
            }

        }


        /// <summary>
        /// Saves the state
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="Model"></param>
        public void SaveState<T>(string fileName, T Model)
        {
            File.Delete(fileName);

            using (var sw = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(sw, Model);
            }

        }


        /// <summary>
        /// Loads the state
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T LoadState<T>(string fileName)
        {
            var type = typeof(T);
            var cons = type.GetConstructor(Type.EmptyTypes);//default(T);

            var model = (T)cons.Invoke(null);

            if (fileName == null)
                return model;

            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                return model;
            }

            using (var sr = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                var formatter = new XmlSerializer(typeof(T));

                model = (T)formatter.Deserialize(sr);
            }


            return model;
        }


        /// <summary>
        /// Loads the rotation state (not used)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ObservableCollection<RotatorViewModel> LoadRotationState(string fileName)
        {
            var model = new ObservableCollection<RotatorViewModel>();

            if ((fileName.Equals("")) || !File.Exists(fileName))
            {
                return model;
            }

            var formatter = new BinaryFormatter();
            using (var sw = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                model = (ObservableCollection<RotatorViewModel>)formatter.Deserialize(sw);
            }
            return model;
        }


        public static RadioDbContext CreateDbContext()
        {
            var connectionString = string.Format("server={0};user id={1};password={2};persistsecurityinfo=True;database={3}",
                                Properties.Settings.Default.Server,
                                Properties.Settings.Default.Username,
                                Properties.Settings.Default.Password,
                                Properties.Settings.Default.Database
                                );

#if DEBUG
            //ConnectionString = "server=127.0.0.1;user id=root;password=great;persistsecurityinfo=True;database=radiodj161";
#endif

            var connection = DbProviderFactories.GetFactory("MySql.Data.MySqlClient")
                                                .CreateConnection();
            connection.ConnectionString = connectionString;
            return new RadioDbContext(connection, true);
        }



        /// <span class="code-SummaryComment"><summary></span>
        /// Perform a deep Copy of the object.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><typeparam name="T">The type of object being copied.</typeparam></span>
        /// <span class="code-SummaryComment"><param name="source">The object instance to copy.</param></span>
        /// <span class="code-SummaryComment"><returns>The copied object.</returns></span>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public void HandleMessage(CsvPathChangedMsg msg)
        {
            LoadCSV(msg.Content);
        }
        #endregion


    }
}
