using System;
using Android.OS;
using Android.Provider;
using Android.Content;

using Xamarin.Forms;
using Java.Util;


[assembly: Dependency(typeof(Project4Bicycle.Droid.CalendarAPI))]

namespace Project4Bicycle.Droid
{
	public class CalendarAPI : ICalendar
	{
		public CalendarAPI()
		{
		}

		public void SetEvent(int alarm, int start, int length, string title, string note)
		{
			ContentValues eventValues = new ContentValues();
			eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Test Event from M4A");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "This is an event created from Xamarin.Android");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(2011, 12, 15, 10, 0));
			eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(2011, 12, 15, 11, 0));

			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

			//Android.Content.ContentResolver.insert
			var uri = Forms.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
			var id = uri.LastPathSegment;
		}

		public void SetReminder(string title)
		{
			throw new NotImplementedException();
		}

		long GetDateTimeMS(int yr, int month, int day, int hr, int min)
		{
			Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

			c.Set(Calendar.DayOfMonth, 22);
			c.Set(Calendar.HourOfDay, 11);
			c.Set(Calendar.Minute, 00);
			c.Set(Calendar.Month, Calendar.June);
			c.Set(Calendar.Year, 2016);

			return c.TimeInMillis;
		}
	}
}

