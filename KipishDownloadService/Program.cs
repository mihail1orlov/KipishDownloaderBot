using System;
using System.ServiceProcess;
using KipishDownloaderBot;
using ServiceCommon;
using Telegram.Bot;

namespace KipishDownloadService
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += UnhandledExceptionHandler;
            InitLog();
            var svc = new MainService(new IService[]
            {
                new TelegramBotService(new TelegramBotClient(AppSettings.Key)), 
            });

            if (Array.IndexOf(args, "console") != -1 || Array.IndexOf(args, "c") != -1)
            {
                svc.StartSvc();
                Console.WriteLine("Press a key for exit...");
                Console.ReadKey(true);
                svc.StopSvc();

            }
            else
            {
                ServiceBase.Run(svc);
            }
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            const string method = "UnhandledExceptionHandler";
            var ex = (Exception)args.ExceptionObject;
            Console.WriteLine(ex == null ? "Error!" : $"{method}\n{ex}");
        }

        private static void InitLog()
        {
            //Logger.Level = Config.Get.Log.Level;

            try
            {
                //Logger.Dir = Config.Get.Log.Dir;
                //Directory.CreateDirectory(Logger.Dir);
            }
            catch
            {
            }

            //Logger.Prefix = Config.Get.Log.Prefix;
            //Logger.Start();
            //Logger.Write(Level.Info, "Старт сервера");
        }
    }
}
