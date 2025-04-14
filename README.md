# 🚀 Hibrit Mikroservis Mimarisi - Kullanıcı Kayıt ve Doğrulama Sistemi

Bu proje, bir iş mülakatı senaryosuna uygun olarak geliştirilmiş hibrit bir mikroservis mimarisi üzerinde dağıtık sistemlerin nasıl haberleşeceğini gösteren bir çalışmadır. 

---

## 🎯 Amaç

- Kullanıcı kaydı alınacak
- Doğrulama kodu e-posta ile gönderilecek (gerçek SMTP ile)
- Kullanıcı doğrulama kodunu girecek
- Doğrulama başarılıysa event fırlatılacak
- Notification servisi bu event'i dinleyip "hoş geldiniz" maili gönderecek (gerçek mail)
- Gönderilen mailler PostgreSQL veritabanına CQRS-Mediator ile loglanacak

---
![index](https://github.com/busenurdmb/HibritMikroservis/blob/master/images/task.png)
## 🧱 Mimaride Kullanılan Teknolojiler

- .NET **9** WebAPI
- PostgreSQL (IdentityService + NotificationService)
- RabbitMQ (Docker)
- MediatR + CQRS
- FluentValidation
- SOLID ve Clean Architecture yaklaşımı
- Event-Driven Design
- Saga Pattern (Choreography)
- MailKit ile SMTP mail gönderimi

---

## 🧩 Proje Yapısı

```
HibritMikroservis/
├── Services/
│   ├── IdentityService/
│   │   ├── API
│   │   ├── Application
│   │   ├── Domain
│   │   └── Infrastructure
│   └── NotificationService/
│       ├── API
│       ├── Application
│       ├── Domain
│       └── Infrastructure
├── SharedKernel/
├── BuildingBlocks/
└── docker-compose.yml
```

---
![index](https://github.com/busenurdmb/HibritMikroservis/blob/master/images/identity.png)
![index](https://github.com/busenurdmb/HibritMikroservis/blob/master/images/register.png)
![index](https://github.com/busenurdmb/HibritMikroservis/blob/master/images/confirm.png)
## 📦 Servisler Arası Akış (Saga Pattern)

```
[POST] /api/auth/register 
   → Kullanıcı kaydı + doğrulama kodu 

[POST] /api/auth/confirm
   → Kod doğrulaması 
   → UserVerifiedIntegrationEvent fırlatılır (Saga başlar)

NotificationService:
   → Event'i dinler
   → Mail gönderir (gerçek SMTP üzerinden MailKit ile)
   → CQRS + MediatR ile PostgreSQL'e MailLog olarak kaydeder
```

---

## 🐳 Docker

```yaml
version: '3.9'
services:
  rabbitmq:
    image: rabbitmq:3-management
    ports:
      - "5673:5672"
      - "15673:15672"
 
```

Yönetim Paneli: http://localhost:15673  
Kullanıcı: `quest` - Şifre: `quest`

---

## 🧪 Test Adımları

1. `POST /api/auth/register` ile kayıt olun
2. Console'dan doğrulama kodunu alın
3. `POST /api/auth/confirm` ile kodu gönderin
4. Mail doğrudan Gmail veya SMTP sunucusuna gider ✉️
5. PostgreSQL → `MailLogs` tablosunda kayıt oluşur:
6. NotificationService konsolunda şu çıkar:

```
[Mail] To: kullanici@example.com | Subject: Hoş Geldiniz! | Body: Sisteme başarıyla kaydoldunuz.
```

5. PostgreSQL → `MailLogs` tablosunda kayıt oluşur:

```
Id | Email               | SentAt
---|---------------------|------------------------
1  | kullanici@example.com | 2024-04-13T12:34:56Z
```

---

## 🧠 Saga Pattern Nerede?

Bu yapıda **Choreography Saga Pattern** uygulanmıştır:
- IdentityService event fırlatır
- NotificationService event'i dinler
- Mail gönderimi sonrası veritabanına kayıt yapılır
- Orkestratör yoktur, event zinciriyle ilerlenir

---

## 📌 Notlar

- Mail işlemleri `IMailSender` ile soyutlanmış ve BuildingBlocks klasörüne taşınmıştır
- SMTP mail gönderimi için MailKit kütüphanesi kullanılmıştır
- Mail konfigürasyonu `appsettings.json` üzerinden `IOptions<MailSettings>` yapısıyla alınır
- NotificationService CQRS yapısına uygun şekilde handler ve command ile çalışır
- Saga Pattern basit ve etkili bir senaryo ile uygulanmıştır

---

## ✅ Sonuç

Bu proje, event-driven microservice yapısında:
- Gerçek zamanlı e-posta işlemi
- Asenkron haberleşme (RabbitMQ)
- Saga Pattern uygulaması
- CQRS + MediatR altyapısı
- PostgreSQL üzerinde kalıcı loglama
- Temiz, modüler ve mülakata uygun yapı

Projeyi geliştiren: `Buse Nur Demirbaş` 💻





---





