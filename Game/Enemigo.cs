using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class Enemigo
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public float ancho { get; private set; }
        public float alto { get; private set; }
        float velocidad = 100;
        float rangoTop;
        float rangoBot;

        public Enemigo()
        {

        }

        public Enemigo(float x, float y, float velocidad, float rangoTop, float rangoBot, float ancho = 180, float alto = 178)
        {
            this.x = x;
            this.y = y;
            this.velocidad = velocidad;
            this.rangoTop = rangoTop;
            this.rangoBot = rangoBot;
            this.ancho = ancho;
            this.alto = alto;
        }

        public void Actualizar()
        {
            if (y >= rangoBot || y <= rangoTop)
            {
                velocidad = -velocidad;
            }

            y += velocidad * Program.deltaTime;
        }

        public void Dibujar()
        {
            Engine.Draw("Texturas/ship.png", x, y, 0.25f, 0.25f, 0, ancho / 2, alto / 2);
        }
    }
}