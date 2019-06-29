using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Manager
{
    class MenuManager
    {
 
        private static MenuManager instance;

        public static MenuManager Instance

        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuManager();
                }
                return instance;
            }
        }
        public enum estado { menu, nivel1, nivel2, gameover, win };

        private estado gameEstado;

        public estado GameEstado{
            get { return gameEstado; }
            set
            {
                gameEstado = value;
            }
        }
    }
}
