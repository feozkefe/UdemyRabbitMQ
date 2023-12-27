# RabbitMQ ile Asenkron İletişim - Örnek Projeler

Bu proje, RabbitMQ'nun temellerini öğrenmek ve kullanarak gerçek hayat senaryolarını uygulamak amacıyla oluşturulmuştur.

## Proje Açıklaması

Bu proje, RabbitMQ mesaj kuyruk sistemini kullanarak iki senaryoyu gerçekleştirmektedir. Her iki senaryo da asenkron iletişimi ve uzun süren işlemlerin arka planda yürütülmesini amaçlamaktadır.

1. **Senaryo 1: Resimlere watermark ekleme işlemi**
   Bu senaryo için *githubrabbitmq.web.watermarkwebapp* projesi kullanılmalıdır. Bu projeyi çalıştırarak, resimlere watermark eklemeyi sağlayan web uygulamasını kullanabilirsiniz.

2. **Senaryo 2: Web uygulamasında tablolardan excel oluşturma işlemi**
   Bu senaryo için *githubrabbitmq.web.excelcreator* ve *filecreatorexcelservice* projeleri birlikte çalıştırılmalıdır. *githubrabbitmq.web.excelcreator*, web uygulamasının olduğu kısmı temsil ederken, *filecreatorexcelservice* ise arka plandaki Worker Service'i ifade eder. Bu iki proje birlikte çalıştırıldığında, tablolardan excel oluşturma işlemi gerçekleşir.

## Kullanım

Bu projeyi kullanmak için aşağıdaki adımları izleyebilirsiniz:

* **RabbitMQ Kurulumu**
  * RabbitMQ *container* olarak ayağa kaldırılabilir veya cloud ortamında kurulum yapılabilir.

* **Proje Kurulumu**
  * Proje dosyalarını indirin veya klonlayın.
  * Her senaryonun açıklamasında belirtilen proje veya projeleri çalıştırın.

* **Senaryo 1: Resimlere watermark ekleme işlemi**
  * *githubrabbitmq.web.watermarkwebapp* projesini çalıştırın.
  * Web uygulamasında resim yükleyin ve işlemin başarılı bir şekilde gerçekleştiğini görün.

* **Senaryo 2: Tablolardan excel oluşturma işlemi**
  * *githubrabbitmq.web.excelcreator* ve *filecreatorexcelservice* projelerini birlikte çalıştırın.
  * Web uygulamasında tablolardan excel oluşturmayı deneyin ve işlemin başarıyla tamamlandığını görün.

## Teknolojiler ve Araçlar

- ASP.NET Core MVC
- RabbitMQ
- C#

## Kurs Bilgileri

Bu proje, Udemy'de Fatih Çakıroğlu'nun "Asp.net Core + Rabbit MQ" kursunu takip ederek oluşturulmuştur.
