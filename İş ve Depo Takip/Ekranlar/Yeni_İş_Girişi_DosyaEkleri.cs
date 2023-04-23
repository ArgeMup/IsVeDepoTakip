using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Yeni_İş_Girişi_DosyaEkleri : Form
    {
        public bool SadeceOkunabilir = false, DeğişiklikYapıldı = false;
        public string SeriNo = null;
        public Action ÖnYüzGörseliniGüncelle = null;

        public Yeni_İş_Girişi_DosyaEkleri()
        {
            InitializeComponent();
        }
        public void Yeni_İş_Girişi_DragEnter(object sender, DragEventArgs e)
        {
            if (SadeceOkunabilir) return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        public void Yeni_İş_Girişi_DragDrop(object sender, DragEventArgs e)
        {
            if (SadeceOkunabilir) return;

            string[] dosyalar = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dosyalar != null && dosyalar.Length > 0)
            {
                foreach (string dosya in dosyalar)
                {
                    if (System.IO.File.Exists(dosya))
                    {
                        _Ekle_(dosya);
                    }
                    else if (System.IO.Directory.Exists(dosya))
                    {
                        string[] kls_içindeki_dosyalar = System.IO.Directory.GetFiles(dosya, "*.*", System.IO.SearchOption.AllDirectories);
                        foreach (string d in kls_içindeki_dosyalar) _Ekle_(d);
                    }
                }

                void _Ekle_(string Girdi)
                {
                    if (System.IO.Path.GetExtension(Girdi) == ".zip")
                    {
                        string soru = "Zip dosyasının kendisini değil içindeki dosyaları dahil etmek ister misiniz?" + Environment.NewLine + Environment.NewLine +
                            "Evet : Zip dosyası bir klasöre geçici olarak çıkartılır ve dosyalar dahil edilir." + Environment.NewLine +
                            "Hayır : Zip dosyası olduğu gibi dahil edilir." + Environment.NewLine +
                            "İptal : Zip dosyası görmezden gelinir";

                        DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                        if (Dr == DialogResult.Cancel) return;
                        else if (Dr == DialogResult.Yes)
                        {
                            string gecici_kls = Ortak.Klasör_Gecici + System.IO.Path.GetRandomFileName();
                            while (System.IO.Directory.Exists(gecici_kls)) gecici_kls = Ortak.Klasör_Gecici + System.IO.Path.GetRandomFileName();
                            SıkıştırılmışDosya.Klasöre(Girdi, gecici_kls);

                            foreach (string dsy in System.IO.Directory.GetFiles(gecici_kls, "*.*", System.IO.SearchOption.AllDirectories))
                            {
                                _Ekle_(dsy);
                            }
                            return;
                        }
                    }

                    if (!P_DosyaEkleri_Liste.Items.Contains(Girdi))
                    {
                        P_DosyaEkleri_Liste.Items.Add(Girdi);
                        DeğişiklikYapıldı = true;
                    }
                }
            }

            ÖnYüzGörseliniGüncelle?.Invoke();
        }

        private void P_DosyaEkleri_Liste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P_DosyaEkleri_Resim.Image != null)
            {
                P_DosyaEkleri_Resim.Image.Dispose();
                P_DosyaEkleri_Resim.Image = null;
            }
            P_DosyaEkleri_MasaüstüneKopyala.Enabled = false;

            P_DosyaEkleri_Sil.Enabled = false;
            if (P_DosyaEkleri_Liste.SelectedIndex < 0) return;
            string SahteKonum = null;

            if (File.Exists(P_DosyaEkleri_Liste.Text))
            {
                //yeni eklenen
                SahteKonum = P_DosyaEkleri_Liste.Text;
            }
            else
            {
                //önceden kaydedilen
                if (SeriNo.DoluMu())
                {
                    SahteKonum = Banka.DosyaEkleri_GeciciKlasöreKopyala(SeriNo, P_DosyaEkleri_Liste.Text);
                }
            }

            if (SahteKonum.BoşMu() || !System.IO.File.Exists(SahteKonum))
            {
                MessageBox.Show("Dosya açılamadı" + Environment.NewLine + Environment.NewLine + P_DosyaEkleri_Liste.Text, Text);
            }
            else
            {
                string soyad = Path.GetExtension(SahteKonum).ToLower();
                if (soyad == ".png" || soyad == ".bmp" || soyad == ".jpg")
                {
                    P_DosyaEkleri_YakınlaşmaOranı.Value = 1;
                    Image rsm = Image.FromFile(SahteKonum);
                    P_DosyaEkleri_Resim.Size = rsm.Size;
                    P_DosyaEkleri_Resim.Image = rsm;
                }

                P_DosyaEkleri_MasaüstüneKopyala.Enabled = true;
            }

            P_DosyaEkleri_Sil.Enabled = !SadeceOkunabilir;
        }
        private void P_DosyaEkleri_YakınlaşmaOranı_ValueChanged(object sender, EventArgs e)
        {
            if (P_DosyaEkleri_Resim.Image == null) return;
            P_DosyaEkleri_Resim.Size = new Size((int)(P_DosyaEkleri_Resim.Image.Size.Width * P_DosyaEkleri_YakınlaşmaOranı.Value), (int)(P_DosyaEkleri_Resim.Image.Size.Height * P_DosyaEkleri_YakınlaşmaOranı.Value));
        }
        private void P_DosyaEkleri_MasaüstüneKopyala_Click(object sender, EventArgs e)
        {
            if (P_DosyaEkleri_Liste.SelectedIndex < 0) return;
            string SahteKonum = null, masaüstü_yansıma_adı = null;

            if (File.Exists(P_DosyaEkleri_Liste.Text))
            {
                //yeni eklenen
                SahteKonum = P_DosyaEkleri_Liste.Text;
                masaüstü_yansıma_adı = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + Path.GetFileName(P_DosyaEkleri_Liste.Text);
            }
            else
            {
                //önceden kaydedilen
                if (SeriNo.DoluMu())
                {
                    SahteKonum = Banka.DosyaEkleri_GeciciKlasöreKopyala(SeriNo, P_DosyaEkleri_Liste.Text);
                    masaüstü_yansıma_adı = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + P_DosyaEkleri_Liste.Text;
                }
            }

            if (SahteKonum.BoşMu() || !File.Exists(SahteKonum) || masaüstü_yansıma_adı.BoşMu(true))
            {
                MessageBox.Show("Dosya açılamadı" + Environment.NewLine + Environment.NewLine + P_DosyaEkleri_Liste.Text, Text);
            }
            else
            {
                if (File.Exists(masaüstü_yansıma_adı))
                {
                    MessageBox.Show("Masaüstünde aynı isimli bir dosya bulunduğundan işlem durduruldu", Text);
                    return;
                }

                File.Copy(SahteKonum, masaüstü_yansıma_adı, true);
            }
        }
        private void P_DosyaEkleri_Sil_Click(object sender, EventArgs e)
        {
            if (P_DosyaEkleri_Liste.SelectedIndex < 0) return;

            DialogResult Dr = MessageBox.Show("Seçtiğniz öğeyi silmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            P_DosyaEkleri_Liste.Items.RemoveAt(P_DosyaEkleri_Liste.SelectedIndex);
            DeğişiklikYapıldı = true;

            ÖnYüzGörseliniGüncelle?.Invoke();
        }
    }
}
