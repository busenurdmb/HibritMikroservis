# 🚀 Hibrit Mikroservis Mimarisi - Kullanıcı Kayıt ve Doğrulama Sistemi

Bu proje, bir iş mülakatı senaryosuna uygun olarak geliştirilmiş hibrit bir mikroservis mimarisi üzerinde dağıtık sistemlerin nasıl haberleşeceğini gösteren bir çalışmadır. 

---

## 🎯 Amaç

- Kullanıcı kaydı alınacak
- Doğrulama kodu e-posta ile gönderilecek (simüle)
- Kullanıcı doğrulama kodunu girecek
- Doğrulama başarılıysa event fırlatılacak
- Notification servisi bu event'i dinleyip "hoş geldiniz" maili gönderecek (simüle)
- Gönderilen mailler PostgreSQL veritabanına loglanacak

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

---

## 🧩 Proje Yapısı

```
HibritMikroservis/
├── IdentityService/               // Kullanıcı kaydı ve onay servisi
├── NotificationService/           // Event'i dinleyip mail atan servis
├── SharedKernel/                  // Ortak event modelleri
├── BuildingBlocks/                // RabbitMQ ve Mail soyutlamaları
└── docker-compose.yml             // RabbitMQ servisi
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
   → Mail gönderir (console üzerinden)
   → PostgreSQL'e MailLog olarak kaydeder
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
4. NotificationService konsolunda şu çıkar:

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
- NotificationService de artık PostgreSQL veritabanı kullanmaktadır
- Gerçek mail yerine console log kullanılmıştır (isteğe bağlı MailKit entegre edilebilir)
- Şifre hashleme veya ek servis eklenmemiştir çünkü task kapsamında istenmemektedir
- Saga tek adımlı şekilde sadelikle uygulanmıştır

---

## ✅ Sonuç

Bu proje, event-driven microservice yapısında:
- Asenkron haberleşme
- Saga Pattern
- SOLID prensipleri
- PostgreSQL üzerinden loglama
- Temiz ve sade bir task uygulamasını ortaya koyar.

Projeyi geliştiren: `Buse Nur Demirbaş` 💻





---





