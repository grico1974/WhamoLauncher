namespace WhamoLauncher.Charts.Views
{
    partial class WorkInProgressView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkInProgressView));
            this.waitLabel = new WhamoLauncher.Charts.Views.AnimatedLabel();
            this.SuspendLayout();
            // 
            // waitLabel
            // 
            this.waitLabel.AnimatedTicksBeforeTextReset = 3;
            this.waitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitLabel.Location = new System.Drawing.Point(33, 28);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(358, 20);
            this.waitLabel.TabIndex = 0;
            this.waitLabel.Text = "waitLabel.";
            this.waitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.waitLabel.TimerInterval = 350;
            // 
            // WorkInProgressView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 83);
            this.ControlBox = false;
            this.Controls.Add(this.waitLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WorkInProgressView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generando graficos";
            this.ResumeLayout(false);

        }

        #endregion

        private WhamoLauncher.Charts.Views.AnimatedLabel waitLabel;
    }
}