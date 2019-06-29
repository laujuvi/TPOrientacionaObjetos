using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameOver
    {
        float espera = 0;

        public void Actualizar()
        {
            espera += Program.deltaTime;
            if (espera >= 3)
            {
                Program.estadoActual = Program.estado.menu;

                espera = 0;
            
            }
        }

        public void Dibujar ()
        {
            Engine.Draw("Texturas/Menu/lose.png");
        }

    }
}
