// MainForm.cs  --> TAMAMINI SİL, BUNU YAPIŞTIR
// Not: MainForm.Designer.cs ve MainForm.resx'e DOKUNMA

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SmartRecycle.Models;
using SmartRecycle.Services;

namespace SmartRecycle.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Designer bazen bu method isimlerini bekliyor
            // (MainForm.Designer.cs içindeki Click bağlarına uyumlu)
            // O yüzden isimleri aynen böyle bıraktım.
            // Eğer Designer bağlamıyorsa da biz burada elle bağlıyoruz:
            if (btnAdd != null) btnAdd.Click += BtnAdd_Click;
            if (btnExport != null) btnExport.Click += BtnExport_Click;

            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;

            EnsureGridColumns();
            UpdateTotalLabel();
        }

        // ========== BUTTONS ==========

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Atık giriş formunu aç
            using (var f = new WasteEntryForm())
            {
                var dr = f.ShowDialog(this);
                if (dr != DialogResult.OK) return;

                // WasteEntryForm içinde CreatedWaste property’si var (sende uyarı çıkıyordu)
                // O yüzden buradan alıyoruz.
                var waste = f.CreatedWaste;
                if (waste == null) return;

                AddWasteToGrid(waste);
                UpdateTotalLabel();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "CSV Kaydet";
                sfd.Filter = "CSV (.csv)|.csv";
                sfd.FileName = "wastes.csv";

                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                ExportGridToCsv(sfd.FileName);
            }
        }

        // ========== KEYBOARD ==========

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Delete: seçili satır(lar)ı sil
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedRows();
                e.Handled = true;
                return;
            }

            // Ctrl+L: hepsini sil
            if (e.Control && e.KeyCode == Keys.L)
            {
                ClearAllRows();
                e.Handled = true;
                return;
            }
        }

        // ========== GRID HELPERS ==========

        private void EnsureGridColumns()
        {
            if (dgv == null) return;

            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = true;

            // Kolonlar yoksa ekle (Designer eklediyse dokunmayız)
            if (!dgv.Columns.Contains("Type"))
                dgv.Columns.Add("Type", "Tür");

            if (!dgv.Columns.Contains("WeightKg"))
                dgv.Columns.Add("WeightKg", "Ağırlık (kg)");

            if (!dgv.Columns.Contains("Date"))
                dgv.Columns.Add("Date", "Tarih");

            if (!dgv.Columns.Contains("RecyclableKg"))
                dgv.Columns.Add("RecyclableKg", "RecyclableKg");

            if (!dgv.Columns.Contains("EnergySavedKwh"))
                dgv.Columns.Add("EnergySavedKwh", "EnergySavedKwh");
        }

        private void AddWasteToGrid(Waste waste)
        {
            if (dgv == null) return;

            EnsureGridColumns();

            var typeText = waste.Type.ToString();
            var weightText = waste.WeightKg.ToString(CultureInfo.InvariantCulture);
            var dateText = waste.Date.ToString("yyyy-MM-dd");

            var recyclableText = waste.RecyclableKg.ToString(CultureInfo.InvariantCulture);
            var energyText = waste.EnergySavedKwh.ToString(CultureInfo.InvariantCulture);

            dgv.Rows.Add(typeText, weightText, dateText, recyclableText, energyText);
        }

        private void RemoveSelectedRows()
        {
            if (dgv == null) return;

            // Seçili satırları sil (IsNewRow kontrolü)
            foreach (DataGridViewRow r in dgv.SelectedRows)
            {
                if (r == null || r.IsNewRow) continue;
                dgv.Rows.Remove(r);
            }

            UpdateTotalLabel();
        }

        private void ClearAllRows()
        {
            if (dgv == null) return;
            dgv.Rows.Clear();
            UpdateTotalLabel();
        }

        // ========== TOTAL ==========

        private void UpdateTotalLabel()
        {
            if (lblTotal == null || dgv == null) return;

            double totalKg = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                object val = null;

                if (dgv.Columns.Contains("WeightKg"))
                    val = row.Cells["WeightKg"].Value;
                else if (row.Cells.Count > 1)
                    val = row.Cells[1].Value;

                if (val == null) continue;

                if (double.TryParse(Convert.ToString(val), NumberStyles.Any, CultureInfo.InvariantCulture, out var w))
                    totalKg += w;
                else if (double.TryParse(Convert.ToString(val), NumberStyles.Any, CultureInfo.CurrentCulture, out w))
                    totalKg += w;
            }

            lblTotal.Text = $"Toplam: {totalKg} kg";
        }

        // ======== CSV EXPORT ========
        private void ExportGridToCsv(string path)
        {
            // Grid’den okumayı bıraktık. Hesapları KENDİMİZ yapıyoruz.

            if (string.IsNullOrWhiteSpace(path)) return;

            var sb = new StringBuilder();
            sb.AppendLine("Type,WeightKg,Date,RecyclableKg,EnergySavedKwh,UnitUsdPerKg,TotalUsd,ProfitUsd");

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                string type = GetCell(row, "Type", 0).Trim();
                double weight = ParseDouble(GetCell(row, "WeightKg", 1));
                string date = GetCell(row, "Date", 2).Trim();

                // Eğer Date boşsa bugünü yaz
                if (string.IsNullOrWhiteSpace(date))
                    date = DateTime.Now.ToString("yyyy-MM-dd");

                // HESAPLAR (StatisticsService içinde bu fonksiyonlar olmalı)
                double recyclableKg = StatisticsService.GetRecyclableKg(type, weight);
                double energyKwh = StatisticsService.GetEnergySavedKwh(type, weight);
                double unitUsd = StatisticsService.GetUnitUsd(type);
                double totalUsd = StatisticsService.GetTotalUsd(type, weight);
                double profitUsd = StatisticsService.GetProfitUsd(type, weight);

                sb.AppendLine(
                    $"{Csv(type)}," +
                    $"{ToInv(weight)}," +
                    $"{Csv(date)}," +
                    $"{ToInv(recyclableKg)}," +
                    $"{ToInv(energyKwh)}," +
                    $"{ToInv(unitUsd)}," +
                    $"{ToInv(totalUsd)}," +
                    $"{ToInv(profitUsd)}"
                );
            }

            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
        }

        private static double ParseDouble(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;

            // Türkçe/Avrupa virgülünü de yakala
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out var v1)) return v1;

            s = s.Replace(",", ".");
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2)) return v2;

            return 0;
        }

        private static string ToInv(double v) => v.ToString("0.##", CultureInfo.InvariantCulture);

        private static string GetCell(DataGridViewRow row, string colName, int fallbackIndex)
        {
            try
            {
                if (row.DataGridView != null && row.DataGridView.Columns.Contains(colName))
                    return Convert.ToString(row.Cells[colName].Value) ?? "";

                if (row.Cells.Count > fallbackIndex)
                    return Convert.ToString(row.Cells[fallbackIndex].Value) ?? "";
            }
            catch { }

            return "";
        }

        private static string Csv(string s)
        {
            s ??= "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r"))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }
    }
}