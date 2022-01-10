using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СandyFactoryLibrory
{
    public class Othet
    {
        Technic Konv;
        DateTime dt;
        public Othet(Technic k, DateTime tn)
        {
            Konv = k;
            dt = tn;
        }
        public string Name
        {
            get
            {
                return Konv.Name;
            }
            set
            {
                Konv.Name = value;
            }
        }
        public string Made
        {
            get
            {
                return Konv.Made;
            }
            set
            {
                Konv.Made = value;
            }
        }
        public byte Age
        {
            get
            {
                return Konv.Age;
            }
            set
            {
                Konv.Age = value;
            }
        }
        public string Time
        {
            get
            {
                // выводим всегда две цыфры   
                // (00:00)  
                string s = dt.Hour.ToString("00");
                s += " : ";
                s += dt.Minute.ToString("00");
                s += " : " + dt.Second.ToString("00");
                return s;
            }
        }
    }
}
