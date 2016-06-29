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

		public void SetEvent(int alarm, int start, int length, string title, string note)
		{
			if (eventStore == null)
			{
				requestAccess();
			}

			// set the controller's event store - it needs to know where/how to save the event
			eventController.EventStore = eventStore;



			EKEvent newEvent = EKEvent.FromStore(eventController.EventStore);
			// set the alarm for 10 minutes from now
			newEvent.AddAlarm(EKAlarm.FromDate((Foundation.NSDate)DateTime.Now.AddMinutes(10)));
			// make the event start 20 minutes from now and last 30 minutes
			newEvent.StartDate = (Foundation.NSDate)DateTime.Now.AddMinutes(20);
			newEvent.EndDate = (Foundation.NSDate)DateTime.Now.AddMinutes(50);
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
	}
}

