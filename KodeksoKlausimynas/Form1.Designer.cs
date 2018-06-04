namespace KodeksoKlausimynas
{
    partial class Main
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
            this.generateQuestion = new System.Windows.Forms.Button();
            this.fineBox = new System.Windows.Forms.TextBox();
            this.QuestionText = new System.Windows.Forms.TextBox();
            this.showAnswer = new System.Windows.Forms.Button();
            this.instantShow = new System.Windows.Forms.CheckBox();
            this.versionText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // generateQuestion
            // 
            this.generateQuestion.Location = new System.Drawing.Point(39, 12);
            this.generateQuestion.Name = "generateQuestion";
            this.generateQuestion.Size = new System.Drawing.Size(118, 32);
            this.generateQuestion.TabIndex = 0;
            this.generateQuestion.Text = "Naujas Klausimas";
            this.generateQuestion.UseVisualStyleBackColor = true;
            this.generateQuestion.Click += new System.EventHandler(this.generateQuestion_Click);
            // 
            // fineBox
            // 
            this.fineBox.Location = new System.Drawing.Point(39, 153);
            this.fineBox.Multiline = true;
            this.fineBox.Name = "fineBox";
            this.fineBox.Size = new System.Drawing.Size(436, 207);
            this.fineBox.TabIndex = 2;
            // 
            // QuestionText
            // 
            this.QuestionText.Location = new System.Drawing.Point(39, 50);
            this.QuestionText.Multiline = true;
            this.QuestionText.Name = "QuestionText";
            this.QuestionText.Size = new System.Drawing.Size(436, 49);
            this.QuestionText.TabIndex = 3;
            // 
            // showAnswer
            // 
            this.showAnswer.Location = new System.Drawing.Point(39, 115);
            this.showAnswer.Name = "showAnswer";
            this.showAnswer.Size = new System.Drawing.Size(118, 32);
            this.showAnswer.TabIndex = 4;
            this.showAnswer.Text = "Rodyti Atsakymą";
            this.showAnswer.UseVisualStyleBackColor = true;
            this.showAnswer.Click += new System.EventHandler(this.showAnswer_Click);
            // 
            // instantShow
            // 
            this.instantShow.AutoSize = true;
            this.instantShow.Location = new System.Drawing.Point(163, 124);
            this.instantShow.Name = "instantShow";
            this.instantShow.Size = new System.Drawing.Size(128, 17);
            this.instantShow.TabIndex = 5;
            this.instantShow.Text = "Iškart rodyti atsakymą";
            this.instantShow.UseVisualStyleBackColor = true;
            // 
            // versionText
            // 
            this.versionText.AutoSize = true;
            this.versionText.Location = new System.Drawing.Point(483, 22);
            this.versionText.Name = "versionText";
            this.versionText.Size = new System.Drawing.Size(0, 13);
            this.versionText.TabIndex = 6;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 381);
            this.Controls.Add(this.versionText);
            this.Controls.Add(this.instantShow);
            this.Controls.Add(this.showAnswer);
            this.Controls.Add(this.QuestionText);
            this.Controls.Add(this.fineBox);
            this.Controls.Add(this.generateQuestion);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Baudų Klausimynas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateQuestion;
        private System.Windows.Forms.TextBox fineBox;
        private System.Windows.Forms.TextBox QuestionText;
        private System.Windows.Forms.Button showAnswer;
        private System.Windows.Forms.CheckBox instantShow;
        private System.Windows.Forms.Label versionText;
    }
}

