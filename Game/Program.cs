using System;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {
        public enum estado { menu, nivel1, nivel2, gameover, win };
        public static estado estadoActual = estado.menu;

        static Menu Menu;
        public static nivel2 nivel2 { get; private set; }
        public static nivel1 nivel1 { get; private set; }
        static GameOver GameOver;
        static Win Win;

        static float tiempoActual;
        public static float deltaTime { get; private set; }
        static void Main(string[] args)
        {
            Engine.Initialize();

            Menu = new Menu();
            nivel1 = new nivel1();
            nivel2 = new nivel2();
            GameOver = new GameOver();
            Win = new Win();

            DateTime fechaInicio = DateTime.Now;
            float tiempoFrameAnterior = 0;

            while (true)
            {

                TimeSpan tiempoDesdeInicio = DateTime.Now - fechaInicio;
                tiempoActual = (float)tiempoDesdeInicio.TotalSeconds;
                deltaTime = tiempoActual - tiempoFrameAnterior;
                tiempoFrameAnterior = tiempoActual;

                Engine.Clear();
                if (estadoActual == estado.menu)
                {
                    Menu.Actualizar();
                    Menu.Dibujar();
                }
                else if (estadoActual == estado.nivel1)
                {
                    nivel1.MapNivel1();
                    nivel1.Actualizar();
                    nivel1.Dibujar();
                }
                else if (estadoActual == estado.nivel2)
                {
                    nivel2.MapNivel2();
                    nivel2.Actualizar();
                    nivel2.Dibujar();
                }
                else if (estadoActual == estado.gameover)
                {
                    GameOver.Actualizar();
                    GameOver.Dibujar();
                }
                else if (estadoActual == estado.win)
                {
                    Win.Actualizar();
                    Win.Dibujar();
                }
                Engine.Show();
            }
        }
    }
}