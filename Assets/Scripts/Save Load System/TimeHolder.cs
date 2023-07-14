using System;

namespace Save_Load_System
{
    [Serializable]
    public class TimeHolder
    {
        public int hours;
        public int minutes;

        public TimeHolder()
        {
            hours = 0;
            minutes = 0;
        }

        public TimeHolder(string timer)
        {
            ParseTime(timer, out minutes, out hours);
        }

        private void ParseTime(string time, out int resultMinutes, out int resultHours)
        {
            string[] timeParts = time.Split(':');

            resultHours = int.Parse(timeParts[0]);
            resultMinutes = int.Parse(timeParts[1]);
        }
    }
}