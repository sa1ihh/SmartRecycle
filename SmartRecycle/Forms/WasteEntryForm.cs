using System;
using System.Windows.Forms;
using SmartRecycle.Models;
using SmartRecycle.Data;

namespace SmartRecycle.Forms
{
    public partial class WasteEntryForm : Form
    {
        public Waste CreatedWaste { get; private set; }

        public WasteEntryForm()
        {
            InitializeComponent();

            cmbType.DataSource = Enum.GetValues(typeof(WasteType));
            dtDate.Value = DateTime.Today;
            numKg.DecimalPlaces = 3;
            numKg.Minimum = 0.001M;
            numKg.Maximum = 100000M;
            numKg.Value = 1;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            var type = (WasteType)cmbType.SelectedItem;
            var kg = (double)numKg.Value;
            var date = dtDate.Value.Date;

            CreatedWaste = WasteRepository.CreateWaste(type, kg, date);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}