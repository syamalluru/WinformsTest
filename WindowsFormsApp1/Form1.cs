using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OnCheckClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked");
            MessageLabel.Text = "Checking ....";
            string scriptfile = File.ReadAllText(@"dotcheck.ps1");
            RunspaceConfiguration config = RunspaceConfiguration.Create();
            Runspace cmdlet = RunspaceFactory.CreateRunspace(config);
            cmdlet.Open();
            Pipeline pipeline = cmdlet.CreatePipeline();
            try
            {
                pipeline.Commands.AddScript(scriptfile);
                Collection<PSObject> output = pipeline.Invoke();
                pipeline.Stop();
                cmdlet.Close();
                StringBuilder results = new StringBuilder();
                foreach (PSObject obj in output)
                {
                    results.AppendLine(obj.ToString());
                }
                if (results.ToString().Contains("Success"))
                {
                    MessageLabel.Text = "Success";
                }
                else
                {
                    MessageLabel.Text = "Failed";
                }
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

    }
}
