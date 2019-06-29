using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game {
    public class Tilemap {
        public int[,] tiles;
        public List<string> texturas = new List<string>();
        public float tamañoTile;

        public void Dibujar() {
            for (int fila = 0; fila < tiles.GetLength(0); fila++) {
                for (int col = 0; col < tiles.GetLength(1); col++) {
                    int numeroTile = tiles[fila, col];
                    if (numeroTile >= 0) {
                        float posX = col * tamañoTile;
                        float posY = fila * tamañoTile;
                        string tex = texturas[numeroTile];
                        Engine.Draw(tex, posX, posY);
                    }
                }
            }
        }


    }
}