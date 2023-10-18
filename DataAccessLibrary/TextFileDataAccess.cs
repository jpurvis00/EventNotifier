using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {
        private static IConfiguration _config;
        private static string _textFile;
        
        public void InitializeConfiguration()
        {
            /* Build the path to the appsettings.json file */
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            _config = builder.Build();

            //_textFile = _config.GetValue<string>("CSVFile");
            _textFile = _config.GetValue<string>("TextFile");
        }

        public List<EventsModel> ReadAllRecords()
        {

            if (File.Exists(_textFile) == false)
            {
                return new List<EventsModel>();
            }

            var output = new List<EventsModel>();
            var lines = File.ReadAllLines(_textFile);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
                EventsModel events = new EventsModel();

                var val = line.Split(',');

                /* val.Length does not seem to be working.  spot 4 is showing as "".  This is only in a csv file.
                 * If I change it to a text file, this seems to work. */
                if (val.Length < 4)
                {
                    throw new Exception($"There are not enough arguments on this line {line}");
                }

                DateTime eventDate;
                if (DateTime.TryParse(val[0], out eventDate) == false)
                {
                    throw new Exception($"Date is not in the correct format {val[0]}.");
                }

                events.EventDate = eventDate;
                events.Subject = val[1];
                events.Event = val[2];
                events.ToEmailAddresses = val[3].Split(';').ToList();

                output.Add(events);
            }

            return output;
        }

        public void WriteAllRecords(List<EventsModel> events)
        {
            var lines = new List<string>();

            foreach(var e in events)
            {
                string singleEvent = $"{e.EventDate},{e.Subject},{e.Event},{String.Join(';', e.ToEmailAddresses)}";
                lines.Add(singleEvent);
            }

            File.WriteAllLines(_textFile, lines);
        }
    }
}
