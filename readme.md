# Teknik Servis Otomasyonu Projesi

## 📌 Proje Hakkında
Bu proje, **SQL Server Express** ve **.NET Framework Windows Forms (C#)** kullanılarak geliştirilmiş bir **Teknik Servis Otomasyonu** uygulamasıdır.  
Amaç; teknik servise gelen cihazların kayıt altına alınması, servis sürecinin uçtan uca takip edilmesi ve kullanıcı rollerine göre yetkilendirilmiş işlemlerin yönetilmesidir.

Uygulama; müşteri, teknisyen, depo, muhasebe ve admin rollerini destekleyen çok katmanlı bir servis takip sistemi sunar.

---

## 🛠 Kullanılan Teknolojiler
- Visual Studio (.NET Framework, Windows Forms)
- C# (WinForms)
- SQL Server Express 2022
- SQL Server Management Studio (SSMS) 2022

---

## 🗄 Veritabanı Bilgileri
**Veritabanı Adı:** `teknikServisOtomasyonDB`

Veritabanı, tek bir SQL script ile sıfırdan oluşturulacak şekilde tasarlanmıştır.  
Script çalıştırıldığında:
- Mevcut veritabanı varsa silinir
- Yeniden oluşturulur
- Tüm tablolar ve ilişkiler kurulur
- Örnek test verileri eklenir

---

## 📋 Veritabanı Tabloları

### **Users**
Kullanıcı bilgilerini ve rollerini tutar.
- Roller: `Customer`, `Staff`, `Admin`, `Accountant`, `Warehouse`
- Email ve telefon alanları **unique**’dir.

### **ServiceRecords**
Servis taleplerinin ana tablosudur.
- Cihaz bilgileri
- Problem açıklaması
- Servis durumu
- Müşteri ve atanmış personel bilgileri
- Base64 formatında cihaz görseli (`Picture64`)

**Servis Durumları:**
- Talep Alındı  
- Müşteriden Cihaz Bekleniyor  
- Cihaz Kontrol Ediliyor  
- Ücret Hesaplanıyor  
- Ücret Onayı Bekleniyor  
- İşlemde  
- Teslime Hazır  
- Tamamlandı  
- Rapor Edildi  
- İptal Edildi  

### **ServiceOperations**
Servis sürecinde yapılan işlemleri ve maliyetlerini tutar.

### **ServiceComments**
Müşterilerin servis sonrası yorum ve puanlarını tutar.

### **ServiceReports**
Teknisyenlerin oluşturduğu servis raporlarını içerir.

📌 `ServiceOperations`, `ServiceComments` ve `ServiceReports` tabloları  
`ServiceRecords` tablosuna **ON DELETE CASCADE** ile bağlıdır.

---

## 👥 Rol Bazlı Yetkiler

### **Customer (Müşteri)**
- Servis talebi oluşturma
- Kendi servis kayıtlarını görüntüleme
- Servis durumu takibi
- Servis tamamlandıktan sonra yorum ve puanlama

### **Staff (Teknisyen)**
- Atanmış servis taleplerini görüntüleme
- Servis işlemleri ekleme
- Servisi rapor etme
- Servis durumunu güncelleme

### **Warehouse (Depo)**
- Müşteriden cihaz teslim alındı onayı
- Cihaz giriş–çıkış takibi

### **Accountant (Muhasebe)**
- Servis maliyetlerini görüntüleme
- Ücret hesaplama ve onay süreci yönetimi

### **Admin**
- Rapor yönetimi

---

## 🧱 Sistem Mimarisi

Proje, **katmanlı mimari** yaklaşımı ile geliştirilmiştir:

- **Presentation Layer (UI)**  
  Windows Forms arayüzleri

- **Business Logic Layer (BLL)**  
  İş kuralları, rol kontrolleri, durum geçişleri

- **Data Access Layer (DAL)**  
  SQL Server bağlantıları, parametreli sorgular, CRUD işlemleri

---

## 🔄 Servis Süreci Akışı
1. Müşteri servis talebi oluşturur  
2. Talep durumu **Talep Alındı**  
3. Depo cihaz teslimini onaylar  
4. Teknisyen kontrol ve işlemleri yapar  
5. Muhasebe ücret hesaplar  
6. Müşteri ücret onayı verir  
7. Servis tamamlanır  
8. Müşteri cihazı teslim alır
9. Müşteri yorum ve puanlama yapar  

---

## 🧪 Test Kullanıcıları

| Rol        | Email           | Şifre     |
|----------- |-----------------|-----------|
| Customer   | ilk@demo.com    | 123456789 |
| Customer   | ikinci@demo.com | 123456789 |
| Customer   | ucuncu@demo.com | 123456789 |
| Staff      | staff@demo.com  | 123456789 |
| Accountant | acc@demo.com    | 123456789 |
| Warehouse  | ware@demo.com   | 123456789 |
| Admin      | admin@demo.com  | 123456789 |

---

## ▶️ Kurulum ve Çalıştırma

1. **SQL Server Management Studio**’yu açın  
2. `Project Setup/DataBase/SQLQueryFull.sql` dosyasındaki tüm kodları çalıştırın  
3. Visual Studio’da projeyi açın  
4. Projeyi çalıştırın

---

## 📝 Notlar
- Proje dağıtımında yalnızca SQL script çalıştırılması yeterlidir
- SQL Server Express **2022 (16.0.1000.6)** sürümü ile test edilmiştir
- Daha eski sürümlerle test edilmemiştir
- `Picture64` alanı Base64 formatında görsel verisi tutar

---

## 🚀 Geliştirilebilir Alanlar
- Şifrelerin hash + salt ile saklanması
- Base64 yerine dosya sistemi veya blob storage kullanımı
- Enum tabanlı servis durumları
- PDF / Excel raporlama
- Yetki bazlı menü yönetimi

---

## 📄 Lisans
Bu proje eğitim ve demo amaçlı geliştirilmiştir.
