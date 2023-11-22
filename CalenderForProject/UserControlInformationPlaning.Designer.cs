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
            this.lBox = new System.Windows.Forms.ListBox();
            this.lstBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbdays
            // 
            this.lbdays.AutoSize = true;
            this.lbdays.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbdays.Location = new System.Drawing.Point(6, 12);
            this.lbdays.Name = "lbdays";
            this.lbdays.Size = new System.Drawing.Size(30, 24);
            this.lbdays.TabIndex = 9;
            this.lbdays.Text = "00";
            // 
            // lBox
            // 
            this.lBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lBox.FormattingEnabled = true;
            this.lBox.ItemHeight = 18;
            this.lBox.Location = new System.Drawing.Point(42, 24);
            this.lBox.Name = "lBox";
            this.lBox.Size = new System.Drawing.Size(120, 76);
            this.lBox.TabIndex = 11;
            // 
            // lstBox
            // 
            this.lstBox.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lstBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lstBox.FormattingEnabled = true;
            this.lstBox.ItemHeight = 18;
            this.lstBox.Location = new System.Drawing.Point(35, 24);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(120, 76);
            this.lstBox.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "00";
            // 
            // UserControlInformationPlaning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lBox);
            this.Controls.Add(this.lbdays);
            this.Name = "UserControlInformationPlaning";
            this.Size = new System.Drawing.Size(162, 100);
            this.Load += new System.EventHandler(this.UserControlInformationPlaning_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lbdays;
        private System.Windows.Forms.ListBox lBox;
        private System.Windows.Forms.ListBox lstBox;
        public System.Windows.Forms.Label label2;
    }
}
