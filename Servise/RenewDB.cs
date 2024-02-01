using AppDataWorker.Data;
using System;

namespace AppDataWorker.Servise
{
    public class RenewDB
    {
        
        public static void StartRenew()
        {
            Timer timer = null;
            timer = new Timer(callback, null, 0, 300000);
        }

        private static void callback(Object state)
        {
            Console.WriteLine("Обновление запустилось");
            DataWorker.UpdateApteks();
        }


    }


}
