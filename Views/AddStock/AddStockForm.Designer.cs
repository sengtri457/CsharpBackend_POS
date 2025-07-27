namespace Group1_POS.Views
{
    partial class AddStockForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbosupplierName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddStock = new System.Windows.Forms.Button();
            this.txtProductId = new System.Windows.Forms.TextBox();
            this.txtRoleTitle = new System.Windows.Forms.Label();
            this.TtitleRole = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.txtCost);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbosupplierName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtQty);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtProductName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnAddStock);
            this.panel1.Controls.Add(this.txtProductId);
            this.panel1.Controls.Add(this.txtRoleTitle);
            this.panel1.Controls.Add(this.TtitleRole);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(749, 1106);
            this.panel1.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Chartreuse;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBack.Location = new System.Drawing.Point(238, 562);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(231, 52);
            this.btnBack.TabIndex = 29;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(238, 328);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(311, 43);
            this.txtCost.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 42);
            this.label1.TabIndex = 27;
            this.label1.Text = "Cost";
            // 
            // cbosupplierName
            // 
            this.cbosupplierName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbosupplierName.FormattingEnabled = true;
            this.cbosupplierName.Location = new System.Drawing.Point(238, 412);
            this.cbosupplierName.Name = "cbosupplierName";
            this.cbosupplierName.Size = new System.Drawing.Size(281, 50);
            this.cbosupplierName.TabIndex = 26;
            this.cbosupplierName.SelectedIndexChanged += new System.EventHandler(this.cboProductName_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 420);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 42);
            this.label7.TabIndex = 17;
            this.label7.Text = "SupplierName";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(238, 249);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(311, 43);
            this.txtQty.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 63);
            this.label4.TabIndex = 9;
            this.label4.Text = "ProductName";
            // 
            // txtProductName
            // 
            this.txtProductName.Enabled = false;
            this.txtProductName.Location = new System.Drawing.Point(238, 177);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(311, 43);
            this.txtProductName.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 42);
            this.label3.TabIndex = 7;
            this.label3.Text = "Qty";
            // 
            // btnAddStock
            // 
            this.btnAddStock.BackColor = System.Drawing.Color.YellowGreen;
            this.btnAddStock.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnAddStock.FlatAppearance.BorderSize = 0;
            this.btnAddStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnAddStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAddStock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddStock.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAddStock.Location = new System.Drawing.Point(238, 495);
            this.btnAddStock.Name = "btnAddStock";
            this.btnAddStock.Size = new System.Drawing.Size(231, 52);
            this.btnAddStock.TabIndex = 3;
            this.btnAddStock.Text = "Add Stock";
            this.btnAddStock.UseVisualStyleBackColor = false;
            this.btnAddStock.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtProductId
            // 
            this.txtProductId.Enabled = false;
            this.txtProductId.Location = new System.Drawing.Point(238, 103);
            this.txtProductId.Name = "txtProductId";
            this.txtProductId.Size = new System.Drawing.Size(311, 43);
            this.txtProductId.TabIndex = 2;
            this.txtProductId.TextChanged += new System.EventHandler(this.txtRole_TextChanged);
            // 
            // txtRoleTitle
            // 
            this.txtRoleTitle.AutoSize = true;
            this.txtRoleTitle.Location = new System.Drawing.Point(5, 106);
            this.txtRoleTitle.Name = "txtRoleTitle";
            this.txtRoleTitle.Size = new System.Drawing.Size(142, 42);
            this.txtRoleTitle.TabIndex = 1;
            this.txtRoleTitle.Text = "ProductId";
            // 
            // TtitleRole
            // 
            this.TtitleRole.Font = new System.Drawing.Font("Poppins", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TtitleRole.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TtitleRole.Location = new System.Drawing.Point(40, 9);
            this.TtitleRole.Name = "TtitleRole";
            this.TtitleRole.Size = new System.Drawing.Size(429, 57);
            this.TtitleRole.TabIndex = 0;
            this.TtitleRole.Text = "Welcome AddStock";
            this.TtitleRole.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStockToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 36);
            // 
            // addStockToolStripMenuItem
            // 
            this.addStockToolStripMenuItem.Name = "addStockToolStripMenuItem";
            this.addStockToolStripMenuItem.Size = new System.Drawing.Size(161, 32);
            this.addStockToolStripMenuItem.Text = "AddStock";
            // 
            // AddStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(749, 1106);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddStockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserForm";
            this.Load += new System.EventHandler(this.RoleForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TtitleRole;
        private System.Windows.Forms.Button btnAddStock;
        private System.Windows.Forms.Label txtRoleTitle;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbosupplierName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addStockToolStripMenuItem;
        public System.Windows.Forms.TextBox txtProductId;
        public System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBack;
    }
}