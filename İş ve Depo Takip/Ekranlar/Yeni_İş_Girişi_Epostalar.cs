using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Yeni_İş_Girişi_Epostalar : Form
    {
        public bool DeğişiklikYapıldı = false;
        Depo_ Epostalar = null;

        public Yeni_İş_Girişi_Epostalar()
        {
            InitializeComponent();
        }
        private void Yeni_İş_Girişi_Epostalar_Shown(object sender, EventArgs e)
        {
            IDepo_Eleman Ayrl_Kullanıcı = Banka.Ayarlar_Kullanıcı(Name, null);

            if (Eposta.Klasörler != null) 
            {
                Seç_Klasör.Items.AddRange(Eposta.Klasörler);
                Çıkış_Klasör.Items.AddRange(Eposta.Klasörler);
            }
            else
            {
                if (Ayrl_Kullanıcı.Oku("Seç_Klasör").DoluMu()) Seç_Klasör.Items.Add(Ayrl_Kullanıcı.Oku("Seç_Klasör"));
                if (Ayrl_Kullanıcı.Oku("Çıkış_Klasör").DoluMu()) Çıkış_Klasör.Items.Add(Ayrl_Kullanıcı.Oku("Çıkış_Klasör"));
            }
            
            Seç_Klasör.Text = Ayrl_Kullanıcı.Oku("Seç_Klasör");
            Seç_SadeceOkunmamışlar.Checked = Ayrl_Kullanıcı.Oku_Bit("Seç_SadeceOkunmamışlar", true);
            Seç_Tümü.Checked = !Seç_SadeceOkunmamışlar.Checked;
            Seç_GünKadarEskiEpostalar.Value = Ayrl_Kullanıcı.Oku_TamSayı("Seç_GünKadarEskiEpostalar", 7);
            Çıkış_Klasör.Text = Ayrl_Kullanıcı.Oku("Çıkış_Klasör");
            Çıkış_OkunduOlarakİşaretle.Checked = Ayrl_Kullanıcı.Oku_Bit("Çıkış_OkunduOlarakİşaretle", true);
            Çıkış_KlasöreTaşı.Checked = !Çıkış_OkunduOlarakİşaretle.Checked;

            Yenile_Click(null, null);
        }
        private void Yeni_İş_Girişi_Epostalar_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Dosyalar_Resim.Image != null) Dosyalar_Resim.Image.Dispose();
        }

        private void Yenile_Click(object sender, EventArgs e)
        {
            Yenile.Image = Properties.Resources.Uzunİslem;
            Eposta.YenileİşaretleSil(Seç_Klasör.Text, (int)Seç_GünKadarEskiEpostalar.Value, Seç_SadeceOkunmamışlar.Checked, null, null, null, _İşlemSonu_);

            void _İşlemSonu_(string Durum)
            {
                Yenile.Invoke(new Action(() =>
                {
                    List<string> beyaz_listede_olmayan = new List<string>();

                    if (Durum.BoşMu())
                    {
                        Epostalar = new Depo_(File.ReadAllText(Eposta.Yenile_DepoDosyaYolu(Seç_Klasör.Text)));
                        List<DataGridViewRow> l = new List<DataGridViewRow>();
                        int sutun_sayısı = Tablo.ColumnCount;
                        TabloİçeriğiArama.Text = null;

                        foreach (IDepo_Eleman epst in Epostalar["Liste"].Elemanları)
                        {
                            if (!Eposta.GönderenBeyazListeİçindeMi(epst[3]))
                            {
                                if (!beyaz_listede_olmayan.Contains(epst[3])) beyaz_listede_olmayan.Add(epst[3]);
                            }

                            object[] dizin = new object[sutun_sayısı];
                            dizin[0] = epst[3] + (epst[4].DoluMu() ? '\n' + epst[4] : null);
                            dizin[1] = epst[0];
                            dizin[2] = epst[2];

                            DataGridViewRow yeni = new DataGridViewRow();
                            yeni.CreateCells(Tablo, dizin);
                            yeni.Tag = epst.Adı;
                            l.Add(yeni);
                        }
                        Tablo.Rows.Clear();
                        Tablo.Rows.AddRange(l.ToArray());
                        Tablo.ClearSelection();

                        if (Eposta.Klasörler != null)
                        {
                            if (Seç_Klasör.Items.Count < Eposta.Klasörler.Length)
                            {
                                string seçili_olan_giriş = Seç_Klasör.Text;
                                string seçili_olan_çıkış = Çıkış_Klasör.Text;

                                List<string> l_kontrol = new List<string>(Eposta.Klasörler);
                                foreach (var a in Seç_Klasör.Items)
                                {
                                    l_kontrol.Add(a as string);
                                }
                                string[] dd = l_kontrol.Distinct().ToArray();
                                Seç_Klasör.Items.Clear(); Seç_Klasör.Items.AddRange(dd); Seç_Klasör.Text = seçili_olan_giriş;
                                Çıkış_Klasör.Items.Clear(); Çıkış_Klasör.Items.AddRange(dd); Çıkış_Klasör.Text = seçili_olan_çıkış;
                            }
                        }

                        if (!Seç_Klasör.Items.Contains(Epostalar["Tipi", 1])) //İndirilen klasörün adı
                        {
                            Seç_Klasör.Items.Add(Epostalar["Tipi", 1]); Seç_Klasör.Text = Epostalar["Tipi", 1];
                            Çıkış_Klasör.Items.Add(Epostalar["Tipi", 1]);
                        }
                    }

                    Yenile.Image = null;
                    Yenile.BackColor = Durum.BoşMu() ? SystemColors.Control : Color.Salmon;
                    İpUcu_Genel.SetToolTip(Yenile, Durum.DoluMu() ? Durum : 
                        "Toplam : " + (Epostalar == null ? 0 : Epostalar["Liste"].Elemanları.Length) + Environment.NewLine +
                        "Beyaz listede olmayan : " + beyaz_listede_olmayan.Count + Environment.NewLine +
                        string.Join(Environment.NewLine, beyaz_listede_olmayan));
                }));
            }
        }

        private void Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Dosyalar.Items.Clear(); Dosyalar.BackColor = SystemColors.Window;
            Dosyalar_Resim.Image?.Dispose(); Dosyalar_Resim.Image = null;
            Yazılar.Text = null;
            Dosyalar_PanodanResimAl.Enabled = false;
            Dosyalar_İlgiliUygulamadaAç.Enabled = false;
            Dosyalar_MasaüstüneKopyala.Enabled = false;

            if (e.ColumnIndex < 0 || e.RowIndex < 0 || Tablo.Rows[e.RowIndex].Tag == null || Epostalar == null) return;
            IDepo_Eleman epst = Epostalar.Bul("Liste/" + Tablo.Rows[e.RowIndex].Tag);
            if (epst == null) return;

            Yazılar.Text = Tablo[0, e.RowIndex].Value.ToString().Replace("\n", " ") + " " + Tablo[1, e.RowIndex].Value;
            if (((string)Tablo[2, e.RowIndex].Value).DoluMu(true)) Yazılar.Text += "\r\n\r\nKonu ---------------\r\n" + _YazıyıDüzenle_(Tablo[2, e.RowIndex].Value as string);
            if (epst["Mesaj", 0].DoluMu(true)) Yazılar.Text += "\r\n\r\nHtml ---------------\r\n" + _YazıyıDüzenle_(AğAraçları.Htmlden_Yazıya(epst["Mesaj", 0]));
            if (epst["Mesaj", 1].DoluMu(true)) Yazılar.Text += "\r\n\r\nDüzyazı ---------------\r\n" + _YazıyıDüzenle_(epst["Mesaj", 1]);

            if (Eposta.GönderenBeyazListeİçindeMi(epst[3]))
            {
                foreach (IDepo_Eleman dsy in epst["Ekler"].Elemanları)
                {
                    Dosyalar.Items.Add(dsy[0]);
                }

                Dosyalar_PanodanResimAl.Enabled = true;
                Dosyalar_İlgiliUygulamadaAç.Enabled = true;
                Dosyalar_MasaüstüneKopyala.Enabled = true;
            }
            else Dosyalar.BackColor = Color.Salmon;

            Seç_YazılarıNotlaraEkle.Checked = false;

            string _YazıyıDüzenle_(string Girdi)
            {
                if (Girdi.BoşMu(true)) return null;

                return Girdi.Trim(' ', '\r', '\n');
            }
        }
        private void Yazılar_TextChanged(object sender, EventArgs e)
        {
            Seç_YazılarıNotlaraEkle.Checked = true;
        }
        private void Dosyalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Dosyalar_Resim.Image != null)
            {
                Dosyalar_Resim.Image.Dispose();
                Dosyalar_Resim.Image = null;
            }

            string dsy = Epostalar_SeçiliDosyaYolu();
            if (dsy.BoşMu()) return;

            string soyad = Path.GetExtension(dsy).ToLower();
            if (soyad == ".png" || soyad == ".bmp" || soyad == ".jpg" || soyad == ".gif")
            {
                Dosyalar_Resim_YaklaşmaOranı.Value = 0;
                Image rsm = Image.FromFile(dsy);
                Dosyalar_Resim.Dock = DockStyle.Fill;
                Dosyalar_Resim.Size = rsm.Size;
                Dosyalar_Resim.Image = rsm;
            }
        }
        private void Dosyalar_Resim_YaklaşmaOranı_ValueChanged(object sender, EventArgs e)
        {
            if (Dosyalar_Resim.Image == null) return;
            if (Dosyalar_Resim_YaklaşmaOranı.Value == 0)
            {
                if (Dosyalar_Resim.Dock != DockStyle.Fill) Dosyalar_Resim.Dock = DockStyle.Fill;
            }
            else
            {
                if (Dosyalar_Resim.Dock != DockStyle.None) Dosyalar_Resim.Dock = DockStyle.None;
                Dosyalar_Resim.Size = new Size((int)(Dosyalar_Resim.Image.Size.Width * Dosyalar_Resim_YaklaşmaOranı.Value), (int)(Dosyalar_Resim.Image.Size.Height * Dosyalar_Resim_YaklaşmaOranı.Value));
            }
        }
        private void Dosyalar_MasaüstüneKopyala_Click(object sender, EventArgs e)
        {
            string SahteKonum = Epostalar_SeçiliDosyaYolu();
            if (SahteKonum.BoşMu()) return;

            string masaüstü_yansıma_adı = Klasör.Depolama(Klasör.Kapsamı.Masaüstü, "", "", "") + "\\" + Path.GetFileName(SahteKonum);

            if (File.Exists(masaüstü_yansıma_adı))
            {
                MessageBox.Show("Masaüstünde aynı isimli bir dosya bulunduğundan işlem durduruldu", Text);
            }
            else Dosya.Kopyala(SahteKonum, masaüstü_yansıma_adı);
        }
        private void Dosyalar_İlgiliUygulamadaAç_Click(object sender, EventArgs e)
        {
            string SahteKonum = Epostalar_SeçiliDosyaYolu();
            if (SahteKonum.BoşMu()) return;

            Ortak.Çalıştır.UygulamayaİşletimSistemiKararVersin(SahteKonum);
        }
        private void Dosyalar_PanodanResimAl_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                Image PanodanAlınanResim = Clipboard.GetImage();
                string dsy = Ortak.Klasör_Gecici + "Pano_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".png";
                PanodanAlınanResim.Save(dsy, System.Drawing.Imaging.ImageFormat.Png);
                Epostalar_DosyaEkle(dsy);
            }
        }

        string Epostalar_SeçiliDosyaYolu()
        {
            if (Dosyalar.SelectedIndex < 0 || Tablo.SelectedRows.Count < 1 || Tablo.SelectedRows[0].Tag == null) return null;

            IDepo_Eleman epst = Epostalar.Bul("Liste/" + Tablo.SelectedRows[0].Tag);
            if (epst == null) return null;

            return epst["Ekler"].Elemanları[Dosyalar.SelectedIndex].Adı;
        }
        void Epostalar_DosyaEkle(string KaynakDosya)
        {
            if (Dosyalar.Items.Count < 1 || Tablo.SelectedRows.Count < 1 || Tablo.SelectedRows[0].Tag == null)
            {
                MessageBox.Show("Öncelikle eki olan bir eposta seçiniz.");
                return;
            }

            IDepo_Eleman epst = Epostalar.Bul("Liste/" + Tablo.SelectedRows[0].Tag);
            if (epst == null) return;

            string dsy_adı = Path.GetFileName(KaynakDosya);
            string kopyalanacak_kls = Klasör.ÜstKlasör(epst["Ekler"].Elemanları[0].Adı);
            string HedefDosya = kopyalanacak_kls + "\\" + dsy_adı;
            if (!Dosya.Kopyala(KaynakDosya, HedefDosya)) return;

            epst["Ekler/" + HedefDosya].Yaz(null, dsy_adı);
            Dosyalar.Items.Add(dsy_adı);
        }

        public bool NotlarıVeDosyaEkleriniAl(out string Yazı, out string[] DosyaEkleri)
        {
            Yazı = null;
            DosyaEkleri = null;
            if (Tablo.SelectedRows.Count < 1 || Tablo.SelectedRows[0].Tag == null) return false;

            IDepo_Eleman epst = Epostalar.Bul("Liste/" + Tablo.SelectedRows[0].Tag);
            if (epst == null) return false;

            Yazı = Seç_YazılarıNotlaraEkle.Checked ? Yazılar.Text : null;

            if (Dosyalar.CheckedIndices.Count > 0)
            {
                DosyaEkleri = new string[Dosyalar.CheckedIndices.Count];
                for (int i = 0; i < DosyaEkleri.Length; i++)
                {
                    DosyaEkleri[i] = epst["Ekler"].Elemanları[Dosyalar.CheckedIndices[i]].Adı;
                }
            }

            return Yazı.DoluMu(true) || DosyaEkleri != null;
        }
        public void KullanılanEpostayıİşle()
        {
            IDepo_Eleman Ayrl_Kullanıcı = Banka.Ayarlar_Kullanıcı(Name, null);
            Ayrl_Kullanıcı.Yaz("Seç_Klasör", Seç_Klasör.Text);
            Ayrl_Kullanıcı.Yaz("Seç_SadeceOkunmamışlar", Seç_SadeceOkunmamışlar.Checked);
            Ayrl_Kullanıcı.Yaz("Seç_GünKadarEskiEpostalar", (int)Seç_GünKadarEskiEpostalar.Value);
            Ayrl_Kullanıcı.Yaz("Çıkış_Klasör", Çıkış_Klasör.Text);
            Ayrl_Kullanıcı.Yaz("Çıkış_OkunduOlarakİşaretle", Çıkış_OkunduOlarakİşaretle.Checked);

            if (!NotlarıVeDosyaEkleriniAl(out _, out _)) return;

            string[] dizi = new string[] { Tablo.SelectedRows[0].Tag as string };

            Eposta.YenileİşaretleSil(Seç_Klasör.Text, 0, false,
                Çıkış_OkunduOlarakİşaretle.Checked ? dizi : null,
                Çıkış_KlasöreTaşı.Checked ? dizi : null, 
                Çıkış_Klasör.Text);
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
                    TabloİçeriğiArama.BackColor = System.Drawing.Color.Salmon;

                    for (int satır = 0; satır < Tablo.RowCount; satır++)
                    {
                        Tablo.Rows[satır].Visible = true;
                        if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 100; }
                    }

                    TabloİçeriğiArama.BackColor = System.Drawing.Color.White;
                    TabloİçeriğiArama_Sayac_Bulundu = 0;
                }

                return;
            }

            if (TabloİçeriğiArama_Çalışıyor) { TabloİçeriğiArama_KapatmaTalebi = true; return; }

            TabloİçeriğiArama_Çalışıyor = true;
            TabloİçeriğiArama_KapatmaTalebi = false;
            TabloİçeriğiArama_Sayac_Bulundu = 0;
            TabloİçeriğiArama_Tik = Environment.TickCount + 500;
            TabloİçeriğiArama.BackColor = System.Drawing.Color.Salmon;

            string[] arananlar = TabloİçeriğiArama.Text.ToLower().Split(' ');
            for (int satır = 0; satır < Tablo.RowCount && !TabloİçeriğiArama_KapatmaTalebi; satır++)
            {
                bool bulundu = false;
                for (int sutun = 0; sutun < Tablo.Columns.Count; sutun++)
                {
                    if (Tablo[sutun, satır].Value == null) continue;

                    string içerik = Tablo[sutun, satır].Value.ToString();
                    if (string.IsNullOrEmpty(içerik)) Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.White;
                    else
                    {
                        içerik = içerik.ToLower();
                        int bulundu_adet = 0;
                        foreach (string arn in arananlar)
                        {
                            if (!içerik.Contains(arn)) break;

                            bulundu_adet++;
                        }

                        if (bulundu_adet == arananlar.Length)
                        {
                            Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.YellowGreen;
                            bulundu = true;
                        }
                        else Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.White;
                    }
                }

                Tablo.Rows[satır].Visible = bulundu;
                if (bulundu) TabloİçeriğiArama_Sayac_Bulundu++;

                if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 500; }
            }

            if (TabloİçeriğiArama_Sayac_Bulundu == 0) TabloİçeriğiArama_Sayac_Bulundu = -1;

            TabloİçeriğiArama.BackColor = System.Drawing.Color.White;
            TabloİçeriğiArama_Çalışıyor = false;
            Tablo.ClearSelection();

            if (TabloİçeriğiArama_KapatmaTalebi) TabloİçeriğiArama_TextChanged(null, null);
            TabloİçeriğiArama_KapatmaTalebi = false;
        }
    }
}
