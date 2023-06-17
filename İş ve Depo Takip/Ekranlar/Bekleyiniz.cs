using ArgeMup.HazirKod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Bekleyiniz : Form
    {
        private int Tik = 0;
        const int Tik_ZamanAşımmı_msn = 500;
        static Bekleyiniz Pencere;
        List<Bir_Talep_> Tümü = new List<Bir_Talep_>();

        public bool Çalışsın
        {
            get
            {
                Bir_Talep_ tlp = Tümü.LastOrDefault();
                if (tlp == default) return false;

                if (Tik < Environment.TickCount) 
                {
                    if (!Visible) Show();

                    Application.DoEvents(); 
                    Tik = Environment.TickCount + Tik_ZamanAşımmı_msn; 
                }

                return tlp.Çalışsın;
            }
        }
        public int İlerleme
        {
            set
            {
                Bir_Talep_ tlp = Tümü.LastOrDefault();
                if (tlp == default) return;

                int toplam = İlerlemeÇubuğu.Value + value;

                if (toplam >= İlerlemeÇubuğu.Maximum) İlerlemeÇubuğu.Value = İlerlemeÇubuğu.Maximum;
                else İlerlemeÇubuğu.Value = toplam;

                tlp.Geçerli_Kademe = İlerlemeÇubuğu.Value;
            }
        }

        public Bekleyiniz()
        {
            InitializeComponent();

            Pencere = this;
            Text = "ArGeMuP " + Kendi.Adı + " V" + Kendi.Sürümü_Dosya + " " + Text;
            Icon = Properties.Resources.kendi;

            Hide();
        }
        
        public void Başlat(string Açıklama, bool İptalEdilebilir, Control Tetikleyen, int Toplam_Kademe)
        {
            Bir_Talep_ yeni = new Bir_Talep_(Açıklama, İptalEdilebilir, Tetikleyen, Toplam_Kademe);
            Tümü.Add(yeni);
        }
        public void Bitir()
        {
            Bir_Talep_ tlp = Tümü.LastOrDefault();
            if (tlp == default) return;

            if (tlp.Tetikleyen != null)
            {
                if (!tlp.Tetikleyen.InvokeRequired) _Bitir_Tetikleyen_(tlp.Tetikleyen);
                else
                {
                    tlp.Tetikleyen.Invoke(new Action(() =>
                    {
                        _Bitir_Tetikleyen_(tlp.Tetikleyen);
                    }));
                }

                tlp.Tetikleyen = null;

                void _Bitir_Tetikleyen_(Control ctrl)
                {
                    if (ctrl.GetType() == typeof(ListBox))
                    {
                        ListBox lb = ctrl as ListBox;
                        lb.Enabled = lb.Items.Count > 0;
                    }
                    else ctrl.Enabled = true;
                }
            }

            Tümü.RemoveAt(Tümü.Count - 1);
            if (Tümü.Count == 0)
            {
                if (!Pencere.InvokeRequired) Hide();
                else
                {
                    Pencere.Invoke(new Action(() =>
                    {
                        Hide();
                    }));
                }
            }
            else Tümü.Last().EkranıGüncelle();
        }

        private void İptalEt_Click(object sender, EventArgs e)
        {
            if (İptalEt.Enabled)
            {
                Bir_Talep_ tlp = Tümü.LastOrDefault();
                if (tlp == default) return;

                tlp.Çalışsın = false;
                İptalEt.Enabled = false;
                Application.DoEvents();
            }
        }

        class Bir_Talep_
        {
            public string Açıklama;
            public bool Çalışsın = true, İptalEdilebilir;
            public Control Tetikleyen;
            public int Toplam_Kademe, Geçerli_Kademe = 0;

            public Bir_Talep_(string Açıklama, bool İptalEdilebilir, Control Tetikleyen, int Toplam_Kademe)
            {
                this.Açıklama = Açıklama;
                this.İptalEdilebilir = İptalEdilebilir;
                this.Tetikleyen = Tetikleyen;
                this.Toplam_Kademe = Toplam_Kademe;

                if (Tetikleyen != null)
                {
                    Tetikleyen.Enabled = false;
                    Application.DoEvents();
                }
                this.Tetikleyen = Tetikleyen;

                EkranıGüncelle();
            }
            public void EkranıGüncelle()
            {
                if (!Pencere.InvokeRequired) _EkranıGüncelle_();
                else
                {
                    Pencere.Invoke(new Action(() =>
                    {
                        _EkranıGüncelle_();
                    }));
                }

                void _EkranıGüncelle_()
                {
                    Pencere.Mesaj.Text = Açıklama;

                    Pencere.İlerlemeÇubuğu.Value = 0;
                    if (Toplam_Kademe > 0)
                    {
                        Pencere.İlerlemeÇubuğu.Maximum = Toplam_Kademe;
                        Pencere.İlerlemeÇubuğu.Value = Geçerli_Kademe;
                    }
                    else
                    {
                        Pencere.İlerlemeÇubuğu.Maximum = 1;
                        Pencere.İlerlemeÇubuğu.Value = 1;
                    }

                    Pencere.İptalEt.Enabled = İptalEdilebilir;

                    Pencere.Tik = Environment.TickCount + Tik_ZamanAşımmı_msn;

                    Application.DoEvents();
                }
            }
        }
    }
}
