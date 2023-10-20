using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {
        public List<EventsModel> ReadAllRecords(string _textFile)
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

        public void WriteAllRecords(List<EventsModel> events, string _textFile)
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
