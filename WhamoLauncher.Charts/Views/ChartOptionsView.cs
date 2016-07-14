using System.Windows.Forms;
using WhamoLauncher.Charts.ViewControllers;

namespace WhamoLauncher.Charts.Views
{
    internal sealed partial class ChartOptionsView : ViewBase
    {
        public ChartOptionsView(IViewController viewController)
            :base(viewController)
        {
            InitializeComponent();
            SetControlTexts();
        }

        protected override void SetControlTexts()
        {
            base.SetControlTexts();
            defaultCharts.Text = Strings.CreateDefaultCharts;
            customCharts.Text = Strings.CreateCustomCharts;
            noCharts.Text = Strings.CreateNoCharts;
            Text = Strings.ChartOptionsCaption;
        }

        private void ChartGenerationOptions_Load(object sender, System.EventArgs e) =>  Activate();

        private void defaultCharts_Click(object sender, System.EventArgs e) =>
            ViewController.ProcessCommand(sender as Button, Command.Default);

        private void customCharts_Click(object sender, System.EventArgs e) =>
            ViewController.ProcessCommand(sender as Button, Command.Custom);

        private void noCharts_Click(object sender, System.EventArgs e) =>
            ViewController.ProcessCommand(sender as Button, Command.None);
    }
}
