namespace Progress
{
    partial class ProgressWindow
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
            this.MainMessageLabel = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.SubMessageLabel = new System.Windows.Forms.Label();
            this.MainBar = new System.Windows.Forms.ProgressBar();
            this.SubBar = new System.Windows.Forms.ProgressBar();
            this.BW = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // MainMessageLabel
            // 
            this.MainMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMessageLabel.Location = new System.Drawing.Point(12, 9);
            this.MainMessageLabel.Name = "MainMessageLabel";
            this.MainMessageLabel.Size = new System.Drawing.Size(376, 51);
            this.MainMessageLabel.TabIndex = 0;
            this.MainMessageLabel.Text = " ";
            this.MainMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cancel
            // 
            this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cancel.Location = new System.Drawing.Point(12, 190);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(376, 48);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Отменить";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SubMessageLabel
            // 
            this.SubMessageLabel.Enabled = false;
            this.SubMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SubMessageLabel.Location = new System.Drawing.Point(12, 126);
            this.SubMessageLabel.Name = "SubMessageLabel";
            this.SubMessageLabel.Size = new System.Drawing.Size(376, 20);
            this.SubMessageLabel.TabIndex = 2;
            this.SubMessageLabel.Text = " ";
            this.SubMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainBar
            // 
            this.MainBar.Location = new System.Drawing.Point(16, 63);
            this.MainBar.Name = "MainBar";
            this.MainBar.Size = new System.Drawing.Size(372, 60);
            this.MainBar.TabIndex = 3;
            // 
            // SubBar
            // 
            this.SubBar.Enabled = false;
            this.SubBar.Location = new System.Drawing.Point(12, 149);
            this.SubBar.Name = "SubBar";
            this.SubBar.Size = new System.Drawing.Size(376, 35);
            this.SubBar.TabIndex = 4;
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.ControlBox = false;
            this.Controls.Add(this.SubBar);
            this.Controls.Add(this.MainBar);
            this.Controls.Add(this.SubMessageLabel);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.MainMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "ProgressWindow";
            this.Text = "wait";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label MainMessageLabel;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label SubMessageLabel;
        private System.Windows.Forms.ProgressBar MainBar;
        private System.Windows.Forms.ProgressBar SubBar;
        private System.ComponentModel.BackgroundWorker BW;
    }
}