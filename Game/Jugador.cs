using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Manager;

namespace Game
{
    public class Jugador
    {
        public Tilemap tilemap { get; private set; }
        float escala = 0.05f;
        float wTex = 720;

        public enum Estado { CorrerDer, CorrerIzq, IdleDer, IdleIzq } 

        static float xGlobal;
        static float yGlobal;
        static int wGame = 980;
        static int hGame = 700;
        static float despX;
        static float despY;

        public float x { get; private set; }
        public float y { get; private set; }
        public float ancho { get; private set; }
        public float alto { get; private set; }
        public float velocidad { get; private set; }
        
        List<Animacion> lAnim = new List<Animacion>();
       int iSentido = 0;

        public Animacion animCorrerIzq { get; private set; }
        public Animacion animCorrerDer { get; private set; }
        public Animacion animIdleIzq { get; private set; }
        public Animacion animIdleDer { get; private set; }
        public Estado estadoActual = Estado.IdleDer;

        public float GetXMin()
        {
            return x - ((wTex * escala) / 2);
        }
        public float GetXMax()
        {
            return x + ((wTex * escala) / 2);
        }

        public Jugador()
        {
        }

        public Jugador(float x, float y, float velocidad, Tilemap tilemap, int vida = 3, float ancho = 53, float alto = 46)
        {
            this.x = x;
            this.y = y;
            this.velocidad = velocidad;
            this.ancho = ancho;
            this.alto = alto;
            this.tilemap = tilemap;
            VidaManager.Instance.Vida = vida;


            despX = wGame - 14 * tilemap.tamañoTile;
            despY = hGame - 10 * tilemap.tamañoTile;

            CargarAnimaciones();
        }

        public bool ChequearColisiones(Enemigo enemigo)
        {


            float diffX = Math.Abs(x - enemigo.x);
            float diffY = Math.Abs(y - enemigo.y);

            float sumaMitadAnchos = ancho / 2 + enemigo.ancho / 2;
            float sumaMitadAltos = alto / 2 + enemigo.alto / 2;

            if (diffX <= sumaMitadAnchos && diffY <= sumaMitadAltos)
            {
                VidaManager.Instance.Vida--;
                return true;

            }
            else
            {
                return false;
            }

        }

        public void Actualizar()
        {
            iSentido = -1;

            if (Engine.GetKey(Keys.D))
            {
                x += velocidad * Program.deltaTime;

                estadoActual = Estado.CorrerDer;
                animCorrerDer.Play();

                if (x >= wGame - ((wTex * escala) / 2))
                    x = wGame - (wTex * escala) / 2;

                xGlobal -= velocidad * Program.deltaTime;
                if (xGlobal <= despX)
                    xGlobal = despX;

                int fila = (int)((y + (-yGlobal)) / tilemap.tamañoTile);
                int columna = (int)((GetXMax() + (-xGlobal)) / tilemap.tamañoTile);
                if (columna > 14) { columna = 14; }
                if (columna < 0) { columna = 0; }
                if (fila > 8) { fila = 8; }
                if (fila < 0) { fila = 0; }
                int numeroTile = tilemap.tiles[fila, columna];

                if (numeroTile == 2 || numeroTile == 3)
                {
                    if (Program.estadoActual == Program.estado.nivel1)
                    {
                        Program.nivel2.ResetearNivel2();
                        Program.estadoActual = Program.estado.nivel2;
                    }
                    else if (Program.estadoActual == Program.estado.nivel2)
                    {
                        Engine.Debug("GANASTE");

                            Program.estadoActual = Program.estado.win;
                       

                    }

                }
                else if (numeroTile != -1)
                {
                    x -= velocidad * Program.deltaTime;
                    xGlobal += velocidad * Program.deltaTime;
                }
            }
            else if (Engine.GetKey(Keys.A))
            {
                x -= velocidad * Program.deltaTime;

                estadoActual = Estado.CorrerIzq;
                animCorrerIzq.Play();


                if (x <= ((wTex * escala) / 2))
                    x = (wTex * escala) / 2;

                xGlobal += velocidad * Program.deltaTime;
                if (xGlobal >= 0)
                    xGlobal = 0;

                int fila = (int)((y + (-yGlobal)) / tilemap.tamañoTile);
                int columna = (int)((GetXMin() + (-xGlobal)) / tilemap.tamañoTile);
                if (columna > 14) { columna = 14; }
                if (columna < 0) { columna = 0; }
                if (fila > 8) { fila = 8; }
                if (fila < 0) { fila = 0; }
                int numeroTile = tilemap.tiles[fila, columna];

                if (numeroTile == 2 || numeroTile == 3)
                {
                    if (Program.estadoActual == Program.estado.nivel1)
                    {
                        Program.nivel2.ResetearNivel2();
                        Program.nivel2.ResetearVida();
                        Program.estadoActual = Program.estado.nivel2;


                    }
                    else if (Program.estadoActual == Program.estado.nivel2)
                    {
                        Engine.Debug("GANASTE");
                        Program.estadoActual = Program.estado.win;
                        
                    }

                }
                else if (numeroTile != -1)
                {
                    x += velocidad * Program.deltaTime;
                    xGlobal -= velocidad * Program.deltaTime;
                }
            }
            else
            {
                if (estadoActual == Estado.CorrerDer)
                {
                    estadoActual = Estado.IdleDer;
                    animIdleDer.Play();
                }
                else if (estadoActual == Estado.CorrerIzq)
                {
                    estadoActual = Estado.IdleIzq;
                    animIdleIzq.Play();
                }
            }

        }

        public void Dibujar()
        {
            if (estadoActual == Estado.IdleIzq)
            {
                Engine.Draw(animIdleIzq.frames[animIdleIzq.actualFrame], x, y, 1, 1, 0f, ancho / 2, alto / 2);
            }
            else if (estadoActual == Estado.IdleDer)
            {
                Engine.Draw(animIdleDer.frames[animIdleDer.actualFrame], x, y, 1, 1, 0f, ancho / 2, alto / 2);
            }
            else if (estadoActual == Estado.CorrerIzq)
            {
                Engine.Draw(animCorrerIzq.frames[animCorrerIzq.actualFrame], x, y, 1, 1, 0f, ancho / 2, alto / 2);
            }
            else if (estadoActual == Estado.CorrerDer)
            {
                Engine.Draw(animCorrerDer.frames[animCorrerDer.actualFrame], x, y, 1, 1, 0f, ancho / 2, alto / 2);
            }
        }

        private void CargarAnimaciones()
        {
            List<Texture> correrDer = new List<Texture>();

            for (int i = 1; i <= 14; i++)
            {
                correrDer.Add(Engine.GetTexture("Texturas/Derecha/" + i.ToString() + ".png"));
                Engine.Debug("Texturas / Derecha / " + i.ToString() + ".png" + "  Tengo: " + correrDer.Count);
            }

            List<Texture> correrIzq = new List<Texture>();

            for (int i = 1; i <= 14; i++)
            {
                correrIzq.Add(Engine.GetTexture("Texturas/Izquierda/" + i.ToString() + ".png"));
                Engine.Debug("Texturas / Derecha / " + i.ToString() + ".png" + "  Tengo: " + correrDer.Count);
            }

            List<Texture> idleDer = new List<Texture>();
            idleDer.Add(Engine.GetTexture("Texturas/Idle.png"));

            List<Texture> idleIzq = new List<Texture>();
            idleIzq.Add(Engine.GetTexture("Texturas/IdleIzq.png"));

            animCorrerDer = new Animacion(correrDer, false);
            animCorrerIzq = new Animacion(correrIzq, false);
            animIdleDer = new Animacion(idleDer, false);
            animIdleIzq = new Animacion(idleIzq, false);
        }


    }
}
