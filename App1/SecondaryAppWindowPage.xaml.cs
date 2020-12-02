using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SecondaryAppWindowPage : Page
    {
        Queue<string> queue = new Queue<string>();
        string begin = @"http://photos.lifeisphoto.ru/";
        int m = 0;
        Point x1, x2;
        Uri uri;
        public SecondaryAppWindowPage()
        {
            this.InitializeComponent();
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Scenario scenario = e.Parameter as Scenario;
            //uri = new Uri(scenario.BeginUri);
            PassData passData = e.Parameter as PassData;
            uri = new Uri(passData.BeginUri);
            //Sectext.Text = uri.ToString();
            await RazborAsync();
            base.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        private async Task RazborAsync()
        {
            Exception exception = null;
            try
            {
                //var uri = new Uri(@"http://www.lifeisphoto.ru/photo.aspx?id=1908188&theme=1");
                var httpClient = new HttpClient();
                var result = await httpClient.GetStringAsync(uri);
                string s = result;
                string bf = "";

                string pattern = @"pP\[\d+\]=\'\d+\'";
                RegexOptions options = RegexOptions.Multiline;
                MatchCollection matchs = Regex.Matches(s, pattern, options);
                foreach (Match m in matchs)
                {
                    bf = m.ToString();
                    int idx = bf.IndexOf('=');
                    int jdx = bf.LastIndexOf('\'');
                    bf = bf.Substring(idx + 2, jdx - idx - 2);

                    queue.Enqueue(bf);
                }
                m = 0;
                bf = queue.ElementAt(m);
                s = begin + bf.Substring(0, 3) + "/0/" + bf + ".jpg";
                Uri url = new Uri(s);
                BitmapImage bitmap = new BitmapImage(url);
                //Image image = new Image();
                image.Source = bitmap;
                //m = 630;
                //forw.IsEnabled = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            if (exception != null)
            {
                MessageDialog msgdlg = new MessageDialog(exception.Message, "Other Error");
                await msgdlg.ShowAsync();
            }
        }
        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            x1 = e.GetCurrentPoint(this).Position;
            base.OnPointerPressed(e);
        }
        async protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            x2 = e.GetCurrentPoint(this).Position;
            if (x1.X > x2.X)
            {
                await MyForw();
            }
            else
            {
                if (m - 2 >= 0) m -= 2;
                await MyForw();
            }
            base.OnPointerReleased(e);
        }
        private async Task MyForw()
        {
            Exception exception = null;
            try
            {
                if ((m++ - 1) > queue.Count) m = 0;

                string bf = queue.ElementAt(m);
                string sbstr;
                if (bf.Substring(0, 1).Equals("1")) sbstr = bf.Substring(0, 3);
                else sbstr = bf.Substring(0, 2);
                string s = begin + sbstr + "/0/" + bf + ".jpg";
                Uri url = new Uri(s);
                BitmapImage bitmap = new BitmapImage(url);
                image.Source = bitmap;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            if (exception != null)
            {
                MessageDialog msgdlg = new MessageDialog(exception.Message, "Forward Error");
                await msgdlg.ShowAsync();
            }
        }
    }
}
