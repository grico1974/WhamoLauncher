using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WhamoLauncher.Charts.Views
{
    internal partial class ChartSetup : UserControl
    { 
        public event EventHandler AddPrimarySeriesClicked, AddSecondarySeriesClicked, RemovePrimarySeriesClicked, RemoveSecondarySeriesClicked;

        internal ChartSetup()
        {
            InitializeComponent();
            SetUpTexts();
            refreshButtons();
        }

        public object DataSource
        {
            get { return allSeriesBox.DataSource; }
            set
            {
                if (value == allSeriesBox.DataSource)
                {
                    return;
                }

                allSeriesBox.DataSource = value;
                primarySeriesBox.DataSource = new BindingList<object>();
                secondarySeriesBox.DataSource = new BindingList<object>();
                refreshButtons();
            }
        }

        public IList PrimarySeries => primarySeriesBox.DataSource as IList;
        public IList SecondarySeries => secondarySeriesBox.DataSource as IList;

        public IEnumerable<object> SelectedSeries
        {
            get
            {
                foreach (var item in allSeriesBox.SelectedItems)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<object> SelectedPrimarySeries
        {
            get
            {
                foreach (var item in primarySeriesBox.SelectedItems)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<object> SelectedSecondarySeries
        {
            get
            {
                foreach (var item in secondarySeriesBox.SelectedItems)
                {
                    yield return item;
                }
            }
        }

        private void addPrimarySeries_Click(object sender, EventArgs e)
        {
            if (AddPrimarySeriesClicked != null)
            {
                AddPrimarySeriesClicked(this, e);
            }

            refreshButtons();
        }

        private void removePrimarySeries_Click(object sender, EventArgs e)
        {
            if (RemovePrimarySeriesClicked != null)
            {
                RemovePrimarySeriesClicked(this, e);
            }

            refreshButtons();
        }

        private void addSecondarySeries_Click(object sender, EventArgs e)
        {
            if (AddSecondarySeriesClicked != null)
            {
                AddSecondarySeriesClicked(this, e);
            }

            refreshButtons();
        }

        private void removeSecondarySeries_Click(object sender, EventArgs e)
        {
            if (RemoveSecondarySeriesClicked != null)
            {
                RemoveSecondarySeriesClicked(this, e);
            }

            refreshButtons();
        }

        public override void Refresh()
        {
            base.Refresh();
            refreshButtons();
        }

        private void refreshButtons()
        {
            if (DataSource == null)
            {
                addPrimarySeries.Enabled = false;
                addSecondarySeries.Enabled = false;
                removePrimarySeries.Enabled = false;
                removeSecondarySeries.Enabled = false;
            }
            else
            {
                var allSeriesCount = allSeriesBox.Items.Count;
                addPrimarySeries.Enabled = allSeriesCount != 0;
                addSecondarySeries.Enabled = allSeriesCount != 0;
                removePrimarySeries.Enabled = primarySeriesBox.Items.Count != 0;
                removeSecondarySeries.Enabled = secondarySeriesBox.Items.Count != 0;
            }
        }

        private void SetUpTexts()
        {
            allSeriesLabel.Text = Strings.AllSeries;
            primarySeriesLabel.Text = Strings.PrimarySeries;
            secondarySeriesLabel.Text = Strings.SecondarySeries;
        }
    }
}
