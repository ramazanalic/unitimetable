using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Net;

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
        protected string File4Description_ = null;
        protected string FileInstructions_ = null;

        protected OpenFileDialog File1Dialog_ = null;
        protected OpenFileDialog File2Dialog_ = null;
        protected OpenFileDialog File3Dialog_ = null;
        protected OpenFileDialog File4Dialog_ = null;

        protected Image Logo_ = Properties.Resources.Unknown;

        protected bool SupportsWeb_ = false;
        protected string Unit1Description_ = null;
        protected string Unit1URL_ = null;
        protected string Unit2Description_ = null;
        protected string Unit2URL_ = null;
        protected string Unit3Description_ = null;
        protected string Unit3URL_ = null;
        protected string Unit4Description_ = null;
        protected string Unit4URL_ = null;

        #endregion

        #region Property accessors

        public string FormatName { get { return FormatName_; } }
        public string University { get { return University_; } }
        public string CreatedBy { get { return CreatedBy_; } }
        public string LastUpdated { get { return LastUpdated_; } }

        public string File1Description { get { return File1Description_; } }
        public string File2Description { get { return File2Description_; } }
        public string File3Description { get { return File3Description_; } }
        public string File4Description { get { return File4Description_; } }
        public string FileInstructions { get { return FileInstructions_; } }

        public OpenFileDialog File1Dialog { get { return File1Dialog_; } }
        public OpenFileDialog File2Dialog { get { return File2Dialog_; } }
        public OpenFileDialog File3Dialog { get { return File3Dialog_; } }
        public OpenFileDialog File4Dialog { get { return File4Dialog_; } }

        public Image Logo { get { return Logo_; } }

        public bool SupportsWeb { get { return SupportsWeb_; } }
        public string Unit1Description { get { return Unit1Description_; } set { Unit1Description_ = value; } }
        public string Unit1URL { get { return Unit1URL_; } set { Unit1URL_ = value; } }
        public string Unit2Description { get { return Unit2Description_; } set { Unit2Description_ = value; } }
        public string Unit2URL { get { return Unit2URL_; } set { Unit2URL_ = value; } }
        public string Unit3Description { get { return Unit3Description_; } set { Unit3Description_ = value; } }
        public string Unit3URL { get { return Unit3URL_; } set { Unit3URL_ = value; } }
        public string Unit4Description { get { return Unit4Description_; } set { Unit4Description_ = value; } }
        public string Unit4URL { get { return Unit4URL_; } set { Unit4URL_ = value; } }

        #endregion

        protected Importer()
        {
            File1Dialog_ = DefaultFileDialog;
            File2Dialog_ = DefaultFileDialog;
            File3Dialog_ = DefaultFileDialog;
            File4Dialog_ = DefaultFileDialog;
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

        public virtual List<TimetableSession> FetchUnits(string unitCode, TextBox textUnitName)
        {
            throw new Exception("FetchUnits method not implemented!");
        }

        public virtual bool PraseTest(string fileName)
        {
            throw new Exception("ParseTest method not implemented!");
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

            // Get file name.
            string fileName = File1Dialog_.FileName;
            if (!fileName.ToLower().EndsWith(".xls"))
            {
                // File doesn't end with .xls!
                MessageBox.Show("Please select a file with extension .xls.",
                                "File Type",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return null;
            }
            // Dump the input file into a string.
            StreamReader streamReader = new StreamReader(fileName);
            string timetable_html = streamReader.ReadToEnd();
            streamReader.Close();

            // Remove useless information.
            // Anything in the following list of regexes will be removed.
            string[] replacements = {
                @"\n",                      // we get rid of newlines so we don't have to bother with multiline regex
                @"<html.*?>",               // opening html
                @"<!--.*?-->",              // html comments
                @"=&quot;", @"&quot;",      // html-escaped quotes
                @"&nbsp;",                  // html-escaped spaces (these are useless anyway as the file contains normal spaces where needed)
                @"<meta.*?>",               // meta tags
                @"<tr><th>.*?</th></tr>",   // table headers
                @"</tr>",                   // remove close row tags (this will make sense for the cleanups)
                @"<table.*?>",              // table opening tag
                @"<body.*?>",               // body open tag
                @"</body></html>"           // end page tags
            };
            // Run the replacements.
            foreach (string replace_regex in replacements) 
            {
                timetable_html = Regex.Replace(timetable_html, replace_regex, "");
            }

            // Build a dictionary of regex cleanups.
            // Probably should use a pair, but this requires less code and is probably more efficient
            Dictionary<string, string> cleanups = new Dictionary<string,string>();
            // Put the ampersands back in.
            cleanups.Add(@"&amp;", "&");
            // Assume that each <tr> actually means </tr><tr> in valid html.
            // Unfortunately this results in a </tr> at the start of the file.
            cleanups.Add(@"<tr>", "</tr><tr>");
            // Assume that the </table> tag actually represents a </tr>.
            cleanups.Add(@"</table>", "</tr>");
            // Run the cleanups
            foreach (string cleanup_regex in cleanups.Keys) 
            {
                timetable_html = Regex.Replace(timetable_html,
                                               cleanup_regex,
                                               cleanups[cleanup_regex]);
            }
            // Get rid of the extra </tr> at the start of the file.
            Regex pattern = new Regex(@"</tr>");
            timetable_html = pattern.Replace(timetable_html, "", 1);

            // Build the regex to parse the file.
            Regex timetable_parser = new Regex(
                @"<tr>" +                                   // start row
                @"<td>(?<subject_code>.*?)</td>" +          // subject code (eg "BIOM2012")
                @"<td>(?<subject_desc>.*?)</td>" +          // subject description (eg "Machine Learning")
                @"<td>(?<session_type_name>.*?)</td>" +     // session time in long form (eg "Tutorial", "Lecture")
                @"<td>(?<stream_code>.*?)</td>" +           // stream (eg "T7", "L2", "P")
                @"<td>(?<start_time>.*?)</td>" +            // session start time (eg "11:00 AM")
                @"<td>(?<stop_time>.*?)</td>" +             // session stop time (eg "11:50 AM")
                @"<td>(?<clash>.*?)</td>" +                 // has a clash occured (pretty useless for our purposes)
                @"<td>(?<building_name>.*?)</td>" +         // full building name (eg "Forgan Smith Building")
                @"<td>(?<building_number>.*?)</td>" +       // building number (eg "01")
                @"<td>(?<room>.*?)</td>" +                  // room code (eg "205", "E105"
                @"<td>(?<running_dates>.*?)</td>" +         // time period over which the stream runs
                @"<td>(?<not_taught_on>.*?)</td>" +         // date the stream is not taught on
                @"<td>.*?</td>" +                           // unknown
                @"<td>.*?</td>" +                           // unknown
                @"<td>(?<session_type_name2>.*?)</td>" +    // not sure why this is here ?
                @"</tr>"                                    // close row
            );

            // Which day are the current sessions on?
            int current_day = -1;

            foreach (Match match in timetable_parser.Matches(timetable_html))
            {
                GroupCollection session_info = match.Groups;
                string subject_code = session_info["subject_code"].Value;
                if (subject_code.Contains("day"))
                {
                    // If we are talking about a day here.
                    switch (subject_code.Substring(0, 2))
                    {
                        case "Su":
                            current_day = 0;
                            break;
                        case "Mo":
                            current_day = 1;
                            break;
                        case "Tu":
                            current_day = 2;
                            break;
                        case "We":
                            current_day = 3;
                            break;
                        case "Th":
                            current_day = 4;
                            break;
                        case "Fr":
                            current_day = 5;
                            break;
                        case "Sa":
                            current_day = 6;
                            break;
                        default:
                            current_day = -1;
                            break;
                    }
                    continue;
                }
                // If we have a day selected already.
                if (current_day != -1)
                {
                    // Build a session object.
                    Session session = new Session();
                    session.Day = current_day;
                    // Do a sequential search for the subject.
                    Subject subject = null;
                    for (int i = 0; i < timetable.SubjectList.Count; i++)
                    {
                        if (timetable.SubjectList[i].Name == subject_code)
                        {
                            subject = timetable.SubjectList[i];
                            break;
                        }
                    }
                    // If it doesn't exist, create it.
                    if (subject == null)
                    {
                        subject = new Subject(subject_code);
                        timetable.SubjectList.Add(subject);
                    }

                    // Get a session type.
                    Type type = null;
                    // Check if the session type exists.
                    string session_code = session_info["stream_code"].Value;
                    string stream_code = session_code.Substring(0, 1);
                    foreach (Type x in subject.Types)
                    {
                        if (x.Code == stream_code)
                        {
                            // Matched on the first letter.
                            type = x;
                            break;
                        }
                    }
                    if (type == null)
                    {
                        // The session type doesn't exist, create it.
                        type = new Type(session_info["session_type_name"].Value,
                                        stream_code,
                                        subject);
                        // Check if this session requires attendance.
                        type.Required = (stream_code != "W" &&
                            stream_code != "S" &&
                            stream_code != "C");
                        timetable.TypeList.Add(type);
                    }

                    // Get a stream object.
                    // Grab the stream number.
                    int stream_number = 0;
                    if (session_code.Length != 1)
                    {
                        // Chop the letter off the stream code.
                        stream_number = Convert.ToInt32(session_code.Substring(1));
                    }
                    // Search to see if the stream exists.
                    Stream stream = null;
                    foreach (Stream x in type.Streams)
                    {
                        if (x.Number == stream_number)
                        {
                            stream = x;
                            break;
                        }
                    }
                    // Otherwise build a new one.
                    if (stream == null)
                    {
                        stream = new Stream(stream_number);
                        // Tack it on to the stream list.
                        timetable.StreamList.Add(stream);
                    }

                    // Link the subject and type.
                    if (!subject.Types.Contains(type)) 
                    {
                        subject.Types.Add(type);
                        type.Subject = subject;
                    }
                    // Link the stream and type.
                    if (!type.Streams.Contains(stream))
                    {
                        type.Streams.Add(stream);
                        stream.Type = type;
                    }
                    // Link the stream and class together.
                    // Add it to our list of classes.
                    timetable.ClassList.Add(session);
                    stream.Classes.Add(session);
                    session.Stream = stream;

                    /****************************************************************************
                     * Calculate session start and stop times
                     ****************************************************************************/

                    string start_time = session_info["start_time"].Value;
                    int colon_index = start_time.IndexOf(":");

                    session.StartHour = Convert.ToInt32(start_time.Substring(0, colon_index)); // grab everything before the colon
                    session.StartMinute = Convert.ToInt32(start_time.Substring(colon_index + 1, 2)); // grab two characters after the colon
                    if (start_time.ToLower().Contains("p") && session.StartHour != 12) // if there is a PM in the time
                    {
                        session.StartHour += 12; // correct for it
                    }

                    string stop_time = session_info["stop_time"].Value;
                    colon_index = stop_time.IndexOf(":");
                    session.EndHour = Convert.ToInt32(stop_time.Substring(0, colon_index)); // grab everything before the colon
                    session.EndMinute = Convert.ToInt32(stop_time.Substring(colon_index + 1, 2)); // grab two characters after the colon
                    if (stop_time.ToLower().Contains("p") && session.EndHour != 12) // if there is a PM in the time
                    {
                        session.EndHour += 12; // correct for it
                    }
                    if (session.EndMinute >= 50)
                    {
                        session.EndHour++;
                        session.EndMinute = 0;
                    }

                    // Insert building location
                    if (session_info["building_number"].Value.Trim() == "")
                    {
                        session.Location = "";
                    }
                    else
                    {
                        session.Location = session_info["building_number"].Value;
                    }
                    if (session_info["room"].Value.Trim() != "")
                    {
                        session.Location += " - " + session_info["room"];
                    }
                }
            }

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

    class UWSHTMLImporter : Importer
    {
        public UWSHTMLImporter()
            : base()
        {
            FormatName_ = "University of Western Sydney HTML File";
            University_ = "University of Western Sydney";
            CreatedBy_ = "Joshua Henderson";
            LastUpdated_ = "November 2010";
            SupportsWeb_ = true;

            /* Clarification: There is a lot of repeated code here.
             * I would load poitners to each type in an array (for example, File1Description, File2Description)
             * and use a loop looping through the array to assign all the strings to everything, but unfortuantly
             * 1. C# doesn't support pointers to the extent C++ does :(     (can't access pointers of strings),
             * 2. I'd need to declare as public unsafe, which doesn't sound safe :P
             * so I figured I'd just leave the repeated code in as it is the "safest" way of getting things done.*/

            File1Description_ = "Subject 1 (*.htm, *.html)";
            File1Dialog_.Title = "Import UWS HTML File";
            File1Dialog_.Filter = "UWS HTML File (*.htm, *.html)|*.htm;*.html";

            File2Description_ = "Subject 2 (*.htm, *.html)";
            File2Dialog_.Title = "Import UWS HTML File";
            File2Dialog_.Filter = "UWS HTML File (*.htm, *.html)|*.htm;*.html";

            File3Description_ = "Subject 3 (*.htm, *.html)";
            File3Dialog_.Title = "Import UWS HTML File";
            File3Dialog_.Filter = "UWS HTML File (*.htm, *.html)|*.htm;*.html";

            File4Description_ = "Subject 4 (*.htm, *.html)";
            File4Dialog_.Title = "Import UWS HTML File";
            File4Dialog_.Filter = "UWS HTML File (*.htm, *.html)|*.htm;*.html";

            Logo_ = Properties.Resources.UWS;
            FileInstructions_  = "Download the Subject timetable page as a HTML file." +
                                 "Do this from your browser. Select the downloaded file" +
                                 "using the browse function. You can add up to 4 subjects. " +
                                 "If you have less than 4 subjects, simply don't select a file.";
        }

        protected override Timetable Parse()
        {
            Timetable timetable = new Timetable();

            // Process first subject. This is compulsory

            // Determine whether or not to do web based or local based.

            if (Unit1URL_ != null)
            {
                ParseOptional(timetable, Unit1URL_);

                if (Unit2URL_ != null)
                    ParseOptional(timetable, Unit2URL_);

                if (Unit3URL_ != null)
                    ParseOptional(timetable, Unit3URL_);

                if (Unit4URL_ != null)
                    ParseOptional(timetable, Unit4URL_);
            }
            else
            {

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

                // Process second subject. This is optional
                fileName = File2Dialog_.FileName;
                if (fileName != "")
                {
                    fileNameLower = fileName.ToLower();
                    if (!(fileNameLower.EndsWith(".htm") || fileNameLower.EndsWith(".html")))
                    {
                        // pop up an error
                        MessageBox.Show("Please choose a proper HTML file for Subject 2.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        // and return null - failed
                        return null;
                    }

                    ParseOptional(timetable, fileName);
                }

                // Process third subject. This is optional
                fileName = File3Dialog_.FileName;
                if (fileName != "")
                {
                    fileNameLower = fileName.ToLower();
                    if (!(fileNameLower.EndsWith(".htm") || fileNameLower.EndsWith(".html")))
                    {
                        // pop up an error
                        MessageBox.Show("Please choose a proper HTML file for Subject 3.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        // and return null - failed
                        return null;
                    }

                    ParseOptional(timetable, fileName);
                }

                // Process fourth subject. This is optional
                fileName = File4Dialog_.FileName;
                if (fileName != "")
                {
                    fileNameLower = fileName.ToLower();
                    if (!(fileNameLower.EndsWith(".htm") || fileNameLower.EndsWith(".html")))
                    {
                        // pop up an error
                        MessageBox.Show("Please choose a proper HTML file for Subject 4.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        // and return null - failed
                        return null;
                    }

                    ParseOptional(timetable, fileName);
                }
            }


            return timetable;
        }

        public override List<TimetableSession> FetchUnits(string unitCode, TextBox textUnitName)
        {
            // Declare all the variables needed later on
            List<TimetableSession> sessions = new List<TimetableSession>();


            // START: GET FROM WEB

            // Open a connection
            string Url = "http://handbook.uws.edu.au/hbook/unit.aspx?unit=" + unitCode;
            HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(Url);

            // You can also specify additional header values like 
            // the user agent or the referer:
            WebRequestObject.UserAgent = ".NET Framework/2.0";
            WebRequestObject.Referer = "http://www.example.com/";

            // Request response:
            WebResponse Response = WebRequestObject.GetResponse();

            // Open data stream:
            System.IO.Stream WebStream = Response.GetResponseStream();

            // Create reader object:
            StreamReader Reader = new StreamReader(WebStream);

            // END: GET FROM WEB


            // Read the entire stream content:
            //string PageContent = Reader.ReadToEnd();
            string line;

            Regex pattern;
            string subject;
            string timeframe;
            string offered;
            string campus;
            string type;
            string url;


            while ((line = Reader.ReadLine()) != null && !line.Contains("Teaching Periods")) ;

            pattern = new Regex("</span><p><span class=\"LBN\">(.*?)</span><span class=\"HBN\">");
            subject = pattern.Match(line).Groups[1].Value;

            textUnitName.Text = subject;

            // Get first subject. This is a special case
            pattern = new Regex("</th></tr><tr><td class=\"NNN\">(.*?)"
                + "</td><td class=\"NNN\">(.*?)"
                + "</td><td class=\"NNN\">(.*?)"
                + "</td><td class=\"NNN\">(.*?)"
                + "</td><td class=\"NNN\"><a href=\"(.*?)"
                + "\" target=\"&#xD;&#xA;         _blank&#xD;&#xA;         \">");

            timeframe = pattern.Match(line).Groups[1].Value;
            offered = pattern.Match(line).Groups[2].Value;
            campus = pattern.Match(line).Groups[3].Value;
            type = pattern.Match(line).Groups[4].Value;
            url = pattern.Match(line).Groups[5].Value;

            if (campus == "")
                throw new Exception("Unit Not On Offer");

            // Fix URL
            url = Regex.Replace(url, "&#xD;&#xA;| |amp;", "");

            sessions.Add(new TimetableSession(timeframe, offered, campus, type, url));

            while ((line = Reader.ReadLine()) != null)
            {
                if (line.Contains("Timetable"))
                    continue;
                if (line.Contains("</a></td></tr></table></p><hr /><p>"))
                    break;

                pattern = new Regex("</a></td></tr><tr><td class=\"NNN\">(.*?)"
                + "</td><td class=\"NNN\">(.*?)"
                + "</td><td class=\"NNN\">(.*?)"
                + "</td><td class=\"NNN\">(.*?)"
                + "</td><td class=\"NNN\"><a href=\"(.*?)"
                + "\" target=\"&#xD;&#xA;         _blank&#xD;&#xA;         \">");

                timeframe = pattern.Match(line).Groups[1].Value;
                offered = pattern.Match(line).Groups[2].Value;
                campus = pattern.Match(line).Groups[3].Value;
                type = pattern.Match(line).Groups[4].Value;
                url = pattern.Match(line).Groups[5].Value;

                // Fix URL
                url = Regex.Replace(url, "&#xD;&#xA;| |amp;", "");

                sessions.Add(new TimetableSession(timeframe, offered, campus, type, url));
            }

            // Cleanup
            Reader.Close();
            WebStream.Close();
            Response.Close();

            return sessions;
        }

        public override bool PraseTest( string fileName )
        {
            try
            {
                Timetable tempTimetable = new Timetable();
                ParseOptional(tempTimetable, fileName);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private void ParseOptional(Timetable timetable, string fileName)
        {
            HttpWebRequest WebRequest;
            WebResponse WebResponse;
            System.IO.Stream WebStream;

            StreamReader inputStream;

            if (fileName.StartsWith("http"))
            {
                WebRequest = (HttpWebRequest)HttpWebRequest.Create(fileName);
                WebRequest.UserAgent = ".NET Framework/2.0";
                WebRequest.Referer = "http://www.uws.edu.au/";
                WebResponse = WebRequest.GetResponse();
                WebStream = WebResponse.GetResponseStream();
                inputStream = new StreamReader(WebStream);
            }
            else
            {
                inputStream = new StreamReader(fileName);
            }
            string line;
            int rowLine = 0;
            int streamNumber = 0;
            bool current = false;

            bool used = false;

            Regex pattern;

            string subjectName = "";
            string typeName = "";
            string day = "";

            string start = "";
            string end = "";
            string location = "";

            Subject subject;
            Type type;
            Session session;
            Stream stream;

            bool newType = false;

            // Extract subject name first
            while ((line = inputStream.ReadLine()) != null && !line.Contains("<hr>")) ;
            while ((line = inputStream.ReadLine()) != null && !line.Contains("<b>")) ;
            while ((line = inputStream.ReadLine()) != null && !line.Contains("<b>")) ;

            pattern = new Regex("<b>(.*?)</b>");
            subjectName = pattern.Match(line).Groups[1].Value;

            Console.WriteLine("Subject: " + subjectName + "\n\n");

            // Create subject here
            subject = new Subject(subjectName);
            timetable.SubjectList.Add(subject);

            // Create a dummy type
            type = new Type("", "", subject);


            // Skip over all the junk that we don't need. Skip straight to <table
            while ((line = inputStream.ReadLine()) != null && !line.Contains("<table")) ;

            // Skip over the first <tr>
            while ((line = inputStream.ReadLine()) != null && !line.Contains("</tr>")) ;

            // Gather the useful information
            while ((line = inputStream.ReadLine()) != null)
            {
                if (line.Contains("<tr>"))
                {
                    used = false;
                    current = true;
                    rowLine = 0;
                }

                if (line.Contains("</tr>"))
                {
                    if (typeName != "")
                    {
                        // Extract type, day, start, end
                        pattern = new Regex("<td>(.*?)(&nbsp;|<\td>)");

                        typeName = pattern.Match(typeName).Groups[1].Value;
                        day = pattern.Match(day).Groups[1].Value;
                        start = pattern.Match(start).Groups[1].Value;
                        end = pattern.Match(end).Groups[1].Value;

                        // Extract location
                        pattern = new Regex("<td width=\"300\">(.*?)(&nbsp;|<\td>)");

                        location = pattern.Match(location).Groups[1].Value;

                        // Create New Type if needed
                        if (newType)
                        {
                            Console.WriteLine("Creating new Type: " + typeName + "\n");
                            streamNumber = 0;

                            // Create type
                            type = new Type(typeName, typeName, subject);
                            type.Required = true;
                            timetable.TypeList.Add(type);

                            // Link the subject and type
                            subject.Types.Add(type);
                            type.Subject = subject;

                            newType = false;
                        }

                        // Parse all the information from the variables
                        //type, day, start, end, location;
                        Console.WriteLine("Type: " + typeName + " " + streamNumber);
                        Console.WriteLine("Day: " + day);
                        Console.WriteLine("Start: " + start);
                        Console.WriteLine("End: " + end);
                        Console.WriteLine("Location: " + location + "\n");

                        // Create new session
                        session = new Session();

                        // Add the day to the session
                        switch (day)
                        {
                            case "Sun":
                                session.Day = 0;
                                break;
                            case "Mon":
                                session.Day = 1;
                                break;
                            case "Tue":
                                session.Day = 2;
                                break;
                            case "Wed":
                                session.Day = 3;
                                break;
                            case "Thu":
                                session.Day = 4;
                                break;
                            case "Fri":
                                session.Day = 5;
                                break;
                            case "Sat":
                                session.Day = 6;
                                break;
                            default:
                                session.Day = -1;
                                break;
                        }

                        // Start
                        pattern = new Regex("(.*)(:)(.*)");
                        session.StartHour = Convert.ToInt32(pattern.Match(start).Groups[1].Value);
                        session.StartMinute = Convert.ToInt32(pattern.Match(start).Groups[3].Value);

                        // End
                        pattern = new Regex("(.*)(:)(.*)");
                        session.EndHour = Convert.ToInt32(pattern.Match(end).Groups[1].Value);
                        session.EndMinute = Convert.ToInt32(pattern.Match(end).Groups[3].Value);
                        
                        // Create new stream
                        streamNumber++;
                        stream = new Stream(streamNumber);
                        timetable.StreamList.Add(stream);

                        // Link the stream and type
                        type.Streams.Add(stream);
                        stream.Type = type;

                        // Link the session and stream
                        timetable.ClassList.Add(session);
                        stream.Classes.Add(session);
                        session.Stream = stream;
                    }
                    else
                    {
                        newType = true;
                    }

                    used = false;
                    current = false;

                }

                if (current)
                {
                    used = true;
                    switch (rowLine)
                    {
                        case 0:
                            // IGNORE
                            break;
                        case 1:
                            // IGNORE
                            break;
                        case 2:
                            typeName = line;
                            break;
                        case 3:
                            day = line;
                            break;
                        case 4:
                            start = line;
                            break;
                        case 5:
                            end = line;
                            break;
                        case 6:
                            // IGNORE
                            break;
                        case 7:
                            location = line;
                            break;
                    }

                    rowLine++;
                }
            }

            inputStream.Close();
        }
    }

}
