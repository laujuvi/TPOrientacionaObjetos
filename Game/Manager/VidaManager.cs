using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Manager
{
    public class VidaManager
    {
        private static VidaManager instance;

         public static VidaManager Instance

        {
            get
            {
                if (instance == null)
                {
                    instance = new VidaManager();
                }
                return instance;
            }
        }
        private int vidaJugador;

        public int Vida
        {
            get { return vidaJugador; }
            set
            {
                vidaJugador = value; 
            }
        }
    }
}
