using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Manager;

namespace Game
{
    public class nivel1
    {
        Jugador jugador;
        Enemigo enemigo;

        static string fondo = "Texturas\\Fondo\\fondo.png";
        public Tilemap tilemap = new Tilemap();
        static int tamañoTile = 70;



        public nivel1()
        {
            tilemap.texturas.Add("Texturas\\Tiles\\castleMid.png");     // 0
            tilemap.texturas.Add("Texturas\\Tiles\\snowCenter.png");    // 1
            tilemap.texturas.Add("Texturas\\Tiles\\door_openTop.png");  // 2
            tilemap.texturas.Add("Texturas\\Tiles\\door_openMid.png");  // 3

            tilemap.tiles = new int[10, 14]
            {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1},
                {1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1},
                {1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1},
                {1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1},
                {1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1},
                {1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,1},
                {1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,3,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            };

            tilemap.tamañoTile = tamañoTile;

            ResetearNivel();

        }

        public void Actualizar()
        {

            enemigo.Actualizar();
            jugador.Actualizar();

            bool colision = jugador.ChequearColisiones(enemigo);
            //VidaManager.Instance.vidaJugador = jugador.vida;
            if (colision)
            {
                if (VidaManager.Instance.Vida <= 0)
                {
                    Engine.Debug("GAME OVER");
                    Program.estadoActual = Program.estado.gameover;
                    VidaManager.Instance.Vida = 3;
                }
                else
                {
                    ResetearNivel();
                    Engine.Debug(VidaManager.Instance.Vida);
                }

            }

        }

        public void Dibujar()
        {
            enemigo.Dibujar();
            jugador.Dibujar();
            Corazon();
        }

        public void ResetearVida()
        {
            jugador = new Jugador(100f, 537f, 500f, tilemap, 3);
        }

        public void ResetearNivel()
        {

            enemigo = new Enemigo(400f, 300f, 100f, 75f, 525f);
            jugador = new Jugador(100f, 537f, 500f, tilemap, VidaManager.Instance.Vida);

        }
        public void MapNivel1()
        {
            Engine.Draw(fondo);
            tilemap.Dibujar();
        }


        public void DibujarTiles()
        {
            for (int i = 0; i < tilemap.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tilemap.tiles.GetLength(1); j++)
                {
                    int numeroTile = tilemap.tiles[i, j];
                    if (numeroTile >= 0)
                    {
                        string texturaTile = tilemap.texturas[numeroTile];
                        int x = tamañoTile * j;
                        int y = tamañoTile * i;
                        Engine.Draw(texturaTile, x, y);
                    }

                }
            }
        }
        public void Corazon()
        {
            float offset = 45;
            float x = 780;

            for (int i = 1; i <= VidaManager.Instance.Vida; i++)
            {
                Engine.Draw("Texturas\\Vida\\Corazon.png", x, 20);
                x += offset;
            }
        }

    }
}
