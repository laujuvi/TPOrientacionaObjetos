using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
    public class Flecha {
        float posX;
        float posY;
        float offset = 30f;

        public void Actualizar(float x, float y) {
            posX = x - offset;
            posY = y;
        }

        public void Dibujar() {
            Engine.Draw("Texturas\\Menu\\flecha.png", posX, posY);
        }
    }
}