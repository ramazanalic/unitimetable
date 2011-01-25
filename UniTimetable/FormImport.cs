using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniTimetable
{
    partial class FormImport : Form
    {
        Timetable Timetable_;
        int CurrentPage_ = 1;

        Importer[] ImporterList_ = new Importer[] {
            new UQSiNetImporter(),
            new UNSWHtmlImporter(),
        };

        Importer Importer_ = null;

        public FormImport()
        {
            InitializeComponent();

            // bring up all the importer options
            listBoxFormats.Items.Clear();
            foreach (Importer importer in ImporterList_)
            {
                listBoxFormats.Items.Add(importer);
            }

            // Select the first item by default.
            listBoxFormats.SetSelected(0, true);
        }

        public new Timetable ShowDialog()
        {
            if (base.ShowDialog() != DialogResult.OK)
                return null;
            return Timetable_;
        }

        private void FormImport_Load(object sender, EventArgs e)
        {
            CurrentPage_ = 1;
            // bring up panel 1
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            // show the right buttons
            btnBack.Enabled = false;
            btnNext.Visible = true;
            btnFinish.Visible = false;

            // clear textboxes etc
            txtFile1.Text = "";
            txtFile2.Text = "";
            txtFile3.Text = "";
        }

        #region Wizard Control Flow Buttons

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Back();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            /*if (TimetableShadow_.HasClashingTypes())
            {
                MessageBox.Show("Please remove clashing streams until none remain.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }*/
            Timetable_.Update();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Next()
        {
            if (CurrentPage_ == 1)
            {
                // if there's no selected format, skip
                if (listBoxFormats.SelectedItem == null)
                    return;

                // load selected importer
                Importer_ = (Importer)listBoxFormats.SelectedItem;
                // clear importer files
                Importer_.File1Dialog.FileName = "";
                Importer_.File2Dialog.FileName = "";
                Importer_.File3Dialog.FileName = "";

                // bring up panel 2 (file import) information
                // file 1
                if (Importer_.File1Description != null)
                {
                    lblFile1.Text = Importer_.File1Description;
                    lblFile1.Visible = true;
                    btnBrowse1.Visible = true;
                    txtFile1.Visible = true;
                }
                else
                {
                    lblFile1.Visible = false;
                    btnBrowse1.Visible = false;
                    txtFile1.Visible = false;
                }
                // file 2
                if (Importer_.File2Description != null)
                {
                    lblFile2.Text = Importer_.File2Description;
                    lblFile2.Visible = true;
                    btnBrowse2.Visible = true;
                    txtFile2.Visible = true;
                }
                else
                {
                    lblFile2.Visible = false;
                    btnBrowse2.Visible = false;
                    txtFile2.Visible = false;
                }
                // file 3
                if (Importer_.File3Description != null)
                {
                    lblFile3.Text = Importer_.File3Description;
                    lblFile3.Visible = true;
                    btnBrowse3.Visible = true;
                    txtFile3.Visible = true;
                }
                else
                {
                    lblFile3.Visible = false;
                    btnBrowse3.Visible = false;
                    txtFile3.Visible = false;
                }
                // file instructions
                if (Importer_.FileInstructions != null)
                {
                    txtFileInstructions.Text = Importer_.FileInstructions;
                }
                else
                {
                    txtFileInstructions.Text = "No instructions provided for " + Importer_.FormatName + ".";
                }

                // bring up panel 2
                panel1.Visible = false;
                panel2.Visible = true;
                // enable back button
                btnBack.Enabled = true;
            }
            else if (CurrentPage_ == 2)
            {
                // try and parse files
                Timetable_ = Importer_.Import();

                // if it failed, alert the user and stay on the current page
                if (Timetable_ == null)
                {
                    MessageBox.Show("Failed to import timetable data.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // build relational data
                Timetable_.BuildEquivalency();
                Timetable_.BuildCompatibility();
                //Timetable_.UpdateStates();

                // build tree
                Timetable_.BuildTreeView(treePreview);
                // and scroll back to the top
                treePreview.Nodes[0].EnsureVisible();
                // clear details box
                txtTreeDetails.Text = "";
                timetableControl1.Clear();

                // bring up panel 3
                panel2.Visible = false;
                panel3.Visible = true;
            }
            else if (CurrentPage_ == 3)
            {
                // clear ignored/required lists
                listViewIgnored.Items.Clear();
                listViewIgnored.Groups.Clear();
                listViewRequired.Items.Clear();
                listViewRequired.Groups.Clear();

                // populate ignored/required lists
                foreach (Subject subject in Timetable_.SubjectList)
                {
                    // create and add groups for the subjects
                    ListViewGroup ignoredSubjectGroup = new ListViewGroup(subject.Name);
                    ignoredSubjectGroup.Tag = subject;
                    listViewIgnored.Groups.Add(ignoredSubjectGroup);

                    ListViewGroup requiredSubjectGroup = new ListViewGroup(subject.Name);
                    requiredSubjectGroup.Tag = subject;
                    listViewRequired.Groups.Add(requiredSubjectGroup);

                    // add stream types to subject groups
                    foreach (Type type in subject.Types)
                    {
                        // create ListViewItem without group
                        ListViewItem item = new ListViewItem(new string[] { type.Code.ToString(), type.Name });
                        item.Tag = type;

                        // add it to the current group in the correct box
                        if (type.Required)
                        {
                            // set group and add to list
                            item.Group = requiredSubjectGroup;
                            listViewRequired.Items.Add(item);
                        }
                        else
                        {
                            // set group and add to list
                            item.Group = ignoredSubjectGroup;
                            listViewIgnored.Items.Add(item);
                        }
                    }
                }

                UpdateClashHighlight();

                btnRequire.Enabled = false;
                btnIgnore.Enabled = false;

                // bring up panel 4
                panel3.Visible = false;
                panel4.Visible = true;
                // swap next button for finish
                btnNext.Visible = false;
                btnFinish.Visible = true;

                // need to refresh to get red highlighter
                listViewIgnored.Refresh();
                listViewRequired.Refresh();
            }
            CurrentPage_++;
        }

        private void Back()
        {
            if (CurrentPage_ == 2)
            {
                // bring up panel 1
                panel2.Visible = false;
                panel1.Visible = true;
                // disable back button
                btnBack.Enabled = false;
            }
            else if (CurrentPage_ == 3)
            {
                // bring up panel 2
                panel3.Visible = false;
                panel2.Visible = true;
            }
            else if (CurrentPage_ == 4)
            {
                // bring up panel 3
                panel4.Visible = false;
                panel3.Visible = true;
                // swap finish button for next
                btnFinish.Visible = false;
                btnNext.Visible = true;
            }
            CurrentPage_--;
        }

        #endregion

        #region Page 1 Event Handlers

        private void listBoxFormats_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            Graphics g = e.Graphics;
            Importer p = ImporterList_[e.Index];

            Rectangle r = new Rectangle(e.Bounds.Location, new Size(80, 80));
            r.Offset(10, 10);
            if (p.Logo != null)
                g.DrawImage(p.Logo, r);

            r = e.Bounds;
            r.X += 100;
            r.Width -= 100;

            Font font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;
            g.DrawString("\n" + p.University, font, Brushes.Black, r);

            font = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Regular);
            g.DrawString("\n\n\n Created by: " + p.CreatedBy + "\n Last updated: " + p.LastUpdated,
                font, Brushes.Black, r);
        }

        private void listBoxFormats_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxFormats.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
                return;
            Next();
        }

        #endregion

        #region Page 2 Event Handlers

        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            if (Importer_.File1Dialog.ShowDialog() == DialogResult.OK)
            {
                txtFile1.Text = Importer_.File1Dialog.FileName;
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            if (Importer_.File2Dialog.ShowDialog() == DialogResult.OK)
            {
                txtFile2.Text = Importer_.File2Dialog.FileName;
            }
        }

        private void btnBrowse3_Click(object sender, EventArgs e)
        {
            if (Importer_.File3Dialog.ShowDialog() == DialogResult.OK)
            {
                txtFile3.Text = Importer_.File3Dialog.FileName;
            }
        }

        #endregion

        #region Page 3 Event Handlers

        private void treePreview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // clear textbox
            txtTreeDetails.Text = "";
            // if nothing selected, done already
            if (treePreview.SelectedNode == null)
                return;

            // level 0: subject
            if (treePreview.SelectedNode.Level == 0)
            {
                // get subject
                Subject subject = (Subject)treePreview.SelectedNode.Tag;
                // print subject name
                txtTreeDetails.Text += subject.Name + "\r\n";
                // print all the types within the subject
                foreach (Type type in subject.Types)
                {
                    txtTreeDetails.Text += "\r\n\t" + type.Name + " (" + type.Streams.Count + ")";
                }
                // preview pane
                timetableControl1.Timetable = Timetable.From(subject);
            }
            // level 1: type
            else if (treePreview.SelectedNode.Level == 1)
            {
                // get type
                Type type = (Type)treePreview.SelectedNode.Tag;
                // print type name
                txtTreeDetails.Text += type.Subject.Name + " " + type.Name + "\r\n";
                // print all the streams within the type
                foreach (Stream stream in type.Streams)
                {
                    txtTreeDetails.Text += "\r\n\t" + stream.ToString();
                }
                // preview pane
                timetableControl1.Timetable = Timetable.From(type);
            }
            // level 2: stream
            else
            {
                // get stream
                Stream stream = (Stream)treePreview.SelectedNode.Tag;
                // print stream name
                txtTreeDetails.Text += stream.Type.Subject.Name + " " + stream.ToString();
                // print all the classes within the type
                foreach (Session session in stream.Classes)
                {
                    txtTreeDetails.Text += "\r\n\t\r\n\t" + session.ToString();
                }
                // preview pane
                timetableControl1.Timetable = Timetable.From(stream);
            }
        }

        #endregion

        #region Page 4 Event Handlers

        private void listViewRequired_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateIgnoreButton();
        }

        private void listViewIgnored_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRequireButton();
        }

        private void listViewRequired_Enter(object sender, EventArgs e)
        {
            UpdateIgnoreButton();
        }

        private void listViewIgnored_Enter(object sender, EventArgs e)
        {
            UpdateRequireButton();
        }

        private void UpdateRequireButton()
        {
            btnRequire.Enabled = false;
            btnIgnore.Enabled = false;
            if (listViewIgnored.SelectedIndices.Count == 0 || listViewIgnored.SelectedIndices[0] == -1)
                return;
            btnRequire.Enabled = true;
        }

        private void UpdateIgnoreButton()
        {
            btnRequire.Enabled = false;
            btnIgnore.Enabled = false;
            if (listViewRequired.SelectedIndices.Count == 0 || listViewRequired.SelectedIndices[0] == -1)
                return;
            btnIgnore.Enabled = true;
        }

        private void UpdateClashHighlight()
        {
            bool clash = false;
            foreach (ListViewItem item in listViewRequired.Items)
            {
                Type type = (Type)item.Tag;
                if (Timetable_.CheckDirectClash(type))
                {
                    clash = true;
                    item.BackColor = Color.Red;
                }
                else
                {
                    item.BackColor = SystemColors.Window;
                }
            }
            lblClashNotice.Visible = clash;

            listViewIgnored.Invalidate();
            listViewRequired.Invalidate();
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            MoveLeft();
        }

        private void btnRequire_Click(object sender, EventArgs e)
        {
            MoveRight();
        }

        private void listViewRequired_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MoveLeft();
        }

        private void listViewIgnored_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MoveRight();
        }

        private void MoveLeft()
        {
            if (listViewRequired.SelectedItems.Count == 0 || listViewRequired.SelectedItems[0] == null)
                return;

            ListViewItem item = listViewRequired.SelectedItems[0];
            item.BackColor = SystemColors.Window;
            Type type = (Type)item.Tag;
            type.Required = false;
            Timetable_.BuildCompatibility();

            int index = listViewRequired.SelectedIndices[0];
            listViewRequired.Items.RemoveAt(index);

            // look through each subject group in the ignored list
            foreach (ListViewGroup group in listViewIgnored.Groups)
            {
                // if we've found the subject group
                if ((Subject)group.Tag == type.Subject)
                {
                    // set group and add to list
                    item.Group = group;
                    listViewIgnored.Items.Add(item);
                    break;
                }
            }

            // select the next item in the list
            if (index == listViewRequired.Items.Count)
                index--;
            if (index >= 0)
            {
                listViewRequired.Items[index].Selected = true;
                listViewRequired.Select();
            }
            else
            {
                btnRequire.Enabled = false;
                btnIgnore.Enabled = false;
            }

            UpdateClashHighlight();
        }

        private void MoveRight()
        {
            if (listViewIgnored.SelectedItems.Count == 0 || listViewIgnored.SelectedItems[0] == null)
                return;

            ListViewItem item = listViewIgnored.SelectedItems[0];
            Type type = (Type)item.Tag;
            type.Required = true;
            Timetable_.BuildCompatibility();

            int index = listViewIgnored.SelectedIndices[0];
            listViewIgnored.Items.RemoveAt(index);

            // look through each subject group in the required list
            foreach (ListViewGroup group in listViewRequired.Groups)
            {
                // if we've found the subject group
                if ((Subject)group.Tag == type.Subject)
                {
                    // set group and add to list
                    item.Group = group;
                    listViewRequired.Items.Add(item);
                    break;
                }
            }

            // select the next item in the list
            if (index == listViewIgnored.Items.Count)
                index--;
            if (index >= 0)
            {
                listViewIgnored.Items[index].Selected = true;
                listViewIgnored.Select();
            }
            else
            {
                btnRequire.Enabled = false;
                btnIgnore.Enabled = false;
            }

            UpdateClashHighlight();
        }

        #endregion

    }
}