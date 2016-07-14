namespace WhamoLauncher.Charts.Views
{
    partial class CustomChartsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomChartsView));
            this.tabs = new System.Windows.Forms.TabControl();
            this.cancel = new System.Windows.Forms.Button();
            this.accept = new System.Windows.Forms.Button();
            this.loadTemplate = new System.Windows.Forms.Button();
            this.addTab = new System.Windows.Forms.Button();
            this.removeTab = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(700, 429);
            this.tabs.TabIndex = 0;
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(616, 447);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(96, 36);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.Location = new System.Drawing.Point(514, 447);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(96, 36);
            this.accept.TabIndex = 4;
            this.accept.Text = "accept";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // loadTemplate
            // 
            this.loadTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadTemplate.Location = new System.Drawing.Point(216, 447);
            this.loadTemplate.Name = "loadTemplate";
            this.loadTemplate.Size = new System.Drawing.Size(96, 36);
            this.loadTemplate.TabIndex = 3;
            this.loadTemplate.Text = "loadTemplate";
            this.loadTemplate.UseVisualStyleBackColor = true;
            this.loadTemplate.Click += new System.EventHandler(this.loadTemplate_Click);
            // 
            // addTab
            // 
            this.addTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addTab.Location = new System.Drawing.Point(12, 447);
            this.addTab.Name = "addTab";
            this.addTab.Size = new System.Drawing.Size(96, 36);
            this.addTab.TabIndex = 1;
            this.addTab.Text = "addTab";
            this.addTab.UseVisualStyleBackColor = true;
            this.addTab.Click += new System.EventHandler(this.addTab_Click);
            // 
            // removeTab
            // 
            this.removeTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeTab.Location = new System.Drawing.Point(114, 447);
            this.removeTab.Name = "removeTab";
            this.removeTab.Size = new System.Drawing.Size(96, 36);
            this.removeTab.TabIndex = 2;
            this.removeTab.Text = "removeTab";
            this.removeTab.UseVisualStyleBackColor = true;
            this.removeTab.Click += new System.EventHandler(this.removeTab_Click);
            // 
            // CustomSeriesConfiguration
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(724, 495);
            this.Controls.Add(this.removeTab);
            this.Controls.Add(this.addTab);
            this.Controls.Add(this.loadTemplate);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "CustomSeriesConfiguration";
            this.Text = "CustomSeriesConfiguration";
            this.Load += new System.EventHandler(this.CustomSeriesConfiguration_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button loadTemplate;
        private System.Windows.Forms.Button addTab;
        private System.Windows.Forms.Button removeTab;
    }
}