using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace phoneBookApp
{
    struct PhoneBookEntry
    {
        public string name;
        public string phone;
    }

    public partial class Form1 : Form
    {

        //field to hold a list of phone book entry objects
        private List<PhoneBookEntry> phoneList =
            new List<PhoneBookEntry>();
        public Form1()
        {
            InitializeComponent();
        }

        //the read file method reads the contents of the phoneList.txt 
        //file and stores it as PhoneBookEntry
        //objects in the phone list 
        private void readFile()
        {
            try
            {
                StreamReader inputFile;     //to read the file 
                string line;                //to hold line from the file

                // create an instance of the PhoneBookEntry structure
                PhoneBookEntry entry = new PhoneBookEntry();

                //create a delimeter array 
                char[] delim = { ',' };

                //open the file 
                inputFile = File.OpenText("PhoneList.txt");

                //read the lines from the file 
                while (!inputFile.EndOfStream)
                {
                    //read a line from the file 
                    line = inputFile.ReadLine();

                    //tokenize the line 
                    string[] tokens = line.Split();

                    //store the tokens in the entry object 
                    entry.name = tokens[0];
                    entry.phone = tokens[1];

                    //add the entry to the phone list 
                    phoneList.Add(entry);
                }
            }
            catch (Exception ex)
            {
                //display an error message
                MessageBox.Show(ex.Message);
            }
        }

        //the displayNames method displays the list of names 
        //in the nameList control Box.
        private void DisplayNames()
        {
            foreach (PhoneBookEntry entry in phoneList)
            {
                nameListBox.Items.Add(entry.name);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'phoneListDataSet.Person' table. You can move, or remove it, as needed.
            this.personTableAdapter.Fill(this.phoneListDataSet.Person);
            //read the phoneList.Txt file.
            readFile();

            //display the names
            DisplayNames();
        }

        private void nameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get the index of the selected item 
            int index = nameListBox.SelectedIndex;

            //display the corresponding phone number 
            phoneLabel.Text = phoneList[index].phone;


        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //close the form 
            this.Close();
        }

        private void personBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.personBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.phoneListDataSet);

        }
    }
}
