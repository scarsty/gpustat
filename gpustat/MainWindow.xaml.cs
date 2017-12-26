using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace gpustat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //创建进程实例
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            //strCommand指定要执行进程的路径
            pProcess.StartInfo.FileName = @"C:\Program Files\NVIDIA Corporation\NVSMI\nvidia-smi.exe";

            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //将输出重定向
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            //如果需要可以指定工作目录
            //pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;

            //执行进程
            pProcess.Start();

            //获得进程执行输出
            string strOutput = pProcess.StandardOutput.ReadToEnd();

            //等待进程执行结束
            pProcess.WaitForExit();

            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(strOutput)));
        }
    }
}
