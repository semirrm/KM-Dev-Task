using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace KM_Dev_Task_Image_Processing_Service
{
    public partial class ImageProcessingService : ServiceBase
    {
        public ImageProcessingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log("Service started at " + DateTime.Now);
        }

        protected override void OnStop()
        {
            Log("Service is stopped at " + DateTime.Now);
        }

        public void Log(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath)) {
                using (StreamWriter sw = File.CreateText(filepath))
                    sw.WriteLine(Message);
            }
            else
            {
                using(StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
