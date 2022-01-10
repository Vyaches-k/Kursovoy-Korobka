using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СandyFactoryLibrory
{
    public class CandyFactory
    {        
        public static bool emply = false;
        Loader ld;
        public static Random r = new Random();     
        public static Label lbstatus;
        Thread myThread = new Thread(new ParameterizedThreadStart(Work));
        public List<Technic> konv = new List<Technic>();
        public List<Othet> otchet = new List<Othet>();
        public CandyFactory(Loader l, Label label)
        {
            lbstatus = label;
            ld = l;          
        }
        public bool IsWork()
        {
            return myThread.IsAlive;

        }
        public void Open()
        {
            myThread = new Thread(new ParameterizedThreadStart(Work));
            myThread.Start(ld);
            foreach (var item in konv)
            {
                item.StartTechnic(otchet);
            }
        }
        public void Close()
        {
            myThread.Abort();
            foreach (var item in konv)
            {
                item.myThread.Abort();
                item.StopImg();               
            }
        }
        static void Work(object obj)
        {                                     
            if (lbstatus.Text.Equals("Склад: 0"))               
            {                   
                Loader ld = (Loader)obj;
                ld.Send();
                Thread.Sleep(1000);
                CandyFactory.lbstatus.Invoke((MethodInvoker)delegate
                {
                    CandyFactory.lbstatus.Text = "Ожидаем заказ";
                });
            }
            Thread.Sleep(3000);
            Work(obj); 
        }
    }
}
