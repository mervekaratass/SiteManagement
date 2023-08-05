# 📝SiteManagement
Bu proje Patika ile gerçekleştirilen Sipay .Net Bootcampinin final case projesidir. 

📫Bizden istenilenlere bir bakalım.
<p>Bir sitede yöneticisiniz. Sitenizde yer alan dairelerin aidat ve ortak kullanım elektrik, su ve doğalgaz faturalarının yönetimini bir sistem üzerinden yapacaksınız. </p>

<p>İki tip kullanıcınız var: </p>

➡️ 1- Admin/Yönetici 

- [x] Daire bilgilerini girebilir. 
- [x] İkamet eden kullanıcı bilgilerini girer. 
- [x] Daire başına ödenmesi gereken aidat ve fatura bilgilerini girer(Aylık olarak). Toplu veya tek tek atama yapılabilir. 
- [x] Gelen ödeme bilgilerini görür. 
- [x] Gelen mesajları görür. 
- [x] Mesajların okunmuş/okunmamış/yeni mesaj olduğu anlaşılmalı. 
- [x] Aylık olarak borç-alacak listesini görür. 
- [x] Kişileri listeler, düzenler, siler. 
- [x] Daire/konut bilgilerini listeler, düzenler siler.

➡️ 2- Kullanıcı 
- [x] Kendisine atanan fatura ve aidat bilgilerini görür.
- [x] Sadece kredi kartı ile ödeme yapabilir. Yöneticiye mesaj gönderebilir.

✍🏻 Sistemin İşleyişi: 
Sistem kullanılmaya başladığında ilk olarak; Yönetici daire bilgilerini girer. Kullanıcı bilgilerini girer.Giriş yapması için otomatik olarak bir şifre oluşturulur.  Kullanıcıları dairelere atar .Ay bazlı olarak aidat bilgilerini girer.Ay bazlı olarak fatura bilgilerini girer .Arayüz dışında kullanıcıların kredi kartı ile ödeme yapabilmesi için ayrı bir servis yazılacaktır. Bu servisde sistemde ki her bir kullanıcı için banka bilgileri(bakiye, kredi kartı no vb.) kontrol edilerek ödeme yapılması sağlanır. 

Gereksinimler: 
Web projesi için: .Net 
Sistemin yönetimi/database için MS SQL Server / PostgreSQL
Sunum in dokumantasyon (Postman,swagger vs.)  

## SİTE MANAGEMENT
Öncelikle projemiz bir .Net projesidir.Projede veritabanı olarak MS SQL, dökümantasyon olarak Swagger kullanılmıştır. Genric Repository Pattern uygulanarak daha yönetilebilir bir sitem oluşturulmuştur. Ayrıca projemizde fluent validation ve auto mapper da kullanılmıştır.
Öncelikle veri tabanı bağlantı yolunu appsetting.json içerisine yazıyoruz. Bunu yaparak uygulama içerisine hardcodelarımızı yazmaktansa daha genel bir yerde kolay bir şekilde yönetilmesini sağlıyoruz.
Böylece bir havuzdaki musluklar gibi hangisini istyorsam o musluktan verileri çekmemi sağlıyor.

```c#
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DbType": "Sql",
    "MsSqlConnection": "Server=DESKTOP-U2SF89T; Database=SiteManagement;Trusted_Connection=True; Encrypt=False;",
    "PostgreSqlConnection": "User ID=postgres;Password=12345;Server=localhost;Port=5435;Database=SiteManagement;Integrated Security=true;Pooling=true;"
  }
```

Daha sonra katmanlarımızı oluşturuyoruz.Katmanalrımız


