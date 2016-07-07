using System;
using Xamarin.Forms;
using EventKit;
using Foundation;
using UIKit;

[assembly: Dependency(typeof(Project4Bicycle.iOS.CalendarAPI))]

namespace Project4Bicycle.iOS
{
	public class CalendarAPI : ICalendar
	{
		EventKitUI.EKEventEditViewController eventController = new EventKitUI.EKEventEditViewController();
		public EKEventStore eventStore { get; set; }
		private bool accessGranted = false;

		public void SetEvent(DateTime date, string title, string note)
		{
			if (eventStore == null)
			{
				requestAccess();
			}

			// set the controller's event store - it needs to know where/how to save the event
			eventController.EventStore = eventStore;


			NSDate date2 = DateTimeToNSDate2(date);

			EKEvent newEvent = EKEvent.FromStore(eventController.EventStore);
			// set the alarm for 10 minutes from now

			newEvent.AddAlarm(EKAlarm.FromDate(date2));
			// make the event start 20 minutes from now and last 30 minutes
			newEvent.StartDate = date2;
			newEvent.EndDate = date2.AddSeconds(1800);
			newEvent.Title = title;
			newEvent.Notes = note;
			newEvent.Calendar = eventController.EventStore.DefaultCalendarForNewEvents;
			NSError a;
			eventController.EventStore.SaveEvent(newEvent, EKSpan.ThisEvent, out a);
			Console.WriteLine("Event Saved, ID: " + newEvent.CalendarItemIdentifier);
		}

		public void SetReminder(string title)
		{

			EventKitUI.EKEventEditViewController eventController = new EventKitUI.EKEventEditViewController();

			eventStore = new EKEventStore();
			eventStore.RequestAccess(EKEntityType.Reminder,
				(bool granted, NSError i) =>
				{
					if (granted)
					{
						accessGranted = true;
					}
					else
					{
						accessGranted = false;
					}
				});

			eventController.EventStore = eventStore;

			EKReminder reminder = EKReminder.Create(eventController.EventStore);
			reminder.Title = title;
			reminder.Calendar = eventController.EventStore.DefaultCalendarForNewReminders;


			// save the reminder
			NSError e;
			eventController.EventStore.SaveReminder(reminder, true, out e);
		}

		public void requestAccess()
		{
			eventStore = new EKEventStore();
			eventStore.RequestAccess(EKEntityType.Event,
				(bool granted, NSError e) =>
				{
					if (granted)
					{
						accessGranted = true;
					}
					else
					{
						accessGranted = false;
					}
				});
		}

		public NSDate DateTimeToNSDate2(DateTime date)
		{
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
				new DateTime(2001, 1, 1, 0, 0, 0));
			return NSDate.FromTimeIntervalSinceReferenceDate(
				(date - reference).TotalSeconds);
		}

		public string getReminder()
		{
			return ("No");
		}
	}
}

