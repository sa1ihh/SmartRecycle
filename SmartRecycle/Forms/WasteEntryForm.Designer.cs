#nullable disable
using System.Windows.Forms;

namespace SmartRecycle.Forms
{
    partial class WasteEntryForm
    {
        private System.ComponentModel.IContainer components = null;

        private ComboBox cmbType;
        private NumericUpDown numKg;
        private DateTimePicker dtDate;
        private Button btnOk;
        private Button btnCancel;
        private Label lblType;
        private Label lblKg;
        private Label lblDate;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbType = new ComboBox();
            this.numKg = new NumericUpDown();
            this.dtDate = new DateTimePicker();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.lblType = new Label();
            this.lblKg = new Label();
            this.lblDate = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.numKg)).BeginInit();
            this.SuspendLayout();

            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(26, 15);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Tür";

            this.cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbType.Location = new System.Drawing.Point(120, 12);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(200, 23);
            this.cmbType.TabIndex = 1;

            this.lblKg.AutoSize = true;
            this.lblKg.Location = new System.Drawing.Point(12, 50);
            this.lblKg.Name = "lblKg";
            this.lblKg.Size = new System.Drawing.Size(67, 15);
            this.lblKg.TabIndex = 2;
            this.lblKg.Text = "Ağırlık (kg)";

            this.numKg.Location = new System.Drawing.Point(120, 47);
            this.numKg.Name = "numKg";
            this.numKg.Size = new System.Drawing.Size(200, 23);
            this.numKg.TabIndex = 3;

            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 85);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 15);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Tarih";

            this.dtDate.Location = new System.Drawing.Point(120, 79);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(200, 23);
            this.dtDate.TabIndex = 5;

            this.btnOk.Location = new System.Drawing.Point(120, 120);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(95, 30);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);

            this.btnCancel.Location = new System.Drawing.Point(225, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 165);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.numKg);
            this.Controls.Add(this.lblKg);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblType);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WasteEntryForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Atık Ekle";

            ((System.ComponentModel.ISupportInitialize)(this.numKg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}