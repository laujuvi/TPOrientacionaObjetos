using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Menu
    {

        Flecha flecha;
        List<Boton> botones = new List<Boton>();
        Boton botonNuevaPartida;
        Boton botonLoad;
        Boton botonSalir;
        Boton botonActual;

        const float tiempoDeEspera = 0.3f;
        float esperaDeInput = 0;

        bool BotonJugar = true;
        bool BotonCargarPartida = false;
        bool BotonControles = false;
        bool PantallaControles = false;

        public Menu()
        {
            botonNuevaPartida = new Boton(350f, 275f, "Texturas/Vida/Corazon.png");
            botones.Add(botonNuevaPartida);

            botonSalir = new Boton(350f, 363f, "");
            botones.Add(botonSalir);

            botonNuevaPartida.SetearBotones(null, botonSalir);
            botonSalir.SetearBotones(botonNuevaPartida, null);

            botonActual = botonNuevaPartida;

            flecha = new Flecha();
            flecha.Actualizar(botonActual.posX, botonActual.posY);
        }

        public void Actualizar()
        {

            esperaDeInput += Program.deltaTime;


            botonActual = botonActual.Actualizar();

            flecha.Actualizar(botonActual.posX, botonActual.posY);

            if (Engine.GetKey(Keys.RETURN))
            {
                EnterButon();
            }

            esperaDeInput = 0;


        }

        private void EnterButon()
        {
            if (botonActual == botonNuevaPartida)
            {
                Program.estadoActual = Program.estado.nivel1;
                Program.nivel1.ResetearNivel();
                Program.nivel1.ResetearVida();
                Program.nivel2.ResetearVida();
            }
            else if (botonActual == botonSalir)

            {
                Environment.Exit(0);

            }
        }


        public void Dibujar()
        {
            Engine.Draw("Texturas/Menu/menu.png");

            flecha.Dibujar();
        }

    }


}
