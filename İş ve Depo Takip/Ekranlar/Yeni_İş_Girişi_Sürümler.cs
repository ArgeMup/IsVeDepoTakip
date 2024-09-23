using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Yeni_İş_Girişi_Sürümler : Form
    {
        public static bool Varmı(IDepo_Eleman SeriNoDalı)
        {
            Banka.Talep_Ayıkla_SeriNoDalı(SeriNoDalı, out string SeriNo, out _, out _, out _, out _);
            IDepo_Eleman Geçmiş_SeriNoDalı = Banka.Tablo_Dal(null, Banka.TabloTürü.Geçmiş_İşler, "Talepler/" + SeriNo);
            return Geçmiş_SeriNoDalı != null && Geçmiş_SeriNoDalı.Elemanları.Length >= 2;
        }

        public Yeni_İş_Girişi_Sürümler(IDepo_Eleman SeriNoDalı)
        {
            InitializeComponent();
            Tag = SeriNoDalı;
        }
        private void Yeni_İş_Girişi_Sürümler_Shown(object sender, EventArgs e)
        {
            Banka.Talep_Ayıkla_SeriNoDalı(Tag as IDepo_Eleman, out string SeriNo, out _, out _, out _, out _);
            IDepo_Eleman Geçmiş = Banka.Tablo_Dal(null, Banka.TabloTürü.Geçmiş_İşler, "Talepler/" + SeriNo);
            if (Geçmiş == null || Geçmiş.Elemanları.Length < 2) return;

            for (int i = 0; i < Geçmiş.Elemanları.Length; i++)
            {
                Sürümler.SelectionColor = Color.Black;
                Sürümler.AppendText(Environment.NewLine + Environment.NewLine + "------------------------------------------------------------" + Environment.NewLine + Environment.NewLine);

                IDepo_Eleman ö, s;
                string _ö, _s;
                if (i == 0)
                {
                    //ilk sürümü her zaman normal yazdır
                    ö = Geçmiş.Elemanları[0];
                    s = Geçmiş.Elemanları[0];
                }
                else
                {
                    ö = Geçmiş.Elemanları[i - 1];
                    s = Geçmiş.Elemanları[i];

                    Sürümler.SelectionColor = Color.Red;
                }

                Sürümler.AppendText(s.Adı);
                Sürümler.SelectionColor = Color.Black;
                Sürümler.AppendText(", ");

                //Kullanıcı
                Sürümler.SelectionColor = (ö[0] == s[0]) ? Color.Black : Color.Red;
                Sürümler.AppendText(s.Oku(null, "Kullanıcı adı yok", 0));
                Sürümler.SelectionColor = Color.Black;
                Sürümler.AppendText(", ");

                //Türü
                Sürümler.SelectionColor = (ö[1] == s[1]) ? Color.Black : Color.Red;
                Sürümler.AppendText(s.Oku(null, "Silindi", 1));
                Sürümler.SelectionColor = Color.Black;
                Sürümler.AppendText(", ");

                //Dosya eki sayısı
                Sürümler.SelectionColor = (ö[2] == s[2]) ? Color.Black : Color.Red;
                Sürümler.AppendText(s.Oku(null, "Dosya eki yok", 2));

                //seri no dalı
                ö = ö.Elemanları[0];
                s = s.Elemanları[0];

                //<Hasta> / <İskonto> / Notlar / Teslim edilme tarihi
                Sürümler.AppendText(Environment.NewLine + "\t");
                int enbüyük_iii = Hesapla.EnBüyük(ö.İçeriği.Length, s.İçeriği.Length);
                enbüyük_iii = enbüyük_iii == 3 ? 2 : enbüyük_iii; //notların virgül koydurtmasını engelle
                for (int iii = 0; iii < enbüyük_iii; iii++)
                {
                    if (iii == 2) continue; //notlar

                    _ö = ö != null && ö.İçeriği != null && ö.İçeriği.Length > iii && ö[iii].DoluMu() ? ö[iii] : "Boş";
                    _s = s != null && s.İçeriği != null && s.İçeriği.Length > iii && s[iii].DoluMu() ? s[iii] : "Boş";
                    Sürümler.SelectionColor = (_ö == _s) ? Color.Black : Color.Red;
                    Sürümler.AppendText(_s);

                    if (iii != enbüyük_iii - 1)
                    {
                        Sürümler.SelectionColor = Color.Black;
                        Sürümler.AppendText(", ");
                    }
                }

                //Notlar
                Sürümler.AppendText(Environment.NewLine);
                _ö = ö != null && ö.İçeriği != null && ö.İçeriği.Length > 2 && ö[2].DoluMu() ? ö[2].Replace(Environment.NewLine, "\n\t\t") : "Not yok";
                _s = s != null && s.İçeriği != null && s.İçeriği.Length > 2 && s[2].DoluMu() ? s[2].Replace(Environment.NewLine, "\n\t\t") : "Not yok";
                Sürümler.SelectionColor = (_ö == _s) ? Color.Black : Color.Red;
                Sürümler.AppendText("\t\t" + _s);

                //iş türleri
                int enbüyük_aaa = Hesapla.EnBüyük(ö.Elemanları.Length, s.Elemanları.Length);
                for (int aaa = 0; aaa < enbüyük_aaa; aaa++)
                {
                    //iş kabul tarihi
                    _ö = ö != null && ö.Elemanları != null && ö.Elemanları.Length > aaa ? ö.Elemanları[aaa].Adı : "Boş";
                    _s = s != null && s.Elemanları != null && s.Elemanları.Length > aaa ? s.Elemanları[aaa].Adı : "Boş";
                    Sürümler.SelectionColor = (_ö == _s) ? Color.Black : Color.Red;
                    Sürümler.AppendText(Environment.NewLine + "\t" + _s);

                    Sürümler.SelectionColor = Color.Black;
                    Sürümler.AppendText(", ");

                    //<İş Türü> / <İş Çıkış Tarihi> / Ücret1 / Ücret2 - Atla / <adet ve konum bayt dizisi>
                    int enbüyük_bbb = Hesapla.EnBüyük(ö.Elemanları.Length > aaa ? ö.Elemanları[aaa].İçeriği.Length : 0, s.Elemanları.Length > aaa ? s.Elemanları[aaa].İçeriği.Length : 0);
                    for (int bbb = 0; bbb < enbüyük_bbb; bbb++)
                    {
                        if (bbb == 3) continue; //Ücret2

                        _ö = ö != null && ö.Elemanları != null && ö.Elemanları.Length > aaa && ö.Elemanları[aaa].İçeriği != null && ö.Elemanları[aaa].İçeriği.Length > bbb && ö.Elemanları[aaa].İçeriği[bbb].DoluMu() ? ö.Elemanları[aaa].İçeriği[bbb] : "Boş";
                        _s = s != null && s.Elemanları != null && s.Elemanları.Length > aaa && s.Elemanları[aaa].İçeriği != null && s.Elemanları[aaa].İçeriği.Length > bbb && s.Elemanları[aaa].İçeriği[bbb].DoluMu() ? s.Elemanları[aaa].İçeriği[bbb] : "Boş";

                        if (bbb == 4)
                        {
                            //adet ve konum bayt dizisi
                            if (_ö != "Boş") _ö = _bd_yazıya_(ö.Elemanları[aaa].Oku_BaytDizisi(null, null, bbb));
                            if (_s != "Boş") _s = _bd_yazıya_(s.Elemanları[aaa].Oku_BaytDizisi(null, null, bbb));

                            string _bd_yazıya_(byte[] _bd_)
                            {
                                string _s_ = null;
                                foreach (byte _b_ in _bd_) _s_ += _b_.ToString() + " ";
                                _s_ = _s_.TrimEnd();
                                if (_bd_.Length > 1) _s_ = _s_.Insert(2, "- ");
                                return _s_;
                            }
                        }

                        Sürümler.SelectionColor = (_ö == _s) ? Color.Black : Color.Red;
                        Sürümler.AppendText(_s);

                        if (bbb != enbüyük_bbb - 1)
                        {
                            Sürümler.SelectionColor = Color.Black;
                            Sürümler.AppendText(", ");
                        }
                    }
                }

                Application.DoEvents();
            }
        }

        private void Çıkış_Geri_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
