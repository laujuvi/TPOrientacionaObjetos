using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Corazon
    {
        float posX;
        float posY;
        string texture;

        public Corazon(float posX, float posY, string texture)
        {
            this.posX = posX;
            this.posY = posY;
            this.texture = texture;
        }

        public void Dibujar()
        {
            Engine.Draw(texture, posX, posY);
        }
    }
}
