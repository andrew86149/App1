using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace App1
{
    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public const string FEATURE_NAME = "Panel - Life is Photo";
        // http://www.lifeisphoto.ru/photo.aspx?id=1972700
        // http://www.lifeisphoto.ru/photo.aspx?id=1972273
        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title="Denis Budkov",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=2008920",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Diletant",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=2009458",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Alexandr..",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1908623&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Verafefilova",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1908188&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Sanmitrich",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1908106&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Vitafil",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1908409",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Valeriya Strunnikova",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1908096",
                ClassType =typeof(Scenario5)},
            new Scenario(){ Title="Fyodor Lashkov",
                Bi = "http://www.lifeisphoto.ru/photo.aspx?id=1907966&theme=1",
                ClassType =typeof(Scenario5) },
            new Scenario() { Title="Alexandr Andronik",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1907791&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="BoNik",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1907678&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Alla Shaposhnikof",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1908014&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Petr Galileev",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1902710&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="IrishaL",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1906877&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Yevgeniy",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1908076&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Sergey Kochnev",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1907653&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario(){ Title="Vladimir Klemeshev",
                Bi = "http://www.lifeisphoto.ru/photo.aspx?id=1906501&theme=1",
                ClassType =typeof(Scenario5) },
            new Scenario() { Title="Igor Mayorov",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1908034&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Sergey Kovyak",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1907971",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Zsm",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1905194",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Grigorich",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1907741&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Sergey N",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1906717&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Sergey Klyucharev",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1907167&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Salomakin",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1907974&theme=1",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="fyb",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1952277",
                ClassType =typeof(Scenario5)},
            new Scenario() { Title="Ilonak",
                Bi = @"http://www.lifeisphoto.ru/photo.aspx?id=1971751&theme=1",
                ClassType =typeof(Scenario5)},
        };
        //http://www.lifeisphoto.ru/photo.aspx?id=1972700
        List<Scenario> Scenarios
        {
            get { return this.scenarios; }
        }

        public MainPage()
        {
            this.InitializeComponent();

            Current = this;
            SimpleTitle.Text = FEATURE_NAME;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }

        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //NotifyUser(String.Empty, NotifyType.StatusMessage);

            ListBox scenarioListBox = sender as ListBox;
            Scenario s = scenarioListBox.SelectedItem as Scenario;

            PassData passData = new PassData
            {
                BeginUri = s.Bi
            };
            if (s != null)
            {
                ScenarioFrame.Navigate(s.ClassType, passData);
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var itemCollection = new List<Scenario>();
            int i = 1;
            foreach (Scenario s in scenarios)
            {
                itemCollection.Add(new Scenario { Title = $"{i++}) {s.Title}", Bi = s.Bi, ClassType = s.ClassType });
            }
            ScenarioControl.ItemsSource = itemCollection;

            if (Window.Current.Bounds.Width < 640)
            {
                ScenarioControl.SelectedIndex = -1;
            }
            else
            {
                ScenarioControl.SelectedIndex = 0;
            }
        }
    }
}
