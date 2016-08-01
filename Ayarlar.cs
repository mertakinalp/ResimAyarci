using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace ImajBoyutlandirici.Ayarlar
{

    public class ImajBoyutlandiriciAyarlari
    {

        private static int _Prev1Yukseklik;

        public static int Prev1Yukseklik
        {
            get {
                _Prev1Yukseklik = 379;
                return _Prev1Yukseklik; }
        }

        private static int _Prev1Genislik;

        public static int Prev1Genislik
        {
            get 
            {

                _Prev1Genislik = 576;
                
                return _Prev1Genislik; 
            }
        }

        private static int _Prev2Yuseklik;

        public static int Prev2Yuseklik
        {
            get { _Prev2Yuseklik = 600; return _Prev2Yuseklik; }

        }

        private static int _Prev2Genislik;

        public int Prev2Genislik
        {
            get { _Prev2Genislik = 800; return _Prev2Genislik; }
   
        }


        private static int _Thumb3Yukseklik;

        public static int Thumb3Yukseklik
        {
            get { _Thumb3Yukseklik = 88; return _Thumb3Yukseklik; }
        }

        private static int _Thumb3Genislik;

        public static int Thumb3Genislik
        {
            get { _Thumb3Genislik = 251; return _Thumb3Genislik; }

        }

        private static int _Thumb4Yuseklik;

        public static int Thumb4Yuseklik
        {
            get { _Thumb4Yuseklik = 87; return _Thumb4Yuseklik; }
        }

        private static int _Thumb4Genislik;

        public static int Thumb4Genislik
        {
            get { _Thumb4Genislik = 113; return _Thumb4Genislik; }
        }

        private static int _Flash1Yukseklik;

        public static int Flash1Yukseklik
        {
            get { _Flash1Yukseklik = 260; return _Flash1Yukseklik; }
        }


        private static int _Flash1Genislik;

        public static int Flash1Genislik
        {
            get { _Flash1Genislik = 560; return _Flash1Genislik; }
        }

        private static int _Flash2Yukseklik;

        public static int Flash2Yukseklik
        {
            get { _Flash2Yukseklik = 199; return _Flash2Yukseklik; }
        }

        private static int _Flash2Genislik;

        public static int Flash2Genislik
        {
            get { _Flash2Genislik = 550; return _Flash2Genislik; }
        }

        private static int _ListYukseklik;

        public static int ListYukseklik
        {
            get { _ListYukseklik = 99; return _ListYukseklik; }
        }

        private static int _ListGenislik;

        public static int ListGenislik
        {
            get { _ListGenislik = 236; return _ListGenislik; }
        }

        private static int _List2Yukseklik;

        public static int List2Yukseklik
        {
            get { _List2Yukseklik = 101; return _List2Yukseklik; }
        }

        private static int _List2Genislik;

        public static int List2Genislik
        {
            get { _List2Genislik = 284; return _List2Genislik; }
        }

        private static int _Thumb1Yukseklik;

        public static int Thumb1Yukseklik
        {
            get { _Thumb1Yukseklik = 52; return _Thumb1Yukseklik; }
        }

        private static int _Thumb1Genislik;

        public static int Thumb1Genislik
        {
            get { _Thumb1Genislik = 92; return _Thumb1Genislik; }
        }

        private static int _Thumb2Yukseklik;

        public static int Thumb2Yukseklik
        {
            get { _Thumb2Yukseklik = 30; return _Thumb2Yukseklik; }
        }

        private static int _Thumb2Genislik;

        public static int Thumb2Genislik
        {
            get { _Thumb2Genislik = 67; return _Thumb2Genislik; }
        }

        private string _Kaynak;

        public string Kaynak
        {
            get { return _Kaynak; }
            set
            {
                _Kaynak = value;        
            }
        }

        private string _Hedef;

        public string Hedef
        {
            get { return _Hedef; }
            set { _Hedef = value; }
        }

        private int _Yukseklik;

        public int Yukseklik
        {
            get { return _Yukseklik; }
            set { _Yukseklik = value; }
        }
        private int _Genislik;

        public int Genislik
        {
            get { return _Genislik; }
            set { _Genislik = value; }
        }
       

        private int _EYukseklik;

        public int EYukseklik
        {
            get { return _EYukseklik; }
            set { _EYukseklik = value; }
        }
        private int _EGenislik;

        public int EGenislik
        {
            get { return _EGenislik; }
            set { _EGenislik = value; }
        }

        private Size _EBoyut;

        public Size EBoyut
        {
            get { return _EBoyut; }
            set { _EBoyut = value; }
        }




        public ImajBoyutlandiriciAyarlari(int yukseklik, int genislik,string hedefdosyadizini,string hedefdosyaadi)
        {
            



            if ( string.IsNullOrEmpty(hedefdosyadizini) || string.IsNullOrEmpty(hedefdosyaadi)) throw new Exception("Hedef dosya adı,dizini veya Kaynak dosya adı,dizini Boş Olamaz;  Hedef Dosya Adı = " + hedefdosyaadi+ " ; Hedef Dosya Dizini = " + hedefdosyadizini);
            else if (yukseklik <= 0 || genislik <= 0) throw new Exception("Yükseklik veya Genişlik 0 yada daha küçük olamaz ; Yükseklik = " + yukseklik.ToString() + " ; Genişlik = " + genislik.ToString());
            

            
            this.Yukseklik = yukseklik;
            this.Genislik = genislik;




            

            if (Directory.Exists(hedefdosyadizini) == false)
            {

                try
                {
                    Directory.CreateDirectory(hedefdosyadizini);
                }
                catch (Exception Exm)
                {

                    throw new Exception("Olmayan Hedef Dosya Dizini Yaratılamadı ; " + Exm.Message);
                }
            }

            if (File.Exists(hedefdosyadizini + hedefdosyaadi) == false)
            {
                try
                {
                   // File.Create(hedefdosyadizini + hedefdosyaadi);

                  //  Image dosya = Image.FromFile(hedefdosyadizini + hedefdosyaadi);

                    //dosya.Save(hedefdosyadizini + hedefdosyaadi);
                }
                catch (Exception exm)
                {
                    throw new Exception("Olmayan hedef dosya yaratılamadı ; " + exm.Message);
                }

            }

            
            this.Hedef = hedefdosyadizini + hedefdosyaadi;
        }

    }
}
