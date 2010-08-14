namespace MyWindowsFormsLibrary
{
    partial class SystemMonitorHolder
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemMonitorHolder));
            this.axSystemMonitor1 = new AxSystemMonitor.AxSystemMonitor();
            ((System.ComponentModel.ISupportInitialize)(this.axSystemMonitor1)).BeginInit();
            this.SuspendLayout();
            // 
            // axSystemMonitor1
            // 
            this.axSystemMonitor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSystemMonitor1.Enabled = true;
            this.axSystemMonitor1.Location = new System.Drawing.Point(0, 0);
            this.axSystemMonitor1.Name = "axSystemMonitor1";
            this.axSystemMonitor1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSystemMonitor1.OcxState")));
            this.axSystemMonitor1.Size = new System.Drawing.Size(300, 200);
            this.axSystemMonitor1.TabIndex = 0;
            // 
            // SystemMonitorHolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axSystemMonitor1);
            this.Name = "SystemMonitorHolder";
            this.Size = new System.Drawing.Size(300, 200);
            ((System.ComponentModel.ISupportInitialize)(this.axSystemMonitor1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxSystemMonitor.AxSystemMonitor axSystemMonitor1;
    }
}
