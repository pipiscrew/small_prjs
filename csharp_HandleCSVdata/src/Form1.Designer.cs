namespace HandleCSVdata
{
    partial class Form1
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
            this.txt1 = new System.Windows.Forms.TextBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt1
            // 
            this.txt1.AllowDrop = true;
            this.txt1.Location = new System.Drawing.Point(13, 13);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(562, 23);
            this.txt1.TabIndex = 0;
            this.txt1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.txt1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // txt2
            // 
            this.txt2.AllowDrop = true;
            this.txt2.Location = new System.Drawing.Point(13, 54);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(562, 23);
            this.txt2.TabIndex = 1;
            this.txt2.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.txt2.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(292, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 296);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt2);
            this.Controls.Add(this.txt1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.Button button1;
    }
}

