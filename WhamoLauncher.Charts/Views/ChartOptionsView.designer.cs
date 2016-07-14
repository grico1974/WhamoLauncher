namespace WhamoLauncher.Charts.Views
{
    partial class ChartOptionsView
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartOptionsView));
            this.customCharts = new System.Windows.Forms.Button();
            this.defaultCharts = new System.Windows.Forms.Button();
            this.noCharts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // customCharts
            // 
            this.customCharts.Location = new System.Drawing.Point(142, 12);
            this.customCharts.Name = "customCharts";
            this.customCharts.Size = new System.Drawing.Size(124, 39);
            this.customCharts.TabIndex = 2;
            this.customCharts.Text = "customCharts";
            this.customCharts.UseVisualStyleBackColor = true;
            this.customCharts.Click += new System.EventHandler(this.customCharts_Click);
            // 
            // defaultCharts
            // 
            this.defaultCharts.Location = new System.Drawing.Point(12, 12);
            this.defaultCharts.Name = "defaultCharts";
            this.defaultCharts.Size = new System.Drawing.Size(124, 39);
            this.defaultCharts.TabIndex = 1;
            this.defaultCharts.Text = "defaultCharts";
            this.defaultCharts.UseVisualStyleBackColor = true;
            this.defaultCharts.Click += new System.EventHandler(this.defaultCharts_Click);
            // 
            // noCharts
            // 
            this.noCharts.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.noCharts.Location = new System.Drawing.Point(272, 12);
            this.noCharts.Name = "noCharts";
            this.noCharts.Size = new System.Drawing.Size(124, 39);
            this.noCharts.TabIndex = 0;
            this.noCharts.Text = "noCharts";
            this.noCharts.UseVisualStyleBackColor = true;
            this.noCharts.Click += new System.EventHandler(this.noCharts_Click);
            // 
            // ChartOptionsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.noCharts;
            this.ClientSize = new System.Drawing.Size(408, 63);
            this.Controls.Add(this.noCharts);
            this.Controls.Add(this.defaultCharts);
            this.Controls.Add(this.customCharts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartOptionsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ChartGenerationOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button customCharts;
        private System.Windows.Forms.Button defaultCharts;
        private System.Windows.Forms.Button noCharts;
    }
}

