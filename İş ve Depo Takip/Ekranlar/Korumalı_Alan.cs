using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.IO;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Korumalı_Alan : Form
    {
        public Korumalı_Alan()
        {
            InitializeComponent();

            var ayrl = Banka.ListeKutusu_Ayarlar(true, false);
            ayrl.Silinebilir = true;
            ayrl.ElemanKonumu = ArgeMup.HazirKod.Ekranlar.ListeKutusu.Ayarlar_.ElemanKonumu_.AdanZyeSıralanmış;
            Liste.Başlat(null, Banka.KorumalıAlan_Listele_Dosyalar(), "Dosya ve klasörler", ayrl);
            Liste.GeriBildirim_İşlemi += Liste_GeriBildirim_İşlemi;

            ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Sürümler);
        }
        private void KorumalıAlan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                foreach (string eleman in Banka.KorumalıAlan_Listele_Dosyalar())
                {
                    if (eleman.StartsWith(":"))
                    {
                        //klasör
                        string açık_hali = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + eleman.Substring(1);
                        if (Directory.Exists(açık_hali))
                        {
                            DialogResult Dr = MessageBox.Show("Alttaki klasörün masaüstünüzde bulunuyor olması güvenlik zaafiyeti oluşturacak." + Environment.NewLine + Environment.NewLine +
                                "Hayır tuşuna basıp" + Environment.NewLine +
                                "Klasörü pencere içerisine sürükleyerek son halinin kaydedilmesi" + Environment.NewLine +
                                "Masaüstündeki kopyanın el ile KALICI olarak silinmesi" + Environment.NewLine +
                                "TAVSİYE EDİLİR." + Environment.NewLine + Environment.NewLine + açık_hali + Environment.NewLine + Environment.NewLine +
                                "Yinede pencereyi kapatıp devam etmek istiyor musunuz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); ;

                            e.Cancel = Dr == DialogResult.No;
                            return;
                        }
                    }
                    else
                    {
                        //Dosya
                        string açık_hali = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + eleman;
                        if (File.Exists(açık_hali))
                        {
                            DialogResult Dr = MessageBox.Show("Alttaki dosyanın masaüstünüzde bulunuyor olması güvenlik zaafiyeti oluşturacak." + Environment.NewLine + Environment.NewLine +
                                "Hayır tuşuna basıp" + Environment.NewLine + 
                                "Dosyayı pencere içerisine sürükleyerek son halinin kaydedilmesi" + Environment.NewLine +
                                "Masaüstündeki kopyanın el ile KALICI olarak silinmesi" + Environment.NewLine +
                                "TAVSİYE EDİLİR." + Environment.NewLine + Environment.NewLine + açık_hali + Environment.NewLine + Environment.NewLine +
                                "Yinede pencereyi kapatıp devam etmek istiyor musunuz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);;
                                
                            e.Cancel = Dr == DialogResult.No;
                            return;
                        }
                    }
                }
            }
        }
        void KorumalıAlan_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        void KorumalıAlan_DragDrop(object sender, DragEventArgs e)
        {
            string[] dosyalar = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dosyalar != null && dosyalar.Length > 0)
            {
                try
                {
                    foreach (string dosya in dosyalar)
                    {
                        Banka.KorumalıAlan_Ekle(dosya);

                        string adı = Path.GetFileName(dosya);
                        if (!File.Exists(dosya) && Directory.Exists(dosya)) adı = ":" + adı;
                        if (!Liste.Tüm_Elemanlar.Contains(adı)) Liste.Tüm_Elemanlar.Add(adı);
                    }

                    Banka.Değişiklikleri_Kaydet(null);
                    Liste.Yenile();
                }
                catch (Exception ex)
                {
                    Banka.Değişiklikler_TamponuSıfırla();
                    MessageBox.Show("İşlem başarısız, gerekli izin veya yetkilere sahip olduğunuzu teyit ediniz." + Environment.NewLine + Environment.NewLine + ex.Günlük().Message, Text);
                }
            }
        }

        private bool Liste_GeriBildirim_İşlemi(string Adı, ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü Türü, string YeniAdı = null)
        {
            MasaüstüneKopyala.Enabled = false;

            if (Adı.BoşMu()) return false;

            switch (Türü)
            {
                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.ElemanSeçildi:
                    ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Sürümler, Banka.KorumalıAlan_Listele_Sürümler(Adı));
                    if (Sürümler.Items.Count > 0) Sürümler.SelectedIndex = 0;
                    break;

                case ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.Silindi:
                    string soru = Adı + " dosyası tüm sürümleriyle birlikte KALICI olarak SİLİNECEK." + Environment.NewLine + Environment.NewLine +
                       "Geri Dönüşüm Kutusundan ERİŞİLEMEYECEK." + Environment.NewLine + Environment.NewLine +
                       "İşleme devam etmek istiyor musunuz?";
                    DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Dr == DialogResult.No) return false;

                    Banka.KorumalıAlan_Sil(Adı);
                    Banka.Değişiklikleri_Kaydet(Liste);
                    break;
            }

            return true;
        }

        private void Sürümler_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasaüstüneKopyala.Enabled = true;
        }
        private void Sürümler_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MasaüstüneKopyala_Click(null, null);
        }
        private void MasaüstüneKopyala_Click(object sender, EventArgs e)
        {
            string dsy_kls_adı = Liste.SeçilenEleman_Adı;
            if (dsy_kls_adı.BoşMu() || Sürümler.SelectedIndex < 0) return;

            if (dsy_kls_adı.StartsWith(":"))
            {
                //klasör
                string açık_hali = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + dsy_kls_adı.Substring(1);
                if (Directory.Exists(açık_hali))
                {
                    MessageBox.Show("Seçtiğniz klasör masaüstünde mevcut. Üzerine yazıp bilgi kaybına sebep olmamak için işlem duraklatıldı." + Environment.NewLine + Environment.NewLine +
                        "Devam edebilmek için masaüstünde aynı isimli klasör bulunmadığından emin olunuz", Text);
                    Ortak.Çalıştır.DosyaGezginindeGöster(açık_hali);
                    return;
                }
            }
            else
            {
                //dosya
                string açık_hali = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + dsy_kls_adı;
                if (File.Exists(açık_hali))
                {
                    MessageBox.Show("Seçtiğniz dosya masaüstünde mevcut. Üzerine yazıp bilgi kaybına sebep olmamak için işlem duraklatıldı." + Environment.NewLine + Environment.NewLine +
                        "Devam edebilmek için masaüstünde aynı isimli dosya bulunmadığından emin olunuz", Text);
                    Ortak.Çalıştır.DosyaGezginindeGöster(açık_hali);
                    return;
                }
            }

            Banka.KorumalıAlan_MasaüstüneKopyala(dsy_kls_adı, Sürümler.Text);
        }

        private void İçeriAl_Click(object sender, EventArgs e)
        {
            DialogResult Dr = MessageBox.Show("Devam etmeden önce LÜTFEN" + Environment.NewLine +
                "Korunan içeriğin başka uygulamalar üzerinde" + Environment.NewLine + "AÇIK OLMADIĞINDAN emin olunuz" + Environment.NewLine + Environment.NewLine +
                "İşleme devam etmek istiyor musunuz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); ;
            if (Dr == DialogResult.No) return;

            try
            {
                foreach (string eleman in Banka.KorumalıAlan_Listele_Dosyalar())
                {
                    if (eleman.StartsWith(":"))
                    {
                        //klasör
                        string açık_hali = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + eleman.Substring(1);
                        if (Directory.Exists(açık_hali))
                        {
                            Banka.KorumalıAlan_Ekle(açık_hali);
                            if (!Klasör.Sil(açık_hali)) throw new Exception("Klasör silinemedi" + Environment.NewLine + açık_hali);
                        }
                    }
                    else
                    {
                        //Dosya
                        string açık_hali = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + eleman;
                        if (File.Exists(açık_hali))
                        {
                            Banka.KorumalıAlan_Ekle(açık_hali);
                            if (!Dosya.Sil(açık_hali)) throw new Exception("Dosya silinemedi" + Environment.NewLine + açık_hali);
                        }
                    }

                    Liste.SeçilenEleman_Adı = eleman;
                }

                Banka.Değişiklikleri_Kaydet(Liste);
            }
            catch (Exception ex)
            {
                Banka.Değişiklikler_TamponuSıfırla();
                MessageBox.Show("Bİr sorunla karşılaşıldı. Lütfen tekrar deneyiniz" + Environment.NewLine + Environment.NewLine + ex.Günlük().Message, Text);
            }
        }
    }
}