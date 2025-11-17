# Teknik Servis Otomasyonu Projesi

## Proje Hakkında
Bu proje, SQL Server Express ve .NET Framework Windows Forms kullanılarak hazırlanmış Teknik Servis Otomasyonu uygulamasıdır.  
Proje test aşamasındadır kullanıcı, form üzerindeki ID alanına bir değer girerek ilgili kullanıcının `name` ve `password` bilgilerini görüntüleyebilir.

## Kullanılan Teknolojiler
- Visual Studio (.NET Framework, Windows Forms)  
- SQL Server Express 22
- SQL Server Management Studio 22 (SSMS)
- C# (WinForms)  

### Test Verileri
| id | name       | password | email                     | phoneNum      |
|----|------------|----------|---------------------------|---------------|
| 1  | First User | 12345    | firstuser01@test.com      | 05516667711   |
| 2  | Second User| 54321    | seconduser02@test.com     | 05526667722   |
| 3  | Third User | 67890    | thirduser03@test.com      | 05536667733   |

## Kullanım
1. SQL Server Management Studio’da `teknikServisOtomasyonDB` oluşturmak için `\Project Setup\DataBase` içindeki `SQLQueryFull.sql` dosyasını içindeki kodları çalıştırın.  
2. Visual Studio’da proje açılır ve çalıştırılır.  
3. Form üzerinde:
   - **ID** alanına bir değer girin  
   - **Getir** butonuna basın  
4. Girilen ID’ye ait **name** ve **password** label’larda görüntülenecektir.

## Notlar
- Proje dağıtıldığında, kullanıcılar sadece SQL script’i çalıştırarak veritabanını hazır hale getirebilir, kodda değişiklik yapmasına gerek yoktur.
- Bu projede SQL Server Express 22 16.0.1000.6 kullanılmıştır ve kullanılması tavsiye edilir daha önceki sürümler ile test edilmemiştir.
