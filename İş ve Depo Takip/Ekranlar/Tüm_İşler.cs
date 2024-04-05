using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Tüm_İşler : Form, IGüncellenenSeriNolar
    {
        bool İzin_Ayarlar, İzin_Tamamlandı;
        public Tüm_İşler()
        {
            InitializeComponent();

            //görsel çiziminin iyileşmsi için
            typeof(Control).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, Tablo, new object[] { DoubleBuffered });

            İzin_Ayarlar = Banka.K_lar.İzinliMi(Banka.K_lar.İzin.Ayarları_değiştirebilir);
            İzin_Tamamlandı = İzin_Ayarlar ? true : Banka.K_lar.İzinliMi(Banka.K_lar.İzin.Tamamlanmış_işler_içinde_işlem_yapabilir);

            İşTakip_Müşteriler.Başlat(null, Banka.Müşteri_Listele(), "Müşteriler", Banka.ListeKutusu_Ayarlar(true, false));
            İşTakip_Müşteriler.GeriBildirim_İşlemi += İşTakip_Müşteriler_GeriBildirim_İşlemi;
            
            İşTakip_Ödendi_Dönem.GeriBildirim_İşlemi += İşTakip_Ödendi_Dönem_GeriBildirim_İşlemi;
            Malzemeler_Malzeme.GeriBildirim_İşlemi += Malzemeler_Malzeme_GeriBildirim_İşlemi;

            var ayrl_arama = Banka.ListeKutusu_Ayarlar(true, true);
            ayrl_arama.GizliOlanlarıGöster = İzin_Tamamlandı;
            Arama_Müşteriler.Başlat(null, Banka.Müşteri_Listele(true), "Müşteriler", ayrl_arama);
            Arama_İş_Türleri.Başlat(null, Banka.İşTürü_Listele(), "İş Türleri", ayrl_arama);

            IDepo_Eleman Ayrl_Kullanıcı = Banka.Ayarlar_Kullanıcı(Name, null);
            İşTakip_Yazdırma_VeGörüntüle.Checked = Ayrl_Kullanıcı.Oku_Bit("İşTakip_Yazdırma_VeGörüntüle", true);
            İşTakip_Yazdırma_VeKlasörüAç.Checked = Ayrl_Kullanıcı.Oku_Bit("İşTakip_Yazdırma_VeKlasörüAç", true);

            DateTime t = DateTime.Now;
            Arama_GirişTarihi_Bitiş.Value = new DateTime(t.Year, t.Month, t.Day, 23, 59, 59);
            t = t.AddMonths(-6);
            Arama_GirişTarihi_Başlangıç.Value = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0);

            P_Üst_Alt.Panel1.Controls.Add(P_SolOrta_Sağ); P_SolOrta_Sağ.Dock = DockStyle.Fill; P_SolOrta_Sağ.Visible = true;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_DevamEden); P_İşTakip_DevamEden.Dock = DockStyle.Fill; P_İşTakip_DevamEden.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_TeslimEdildi); P_İşTakip_TeslimEdildi.Dock = DockStyle.Fill; P_İşTakip_TeslimEdildi.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_ÖdemeBekleyen); P_İşTakip_ÖdemeBekleyen.Dock = DockStyle.Fill; P_İşTakip_ÖdemeBekleyen.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_Ödendi); P_İşTakip_Ödendi.Dock = DockStyle.Fill; P_İşTakip_Ödendi.Visible = false;
            P_Üst_Alt.Panel1.Controls.Add(P_Arama); P_Arama.Dock = DockStyle.Fill; P_Arama.Visible = false;
            P_Üst_Alt.Panel1.Controls.Add(P_Malzemeler); P_Malzemeler.Dock = DockStyle.Fill; P_Malzemeler.Visible = false;
            P_Üst_Alt.Panel1.Controls.Remove(P_Üstteki_İşTakip_Arama_Tip); P_Üst_Alt.Panel1.Controls.Add(P_Üstteki_İşTakip_Arama_Tip);

            P_SolOrta_Sağ.SplitterDistance = P_SolOrta_Sağ.Width * 70 / 100; //müşteriler, tuşlar + yazdırma
            P_Sol_Orta.SplitterDistance = P_Sol_Orta.Width * 30 / 100; //müşteriler + tuşları
            P_Üst_Alt.SplitterDistance = Height * 25 / 100; //tuşlar + tablo

            Seviye1_işTakip.Tag = 1;
            Seviye1_Arama.Tag = 2;
            Seviye2_DevamEden.Tag = 10;
            Seviye2_TeslimEdildi.Tag = 11;
            Seviye2_ÖdemeBekleyen.Tag = 12;
            Seviye2_Ödendi.Tag = 13;

            Logo.Image = Ortak.Firma_Logo;

            if (!İzin_Tamamlandı)
            {
                İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Visible = false;
                İşTakip_TeslimEdildi_İlaveÖdeme.Visible = false;
                İşTakip_TeslimEdildi_Sekmeler.TabPages.Remove(İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl);
                İşTakip_TeslimEdildi_ÖdemeTalebiOluştur.Visible = false;
                İşTakip_TeslimEdildi_Açıklama.Visible = false;
                Müşteri_KDV.Visible = false;
                Müşteri_İskonto.Visible = false;
                Seviye2_ÖdemeBekleyen.Visible = false;
                Seviye2_Ödendi.Visible = false;
                P_Yazdırma.Visible = false;
                İşTakip_Eposta_Kişiye.Visible = false;
            }
        }
        public int AramaPenceresiniAç(DateTime? BaşlangıçTarihi = null, DateTime? BitişTarihi = null, string Müşteri = null, string TabloİçeriğiArama_İçeriği = null, bool SadeceHastaAdındaAra = false, bool Ve0_Veya1 = false)
        {
            if (BaşlangıçTarihi != null) Arama_GirişTarihi_Başlangıç.Value = BaşlangıçTarihi.Value;
            if (BitişTarihi != null) Arama_GirişTarihi_Bitiş.Value = BitişTarihi.Value;
            if (Müşteri != null) { Arama_Müşteriler.Tüm_Elemanlar = new List<string>() { Müşteri }; Arama_Müşteriler.Yenile(); }

            Seviye_Değişti(Seviye1_Arama, null);

            TabloİçeriğiArama_SadeceHastaAdındaAra.Checked = SadeceHastaAdındaAra;
            TabloİçeriğiArama_VeVeya.Checked = Ve0_Veya1;

            if (TabloİçeriğiArama_İçeriği != null) TabloİçeriğiArama.Text = TabloİçeriğiArama_İçeriği;

            return Tablo.Rows.GetRowCount(DataGridViewElementStates.Visible);
        }
        private void Tüm_İşler_Shown(object sender, EventArgs e)
        {
            if (Seviye1_işTakip.Checked)
            {
                if ((int)Seviye1_işTakip.Tag == 1) İşTakip_Müşteriler.Odaklan();
                else Malzemeler_Malzeme.Odaklan();
            }
            else 
            {
                Tablo.ClearSelection();
                TabloİçeriğiArama.Focus(); 
            }
        }

        private void Seviye_Değişti(object sender, EventArgs e)
        {
            int no = 3; //boşsa illa ki başa dön
            if (sender != null)
            {
                no = (int)(sender as CheckBox).Tag;
                if (no < 1) return;
            }

            Banka_Tablo_ bt;
            Tablo.Rows.Clear();
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;

            switch (no)
            {
                case 1:
                    //iş takip
                    if (Seviye1_işTakip.Checked && İzin_Tamamlandı)
                    {
                        //malzeme kullanım detayı sayfasına geç
                        Seviye1_işTakip.Tag = 3;
                        Seviye1_işTakip.FlatAppearance.CheckedBackColor = Color.Khaki;
                        P_Malzemeler.Visible = true;
                        P_SolOrta_Sağ.Visible = false;

                        var ayrl = Banka.ListeKutusu_Ayarlar(true, false);
                        ayrl.ElemanKonumu = ArgeMup.HazirKod.Ekranlar.ListeKutusu.Ayarlar_.ElemanKonumu_.AdanZyeSıralanmış;
                        Malzemeler_Malzeme.Başlat(null, Banka.Malzeme_Listele(true), "Malzemeler", ayrl);
                    }
                    else
                    {
                        //İş türleri
                        P_Malzemeler.Visible = false;
                        Seviye1_işTakip.Checked = true;
                        Seviye1_Arama.Checked = false;

                        CheckBox c = null;
                        if (Seviye2_DevamEden.Checked) c = Seviye2_DevamEden;
                        else if (Seviye2_TeslimEdildi.Checked) c = Seviye2_TeslimEdildi;
                        else if (Seviye2_ÖdemeBekleyen.Checked) c = Seviye2_ÖdemeBekleyen;
                        else if (Seviye2_Ödendi.Checked) c = Seviye2_Ödendi;
                        if (c != null) Seviye_Değişti(c, null);
                    }
                    break;

                case 2:
                    //arama
                    if ((int)Seviye1_işTakip.Tag != 1) Seviye_Değişti(Seviye1_işTakip, null);

                    Arama_Müşteriler.SeçilenEleman_Adları = Arama_Müşteriler.Tüm_Elemanlar.Where(x => !x.StartsWith(".:Gizli:. ")).ToList();
                    Arama_İş_Türleri.SeçilenEleman_Adları = Arama_İş_Türleri.Tüm_Elemanlar.Where(x => !x.StartsWith(".:Gizli:. ")).ToList();

                    Seviye1_işTakip.Checked = false;
                    Seviye1_Arama.Checked = true;
                    break;

                case 3:
                    //Malzeme kullanım detay sayfasından iş takip sayfasına dön
                    Seviye1_işTakip.FlatAppearance.CheckedBackColor = Color.SkyBlue;
                    Seviye1_işTakip.Tag = 1;
                    Seviye1_işTakip.Checked = false;

                    Seviye_Değişti(Seviye1_işTakip, null);
                    break;

                case 10:
                    //devam eden
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

                    Seviye2_DevamEden.Checked = true;
                    Seviye2_TeslimEdildi.Checked = false;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = false;
                    
                    İşTakip_DevamEden_ÜcretHesaplama.Checked = false;
                    
                    if (Banka.Müşteri_MevcutMu(MüşteriAdı))
                    {
                        bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.DevamEden);
                        Banka.Talep_TablodaGöster(Tablo, bt);
                    }
                    break;

                case 11:
                    //teslim edildi 
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

                    Seviye2_DevamEden.Checked = false;
                    Seviye2_TeslimEdildi.Checked = true;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = false;

                    if (Banka.Müşteri_MevcutMu(MüşteriAdı))
                    {
                        bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.TeslimEdildi);
                        Banka.Talep_TablodaGöster(Tablo, bt);
                    }
                    break;

                case 12:
                    //ödeme bekliyor
                    if (!İzin_Tamamlandı) return;
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

                    Seviye2_DevamEden.Checked = false;
                    Seviye2_TeslimEdildi.Checked = false;
                    Seviye2_ÖdemeBekleyen.Checked = true;
                    Seviye2_Ödendi.Checked = false;

                    İşTakip_ÖdemeBekleyen_Dönem.Text = null;
                    İşTakip_ÖdemeBekleyen_Dönem.Items.Clear();
                    if (Banka.Müşteri_MevcutMu(MüşteriAdı))
                    {
                        İşTakip_ÖdemeBekleyen_Dönem.Items.AddRange(Banka.Dosya_Listele_Müşteri(MüşteriAdı, false));
                        if (İşTakip_ÖdemeBekleyen_Dönem.Items.Count > 0)
                        {
                            if (İşTakip_ÖdemeBekleyen_Dönem.SelectedIndex != 0) İşTakip_ÖdemeBekleyen_Dönem.SelectedIndex = 0;
                        }
                    }
                    break;

                case 13:
                    //ödendi
                    if (!İzin_Tamamlandı) return;
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

                    Seviye2_DevamEden.Checked = false;
                    Seviye2_TeslimEdildi.Checked = false;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = true;

                    İşTakip_Ödendi_Dönem.Başlat(null, Banka.Müşteri_MevcutMu(MüşteriAdı) ? Banka.Dosya_Listele_Müşteri(MüşteriAdı, true).ToList() : null, "Dönemler", Banka.ListeKutusu_Ayarlar(true, true));
                    break;
            }

            if ((int)Seviye1_işTakip.Tag == 1)
            {
                //işler sayfası
                İşTakip_Eposta_Ödendi.Text = "Ödendi : Son dönem";
                Seviye2_DevamEden.Enabled = true;
                Seviye2_TeslimEdildi.Enabled = true;
                Seviye2_ÖdemeBekleyen.Enabled = true;
                Seviye2_Ödendi.Enabled = true;

                P_SolOrta_Sağ.Visible = Seviye1_işTakip.Checked;
                if (Seviye1_işTakip.Checked)
                {
                    P_İşTakip_DevamEden.Visible = Seviye2_DevamEden.Checked;
                    P_İşTakip_TeslimEdildi.Visible = Seviye2_TeslimEdildi.Checked;
                    P_İşTakip_ÖdemeBekleyen.Visible = Seviye2_ÖdemeBekleyen.Checked;
                    P_İşTakip_Ödendi.Visible = Seviye2_Ödendi.Checked;
                }

                P_Arama.Visible = Seviye1_Arama.Checked;
                if (Seviye1_Arama.Checked)
                {
                    Seviye2_DevamEden.Checked = true;
                    Seviye2_TeslimEdildi.Checked = true;
                    Seviye2_ÖdemeBekleyen.Checked = İzin_Tamamlandı;
                    Seviye2_Ödendi.Checked = İzin_Tamamlandı;
                    Arama_Sorgula_Click(null, null);
                }

                Müşteri_KDV.Enabled = Seviye1_işTakip.Checked;
            }
            else
            {
                //malzeme kullanım detayı sayfası
                Seviye2_DevamEden.Enabled = false;
                Seviye2_TeslimEdildi.Enabled = false;
                Seviye2_ÖdemeBekleyen.Enabled = false;
                Seviye2_Ödendi.Enabled = false;

                Seviye2_DevamEden.Checked = true;
                Seviye2_TeslimEdildi.Checked = false;
                Seviye2_ÖdemeBekleyen.Checked = false;
                Seviye2_Ödendi.Checked = false;

                Müşteri_KDV.Enabled = false;
            }
            Tablo_İçeriğeGöreGüncelle();
            return;

        AramaİçinSeçenekleriBelirle:
            CheckBox chcb = sender as CheckBox;
            chcb.Checked = !chcb.Checked;
        }
        
        private bool İşTakip_Müşteriler_GeriBildirim_İşlemi(string MüşteriAdı, ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü Türü, string YeniAdı = null)
        {
            if (MüşteriAdı.BoşMu() || Türü != ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.ElemanSeçildi) return false;

            CheckBox c = null;
            if (Seviye2_DevamEden.Checked) c = Seviye2_DevamEden;
            else if (Seviye2_TeslimEdildi.Checked) c = Seviye2_TeslimEdildi;
            else if (Seviye2_ÖdemeBekleyen.Checked) c = Seviye2_ÖdemeBekleyen;
            else if (Seviye2_Ödendi.Checked) c = Seviye2_Ödendi;
            if (c != null) Seviye_Değişti(c, null);

            //İlave Ödeme Detayları
            İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text = null;
            İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Text = null;
            İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked = false;
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Notlar.Text = null;
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Text = "0";

            //Ödeme bekleyen sayfası
            İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text = null;
            İşTakip_ÖdemeBekleyen_Notlar.Text = null;

            //eposta gönderimi için iş adetlerinin menüde gösterilemsi
            İşTakip_Eposta_DevamEden.Checked = false;
            İşTakip_Eposta_TeslimEdildi.Checked = false;
            İşTakip_Eposta_ÖdemeBekleyen.Checked = false;
            İşTakip_Eposta_Ödendi.Checked = false;
            Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.DevamEden);
            İşTakip_Eposta_DevamEden.Text = "Devam eden : " + bt.Talepler.Count + " iş";
            bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.ÜcretHesaplama);
            İşTakip_Eposta_ÜcretiHesaplanan.Text = "Ücreti hesaplanan : " + bt.Talepler.Count + " iş";
            bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.TeslimEdildi);
            İşTakip_Eposta_TeslimEdildi.Text = "Teslim edilen : " + bt.Talepler.Count + " iş";
            İşTakip_Eposta_ÖdemeBekleyen.Text = "Ödeme talebi : " + Banka.Dosya_Listele_Müşteri(MüşteriAdı, false).Length + " dönem";
            İşTakip_Eposta_Ödendi.Text = "Ödendi : Son dönem";

            //Müşteri bütçe görselleri
            Banka.Müşteri_KDV_İskonto(MüşteriAdı, out bool KDV_Ekle, out double KDV_Yüzde, out bool İskonto_Yap, out double İskonto_Yüzde, out _);
            Müşteri_KDV.CheckedChanged -= Müşteri_KDV_CheckedChanged;
            Müşteri_KDV.Checked = KDV_Ekle;
            Müşteri_KDV.CheckedChanged += Müşteri_KDV_CheckedChanged;
            İpUcu.SetToolTip(Müşteri_KDV, "KDV % " + KDV_Yüzde.Yazıya());
            Müşteri_İskonto.Checked = İskonto_Yap;
            if (Müşteri_İskonto.Checked) Müşteri_İskonto.FlatAppearance.CheckedBackColor = İskonto_Yüzde > 25 ? Color.Salmon : Color.YellowGreen;
            İpUcu.SetToolTip(Müşteri_İskonto, "İskonto % " + İskonto_Yüzde.Yazıya());

            //Müşteri Notları
            IDepo_Eleman müş = Banka.Ayarlar_Müşteri(MüşteriAdı, "Notlar");
            if (müş != null && müş[0].DoluMu(true))
            {
                Müşteri_Notlar.Checked = true;
                İpUcu.SetToolTip(Müşteri_Notlar, müş[0]);
            }
            else
            {
                Müşteri_Notlar.Checked = false;
                İpUcu.SetToolTip(Müşteri_Notlar, null);
            }

            return true;
        }
        private void İşTakip_DevamEden_Sil_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;

            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            List<string> l = new List<string>();
            List<DataGridViewRow> silinecek_satırlar = new List<DataGridViewRow>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                    silinecek_satırlar.Add(Tablo.Rows[i]);
                }
            }
            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

            DialogResult Dr = MessageBox.Show("Seçili " + l.Count + " adet öğeyi KALICI OLARAK SİLMEK istediğinize emin misiniz?" + Environment.NewLine + Environment.NewLine + "Dosya ekleri dahil herşey silinecektir.", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Talep_Sil(MüşteriAdı, l, İşTakip_DevamEden_ÜcretHesaplama.Checked);
            Banka.Değişiklikleri_Kaydet(İşTakip_DevamEden_Sil);

            foreach (DataGridViewRow s in silinecek_satırlar)
            {
                Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle((string)s.Cells[Tablo_SeriNo.Index].Value);
                Tablo.Rows.Remove(s);
            }
        }
        private void İşTakip_DevamEden_Düzenle_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;

            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            List<string> l = new List<string>();

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                }
            }

            if (l.Count != 1)
            {
                MessageBox.Show("Düzenleme yapılabilmesi için sadece 1 adet talebin seçili olduğundan emin olunuz", Text);
                return;
            }

            Ekranlar.ÖnYüzler.Ekle(new Yeni_İş_Girişi(l[0], MüşteriAdı, İşTakip_DevamEden_ÜcretHesaplama.Checked ? Banka.TabloTürü.ÜcretHesaplama : Banka.TabloTürü.DevamEden));
        }
        private void İşTakip_DevamEden_MüşteriyeGönder_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;

            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            List<string> l = new List<string>();

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                }
            }

            if (l.Count < 1)
            {
                MessageBox.Show("Düzenleme yapılabilmesi için en az 1 adet talebin seçili olduğundan emin olunuz", Text);
                return;
            }

            Banka.Talep_İşaretle_DevamEden_MüşteriyeGönderildi(MüşteriAdı, l);
            Banka.Değişiklikleri_Kaydet(İşTakip_DevamEden_MüşteriyeGönder);
            foreach (string biri in l) Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(biri);

            Seviye_Değişti(null, null);
        }
        private void İşTakip_DevamEden_İsaretle_TeslimEdildi_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            List<string> l = new List<string>();
            List<DataGridViewRow> silinecek_satırlar = new List<DataGridViewRow>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                    silinecek_satırlar.Add(Tablo.Rows[i]);
                }
            }
            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

            Banka.Talep_İşaretle_DevamEden_TeslimEdilen(MüşteriAdı, l, true);
            Banka.Değişiklikleri_Kaydet(İşTakip_DevamEden_İsaretle_TeslimEdildi);
           
            foreach (DataGridViewRow s in silinecek_satırlar)
            {
                Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle((string)s.Cells[Tablo_SeriNo.Index].Value);
                Tablo.Rows.Remove(s);
            }
        }
        private void İşTakip_DevamEden_ÜcretHesaplama_CheckedChanged(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            İşTakip_DevamEden_MüşteriyeGönder.Enabled = !İşTakip_DevamEden_ÜcretHesaplama.Checked;
            İşTakip_DevamEden_İsaretle_TeslimEdildi.Enabled = !İşTakip_DevamEden_ÜcretHesaplama.Checked;

            if (İşTakip_DevamEden_ÜcretHesaplama.Checked && Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.ÜcretHesaplama);
                Banka.Talep_TablodaGöster(Tablo, bt);
            }
            else Seviye_Değişti(Seviye2_DevamEden, null);
        }
        private void İşTakip_TeslimEdildi_İşaretle_DevamEden_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            List<string> l = new List<string>();
            List<DataGridViewRow> silinecek_satırlar = new List<DataGridViewRow>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                    silinecek_satırlar.Add(Tablo.Rows[i]);
                }
            }
            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

            Banka.Talep_İşaretle_DevamEden_TeslimEdilen(MüşteriAdı, l, false);
            Banka.Değişiklikleri_Kaydet(İşTakip_TeslimEdildi_İşaretle_DevamEden);

            foreach (DataGridViewRow s in silinecek_satırlar)
            {
                Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle((string)s.Cells[Tablo_SeriNo.Index].Value);
                Tablo.Rows.Remove(s);
            }
        }
        private void İşTakip_TeslimEdildi_ÖdemeTalebiOluştur_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            string İlaveÖdeme_Açıklama = null, İlaveÖdeme_Miktar = null;
            if (İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked)
            {
                İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text = İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text.Trim();
                if (!string.IsNullOrEmpty(İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text))
                {
                    string ilave_ödeme_miktar = İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Text;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref ilave_ödeme_miktar, "İlave ödeme Miktar kutucuğu"))
                    {
                        İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Focus();
                        return;
                    }

                    İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Text = ilave_ödeme_miktar;
                }
                else İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text = null;

                İlaveÖdeme_Açıklama = İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text;
                İlaveÖdeme_Miktar = İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Text;
            }
            
            List<string> l = new List<string>();
            List<DataGridViewRow> silinecek_satırlar = new List<DataGridViewRow>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                    silinecek_satırlar.Add(Tablo.Rows[i]);
                }
            }
            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

            İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked = false;

            string snç = Banka.Talep_İşaretle_TeslimEdilen_ÖdemeTalepEdildi(MüşteriAdı, l, İlaveÖdeme_Açıklama, İlaveÖdeme_Miktar, out _);
            if (string.IsNullOrEmpty(snç))
            {
                //başarılı
                Banka.Değişiklikleri_Kaydet(İşTakip_TeslimEdildi_ÖdemeTalebiOluştur);

                foreach (DataGridViewRow s in silinecek_satırlar)
                {
                    Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle((string)s.Cells[Tablo_SeriNo.Index].Value);
                    Tablo.Rows.Remove(s);
                }
            }
            else
            {
                Banka.Değişiklikler_TamponuSıfırla();

                DialogResult Dr = MessageBox.Show(snç + Environment.NewLine + Environment.NewLine +
                    "Ücretler sayfasını açmak ister misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Ekranlar.ÖnYüzler.Ekle(new Ayarlar_Ücretler());
            }
        }
        private void İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt_CheckedChanged(object sender, EventArgs e)
        {
            İşTakip_TeslimEdildi_İlaveÖdeme.Enabled = İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked;
        }
        private void İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama_TextChanged(object sender, EventArgs e)
        {
            İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Enabled = !string.IsNullOrWhiteSpace(İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text);
        }
        private void İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click(null, null);
            }
        }
        private void İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click(object sender, EventArgs e)
        {
            Ortak.İşTakip_TeslimEdildi_İşSeç_Seç ort_seçim = new Ortak.İşTakip_TeslimEdildi_İşSeç_Seç();
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl.Tag = ort_seçim;

            if (Tablo.RowCount < 1) return;

            string alınan_miktar = İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref alınan_miktar, "İş Seçme Miktarı", null, 0))
            {
                İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Focus();
                return;
            }
            ort_seçim.AlınanÖdeme = alınan_miktar.NoktalıSayıya();

            //Ödeme Yapılarak Ödendi Olarak İşasaretleme
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            IDepo_Eleman müş = Banka.Tablo_Dal(MüşteriAdı, Banka.TabloTürü.Ödemeler, "Mevcut Ön Ödeme");
            if (müş != null) ort_seçim.MevcutÖnÖdeme = müş.Oku_Sayı(null);

            for (int i = 0; i < Tablo.RowCount; i++) Tablo[Tablo_Seç.Index, i].Value = false;
            Banka.Müşteri_KDV_İskonto(MüşteriAdı, out bool KDV_Ekle, out double KDV_Yüzde, out bool İskonto_Yap, out double İskonto_Yüzde, out _);
            if (KDV_Ekle) ort_seçim.KDV_Oranı = KDV_Yüzde;
            if (İskonto_Yap) ort_seçim.İskonto_Oranı = İskonto_Yüzde;

            double ToplamHarcama = 0;
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                double ücreti = (double)Tablo[Tablo_İş.Index, i].Tag;
                ort_seçim.AltToplam = ücreti + ToplamHarcama;
                ort_seçim.Güncelle();

                if (ort_seçim.Hesaplanan_İşlemSonrasıÖnÖdeme < 0) break;

                ToplamHarcama += ücreti;
                Tablo[Tablo_Seç.Index, i].Value = true;
            }

            ort_seçim.AltToplam = ToplamHarcama;
            ort_seçim.Güncelle();

            İşTakip_TeslimEdildi_Açıklama.Text = ort_seçim.Yazdır();
            İşTakip_TeslimEdildi_Açıklama.ForeColor = ort_seçim.Hesaplanan_İşlemSonrasıÖnÖdeme < 0 ? Color.Red : SystemColors.ControlText;
        }
        private void İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click_2(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!P_İşTakip_TeslimEdildi.Visible || !Banka.Müşteri_MevcutMu(MüşteriAdı)) return;

            Ortak.İşTakip_TeslimEdildi_İşSeç_Seç ort_seçim = new Ortak.İşTakip_TeslimEdildi_İşSeç_Seç();
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl.Tag = ort_seçim;

            string alınan_miktar = İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref alınan_miktar, "Ödeme miktarı kutucuğu", "Kullanılmayacak ise 0 yazınız"))
            {
                İşTakip_TeslimEdildi_Sekmeler.SelectedTab = İşTakip_TeslimEdildi_Sekmeler.TabPages["İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl"];
                İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Focus();
                return;
            }
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Text = alınan_miktar;
            ort_seçim.AlınanÖdeme = alınan_miktar.NoktalıSayıya();

            //Ödeme Yapılarak Ödendi Olarak İşaretle
            IDepo_Eleman müş = Banka.Tablo_Dal(MüşteriAdı, Banka.TabloTürü.Ödemeler, "Mevcut Ön Ödeme");
            if (müş != null) ort_seçim.MevcutÖnÖdeme = müş.Oku_Sayı(null);

            Banka.Müşteri_KDV_İskonto(MüşteriAdı, out bool KDV_Ekle, out double KDV_Yüzde, out bool İskonto_Yap, out double İskonto_Yüzde, out _);
            if (KDV_Ekle) ort_seçim.KDV_Oranı = KDV_Yüzde;
            if (İskonto_Yap) ort_seçim.İskonto_Oranı = İskonto_Yüzde;

            double ToplamHarcama = 0;
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (!(bool)Tablo[Tablo_Seç.Index, i].Value) continue;

                ToplamHarcama += (double)Tablo[Tablo_İş.Index, i].Tag;
            }

            ort_seçim.AltToplam = ToplamHarcama;
            ort_seçim.Güncelle();

            İşTakip_TeslimEdildi_Açıklama.Text = ort_seçim.Yazdır();
            İşTakip_TeslimEdildi_Açıklama.ForeColor = ort_seçim.Hesaplanan_İşlemSonrasıÖnÖdeme < 0 ? Color.Red : SystemColors.ControlText;
        }
        private void İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_ÖdendiOlarakİşsaretle_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click_2(null, null);
            Ortak.İşTakip_TeslimEdildi_İşSeç_Seç ort_seçim = İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl.Tag as Ortak.İşTakip_TeslimEdildi_İşSeç_Seç;
            if (ort_seçim == null || (ort_seçim.AltToplam == 0 && ort_seçim.AlınanÖdeme == 0))
            {
                MessageBox.Show("Tabloda seçili iş bulunamadı veya aldığınız ödeme 0 ₺ olduğundan işlem ilerleyemedi", Text);
                return;
            }

            List<string> l = new List<string>();
            List<DataGridViewRow> silinecek_satırlar = new List<DataGridViewRow>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[Tablo_Seç.Index, i].Value)
                {
                    l.Add((string)Tablo[Tablo_SeriNo.Index, i].Value);
                    silinecek_satırlar.Add(Tablo.Rows[i]);
                }
            }

            string soru = (l.Count == 0 ? null : "Alttaki detayların oluşturulmasında kullanılan seçili işler KALICI olarak ÖDENDİ olarak işaretlenecek." + Environment.NewLine + Environment.NewLine) +
                "İşleme devam etmek istiyor musunuz?" + Environment.NewLine + Environment.NewLine +
                ort_seçim.Yazdır_Kısa();
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            string snç = Banka.Talep_İşaretle_TeslimEdilen_ÖdemeTalepEdildi(MüşteriAdı, l, null, null, out string DosyaAdı);
            if (!string.IsNullOrEmpty(snç))
            {
                //hatalı
                Banka.Değişiklikler_TamponuSıfırla();

                Dr = MessageBox.Show(snç + Environment.NewLine + Environment.NewLine +
                    "Ücretler sayfasını açmak ister misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Ekranlar.ÖnYüzler.Ekle(new Ayarlar_Ücretler());
                return;
            }

            if (string.IsNullOrWhiteSpace(İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Notlar.Text)) İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Notlar.Text = null;
            
            Banka.Talep_İşaretle_ÖdemeTalepEdildi_Ödendi(MüşteriAdı, DosyaAdı, İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Text, İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Notlar.Text);

            //başarılı
            Banka.Değişiklikleri_Kaydet(İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_ÖdendiOlarakİşsaretle);
            Banka.Değişiklikler_TamponuSıfırla();

            foreach (DataGridViewRow s in silinecek_satırlar)
            {
                Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle((string)s.Cells[Tablo_SeriNo.Index].Value);
                Tablo.Rows.Remove(s);
            }

            Seviye_Değişti(Seviye2_TeslimEdildi, null);
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Text = "0";
        }
        private void İşTakip_ÖdemeBekleyen_Dönem_TextChanged(object sender, EventArgs e)
        {
            İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Tag = null;
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text))
            {
                İşTakip_ÖdemeBekleyen_Açıklama.Text = null;
                return;
            }

            Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.ÖdemeTalepEdildi, İşTakip_ÖdemeBekleyen_Dönem.Text);
            Banka.Talep_TablodaGöster(Tablo, bt);
            Tablo_İçeriğeGöreGüncelle();

            bool ÖdemeİşlemiYapılmış = Banka.Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle(MüşteriAdı, ref bt.Ödeme);
            Banka.Talep_Ayıkla_ÖdemeDalı_Açıklama(bt.Ödeme, out string Açıklama, out bool İşlemSonucundaMüşteriBorçlu, out double GenelToplam);
            İşTakip_ÖdemeBekleyen_Açıklama.Text = Açıklama;
            İşTakip_ÖdemeBekleyen_Açıklama.ForeColor = İşlemSonucundaMüşteriBorçlu ? Color.Red : SystemColors.ControlText;
            İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Tag = ÖdemeİşlemiYapılmış ? GenelToplam - Banka.Müşteri_ÖnÖdemeMiktarı(MüşteriAdı) : (object)null;
        }
        private void İşTakip_ÖdemeBekleyen_İptalEt_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text))
            {
                MessageBox.Show("Lütfen geçerli bir eleman seçiniz", Text);
                İşTakip_ÖdemeBekleyen_Dönem.Focus();
                return;
            }

            DialogResult Dr = MessageBox.Show("Seçilen döneme ait altta listelenmiş işlerin ödeme talebini iptal etmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Talep_İşaretle_ÖdemeTalepEdildi_TeslimEdildi(MüşteriAdı, İşTakip_ÖdemeBekleyen_Dönem.Text);
            Banka.Değişiklikleri_Kaydet(İşTakip_ÖdemeBekleyen_İptalEt);
            foreach (DataGridViewRow biri in Tablo.Rows) Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(biri.Cells[Tablo_SeriNo.Index].Value as string);

            Banka.Değişiklikler_TamponuSıfırla();
            Seviye_Değişti(Seviye2_ÖdemeBekleyen, null);
        }
        private void İşTakip_ÖdemeBekleyen_ÖdemeMiktarı_DoubleClick(object sender, EventArgs e)
        {
            if (İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text.BoşMu(true))
            {
                if (İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Tag == null) return;
                İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text = string.Format("{0:,0.00}", (double)İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Tag);
            }
            else İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text = null;
        }
        private void İşTakip_ÖdemeBekleyen_ÖdendiOlarakİşaretle_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text))
            {
                MessageBox.Show("Lütfen geçerli bir eleman seçiniz", Text);
                İşTakip_ÖdemeBekleyen_Dönem.Focus();
                return;
            }

            string gecici = İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "Ödeme miktarı kutucuğu", null, 0))
            {
                İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Focus();
                return;
            }
            İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text = gecici;

            if (string.IsNullOrWhiteSpace(İşTakip_ÖdemeBekleyen_Notlar.Text)) İşTakip_ÖdemeBekleyen_Notlar.Text = null;

            İşTakip_ÖdemeBekleyen_Dönem_TextChanged(null, null);
            string soru = İşTakip_ÖdemeBekleyen_Açıklama.Text + Environment.NewLine + Environment.NewLine;
            if (İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Tag != null || İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text != "0")
            {
                double ÖdenenÜcret = İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text.NoktalıSayıya();
                soru += "Alınan Ödeme : " + Banka.Yazdır_Ücret(ÖdenenÜcret) + Environment.NewLine;

                if (İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Tag == null)
                {
                    soru += "Bu işlem ile müşteriniz ön ödeme sistemine dahil edilecek ve kalan miktar kayda geçirilecektir.";
                }
                else
                {
                    double İşlemSonrasıÖnÖdeme = ÖdenenÜcret - (double)İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Tag;
                    if (İşlemSonrasıÖnÖdeme > 0) soru += "İşlem sonrasında müşterinizin " + Banka.Yazdır_Ücret(İşlemSonrasıÖnÖdeme) + " alacağı kalacaktır.";
                    else if (İşlemSonrasıÖnÖdeme < 0) soru += "İşlem sonrasında müşterinizin " + Banka.Yazdır_Ücret(Math.Abs(İşlemSonrasıÖnÖdeme)) + " borcu olacaktır.";
                    else soru += "İşlem sonrasında alacak ve verecek kalmayacaktır.";
                }

                soru += Environment.NewLine + Environment.NewLine;
            }
            soru += "Seçilen döneme ait işleri KALICI olarak ÖDENDİ olarak işaretlemek istediğinize emin misiniz?"; 
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Talep_İşaretle_ÖdemeTalepEdildi_Ödendi(MüşteriAdı, İşTakip_ÖdemeBekleyen_Dönem.Text, İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text, İşTakip_ÖdemeBekleyen_Notlar.Text);
            Banka.Değişiklikleri_Kaydet(İşTakip_ÖdemeBekleyen_ÖdendiOlarakİşaretle);
            foreach (DataGridViewRow biri in Tablo.Rows) Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(biri.Cells[Tablo_SeriNo.Index].Value as string);
            Banka.Değişiklikler_TamponuSıfırla();

            Seviye_Değişti(Seviye2_ÖdemeBekleyen, null);
            İşTakip_ÖdemeBekleyen_ÖdemeMiktarı.Text = null;
            İşTakip_ÖdemeBekleyen_Dönem_TextChanged(null, null);
        }
        private bool İşTakip_Ödendi_Dönem_GeriBildirim_İşlemi(string Adı, ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü Türü, string YeniAdı = null)
        {
            İşTakip_Ödendi_Dönem_Açıklama.Text = null;
            Tablo.Rows.Clear();

            if (Türü != ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.ElemanSeçildi) return false;
            
            if (Adı.DoluMu())
            {
                string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
                if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
                {
                    MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                    İşTakip_Müşteriler.Odaklan();
                    return false;
                }

                Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.Ödendi, Adı);
                Banka.Talep_TablodaGöster(Tablo, bt);
                Tablo_İçeriğeGöreGüncelle();

                Banka.Talep_Ayıkla_ÖdemeDalı(bt.Ödeme, out List<string> Açıklamalar, out List<string> Ücretler, out _, out string Notlar, out bool MüşteriBorçluMu);
                string ipucu = "";
                for (int i = 0; i < Açıklamalar.Count; i++)
                {
                    ipucu += Açıklamalar[i] + " : " + Ücretler[i] + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(Notlar))
                {
                    ipucu += "Notlar : " + Notlar;
                }

                İşTakip_Ödendi_Dönem_Açıklama.Text = ipucu;
                İşTakip_Ödendi_Dönem_Açıklama.ForeColor = MüşteriBorçluMu ? Color.Red : SystemColors.ControlText;
            }
            
            İşTakip_Eposta_Ödendi.Text = "Ödendi : " + İşTakip_Ödendi_Dönem.SeçilenEleman_Adları.Count + " dönem";

            return true;
        }
        private void İşTakip_Yazdırma_Yazdır_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            ArgeMup.HazirKod.Depo_ depo;
            string gerçekdosyadı;

            if (Seviye2_DevamEden.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, İşTakip_DevamEden_ÜcretHesaplama.Checked ? Banka.TabloTürü.ÜcretHesaplama : Banka.TabloTürü.DevamEden);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo["Tür", 1] = MüşteriAdı;

                foreach (IDepo_Eleman elm in bt.Talepler)
                {
                    talepler.Ekle(null, elm.YazıyaDönüştür(null));
                }

                if (İşTakip_DevamEden_ÜcretHesaplama.Checked)
                {
                    depo = Banka.YazdırmayaHazırla_ÜcretHesaplama(depo);
                    if (depo == null) return;
                }

                gerçekdosyadı = (İşTakip_DevamEden_ÜcretHesaplama.Checked ? "Ücret_Hesaplama_" : "Devam_Eden_") + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";
            }
            else if (Seviye2_TeslimEdildi.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.TeslimEdildi);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo["Tür", 1] = MüşteriAdı;

                foreach (IDepo_Eleman elm in bt.Talepler)
                {
                    talepler.Ekle(null, elm.YazıyaDönüştür(null));
                }

                gerçekdosyadı = "Teslim_Edildi_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";
            }
            else if (Seviye2_ÖdemeBekleyen.Checked)
            {
                if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text)) return;
                
                depo = Banka.Tablo(MüşteriAdı, Banka.TabloTürü.ÖdemeTalepEdildi, false, İşTakip_ÖdemeBekleyen_Dönem.Text);
                Banka.Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle(MüşteriAdı, ref depo);

                gerçekdosyadı = "Ödeme_Talebi_" + İşTakip_ÖdemeBekleyen_Dönem.Text + ".pdf";
            }
            else if (Seviye2_Ödendi.Checked)
            {
                string Dönem = İşTakip_Ödendi_Dönem.SeçilenEleman_Adı;
                if (Dönem.BoşMu()) return;

                depo = Banka.Tablo(MüşteriAdı, Banka.TabloTürü.Ödendi, false, Dönem);
                gerçekdosyadı = "Ödendi_" + Dönem + ".pdf";
            }
            else return;

            if (depo == null || depo["Talepler"].Elemanları.Length == 0)
            {
                MessageBox.Show("Hiç kayıt bulunamadı", Text);
                return;
            }

            string dosyayolu = Ortak.Klasör_Gecici + Path.GetRandomFileName() + ".pdf";

            Ayarlar_Yazdırma y = new Ayarlar_Yazdırma();
            y.İşler_Yazdır(depo, dosyayolu);
            y.Dispose();

            if (!string.IsNullOrEmpty(Ortak.Kullanıcı_Klasör_Pdf) &&
                !Ortak.Klasör_KendiKlasörleriİçindeMi(Ortak.Kullanıcı_Klasör_Pdf))
            {
                string hedef = Ortak.Kullanıcı_Klasör_Pdf + MüşteriAdı + "\\" + gerçekdosyadı;
                if (!Dosya.Kopyala(dosyayolu, hedef))
                {
                    MessageBox.Show("Üretilen pdf kullanıcı klasörüne kopyalanamadı", Text);
                }
                else
                {
                    if (İşTakip_Yazdırma_VeGörüntüle.Checked) Ortak.Çalıştır.UygulamayaİşletimSistemiKararVersin(hedef);
                    if (İşTakip_Yazdırma_VeKlasörüAç.Checked) Ortak.Çalıştır.DosyaGezginindeGöster(hedef);
                }
            }
            else
            {
                if (İşTakip_Yazdırma_VeGörüntüle.Checked) Ortak.Çalıştır.UygulamayaİşletimSistemiKararVersin(dosyayolu);
                if (İşTakip_Yazdırma_VeKlasörüAç.Checked) Ortak.Çalıştır.DosyaGezginindeGöster(dosyayolu);
            }

            IDepo_Eleman Ayrl_Kullanıcı = Banka.Ayarlar_Kullanıcı(Name, null);
            Ayrl_Kullanıcı.Yaz("İşTakip_Yazdırma_VeGörüntüle", İşTakip_Yazdırma_VeGörüntüle.Checked);
            Ayrl_Kullanıcı.Yaz("İşTakip_Yazdırma_VeKlasörüAç", İşTakip_Yazdırma_VeKlasörüAç.Checked);
        }
        private void İşTakip_Eposta_CheckedChanged(object sender, EventArgs e)
        {
            İşTakip_Eposta_Gönder.Enabled = İşTakip_Eposta_ÜcretiHesaplanan.Checked || İşTakip_Eposta_DevamEden.Checked || İşTakip_Eposta_TeslimEdildi.Checked || İşTakip_Eposta_ÖdemeBekleyen.Checked || İşTakip_Eposta_Ödendi.Checked;

            CheckBox chb = sender as CheckBox;
            List<string> seçilen_dönemler = İşTakip_Ödendi_Dönem.SeçilenEleman_Adları;
            if (chb != null && chb == İşTakip_Eposta_Ödendi) İşTakip_Eposta_Ödendi.Text = "Ödendi : " + (seçilen_dönemler.Count > 0 ? seçilen_dönemler.Count.ToString() : "Son") + " dönem";
        }
        private void İşTakip_Eposta_Gönder_Click(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (!Banka.Müşteri_MevcutMu(MüşteriAdı))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Odaklan();
                return;
            }

            if (!Eposta.BirEpostaHesabıEklenmişMi)
            {
                MessageBox.Show("Bir eposta hesabı tanımlanması gerekmektedir." + Environment.NewLine + "Ana Ekran - Ayarlar - E-posta sayfasınından ayarlar gözden geçirilebilir", Text);
                return;
            }

            string FirmaİçiKişiler = null;                    
            if (İşTakip_Eposta_Kişiye.Checked)
            {
                IDepo_Eleman fik = Banka.Ayarlar_Genel("Eposta/Firma İçi Kişiler");
                if (fik != null) FirmaİçiKişiler = fik.Oku(null);

                if (FirmaİçiKişiler.BoşMu(true))
                {
                    MessageBox.Show("Firma içi kişi adresi bulunamadı" + Environment.NewLine + "Ana Ekran - Ayarlar - Eposta sayfasını kullanabilirsiniz", Text);
                    return;
                }
            }
            else
            {
                IDepo_Eleman m = Banka.Ayarlar_Müşteri(MüşteriAdı, "Eposta");
                if (m == null || string.IsNullOrEmpty(m.Oku("Kime") + m.Oku("Bilgi") + m.Oku("Gizli")))
                {
                    MessageBox.Show("Müşteriye tanımlı e-posta adresi bulunamadı" + Environment.NewLine + "Ana Ekran - Ayarlar - Müşteriler sayfasını kullanabilirsiniz", Text);
                    return;
                }
            }

            ArgeMup.HazirKod.Depo_ depo;
            string gecici_dosyadı;
            string gecici_klasör = Ortak.Klasör_Gecici + Path.GetRandomFileName() + "\\";
            Directory.CreateDirectory(gecici_klasör);

            Ayarlar_Yazdırma y = new Ayarlar_Yazdırma();

            if (İşTakip_Eposta_ÜcretiHesaplanan.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.ÜcretHesaplama);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo["Tür", 1] = MüşteriAdı;

                if (bt.Talepler.Count > 0)
                {
                    foreach (IDepo_Eleman elm in bt.Talepler)
                    {
                        talepler.Ekle(null, elm.YazıyaDönüştür(null));
                    }

                    gecici_dosyadı = gecici_klasör + "Ücret_Hesaplama_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";
                    depo = Banka.YazdırmayaHazırla_ÜcretHesaplama(depo);
                    if (depo == null) return;

                    y.İşler_Yazdır(depo, gecici_dosyadı);
                }
            }

            if (İşTakip_Eposta_DevamEden.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.DevamEden);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo["Tür", 1] = MüşteriAdı;

                if (bt.Talepler.Count > 0)
                {
                    foreach (IDepo_Eleman elm in bt.Talepler)
                    {
                        talepler.Ekle(null, elm.YazıyaDönüştür(null));
                    }

                    gecici_dosyadı = gecici_klasör + "Devam_Eden_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";

                    y.İşler_Yazdır(depo, gecici_dosyadı);
                }
            }

            if (İşTakip_Eposta_TeslimEdildi.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(MüşteriAdı, Banka.TabloTürü.TeslimEdildi);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo["Tür", 1] = MüşteriAdı;

                if (bt.Talepler.Count > 0)
                {
                    foreach (IDepo_Eleman elm in bt.Talepler)
                    {
                        talepler.Ekle(null, elm.YazıyaDönüştür(null));
                    }

                    gecici_dosyadı = gecici_klasör + "Teslim_Edildi_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";

                    y.İşler_Yazdır(depo, gecici_dosyadı);
                }
            }

            if (İşTakip_Eposta_ÖdemeBekleyen.Checked)
            {
                foreach (string ö in Banka.Dosya_Listele_Müşteri(MüşteriAdı, false))
                {
                    depo = Banka.Tablo(MüşteriAdı, Banka.TabloTürü.ÖdemeTalepEdildi, false, ö);
                    if (depo != null)
                    {
                        IDepo_Eleman de = depo.Bul("Talepler");
                        if (de != null && de.Elemanları.Length > 0)
                        {
                            Banka.Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle(MüşteriAdı, ref depo);

                            gecici_dosyadı = gecici_klasör + "Ödeme_Talebi_" + ö + ".pdf";
                            y.İşler_Yazdır(depo, gecici_dosyadı);
                        }
                    }
                }
            }

            if (İşTakip_Eposta_Ödendi.Checked)
            {
                List<string> dönemler = İşTakip_Ödendi_Dönem.SeçilenEleman_Adları;
                if (!Seviye2_Ödendi.Checked || dönemler.Count == 0) dönemler = new List<string>() { Banka.Dosya_Listele_Müşteri(MüşteriAdı, true).First() };

                foreach (string dönem in dönemler)
                {
                    if (dönem.BoşMu()) continue;

                    depo = Banka.Tablo(MüşteriAdı, Banka.TabloTürü.Ödendi, false, dönem);
                    if (depo != null)
                    {
                        IDepo_Eleman de = depo.Bul("Talepler");
                        if (de != null && de.Elemanları.Length > 0)
                        {
                            gecici_dosyadı = gecici_klasör + "Ödendi_" + dönem + ".pdf";
                            y.İşler_Yazdır(depo, gecici_dosyadı);
                        }
                    }
                }
            }

            y.Dispose();
            string[] dsy_lar = Directory.GetFiles(gecici_klasör);
            if (dsy_lar.Length > 0)
            {
                DialogResult Dr = MessageBox.Show("Oluşturulan toplam " + dsy_lar.Length + " adet belge e-posta yoluyla gönderilecek" +
                Environment.NewLine + Environment.NewLine +
                MüşteriAdı + Environment.NewLine + Environment.NewLine +
                FirmaİçiKişiler + Environment.NewLine + Environment.NewLine +
                "Devam etmek için Evet tuşuna basınız", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                if (İşTakip_Eposta_Kişiye.Checked) Eposta.Gönder_Kişiye(FirmaİçiKişiler, MüşteriAdı, dsy_lar, _GeriBildirimİşlemei_Tamamlandı);
                else Eposta.Gönder_Müşteriye(MüşteriAdı, dsy_lar, _GeriBildirimİşlemei_Tamamlandı);
                void _GeriBildirimİşlemei_Tamamlandı(string Sonuç)
                {
                    if (!string.IsNullOrEmpty(Sonuç)) MessageBox.Show(Sonuç, Text);
                }
            }
            else MessageBox.Show("Hiç kayıt bulunamadı", Text);
        }
        private void İşTakip_Eposta_Kişiye_CheckedChanged(object sender, EventArgs e)
        {
            İşTakip_Eposta_Gönder.Text = İşTakip_Eposta_Kişiye.Checked ? "Kişiye" : "Müşteriye";
        }
        private void İşTakip_TeslimEdildi_ÖdemeBekleyen_Notlar_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.BackColor = tb.Text.DoluMu(true) ? SystemColors.Window : Color.Salmon;
        }

        private void Tablo_TümünüSeç_Click(object sender, EventArgs e)
        {
            if (Tablo.Tag != null || Tablo.RowCount < 1) return;

            bool b = !(bool)Tablo[0, 0].Value;
            Tablo.Tag = 0;
            Tablo_TümünüSeç.Enabled = false;

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                Tablo[0, i].Value = b;
            }

            Tablo.Tag = null;
            Tablo_TümünüSeç.Enabled = true;

            Tablo_İçeriğeGöreGüncelle();
        }
        private void Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tablo.Tag != null || e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex > 0) return;

            Tablo[0, e.RowIndex].Value = !(bool)Tablo[0, e.RowIndex].Value;

            Tablo_İçeriğeGöreGüncelle();
        }
        private void Tablo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tablo.Tag != null || Tablo.RowCount < 1 || e.ColumnIndex < 0 || e.RowIndex < 0 || (int)Seviye1_işTakip.Tag != 1) return;

            string Müşteri = Tablo[2, e.RowIndex].Value as string, SeriNo = Tablo[1, e.RowIndex].Value as string, EkTanım = null;
            if (Müşteri == null || SeriNo == null) return;
            Banka.TabloTürü SeriNoTürü;

            if (Tablo[7, e.RowIndex].Tag != null) //teslim tarihi
            {
                SeriNoTürü = Banka.TabloTürü.TeslimEdildi;

                if (Tablo[8, e.RowIndex].Tag != null) //ödeme talep tarihi
                {
                    if (Tablo[9, e.RowIndex].Tag != null) //ödeme tarihi
                    {
                        SeriNoTürü = Banka.TabloTürü.Ödendi;
                        EkTanım = ((DateTime)Tablo[9, e.RowIndex].Tag).Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);
                    }
                    else
                    {
                        SeriNoTürü = Banka.TabloTürü.ÖdemeTalepEdildi;
                        EkTanım = ((DateTime)Tablo[8, e.RowIndex].Tag).Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);
                    }
                }
            }
            else SeriNoTürü = İşTakip_DevamEden_ÜcretHesaplama.Checked ? Banka.TabloTürü.ÜcretHesaplama : Banka.TabloTürü.DevamEden;

            string soru;
            if (SeriNoTürü == Banka.TabloTürü.TeslimEdildi)
            {
                soru = "Seçtiğiniz hastaya ait kayıt TESLİM EDİLMİŞ olarak görünüyor." + Environment.NewLine + Environment.NewLine +
                    "İçeriğinde değişiklik yapılır ise DEVAM EDİYOR olarak işaretlenecektir." + Environment.NewLine + Environment.NewLine +
                    "İşleme devam etmek istiyor musunuz?";
            }
            else if (SeriNoTürü == Banka.TabloTürü.ÖdemeTalepEdildi || SeriNoTürü == Banka.TabloTürü.Ödendi)
            {
                soru = "Seçtiğiniz hastaya ait kayıt içeriği artık değiştirilemez." + Environment.NewLine +
                    "Yapılacak değişiklikler KAYDEDİLMEYECEKTİR." + Environment.NewLine + Environment.NewLine +
                    "İşleme devam etmek istiyor musunuz?";
            }
            else
            {
                soru = "İşleme devam etmek istiyor musunuz?";
            }
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Ekranlar.ÖnYüzler.Ekle(new Yeni_İş_Girişi(SeriNo, Müşteri, SeriNoTürü, EkTanım));
        }
        private void Tablo_İçeriğeGöreGüncelle()
        {
            if (Tablo.Tag != null) return;

            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click_2(null, null);

            int seçili = 0;
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value) seçili++;
            }
            
            Tablo_Notlar.HeaderText = "Notlar ( " + seçili + " / " + Tablo.RowCount + " )";
        }

        bool TabloİçeriğiArama_Çalışıyor = false;
        bool TabloİçeriğiArama_KapatmaTalebi = false;
        int TabloİçeriğiArama_Tik = 0;
        int TabloİçeriğiArama_Sayac_Bulundu = 0;
        private void TabloİçeriğiArama_TextChanged(object sender, EventArgs e)
        {
            TabloİçeriğiArama_Tik = Environment.TickCount + 100;
            if (TabloİçeriğiArama.Text.Length < 2)
            {
                if (TabloİçeriğiArama_Sayac_Bulundu != 0)
                {
                    TabloİçeriğiArama.BackColor = Color.Salmon;

                    for (int satır = 0; satır < Tablo.RowCount; satır++)
                    {
                        Tablo.Rows[satır].Visible = true;
                        if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 100; }
                    }

                    TabloİçeriğiArama.BackColor = Color.White;
                    TabloİçeriğiArama_Sayac_Bulundu = 0;
                    Tablo_Notlar.HeaderText = "Notlar";
                }

                return;
            }

            if (TabloİçeriğiArama_Çalışıyor) { TabloİçeriğiArama_KapatmaTalebi = true; return; }

            TabloİçeriğiArama_Çalışıyor = true;
            TabloİçeriğiArama_KapatmaTalebi = false;
            TabloİçeriğiArama_Sayac_Bulundu = 0;
            TabloİçeriğiArama_Tik = Environment.TickCount + 500;
            TabloİçeriğiArama.BackColor = Color.Salmon;

            string[] arananlar = TabloİçeriğiArama.Text.ToLower().Split(' ');
            for (int satır = 0; satır < Tablo.RowCount && !TabloİçeriğiArama_KapatmaTalebi; satır++)
            {
                bool bulundu = false;
                for (int sutun = 1; sutun < Tablo.Columns.Count; sutun++)
                {
                    string içerik = (string)Tablo[sutun, satır].Value;
                    if (string.IsNullOrEmpty(içerik)) Tablo[sutun, satır].Style.BackColor = Color.White;
                    else
                    {
                        int bulundu_adet = 0;
                        if (!TabloİçeriğiArama_SadeceHastaAdındaAra.Checked ||
                            (TabloİçeriğiArama_SadeceHastaAdındaAra.Checked && sutun == Tablo_Hasta.Index))
                        {
                            içerik = içerik.ToLower();
                            foreach (string arn in arananlar)
                            {
                                if (TabloİçeriğiArama_VeVeya.Checked)
                                {
                                    //veya
                                    if (!içerik.Contains(arn)) continue;

                                    bulundu_adet = arananlar.Length;
                                    break; //veya için yeterli
                                }
                                else
                                {
                                    //ve
                                    if (!içerik.Contains(arn)) break;

                                    bulundu_adet++;
                                }
                            }
                        }

                        if (bulundu_adet == arananlar.Length)
                        {
                            Tablo[sutun, satır].Style.BackColor = Color.YellowGreen;
                            bulundu = true;
                        }
                        else Tablo[sutun, satır].Style.BackColor = Color.White;
                    }
                }

                Tablo.Rows[satır].Visible = bulundu;
                if (bulundu) TabloİçeriğiArama_Sayac_Bulundu++;

                if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 500; }
            }

            if (TabloİçeriğiArama_Sayac_Bulundu == 0) TabloİçeriğiArama_Sayac_Bulundu = -1;
            else Tablo_Notlar.HeaderText = "Bulundu : " + TabloİçeriğiArama_Sayac_Bulundu;

            TabloİçeriğiArama.BackColor = Color.White;
            TabloİçeriğiArama_Çalışıyor = false;
            Tablo.ClearSelection();

            if (TabloİçeriğiArama_KapatmaTalebi) TabloİçeriğiArama_TextChanged(null, null);
            TabloİçeriğiArama_KapatmaTalebi = false;
        }
        private void TabloİçeriğiArama_SadeceHastaAdındaAra_VeVeya_CheckedChanged(object sender, EventArgs e)
        {
            TabloİçeriğiArama_VeVeya.Text = TabloİçeriğiArama_VeVeya.Checked ? "Veya" : "Ve";
            TabloİçeriğiArama_TextChanged(null, null);
        }

        List<string> Arama_Sorgula_Aranan_Müşteriler, Arama_Sorgula_Aranan_İşTürleri;
        private void Arama_Sorgula_Click(object sender, EventArgs e)
        {
            Ortak.Gösterge.Başlat("Sayılıyor", true, Arama_Sorgula, 0);
            TabloİçeriğiArama.Text = null;

            if (Arama_GirişTarihi_Başlangıç.Value > Arama_GirişTarihi_Bitiş.Value)
            {
                DateTime gecici = Arama_GirişTarihi_Başlangıç.Value;
                Arama_GirişTarihi_Başlangıç.Value = new DateTime(Arama_GirişTarihi_Bitiş.Value.Year, Arama_GirişTarihi_Bitiş.Value.Month, Arama_GirişTarihi_Bitiş.Value.Day, 0, 0, 0);
                Arama_GirişTarihi_Bitiş.Value = new DateTime(gecici.Year, gecici.Month, gecici.Day, 23, 59, 00);
            }
            Arama_Sorgula_Aranan_Müşteriler = Arama_Müşteriler.SeçilenEleman_Adları;
            if (Arama_Sorgula_Aranan_Müşteriler.Count == 0) Arama_Sorgula_Aranan_Müşteriler = Arama_Müşteriler.Tüm_Elemanlar;
            Arama_Sorgula_Aranan_İşTürleri = Arama_İş_Türleri.SeçilenEleman_Adları;
            if (Arama_Sorgula_Aranan_İşTürleri.Count == 0) Arama_Sorgula_Aranan_İşTürleri = Arama_İş_Türleri.Tüm_Elemanlar;

            if (!Seviye2_DevamEden.Checked && !Seviye2_TeslimEdildi.Checked && !Seviye2_ÖdemeBekleyen.Checked && !Seviye2_Ödendi.Checked)
            {
                Seviye2_DevamEden.Checked = true;
            }

            int kademe = 0;
            for (int i = 0; i < Arama_Sorgula_Aranan_Müşteriler.Count && Ortak.Gösterge.Çalışsın; i++)
            {
                if (Ortak.Gösterge.Çalışsın)
                {
                    if (Seviye2_DevamEden.Checked) kademe += 1;
                    if (Seviye2_TeslimEdildi.Checked) kademe += 1;
                    if (Seviye2_ÖdemeBekleyen.Checked) kademe += Banka.Dosya_Listele_Müşteri(Arama_Sorgula_Aranan_Müşteriler[i], false).Length;
                    if (Seviye2_Ödendi.Checked) kademe += Banka.Dosya_Listele_Müşteri(Arama_Sorgula_Aranan_Müşteriler[i], true).Length;
                }
            }
            Ortak.Gösterge.Bitir();

            Ortak.Gösterge.Başlat("Sorgu devam ediyor", true, Arama_Sorgula, kademe);
            Banka_Tablo_ bt = new Banka_Tablo_(null);
            bt.Türü = Banka.TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi;
            Banka.Talep_TablodaGöster(Tablo, bt);

            for (int i = 0; i < Arama_Sorgula_Aranan_Müşteriler.Count && Ortak.Gösterge.Çalışsın; i++)
            {
                if (Seviye2_DevamEden.Checked && Ortak.Gösterge.Çalışsın)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Sorgula_Aranan_Müşteriler[i], Banka.TabloTürü.DevamEden));
                }

                if (Seviye2_TeslimEdildi.Checked && Ortak.Gösterge.Çalışsın)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Sorgula_Aranan_Müşteriler[i], Banka.TabloTürü.TeslimEdildi));
                }

                if (Seviye2_ÖdemeBekleyen.Checked && Ortak.Gösterge.Çalışsın)
                {
                    string[] l = Banka.Dosya_Listele_Müşteri(Arama_Sorgula_Aranan_Müşteriler[i], false);

                    for (int s = 0; s < l.Length && Ortak.Gösterge.Çalışsın; s++)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        //DateTime t = l[s].TarihSaate(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);
                        //if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                        Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Sorgula_Aranan_Müşteriler[i], Banka.TabloTürü.ÖdemeTalepEdildi, l[s]));
                    }
                }

                if (Seviye2_Ödendi.Checked && Ortak.Gösterge.Çalışsın)
                {
                    string[] l = Banka.Dosya_Listele_Müşteri(Arama_Sorgula_Aranan_Müşteriler[i], true);

                    for (int s = 0; s < l.Length && Ortak.Gösterge.Çalışsın; s++)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        //DateTime t = l[s].TarihSaate(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);
                        //if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                        Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Sorgula_Aranan_Müşteriler[i], Banka.TabloTürü.Ödendi, l[s]));
                    }
                }
                
            }

            Tablo_İçeriğeGöreGüncelle();

            Ortak.Gösterge.Bitir();
            TabloİçeriğiArama.Focus();
        }
        private void Arama_Sorgula_Click_2(Banka_Tablo_ bt)
        {
            string sn_ler = "";
            List<IDepo_Eleman> uyuşanlar = new List<IDepo_Eleman>();
            foreach (IDepo_Eleman serino in bt.Talepler)
            {
                bool evet = false;

                foreach (IDepo_Eleman iş in serino.Elemanları)
                {
                    Banka.Talep_Ayıkla_İşTürüDalı(iş, out string İşTürü, out string GirişTarihi, out _, out _, out _, out _);

                    if (!Arama_Sorgula_Aranan_İşTürleri.Contains(İşTürü)) continue;

                    DateTime t = GirişTarihi.TarihSaate();
                    if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                    evet = true;
                    break;
                }

                if (evet)
                {
                    sn_ler += serino.Adı + " ";
                    uyuşanlar.Add(serino);
                }
            }

            if (uyuşanlar.Count == 0) return;

            if (Arama_Sorgula_Detaylı.Checked && bt.Ödeme != null)
            {
                //boş IDepo_Eleman oluşturulması
                IDepo_Eleman y = uyuşanlar[0].Bul(null, false, true);
                y.Sil(null, true, true);
                y.Adı = "";

                sn_ler = sn_ler.TrimEnd() + Environment.NewLine + Environment.NewLine;
                sn_ler += "Talep : " + D_TarihSaat.Yazıya(bt.Ödeme.Oku_TarihSaat(null), D_TarihSaat.Şablon_DosyaAdı2);
                if (bt.Türü == Banka.TabloTürü.Ödendi) sn_ler += Environment.NewLine + "Ödeme : " + D_TarihSaat.Yazıya(bt.Ödeme.Oku_TarihSaat(null, default, 1), D_TarihSaat.Şablon_DosyaAdı2);

                //sn ler + tarihler
                y.Yaz(" ", sn_ler);

                sn_ler = "";
                Banka.Talep_Ayıkla_ÖdemeDalı(bt.Ödeme, out List<string> Açıklamalar, out List<string> Ücretler, out _, out string Notlar, out _);
                for (int i = 0; i < Açıklamalar.Count; i++) sn_ler += Açıklamalar[i] + " : " + Ücretler[i] + "\n";
                sn_ler = sn_ler.TrimEnd('\n');

                if (Notlar.DoluMu()) sn_ler += "\n\n" + Notlar;
                y[2] = sn_ler;

                uyuşanlar.Add(y);
            }

            bt.Talepler = uyuşanlar;
            Banka.Talep_TablodaGöster(Tablo, bt, false, true);
        }

        private void Müşteri_KDV_CheckedChanged(object sender, EventArgs e)
        {
            string MüşteriAdı = İşTakip_Müşteriler.SeçilenEleman_Adı;
            if (MüşteriAdı.BoşMu(true)) return;

            DialogResult Dr = MessageBox.Show("Bu ayar siz tekrar seçim yapana kadar KALICI olarak değiştirilecek." + Environment.NewLine + Environment.NewLine +
                "Devam etmek istediğinize emin misiniz?" + Environment.NewLine + Environment.NewLine,
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) 
            {
                Müşteri_KDV.CheckedChanged -= Müşteri_KDV_CheckedChanged;
                Müşteri_KDV.Checked = !Müşteri_KDV.Checked;
                Müşteri_KDV.CheckedChanged += Müşteri_KDV_CheckedChanged;
                return; 
            }

            Banka.Müşteri_KDV_İskonto(MüşteriAdı, Müşteri_KDV.Checked);
            Banka.Değişiklikleri_Kaydet(Müşteri_KDV);
            if (Seviye2_TeslimEdildi.Checked) İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click_2(null, null);
        }

        private bool Malzemeler_Malzeme_GeriBildirim_İşlemi(string Adı, ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü Türü, string YeniAdı = null)
        {
            Malzemeler_Açıklama.Text = null;
            Tablo.Rows.Clear();

            if (Adı.BoşMu() || Türü != ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.ElemanSeçildi) return false;

            Banka.Malzeme_KullanımDetayı_TablodaGöster(Tablo, Adı, out string Açıklama);
            Malzemeler_Açıklama.Text = Açıklama;
            Tablo_İçeriğeGöreGüncelle();

            return true;
        }
        private void Malzemeler_SeçilenleriSil_Click(object sender, EventArgs e)
        {
            string MalzemeAdı = Malzemeler_Malzeme.SeçilenEleman_Adı;
            if (!Banka.Malzeme_MevcutMu(MalzemeAdı))
            {
                MessageBox.Show("Lütfen geçerli bir malzeme seçiniz", Text);
                Malzemeler_Malzeme.Odaklan();
                return;
            }

            List<string> l = new List<string>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value) l.Add((string)Tablo[1, i].Value);
            }

            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

            DialogResult Dr = MessageBox.Show("Seçili " + l.Count + " adet öğeyi KALICI OLARAK SİLMEK istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Malzeme_KullanımDetayı_Tablodaki_SeçiliOlanlarıSil(Tablo, MalzemeAdı);
            Banka.Değişiklikleri_Kaydet(İşTakip_DevamEden_Sil);

        YenidenDene:
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    Tablo.Rows.RemoveAt(i);
                    goto YenidenDene;
                }
            }
            
            Tablo_İçeriğeGöreGüncelle();
            Malzemeler_Açıklama.Text = null;
        }

        void IGüncellenenSeriNolar.KontrolEt(List<string> GüncellenenSeriNolar)
        {
            if (Seviye1_işTakip.Checked)
            {
                if((int)Seviye1_işTakip.Tag == 1)
                {
                    //İş takip
                    foreach (DataGridViewRow satır in Tablo.Rows)
                    {
                        if (GüncellenenSeriNolar.Contains(satır.Cells[Tablo_SeriNo.Index].Value))
                        {
                            CheckBox c = null;
                            if (Seviye2_DevamEden.Checked) c = Seviye2_DevamEden;
                            else if (Seviye2_TeslimEdildi.Checked) c = Seviye2_TeslimEdildi;
                            else if (Seviye2_ÖdemeBekleyen.Checked) c = Seviye2_ÖdemeBekleyen;
                            else if (Seviye2_Ödendi.Checked) c = Seviye2_Ödendi;
                            if (c != null) Seviye_Değişti(c, null);

                            break;
                        }
                    }
                }
                else
                {
                    //malzemeler
                    Malzemeler_Malzeme_GeriBildirim_İşlemi(Malzemeler_Malzeme.SeçilenEleman_Adı, ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü.ElemanSeçildi);
                }
            }
            else
            {
                //arama
                Tablo.ClearSelection();
                foreach (DataGridViewRow satır in Tablo.Rows)
                {
                    if (GüncellenenSeriNolar.Contains(satır.Cells[Tablo_SeriNo.Index].Value))
                    {
                        satır.Cells[Tablo_SeriNo.Index].ToolTipText = "Değiştirildi";

                        foreach (DataGridViewCell hücre in satır.Cells)
                        {
                            hücre.Style.BackColor = Color.Red;
                        }
                    }
                }
            }
        }
    }
}
