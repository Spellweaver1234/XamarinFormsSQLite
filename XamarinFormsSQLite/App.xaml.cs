using System;
using System.IO;
using System.Reflection;

using Xamarin.Forms;

namespace XamarinFormsSQLite
{
    public partial class App : Application
    {
        // создание пустой БД
        //public const string DATABASE_NAME = "friends2.db";
        //public static FriendAsyncRepository database;
        //public static FriendAsyncRepository Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            database = new FriendAsyncRepository(
        //                Path.Combine(
        //                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
        //        }
        //        return database;
        //    }
        //}

        // подключение к существующей БД
        public const string DATABASE_NAME = "friends5.db";
        public static FriendAsyncRepository database;
        public static FriendAsyncRepository Database
        {
            get
            {
                if (database == null)
                {
                    // путь, по которому будет находиться база данных
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
                    // если база данных не существует (еще не скопирована)
                    if (!File.Exists(dbPath))
                    {
                        // получаем текущую сборку
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        // берем из нее ресурс базы данных и создаем из него поток
                        using (Stream stream = assembly.GetManifestResourceStream($"XamarinFormsSQLite.{DATABASE_NAME}"))
                        {
                            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                                fs.Flush();
                            }
                        }
                    }
                    database = new FriendAsyncRepository(dbPath);
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
