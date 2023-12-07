using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalenderForProject
{
    public partial class GoogleCalender : Form
    {
        public GoogleCalender()
        {
            InitializeComponent();
        }
       
        private void Kaydet()
        {
            UserCredential credential;

            using (var stream = new FileStream("path/to/client_secret.json", FileMode.Open, FileAccess.Read))
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
                ApplicationName = "YourAppName",
            });

            var newEvent = new Event()
            {
                Summary = FormCalendar.title ,
                Description = FormCalenderInformationPlaning.description,
                Start = new EventDateTime
                {
                    DateTime = new DateTime(Convert.ToInt16(FormCalenderInformationPlaning.static_year), Convert.ToInt16(FormCalenderInformationPlaning.static_month), Convert.ToInt16(FormCalenderInformationPlaning.static_day), Convert.ToInt16(cBoxStartHours.SelectedItem), Convert.ToInt16(cBoxStartMinutes.SelectedItem), 0),
                    TimeZone = "Europe/Istanbul",
                },
                End = new EventDateTime
                {
                    DateTime = new DateTime(Convert.ToInt16(FormCalenderInformationPlaning.static_year), Convert.ToInt16(FormCalenderInformationPlaning.static_month), Convert.ToInt16(FormCalenderInformationPlaning.static_day), Convert.ToInt16(cBoxEndHours.SelectedItem), Convert.ToInt16(cBoxEndMinutes.SelectedItem), 0),
                    TimeZone = "Europe/Istanbul",
                },
            };

            var calendarId = "primary"; // Kullanıcının ana takvimi
            var request = service.Events.Insert(newEvent, calendarId);
            var createdEvent = request.Execute();
            MessageBox.Show($"Event created: {createdEvent.HtmlLink}");

            FormCalenderInformationPlaning formCalenderInformationPlaning = new FormCalenderInformationPlaning();
            formCalenderInformationPlaning.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = FormCalenderInformationPlaning.static_day + "." + FormCalenderInformationPlaning.static_month + "." + FormCalenderInformationPlaning.static_year;
            tBoxDate.Text = date;
            Kaydet();

        }
    }
}
