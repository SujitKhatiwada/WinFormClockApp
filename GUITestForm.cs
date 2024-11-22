using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace GUITestApplication
{
    public partial class GUITestForm : Form
    {
        private Label currentTimeLabel = new Label();

        //1: Created a property so code outside this class can read and write to currentTimeLabel.Text
        public string CurrentTimeText
        {
            get { return currentTimeLabel.Text; }
            set { currentTimeLabel.Text = value; }
        }

        // A background worker thread to update the label to current time
        private BackgroundWorker worker = new BackgroundWorker();

        public GUITestForm()
        {
            InitializeComponent();
            SetupForm();

            // Hook to DoWork event and Run the background worker 
            worker.DoWork += Worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }

        private void SetupForm()
        { 
            //Setup the properties of the display, form, and add label
            this.Text = "GUI Test Application";
            this.BackColor = System.Drawing.Color.Gray;
            this.Size = new System.Drawing.Size(400, 300);

            currentTimeLabel.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
            currentTimeLabel.ForeColor = System.Drawing.Color.DarkBlue;
            currentTimeLabel.AutoSize = true;
            currentTimeLabel.Location = new System.Drawing.Point((this.Width - currentTimeLabel.Width) / 2, this.Height / 2);
            this.Controls.Add(currentTimeLabel);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Keep the worker running until cancellation is requested
            while (!worker.CancellationPending)
            {
                try
                {
                    //#2: Got the current time and set currentTimeLabel.Text
                    // Updating the label on UI thread using Invoke to safely access UI controls from the worker thread
                    this.Invoke((MethodInvoker)(() =>
                    {
                        CurrentTimeText = DateTime.Now.ToString("hh:mm:ss tt");
                    }));

                    // Sleep for 1 second before updating the time again
                    Thread.Sleep(1000);
                }
                catch (ObjectDisposedException)
                {
                    // If the worker or the form is disposed, exit the loop gracefully to stop the worker
                    break;
                }
            }
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (worker.IsBusy)
                {
                    worker.CancelAsync();
                    Thread.Sleep(500);
                }
                worker.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
