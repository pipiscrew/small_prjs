namespace sampleSQLite
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSupplierID = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dg2 = new System.Windows.Forms.DataGridView();
            this.txtcontactname = new CustomTextBox();
            this.txtcompanyname = new CustomTextBox();
            this.txtcustomerid = new CustomTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(94, 550);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSupplierID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtcontactname);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtcompanyname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtcustomerid);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(17, 310);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(806, 234);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " details : ";
            // 
            // cmdSupplierID
            // 
            this.cmdSupplierID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdSupplierID.FormattingEnabled = true;
            this.cmdSupplierID.Location = new System.Drawing.Point(138, 107);
            this.cmdSupplierID.Name = "cmdSupplierID";
            this.cmdSupplierID.Size = new System.Drawing.Size(196, 23);
            this.cmdSupplierID.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "ContactName :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "ContactName :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "CompanyName :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "CustomerID :";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(842, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(107, 34);
            this.btnNew.TabIndex = 9;
            this.btnNew.Text = "new";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(842, 52);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(107, 34);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(842, 92);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 34);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dg2
            // 
            this.dg2.AllowUserToAddRows = false;
            this.dg2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.dg2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dg2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg2.Location = new System.Drawing.Point(17, 12);
            this.dg2.MultiSelect = false;
            this.dg2.Name = "dg2";
            this.dg2.ReadOnly = true;
            this.dg2.RowHeadersVisible = false;
            this.dg2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg2.Size = new System.Drawing.Size(806, 292);
            this.dg2.TabIndex = 12;
            // 
            // txtcontactname
            // 
            this.txtcontactname.Location = new System.Drawing.Point(138, 77);
            this.txtcontactname.Name = "txtcontactname";
            this.txtcontactname.Size = new System.Drawing.Size(196, 23);
            this.txtcontactname.TabIndex = 4;
            // 
            // txtcompanyname
            // 
            this.txtcompanyname.Location = new System.Drawing.Point(138, 48);
            this.txtcompanyname.Name = "txtcompanyname";
            this.txtcompanyname.Size = new System.Drawing.Size(196, 23);
            this.txtcompanyname.TabIndex = 2;
            // 
            // txtcustomerid
            // 
            this.txtcustomerid.Location = new System.Drawing.Point(138, 19);
            this.txtcustomerid.Name = "txtcustomerid";
            this.txtcustomerid.Size = new System.Drawing.Size(196, 23);
            this.txtcustomerid.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 595);
            this.Controls.Add(this.dg2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private CustomTextBox txtcustomerid;
        private System.Windows.Forms.Label label3;
        private CustomTextBox txtcontactname;
        private System.Windows.Forms.Label label2;
        private CustomTextBox txtcompanyname;
        private System.Windows.Forms.ComboBox cmdSupplierID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dg2;
    }
}

