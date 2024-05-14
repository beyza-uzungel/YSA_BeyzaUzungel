<div align="center">
  <h1>🧠 Yapay Sinir Ağı ile Harf Tanıma Yazılımı 🚀</h1>
</div>


Bu proje, C# programlama dili kullanılarak geliştirilmiş bir yapay sinir ağı (YSA) uygulamasını içerir. Proje, Windows Form App kullanılarak geliştirilmiş olup nesne yönelimli programlama ilkelerine bağlı olarak kodlanmıştır.

## 📝 Proje Açıklaması

Bu proje, A-B-C-D-E harfleri için 7x5 lik bir matrisin giriş vektörü kabul edilerek, çıkış katmanında 5 proses elemanı bulunan birçok katmanlı algılayıcı (ÇKA) ağı kullanılarak bir harf tanıma yazılımı geliştirilmesini amaçlamaktadır. Tek ara (gizli) katman kullanılarak bu amaç gerçekleştirilmiştir. Eğitim işlemleri tamamlandıktan sonra test girişleri kullanım kolaylığı açısından 7x5 lik matrisi tıklama şeklinde alınabilir. 

## Gereksinimler

- Visual Studio (projenin derlenmesi ve çalıştırılması için)
- .NET Framework

## Kullanım

Projeyi çalıştırdığınızda, ana form üzerinde harf tanıma işlemi için gerekli ara yüzü göreceksiniz. Form tasarımı serbest olmakla birlikte örnek bir form tasarımı proje içerisinde bulunmaktadır. 

- Eğitim veri seti, statik olarak projenin içine dahil edilmiştir. Ancak istenirse dosyadan ya da veritabanından okunarak kullanılabilir.
- Test giriş değerleri form üzerinden girilebilir.
- Önemli olan hata epsilon değeri ve test giriş değerlerinin form üzerinden girilebilir olmasıdır.

## 📋 Örnek Eğitim Veri Seti

```csharp
egitim = new int[5, 7, 5] { 
    { {0,0,1,0,0}, {0,1,0,1,0}, {1,0,0,0,1}, {1,0,0,0,1}, {1,1,1,1,1}, {1,0,0,0,1}, {1,0,0,0,1} },
    { {1,1,1,1,0}, {1,0,0,0,1}, {1,0,0,0,1}, {1,1,1,1,0}, {1,0,0,0,1}, {1,0,0,0,1}, {1,1,1,1,0} },
    { {0,0,1,1,1}, {0,1,0,0,0}, {1,0,0,0,0}, {1,0,0,0,0}, {1,0,0,0,0}, {0,1,0,0,0}, {0,0,1,1,1} },
    { {1,1,1,0,0}, {1,0,0,1,0}, {1,0,0,0,1}, {1,0,0,0,1}, {1,0,0,0,1}, {1,0,0,1,0}, {1,1,1,0,0} },
    { {1,1,1,1,1}, {1,0,0,0,0}, {1,0,0,0,0}, {1,1,1,1,1}, {1,0,0,0,0}, {1,0,0,0,0}, {1,1,1,1,1} }
};

