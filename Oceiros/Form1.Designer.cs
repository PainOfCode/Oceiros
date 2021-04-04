namespace Oceiros
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.DonwloadBTN = new System.Windows.Forms.Button();
            this.DownloadUrl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(12, 12);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(12, 67);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 1;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.Location = new System.Drawing.Point(12, 41);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ReadOnly = true;
            this.StatusBox.Size = new System.Drawing.Size(100, 20);
            this.StatusBox.TabIndex = 2;
            // 
            // DonwloadBTN
            // 
            this.DonwloadBTN.Location = new System.Drawing.Point(12, 335);
            this.DonwloadBTN.Name = "DonwloadBTN";
            this.DonwloadBTN.Size = new System.Drawing.Size(65, 23);
            this.DonwloadBTN.TabIndex = 3;
            this.DonwloadBTN.Text = "Download";
            this.DonwloadBTN.UseVisualStyleBackColor = true;
            this.DonwloadBTN.Click += new System.EventHandler(this.DonwloadBTN_Click);
            // 
            // DownloadUrl
            // 
            this.DownloadUrl.Location = new System.Drawing.Point(12, 309);
            this.DownloadUrl.Name = "DownloadUrl";
            this.DownloadUrl.Size = new System.Drawing.Size(218, 20);
            this.DownloadUrl.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 370);
            this.Controls.Add(this.DownloadUrl);
            this.Controls.Add(this.DonwloadBTN);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StartBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "REEEEE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button StartBtn;
        public System.Windows.Forms.TextBox StatusBox;
        public System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button DonwloadBTN;
        private System.Windows.Forms.TextBox DownloadUrl;
    }
}

