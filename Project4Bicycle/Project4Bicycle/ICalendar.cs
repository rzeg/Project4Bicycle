using System;

namespace Project4Bicycle
{
	public interface ICalendar
	{
		void SetEvent(DateTime date, string title, string note);
		void SetReminder(string title);

		string getReminder();

		//void GetCalendars();
	}
}

