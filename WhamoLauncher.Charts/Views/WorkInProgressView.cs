using System;
using System.Windows.Forms;
using WhamoLauncher.Charts.ViewControllers;

namespace WhamoLauncher.Charts.Views
{
    internal partial class WorkInProgressView : ViewBase
    {
        public WorkInProgressView(IViewController viewController)
            :base(viewController)
        {
            InitializeComponent();
            SetControlTexts();
        }

        protected override void SetControlTexts()
        {
            base.SetControlTexts();
            waitLabel.WaitText = Strings.WaitChartsMessage;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            waitLabel.StartAnimation();
        }
    }
}
