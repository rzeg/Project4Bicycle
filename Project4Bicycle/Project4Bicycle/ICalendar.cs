using System;
namespace Project4Bicycle
{
	public interface ICalendar
	{
		void SetEvent(int alarm, int start, int length, string title, string note);
		void SetReminder(string title);
	}
}

