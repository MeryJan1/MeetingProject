namespace CalenderForProject
{
    partial class UserControlInformationPlaning
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
            this.lbdays = new System.Windows.Forms.Label();
            this.rBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lbdays
            // 
            this.lbdays.AutoSize = true;
            this.lbdays.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbdays.Location = new System.Drawing.Point(12, 11);
            this.lbdays.Name = "lbdays";
            this.lbdays.Size = new System.Drawing.Size(30, 24);
            this.lbdays.TabIndex = 9;
            this.lbdays.Text = "00";
            // 
            // rBox
            // 
            this.rBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rBox.Location = new System.Drawing.Point(32, 38);
            this.rBox.Name = "rBox";
            this.rBox.Size = new System.Drawing.Size(133, 62);
            this.rBox.TabIndex = 10;
            this.rBox.Text = "";
            // 
            // UserControlInformationPlaning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rBox);
            this.Controls.Add(this.lbdays);
            this.Name = "UserControlInformationPlaning";
            this.Size = new System.Drawing.Size(162, 100);
            this.Load += new System.EventHandler(this.UserControlInformationPlaning_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lbdays;
        private System.Windows.Forms.RichTextBox rBox;
    }
}
