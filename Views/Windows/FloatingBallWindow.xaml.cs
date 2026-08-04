using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace rand7.Views.Windows
{
    /// <summary>
    /// FloatingBallWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingBallWindow : Window
    {
        private Point _mouseDownPosition = new Point();


        private const double HiddenOffset = 25; // 贴边隐藏的偏移量（比如 25 像素）

        private const int HWND_TOPMOST = -1;
        private const int HWND_NOTOPMOST = -2;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_SHOWWINDOW = 0x0040;

        const int WM_SHOWME = 0x8001; // WM_APP + 1

        private static bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam)
        {
            // 获取窗口文本
            string windowText = GetWindowText(hwnd);

            if (windowText == null) return true;
            // 检查是否是目标窗口
            if (windowText.Contains("抽号器"))
            {
                // 获取窗口所属进程ID
                //uint processId;
                //GetWindowThreadProcessId(hwnd, out processId);

                // 获取进程对象
                //Process process = Process.GetProcessById((int)processId);

                // 确认进程名称以进一步验证
                //if (process.MainWindowTitle.Contains("抽号器"))
                {
                    SendMessage(hwnd, WM_SHOWME, IntPtr.Zero, IntPtr.Zero);

                    return false;
                }
            }

            // 继续枚举下一个窗口
            return true;
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;    // 在任务栏中不显示窗口
        private const int WS_EX_NOACTIVATE = 0x08000000;    // 确保不会抢占焦点

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(WndEnumProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(
        IntPtr hWnd,
        uint Msg,
        IntPtr wParam,
        IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private delegate bool WndEnumProc(IntPtr hWnd, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }


        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

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

        public FloatingBallWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) => KeepOnTop();
            LocationChanged += Window_LocationChanged;

            //this.Loaded += FloatingBallWindow _Loaded;
            //this.LocationChanged += FloatingBallWindow _LocationChanged;
        }

        private void KeepOnTop()
        {
            IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE | SWP_SHOWWINDOW);

            int extendedStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, extendedStyle | WS_EX_TOOLWINDOW | WS_EX_NOACTIVATE);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            var window = this;

            var workArea = SystemParameters.WorkArea;

            double left = window.Left;
            double top = window.Top;

            if (left < workArea.Left)
                left = workArea.Left;

            if (top < workArea.Top)
                top = workArea.Top;

            if (left + window.ActualWidth > workArea.Right)
                left = workArea.Right - window.ActualWidth;

            if (top + window.ActualHeight > workArea.Bottom)
                top = workArea.Bottom - window.ActualHeight;

            window.Left = left;
            window.Top = top;

            //AnimateBall(left, top);
        }

        /*private void FloatingBallWindow _Loaded(object sender, RoutedEventArgs e)
        {
            StickToEdgeState(true); // 窗口加载时检查是否需要贴边
        }

        private void FloatingBallWindow _LocationChanged(object sender, EventArgs e)
        {
            StickToEdgeState(true); // 移动窗口时检查是否需要贴边
        }*/
        private DispatcherTimer animationTimer;
        private double targetLeft, targetTop;
        private bool isDragging = false; // 是否正在拖动
        private Point mouseOffset; // 记录鼠标按下时的位置
        private void AnimateBall(double toLeft, double toTop)
        {
            // 如果正在拖动，则不执行动画
            if (isDragging) return;

            targetLeft = toLeft;
            targetTop = toTop;

            if (animationTimer == null)
            {
                animationTimer = new DispatcherTimer();
                animationTimer.Interval = TimeSpan.FromMilliseconds(10);
                animationTimer.Tick += (s, e) =>
                {
                    if (isDragging) // 防止拖动时动画冲突
                    {
                        animationTimer.Stop();
                        return;
                    }

                    double dx = (targetLeft - this.Left) * 0.2;
                    double dy = (targetTop - this.Top) * 0.2;

                    if (Math.Abs(dx) < 0.5 && Math.Abs(dy) < 0.5)
                    {
                        this.Left = targetLeft;
                        this.Top = targetTop;
                        animationTimer.Stop();
                    }
                    else
                    {
                        this.Left += dx;
                        this.Top += dy;
                    }
                };
            }

            animationTimer.Start();
        }
        private CancellationTokenSource _hideCts;

        private async void StickToEdgeState(bool hide)
        {
            // 不贴边了捏，要不然触屏塞进去就拿不出来了
            return;

            if (!hide)
            {
                // 取消等待中的隐藏
                _hideCts?.Cancel();
                _hideCts = null;

                StickToEdge(false);
            }
            else
            {
                // 取消之前的等待，重新开始计时
                _hideCts?.Cancel();

                var cts = new CancellationTokenSource();
                _hideCts = cts;

                try
                {
                    await Task.Delay(5000, cts.Token);

                    // 如果5秒内没有被取消，执行隐藏
                    if (_hideCts == cts)
                    {
                        _hideCts = null;

                        StickToEdge(true);
                    }
                }
                catch (TaskCanceledException)
                {
                    // 被取消，不需要处理
                }
            }
        }
        private void StickToEdge(bool hide)
        {


            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            double left = this.Left;
            double top = this.Top;
            double ballWidth = this.Width;
            double ballHeight = this.Height;

            /*
            if (hide == false)
            {
                // 鼠标进入时，完全显示悬浮球
                if (left < 0)
                {
                    this.Left = 0;
                }
                else if (left + ballWidth > screenWidth)
                {
                    this.Left = screenWidth - ballWidth;
                }
                if (top < 0)
                {
                    this.Top = 0;
                }
                else if (top + ballHeight > screenHeight)
                {
                    this.Top = screenHeight - ballHeight;
                }
            }
            else
            {
                // 靠边隐藏逻辑
                if (left < HiddenOffset)
                {
                    this.Left = -ballWidth / 2;  // 左边隐藏一半
                }
                else if (left + ballWidth > screenWidth - HiddenOffset)
                {
                    this.Left = screenWidth - ballWidth / 2;  // 右边隐藏一半
                }
                if (top < HiddenOffset)
                {
                    this.Top = -ballHeight / 2;  // 上边隐藏一半
                }
                else if (top + ballHeight > screenHeight - HiddenOffset)
                {
                    this.Top = screenHeight - ballHeight / 2;  // 下边隐藏一半
                }
            }
            */

            if (hide == false)
            {
                // 鼠标进入时，完全显示悬浮球
                double newLeft = left < 0 ? 0 : (left + ballWidth > screenWidth ? screenWidth - ballWidth : left);
                double newTop = top < 0 ? 0 : (top + ballHeight > screenHeight ? screenHeight - ballHeight : top);

                AnimateBall(newLeft, newTop);
            }
            else
            {
                // 靠边隐藏逻辑
                double newLeft = left < HiddenOffset ? -ballWidth / 2 :
                                 (left + ballWidth > screenWidth - HiddenOffset ? screenWidth - ballWidth / 2 : left);
                double newTop = top < HiddenOffset ? -ballHeight / 2 :
                                (top + ballHeight > screenHeight - HiddenOffset ? screenHeight - ballHeight / 2 : top);

                AnimateBall(newLeft, newTop);
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            StickToEdgeState(false);

            var anim = new DoubleAnimation(1.0, TimeSpan.FromMilliseconds(300));
            this.BeginAnimation(Window.OpacityProperty, anim);
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            StickToEdgeState(true); // 鼠标离开时进入贴边状态

            var anim = new DoubleAnimation(0.5, TimeSpan.FromMilliseconds(300));
            this.BeginAnimation(Window.OpacityProperty, anim);
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true; // 进入拖动模式

            _mouseDownPosition = GetCursorPosition(); // 获取相对于屏幕的位置

            StickToEdgeState(false); // 鼠标按下时恢复状态

            this.Opacity = 1.0;

            try
            {
                this.DragMove();
            }
            catch (InvalidOperationException)
            {
                // 右键拖动会崩溃
            }
        }
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false; // 结束拖动



            // 获取松开时的鼠标位置，相对于屏幕
            Point mouseUpPosition = GetCursorPosition();
            //MessageBox.Show(_mouseDownPosition.X.ToString()+","+ _mouseDownPosition.Y.ToString() + " " + mouseUpPosition.X.ToString()+","+ mouseUpPosition.Y.ToString());
            // 判断按下和松开位置的差距，认为是点击时差距小于阈值
            if (Math.Abs(_mouseDownPosition.X - mouseUpPosition.X) < 5 &&
                Math.Abs(_mouseDownPosition.Y - mouseUpPosition.Y) < 5)
            {
                EnumWindows(new WndEnumProc(EnumWindowsProc), IntPtr.Zero);

            }


            StickToEdgeState(true); // 拖动结束后，检测贴边
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal; // 阻止最大化
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // 取消关闭操作
        }
    }

}