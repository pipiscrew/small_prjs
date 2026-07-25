namespace App.Dialogs
{
    partial class frmProduct
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg = new System.Windows.Forms.DataGridView();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtNutritiontable = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtIngredients = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbCategoryID = new System.Windows.Forms.ComboBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblWhen2check = new System.Windows.Forms.Label();
            this.txtWhen2check = new System.Windows.Forms.TextBox();
            this.lblDateupdated = new System.Windows.Forms.Label();
            this.txtDateupdated = new System.Windows.Forms.TextBox();
            this.lblSmarketab = new System.Windows.Forms.Label();
            this.txtSmarketab = new System.Windows.Forms.TextBox();
            this.lblSmarketsklav = new System.Windows.Forms.Label();
            this.txtSmarketsklav = new System.Windows.Forms.TextBox();
            this.lblSmarketbazaar = new System.Windows.Forms.Label();
            this.txtSmarketbazaar = new System.Windows.Forms.TextBox();
            this.lblSmarketmymarket = new System.Windows.Forms.Label();
            this.txtSmarketmymarket = new System.Windows.Forms.TextBox();
            this.lblHomepage = new System.Windows.Forms.Label();
            this.txtHomepage = new System.Windows.Forms.TextBox();
            this.lblCategory_id = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.dg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dg.Location = new System.Drawing.Point(9, 12);
            this.dg.MultiSelect = false;
            this.dg.Name = "dg";
            this.dg.RowHeadersVisible = false;
            this.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg.ShowCellErrors = false;
            this.dg.ShowCellToolTips = false;
            this.dg.ShowEditingIcon = false;
            this.dg.ShowRowErrors = false;
            this.dg.Size = new System.Drawing.Size(863, 337);
            this.dg.TabIndex = 1;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(882, 11);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(121, 31);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "new";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(882, 56);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(121, 31);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(882, 101);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(121, 31);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.cmbCategoryID);
            this.groupBox1.Controls.Add(this.lblId);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.lblTitle);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Controls.Add(this.lblUrl);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.lblWhen2check);
            this.groupBox1.Controls.Add(this.txtWhen2check);
            this.groupBox1.Controls.Add(this.lblDateupdated);
            this.groupBox1.Controls.Add(this.txtDateupdated);
            this.groupBox1.Controls.Add(this.lblSmarketab);
            this.groupBox1.Controls.Add(this.txtSmarketab);
            this.groupBox1.Controls.Add(this.lblSmarketsklav);
            this.groupBox1.Controls.Add(this.txtSmarketsklav);
            this.groupBox1.Controls.Add(this.lblSmarketbazaar);
            this.groupBox1.Controls.Add(this.txtSmarketbazaar);
            this.groupBox1.Controls.Add(this.lblSmarketmymarket);
            this.groupBox1.Controls.Add(this.txtSmarketmymarket);
            this.groupBox1.Controls.Add(this.lblHomepage);
            this.groupBox1.Controls.Add(this.txtHomepage);
            this.groupBox1.Controls.Add(this.lblCategory_id);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(9, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(863, 299);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " details : ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(498, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(359, 267);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtNutritiontable);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(351, 239);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "nutrition table";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtNutritiontable
            // 
            this.txtNutritiontable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNutritiontable.Location = new System.Drawing.Point(3, 3);
            this.txtNutritiontable.Multiline = true;
            this.txtNutritiontable.Name = "txtNutritiontable";
            this.txtNutritiontable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNutritiontable.Size = new System.Drawing.Size(345, 233);
            this.txtNutritiontable.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtIngredients);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(351, 239);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ingredients";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtIngredients
            // 
            this.txtIngredients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIngredients.Location = new System.Drawing.Point(3, 3);
            this.txtIngredients.Multiline = true;
            this.txtIngredients.Name = "txtIngredients";
            this.txtIngredients.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIngredients.Size = new System.Drawing.Size(345, 233);
            this.txtIngredients.TabIndex = 13;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtComment);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(351, 239);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "comments";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Location = new System.Drawing.Point(0, 0);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(351, 239);
            this.txtComment.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::posokanei2.Properties.Resources.add16;
            this.pictureBox1.Location = new System.Drawing.Point(431, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // cmbCategoryID
            // 
            this.cmbCategoryID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryID.FormattingEnabled = true;
            this.cmbCategoryID.Location = new System.Drawing.Point(276, 26);
            this.cmbCategoryID.Name = "cmbCategoryID";
            this.cmbCategoryID.Size = new System.Drawing.Size(149, 23);
            this.cmbCategoryID.TabIndex = 20;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(7, 26);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(39, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "lblId :";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(104, 23);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(69, 23);
            this.txtId.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(7, 64);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(52, 15);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "lblTitle :";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(104, 61);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(343, 23);
            this.txtTitle.TabIndex = 1;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(7, 102);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(70, 15);
            this.lblUrl.TabIndex = 2;
            this.lblUrl.Text = "posokanei :";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(104, 99);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(343, 23);
            this.txtUrl.TabIndex = 2;
            // 
            // lblWhen2check
            // 
            this.lblWhen2check.AutoSize = true;
            this.lblWhen2check.Location = new System.Drawing.Point(101, 278);
            this.lblWhen2check.Name = "lblWhen2check";
            this.lblWhen2check.Size = new System.Drawing.Size(98, 15);
            this.lblWhen2check.TabIndex = 3;
            this.lblWhen2check.Text = "lblWhen2check :";
            // 
            // txtWhen2check
            // 
            this.txtWhen2check.Location = new System.Drawing.Point(205, 275);
            this.txtWhen2check.Name = "txtWhen2check";
            this.txtWhen2check.Size = new System.Drawing.Size(45, 23);
            this.txtWhen2check.TabIndex = 3;
            // 
            // lblDateupdated
            // 
            this.lblDateupdated.AutoSize = true;
            this.lblDateupdated.Location = new System.Drawing.Point(256, 278);
            this.lblDateupdated.Name = "lblDateupdated";
            this.lblDateupdated.Size = new System.Drawing.Size(98, 15);
            this.lblDateupdated.TabIndex = 4;
            this.lblDateupdated.Text = "lblDateupdated :";
            // 
            // txtDateupdated
            // 
            this.txtDateupdated.Location = new System.Drawing.Point(360, 275);
            this.txtDateupdated.Name = "txtDateupdated";
            this.txtDateupdated.Size = new System.Drawing.Size(99, 23);
            this.txtDateupdated.TabIndex = 4;
            // 
            // lblSmarketab
            // 
            this.lblSmarketab.AutoSize = true;
            this.lblSmarketab.Location = new System.Drawing.Point(7, 135);
            this.lblSmarketab.Name = "lblSmarketab";
            this.lblSmarketab.Size = new System.Drawing.Size(30, 15);
            this.lblSmarketab.TabIndex = 5;
            this.lblSmarketab.Text = "AB : ";
            // 
            // txtSmarketab
            // 
            this.txtSmarketab.Location = new System.Drawing.Point(104, 132);
            this.txtSmarketab.Name = "txtSmarketab";
            this.txtSmarketab.Size = new System.Drawing.Size(343, 23);
            this.txtSmarketab.TabIndex = 5;
            // 
            // lblSmarketsklav
            // 
            this.lblSmarketsklav.AutoSize = true;
            this.lblSmarketsklav.Location = new System.Drawing.Point(7, 164);
            this.lblSmarketsklav.Name = "lblSmarketsklav";
            this.lblSmarketsklav.Size = new System.Drawing.Size(72, 15);
            this.lblSmarketsklav.TabIndex = 6;
            this.lblSmarketsklav.Text = "Sklavenitis :";
            // 
            // txtSmarketsklav
            // 
            this.txtSmarketsklav.Location = new System.Drawing.Point(104, 161);
            this.txtSmarketsklav.Name = "txtSmarketsklav";
            this.txtSmarketsklav.Size = new System.Drawing.Size(343, 23);
            this.txtSmarketsklav.TabIndex = 6;
            // 
            // lblSmarketbazaar
            // 
            this.lblSmarketbazaar.AutoSize = true;
            this.lblSmarketbazaar.Location = new System.Drawing.Point(7, 193);
            this.lblSmarketbazaar.Name = "lblSmarketbazaar";
            this.lblSmarketbazaar.Size = new System.Drawing.Size(51, 15);
            this.lblSmarketbazaar.TabIndex = 7;
            this.lblSmarketbazaar.Text = "Bazaar :";
            // 
            // txtSmarketbazaar
            // 
            this.txtSmarketbazaar.Location = new System.Drawing.Point(104, 190);
            this.txtSmarketbazaar.Name = "txtSmarketbazaar";
            this.txtSmarketbazaar.Size = new System.Drawing.Size(343, 23);
            this.txtSmarketbazaar.TabIndex = 7;
            // 
            // lblSmarketmymarket
            // 
            this.lblSmarketmymarket.AutoSize = true;
            this.lblSmarketmymarket.Location = new System.Drawing.Point(7, 222);
            this.lblSmarketmymarket.Name = "lblSmarketmymarket";
            this.lblSmarketmymarket.Size = new System.Drawing.Size(69, 15);
            this.lblSmarketmymarket.TabIndex = 8;
            this.lblSmarketmymarket.Text = "MyMarket :";
            // 
            // txtSmarketmymarket
            // 
            this.txtSmarketmymarket.Location = new System.Drawing.Point(104, 219);
            this.txtSmarketmymarket.Name = "txtSmarketmymarket";
            this.txtSmarketmymarket.Size = new System.Drawing.Size(343, 23);
            this.txtSmarketmymarket.TabIndex = 8;
            // 
            // lblHomepage
            // 
            this.lblHomepage.AutoSize = true;
            this.lblHomepage.Location = new System.Drawing.Point(7, 251);
            this.lblHomepage.Name = "lblHomepage";
            this.lblHomepage.Size = new System.Drawing.Size(70, 15);
            this.lblHomepage.TabIndex = 10;
            this.lblHomepage.Text = "Homepage :";
            // 
            // txtHomepage
            // 
            this.txtHomepage.Location = new System.Drawing.Point(104, 248);
            this.txtHomepage.Name = "txtHomepage";
            this.txtHomepage.Size = new System.Drawing.Size(343, 23);
            this.txtHomepage.TabIndex = 10;
            // 
            // lblCategory_id
            // 
            this.lblCategory_id.AutoSize = true;
            this.lblCategory_id.Location = new System.Drawing.Point(209, 26);
            this.lblCategory_id.Name = "lblCategory_id";
            this.lblCategory_id.Size = new System.Drawing.Size(61, 15);
            this.lblCategory_id.TabIndex = 12;
            this.lblCategory_id.Text = "Category :";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(882, 200);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(50, 15);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "search :";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(882, 218);
            this.txtSearch.MaxLength = 60;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(121, 23);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(882, 313);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(121, 36);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "export EXCEL";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(882, 146);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(121, 31);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 672);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.dg);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "frmProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmProduct";
            this.Load += new System.EventHandler(this.frmProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
            private System.Windows.Forms.Label lblId;
            private System.Windows.Forms.TextBox txtId;
            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.TextBox txtTitle;
            private System.Windows.Forms.Label lblUrl;
            private System.Windows.Forms.TextBox txtUrl;
            private System.Windows.Forms.Label lblWhen2check;
            private System.Windows.Forms.TextBox txtWhen2check;
            private System.Windows.Forms.Label lblDateupdated;
            private System.Windows.Forms.TextBox txtDateupdated;
            private System.Windows.Forms.Label lblSmarketab;
            private System.Windows.Forms.TextBox txtSmarketab;
            private System.Windows.Forms.Label lblSmarketsklav;
            private System.Windows.Forms.TextBox txtSmarketsklav;
            private System.Windows.Forms.Label lblSmarketbazaar;
            private System.Windows.Forms.TextBox txtSmarketbazaar;
            private System.Windows.Forms.Label lblSmarketmymarket;
            private System.Windows.Forms.TextBox txtSmarketmymarket;
            private System.Windows.Forms.TextBox txtComment;
            private System.Windows.Forms.Label lblHomepage;
            private System.Windows.Forms.TextBox txtHomepage;
            private System.Windows.Forms.TextBox txtNutritiontable;
            private System.Windows.Forms.Label lblCategory_id;
            private System.Windows.Forms.TextBox txtIngredients;
            private System.Windows.Forms.PictureBox pictureBox1;
            private System.Windows.Forms.ComboBox cmbCategoryID;
            private System.Windows.Forms.TabControl tabControl1;
            private System.Windows.Forms.TabPage tabPage1;
            private System.Windows.Forms.TabPage tabPage2;
            private System.Windows.Forms.TabPage tabPage3;

    }
}

