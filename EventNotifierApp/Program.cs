using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Security;
using SendSMTPLibrary;
using System.Runtime.CompilerServices;

public class Program
{
    
    private static IConfiguration _config;
    private static string _textFile;
    private static EmailSettingsOptions _emailOptions;

    private static void Main(string[] args)
    {

        InitializeConfiguration();
        
        var fileAccess = new TextFileDataAccess();
        
        var fileContents = fileAccess.ReadAllRecords(_textFile);

        //Console.WriteLine($"Todays Month: {DateTime.Now.Month}  Todays Day: {DateTime.Now.Day}");
        //foreach (var record in fileContents)
        //{
        //    Console.WriteLine($"Date: {record.EventDate} Subject: {record.Subject} Event: {record.Event}");
        //}

        CheckEventDateMatchesTodaysDate(fileContents);

        fileAccess.WriteAllRecords(fileContents, _textFile);

        Console.WriteLine("Finished Executing");
    }

    public static void CheckEventDateMatchesTodaysDate(List<EventsModel> events)
    {
        var sendMail = new SendEmail();
        
        foreach(var e in events)
        {
            if(e.EventDate.Day == DateTime.Now.Day && e.EventDate.Month == DateTime.Now.Month)
            {
                sendMail.SendEmailMessage(e.Subject, e.Event, String.Join(',', e.ToEmailAddresses), _emailOptions);
            }
        }
    }

    public static void InitializeConfiguration()
    {

        /* Build the path to the appsettings.json file */
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddUserSecrets<EmailSettingsOptions>();
        
        _config = builder.Build();

        //_textFile = _config.GetValue<string>("CSVFile");
        _textFile = _config.GetValue<string>("TextFile");

        _emailOptions = _config.GetSection("EmailSettings").Get<EmailSettingsOptions>();
    }

}