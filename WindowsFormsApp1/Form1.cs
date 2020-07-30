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

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked");
            label1.Text = "pre check";
            //RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();

            //Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
            //runspace.Open();

            //Pipeline pipeline = runspace.CreatePipeline();
            //string scriptfile = File.ReadAllText(@"dotcheck.ps1");
            ////Here's how you add a new script with arguments
            //Command myCommand = new Command(scriptfile);
            //CommandParameter testParam = new CommandParameter("key", "value");
            //myCommand.Parameters.Add(testParam);

            //pipeline.Commands.Add(myCommand);

            //// Execute PowerShell script
            //var results = pipeline.Invoke();


            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = @"dotcheck.ps1";
            //startInfo.RedirectStandardOutput = true;
            //startInfo.RedirectStandardError = true;
            //startInfo.UseShellExecute = false;
            //startInfo.CreateNoWindow = true;
            //Process process = new Process();
            //process.StartInfo = startInfo;
            //process.Start();

            //string output = process.StandardOutput.ReadToEnd();

            string scriptfile = File.ReadAllText(@"dotcheck.ps1");
            RunspaceConfiguration config = RunspaceConfiguration.Create();
            PSSnapInException psEx;
            //add Microsoft SharePoint PowerShell SnapIn
            //create powershell runspace
            Runspace cmdlet = RunspaceFactory.CreateRunspace(config);
            cmdlet.Open();
            RunspaceInvoke scriptInvoker = new RunspaceInvoke(cmdlet);
            // set powershell execution policy to unrestricted
            //scriptInvoker.Invoke("Set-ExecutionPolicy Unrestricted");
            // create a pipeline and load it with command object
            Pipeline pipeline = cmdlet.CreatePipeline();
            try
            {
                // Using Get-SPFarm powershell command 
                pipeline.Commands.AddScript(scriptfile);
                // this will format the output
                Collection<PSObject> output = pipeline.Invoke();
                pipeline.Stop();
                cmdlet.Close();
                // process each object in the output and append to stringbuilder  
                StringBuilder results = new StringBuilder();
                foreach (PSObject obj in output)
                {
                    results.AppendLine(obj.ToString());
                }
                if (results.ToString().Contains("Success"))
                {
                    label1.Text = "Success";
                }
                else
                {
                    label1.Text = "Failed";
                }
            }
            catch (Exception ex)
            {
                label1.Text = "Exception";
            }
            //foreach (var  i in results)
            //{

            //}
            //runspace.Close();
        }

    }
}
