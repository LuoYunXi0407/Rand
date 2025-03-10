using rand7.ViewModels.Pages;
using rand7.Views.Windows;
using SpeechLib;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using Wpf.Ui.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages
{
    public partial class MultiplePage : INavigableView<MultipleViewModel>
    {
        public static int people_num_temp = 0;
        public static int textsize_temp = 0;
        bool rolling = false;
        int num_cur;
        int num_t;
        int from = int.Parse(v.x[20].value), to = int.Parse(v.x[21].value);
        public static ArrayList num_list = new ArrayList();
        public static ArrayList num_list_incomplete = new ArrayList();
        public static ArrayList result_list_cur = new ArrayList();
        public static ArrayList result_list_t = new ArrayList();
        public static ArrayList result_list_real = new ArrayList();
        bool is_num_list_incomplete = false;
        bool reset = false;
        public MultipleViewModel ViewModel { get; }

        public MultiplePage(MultipleViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            if (v.x[12].value == "1") Button_Roll.Content = "开始";
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(0.05);   //设置刷新的间隔时间
        }

        DispatcherTimer timer = new DispatcherTimer();
        void timer_Tick(object sender, EventArgs e)
        {
            if (v.x[13].value == "0")
            {
                Random rand = new Random();
                result_list_t.Clear();
                result_list_cur.Clear();
                result_list_real.Clear();
                ArrayList temp_num_list = new ArrayList();
                temp_num_list.AddRange(num_list);
                ArrayList temp_num_list_incomplete = new ArrayList();
                temp_num_list_incomplete.AddRange(num_list_incomplete);
                //temp_num_list = num_list;
                //MessageBox.Show(temp_num_list.Count.ToString());
                for (int i = 1; i <= num_list_incomplete.Count; i++)
                {
                    num_t = rand.Next(0, temp_num_list_incomplete.Count);
                    num_cur = (int)temp_num_list_incomplete[num_t];
                    result_list_cur.Add(num_cur);
                    temp_num_list_incomplete.RemoveAt(num_t);
                }
                for (int i = 1; i <= people_num_temp - num_list_incomplete.Count; i++)
                {
                    num_t = rand.Next(0, temp_num_list.Count);
                    num_cur = (int)temp_num_list[num_t];
                    result_list_cur.Add(num_cur);
                    result_list_real.Add(num_cur);
                    result_list_t.Add(num_list.IndexOf(num_cur));
                    temp_num_list.RemoveAt(num_t);
                }
                if (v.x[11].value == "1")
                {
                    result_list_cur.Sort();
                }
                for (int i = 0; i < people_num_temp; i++)
                {
                    string t = "";



                    TextBlock tb = (TextBlock)Result.Children[i];

                    //result_list_t.Add(num_t);
                    if (v.x[15].value == "1")
                    {
                        string num_string = "";
                        if ((int)result_list_cur[i] < 10)
                        {
                            if (v.x[23].value == "1") num_string += "0";
                        }
                        num_string += result_list_cur[i].ToString();
                        if ((int)result_list_cur[i] < 10)
                        {
                            if (v.x[23].value == "2") num_string += "  ";
                        }
                        if (v.x[23].value == "2") num_string += "  ";

                        t += num_string.ToString();
                    }
                    string name_string = "";
                    name_string += v.name[(int)result_list_cur[i]];
                    //MessageBox.Show(name_string.Length.ToString()); 
                    if (name_string.Length == 2)
                    {
                        name_string = name_string.Insert(1, "　");
                        //MessageBox.Show(name_string); 
                    }
                    if (v.x[14].value == "1") t += name_string;
                    tb.Text = t;

                    //tb1.Text = "?";
                }



            }
            else
            {

                Random rand = new Random();


                if (result_list_cur.Count != people_num_temp || reset == true)
                {
                    //MessageBox.Show("?");
                    reset = false;
                    result_list_t.Clear();
                    result_list_cur.Clear();
                    ArrayList temp_num_list_incomplete = new ArrayList();
                    temp_num_list_incomplete.AddRange(num_list_incomplete);
                    for (int i = 1; i <= num_list_incomplete.Count; i++)
                    {
                        num_t = rand.Next(0, temp_num_list_incomplete.Count);
                        num_cur = (int)temp_num_list_incomplete[num_t];
                        result_list_cur.Add(num_cur);
                        temp_num_list_incomplete.RemoveAt(num_t);
                    }

                    ArrayList temp_num_list = new ArrayList();
                    temp_num_list.AddRange(num_list);

                    //temp_num_list = num_list;
                    //MessageBox.Show(temp_num_list.Count.ToString());

                    for (int i = 1; i <= people_num_temp - num_list_incomplete.Count; i++)
                    {
                        num_t = rand.Next(0, temp_num_list.Count);
                        num_cur = (int)temp_num_list[num_t];
                        result_list_cur.Add(num_cur);
                        result_list_real.Add(num_cur);
                        result_list_t.Add(num_list.IndexOf(num_cur));
                        temp_num_list.RemoveAt(num_t);
                    }
                    if (v.x[11].value == "1")
                    {
                        result_list_cur.Sort();
                    }
                    for (int i = 0; i < people_num_temp; i++)
                    {
                        string t = "";

                        TextBlock tb = (TextBlock)Result.Children[i];

                        //result_list_t.Add(num_t);
                        if (v.x[15].value == "1")
                        {
                            string num_string = "";
                            if ((int)result_list_cur[i] < 10)
                            {
                                if (v.x[23].value == "1") num_string += "0";
                            }
                            num_string += result_list_cur[i].ToString();
                            if ((int)result_list_cur[i] < 10)
                            {
                                if (v.x[23].value == "2") num_string += "  ";
                            }
                            if (v.x[23].value == "2") num_string += "  ";

                            t += num_string.ToString();
                        }

                        string name_string = "";
                        name_string += v.name[(int)result_list_cur[i]];
                        //MessageBox.Show(name_string.Length.ToString()); 
                        if (name_string.Length == 2)
                        {
                            name_string = name_string.Insert(1, "　");
                            //MessageBox.Show(name_string); 
                        }
                        if (v.x[14].value == "1") t += name_string;
                        tb.Text = t;

                        /*string t3= "smjb\n";
                for (int i = 0; i < num_list.Count; i++)
                    t3 += num_list[i].ToString() + " ";
                MessageBox.Show(t3.ToString());*/
                    }

                }
                else
                {

                    // MessageBox.Show("?114514");
                    string t3 = "smjb\n";
                    for (int i = num_list_incomplete.Count; i < people_num_temp; i++)
                    {
                        int t = i - num_list_incomplete.Count;
                        result_list_t[t] = (int)result_list_t[t] + 1;
                        t3 += result_list_t[t].ToString() + "\n";
                        if ((int)result_list_t[t] >= num_list.Count) result_list_t[t] = 0;
                        result_list_cur[i] = num_list[(int)result_list_t[t]];
                        result_list_real[t] = num_list[(int)result_list_t[t]];
                        //MessageBox.Show("?");
                    }
                    //MessageBox.Show(t3.ToString());
                    if (v.x[11].value == "1")
                    {
                        result_list_cur.Sort();
                    }
                    /*string t2 = "abab";
                for (int i = 0; i < result_list_cur.Count; i++)
                    t2 += result_list_cur[i].ToString() + " ";
                MessageBox.Show(t2.ToString());*/
                    for (int i = 0; i < people_num_temp; i++)
                    {
                        string t = "";

                        TextBlock tb = (TextBlock)Result.Children[i];

                        //result_list_t.Add(num_t);
                        if (v.x[15].value == "1")
                        {
                            string num_string = "";
                            if ((int)result_list_cur[i] < 10)
                            {
                                if (v.x[23].value == "1") num_string += "0";
                            }
                            num_string += result_list_cur[i].ToString();
                            if ((int)result_list_cur[i] < 10)
                            {
                                if (v.x[23].value == "2") num_string += "  ";
                            }
                            if (v.x[23].value == "2") num_string += "  ";

                            t += num_string.ToString();
                        }
                        string name_string = "";
                        name_string += v.name[(int)result_list_cur[i]];
                        //MessageBox.Show(name_string.Length.ToString()); 
                        if (name_string.Length == 2)
                        {
                            name_string = name_string.Insert(1, "　");
                            //MessageBox.Show(name_string); 
                        }
                        if (v.x[14].value == "1") t += name_string;
                        tb.Text = t;

                    }
                }

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
                //MessageBox.Show(from.ToString()+" "+to.ToString());
                num_list.Add(i);

            }
            sync();
            /*string t3 = "";
            for (int i = 0; i < num_list.Count; i++)
                t3 += num_list[i].ToString() + " ";
            MessageBox.Show(t3.ToString());*/
        }
        private void sync()
        {
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
                    App.writemul();
                }
                else if (v.x[19].value == "3")
                {
                    v.saved_shared_num_list.Clear();
                    v.saved_shared_num_list.AddRange(num_list);
                    App.writeshared();
                }
            }
        }
        private void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void rlimitnumber(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }


        private void Button_Roll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            var viewModel = mainWindow.ViewModel;

            int People_Num_Value = 2, TextSize_Value = 100;
            if (People_Num.Value == null)
            {
                viewModel.ShowSnackbar(
                "错误", // 标题
                "啊呜，你还没填写抽号人数，搁这搁这呢？", // 内容
                ControlAppearance.Danger, // 样式
                new SymbolIcon(SymbolRegular.ErrorCircle24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                );

                //error_snackbar.Message = "啊呜，你还没填写抽号人数，搁这搁这呢？";
                //error_snackbar.Show();
                return;
            }
            if (TextSize.Value == null)
            {
                viewModel.ShowSnackbar(
                "错误", // 标题
                "啊呜，你还没填写字体大小，搁这搁这呢？", // 内容
                ControlAppearance.Danger, // 样式
                new SymbolIcon(SymbolRegular.ErrorCircle24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                );

                //error_snackbar.Message = "啊呜，你还没填写字体大小，搁这搁这呢？";
                //error_snackbar.Show();
                return;
            }

            People_Num_Value = (int)People_Num.Value;
            TextSize_Value = (int)TextSize.Value;
            /*if (int.TryParse(People_Num.Text, out People_Num_Value) == false || int.TryParse(TextSize.Text, out TextSize_Value) == false)
            {
                //invalid.Show();
                
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
            if ((int)People_Num_Value > int.Parse(v.x[21].value) - int.Parse(v.x[20].value) - v.stu_blank_num + 1)
            {
                viewModel.ShowSnackbar(
                "错误", // 标题
                "啊呜，抽号人数大于班级人数，搁这搁这呢？", // 内容
                ControlAppearance.Danger, // 样式
                new SymbolIcon(SymbolRegular.ErrorCircle24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                );


                //error_snackbar.Message = "啊呜，抽号人数大于班级人数，搁这搁这呢？";
                //error_snackbar.Show();
                return;
            }
            if (people_num_temp != People_Num_Value || textsize_temp != TextSize_Value)
            {
                people_num_temp = (int)People_Num_Value;
                textsize_temp = (int)TextSize_Value;


                Result.Children.Clear();
                for (int i = 1; i <= People_Num_Value; i++)
                {
                    TextBlock tb = new TextBlock();
                    tb.Name = $"tb{i}";
                    tb.Text = "--别紧张";
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.FontSize = TextSize_Value;
                    tb.Margin = new Thickness(0, 0, TextSize_Value / 2, 0);
                    Result.Children.Add(tb);
                }

            }

            if (v.x[12].value == "1" && rolling == true)
            {
                Button_Roll.Content = "开始";
                People_Num.IsEnabled = true;
                TextSize.IsEnabled = true;
                TextSizeSlider.IsEnabled = true;
            }
            else if (v.x[12].value == "0") Button_Roll.Content = "抽号";
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

            if (v.x[12].value == "1" && rolling != true)
            {
                Button_Roll.Content = "结束";
                People_Num.IsEnabled = false;
                TextSize.IsEnabled = false;
                TextSizeSlider.IsEnabled = false;
            }
            if (v.x[18].value == "1" && v.x[19].value == "3")
            {
                num_list.Clear();
                num_list.AddRange(v.shared_num_list);
            }
            if (v.x[18].value == "2" && v.x[19].value == "1")
            {
                num_list.Clear();
                num_list.AddRange(v.saved_mul_num_list);
            }
            if (v.x[18].value == "2" && v.x[19].value == "2")
            {
                num_list.Clear();
                num_list.AddRange(v.saved_mul_num_list);
            }
            if (v.x[18].value == "2" && v.x[19].value == "3")
            {
                num_list.Clear();
                num_list.AddRange(v.saved_shared_num_list);
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
            if (num_list.Count == people_num_temp && rolling != true)
            {
                /*if (v.achievement_x[5].value != 0)
                {
                    //warning_snackbar.Title = "你已解锁了该成就，该内容仅作为提示作用";
                    //warning_snackbar.Message = "成就「别想逃开哦」已经解锁过了！\n\n摇号，但是池子里的人数等于要摇的人数！\n可能是因为池子里的人数等于要摇的人数，也有可能是你开了不重复选项ww";
                }
                else
                {
                    //warning_snackbar.Title = "成就解锁";
                    //warning_snackbar.Message = "成就「别想逃开哦」已解锁！\n\n摇号，但是池子里的人数等于要摇的人数！！\n可能是因为池子里的人数等于要摇的人数，也有可能是你开了不重复选项ww";
                    DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                    long timeStamp = dto.ToUnixTimeSeconds();
                    v.achievement_x[5].value = (int)timeStamp;
                    App.write_achievement();
                }*/

                //warning_snackbar.Show();

                viewModel.ShowSnackbar(
                "提示", // 标题
                "这里不是出现 Bug 了，而是因为池子某些人必定被抽到\n可能是因为池子人数等于抽号人数，也有可能是你开了不重复选项ww", // 内容
                ControlAppearance.Caution, // 样式
                new SymbolIcon(SymbolRegular.Info24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(10) // 超时时间
                 );
            }
            if (num_list.Count < people_num_temp && rolling != true)
            {
                num_list_incomplete.Clear();
                num_list_incomplete.AddRange(num_list);
                num_list.Clear();
                gen_num_list();
                /*string t3= "smjb\n";
                for (int i = 0; i < num_list.Count; i++)
                    t3 += num_list[i].ToString() + " ";
                MessageBox.Show(t3.ToString());*/
                for (int i = 0; i < num_list_incomplete.Count; i++)
                {
                    num_list.Remove(num_list_incomplete[i]);
                    sync();
                }
                is_num_list_incomplete = true;
                if (v.x[12].value == "1")
                {
                    /*if (v.achievement_x[5].value != 0)
                    {
                        //warning_snackbar.Title = "你已解锁了该成就，该内容仅作为提示作用";
                        //warning_snackbar.Message = "成就「别想逃开哦」已经解锁过了！\n\n摇号，但是池子有某些人一定会被抽到！\n可能是因为设置的摇号人数与池子人数相等，也有可能是你开了不重复选项导致池子人数和摇号人数相等ww";
                    }
                    else
                    {
                        //warning_snackbar.Title = "成就解锁";
                        //warning_snackbar.Message = "成就「别想逃开哦」已解锁！\n\n摇号，但是池子有某些人一定会被抽到！\n可能是因为设置的摇号人数与池子人数相等，也有可能是你开了不重复选项导致池子人数和摇号人数相等ww";
                        DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
                        long timeStamp = dto.ToUnixTimeSeconds();
                        v.achievement_x[5].value = (int)timeStamp;
                        App.write_achievement();
                    }*/
                    //warning_snackbar.Show();

                    viewModel.ShowSnackbar(
                "提示", // 标题
                "这里不是出现 Bug 了，而是因为池子某些人必定被抽到\n可能是因为池子人数等于抽号人数，也有可能是你开了不重复选项ww", // 内容
                ControlAppearance.Caution, // 样式
                new SymbolIcon(SymbolRegular.Info24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(10) // 超时时间
                 );

                }
                string t = "?\n";
                for (int i = 0; i < num_list_incomplete.Count; i++)
                    t += num_list_incomplete[i].ToString() + " ";
                //MessageBox.Show(t.ToString());

                string t2 = "!\n";
                for (int i = 0; i < num_list.Count; i++)
                    t2 += num_list[i].ToString() + " ";
                //MessageBox.Show(t2.ToString());

            }
            if (v.x[12].value == "1" && Button_Roll.Content == "结束")
            {

                if (num_t == 0 || num_t < (int)num_list[0]) num_t = -1;

                timer.Start();
                rolling = true;
                reset = true;



            }
            else if (v.x[12].value == "1" && Button_Roll.Content == "开始")
            {
                timer.Stop();
                rolling = false;
                if (v.x[14].value == "0" && v.x[15].value == "0" && v.x[16].value == "0" && v.x[17].value == "0")
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
                if (v.x[14].value == "0" && v.x[15].value == "0" && (v.x[16].value == "1" || v.x[17].value == "1"))
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
                        //warning_snackbar.Show();

                    }*/

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
                if ((v.x[14].value == "1" || v.x[15].value == "1") && (v.x[16].value == "1" || v.x[17].value == "1"))
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
                if (is_num_list_incomplete == true)
                {
                    /*string t4 = "before aw";
                    for (int i = 0; i < num_list.Count; i++)
                        t4 += num_list[i].ToString() + " ";
                    MessageBox.Show(t4.ToString());*/
                    is_num_list_incomplete = false;
                    num_list.AddRange(num_list_incomplete);
                    num_list_incomplete.Clear();
                    sync();
                    /*string t3= "aw";
                    for (int i = 0; i < num_list.Count; i++)
                        t3 += num_list[i].ToString() + " ";
                    MessageBox.Show(t3.ToString());*/
                }
                string t = "";
                for (int i = 0; i < people_num_temp; i++)
                {
                    if (v.x[17].value == "1") t += result_list_cur[i].ToString() + "号";
                    if (v.x[16].value == "1" && v.name[(int)result_list_cur[i]] != "" && v.name[(int)result_list_cur[i]] != null) t += v.name[(int)result_list_cur[i]];

                    /*if (v.x[18].value == "1")
                    {
                        if (v.x[19].value == "0" || v.x[19].value == "2") num_list.RemoveAt(num_t);
                        else if (v.x[19].value == "3") num_list.RemoveAt(num_t);
                    }*/
                }

                if (v.x[18].value == "1")
                {
                    if (v.x[19].value == "1" || v.x[19].value == "2")
                    {
                        for (int j = 0; j < result_list_t.Count; j++)
                            //num_list.RemoveAt((int)result_list_t[j]);
                            num_list.Remove((int)result_list_real[j]);
                    }
                    else if (v.x[19].value == "3")
                    {
                        for (int j = 0; j < result_list_t.Count; j++)
                            //num_list.RemoveAt((int)result_list_t[j]);
                            num_list.Remove((int)result_list_real[j]);
                        v.shared_num_list.Clear();
                        v.shared_num_list.AddRange(num_list);
                    }
                }
                else if (v.x[18].value == "2")
                {
                    if (v.x[19].value == "1" || v.x[19].value == "2")
                    {
                        for (int j = 0; j < result_list_t.Count; j++)
                            //num_list.RemoveAt((int)result_list_t[j]);
                            num_list.Remove((int)result_list_real[j]);
                        v.saved_mul_num_list.Clear();
                        v.saved_mul_num_list.AddRange(num_list);
                        App.writemul();
                    }
                    else if (v.x[19].value == "3")
                    {
                        for (int j = 0; j < result_list_t.Count; j++)
                            //num_list.RemoveAt((int)result_list_t[j]);
                            num_list.Remove((int)result_list_real[j]);
                        v.saved_shared_num_list.Clear();
                        v.saved_shared_num_list.AddRange(num_list);
                        App.writeshared();
                    }
                }
                if (v.x[17].value == "1" || v.x[16].value == "1") voiceThreadStart(t);

                /*string t2 = "";
                for (int i = 0; i < num_list.Count; i++)
                    t2 += num_list[i].ToString() + " ";
                MessageBox.Show(t2.ToString());*/

            }

            else if (rolling == true)
            {
                timer.Stop();
                rolling = false;
                People_Num.IsEnabled = true;
                TextSize.IsEnabled = true;
                TextSizeSlider.IsEnabled = true;
            }
            else if (v.x[12].value == "0")
            {
                Random rand = new Random();

                /*int t = rand.Next(0, num_list.Count);
                num_cur = (int)num_list[t];

                string st = "";
                if (v.x[10].value == "1") st += num_cur.ToString() + "号";
                if (v.x[9].value == "1" && v.name[num_cur] != "" && v.name[num_cur] != null) st += v.name[num_cur];*/
                result_list_t.Clear();
                result_list_cur.Clear();
                result_list_real.Clear();
                ArrayList temp_num_list = new ArrayList();
                temp_num_list.AddRange(num_list);
                ArrayList temp_num_list_incomplete = new ArrayList();
                temp_num_list_incomplete.AddRange(num_list_incomplete);
                //temp_num_list = num_list;
                //MessageBox.Show(temp_num_list.Count.ToString());
                for (int i = 1; i <= num_list_incomplete.Count; i++)
                {
                    num_t = rand.Next(0, temp_num_list_incomplete.Count);
                    num_cur = (int)temp_num_list_incomplete[num_t];
                    result_list_cur.Add(num_cur);
                    temp_num_list_incomplete.RemoveAt(num_t);
                }
                for (int i = 1; i <= people_num_temp - num_list_incomplete.Count; i++)
                {
                    num_t = rand.Next(0, temp_num_list.Count);
                    num_cur = (int)temp_num_list[num_t];
                    result_list_cur.Add(num_cur);
                    result_list_real.Add(num_cur);
                    result_list_t.Add(num_list.IndexOf(num_cur));

                    /*string t3 = "";
                    for (int i2 = 0; i2 < num_list.Count; i2++)
                        t3 += num_list[i2].ToString() + " ";
                    t3 += "\n";
                    t3 += num_cur.ToString();
                    t3 += "\n";
                    t3 += num_list.IndexOf(num_cur).ToString();

                    MessageBox.Show(t3.ToString());*/

                    temp_num_list.RemoveAt(num_t);
                }
                if (v.x[11].value == "1")
                {
                    result_list_cur.Sort();
                }
                for (int i = 0; i < people_num_temp; i++)
                {
                    string st = "";



                    TextBlock tb = (TextBlock)Result.Children[i];

                    //result_list_t.Add(num_t);
                    if (v.x[15].value == "1")
                    {
                        string num_string = "";
                        if ((int)result_list_cur[i] < 10)
                        {
                            if (v.x[23].value == "1") num_string += "0";
                        }
                        num_string += result_list_cur[i].ToString();
                        if ((int)result_list_cur[i] < 10)
                        {
                            if (v.x[23].value == "2") num_string += "  ";
                        }
                        if (v.x[23].value == "2") num_string += "  ";

                        st += num_string.ToString();
                    }
                    //if ((int)result_list_cur[i] < 10) st += " ";
                    string name_string = "";
                    name_string += v.name[(int)result_list_cur[i]];
                    //MessageBox.Show(name_string.Length.ToString()); 
                    if (name_string.Length == 2)
                    {
                        name_string = name_string.Insert(1, "　");
                        //MessageBox.Show(name_string); 
                    }
                    if (v.x[14].value == "1") st += name_string;
                    tb.Text = st;


                    //tb1.Text = "?";
                }
                string t = "";
                for (int i = 0; i < people_num_temp; i++)
                {
                    if (v.x[17].value == "1") t += result_list_cur[i].ToString() + "号";
                    if (v.x[16].value == "1" && v.name[(int)result_list_cur[i]] != "" && v.name[(int)result_list_cur[i]] != null) t += v.name[(int)result_list_cur[i]];


                }
                if (v.x[10].value == "1" || v.x[9].value == "1") voiceThreadStart(t);
                if (v.x[14].value == "0" && v.x[15].value == "0" && v.x[16].value == "0" && v.x[17].value == "0")
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
                if (v.x[14].value == "0" && v.x[15].value == "0" && (v.x[16].value == "1" || v.x[17].value == "1"))
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
                if ((v.x[14].value == "1" || v.x[15].value == "1") && (v.x[16].value == "1" || v.x[17].value == "1"))
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
                    }*/
                    //warning_snackbar.Show();
                }
                if (is_num_list_incomplete == true)
                {
                    is_num_list_incomplete = false;
                    num_list.AddRange(num_list_incomplete);
                    num_list_incomplete.Clear();
                }
                if (v.x[18].value == "1")
                {
                    if (v.x[19].value == "1" || v.x[19].value == "2")
                    {
                        for (int j = 0; j < result_list_t.Count; j++)
                            //num_list.RemoveAt((int)result_list_t[j]);
                            num_list.Remove((int)result_list_real[j]);
                    }
                    else if (v.x[19].value == "3")
                    {
                        for (int j = 0; j < result_list_t.Count; j++)
                            //num_list.RemoveAt((int)result_list_t[j]);
                            num_list.Remove((int)result_list_real[j]);
                        v.shared_num_list.Clear();
                        v.shared_num_list.AddRange(num_list);
                    }
                }
                else if (v.x[18].value == "2")
                {
                    if (v.x[19].value == "1" || v.x[19].value == "2")
                    {
                        for (int j = 0; j < result_list_t.Count; j++)
                            //num_list.RemoveAt((int)result_list_t[j]);
                            num_list.Remove((int)result_list_real[j]);
                        v.saved_mul_num_list.Clear();
                        v.saved_mul_num_list.AddRange(num_list);
                        App.writemul();
                    }
                    else if (v.x[19].value == "3")
                    {
                        for (int j = 0; j < result_list_t.Count; j++)
                            //num_list.RemoveAt((int)result_list_t[j]);
                            num_list.Remove((int)result_list_real[j]);
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



        private void TextSize_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (TextSize.Value == null)
            {
                TextSize.Value = 100;
                    }
            TextSizeSlider.Value = (int)TextSize.Value;

            for (int i = 0; i < people_num_temp; i++)
            {
                TextBlock tb = (TextBlock)Result.Children[i];
                tb.FontSize = (int)TextSize.Value;
            }
        }

        private void TextSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TextSize == null) return;

            TextSize.Value = (int)TextSizeSlider.Value;

            for (int i = 0; i < people_num_temp; i++)
            {
                TextBlock tb = (TextBlock)Result.Children[i];
                tb.FontSize = (int)TextSize.Value;
            }

        }
    }
}