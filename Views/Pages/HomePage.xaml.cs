using rand7.ViewModels.Pages;
using System.Collections;
using Wpf.Ui.Controls;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;
using SpeechLib;
using System.Threading;
using System.Collections;
using System.Windows.Controls;
using System.DirectoryServices.ActiveDirectory;
using rand7.Views.Windows;
using Wpf.Ui.Appearance;
using Wpf.Ui.Abstractions.Controls;


namespace rand7.Views.Pages
{
    public partial class HomePage : INavigableView<HomeViewModel>
    {

        int from = 0, to = 0;
        int num_cur;
        int num_t;
        public static bool rolling = false;
        public static ArrayList num_list = new ArrayList();
        public HomeViewModel ViewModel { get; }

        public HomePage(HomeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();


            




            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(0.05);   //设置刷新的间隔时间

            if (v.x[2].value == "1") Button_Roll.Content = "开始";

            
        }
        DispatcherTimer timer = new DispatcherTimer();


        void timer_Tick(object sender, EventArgs e)
        {
            if (v.x[3].value == "0")
            {
                Random rand = new Random();
                /*num_cur = rand.Next(from, to + 1);
                Num.Text = num_cur.ToString();*/
                num_t = rand.Next(0, num_list.Count);
                num_cur = (int)num_list[num_t];
                string num_string = "";
                if (num_cur < 10)
                {
                    if (v.x[23].value == "1") num_string += "0";
                }
                num_string += num_cur.ToString();
                if (num_cur < 10)
                {
                    if (v.x[23].value == "2") num_string += "  ";
                }
                if (v.x[23].value == "2") num_string += "  ";
                Num.Text = num_string;
            }
            else
            {
                /*num_cur++;
                if (num_cur > to) num_cur = from;
                Num.Text = num_cur.ToString();*/
                num_t++;
                if (num_t >= num_list.Count) num_t = 0;
                num_cur = (int)num_list[num_t];
                string num_string = "";
                if (num_cur < 10)
                {
                    if (v.x[23].value == "1") num_string += "0";
                }
                num_string += num_cur.ToString();
                if (num_cur < 10)
                {
                    if (v.x[23].value == "2") num_string += "  ";
                }
                if (v.x[23].value == "2") num_string += "  ";
                Num.Text = num_string;
            }
        }
        public static void Speech(string text)
        {
            SpVoice voice = new SpVoice();
            voice.Rate = 1;
            voice.Volume = 100;
            voice.Voice = voice.GetVoices().Item(0);
            voice.Speak(text);
        }
        public static void voiceThreadStart(string s)
        {
            Thread thread = new Thread(() => Speech(s));
            thread.Start();
        }

        private void Button_Roll_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            var viewModel = mainWindow.ViewModel;


            if (v.x[2].value == "1" && rolling == true) Button_Roll.Content = "开始";
            else if (v.x[2].value == "0") Button_Roll.Content = "抽号";
            int from_t = 0, to_t = 0;
            if (Numfrom.Value == null || Numto.Value == null)
            {
                viewModel.ShowSnackbar(
                "错误", // 标题
                "范围不能为空", // 内容
                ControlAppearance.Danger, // 样式
                new SymbolIcon(SymbolRegular.ErrorCircle24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                 );

                //error_snackbar.Message = "范围不能为空";
                //error_snackbar.Show();

                return;
            }
            from_t = (int)Numfrom.Value;
            to_t = (int)Numto.Value;

            /*if (int.TryParse(Numfrom.Text, out from_t) == false || int.TryParse(Numto.Text, out to_t) == false)
            {
                
                if (v.achievement_x[1].value != 0)
                {
                    //warning_snackbar.Title = "你已解锁了该成就，该内容仅作为提示作用";
                    //warning_snackbar.Message = "成就「来自世界之外的禁忌知识」已解锁过了！\n\n就你这小子往数字框里面塞其他东西是吧？";
                }
                else
                {
                    //warning_snackbar.Title = "成就解锁";
                    //warning_snackbar.Message = "成就「来自世界之外的禁忌知识」已解锁！\n\n就你这小子往数字框里面塞其他东西是吧？";
                    DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                    long timeStamp = dto.ToUnixTimeSeconds();
                    v.achievement_x[1].value = (int)timeStamp;
                    App.write_achievement();
                }

                //warning_snackbar.Show();
                return;
            }*/
            if (from_t > to_t)
            {

                viewModel.ShowSnackbar(
                "错误", // 标题
                "范围的起点不能大于终点", // 内容
                ControlAppearance.Danger, // 样式
                new SymbolIcon(SymbolRegular.ErrorCircle24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                );

                //reverse.Show();
                /*error_info.Message = "范围的起点不能大于终点";
                error_info.IsOpen = true;*/
                //error_snackbar.Message = "范围的起点不能大于终点";
                //error_snackbar.Show();
                return;
            }
            if (v.x[2].value == "1" && rolling != true) Button_Roll.Content = "结束";
            if (from_t != from || to_t != to || num_list.Count == 0)
            {
                from = from_t;
                to = to_t;
                num_list.Clear();
                for (int i = from; i <= to; i++)
                {
                    num_list.Add(i);
                    //MessageBox.Show(i.ToString());
                }
                /*string t = "";
                for (int i = 0; i < num_list.Count; i++)
                    t += num_list[i].ToString()+" ";*/
                //MessageBox.Show(t.ToString());
            }
            if (v.x[2].value == "1" && Button_Roll.Content == "结束")
            {
                //Button_Roll.Content = "结束";
                //if(num_cur == 0 || num_cur < from) num_cur = from - 1;
                if (num_t == 0 || num_t < (int)num_list[0]) num_t = -1;
                if (num_list.Count == 1)
                {
                    /*if (v.achievement_x[5].value != 0)
                    {
                        //warning_snackbar.Title = "你已解锁了该成就，该内容仅作为提示作用";
                        //warning_snackbar.Message = "成就「别想逃开哦」已经解锁过了！\n\n摇号，但是池子里只有一个人！\n可能是因为池子确实只有一个人，也有可能是你开了不重复选项ww";
                    }
                    else
                    {
                        //warning_snackbar.Title = "成就解锁";
                        //warning_snackbar.Message = "成就「别想逃开哦」已解锁！\n\n摇号，但是池子里只有一个人！\n可能是因为池子确实只有一个人，也有可能是你开了不重复选项ww";
                        DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                        long timeStamp = dto.ToUnixTimeSeconds();
                        v.achievement_x[5].value = (int)timeStamp;
                        App.write_achievement();
                    }*/
                    //warning_snackbar.Show();

                    viewModel.ShowSnackbar(
                "提示", // 标题
                "这里不是出现 Bug 了，而是因为池子里面只有一个人\n可能是因为池子确实只有一个人，也有可能是你开了不重复选项ww", // 内容
                ControlAppearance.Caution, // 样式
                new SymbolIcon(SymbolRegular.Info24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(10) // 超时时间
                 );
                }
                timer.Start();
                rolling = true;



            }
            else if (v.x[2].value == "1" && Button_Roll.Content == "开始")
            {
                timer.Stop();
                rolling = false;
                //if (v.x[4].value == "1") voiceThreadStart(num_cur.ToString());
                if (v.x[4].value == "1") voiceThreadStart(num_cur.ToString());
                if (Single_Number_Avoid_Duplication_Until_Restart.IsChecked == true) num_list.RemoveAt(num_t);
                /*string t2 = "";
                for (int i = 0; i < num_list.Count; i++)
                    t2 += num_list[i].ToString() + " ";
                MessageBox.Show(t2.ToString());*/
                //Button_Roll.Content = "开始";
            }
            else if (rolling == true)
            {
                timer.Stop();
                rolling = false;
                //if (v.x[4].value == "1") voiceThreadStart(num_cur.ToString());
                //Button_Roll.Content = "开始";
            }
            else if (v.x[2].value == "0")
            {
                //MessageBox.Show("?");
                Random rand = new Random();
                /*num_cur = rand.Next(from, to + 1);
                Num.Text = num_cur.ToString();*/
                int t = rand.Next(0, num_list.Count);
                num_cur = (int)num_list[t];
                // MessageBox.Show(t.ToString());
                string num_string = "";
                if (num_cur < 10)
                {
                    if (v.x[23].value == "1") num_string += "0";
                }
                num_string += num_cur.ToString();
                if (num_cur < 10)
                {
                    if (v.x[23].value == "2") num_string += "  ";
                }
                if (v.x[23].value == "2") num_string += "  ";
                Num.Text = num_string;
                if (v.x[4].value == "1") voiceThreadStart(num_cur.ToString());
                if (Single_Number_Avoid_Duplication_Until_Restart.IsChecked == true) num_list.RemoveAt(t);
                /*string t2 = "";
                for (int i = 0; i < num_list.Count; i++)
                    t2 += num_list[i].ToString() + " ";
                MessageBox.Show(t2.ToString());*/
            }
        }
    }
}
