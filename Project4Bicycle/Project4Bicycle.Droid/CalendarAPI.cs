using System;
using Android.OS;
using Android.Provider;
using Android.Content;

using Xamarin.Forms;
using Java.Util;
using Android.Net;
using Android.Locations;
using Android.App;
using Java.IO;

[assembly: Dependency(typeof(Project4Bicycle.Droid.CalendarAPI))]

namespace Project4Bicycle.Droid
{
  public class CalendarAPI : ICalendar
  {
    public void SetEvent(DateTime date, string title, string note)
    {
      ContentValues eventValues = new ContentValues();
      eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
      eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, title);
      eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, note);
      eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(date.Year, date.Month, date.Day, date.Hour, date.Minute));
      eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(date.Year, date.Month, date.Day, date.Hour, date.AddMinutes(30).Minute));

      eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
      eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

      //Android.Content.ContentResolver.insert
      var uri = Forms.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
      var id = uri.LastPathSegment;
    }

    public void SetReminder(string title)
    {
      var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
      var filename = System.IO.Path.Combine(path.ToString(), "reminder.txt");

      System.IO.File.WriteAllText(filename, title);
      //using (var streamWriter = new System.IO.StreamWriter(filename, true))
      //{
      //  System.IO.File.WriteAllText(filename, title);
      //  //streamWriter.WriteLine(title);
      //}
    }
    public string getReminder()
    {
      string reminderText = "Memes";
      
      var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
      var filename = System.IO.Path.Combine(path.ToString(), "reminder.txt");
      File file = new File(filename);

      if (!System.IO.File.Exists(filename))
      {
        return "No reminder set";
      }
      reminderText = System.IO.File.ReadAllText(filename);
      //using (var streamReader = new System.IO.StreamReader(filename, true))
      //{
      //  //System.IO.File.WriteAllText(filename, title);
      //  //streamWriter.WriteLine(title);
      //  reminderText = streamReader.ReadLine();
      //}
      return reminderText;
    }

    long GetDateTimeMS(int yr, int month, int day, int hr, int min)
    {
      Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

      c.Set(Calendar.DayOfMonth, day);
      c.Set(Calendar.HourOfDay, hr);
      c.Set(Calendar.Minute, min);
      c.Set(Calendar.Month, month - 1);
      c.Set(Calendar.Year, yr);

      return c.TimeInMillis;
    }
  }
}

