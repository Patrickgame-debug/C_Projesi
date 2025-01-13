using Newtonsoft.Json;  
using System;
using System.Collections.Generic;  
using System.IO;  
using System.Threading;  

namespace C_Projesi
{
    // Öğrenci sınıfı
    public class Ogrenci
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Bolum { get; set; }
        public List<Ders> KayıtlıDersler { get; set; } = new List<Ders>();

        public void BilgiGoster()
        {
            Console.WriteLine($"Ad: {Ad}, Soyad: {Soyad}, Bölüm: {Bolum}");
        }

        public void DersKaydet(Ders ders)
        {
            KayıtlıDersler.Add(ders);
            ders.Ogrenciler.Add(this);
        }
    }

    // Ders sınıfı
    public class Ders
    {
        public string Ad { get; set; }
        public int Kredi { get; set; }
        public OgretimGorevlisi OgretimGorevlisi { get; set; }

        [JsonIgnore]  
        public List<Ogrenci> Ogrenciler { get; set; } = new List<Ogrenci>();

        public void BilgileriGoster()
        {
            Console.WriteLine($"Ders Adı: {Ad}, Kredi: {Kredi}, Öğretim Görevlisi: {OgretimGorevlisi.Ad} {OgretimGorevlisi.Soyad}");
            Console.WriteLine("Kayıtlı Öğrenciler:");
            foreach (var ogrenci in Ogrenciler)
            {
                Console.WriteLine($"{ogrenci.Ad} {ogrenci.Soyad}");
            }
        }
    }

    // Öğretim Görevlisi sınıfı
    public class OgretimGorevlisi
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Departman { get; set; }

        public void BilgiGoster()
        {
            Console.WriteLine($"Ad: {Ad}, Soyad: {Soyad}, Departman: {Departman}");
        }
    }

    // Ana menü sınıfı
    internal class Anamenu
    {
        private static int ogrenciEklemeSayisi = 0;
        private static int dersEklemeSayisi = 0;
        private static int ogretimGorevlisiEklemeSayisi = 0;
        private static int derseKayitSayisi = 0;
        private static List<Ogrenci> ogrenciler = new List<Ogrenci>();
        private static List<OgretimGorevlisi> ogretimGorevlileri = new List<OgretimGorevlisi>();
        private static List<Ders> dersler = new List<Ders>();
        static int gecersizdeneme_hakki = 3;

        // Dosya yolu
        static string ogrenciDosyaYolu = @"C:\Users\araba\source\repos\C#Projesii\Deneme10\ogrenciler.json";
        static string ogretimGorevlisiDosyaYolu = @"C:\Users\araba\source\repos\C#Projesii\Deneme10\ogretimGorevlileri.json";
        static string dersDosyaYolu = @"C:\Users\araba\source\repos\C#Projesii\Deneme10\dersler.json";

        static void Main(string[] args)
        {
            // Verileri JSON'dan okuma
            LoadDataFromJson();

            string secim;
            do
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("   Öğrenci ve Ders Yönetim Sistemi");
                Console.WriteLine("====================================");
                Console.WriteLine("\nLütfen yapmak istediğiniz işlemi seçin:");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1-  Öğrenci ve Ders Bilgilerini Görüntüle");
                Console.WriteLine("2-  Yeni Öğrenci Ekle");
                Console.WriteLine("3-  Yeni Ders Ekle");
                Console.WriteLine("4-  Öğretim Görevlisi Ekle");
                Console.WriteLine("5-  Öğrenciyi Derse Kaydet");
                Console.WriteLine("6-  Sistemden Çıkış");
                Console.WriteLine("7-  Verileri Temizle");  // Verileri temizle seçeneği
                Console.WriteLine("------------------------------------");

                bool gecerliIslem = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Seçiminizi yapınız: ");
                Console.ResetColor();
                secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        BilgileriGoruntule();
                        break;
                    case "2":
                        OgrenciEkle();
                        Thread.Sleep(1000);
                        break;
                    case "3":
                        DersEkle();
                        Thread.Sleep(1000);
                        break;
                    case "4":
                        OgretimGorevlisiEkle();
                        Thread.Sleep(1000);
                        break;
                    case "6":
                        Console.WriteLine("Çıkılıyor...");
                        Thread.Sleep(1000);
                        break;
                    case "5":
                        DersKaydet();
                        break;
                    case "7":
                        VerileriTemizle();  // Verileri temizleme fonksiyonu çağrılıyor
                        break;
                    default:
                        gecerliIslem = false;
                        Console.WriteLine("Geçersiz seçenek. 1 ile 7 arasında bir değer giriniz.");
                        Thread.Sleep(1500);
                        gecersizdeneme_hakki--;
                        if (gecersizdeneme_hakki == 0)
                        {
                            Console.WriteLine("Çok fazla geçersiz deneme yaptınız. Çıkış yapılıyor...");
                            Thread.Sleep(1500);
                            secim = "6";
                        }
                        break;
                }

                if (secim != "6" && gecerliIslem)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ana Menüye Aktarılıyorsunuz...");
                    Console.ResetColor();
                    Thread.Sleep(2000);
                }

            } while (secim != "6");
        }

        // JSON'dan veri okuma fonksiyonu
        private static void LoadDataFromJson()
        {
            if (File.Exists(ogrenciDosyaYolu))
            {
                var jsonVerisi = File.ReadAllText(ogrenciDosyaYolu);
                ogrenciler = JsonConvert.DeserializeObject<List<Ogrenci>>(jsonVerisi);
                ogrenciEklemeSayisi = ogrenciler.Count;
            }

            if (File.Exists(ogretimGorevlisiDosyaYolu))
            {
                var jsonVerisi = File.ReadAllText(ogretimGorevlisiDosyaYolu);
                ogretimGorevlileri = JsonConvert.DeserializeObject<List<OgretimGorevlisi>>(jsonVerisi);
                ogretimGorevlisiEklemeSayisi = ogretimGorevlileri.Count;
            }

            if (File.Exists(dersDosyaYolu))
            {
                var jsonVerisi = File.ReadAllText(dersDosyaYolu);
                dersler = JsonConvert.DeserializeObject<List<Ders>>(jsonVerisi);
                dersEklemeSayisi = dersler.Count;
            }
        }

        // Öğrenci, ders ve öğretim görevlisi bilgilerini görüntüleme fonksiyonu
        // Öğrenci, ders ve öğretim görevlisi bilgilerini görüntüleme fonksiyonu
        private static void BilgileriGoruntule()
        {
            Console.Clear();

            // Öğrenciler bilgilerini görüntüle
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n---- Kayıtlı Öğrenciler ----");
            Console.ResetColor();
            if (ogrenciler.Count > 0)
            {
                foreach (var ogrenci in ogrenciler)
                {
                    ogrenci.BilgiGoster();
                    Console.WriteLine("Kayıtlı Dersler:");
                    foreach (var ders in ogrenci.KayıtlıDersler)
                    {
                        Console.WriteLine($"- {ders.Ad}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Hiç öğrenci kaydedilmemiş.\n");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            // Öğretim görevlileri bilgilerini görüntüle
            Console.WriteLine("\n---- Kayıtlı Öğretim Görevlileri ----");
            Console.ResetColor();
            if (ogretimGorevlileri.Count > 0)
            {
                foreach (var ogretimGorevlisi in ogretimGorevlileri)
                {
                    ogretimGorevlisi.BilgiGoster();
                }
            }
            else
            {
                Console.WriteLine("Hiç öğretim görevlisi kaydedilmemiş.\n");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            // Dersler bilgilerini görüntüle
            Console.WriteLine("\n---- Kayıtlı Dersler ----");
            Console.ResetColor();
            if (dersler.Count > 0)
            {
                foreach (var ders in dersler)
                {
                    // Ders adı ve kredisi
                    Console.WriteLine($"Ders Adı: {ders.Ad}, Kredi: {ders.Kredi}");

                    // Öğretim görevlisi bilgisi
                    Console.WriteLine($"Öğretim Görevlisi: {ders.OgretimGorevlisi.Ad} {ders.OgretimGorevlisi.Soyad}");
                    Console.WriteLine();  // Boş satır ekleyelim
                }
            }
            else
            {
                Console.WriteLine("Hiç ders kaydedilmemiş.\n");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ana menüye dönmek için herhangi bir tuşa basınız");
            Console.ResetColor();
            Console.ReadLine();
            Thread.Sleep(1000);
        }


        // Öğrenci ekleme fonksiyonu
        private static void OgrenciEkle()
        {
            Console.Clear();
            string ad, soyad, bolum;
            int id;
            Console.WriteLine($"Toplam {ogrenciEklemeSayisi} öğrenci var.");

            // Öğrenci adı
            while (true)
            {
                Console.Write("\nÖğrenci Adı: ");
                ad = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(ad))
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş! Öğrenci adı boş bırakılamaz.");
            }

            // Öğrenci soyadı
            while (true)
            {
                Console.Write("Öğrenci Soyadı: ");
                soyad = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(soyad))
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş! Öğrenci soyadı boş bırakılamaz.");
            }

            // Öğrenci bölümü
            while (true)
            {
                Console.Write("Öğrenci Bölümü: ");
                bolum = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(bolum))
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş! Öğrenci bölümü boş bırakılamaz.");
            }

            // Öğrenci ID
            while (true)
            {
                Console.Write("Öğrenci ID: ");
                if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş! Geçerli bir sayı giriniz (pozitif bir tamsayı).");
            }
            Console.WriteLine("\n\n");

            // Yeni öğrenci oluşturma ve listeye ekleme
            Ogrenci ogrenci = new Ogrenci { Ad = ad, Soyad = soyad, Id = id, Bolum = bolum };
            ogrenciler.Add(ogrenci);
            Console.WriteLine("Öğrenci başarıyla eklendi.");
            ogrenciEklemeSayisi++;

            // Veriyi JSON'a yazma
            SaveDataToJson();
        }

        private static void DersKaydet()
        {
            Console.Clear();
            bool kontrol_derssayisi = true;
            Console.WriteLine($"Toplam {dersEklemeSayisi} ders bulunmaktadır.");

            // Dersler bilgilerini yazdırma
            Console.WriteLine("\n---- Kayıtlı Dersler ----");
            if (dersler.Count > 0)
            {
                for (int i = 0; i < dersler.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dersler[i].Ad}");
                }

                // Kullanıcının ders seçimi
                int dersSecim;
                while (true)
                {
                    Console.Write("Bir ders seçin (numara): ");
                    if (int.TryParse(Console.ReadLine(), out dersSecim) && dersSecim >= 1 && dersSecim <= dersler.Count)
                    {
                        dersSecim--;  // İndeks sıfırdan başlar, bu yüzden 1 çıkarıyoruz
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz seçim! Lütfen doğru bir ders numarası giriniz.");
                    }
                }

                Console.WriteLine("\n---- Kayıtlı Öğrenciler ----");
                if (ogrenciler.Count > 0)
                {
                    for (int i = 0; i < ogrenciler.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {ogrenciler[i].Ad} {ogrenciler[i].Soyad}");
                    }

                    // Kullanıcının öğrenci seçimi
                    int ogrenciSecim;
                    while (true)
                    {
                        Console.Write("Bir öğrenci seçin (numara): ");
                        if (int.TryParse(Console.ReadLine(), out ogrenciSecim) && ogrenciSecim >= 1 && ogrenciSecim <= ogrenciler.Count)
                        {
                            ogrenciSecim--;  // İndeks sıfırdan başlar, bu yüzden 1 çıkarıyoruz
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz seçim! Lütfen doğru bir öğrenci numarası giriniz.");
                        }
                    }

                    Ogrenci ogrenci = ogrenciler[ogrenciSecim];
                    Ders ders = dersler[dersSecim];

                    // Öğrencinin ders kaydını yap
                    ogrenci.DersKaydet(ders);
                    Console.WriteLine($"{ogrenci.Ad} {ogrenci.Soyad} adlı öğrenci, {ders.Ad} dersine kaydedildi.");

                    derseKayitSayisi++;  // Ders kaydını artırıyoruz
                }
                else
                {
                    Console.WriteLine("Hiç öğrenci bulunmamaktadır.");
                    kontrol_derssayisi = false;
                }

            }
            else
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("Hiç ders bulunmamaktadır.");
                kontrol_derssayisi = false;
            }

            // Veriyi JSON'a kaydetme
            if (kontrol_derssayisi)
                SaveDataToJson();
        }


        private static void SaveDataToJson()
        {
            // Öğrenciler JSON'a yazma
            var ogrenciJson = JsonConvert.SerializeObject(ogrenciler, Formatting.Indented);
            File.WriteAllText(ogrenciDosyaYolu, ogrenciJson);

            // Öğretim Görevlileri JSON'a yazma
            var ogretimGorevlisiJson = JsonConvert.SerializeObject(ogretimGorevlileri, Formatting.Indented);
            File.WriteAllText(ogretimGorevlisiDosyaYolu, ogretimGorevlisiJson);

            // Dersler JSON'a yazma
            var dersJson = JsonConvert.SerializeObject(dersler, Formatting.Indented);
            File.WriteAllText(dersDosyaYolu, dersJson);

            Console.WriteLine("Veriler başarıyla JSON dosyasına kaydedildi!");
        }

        // Verileri temizleme fonksiyonu
        private static void VerileriTemizle()
        {
            // Öğrenciler, öğretim görevlileri ve dersleri sıfırlıyoruz
            ogrenciler.Clear();
            ogretimGorevlileri.Clear();
            dersler.Clear();

            // JSON dosyalarındaki veriyi de sıfırlıyoruz
            File.WriteAllText(ogrenciDosyaYolu, "[]");
            File.WriteAllText(ogretimGorevlisiDosyaYolu, "[]");
            File.WriteAllText(dersDosyaYolu, "[]");

            // Sıfırlama işleminin başarılı olduğuna dair bir mesaj gösterelim
            Console.WriteLine("Tüm veriler başarıyla temizlendi!");
        }

        private static void DersEkle()
        {
            Console.Clear();
            bool kontrol_ders = true;
            Console.WriteLine($"Toplam listeye eklenmiş ders sayısı:{dersEklemeSayisi} .");
            if (ogretimGorevlileri.Count == 0)
            {
                Console.WriteLine("Öğretim görevlisi bulunamadı! Lütfen önce bir öğretim görevlisi ekleyin.");
                Console.WriteLine("Ana menüye dönülüyor...");
                System.Threading.Thread.Sleep(2000);
                return;
            }

            Console.Write("Ders Adı: ");
            string dersAd = Console.ReadLine();

            int kredi;
            while (true)
            {
                Console.Write("Ders Kredisi: ");
                if (int.TryParse(Console.ReadLine(), out kredi) && kredi > 0)
                {
                    kontrol_ders = true;
                    break;
                }
                Console.WriteLine("Geçerli bir kredi değeri girin (pozitif bir sayı).");
            }

            Console.WriteLine("Öğretim Görevlisi Seçin:");
            for (int i = 0; i < ogretimGorevlileri.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ogretimGorevlileri[i].Ad} {ogretimGorevlileri[i].Soyad}");
            }

            int ogretimSecim;
            while (true)
            {
                Console.Write("Seçiminiz (sayı): ");
                if (int.TryParse(Console.ReadLine(), out ogretimSecim) && ogretimSecim > 0 && ogretimSecim <= ogretimGorevlileri.Count)
                {
                    kontrol_ders = true;
                    ogretimSecim--;
                    break;
                }
                Console.WriteLine("Geçerli bir seçim yapın.");
                kontrol_ders = false;
            }

            OgretimGorevlisi ogretimGorevlisi = ogretimGorevlileri[ogretimSecim];

            Ders ders = new Ders { Ad = dersAd, Kredi = kredi, OgretimGorevlisi = ogretimGorevlisi };
            dersler.Add(ders);
            Console.WriteLine("Ders başarıyla eklendi!");
            if (kontrol_ders)
                dersEklemeSayisi++;

            // Veriyi JSON'a yazma
            SaveDataToJson();
        }

        // Öğretim görevlisi ekleme fonksiyonu
        private static void OgretimGorevlisiEkle()
        {
            Console.Clear();
            bool kontrol_Ogrentim;

            Console.WriteLine($"Toplam {ogretimGorevlisiEklemeSayisi} öğretim görevlisi eklendi.");
            string ad, soyad, departman;
            int id;

            // Öğretim Görevlisi Adı
            while (true)
            {
                Console.Write("Öğretim Görevlisi Adı: ");
                ad = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(ad))
                {

                    break;
                }
                Console.WriteLine("Hatalı giriş! Öğretim görevlisi adı boş bırakılamaz.");
            }

            // Öğretim Görevlisi Soyadı
            while (true)
            {
                Console.Write("Öğretim Görevlisi Soyadı: ");
                soyad = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(soyad))
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş! Öğretim görevlisi soyadı boş bırakılamaz.");
            }

            // Öğretim Görevlisi Departmanı
            while (true)
            {
                Console.Write("Öğretim Görevlisi Departmanı: ");
                departman = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(departman))
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş! Öğretim görevlisi departmanı boş bırakılamaz.");
            }

            // Öğretim Görevlisi ID
            while (true)
            {
                Console.Write("Öğretim Görevlisi ID: ");
                if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                {
                    kontrol_Ogrentim = true;
                    break;
                }
                Console.WriteLine("Hatalı giriş! Geçerli bir pozitif ID değeri giriniz.");
                kontrol_Ogrentim = false;
            }

            // Yeni öğretim görevlisi oluşturma ve listeye ekleme
            OgretimGorevlisi ogretimGorevlisi = new OgretimGorevlisi
            {
                Ad = ad,
                Soyad = soyad,
                Departman = departman,
                Id = id
            };
            ogretimGorevlileri.Add(ogretimGorevlisi);
            Console.WriteLine("Öğretim Görevlisi başarıyla eklendi!");
            if (kontrol_Ogrentim)
                ogretimGorevlisiEklemeSayisi++;

            // Veriyi JSON'a yazma
            SaveDataToJson();
        }
    }
}
