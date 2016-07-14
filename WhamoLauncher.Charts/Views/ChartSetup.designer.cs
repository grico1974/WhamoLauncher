namespace WhamoLauncher.Charts.Views
{
    partial class ChartSetup
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.allSeriesLabel = new System.Windows.Forms.Label();
            this.allSeriesBox = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.primarySeriesLabel = new System.Windows.Forms.Label();
            this.primarySeriesBox = new System.Windows.Forms.ListBox();
            this.removePrimarySeries = new System.Windows.Forms.Button();
            this.addPrimarySeries = new System.Windows.Forms.Button();
            this.secondarySeriesLabel = new System.Windows.Forms.Label();
            this.secondarySeriesBox = new System.Windows.Forms.ListBox();
            this.addSecondarySeries = new System.Windows.Forms.Button();
            this.removeSecondarySeries = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.allSeriesLabel);
            this.splitContainer1.Panel1.Controls.Add(this.allSeriesBox);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 250;
            this.splitContainer1.Size = new System.Drawing.Size(671, 406);
            this.splitContainer1.SplitterDistance = 407;
            this.splitContainer1.TabIndex = 0;
            // 
            // allSeriesLabel
            // 
            this.allSeriesLabel.AutoSize = true;
            this.allSeriesLabel.Location = new System.Drawing.Point(3, 14);
            this.allSeriesLabel.Name = "allSeriesLabel";
            this.allSeriesLabel.Size = new System.Drawing.Size(72, 13);
            this.allSeriesLabel.TabIndex = 1;
            this.allSeriesLabel.Text = "allSeriesLabel";
            // 
            // allSeriesBox
            // 
            this.allSeriesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.allSeriesBox.FormattingEnabled = true;
            this.allSeriesBox.Location = new System.Drawing.Point(3, 35);
            this.allSeriesBox.Name = "allSeriesBox";
            this.allSeriesBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.allSeriesBox.Size = new System.Drawing.Size(399, 355);
            this.allSeriesBox.Sorted = true;
            this.allSeriesBox.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.primarySeriesLabel);
            this.splitContainer2.Panel1.Controls.Add(this.primarySeriesBox);
            this.splitContainer2.Panel1.Controls.Add(this.removePrimarySeries);
            this.splitContainer2.Panel1.Controls.Add(this.addPrimarySeries);
            this.splitContainer2.Panel1MinSize = 100;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.secondarySeriesLabel);
            this.splitContainer2.Panel2.Controls.Add(this.secondarySeriesBox);
            this.splitContainer2.Panel2.Controls.Add(this.addSecondarySeries);
            this.splitContainer2.Panel2.Controls.Add(this.removeSecondarySeries);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(260, 406);
            this.splitContainer2.SplitterDistance = 194;
            this.splitContainer2.TabIndex = 0;
            // 
            // primarySeriesLabel
            // 
            this.primarySeriesLabel.AutoSize = true;
            this.primarySeriesLabel.Location = new System.Drawing.Point(63, 14);
            this.primarySeriesLabel.Name = "primarySeriesLabel";
            this.primarySeriesLabel.Size = new System.Drawing.Size(95, 13);
            this.primarySeriesLabel.TabIndex = 3;
            this.primarySeriesLabel.Text = "primarySeriesLabel";
            // 
            // primarySeriesBox
            // 
            this.primarySeriesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primarySeriesBox.FormattingEnabled = true;
            this.primarySeriesBox.Location = new System.Drawing.Point(66, 38);
            this.primarySeriesBox.Name = "primarySeriesBox";
            this.primarySeriesBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.primarySeriesBox.Size = new System.Drawing.Size(189, 134);
            this.primarySeriesBox.Sorted = true;
            this.primarySeriesBox.TabIndex = 3;
            // 
            // removePrimarySeries
            // 
            this.removePrimarySeries.Location = new System.Drawing.Point(13, 82);
            this.removePrimarySeries.Name = "removePrimarySeries";
            this.removePrimarySeries.Size = new System.Drawing.Size(47, 38);
            this.removePrimarySeries.TabIndex = 2;
            this.removePrimarySeries.Text = "<<";
            this.removePrimarySeries.UseVisualStyleBackColor = true;
            this.removePrimarySeries.Click += new System.EventHandler(this.removePrimarySeries_Click);
            // 
            // addPrimarySeries
            // 
            this.addPrimarySeries.Location = new System.Drawing.Point(13, 38);
            this.addPrimarySeries.Name = "addPrimarySeries";
            this.addPrimarySeries.Size = new System.Drawing.Size(47, 38);
            this.addPrimarySeries.TabIndex = 1;
            this.addPrimarySeries.Text = ">>";
            this.addPrimarySeries.UseVisualStyleBackColor = true;
            this.addPrimarySeries.Click += new System.EventHandler(this.addPrimarySeries_Click);
            // 
            // secondarySeriesLabel
            // 
            this.secondarySeriesLabel.AutoSize = true;
            this.secondarySeriesLabel.Location = new System.Drawing.Point(63, 12);
            this.secondarySeriesLabel.Name = "secondarySeriesLabel";
            this.secondarySeriesLabel.Size = new System.Drawing.Size(111, 13);
            this.secondarySeriesLabel.TabIndex = 5;
            this.secondarySeriesLabel.Text = "secondarySeriesLabel";
            // 
            // secondarySeriesBox
            // 
            this.secondarySeriesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secondarySeriesBox.FormattingEnabled = true;
            this.secondarySeriesBox.Location = new System.Drawing.Point(66, 38);
            this.secondarySeriesBox.Name = "secondarySeriesBox";
            this.secondarySeriesBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.secondarySeriesBox.Size = new System.Drawing.Size(189, 147);
            this.secondarySeriesBox.Sorted = true;
            this.secondarySeriesBox.TabIndex = 6;
            // 
            // addSecondarySeries
            // 
            this.addSecondarySeries.Location = new System.Drawing.Point(13, 38);
            this.addSecondarySeries.Name = "addSecondarySeries";
            this.addSecondarySeries.Size = new System.Drawing.Size(47, 38);
            this.addSecondarySeries.TabIndex = 4;
            this.addSecondarySeries.Text = ">>";
            this.addSecondarySeries.UseVisualStyleBackColor = true;
            this.addSecondarySeries.Click += new System.EventHandler(this.addSecondarySeries_Click);
            // 
            // removeSecondarySeries
            // 
            this.removeSecondarySeries.Location = new System.Drawing.Point(13, 82);
            this.removeSecondarySeries.Name = "removeSecondarySeries";
            this.removeSecondarySeries.Size = new System.Drawing.Size(47, 38);
            this.removeSecondarySeries.TabIndex = 5;
            this.removeSecondarySeries.Text = "<<";
            this.removeSecondarySeries.UseVisualStyleBackColor = true;
            this.removeSecondarySeries.Click += new System.EventHandler(this.removeSecondarySeries_Click);
            // 
            // GraphSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "GraphSetup";
            this.Size = new System.Drawing.Size(671, 406);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label allSeriesLabel;
        private System.Windows.Forms.ListBox allSeriesBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox primarySeriesBox;
        private System.Windows.Forms.Button removePrimarySeries;
        private System.Windows.Forms.Button addPrimarySeries;
        private System.Windows.Forms.ListBox secondarySeriesBox;
        private System.Windows.Forms.Button addSecondarySeries;
        private System.Windows.Forms.Button removeSecondarySeries;
        private System.Windows.Forms.Label primarySeriesLabel;
        private System.Windows.Forms.Label secondarySeriesLabel;
    }
}
