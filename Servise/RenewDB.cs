using AppDataWorker.Data;
using System;

namespace AppDataWorker.Servise
{
    public class RenewDB
    {
        public bool update_now = false;
        public void StartRenew()
        {
            if(update_now == false)
            {
                Console.WriteLine("Обновление запустилось");
                DataWorker.UpdateApteks();
                update_now = true;
            }
            
        }


    }


}
