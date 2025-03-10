using rand7.ViewModels.Pages;
using rand7.Views.Windows;
using SpeechLib;
using System.Collections;
using System.Windows.Shell;
using System.Windows.Threading;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages
{
    public partial class SinglePage : INavigableView<SingleViewModel>
    {
        bool rolling = false;
        int num_cur;
        int num_t;
        int from = int.Parse(v.x[20].value), to = int.Parse(v.x[21].value);
        public static ArrayList num_list = new ArrayList();
        public SingleViewModel ViewModel { get; }

        public SinglePage(SingleViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            if (v.x[5].value == "1") Button_Roll.Content = "开始";
            if (v.x[7].value == "0") Name.Visibility = System.Windows.Visibility.Collapsed;
            if (v.x[8].value == "0") Num.Visibility = System.Windows.Visibility.Collapsed;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(0.05);   //设置刷新的间隔时间
        }

        DispatcherTimer timer = new DispatcherTimer();
        void timer_Tick(object sender, EventArgs e)
        {
            if (v.x[6].value == "0")
            {
                Random rand = new Random();

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

                string name_string = "";
                name_string += v.name[num_cur];
                //MessageBox.Show(name_string.Length.ToString()); 
                if (name_string.Length == 2)
                {
                    name_string = name_string.Insert(1, "　");
                    //MessageBox.Show(name_string); 
                }
                Name.Text = name_string;
            }
            else
            {
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
                string name_string = "";
                name_string += v.name[num_cur];
                //MessageBox.Show(name_string.Length.ToString()); 
                if (name_string.Length == 2)
                {
                    name_string = name_string.Insert(1, "　");
                    //MessageBox.Show(name_string); 
                }
                Name.Text = name_string;
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
        private void gen_num_list()
        {
            for (int i = from; i <= to; i++)
            {
                if (v.name[i] == "【空】") continue;

                num_list.Add(i);

            }
            if (v.x[18].value == "1")
            {
                if (v.x[19].value == "1" || v.x[19].value == "2")
                {

                }
                else if (v.x[19].value == "3")
                {
                    v.shared_num_list.Clear();
                    v.shared_num_list.AddRange(num_list);
                }
            }
            else if (v.x[18].value == "2")
            {
                if (v.x[19].value == "1" || v.x[19].value == "2")
                {
                    v.saved_mul_num_list.Clear();
                    v.saved_mul_num_list.AddRange(num_list);
                    App.writesig();
                }
                else if (v.x[19].value == "3")
                {
                    v.saved_shared_num_list.Clear();
                    v.saved_shared_num_list.AddRange(num_list);
                    App.writeshared();
                }
            }
        }
        private void blank_OnButtonRightClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //blank.Hide();
        }

        private void Button_Roll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            var viewModel = mainWindow.ViewModel;

            if (v.x[7].value == "0") Name.Visibility = System.Windows.Visibility.Collapsed;
            if (v.x[8].value == "0") Num.Visibility = System.Windows.Visibility.Collapsed;
            if (v.x[7].value == "1") Name.Visibility = System.Windows.Visibility.Visible;
            if (v.x[8].value == "1") Num.Visibility = System.Windows.Visibility.Visible;


            if (v.x[5].value == "1" && rolling == true) Button_Roll.Content = "开始";
            else if (v.x[5].value == "0") Button_Roll.Content = "抽号";

            int from_t = int.Parse(v.x[20].value), to_t = int.Parse(v.x[21].value);
            if (v.x[22].value == "0")
            {
                viewModel.ShowSnackbar(
                "错误", // 标题
                "未设置抽号范围\n请前往「设置 - 抽号范围设置」处设置范围喵w", // 内容
                ControlAppearance.Danger, // 样式
                new SymbolIcon(SymbolRegular.ErrorCircle24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(10) // 超时时间
                );

                //blank.Show();
                //error_info.Message = "未设置抽号范围\n请前往「设置 - 抽学生设置 - 抽号范围设置」处设置范围喵w";
                //error_info.IsOpen = true;
                //error_snackbar.Message = "未设置抽号范围\n请前往「设置 - 抽学生设置 - 抽号范围设置」处设置范围喵w";
                //error_snackbar.Show();
                return;
            }
            if (v.x[5].value == "1" && rolling != true) Button_Roll.Content = "结束";
            if (v.x[18].value == "1" && v.x[19].value == "3")
            {
                num_list.Clear();
                num_list.AddRange(v.shared_num_list);
                //num_list = v.shared_num_list;
            }
            if (v.x[18].value == "2" && v.x[19].value == "0")
            {
                num_list.Clear();
                num_list.AddRange(v.saved_sig_num_list);
                //num_list = v.saved_sig_num_list;
            }
            if (v.x[18].value == "2" && v.x[19].value == "2")
            {
                num_list.Clear();
                num_list.AddRange(v.saved_sig_num_list);
                //num_list = v.saved_sig_num_list; ;
            }
            if (v.x[18].value == "2" && v.x[19].value == "3")
            {
                num_list.Clear();
                num_list.AddRange(v.saved_shared_num_list);
                //num_list = v.saved_shared_num_list;
            }
            if (v.x[18].value == "0")
            {
                if (from != from_t || to != to_t) num_list.Clear();
            }
            if (num_list.Count == 0)
            {
                from = from_t;
                to = to_t;
                num_list.Clear();

                if (num_list.Count == 0)
                {
                    gen_num_list();
                }

                /*string t = "";
                for (int i = 0; i < num_list.Count; i++)
                    t += num_list[i].ToString()+" ";
                MessageBox.Show(t.ToString());*/

            }
            /*string t1 = "";
                for (int i = 0; i < num_list.Count; i++)
                    t1 += v.saved_sig_num_list[i].ToString()+" ";
                MessageBox.Show(t1.ToString());*/
            if (v.x[5].value == "1" && Button_Roll.Content == "结束")
            {

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
                    //warning_snackbar.Show();
                }
                timer.Start();
                rolling = true;



            }
            else if (v.x[5].value == "1" && Button_Roll.Content == "开始")
            {
                timer.Stop();
                rolling = false;
                string t = "";
                if (v.x[10].value == "0" && v.x[9].value == "0" && v.x[8].value == "0" && v.x[7].value == "0")
                {
                    /*if (v.achievement_x[4].value != 0)
                    {
                        //warning_snackbar.Title = "你已解锁了该成就，该内容仅作为提示作用";
                        //warning_snackbar.Message = "成就「一叶障目」已经解锁过了！\n\n是不是小提开 E 了，不开显示也不开语音是吧？";
                    }
                    else
                    {
                        //warning_snackbar.Title = "成就解锁";
                        //warning_snackbar.Message = "成就「一叶障目」已解锁！\n\n是不是小提开 E 了，不开显示也不开语音是吧？";
                        DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                        long timeStamp = dto.ToUnixTimeSeconds();
                        v.achievement_x[4].value = (int)timeStamp;
                        App.write_achievement();
                    }*/
                    //warning_snackbar.Show();

                    viewModel.ShowSnackbar(
                "提示", // 标题
                "不开显示也不开语音，你搁这隔这呢？", // 内容
                ControlAppearance.Caution, // 样式
                new SymbolIcon(SymbolRegular.Info24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                 );
                }
                if (v.x[8].value == "0" && v.x[7].value == "0" && (v.x[10].value == "1" || v.x[9].value == "1"))
                {
                    /*if (v.achievement_x[2].value != 0)
                    {
                        //warning_snackbar.Title = "你已解锁了该成就，该内容仅作为提示作用";
                        //warning_snackbar.Message = "成就「让我听听，你在哪儿」已经解锁过了！\n\n看不见，但是听的着 xwx";
                    }
                    else
                    {
                        //warning_snackbar.Title = "成就解锁";
                        //warning_snackbar.Message = "成就「让我听听，你在哪儿」已解锁！\n\n看不见，但是听的着 xwx";
                        DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                        long timeStamp = dto.ToUnixTimeSeconds();
                        v.achievement_x[2].value = (int)timeStamp;
                        App.write_achievement();
                    }*/
                    //warning_snackbar.Show();

                    viewModel.ShowSnackbar(
                "提示", // 标题
                "只开语音不开显示，你搁这做听力呢？", // 内容
                ControlAppearance.Caution, // 样式
                new SymbolIcon(SymbolRegular.Info24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                 );

                }
                if ((v.x[8].value == "1" || v.x[7].value == "1") && (v.x[10].value == "1" || v.x[9].value == "1"))
                {
                    /*if (v.achievement_x[6].value != 0)
                    {

                    }
                    else
                    {
                        //warning_snackbar.Title = "成就解锁";
                        //warning_snackbar.Message = "成就「声音，我听见了。」已解锁！\n\n第一次听到声音呢 xwx";
                        DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                        long timeStamp = dto.ToUnixTimeSeconds();
                        v.achievement_x[6].value = (int)timeStamp;
                        App.write_achievement();
                        //warning_snackbar.Show();
                    }*/

                }
                if (v.x[10].value == "1") t += num_cur.ToString() + "号";
                if (v.x[9].value == "1" && v.name[num_cur] != "" && v.name[num_cur] != null) t += v.name[num_cur];
                if (v.x[10].value == "1" || v.x[9].value == "1") voiceThreadStart(t);
                /* if (v.x[18].value == "1")
                 {
                     if (v.x[19].value == "0" || v.x[19].value == "2") num_list.RemoveAt(num_t);
                     else if (v.x[19].value == "3") num_list.RemoveAt(num_t);
                 }*/
                if (v.x[18].value == "1")
                {
                    if (v.x[19].value == "0" || v.x[19].value == "2")
                    {
                        num_list.RemoveAt(num_t);
                    }
                    else if (v.x[19].value == "3")
                    {
                        num_list.RemoveAt(num_t);
                        v.shared_num_list.Clear();
                        v.shared_num_list.AddRange(num_list);
                    }
                }
                else if (v.x[18].value == "2")
                {
                    if (v.x[19].value == "0" || v.x[19].value == "2")
                    {
                        num_list.RemoveAt(num_t);
                        v.saved_sig_num_list.Clear();
                        v.saved_sig_num_list.AddRange(num_list);
                        App.writesig();
                    }
                    else if (v.x[19].value == "3")
                    {
                        num_list.RemoveAt(num_t);
                        v.saved_shared_num_list.Clear();
                        v.saved_shared_num_list.AddRange(num_list);
                        App.writeshared();
                    }
                }

                /*string t2 = "";
                for (int i = 0; i < num_list.Count; i++)
                    t2 += num_list[i].ToString() + " ";
                MessageBox.Show(t2.ToString());*/

            }
            else if (rolling == true)
            {
                timer.Stop();
                rolling = false;
            }
            else if (v.x[5].value == "0")
            {
                Random rand = new Random();

                int t = rand.Next(0, num_list.Count);
                num_cur = (int)num_list[t];
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
                string name_string = "";
                name_string += v.name[num_cur];
                //MessageBox.Show(name_string.Length.ToString()); 
                if (name_string.Length == 2)
                {
                    name_string = name_string.Insert(1, "　");
                    //MessageBox.Show(name_string); 
                }
                Name.Text = name_string;
                string st = "";
                if (v.x[10].value == "1") st += num_cur.ToString() + "号";
                if (v.x[9].value == "1" && v.name[num_cur] != "" && v.name[num_cur] != null) st += v.name[num_cur];
                if (v.x[10].value == "1" || v.x[9].value == "1") voiceThreadStart(st);
                if (v.x[8].value == "0" && v.x[7].value == "0" && v.x[10].value == "0" && v.x[9].value == "0")
                {
                    /*if (v.achievement_x[4].value != 0)
                    {
                        //warning_snackbar.Title = "你已解锁了该成就，该内容仅作为提示作用";
                        //warning_snackbar.Message = "成就「一叶障目」已经解锁过了！\n\n是不是小提开 E 了，不开显示也不开语音是吧？";
                    }
                    else
                    {
                        //warning_snackbar.Title = "成就解锁";
                        //warning_snackbar.Message = "成就「一叶障目」已解锁！\n\n是不是小提开 E 了，不开显示也不开语音是吧？";
                        DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                        long timeStamp = dto.ToUnixTimeSeconds();
                        v.achievement_x[4].value = (int)timeStamp;
                        App.write_achievement();
                    }*/
                    //warning_snackbar.Show();

                    viewModel.ShowSnackbar(
                "提示", // 标题
                "不开显示也不开语音，你搁这隔这呢？", // 内容
                ControlAppearance.Caution, // 样式
                new SymbolIcon(SymbolRegular.Info24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                 );
                }
                if (v.x[8].value == "0" && v.x[7].value == "0" && (v.x[10].value == "1" || v.x[9].value == "1"))
                {
                    /*if (v.achievement_x[2].value != 0)
                    {
                        //warning_snackbar.Title = "你已解锁了该成就，该内容仅作为提示作用";
                        //warning_snackbar.Message = "成就「让我听听，你在哪儿」已经解锁过了！\n\n看不见，但是听的着 xwx";
                    }
                    else
                    {
                        //warning_snackbar.Title = "成就解锁";
                        //warning_snackbar.Message = "成就「让我听听，你在哪儿」已解锁！\n\n看不见，但是听的着 xwx";
                        DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                        long timeStamp = dto.ToUnixTimeSeconds();
                        v.achievement_x[2].value = (int)timeStamp;
                        App.write_achievement();
                    }*/
                    //warning_snackbar.Show

                    viewModel.ShowSnackbar(
                "提示", // 标题
                "只开语音不开显示，你搁这做听力呢？", // 内容
                ControlAppearance.Caution, // 样式
                new SymbolIcon(SymbolRegular.Info24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                 );
                }
                if ((v.x[8].value == "1" || v.x[7].value == "1") && (v.x[10].value == "1" || v.x[9].value == "1"))
                {
                    /*if (v.achievement_x[6].value != 0)
                    {

                    }
                    else
                    {
                        //warning_snackbar.Title = "成就解锁";
                        //warning_snackbar.Message = "成就「声音，我听见了。」已解锁！\n\n第一次听到声音呢 xwx";
                        DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                        long timeStamp = dto.ToUnixTimeSeconds();
                        v.achievement_x[6].value = (int)timeStamp;
                        App.write_achievement();
                        //warning_snackbar.Show();

                    }*/
                }
                if (v.x[18].value == "1")
                {
                    if (v.x[19].value == "0" || v.x[19].value == "2")
                    {
                        num_list.RemoveAt(t);
                    }
                    else if (v.x[19].value == "3")
                    {
                        num_list.RemoveAt(t);
                        v.shared_num_list.Clear();
                        v.shared_num_list.AddRange(num_list);
                    }
                }
                else if (v.x[18].value == "2")
                {
                    if (v.x[19].value == "0" || v.x[19].value == "2")
                    {
                        num_list.RemoveAt(t);
                        v.saved_sig_num_list.Clear();
                        v.saved_sig_num_list.AddRange(num_list);
                        App.writesig();
                    }
                    else if (v.x[19].value == "3")
                    {
                        num_list.RemoveAt(t);
                        v.saved_shared_num_list.Clear();
                        v.saved_shared_num_list.AddRange(num_list);
                        App.writeshared();
                    }
                }
                /*string t2 = "";
                for (int i = 0; i < num_list.Count; i++)
                    t2 += num_list[i].ToString() + " ";
                MessageBox.Show(t2.ToString());*/
            }
        }
    }
}