using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Windows;

namespace abcCompleto
{
    class start
    {
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        public start()
        {
            CargarArchivoConfig();
        }

        private bool CargarArchivoConfig()
        {
            bool bRegresa = false;
            try
            {
                using (StreamReader reader = new StreamReader(@"abcDat.txt"))
                {
                    string[] line = reader.ReadToEnd().Split('\n');
                    Conexion.sIp = line[0].Trim();
                    Conexion.sDB = line[1].Trim();
                    Conexion.sUsuario = line[2].Trim();
                    Conexion.sContraseña = line[3].Trim();
                    bRegresa = true;
                }
            }
            catch
            {
                MessageBox.Show("Error en el archivo de Configuracion.");
            }
            return bRegresa;
        }
    }
}
