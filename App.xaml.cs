using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using rand7.Services;
using rand7.ViewModels.Pages;
using rand7.ViewModels.Pages.Settings;
using rand7.ViewModels.Windows;
using rand7.Views.Pages;
using rand7.Views.Pages.Settings;
using rand7.Views.Windows;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Threading;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;
using Wpf.Ui.Abstractions.Controls;

using Wpf.Ui.Controls;
using Wpf.Ui.Appearance;
using System.Collections;

using System.Runtime.InteropServices;
using System.Text;
using System;
using System.Collections;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using System.Diagnostics;

namespace rand7
{
    public class setting
    {
        public string section = string.Empty;
        /*public string Section
        {
            get { return section; }
            set { section = value; }
        }*/

        public string key = string.Empty;
        public string value = string.Empty;
        public string def = string.Empty;
    };
    public class achievement
    {

        public string key = string.Empty;
        public int value = 0;
        public int def = 0;
    };
    public class v
    {
        public static int num = 0;
        public static int achievement_num = 0;
        public static setting[] x = new setting[101];
        public static achievement[] achievement_x = new achievement[101];
        public static int stu_from = 0, stu_to = 0, stu_t = 0;
        public static int stu_auto_num = 0;
        public static int stu_blank_num = 0;
        public static string[] name = new string[100001];
        public static ArrayList shared_num_list = new ArrayList();
        public static ArrayList saved_shared_num_list = new ArrayList();
        public static ArrayList saved_sig_num_list = new ArrayList();
        public static ArrayList saved_mul_num_list = new ArrayList();
        public static void set(string section, string key, string def)
        {
            num++;
            x[v.num] = new setting();
            x[v.num].section = section;
            //x[0].section = "1";
            x[v.num].key = key;
            x[v.num].def = def;


        }
        public static void set_achievement(string section, string key, int def)
        {
            achievement_num++;
            achievement_x[v.achievement_num] = new achievement();
            //achievement_x[0].section = "1";
            achievement_x[v.achievement_num].key = key;
            achievement_x[v.achievement_num].def = def;


        }
    }
    public static class Program
    {

        

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(WndEnumProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private delegate bool WndEnumProc(IntPtr hWnd, IntPtr lParam);

        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {

            EnumWindows(new WndEnumProc(EnumWindowsProc), IntPtr.Zero);
            //Window _mainWindow = Application.Current.MainWindow as MainWindow;
            //SystemThemeWatcher.Watch(_mainWindow, WindowBackdropType.Mica, true);
            //ApplicationThemeManager.ApplySystemTheme();

            //Window _mainWindow = Application.Current.MainWindow as MainWindow;


            v.set("Personalization", "Dark_Mode", "0");

            v.set("Single_Number", "Roll", "1");
            v.set("Single_Number", "Roll_In_Order", "0");
            v.set("Single_Number", "Voice", "0");

            v.set("Single_Student", "Roll", "1");
            v.set("Single_Student", "Roll_In_Order", "0");
            v.set("Single_Student", "Show_Name", "1");
            v.set("Single_Student", "Show_Num", "1");
            v.set("Single_Student", "Voice_Name", "1");
            v.set("Single_Student", "Voice_Num", "0");

            v.set("Multiple_Student", "Show_In_Order", "0");
            v.set("Multiple_Student", "Roll", "1");
            v.set("Multiple_Student", "Roll_In_Order", "0");
            v.set("Multiple_Student", "Show_Name", "1");
            v.set("Multiple_Student", "Show_Num", "1");
            v.set("Multiple_Student", "Voice_Name", "0");
            v.set("Multiple_Student", "Voice_Num", "0");

            v.set("Student", "Avoid_Duplication_Setting", "0");
            v.set("Student", "Avoid_Duplication_Mode", "3");
            v.set("Student", "From", "1");
            v.set("Student", "To", "60");
            v.set("Student", "Set_Range", "0");

            v.set("Personalization", "One_Digit_Number", "1");
            v.set("Personalization", "Two_Character_Name", "1");

            App.readini();
            App.writeini();

            v.stu_from = int.Parse(v.x[20].value);
            v.stu_t = int.Parse(v.x[21].value);

            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                using (StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "Name.txt", Encoding.UTF8))
                {
                    string line;
                    v.stu_t = v.stu_from;
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {

                        int commentIndex = line.IndexOf("//");
                        if (commentIndex >= 0)
                        {
                            line = line.Substring(0, commentIndex);
                        }

                        // 可选：移除行首尾空白（根据需要）
                        line = line.Trim();

                        if (string.IsNullOrWhiteSpace(line)) continue;

                        v.name[v.stu_t] = line;
                        v.stu_t++;
                        v.stu_auto_num++;
                        if (line == "【空】") v.stu_blank_num++;

                    }

                }

                if (v.stu_auto_num == 0)
                {
                   //System.Windows.MessageBox.Show(v.stu_auto_num.ToString());

                    throw new Exception("");
                }
            }
            catch (Exception F)
            {
                // 向用户显示出错消息
                //Console.WriteLine("The file could not be read:");
                //Console.WriteLine(e.Message);
                //FileStream fs = File.Create("Name.txt");
                //fs.Close();

                try
                {
                    StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Name.txt", false, Encoding.UTF8);
                    //System.Windows.MessageBox.Show(System.AppDomain.CurrentDomain.BaseDirectory + "Name.txt");
                    
                    sw.WriteLine("// 请将姓名粘贴在横线下面，每个一行。");
                    sw.WriteLine();
                    sw.WriteLine("// 提示 1：可以直接从 Excel 表格中复制姓名并在横线下面粘贴");
                    sw.WriteLine("// 提示 2：会从 1 开始按照顺序自动分配座号，如果不需要自动分配的座号，也可以在显示设置中关闭显示座号。");
                    sw.WriteLine("// 提示 3：如果某个座号对应没有学生，请用「【空】」代替这一行");
                    sw.WriteLine();
                    sw.WriteLine("// ————————————————————————");

                    sw.Close();
                }
                catch (Exception e)
                {
                }

            }
            App.readsig();
            App.readmul();
            App.readshared();

            if (v.x[22].value == "0")
            {
                if (v.stu_auto_num != 0)
                {
                    v.x[22].value = "-1";


                    if (int.Parse(v.x[21].value)!=v.stu_auto_num)
                    {
                        v.x[21].value = v.stu_auto_num.ToString();

                        v.saved_sig_num_list.Clear();
                        v.saved_mul_num_list.Clear();
                        v.saved_shared_num_list.Clear();
                        App.writesig();
                        App.writemul();
                        App.writeshared();
                    }
                    App.writeini();
                }
            }



            //SystemThemeWatcher.Watch(_mainWindow, WindowBackdropType.Mica, true);
            //ApplicationThemeManager.ApplySystemTheme();
            //SystemThemeWatcher.UnWatch(_mainWindow);
            //ApplicationThemeManager.Apply(ApplicationTheme.Light);

            rand7.App app = new rand7.App();
           app.InitializeComponent();
           app.Run();
       }

        private static bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam)
        {
            // 获取窗口文本
            string windowText = GetWindowText(hwnd);

            if (windowText == null) return true;
            // 检查是否是目标窗口
            if (windowText.Contains("抽号器"))
            {
                // 获取窗口所属进程ID
                uint processId;
                GetWindowThreadProcessId(hwnd, out processId);

                // 获取进程对象
                Process process = Process.GetProcessById((int)processId);

                // 确认进程名称以进一步验证
                if (process.MainWindowTitle.Contains("抽号器"))
                {
                    // 最大化窗口
                    ShowWindow(hwnd, 5);

                    // 设置为前台窗口
                    SetForegroundWindow(hwnd);

                    // 设置焦点
                    SetFocus(hwnd);

                    // 结束程序
                    Environment.Exit(0);

                    // 返回 false 表示停止枚举其他窗口
                    return false;
                }
            }

            // 继续枚举下一个窗口
            return true;
        }

        // 获取窗口文本的方法
        private static string GetWindowText(IntPtr hWnd)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(hWnd, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }

            return null;
        }

    }
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(System.IO.Path.GetDirectoryName(AppContext.BaseDirectory)); })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();

                // Page resolver service
                //services.AddSingleton<IPageService, PageService>();
                services.AddNavigationViewPageProvider();

                // Theme manipulation
                services.AddSingleton<IThemeService, ThemeService>();

                // TaskBar manipulation
                services.AddSingleton<ITaskBarService, TaskBarService>();

                // Service containing navigation, same as INavigationWindow... but without window
                services.AddSingleton<INavigationService, NavigationService>();

                // Main window with navigation
                services.AddSingleton<INavigationWindow, MainWindow>();


                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<MainWindowViewModel>();

                services.AddSingleton<HomePage>();
                services.AddSingleton<SinglePage>();
                services.AddSingleton<MultiplePage>();
                services.AddSingleton<RewardPage>();
                services.AddSingleton<SettingsPage>();
                services.AddSingleton<PersonalizationPage>();
                services.AddSingleton<BalancePage>();
                services.AddSingleton<NumberPage>();
                services.AddSingleton<SingleStudentPage>();
                services.AddSingleton<MultipleStudentPage>();
                services.AddSingleton<DeveloperPage>();
                services.AddSingleton<AboutPage>();



                services.AddSingleton<HomeViewModel>();
                services.AddSingleton<SingleViewModel>();
                services.AddSingleton<MultipleViewModel>();
                services.AddSingleton<RewardViewModel>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<PersonalizationViewModel>();
                services.AddSingleton<BalanceViewModel>();
                services.AddSingleton<NumberViewModel>();
                services.AddSingleton<SingleStudentViewModel>();
                services.AddSingleton<MultipleStudentViewModel>();
                services.AddSingleton<DeveloperViewModel>();
                services.AddSingleton<AboutViewModel>();


            }).Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        
        private void OnStartup(object sender, StartupEventArgs e)
        {
            _host.Start();


            Window _mainWindow = Application.Current.MainWindow as MainWindow;
            if (v.x[1].value == "0")
            {
                SystemThemeWatcher.Watch(_mainWindow, WindowBackdropType.Mica, true);
                ApplicationThemeManager.ApplySystemTheme();
            }
            else if (v.x[1].value == "1")
            {
                SystemThemeWatcher.UnWatch(_mainWindow);
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
            }
            else if (v.x[1].value == "2")
            {
                SystemThemeWatcher.UnWatch(_mainWindow);
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
            }
            else if (v.x[1].value == "3")
            {
                SystemThemeWatcher.UnWatch(_mainWindow);
                ApplicationThemeManager.Apply(ApplicationTheme.HighContrast);
            }



        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }

        public static void readini()
        {
            //string 
            INIFile ini = new INIFile(System.AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            for (int i = 1; i <= v.num; i++)
            {
                string t = ini.IniReadValue(v.x[i].section, v.x[i].key);
                if (t != "")
                {
                    v.x[i].value = t;
                }
                else v.x[i].value = v.x[i].def;
            }
        }
        public static void resetini()
        {
            for (int i = 1; i <= v.num; i++)
            {
                v.x[i].value = v.x[i].def;
            }
        }

        public static void writeini()
        {

            INIFile ini = new INIFile(System.AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            for (int i = 1; i <= v.num; i++)
            {
                ini.IniWriteValue(v.x[i].section, v.x[i].key, v.x[i].value);

            }
        }
        // 废弃的成就系统，因为感觉用处不大，再加上很多一体机都有还原
        /*public static void read_achievement()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LuoYunXi0407\Rand");
            if (key != null)
            {
                for (int i = 1; i <= v.achievement_num; i++)
                {
                    object t = key.GetValue(v.achievement_x[i].key);


                    if (t != null)
                    {
                        int value = (int)t;
                        v.achievement_x[i].value = value;
                    }
                    else v.achievement_x[i].value = v.achievement_x[i].def;
                }
                key.Close();
            }
            else Registry.LocalMachine.CreateSubKey(@"SOFTWARE\LuoYunXi0407\Rand");



        }
        public static void write_achievement()
        {

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LuoYunXi0407\Rand", true);
            for (int i = 1; i <= v.achievement_num; i++)
            {
                key.SetValue(v.achievement_x[i].key, v.achievement_x[i].value);

            }


            key.Close();
        }
        */
        public static void readsig()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "sig");
                int t = 1;
                line = sr.ReadLine();
                while (line != null)
                {
                    v.saved_sig_num_list.Add(int.Parse(line));
                    line = sr.ReadLine();

                }
                sr.Close();
            }
            catch (Exception e)
            {
            }

        }
        public static void readmul()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "mul");
                int t = 1;
                line = sr.ReadLine();
                while (line != null)
                {
                    v.saved_mul_num_list.Add(int.Parse(line));
                    line = sr.ReadLine();

                }
                sr.Close();
            }
            catch (Exception e)
            {
            }
        }
        public static void readshared()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "shared");
                int t = 1;
                line = sr.ReadLine();
                while (line != null)
                {
                    v.saved_shared_num_list.Add(int.Parse(line));
                    line = sr.ReadLine();

                }
                sr.Close();
            }
            catch (Exception e)
            {
            }
        }
        public static void writesig()
        {
            try
            {
                StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "sig");
                for (int i = 0; i < v.saved_sig_num_list.Count; i++)
                {
                    sw.WriteLine(v.saved_sig_num_list[i]);
                }
                sw.Close();
            }
            catch (Exception e)
            {
            }
        }
        public static void writemul()
        {
            try
            {
                StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "mul");
                for (int i = 0; i < v.saved_mul_num_list.Count; i++)
                {
                    sw.WriteLine(v.saved_mul_num_list[i]);
                }
                sw.Close();
            }
            catch (Exception e)
            {
            }
        }
        public static void writeshared()
        {
            try
            {
                StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "shared");
                for (int i = 0; i < v.saved_shared_num_list.Count; i++)
                {
                    sw.WriteLine(v.saved_shared_num_list[i]);
                }
                sw.Close();
            }
            catch (Exception e)
            {
            }
        
        }

        /*
        public int RegReadInt(string Key)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LuoYunXi0407\Rand");
            int value = (int)key.GetValue(Key);
            key.Close();
            return value;
        }

        public void RegWriteInt(string Key, int Value)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LuoYunXi0407\Rand", true);
            key.SetValue(Key, Value);
            key.Close();
        }
        */

        public static string GetTimeStamp()
        {
            //DateTime.Now获取的是电脑上的当前时间
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();//精确到秒
        }

        public static DateTime TimestampToDataTime(long unixTimeStamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当			  地时区
            DateTime dt = startTime.AddSeconds(unixTimeStamp);
            System.Console.WriteLine(dt.ToString("yyyy/MM/dd HH:mm:ss:ffff"));
            return dt;
        }

        /// <summary>
        /// DataTime转时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DataTimeToTimestamp(DateTime dateTime)
        {
            //new System.DateTime(1970, 1, 1)
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(dateTime); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数
            System.Console.WriteLine(timeStamp);
            return timeStamp;
        }
    }
    public class INIFile
    {
        public string path { get; private set; }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public INIFile(string INIPath)
        {
            path = INIPath;
        }
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }

    }
}

