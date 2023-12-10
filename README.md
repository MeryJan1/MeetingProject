# Planlama Projesi

Bu proje, Windows Form uygulaması uygulaması ile yapılmıştır. Amacı farklı katılımcılar arasında yapılan etkinlşk için herkese en uygunu günü seçmektir.


## Kurulum

### .EXE Dosyası ile kurulum

- Setup dosyasına tıklayarak uygulamayı istediğiniz dizine kurun.

### GitHub ile kurulum

- Visual Studio'ya girin ve terminali açıp aşağıdaki kodu giriniz.
  
  ```Terminal
  git clone https://github.com/MeryJan1/MeetingProject
    ```
  
- NuGet paketlerini ekleyin:
  
  ```bash
  Install-Package Google.Apis
  Install-Package Google.Apis.Calendar.v3
  Install-Package Google.Apis.Auth
  Install-Package Google.Apis.Auth.OAuth2
    ```  
- Projeyi derleyin ve çalıştırın.


## Kullanım

- Uygulamayı başlatın.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/4c3a4ea7-bfff-4cd6-a2ee-da7219cfceec)

- Eğer oluşturulmuş kod ile giriş yapacaksanız "Join a Planing" butonuna; Eğer plan oluşturacaksanız veya daha önce oluşturmuş olduğunuz plan bilgilerine erişim sağlayacaksanız "Create a Planing" butonuna tıklayınız.
 
### Plan OLuşturma :

- Create a Planing tuşuna basınız ve isim soyisim bilgilerini giriniz.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/edb242c7-c8eb-456f-b1ad-8a86c0f7c9e3)

- Size uygun olan günleri seçip "OK" butonuna basınız.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/33d9490e-1726-46f0-ae8b-a28eb42fbd18)

- Başlık ve açıklama kısmını doldurup Save tuşuna basınız.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/dc4c2f71-9200-4602-a20e-f527ff0dcfb6)

- Daha sonra çıkan ekrandan COPY CODE butonuna basarak kodu kopyalayın ve OK butonuna basarak ana takvime dönebilirsiniz.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/fff01bb5-9eb5-4798-8d06-0362d0a7c6ff)

### Geçmiş Planlara Bakma:

- Takvimin yan tarafında bulunan listede, bilgisini almak istediğiniz toplantının başlığına tıklayarak gidebilirsiniz.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/268125b9-3a54-464f-b59b-b833c26c0944)

- Burada kimlerin hangi günleri seçtiğini, kimlerin giriş yaptığını, toplantıya ait kodu, plana ait başlık ve açıklamayı bulabilirsiniz.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/0407fc17-ffda-4729-85da-cc26af0a9101)

- Planın gerçekleşmesini istediğiniz günün üzerine tıklayıp planın başlangıç ve bitiş zamanlarını giriniz. Save tuşuna basın.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/8f1679b5-3e7e-48a4-80c4-1078c5447988)

- Google Takvim uygulamasına kaydediliyor ve Google Takvim uygulamasına gidiyor.

  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/0ddcfb1a-4ab0-4e2a-8f1e-aedfdf7828b7)

### Kod ile uygun tarih seçme:

- Create a Planing Butonuna tıkladıktan sonra. Kodunuzu ve isim soyismi gerekli alanlara girin ve Login butonuna basınız.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/7ac4dcad-d5ec-408f-90c6-5cf29f53f6f9)

- Size uyan günleri seçip "OK" butonuna tıklayınız.
  
  ![image](https://github.com/MeryJan1/MeetingProject/assets/125815842/0371ecd5-7a70-4638-b0a1-935e60b54e2c)


## Bağımlılıklar

- [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472)


## İletişim

- E-posta: [meryemaskaroglu91@gmail.com](mailto:meryemaskaroglu91@gmail.com)


## Versiyon Geçmişi

- v1.0.0 (2023-10-12): İlk sürüm.

