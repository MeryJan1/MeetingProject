using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace CalenderForProject
{
    public partial class GoogleCalender : Form
    {
        public GoogleCalender()
        {
            InitializeComponent();

            string date = FormCalenderInformationPlaning.static_day + "." + FormCalenderInformationPlaning.static_month + "." + FormCalenderInformationPlaning.static_year;
            lblDate.Text = date;
        }
       
        private void Kaydet()
        {
            UserCredential credential;

            using (var stream = new FileStream($"{Form1.userProfilePath}\\Documents\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Oluşturulan credential ile CalendarService oluşturulur
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = FormLogin.userNameSurname,
            });

            int startHour, startMinute, endHour, endMinute;
            startHour = Convert.ToInt32(cBoxStartHours.SelectedItem); 
            startMinute = Convert.ToInt32(cBoxStartMinutes.SelectedItem);
            endHour = Convert.ToInt32(cBoxEndHours.SelectedItem);
            endMinute = Convert.ToInt32(cBoxEndMinutes.SelectedItem);

            if (int.TryParse(cBoxStartHours.SelectedItem?.ToString(), out startHour) &&
                int.TryParse(cBoxStartMinutes.SelectedItem?.ToString(), out startMinute) &&
                int.TryParse(cBoxEndHours.SelectedItem?.ToString(), out endHour) &&
                int.TryParse(cBoxEndMinutes.SelectedItem?.ToString(), out endMinute))
            {
                // Geçerli aralık kontrolü
                if (startHour < 0 || startHour > 23 || startMinute < 0 || startMinute > 59 ||
                    endHour < 0 || endHour > 23 || endMinute < 0 || endMinute > 59)
                {
                    // Hata işleme veya kullanıcıya bildirim ekleme
                    // Örneğin: MessageBox.Show("Geçersiz saat veya dakika değerleri!");
                }
            }
            else
            {
                // Hata işleme veya kullanıcıya bildirim ekleme
                // Örneğin: MessageBox.Show("Geçersiz saat veya dakika değerleri!");
            }
            var newEvent = new Event()
            {
                Summary = FormCalendar.title ,
                Description = FormCalenderInformationPlaning.description,
                Start = new EventDateTime
                {
                    DateTime = new DateTime(Convert.ToInt16(FormCalenderInformationPlaning.static_year), Convert.ToInt16(FormCalenderInformationPlaning.static_month), Convert.ToInt16(FormCalenderInformationPlaning.static_day), startHour, startMinute, 0),
                    TimeZone = "Europe/Istanbul",
                },
                End = new EventDateTime
                {
                    DateTime = new DateTime(Convert.ToInt16(FormCalenderInformationPlaning.static_year), Convert.ToInt16(FormCalenderInformationPlaning.static_month), Convert.ToInt16(FormCalenderInformationPlaning.static_day), endHour, endMinute, 0),
                    TimeZone = "Europe/Istanbul",
                },
            };

            var calendarId = "primary"; // Kullanıcının ana takvimi
            var request = service.Events.Insert(newEvent, calendarId);
            var createdEvent = request.Execute();
            MessageBox.Show($"Event created: {createdEvent.HtmlLink}");
            string htmlLink = createdEvent.HtmlLink;

            if (!string.IsNullOrEmpty(htmlLink) && Uri.IsWellFormedUriString(htmlLink, UriKind.Absolute))
            {
                try
                {
                    // Tarayıcıda aç
                    Process.Start(new ProcessStartInfo(htmlLink) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bildirim ver
                    MessageBox.Show($"Error opening link: {ex.Message}");
                }
            }

            this.Close();

            FormCalenderInformationPlaning formCalenderInformationPlaning = new FormCalenderInformationPlaning();
            formCalenderInformationPlaning.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kaydet();

        }
    }
}
