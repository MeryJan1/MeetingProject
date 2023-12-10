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
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System.Net;
using System.Xml.Linq;

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
       
        private void Save()
        {
            UserCredential credential;
            string credentialsPath;
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if(System.IO.File.Exists($"{userProfilePath}\\OneDrive")) 
            {
                credentialsPath = $"{userProfilePath}\\OneDrive\\Desktop\\credentials.json";
            }
            else
            {
                credentialsPath = $"{userProfilePath}\\Desktop\\credentials.json";
            }

            
            
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
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
                ApplicationName = "Project",
            });

            int startHour, startMinute, endHour, endMinute;
            startHour = Convert.ToInt32(cBoxStartHours.SelectedItem); 
            startMinute = Convert.ToInt32(cBoxStartMinutes.SelectedItem);
            endHour = Convert.ToInt32(cBoxEndHours.SelectedItem);
            endMinute = Convert.ToInt32(cBoxEndMinutes.SelectedItem);

            var newEvent = new Event()
            {
                Summary = FormCalendar.title ,
                Description = FormCalenderInformationPlaning.description,
                Start = new EventDateTime
                {
                    DateTime = new DateTime(Convert.ToInt32(FormCalenderInformationPlaning.static_year), Convert.ToInt32(FormCalenderInformationPlaning.static_month), Convert.ToInt16(FormCalenderInformationPlaning.static_day), startHour, startMinute, 0),
                    TimeZone = "Europe/Istanbul",
                },
                End = new EventDateTime
                {
                    DateTime = new DateTime(Convert.ToInt32(FormCalenderInformationPlaning.static_year), Convert.ToInt32(FormCalenderInformationPlaning.static_month), Convert.ToInt32(FormCalenderInformationPlaning.static_day), endHour, endMinute, 0),
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
            Save();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormCalenderInformationPlaning formCalenderInformationPlaning = new FormCalenderInformationPlaning();
            formCalenderInformationPlaning.Show();

            Close();

        }
    }
}
