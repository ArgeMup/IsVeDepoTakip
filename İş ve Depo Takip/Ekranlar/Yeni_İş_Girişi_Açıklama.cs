using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Yeni_İş_Girişi_Açıklama : Form
    {
        IDepo_Eleman depo = null;

        public Yeni_İş_Girişi_Açıklama()
        {
            InitializeComponent();

            depo = Banka.Tablo_Dal(null, Banka.TabloTürü.Etiket_Açıklamaları, "Açıklamalar", true);
            Açıklamalar.Başlat(null, depo.İçeriği.ToList(), "Etiket Açıklamaları",
                new ArgeMup.HazirKod.Ekranlar.ListeKutusu.Ayarlar_(false, false, ArgeMup.HazirKod.Ekranlar.ListeKutusu.Ayarlar_.ElemanKonumu_.AdanZyeSıralanmış, true, false, İşlemYapmadanÖnceSor:false));
            Açıklamalar.GeriBildirim_İşlemi += Açıklamalar_GeriBildirim_İşlemi;
        }

        private bool Açıklamalar_GeriBildirim_İşlemi(string Adı, ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü Türü, string YeniAdı = null)
        {
            switch (Türü)
            {
                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.ElemanSeçildi:
                    SeçiliOlanAçıklama.Text = Açıklamalar.SeçilenEleman_Adı;
                    return true;
                
                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.Silindi:
                    var l = depo.İçeriği.ToList();
                    l.Remove(Adı);
                    depo.İçeriği = l.ToArray();
                    return true;

                default:
                    return false;
            }
        }

        private void SağaAkar_Click(object sender, EventArgs e)
        {
            Çıktı.AppendText((Çıktı.Text.DoluMu() ? " " : null) + SeçiliOlanAçıklama.Text);
        }

        private void ListeyeEkle_Click(object sender, EventArgs e)
        {
            Çıktı.Text = Çıktı.Text.Trim();
            if (Çıktı.Text.DoluMu(true) && !depo.İçeriği.Contains(Çıktı.Text))
            {
                Açıklamalar.Tüm_Elemanlar.Add(Çıktı.Text);
                Açıklamalar.Yenile();
                depo.İçeriği = Açıklamalar.Tüm_Elemanlar.ToArray();
            }
        }

        private void Geri_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void YazdırVeKaydet_Click(object sender, EventArgs e)
        {
            Çıktı.Text = Çıktı.Text.Trim();
            if (Çıktı.Text.BoşMu()) return;

            YazdırVeKaydet.Tag = Çıktı.Text;

            Close();
        }
    }
}
