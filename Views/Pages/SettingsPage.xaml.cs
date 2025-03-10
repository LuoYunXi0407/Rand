using rand7.ViewModels.Pages;
using rand7.ViewModels.Pages.Settings;
using rand7.ViewModels.Windows;
using rand7.Views.Pages.Settings;
using rand7.Views.Windows;
using System.Diagnostics;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;
using Wpf.Ui.Abstractions.Controls;

namespace rand7.Views.Pages
{

    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {

        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            

            InitializeComponent();

            //Numfrom.Text = v.x[20].value;
           // Numto.Text = v.x[21].value;

            Numfrom.Value = int.Parse(v.x[20].value);
            Numto.Value = int.Parse(v.x[21].value);

            if (v.x[22].value == "1")
            {

                Set_Range_Expander.IsExpanded = true;
                Set_Range_Menu.IsEnabled = true;
                App.writeini();
            }
            else
            {
                Set_Range.IsChecked = true;
            }
            if (v.x[22].value == "0")
            {
                if (v.stu_auto_num != 0)
                {
                    v.x[22].value = "-1";
                    v.x[21].value = v.stu_auto_num.ToString();
                    //Numto.Text = v.stu_auto_num.ToString();
                    Numto.Value += v.stu_auto_num;
                    App.writeini();
                }
            }

            Student_Avoid_Duplication_Setting.SelectedIndex = int.Parse(v.x[18].value);
            Student_Avoid_Duplication_Mode.SelectedIndex = int.Parse(v.x[19].value);
            if (Student_Avoid_Duplication_Setting.SelectedIndex != 0)
            {
                Student_Avoid_Duplication_Mode_Menu.IsEnabled = true;
                Student_Avoid_Duplication_Setting_Expander.IsExpanded = true;
            }
        }

       

        private void Button_Set_Name_Click(object sender, RoutedEventArgs e)
        {
            Set_Name_flyout.Show();

            //Application.Current.Shutdown();

            //var _singleStudentViewModel = new SingleStudentViewModel();
            //var _singleStudentPage = new SingleStudentPage(_singleStudentViewModel);
            //_singleStudentPage.Single_Student_Roll_Expander.IsExpanded = false;




        }

        private void Set_Defualt_Click(object sender, RoutedEventArgs e)
        {
            Set_Defualt_flyout.Show();
        }

        private void Set_Range_Checked(object sender, RoutedEventArgs e)
        {
            if (v.stu_auto_num != 0)
            {
                v.x[22].value = "-1";
                v.x[21].value = v.stu_auto_num.ToString();
                //Numto.Text = v.stu_auto_num.ToString();
                Numto.Value = v.stu_auto_num;

            }
            else
            {
                v.x[22].value = "0";
            }
            Set_Range_Expander.IsExpanded = false;
            Set_Range_Menu.IsEnabled = false;
            App.writeini();
        }

        private void Set_Range_Unchecked(object sender, RoutedEventArgs e)
        {
            v.x[22].value = "1";
            Set_Range_Expander.IsExpanded = true;
            Set_Range_Menu.IsEnabled = true;
            App.writeini();
            
        }

        private void Button_Auto_Range_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            var viewModel = mainWindow.ViewModel;
            if (v.stu_auto_num != 0)
            {
                viewModel.ShowSnackbar(
                "成功", // 标题
                "自动识别成功！请按保存按钮保存！", // 内容
                ControlAppearance.Success, // 样式
                new SymbolIcon(SymbolRegular.Checkmark24)
                {
                    FontSize = 32
                },
                 // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                );
                //success_snackbar.Message = "自动识别成功！请按保存按钮保存！";
                //success_snackbar.Show();
                v.x[21].value = v.stu_auto_num.ToString();
                //Numto.Text = v.stu_auto_num.ToString();
                Numto.Value = v.stu_auto_num;
                App.writeini();
            }
            else
            {
                viewModel.ShowSnackbar(
                "错误", // 标题
                "自动识别失败，未导入姓名或导入后没有重启抽号器！", // 内容
                ControlAppearance.Danger, // 样式
                new SymbolIcon(SymbolRegular.ErrorCircle24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                );
                //error_snackbar.Message = "自动识别失败，未导入姓名或导入后没有重启抽号器！";
                //error_snackbar.Show();
            }
        }

        private void Button_Save_Range_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            var viewModel = mainWindow.ViewModel;
            int from_t = 0, to_t = 0;
            if (Numfrom.Value == null|| Numto.Value == null)
            {

                //var mainWindow = Application.Current.MainWindow as MainWindow;
                //var viewModel = mainWindow.ViewModel;

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

                

                viewModel.ShowSnackbar(
                "错误", // 标题
                "你居然能往数字框里塞其他东西？", // 内容
                ControlAppearance.Danger, // 样式
                new SymbolIcon(SymbolRegular.ErrorCircle24)
                {
                    FontSize = 32
                }, // 图标
                TimeSpan.FromSeconds(5) // 超时时间
                );

                //invalid.Show();
                warning_info.Message = "成就「来自世界之外的禁忌知识」已解锁！\n\n就你这小子往数字框里面塞其他东西是吧？";
                warning_info.IsOpen = true;
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
            }*/


            //success_snackbar.Message = "成功保存座号范围\n若开启了不重复设置，已将其结果清空。";
            //success_snackbar.Show();



            viewModel.ShowSnackbar(
            "成功", // 标题
            "成功保存座号范围\n若开启了不重复选项，已将其结果清空。", // 内容
            ControlAppearance.Success, // 样式
            new SymbolIcon(SymbolRegular.Checkmark24)
            {
                FontSize = 32
            }, // 图标
            TimeSpan.FromSeconds(5) // 超时时间
            );
            v.x[22].value = "1";
            v.x[20].value = from_t.ToString();
            v.x[21].value = to_t.ToString();
            v.saved_mul_num_list.Clear();
            v.saved_sig_num_list.Clear();
            v.saved_shared_num_list.Clear();
            App.writesig();
            App.writemul();
            App.writeshared();

             
            App.writeini();
        }

        private void Button_Set_Name_Confirm_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Notepad.exe", "Name.txt");
            Application.Current.Shutdown();
        }

        private void Button_Set_Defualt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Set_Defualt_flyout.Hide();
        }
        private void Button_Set_Defualt_Confirm_Click(object sender, RoutedEventArgs e)
        {
            App.resetini();
            App.writeini();

            v.saved_sig_num_list.Clear();
            v.saved_mul_num_list.Clear();
            v.saved_shared_num_list.Clear();
            App.writesig();
            App.writemul();
            App.writeshared();

            Application.Current.Shutdown();

        }

        private void Student_Avoid_Duplication_Mode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Name_ini == null) return;
            if (Student_Avoid_Duplication_Mode == null) return;

            v.saved_sig_num_list.Clear();
            v.saved_mul_num_list.Clear();
            v.saved_shared_num_list.Clear();
            App.writesig();
            App.writemul();
            App.writeshared();

            v.x[19].value = Student_Avoid_Duplication_Mode.SelectedIndex.ToString();
            //MessageBox.Show("?2");
            App.writeini();
        }

        private void Student_Avoid_Duplication_Setting_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Student_Avoid_Duplication_Mode_Menu == null) return;
            if (Student_Avoid_Duplication_Setting.SelectedIndex != 0)
            {
                Student_Avoid_Duplication_Mode_Menu.IsEnabled = true;
                Student_Avoid_Duplication_Setting_Expander.IsExpanded = true;
            }
            else if (Student_Avoid_Duplication_Setting.SelectedIndex == 0)
            {
                Student_Avoid_Duplication_Mode_Menu.IsEnabled = false;
                Student_Avoid_Duplication_Setting_Expander.IsExpanded = false;
            }
            v.saved_sig_num_list.Clear();
            v.saved_mul_num_list.Clear();
            v.saved_shared_num_list.Clear();
            App.writesig();
            App.writemul();
            App.writeshared();



            v.x[18].value = Student_Avoid_Duplication_Setting.SelectedIndex.ToString();
            //MessageBox.Show("?1");
            App.writeini();
        }
    }
}
