using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СandyFactoryLibrory
{
    public class Technic:ITechnic
    {
        string name = "Default";
        string made = "Default";
        byte age = 0;
        public Thread myThread;
        static object locker = new object();
        //public bool breaking = false;
        PictureBox pBox;
        Bitmap animatedImage = new Bitmap("img\\Chups.gif");
        public Technic(string n, string md, byte a,PictureBox pb)
        {
            name = n;
            made = md;
            age = a;
            pBox = pb;          
            
        }
        public void StartTechnic(List<Othet> otchet)
        {
            this.pBox.Paint += new PaintEventHandler(pictureBox1_Paint);
            StartImg();
            myThread = new Thread(new ParameterizedThreadStart(Work));
            myThread.Start((this, otchet));
           
        }
        public static void Work(object obj)
        {
            (Technic, List<Othet>) mechIOtch = ((Technic, List<Othet>))obj;
            Thread.Sleep(300);
            //lock (locker)
            //{
                if (CandyFactory.lbstatus.Text.Equals("Склад: 0") || CandyFactory.lbstatus.Text.Equals("Ожидаем заказ"))            
                {               
                    mechIOtch.Item1.StopImg();               
                    Thread.Sleep(1000);
                    Work(obj);
                }
                else if(CandyFactory.lbstatus.Text.Equals("Склад: 100")) mechIOtch.Item1.StartImg();
            //}
                CandyFactory.lbstatus.Invoke((MethodInvoker)delegate
                {
                    CandyFactory.lbstatus.Text = "Склад: " + (Convert.ToInt32(CandyFactory.lbstatus.Text.Substring(7, CandyFactory.lbstatus.Text.Length - 7)) - 1);
                });

                if (CandyFactory.r.Next(50) == 0)
                {
                    mechIOtch.Item1.StopImg();
                    Thread.Sleep(5000);
                    mechIOtch.Item1.StartImg();

                mechIOtch.Item2.Add(new Othet(mechIOtch.Item1, DateTime.Now));
                }
            
            Work(obj);
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            AnimateImage();
            ImageAnimator.UpdateFrames();
            e.Graphics.DrawImage(this.animatedImage, 0, 0, pBox.Width, pBox.Height);
            //pbCandy.Image = (Image)animatedImage.;
        }
        // Bitmap animatedImage = (Bitmap)Properties.Resources.cat21;
        bool currentlyAnimating = false;

        //This method begins the animation. 
        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                //Begin the animation only once. 
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }
  
        //call the paint method of the PictureBox. 
        void OnFrameChanged(object sender, EventArgs e)
        {
            pBox.Invalidate();
        }

        public void StopImg()
        {
            ImageAnimator.StopAnimate(this.animatedImage, new EventHandler(this.OnFrameChanged));
        }

        public void StartImg()
        {
            ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Made
        {
            get
            {
                return made;
            }
            set
            {
                made = value;
            }
        }
        public byte Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

    }
}
