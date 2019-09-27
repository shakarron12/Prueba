using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

/*
--* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *--
----* Programador : Jesus Elias Vidrio Ibarra												  *----
------* Fecha : 28/09/2017																	  *------
----* Descripcion : Clase para Obtener Contraseñas de servidores Coppel                       *----
--* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *--
*/
namespace System.Collections.Generic
{    
    public enum TipoPassword
    {
        Compras = 1 , Bodega = 2, Protheus = 3, PostgreSQL = 4, SqlServer = 5
    }

    public class clsMD5
    {
        private bool bMayus = true;

        public clsMD5()
        {
        }

        public bool Mayusculas
        {
            set 
            {
                bMayus = value;
            }
        }

        #region Funciones y procedimientos publicos
        /// <summary>
        /// Constructor que genera Password para servidor.
        /// </summary>
        /// <param name="Usuario">Usuario del cual se obtendra la contraseña.</param>
        /// <param name="BaseDeDatos">Base de datos del cual se obtendra la contraseña.</param>
        /// <param name="Tipo"><para>tipo de conexion para generar la contraseña. </para>
        /// <para>1 = Usuario: sysprogsropa - Compras</para>
        /// <para>2 = Usuario: sysprogsropa - Bodega</para>
        /// <para>3 = Usuario: sysinterfaz  - HML_AR</para>
        /// <para>4 = PostgreSQL : generico - generico</para>
        /// <para>5 = SqlServer :  generico - generico</para></param>
        /// <returns>string</returns>
        public string GenerarPassword(string Usuario, string BaseDeDatos, TipoPassword Tipo )
        {
            string sCaracter = "";

            switch (Tipo)
            {
                case TipoPassword.Compras:
                    BaseDeDatos = "sisropa";
                    sCaracter = "-";
                    break;

                case TipoPassword.Bodega:
                    BaseDeDatos = "bodega";
                    sCaracter = "-";
                    break;

                case TipoPassword.Protheus:
                    BaseDeDatos = "protheus";
                    sCaracter = "-";
                    break;

                case TipoPassword.PostgreSQL:
                    sCaracter = "-";
                    break;

                case TipoPassword.SqlServer:
                    sCaracter = "";
                    break;
            }

            string sRegresa = GetMD5Cadena(Usuario + sCaracter + BaseDeDatos).ToLower();
            return sRegresa;
        }

        public string GetMD5(string sArchivo)
        {
            string sMD5 = "";

            try
            {
                FileStream bufferArchivo;
                byte[] bytesArchivo;
                MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

                bufferArchivo = File.Open(sArchivo, FileMode.Open, FileAccess.Read);
                bytesArchivo = MD5Crypto.ComputeHash(bufferArchivo);
                bufferArchivo.Close();

                sMD5 = BitConverter.ToString(bytesArchivo);
                sMD5 = sMD5.Replace("-", "");

                if ( ! bMayus )
                    sMD5 = sMD5.ToLower();
                else
                    sMD5 = sMD5.ToUpper();

            }
            catch
            {
            }
            return sMD5;
        }

        public string GetMD5Cadena(string Cadena)
        {
            string sMD5 = "";
            byte[] bytesCadena;
            byte[] byteResultado;

            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bytesCadena = System.Text.Encoding.UTF8.GetBytes(Cadena); ;
            //Convert.ToByte(Cadena);


            byteResultado = MD5Crypto.ComputeHash(bytesCadena);

            sMD5 = BitConverter.ToString(byteResultado);
            sMD5 = sMD5.Replace("-", "");

            if (!bMayus)
                sMD5 = sMD5.ToLower();
            else
                sMD5 = sMD5.ToUpper();

            return sMD5;
        }

        public string Encripta512(string prtKey)
        {
            string sRegresa = "";
            SHA512Managed myHash = new SHA512Managed();
            byte[] bytValue;
            byte[] bytHash;

            // Cadena clave para encriptar
            bytValue = System.Text.Encoding.UTF8.GetBytes(prtKey);
            bytHash = myHash.ComputeHash(bytValue);

            myHash.Clear();
            sRegresa = Convert.ToBase64String(bytHash);
            //sRegresa = Fg.Left(sRegresa, iLargoKey);

            return sRegresa;
        }
        #endregion

    }
}
