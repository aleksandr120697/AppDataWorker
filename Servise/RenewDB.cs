using AppDataWorker.Data;
using System;

namespace AppDataWorker.Servise
{
    public class RenewDB
    {
        public bool update_now = false;
        public void StartRenew()
        {
            DataWorker dw = new DataWorker();
            if(update_now == false)
            {
                Console.WriteLine("Обновление запустилось");
                dw.RenewApteks();
                //DataWorker.CreateProducts();
                update_now = true;
            }
            
        }


    }


}
