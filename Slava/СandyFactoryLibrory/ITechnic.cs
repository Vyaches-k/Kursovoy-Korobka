using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СandyFactoryLibrory
{
    interface ITechnic
    {
        void StopImg();
        void StartImg();
        void StartTechnic(List<Othet> otchet);
    }
}
