using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WhamoLauncher.Charts.ViewControllers;

namespace WhamoLauncher.Charts.Views
{
    internal sealed partial class CustomChartsView : ViewBase
    {
        public CustomChartsView(IViewController viewController)
            : base(viewController)
        {
            InitializeComponent();
            SetControlTexts();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<TabPage> Tabs
        {
            get
            {
                foreach (var tab in tabs.TabPages.Cast<TabPage>())
                {
                    yield return tab;
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TabPage SelectedTab { get { return tabs.SelectedTab; } }

        public TabPage AddNewTab()
        {
            var newTab = new TabPage(string.Format(Strings.ExcelChartSheetName, tabs.TabCount + 1));
            var chartSetup = new ChartSetup();
            newTab.Controls.Add(chartSetup);
            newTab.Location = new System.Drawing.Point(4, 22);
            newTab.Padding = new System.Windows.Forms.Padding(3);
            newTab.Size = new System.Drawing.Size(692, 403);
            newTab.UseVisualStyleBackColor = true;
            chartSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            chartSetup.AddPrimarySeriesClicked += series_AddPrimarySeriesClicked;
            chartSetup.AddSecondarySeriesClicked += series_AddSecondarySeriesClicked;
            chartSetup.RemovePrimarySeriesClicked += series_RemovePrimarySeriesClicked;
            chartSetup.RemoveSecondarySeriesClicked += series_RemoveSecondarySeriesClicked;
            tabs.TabPages.Add(newTab);
            tabs.SelectedTab = newTab;
            removeTab.Enabled = true;
            return newTab;
        }

        public void RemoveTab(TabPage tab)
        {
            Debug.Assert(tab != null);
            tabs.TabPages.Remove(tab);

            if (tabs.TabCount == 0)
            {
                removeTab.Enabled = false;
            }
            else
            {
                tabs.SelectedTab = tabs.TabPages[tabs.TabCount - 1];
            }
        }

        public void ClearTabs() => tabs.TabPages.Clear();
        private void CustomSeriesConfiguration_Load(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.AddTab);
        private void accept_Click(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.Accept);
        private void cancel_Click(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.Cancel);
        private void loadTemplate_Click(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button as Button, Command.Load);
        private void series_RemoveSecondarySeriesClicked(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.RemoveSecondary);
        private void series_RemovePrimarySeriesClicked(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.RemovePrimary);
        private void series_AddSecondarySeriesClicked(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.AddSecondary);
        private void series_AddPrimarySeriesClicked(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.AddPrimary);
        private void addTab_Click(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.AddTab);
        private void removeTab_Click(object sender, EventArgs e) => ViewController.ProcessCommand(sender as Button, Command.RemoveTab);

        protected override void SetControlTexts()
        {
            base.SetControlTexts();
            Text = Strings.CustomCharts;
            accept.Text = Strings.Accept;
            cancel.Text = Strings.Cancel;
            addTab.Text = Strings.AddChart;
            removeTab.Text = Strings.RemoveChart;
            loadTemplate.Text = Strings.LoadTemplate;
        }
    }
}

