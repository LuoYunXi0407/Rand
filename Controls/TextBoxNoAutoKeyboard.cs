using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using Wpf.Ui.Controls;


namespace rand7.Controls
{
    public class TextBoxNoAutoKeyboard : TextBox
    {

        public TextBoxNoAutoKeyboard()
        {
            this.Style = FindResource(typeof(Wpf.Ui.Controls.TextBox)) as Style;

        }
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            // 返回基类 AutomationPeer 而非 TextBox 的默认实现
            return new FrameworkElementAutomationPeer(this);
        }
    }
    public class NumberBoxNoAutoKeyboard : NumberBox
    {
        public NumberBoxNoAutoKeyboard()
        {
            this.Style = FindResource(typeof(Wpf.Ui.Controls.NumberBox)) as Style;

        }
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new FrameworkElementAutomationPeer(this);
        }
    }
}
