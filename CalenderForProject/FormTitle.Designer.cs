namespace CalenderForProject
{
    partial class FormTitle
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
            this.txtBoxTitleMeet = new System.Windows.Forms.TextBox();
            this.richBoxDescribtion = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSaveMeet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBoxTitleMeet
            // 
            this.txtBoxTitleMeet.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtBoxTitleMeet.Location = new System.Drawing.Point(142, 143);
            this.txtBoxTitleMeet.Name = "txtBoxTitleMeet";
            this.txtBoxTitleMeet.Size = new System.Drawing.Size(413, 29);
            this.txtBoxTitleMeet.TabIndex = 0;
            // 
            // richBoxDescribtion
            // 
            this.richBoxDescribtion.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richBoxDescribtion.Location = new System.Drawing.Point(143, 213);
            this.richBoxDescribtion.Name = "richBoxDescribtion";
            this.richBoxDescribtion.Size = new System.Drawing.Size(413, 166);
            this.richBoxDescribtion.TabIndex = 1;
            this.richBoxDescribtion.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(139, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Title:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(138, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Describtion :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(264, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 28);
            this.label3.TabIndex = 4;
            this.label3.Text = "SAVE MEETİNG";
            // 
            // buttonSaveMeet
            // 
            this.buttonSaveMeet.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveMeet.Location = new System.Drawing.Point(450, 398);
            this.buttonSaveMeet.Name = "buttonSaveMeet";
            this.buttonSaveMeet.Size = new System.Drawing.Size(105, 29);
            this.buttonSaveMeet.TabIndex = 5;
            this.buttonSaveMeet.Text = "Save";
            this.buttonSaveMeet.UseVisualStyleBackColor = true;
            this.buttonSaveMeet.Click += new System.EventHandler(this.buttonSaveMeet_Click);
            // 
            // FormTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 467);
            this.Controls.Add(this.buttonSaveMeet);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richBoxDescribtion);
            this.Controls.Add(this.txtBoxTitleMeet);
            this.Name = "FormTitle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTitle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxTitleMeet;
        private System.Windows.Forms.RichTextBox richBoxDescribtion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSaveMeet;
    }
}