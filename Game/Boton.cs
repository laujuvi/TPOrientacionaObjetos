using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
    public class Boton {
        public float posX { get; private set; }
        public float posY { get; private set; }
        Boton botonUp;
        Boton botonDown;
        string texture;
        float timer = 0f;
        float tiempoParaApretar = 0.25f;

        public Boton(float posX, float posY, string texture) {
            this.posX = posX;
            this.posY = posY;
            this.texture = texture;
        }

        public void SetearBotones(Boton up, Boton down) {
            botonUp = up;
            botonDown = down;
        }

        public Boton Actualizar() {
            timer += Program.deltaTime;

            if (Engine.GetKey(Keys.W) && timer >= tiempoParaApretar) {
                timer = 0f;
                return GetUp();
            } else if (Engine.GetKey(Keys.S) && timer >= tiempoParaApretar) {
                timer = 0f;
                return GetDown();
            } else return this;
        }

        public void Dibujar() {
            Engine.Draw(texture, posX, posY);
        }

        public Boton GetUp() {
            if (botonUp != null) {
                return botonUp;
            } else return this;
        }

        public Boton GetDown() {
            if (botonDown != null) {
                return botonDown;
            } else return this;
        }
    }
}
