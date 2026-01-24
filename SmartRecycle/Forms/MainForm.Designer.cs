#nullable disable
using System.Windows.Forms;

namespace SmartRecycle.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnAdd;
        private Button btnExport;
        private DataGridView dgv;
        private Label lblTotal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnAdd = new Button();
            this.btnExport = new Button();
            this.dgv = new DataGridView();
            this.lblTotal = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();

            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Atık Ekle";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            this.btnExport.Location = new System.Drawing.Point(158, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(140, 30);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "CSV Dışa Aktar";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);

            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.Location = new System.Drawing.Point(12, 55);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(760, 350);
            this.dgv.TabIndex = 2;

            this.dgv.Columns.Add("Type", "Tür");
            this.dgv.Columns.Add("Weight", "Ağırlık (kg)");
            this.dgv.Columns.Add("Date", "Tarih");

            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 420);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(80, 15);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Toplam: 0 kg";

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 451);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnAdd);
            this.Name = "MainForm";
            this.Text = "SmartRecycle";

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}