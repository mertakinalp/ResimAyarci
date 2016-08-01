using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using ImajBoyutlandirici.Ayarlar;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;


namespace ImajBoyutlandirici.Boyutlandirma
{
    public class ImajBoyutlandirma
    {


        private ImajBoyutlandiriciAyarlari[] _Ayarlar;

        public ImajBoyutlandiriciAyarlari[] Ayarlar
        {
            get { return _Ayarlar; }
            set { _Ayarlar = value; }
        }


        private Image _Resim;

        public Image Resim
        {
            get { return _Resim; }
            set { _Resim = value; }
        }
        private float _EOran;

        protected float EOran
        {
            get { return _EOran; }
            set { _EOran = value; }
        }

        private float _Oran;

        protected float Oran
        {
            get { return _Oran; }
            set { _Oran = value; }
        }
        private Enum _kalite;

        public Enum Kalite
        {
            get { return _kalite; }
            set { _kalite = value; }
        }

        public enum Kaliteler
        {
            Bicubic,
            Bilinear,
            Default,
            High,
            HighQualityBicubic,
            HighQualityBilinear,
            Invalid,
            Low,
            NearestNeighbor
        }
      

        private void BoyutKucultucu(ImajBoyutlandiriciAyarlari Ayarlar)
        {
            EOran = (float)Ayarlar.EYukseklik / (float)Ayarlar.EGenislik;

            Oran = (float)Ayarlar.Yukseklik / (float)Ayarlar.Genislik;

            Ayarlar.Yukseklik = (int)(Ayarlar.Genislik * EOran);

            Bitmap btmp = new Bitmap(Ayarlar.Genislik, Ayarlar.Yukseklik, Resim.PixelFormat);

            Graphics YeniResim = Graphics.FromImage(btmp);

            YeniResim.DrawImage(Resim, 0, 0, Ayarlar.Genislik, Ayarlar.Yukseklik);

            if (File.Exists(Ayarlar.Hedef) == true) { File.Delete(Ayarlar.Hedef); }

            btmp.Save(Ayarlar.Hedef);

            btmp.Dispose();
            YeniResim.Dispose();


            GC.Collect();
        }



        public ImajBoyutlandirma(ImajBoyutlandiriciAyarlari[] ayarlar, string kaynakdosyadizini, string kaynakdosyaadi, Kaliteler kalite)
        {

           kaynakdosyaadi = kaynakdosyaadi.ToLower();

            if (string.IsNullOrEmpty(kaynakdosyadizini) || string.IsNullOrEmpty(kaynakdosyaadi)) throw new Exception("Kaynak Dosya dizini veya Kaynak Dosya adı boş olamaz ; Kaynak Dosya Dizini = " + kaynakdosyadizini + " ; Kaynak Dosya Adı = " + kaynakdosyaadi);
            else if ((kaynakdosyaadi.EndsWith(".jpg") || kaynakdosyaadi.EndsWith(".gif") || kaynakdosyaadi.EndsWith(".png") || kaynakdosyaadi.EndsWith(".bmp") || kaynakdosyaadi.EndsWith(".jpeg")) == false) throw new Exception("Kaynak dosya Resim formatı Taşımalı ; Kaynak = " + kaynakdosyaadi);
            else if (File.Exists(kaynakdosyadizini + kaynakdosyaadi) == false) throw new Exception("Kaynak gösterilen dizindeki dosya mevcut değil ; Kaynak = " + kaynakdosyadizini + kaynakdosyaadi);
            else if (ayarlar.Length <= 0) throw new Exception("Ayarlar Elemanı en az 1 olabilir ; ayarlar eleman sayısı = " + ayarlar.Length.ToString());

            this.Ayarlar = ayarlar;


            foreach (ImajBoyutlandiriciAyarlari Ayar in this.Ayarlar)
            {
                Ayar.Kaynak = kaynakdosyadizini + kaynakdosyaadi;
                
                this.Resim = ((Image)(Bitmap.FromFile(Ayar.Kaynak).Clone()));
                //EncoderParameter kalite = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Ayar.Kalite);
                
                Ayar.EGenislik = Resim.Width;
                
                Ayar.EYukseklik = Resim.Height;
                
                Ayar.EBoyut = Resim.Size;
                
                Ayar.Hedef = Ayar.Hedef;


                if (Ayar.Hedef.Contains("prev1") == true || Ayar.Hedef.Contains("prev2") == true)
                {

                   
                    if (Resim.Width < Resim.Height)
                    {

                        Ayar.Genislik = Ayar.Yukseklik;

                        Ayar.Yukseklik = Ayar.Genislik;
                    }
                }

              

                ImajKaydedici(OrtadanParcalayici(Ayar.Genislik, Ayar.Yukseklik,Kaliteler.HighQualityBilinear), Ayar.Hedef);
                GC.Collect();
            }

        }
        private Image OrtadanParcalayici(int GelenGenislik, int GelenYukseklik, Kaliteler GelenKalite)
        {
            int GelenKaynakGenislik = Resim.Width;

            int GelenKaynakYukseklik = Resim.Height;

            int EskiX = 0;

            int EskiY = 0;

            int YeniX = 0;

            int YeniY = 0;

            float nOranti = 0;

            float nOrantiG = 0;

            float nOrantiY = 0;

            nOrantiG = ((float)GelenGenislik / (float)GelenKaynakGenislik);

            nOrantiY = ((float)GelenYukseklik / (float)GelenKaynakYukseklik);

            if (nOrantiY < nOrantiG)
            {
                nOranti = nOrantiG;

                YeniY = (int)((GelenYukseklik - (GelenKaynakYukseklik * nOranti)) / 2);


            }
            else
            {
                nOranti = nOrantiY;

                YeniX = (int)((GelenGenislik - (GelenKaynakGenislik * nOranti)) / 2);

            }

            int YeniGenislik = (int)(GelenKaynakGenislik * nOranti);

            int YeniYukseklik = (int)(GelenKaynakYukseklik * nOranti);


            Bitmap YeniFoto = new Bitmap(GelenGenislik, GelenYukseklik, PixelFormat.Format24bppRgb);

            YeniFoto.SetResolution(Resim.HorizontalResolution, Resim.VerticalResolution);
            

            Graphics Grafik = Graphics.FromImage(YeniFoto);

            switch (GelenKalite)
            {
                case Kaliteler.Bicubic:
                    Grafik.InterpolationMode = InterpolationMode.Bicubic;
                    break;
                case Kaliteler.Bilinear:
                    Grafik.InterpolationMode = InterpolationMode.Bilinear;
                    break;
                case Kaliteler.Default:
                    Grafik.InterpolationMode = InterpolationMode.Default;
                    break;
                case Kaliteler.High:
                    Grafik.InterpolationMode = InterpolationMode.High;
                    break;
                case Kaliteler.HighQualityBicubic:
                    Grafik.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    break;
                case Kaliteler.HighQualityBilinear:
                    Grafik.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    break;
                case Kaliteler.Invalid:
                    Grafik.InterpolationMode = InterpolationMode.Invalid;
                    break;
                case Kaliteler.Low:
                    Grafik.InterpolationMode = InterpolationMode.Low;
                    break;
                case Kaliteler.NearestNeighbor:
                    Grafik.InterpolationMode = InterpolationMode.NearestNeighbor;
                    break;
                default:
                    Grafik.InterpolationMode = InterpolationMode.Default;
                    break;
            }

 
            Grafik.DrawImage(Resim, new Rectangle(YeniX, YeniY, YeniGenislik, YeniYukseklik), new Rectangle(EskiX, EskiY, GelenKaynakGenislik, GelenKaynakYukseklik), GraphicsUnit.Pixel);


            Grafik.Dispose();

            GC.Collect();

            return YeniFoto;
            


        }

        public static string degisti6;
        private void ImajKaydedici(Image KaydedilecekResim, string KaydedilecekHedef)
        {


            EncoderParameter qualityParam =
      new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)75 );
            
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            

            
            
            if (File.Exists(KaydedilecekHedef) == true) File.Delete(KaydedilecekHedef);
            KaydedilecekResim.Save(KaydedilecekHedef,jpegCodec,encoderParams);
            KaydedilecekResim.Dispose();
            GC.Collect();


        }
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        } 


    }
}
