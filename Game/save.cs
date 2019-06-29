using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
    static class Save {
        static byte[] GetBytes(Jugador jugador, Enemigo enemigo, Program.estado estado, Enemigo enemigo1 = null) {
            List<byte> bytes = new List<byte>();

            byte[] estadoBytes = BitConverter.GetBytes((int)estado);
            bytes.AddRange(estadoBytes);

            byte[] yPersonajeBytes = BitConverter.GetBytes(jugador.y);
            bytes.AddRange(yPersonajeBytes);

            byte[] xPersonajeBytes = BitConverter.GetBytes(jugador.x);
            bytes.AddRange(xPersonajeBytes);

            byte[] yEnemigoBytes = BitConverter.GetBytes(enemigo.y);
            bytes.AddRange(yEnemigoBytes);

            byte[] xEnemigoBytes = BitConverter.GetBytes(enemigo.x);
            bytes.AddRange(xEnemigoBytes);
            if (estado == Program.estado.nivel2) {

                byte[] yEnemigo1Bytes = BitConverter.GetBytes(enemigo1.y);
                bytes.AddRange(yEnemigo1Bytes);

                byte[] xEnemigo1Bytes = BitConverter.GetBytes(enemigo1.x);
                bytes.AddRange(xEnemigo1Bytes);
            }
            return bytes.ToArray();

        }

        public static void SaveBytesFile(Program.estado estado, Jugador jugador, Enemigo enemigo, Enemigo enemigo1 = null) {
            byte[] bytes = GetBytes(jugador, enemigo, estado, enemigo1);

            string filePath = Directory.GetCurrentDirectory() + "\\savefile.sav";

            using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate)) {
                foreach (byte b in bytes) {
                    stream.WriteByte(b);
                }
                stream.Close();
            }

        }

        static SaveData LoadBytesFile(byte[] bytes) {

            int header = 0;

            Program.estado estado;
            Jugador jugador = new Jugador();
            Enemigo enemigo = new Enemigo();
            Enemigo enemigo1 = new Enemigo();

            estado = (Program.estado)Enum.ToObject(typeof(Program.estado), BitConverter.ToInt64(bytes, header));
            header += sizeof(int);

            jugador.y = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            jugador.x = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            enemigo.y = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            enemigo.x = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);
            if (estado == Program.estado.nivel2) {
                enemigo1.y = BitConverter.ToSingle(bytes, header);
                header += sizeof(float);

                enemigo1.x = BitConverter.ToSingle(bytes, header);
                header += sizeof(float);
            }
            SaveData saveData = new SaveData(estado, jugador, enemigo, enemigo1);

            return saveData;

        }

        public static SaveData LoadSave() {
            string filePath = Directory.GetCurrentDirectory() + "\\savefile.sav";
            if (File.Exists(filePath)) {
                byte[] bytes = File.ReadAllBytes(filePath);
                SaveData saveData = LoadBytesFile(bytes);
                return saveData;
            }
            return null;
        }

    }

    class SaveData {
        public Program.estado estado;
        public Jugador jugador;
        public Enemigo enemigo;
        public Enemigo enemigo1;

        public SaveData(Program.estado estado, Jugador jugador, Enemigo enemigo, Enemigo enemigo1) {
            this.estado = estado;
            this.jugador = jugador;
            this.enemigo = enemigo;
            this.enemigo1 = enemigo1;
        }

    }

}
