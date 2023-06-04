using ArgeMup.HazirKod;
using System;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Bekleyiniz : Form
    {
        private int Tik = 0;
        const int Tik_ZamanAşımmı_msn = 500;
        private bool _Çalışsın = false;
        private Control Tetikleyen = null;

        public bool Çalışsın
        {
            get
            {
                if (Tik < Environment.TickCount) 
                {
                    if (!Visible) Show();

                    Application.DoEvents(); 
                    Tik = Environment.TickCount + Tik_ZamanAşımmı_msn; 
                }
                
                return _Çalışsın;
            }
        }
        public int İlerleme
        {
            set
            {
                int toplam = İlerlemeÇubuğu.Value + value;

                if (toplam >= İlerlemeÇubuğu.Maximum) İlerlemeÇubuğu.Value = İlerlemeÇubuğu.Maximum;
                else İlerlemeÇubuğu.Value = toplam;
            }
        }

        public Bekleyiniz()
        {
            InitializeComponent();

            Text = "ArGeMuP " + Kendi.Adı + " V" + Kendi.Sürümü_Dosya + " " + Text;
            Icon = Properties.Resources.kendi;

            Hide();
        }
        
        public void Başlat(string Açıklama, bool İptalEdilebilir, Control Tetikleyen, int Toplam_Kademe)
        {
            Mesaj.Text = Açıklama;

            İlerlemeÇubuğu.Value = 0;
            if (Toplam_Kademe > 0) İlerlemeÇubuğu.Maximum = Toplam_Kademe;
            else
            {
                İlerlemeÇubuğu.Maximum = 1;
                İlerlemeÇubuğu.Value = 1;
            }

            İptalEt.Enabled = İptalEdilebilir;

            _Çalışsın = true;

            if (Tetikleyen != null)
            {
                Tetikleyen.Enabled = false;
                Application.DoEvents();
            }
            this.Tetikleyen = Tetikleyen;

            Tik = Environment.TickCount + Tik_ZamanAşımmı_msn;
        }
        public void Bitir()
        {
            if (Tetikleyen != null)
            {
                if (!Tetikleyen.InvokeRequired) _Bitir_();
                else
                {
                    Tetikleyen.Invoke(new Action(() =>
                    {
                        _Bitir_();
                    }));
                }

                Tetikleyen = null;
            }

            Hide();
        }
        void _Bitir_()
        {
            if (Tetikleyen.GetType() == typeof(ListBox))
            {
                ListBox lb = Tetikleyen as ListBox;
                lb.Enabled = lb.Items.Count > 0;
            }
            else Tetikleyen.Enabled = true;
        }

        private void İptalEt_Click(object sender, EventArgs e)
        {
            if (İptalEt.Enabled) _Çalışsın = false;
        }
    }
}
