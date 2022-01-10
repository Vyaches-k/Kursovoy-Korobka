using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СandyFactoryLibrory
{
    public class Loader 
    {
        Thread myThread;
        PictureBox pb;
        static int step = 2;
        bool gruzit = false;
        public Loader(PictureBox box)
        {
            pb = box;
        }
        public void Gruzit()
        {
            if (gruzit)
            {
                pb.Image = Image.FromFile("Img//Pogr.jpg");
                gruzit = false;
            }
            else
            {
                pb.Image = Image.FromFile("Img//Pogrlow.jpg");
                gruzit = true;
            }
        }

        public void Send()
        {
            myThread = new Thread(new ParameterizedThreadStart(Go));
            myThread.Start(this);
        }
        public static void Go(object obj)
        {            
            Loader loader = (Loader)obj;
            PictureBox pb = loader.pb;
            if (pb.Location.X >= 352)
            {
                Thread.Sleep(1500);
                loader.Gruzit();
                CandyFactory.lbstatus.Invoke((MethodInvoker)delegate
                {
                    CandyFactory.lbstatus.Text = "Склад: 100";
                });
                
                step = -2;
            }
            pb.Invoke((MethodInvoker)delegate
            {              
                pb.Location = new Point(pb.Location.X + step, pb.Location.Y);
            });
            if (pb.Location.X < 155)
            {
                loader.Gruzit();
                step = 2;
                pb.Invoke((MethodInvoker)delegate
                {
                    pb.Location = new Point(pb.Location.X + step, pb.Location.Y);
                });
                Thread.CurrentThread.Abort();
            }

            Thread.Sleep(20);
            Go(loader);
        }
    }
}
