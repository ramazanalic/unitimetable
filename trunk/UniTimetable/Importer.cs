using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;

namespace UniTimetable
{
    class Importer
    {
        #region Property variables

        protected string FormatName_ = null;
        protected string University_ = null;
        protected string CreatedBy_ = null;
        protected string LastUpdated_ = null;

        protected string File1Description_ = null;
        protected string File2Description_ = null;
        protected string File3Description_ = null;
        protected string FileInstructions_ = null;

        protected OpenFileDialog File1Dialog_ = null;
        protected OpenFileDialog File2Dialog_ = null;
        protected OpenFileDialog File3Dialog_ = null;

        protected Image Logo_ = Properties.Resources.Unknown;

        #endregion

        #region Property accessors

        public string FormatName { get { return FormatName_; } }
        public string University { get { return University_; } }
        public string CreatedBy { get { return CreatedBy_; } }
        public string LastUpdated { get { return LastUpdated_; } }

        public string File1Description { get { return File1Description_; } }
        public string File2Description { get { return File2Description_; } }
        public string File3Description { get { return File3Description_; } }
        public string FileInstructions { get { return FileInstructions_; } }

        public OpenFileDialog File1Dialog { get { return File1Dialog_; } }
        public OpenFileDialog File2Dialog { get { return File2Dialog_; } }
        public OpenFileDialog File3Dialog { get { return File3Dialog_; } }

        public Image Logo { get { return Logo_; } }

        #endregion

        protected Importer()
        {
            File1Dialog_ = DefaultFileDialog;
            File2Dialog_ = DefaultFileDialog;
            File3Dialog_ = DefaultFileDialog;
        }

        private OpenFileDialog DefaultFileDialog
        {
            get
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.RestoreDirectory = true;
                return dialog;
            }
        }

        #region Importing

        /// <summary>
        /// Takes the user-selected files and builds the stream data for a timetable.
        /// </summary>
        /// <returns>The stream data parsed from the files if it succeeded, null if it failed.</returns>
        protected virtual Timetable Parse()
        {
            throw new Exception("Parse method not implemented!");
        }

        public Timetable Import()
        {
            // try and parse files
            Timetable t = Parse();
            // if parsing failed
            if (t == null || !t.HasData())
            {
                return null;
            }
            // do colours
            SetColors(t);

            return t;
        }

        private void SetColors(Timetable timetable)
        {
            ColorScheme scheme = ColorScheme.Schemes[0];
            for (int i = 0; i < timetable.SubjectList.Count; i++)
            {
                timetable.SubjectList[i].Color = scheme.Colors[i % scheme.Colors.Count];
                /*switch (i % 6)
                {
                    case 0:
                        timetable.SubjectList[i].Color = Color.Red;
                        break;
                    case 1:
                        timetable.SubjectList[i].Color = Color.Blue;
                        break;
                    case 2:
                        timetable.SubjectList[i].Color = Color.Green;
                        break;
                    case 3:
                        timetable.SubjectList[i].Color = Color.Yellow;
                        break;
                    case 4:
                        timetable.SubjectList[i].Color = Color.Purple;
                        break;
                    case 5:
                        timetable.SubjectList[i].Color = Color.Orange;
                        break;
                    default:
                        timetable.SubjectList[i].Color = Color.White;
                        break;
                }*/
            }
        }

        #endregion

        #region Overloaded sealed base members

        public sealed override string ToString()
        {
            return FormatName_;
        }

        public sealed override bool Equals(object obj)
        {
            return Importer.ReferenceEquals(this, obj);
        }

        public sealed override int GetHashCode()
        {
            return (FormatName_.Length + University_.Length + CreatedBy_.Length + LastUpdated_.Length) % 16;
        }

        #endregion
    }

    class UQSiNetImporter : Importer
    {
        public UQSiNetImporter()
            : base()
        {
            FormatName_ = "University of Queensland SI-net XLS File";
            University_ = "University of Queensland";
            CreatedBy_ = "Jack Valmadre (updated by Jeremy Herbert)";
            LastUpdated_ = "June 2010";

            File1Description_ = "SI-net XLS File (*.xls)";
            File1Dialog_.Title = "Import SI-net XLS File";
            File1Dialog_.Filter = "SI-net XLS File (*.xls)|*.xls";

            Logo_ = Properties.Resources.UQ;

            //FileInstructions_ = "";
        }

        protected override Timetable Parse()
        {
            Timetable timetable = new Timetable();

            // get file name
            string fileName = File1Dialog_.FileName;
            // if it doesn't end with .xls
            if (!(fileName.ToLower().EndsWith(".xls")))
            {
                // pop up an error
                MessageBox.Show("Please select a file with extension .xls.", "File Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // and return null - failed
                return null;
            }

            // create an object to read the input file
            StreamReader streamReader = new StreamReader(fileName);
            string line;
            int pos, len;
            int rowLine = 0;

            int day = -1;
            string typeName = "";

            Subject subject = null;
            Type type = null;
            Stream stream = null;
            Session session = null;

            // keep reading lines to the end of the file
            while ((line = streamReader.ReadLine()) != null)
            {
                string data = null;

                // updated to ignore html added by SINet
                if (line.StartsWith("<html") || line.StartsWith("<!--") || line.StartsWith("<meta") || line.StartsWith("<td>&nbsp;"))
                {
                    // don't care about this because it is useless (thanks SINet crew!)
                    continue;
                }

                if ((line == "<td></td>") && (day == -1))
                {
                    continue;
                }
                else if (line == "<td></td>")
                {
                    rowLine++;
                    continue;
                }

                // if we've found the start of a day
                if (line.Contains("day"))
                {
                    // look for the ';' before the start of the name of the day
                    // from 7 characters before the location of "day"
                    // (WEDNESday = 6 chars max => 7 to include ';')
                    pos = line.IndexOf(";", line.IndexOf("day") - 7) + 1;
                    // just get the first 2 characters of the day name
                    len = 2;
                    data = line.Substring(pos+4, len);
                    // find the zero-indexed day number
                    switch (data)
                    {
                        case "Su":
                            day = 0;
                            break;
                        case "Mo":
                            day = 1;
                            break;
                        case "Tu":
                            day = 2;
                            break;
                        case "We":
                            day = 3;
                            break;
                        case "Th":
                            day = 4;
                            break;
                        case "Fr":
                            day = 5;
                            break;
                        case "Sa":
                            day = 6;
                            break;
                        default:
                            day = -1;
                            break;
                    }
                    // clear the current subject - starting a new day
                    subject = null;
                    // go to the next line
                    continue;
                }

                // if we haven't identified the current day, skip
                if (day == -1)
                    continue;

                // the start of a row
                if (line.Contains("<tr"))
                {
                    rowLine = 0;
                    continue;
                }

                // increase the number of lines seen in the current row
                rowLine++;

                // if the line contains the start of a cell and not the end, parse until we've found the end
                if (line.Contains("<td") && !line.Contains("</td>"))
                {
                    string nextLine;
                    while (!(nextLine = streamReader.ReadLine()).Contains("</td>"))
                        line += " " + nextLine;
                    line += " " + nextLine;
                }

                // not a line we're interested in, skip
                if (!(rowLine == 1 || (rowLine >= 3 && rowLine <= 6) || rowLine == 9 || rowLine == 10))
                    continue;

                // get rid of the <td> and </td>
                data = line.Replace("<td>", "").Replace("</td>", "");

                // data contains subject name
                if (rowLine == 1)
                {
                    // add new class
                    session = new Session();
                    session.Day = day;

                    if (line != "</td>") // hack, thanks again UQ team
                    {
                        timetable.ClassList.Add(session);
                    }

                    // add subject data to class
                    subject = null;
                    // sequential search for subject by name
                    for (int i = 0; i < timetable.SubjectList.Count; i++)
                    {
                        if (timetable.SubjectList[i].Name == data)
                        {
                            subject = timetable.SubjectList[i];
                            break;
                        }
                    }
                    // couldn't find subject - create a new one with the name and add to list
                    if ((subject == null) && (line != "</td>")) // more edge cases, damn you UQ
                    {
                        subject = new Subject(data);
                        timetable.SubjectList.Add(subject);
                    }
                }

                // line contains a value
                if (line.Contains("=&quot;"))
                {
                    // start of value: "=&quot;".Length => 7;
                    pos = line.IndexOf("=&quot;") + 7;
                    len = line.IndexOf("&quot;", pos) - pos;
                    data = line.Substring(pos, len);
                }
                // backwards compatility - type name didn't used to have ="    " around it
                else if (rowLine == 2)
                {
                    // take data to be between tags
                    pos = line.IndexOf('>', line.IndexOf("<td")) + 1;
                    len = line.IndexOf('<', pos) - pos;
                    data = line.Substring(pos, len);
                }
                
                // data contains type name
                if (rowLine == 3)
                {
                    typeName = data;
                }
                // data contains type letter and stream number
                else if (rowLine == 4)
                {
                    type = null;
                    // do sequential search for type letter
                    foreach (Type x in subject.Types)
                    {
                        if (x.Code == data.Substring(0, 1))
                        {
                            type = x;
                            break;
                        }
                    }
                    // didn't find type - create new
                    if (type == null)
                    {
                        type = new Type(typeName, data.Substring(0, 1), subject);
                        // automatically ignore Contacts, Workshops and Seminars
                        type.Required = (data[0] != 'C' && data[0] != 'W' && data[0] != 'S');
                        // add new type to list
                        timetable.TypeList.Add(type);
                    }

                    // get stream number from data
                    int number;
                    if (data.Length == 1)
                    {
                        number = 0;
                    }
                    else
                    {
                        Match match = Regex.Match(data, @"\d+");
                        number = Convert.ToInt32(match.Value);
                    }
                    // do sequential search for stream with the right number
                    stream = null;
                    foreach (Stream x in type.Streams)
                    {
                        if (x.Number == number)
                        {
                            stream = x;
                            break;
                        }
                    }
                    // didn't find stream - create new
                    if (stream == null)
                    {
                        stream = new Stream(number);
                        // add to the list
                        timetable.StreamList.Add(stream);
                    }

                    // link subject and type
                    if (!subject.Types.Contains(type))
                    {
                        subject.Types.Add(type);
                        type.Subject = subject;
                    }
                    // link type and stream
                    if (!type.Streams.Contains(stream))
                    {
                        type.Streams.Add(stream);
                        stream.Type = type;
                    }
                    // link stream and class
                    stream.Classes.Add(session);
                    session.Stream = stream;
                }
                // data contains start time
                else if (rowLine == 5)
                {
                    session.StartHour = Convert.ToInt32(data.Substring(0, data.IndexOf(':')));
                    session.StartMinute = Convert.ToInt32(data.Substring(data.IndexOf(':') + 1, 2));
                    if (data.ToLower().Contains("p") && session.StartHour != 12)
                        session.StartHour += 12;
                }
                // data contains end time
                else if (rowLine == 6)
                {
                    session.EndHour = Convert.ToInt32(data.Substring(0, data.IndexOf(':')));
                    session.EndMinute = Convert.ToInt32(data.Substring(data.IndexOf(':') + 1, 2));
                    if (data.ToLower().Contains("p") && session.EndHour != 12)
                        session.EndHour += 12;
                    if (session.EndMinute >= 50)
                    {
                        session.EndHour++;
                        session.EndMinute = 0;
                    }
                }
                // data contains building
                else if (rowLine == 9)
                {
                    // don't use just special character such as _ or ?
                    if (data.Trim().Length == 0)
                        data = "";
                    session.Location = data;
                }
                // data contains room
                else if (rowLine == 10)
                {
                    if (data.Trim().Length == 0)
                        data = "";
                    session.Location += "-" + data;
                }
            }
            streamReader.Close();

            return timetable;
        }
    }

    class UNSWHtmlImporter : Importer
    {
        public UNSWHtmlImporter()
            : base()
        {
            FormatName_ = "University of NSW HTML File";
            University_ = "University of New South Wales";
            CreatedBy_ = "Jack Valmadre";
            LastUpdated_ = "16/2/08";

            File1Description_ = "UNSW HTML File (*.htm, *.html)";
            File1Dialog_.Title = "Import UNSW HTML File";
            File1Dialog_.Filter = "UNSW HTML File (*.htm, *.html)|*.htm;*.html";

            Logo_ = Properties.Resources.UNSW;

            //FileInstructions_ = "";
        }

        protected override Timetable Parse()
        {
            Timetable timetable = new Timetable();

            // get file name
            string fileName = File1Dialog_.FileName;
            // if it doesn't end with .xls
            string fileNameLower = fileName.ToLower();
            if (!(fileNameLower.EndsWith(".htm") || fileNameLower.EndsWith(".html")))
            {
                // pop up an error
                MessageBox.Show("Please select a HTML file.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // and return null - failed
                return null;
            }

            ParseOptional(timetable, fileName);

            return timetable;
        }

        private void ParseOptional(Timetable timetable, string fileName)
        {
            StreamReader inputStream = new StreamReader(fileName);
            string line, data;
            int pos, len;
            int rowLine = 0;

            Subject subject = null;
            Type type = null;
            Stream stream = null;
            List<Session> sessions = new List<Session>();

            bool newStream = true;
            int streamNumber = 0;

            // skip the rubbish
            while ((line = inputStream.ReadLine()) != null && !line.Contains("<form")) ;

            while ((line = inputStream.ReadLine()) != null && !line.Contains("</form"))
            {
                if (line.Contains("sectionSubHeading"))
                {
                    // extract the subject name
                    pos = line.IndexOf(">") + 1;
                    len = line.IndexOf("&nbsp;", pos) - pos;
                    data = line.Substring(pos, len);
                    // create subject and add to list
                    subject = new Subject(data);
                    timetable.SubjectList.Add(subject);
                }
                else if (line.Contains("paragraphHeading"))
                {
                    // extract the stream type
                    pos = line.IndexOf(">") + 1;
                    len = line.IndexOf("<", pos) - pos;
                    data = line.Substring(pos, len);
                    string dataUpper = data.ToUpper();
                    char typeCode = '?';
                    // search through all other types in the current subject to see
                    // if the stream type letter is already in use
                    bool foundUnique = false;
                    for (int i = 0; i < data.Length && !foundUnique; i++)
                    {
                        foundUnique = true;
                        for (int j = 0; j < subject.Types.Count && foundUnique; j++)
                        {
                            if (subject.Types[j].Code[0] == dataUpper[i])
                            {
                                foundUnique = false;
                            }
                        }
                        if (foundUnique)
                            typeCode = dataUpper[i];
                    }
                    // add new type
                    type = new Type(typeCode.ToString(), data, subject);
                    type.Required = true;
                    timetable.TypeList.Add(type);
                    // update parent subject
                    subject.Types.Add(type);
                    // reset stream number counter
                    streamNumber = 0;
                }
                else if (line.Contains("<tr"))
                {
                    rowLine = 0;
                }
                else if (line.Contains("</table>"))
                {
                    newStream = true;
                }
                else if (line.Contains("bsktData"))
                {
                    if (rowLine == 0)
                    {
                        // get the days
                        sessions.Clear();
                        len = 2;
                        for (pos = line.IndexOf(">") + 1; line[pos] != '<'; pos += 5)
                        {
                            int day;
                            data = line.Substring(pos, len);
                            switch (data)
                            {
                                case "Su":
                                    day = 0;
                                    break;
                                case "Mo":
                                    day = 1;
                                    break;
                                case "Tu":
                                    day = 2;
                                    break;
                                case "We":
                                    day = 3;
                                    break;
                                case "Th":
                                    day = 4;
                                    break;
                                case "Fr":
                                    day = 5;
                                    break;
                                case "Sa":
                                    day = 6;
                                    break;
                                default:
                                    day = -1;
                                    break;
                            }
                            // add new class
                            Session session = new Session();
                            session.Day = day;
                            timetable.ClassList.Add(session);
                            sessions.Add(session);
                        }

                        // create a new stream if necessary
                        if (newStream)
                        {
                            newStream = false;
                            // create the stream
                            stream = new Stream(++streamNumber, type);
                            timetable.StreamList.Add(stream);
                            // update the parent type
                            type.Streams.Add(stream);
                        }
                        
                        foreach (Session session in sessions)
                        {
                            // set parent stream
                            session.Stream = stream;
                            // update the parent stream
                            stream.Classes.Add(session);
                        }
                    }

                    else if (rowLine == 1)
                    {
                        // get the start time
                        pos = line.IndexOf(">") + 1;
                        len = line.IndexOf(":", pos) - pos;
                        data = line.Substring(pos, len);
                        foreach (Session session in sessions)
                            session.StartHour = Convert.ToInt32(data);
                        // now get the minutes
                        pos += len + 1;
                        len = 2;
                        data = line.Substring(pos, len);
                        foreach (Session session in sessions)
                            session.StartMinute = Convert.ToInt32(data);
                        // now check am/pm
                        pos += 6 + 2;
                        len = 1;
                        data = line.Substring(pos, len);
                        if (data.ToLower() == "p" && sessions[0].StartHour != 12)
                        {
                            foreach (Session session in sessions)
                                session.StartHour += 12;
                        }
                    }

                    else if (rowLine == 2)
                    {
                        // get the end time
                        pos = line.IndexOf(">") + 1;
                        len = line.IndexOf(":", pos) - pos;
                        data = line.Substring(pos, len);
                        foreach (Session session in sessions)
                            session.EndHour = Convert.ToInt32(data);
                        // now get the minutes
                        pos += len + 1;
                        len = 2;
                        data = line.Substring(pos, len);
                        foreach (Session session in sessions)
                            session.EndMinute = Convert.ToInt32(data);
                        // now check am/pm
                        pos += 6 + 2;
                        len = 1;
                        data = line.Substring(pos, len);
                        if (data.ToLower() == "p" && sessions[0].EndHour != 12)
                        {
                            foreach (Session session in sessions)
                                session.EndHour += 12;
                        }
                    }
                    rowLine++;
                }
            }
            inputStream.Close();
        }
    }

    class QUTImporter : Importer
    {
        public QUTImporter()
            : base()
        {
            FormatName_ = "QUT Data";
            University_ = "Queensland University of Technology";
            CreatedBy_ = "Jack Valmadre";
            LastUpdated_ = "COMING SOON";

            File1Description_ = "QUT Data File (*.*)";
            File1Dialog_.Title = "Import QUT Timetable File";
            File1Dialog_.Filter = "QUT Data File (*.*)|*.*";

            Logo_ = Properties.Resources.QUT;

            //FileInstructions_ = "";
        }

        protected override Timetable Parse()
        {
            return new Timetable();
        }
    }
}
