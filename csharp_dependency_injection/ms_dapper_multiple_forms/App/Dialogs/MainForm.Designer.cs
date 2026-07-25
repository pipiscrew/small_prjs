namespace App.Dialogs
{
    partial class MainForm
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
            this.btnCheckProducts = new System.Windows.Forms.Button();
            this.btnCRUDproducts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheckProducts
            // 
            this.btnCheckProducts.Location = new System.Drawing.Point(58, 31);
            this.btnCheckProducts.Name = "btnCheckProducts";
            this.btnCheckProducts.Size = new System.Drawing.Size(194, 36);
            this.btnCheckProducts.TabIndex = 0;
            this.btnCheckProducts.Text = "check products";
            this.btnCheckProducts.UseVisualStyleBackColor = true;
            this.btnCheckProducts.Click += new System.EventHandler(this.btnCheckProducts_Click);
            // 
            // btnCRUDproducts
            // 
            this.btnCRUDproducts.Location = new System.Drawing.Point(58, 73);
            this.btnCRUDproducts.Name = "btnCRUDproducts";
            this.btnCRUDproducts.Size = new System.Drawing.Size(194, 36);
            this.btnCRUDproducts.TabIndex = 1;
            this.btnCRUDproducts.Text = "CRUD Products";
            this.btnCRUDproducts.UseVisualStyleBackColor = true;
            this.btnCRUDproducts.Click += new System.EventHandler(this.btnCRUDproducts_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 301);
            this.Controls.Add(this.btnCRUDproducts);
            this.Controls.Add(this.btnCheckProducts);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckProducts;
        private System.Windows.Forms.Button btnCRUDproducts;
    }
}