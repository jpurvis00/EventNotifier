using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Security;
using SendSMTPLibrary;


public class Program
{
    

    private static void Main(string[] args)
    {

        var fileAccess = new TextFileDataAccess();
        fileAccess.InitializeConfiguration();
        
        var fileContents = fileAccess.ReadAllRecords();

        //Console.WriteLine($"Todays Month: {DateTime.Now.Month}  Todays Day: {DateTime.Now.Day}");
        //foreach (var record in fileContents)
        //{
        //    Console.WriteLine($"Date: {record.EventDate} Subject: {record.Subject} Event: {record.Event}");
        //}

        //CheckEventDateMatchesTodaysDate(fileContents);

        //fileAccess.WriteAllRecords(fileContents);

        Console.WriteLine("Finished Executing");
    }

    public static void CheckEventDateMatchesTodaysDate(List<EventsModel> events)
    {
        var sendMail = new SendEmail();
        
        foreach(var e in events)
        {
            if(e.EventDate.Day == DateTime.Now.Day && e.EventDate.Month == DateTime.Now.Month)
            {
                sendMail.SendEmailMessage(e.Subject, e.Event, String.Join(',', e.ToEmailAddresses));
            }
        }
    }

}