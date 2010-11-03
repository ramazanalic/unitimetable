using System;
using System.Collections.Generic;
using System.Text;

namespace UniTimetable
{
    public class TimetableSession
    {
        string timeframe;
        string offered;
        string campus;
        string type;
        string url;

        public TimetableSession(string timeframe, string offered, string campus, string type, string url)
        {
            this.timeframe = timeframe;
            this.offered = offered;
            this.campus = campus;
            this.type = type;
            this.url = url;
        }

        public string getTimeframe { get { return this.timeframe; } }
        public string getOffered { get { return this.offered; } }
        public string getCampus { get { return this.campus; } }
        public string getType { get { return this.type; } }
        public string getURL { get { return this.url; } }

    }
}
