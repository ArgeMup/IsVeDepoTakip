using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class KorumalıAlan : Form
    {
        public KorumalıAlan()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            AramaÇubuğu_Liste = Banka.KorumalıAlan_Listele_Dosyalar();
            Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste);
            Ortak.GrupArayıcı(Sürümler);
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
                        string açık_hali = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + eleman.Substring(1);
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
                        string açık_hali = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + eleman;
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
            splitContainer1.Enabled = false;
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
                        if (!AramaÇubuğu_Liste.Contains(adı)) AramaÇubuğu_Liste.Add(adı);
                    }

                    Banka.Değişiklikleri_Kaydet(null);
                    Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste, AramaÇubuğu.Text);
                }
                catch (Exception ex)
                {
                    Banka.Değişiklikler_TamponuSıfırla();
                    MessageBox.Show("İşlem başarısız, gerekli izin veya yetkilere sahip olduğunuzu teyit ediniz." + Environment.NewLine + Environment.NewLine + ex.Günlük().Message, Text);
                }
            }

            splitContainer1.Enabled = true;
        }

        List<string> AramaÇubuğu_Liste = null;
        private void AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            Sil.Enabled = false;
         
            Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste, AramaÇubuğu.Text);
        }

        private void Liste_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0) { splitContainer1.Panel2.Enabled = false; return; }

            Ortak.GrupArayıcı(Sürümler, Banka.KorumalıAlan_Listele_Sürümler(Liste.Text));
            if (Sürümler.Items.Count > 0) Sürümler.SelectedIndex = 0;

            splitContainer1.Panel2.Enabled = true;
            Sil.Enabled = true;
        }
        private void Sürümler_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasaüstüneKopyala.Enabled = true;
        }
        private void Sürümler_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MasaüstüneKopyala_Click(null, null);
        }

        private void Sil_Click(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0) return;

            string soru = Liste.Text + " dosyası tüm sürümleriyle birlikte KALICI olarak SİLİNECEK." + Environment.NewLine + Environment.NewLine +
                "Geri Dönüşüm Kutusundan ERİŞİLEMEYECEK." + Environment.NewLine + Environment.NewLine +
                "İşleme devam etmek istiyor musunuz?";
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            splitContainer1.Enabled = false;
            Sil.Enabled = false;

            Banka.KorumalıAlan_Sil(Liste.Text);
            Banka.Değişiklikleri_Kaydet(null);

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste, AramaÇubuğu.Text);
            splitContainer1.Enabled = true;
        }

        private void MasaüstüneKopyala_Click(object sender, EventArgs e)
        {
            if (Liste.SelectedIndex < 0 || Sürümler.SelectedIndex < 0) return;

            if (Liste.Text.StartsWith(":"))
            {
                //klasör
                string açık_hali = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + Liste.Text.Substring(1);
                if (Directory.Exists(açık_hali))
                {
                    MessageBox.Show("Seçtiğniz klasör masaüstünde mevcut. Üzerine yazıp bilgi kaybına sebep olmamak için işlem duraklatıldı." + Environment.NewLine + Environment.NewLine +
                        "Devam edebilmek için masaüstünde aynı isimli klasör bulunmadığından emin olunuz", Text);
                    System.Diagnostics.Process.Start("explorer.exe", "/select, " + açık_hali);
                    return;
                }
            }
            else
            {
                //dosya
                string açık_hali = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + Liste.Text;
                if (File.Exists(açık_hali))
                {
                    MessageBox.Show("Seçtiğniz dosya masaüstünde mevcut. Üzerine yazıp bilgi kaybına sebep olmamak için işlem duraklatıldı." + Environment.NewLine + Environment.NewLine +
                        "Devam edebilmek için masaüstünde aynı isimli dosya bulunmadığından emin olunuz", Text);
                    System.Diagnostics.Process.Start("explorer.exe", "/select, " + açık_hali);
                    return;
                }
            }

            splitContainer1.Enabled = false;
            Banka.KorumalıAlan_MasaüstüneKopyala(Liste.Text, Sürümler.Text);
            splitContainer1.Enabled = true;
        }

        private void İçeriAl_Click(object sender, EventArgs e)
        {
            DialogResult Dr = MessageBox.Show("Devam etmeden önce LÜTFEN" + Environment.NewLine +
                "Korunan içeriğin başka uygulamalar üzerinde" + Environment.NewLine + "AÇIK OLMADIĞINDAN emin olunuz" + Environment.NewLine + Environment.NewLine +
                "İşleme devam etmek istiyor musunuz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); ;
            if (Dr == DialogResult.No) return;

            splitContainer1.Enabled = false;
            try
            {
                foreach (string eleman in Banka.KorumalıAlan_Listele_Dosyalar())
                {
                    if (eleman.StartsWith(":"))
                    {
                        //klasör
                        string açık_hali = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + eleman.Substring(1);
                        if (Directory.Exists(açık_hali))
                        {
                            Banka.KorumalıAlan_Ekle(açık_hali);
                            if (!Klasör.Sil(açık_hali)) throw new Exception("Klasör silinemedi" + Environment.NewLine + açık_hali);
                        }
                    }
                    else
                    {
                        //Dosya
                        string açık_hali = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + eleman;
                        if (File.Exists(açık_hali))
                        {
                            Banka.KorumalıAlan_Ekle(açık_hali);
                            if (!Dosya.Sil(açık_hali)) throw new Exception("Dosya silinemedi" + Environment.NewLine + açık_hali);
                        }
                    }

                    Liste.Text = eleman;
                }

                Banka.Değişiklikleri_Kaydet(null);
                Liste_SelectedValueChanged(null, null);
            }
            catch (Exception ex)
            {
                Banka.Değişiklikler_TamponuSıfırla();
                MessageBox.Show("Bİr sorunla karşılaşıldı. Lütfen tekrar deneyiniz" + Environment.NewLine + Environment.NewLine + ex.Günlük().Message, Text);
            }
            splitContainer1.Enabled = true;
        }
    }
}