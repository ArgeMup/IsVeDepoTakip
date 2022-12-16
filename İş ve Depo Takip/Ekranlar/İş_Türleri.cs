using ArgeMup.HazirKod;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class İş_Türleri : Form
    {
        public İş_Türleri()
        {
            InitializeComponent();
        }
        private void İş_Türleri_Load(object sender, System.EventArgs e)
        {
            Liste.Items.Clear();
            Liste.Items.AddRange(Banka.İşTürü_Listele().ToArray());
            if (Liste.Items.Count > 0) Sil.Enabled = true;

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        }
        private void İş_Türleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
        }

        private void Sil_Click(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0 || Liste.SelectedIndex >= Liste.Items.Count) return;

            DialogResult Dr = MessageBox.Show(Liste .Text + " öğesini silmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.İşTürü_Sil(Liste.Text);
            Banka.Değişiklikleri_Kaydet();
            Liste.Items.RemoveAt(Liste.SelectedIndex);
        }

        private void Liste_SelectedValueChanged(object sender, System.EventArgs e)
        {
            Sil.Enabled = !string.IsNullOrEmpty(Liste.Text);
        }

        private void Yeni_TextChanged(object sender, System.EventArgs e)
        {
            Ekle.Enabled = !string.IsNullOrWhiteSpace(Yeni.Text);
        }

        private void Ekle_Click(object sender, System.EventArgs e)
        {
            if (Banka.İşTürü_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Önceden eklenmiş", Text);
                return;
            }

            Banka.İşTürü_Ekle(Yeni.Text);
            Banka.Değişiklikleri_Kaydet();
            Liste.Items.Add(Yeni.Text);
        }
    }
}
