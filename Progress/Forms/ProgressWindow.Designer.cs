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
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.SubMessageLabel = new System.Windows.Forms.Label();
            this.MainBar = new System.Windows.Forms.ProgressBar();
            this.SubBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // MainMessageLabel
            // 
            this.MainMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMessageLabel.Location = new System.Drawing.Point(12, 9);
            this.MainMessageLabel.Name = "MainMessageLabel";
            this.MainMessageLabel.Size = new System.Drawing.Size(576, 75);
            this.MainMessageLabel.TabIndex = 0;
            this.MainMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Button_Cancel.Location = new System.Drawing.Point(12, 284);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(576, 54);
            this.Button_Cancel.TabIndex = 1;
            this.Button_Cancel.Text = "Отменить";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SubMessageLabel
            // 
            this.SubMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SubMessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SubMessageLabel.Location = new System.Drawing.Point(12, 150);
            this.SubMessageLabel.Name = "SubMessageLabel";
            this.SubMessageLabel.Size = new System.Drawing.Size(576, 75);
            this.SubMessageLabel.TabIndex = 2;
            this.SubMessageLabel.Text = " ";
            this.SubMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainBar
            // 
            this.MainBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainBar.Location = new System.Drawing.Point(12, 87);
            this.MainBar.Name = "MainBar";
            this.MainBar.Size = new System.Drawing.Size(576, 60);
            this.MainBar.TabIndex = 3;
            // 
            // SubBar
            // 
            this.SubBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SubBar.Location = new System.Drawing.Point(12, 228);
            this.SubBar.Name = "SubBar";
            this.SubBar.Size = new System.Drawing.Size(576, 50);
            this.SubBar.TabIndex = 4;
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 350);
            this.ControlBox = false;
            this.Controls.Add(this.SubBar);
            this.Controls.Add(this.MainBar);
            this.Controls.Add(this.SubMessageLabel);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.MainMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 650);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 30);
            this.Name = "ProgressWindow";
            this.Text = "wait";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label MainMessageLabel;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label SubMessageLabel;
        private System.Windows.Forms.ProgressBar MainBar;
        private System.Windows.Forms.ProgressBar SubBar;
    }
}