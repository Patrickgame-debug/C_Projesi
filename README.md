Öğrenci ve Ders Yönetim Sistemi Ümit Güldalı

Bu proje, öğrenciler, dersler ve öğretim görevlileri ile ilgili işlemleri kolaylıkla yönetmek için geliştirilmiş bir uygulamadır. Kullanıcı dostu arayüzü ve JSON tabanlı veri kaydetme altyapısı ile veriler güvenle saklanır ve işlenir.

Özellikler
- Öğrenci, ders ve öğretim görevlisi bilgilerini görüntüleme
- Yeni öğrenci, ders veya öğretim görevlisi ekleme
- Öğrencileri derslere kaydetme
- Verileri JSON formatında saklama ve yükleme
- Verileri temizleme seçeneği

Kullanım
1. Uygulamayı başlatın.
2. Menüdeki seçeneklerden birini seçerek işlem yapın:
   - **1:** Öğrenci ve ders bilgilerini görüntüleyin.
   - **2:** Yeni bir öğrenci ekleyin.
   - **3:** Yeni bir ders ekleyin.
   - **4:** Yeni bir öğretim görevlisi ekleyin.
   - **5:** Öğrenciyi derse kaydedin.
   - **6:** Sistemden çıkış yapın.
   - **7:** Verileri temizleyin.

 Gereksinimler
- .NET Framework veya .NET Core destekleyen bir ortam
- `Newtonsoft.Json` kütüphanesi

JSON Dosyaları
Veriler aşağıdaki JSON dosyalarında saklanır:
- Öğrenciler: `ogrenciler.json`
- Öğretim Görevlileri: `ogretimGorevlileri.json`
- Dersler: `dersler.json`

Kurulum
1. Projeyi indirin veya kopyalayın.
2. Gerekli JSON dosyalarının belirtilen dosya yollarında mevcut olduğundan emin olun.
3. Uygulamayı çalıştırın ve menüden istediğiniz işlemi seçin.
