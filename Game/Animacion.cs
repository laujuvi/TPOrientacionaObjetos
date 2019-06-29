using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Animacion
    {
        public List<Texture> frames = new List<Texture>();
        public bool loop { get; private set; }
        public float velocidad { get; private set; }
        public int actualFrame { get; private set; }

        public float time { get; private set; }

        public Animacion(List<Texture> frames, bool loop = true, float velocidad = 0.05f)
        {
            this.frames = frames;
            this.loop = loop;
            this.velocidad = velocidad;
        }

        public void Play()
        {
            time += Program.deltaTime;

            if (time >= velocidad)
            {
                actualFrame++;
                time = 0f;

                if (actualFrame >= frames.Count)
                {
                    actualFrame = 0;
                }
            }
        }
    }
}
