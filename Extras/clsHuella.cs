using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;

/*
--* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *--
----* Programador : Jesus Elias Vidrio Ibarra												  *----
------* Fecha : 10/01/2018																	  *------
----* Descripcion : Clase para Obtener datos de quien opera el sistema                        *----
--* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *--
*/
namespace System.Collections.Generic
{
    public class clsHuella
    {
        Process Huella;

        // Asegurar que el archivo se encuentre en la unidad del SO
        string sRutaHuella = Environment.SystemDirectory.ToString().Trim().Substring(0,1).ToString() + @":\SYS\PROGS\HE.EXE";
        public string sNomEmpleado = string.Empty;
        public long lNumEmpleado = 0;
        public int iNumGrupo = 0;
    	
        ClsMetodos Metodos;
        
		/// <summary>
        /// Constructor de clase para huellas
        /// </summary>
        public clsHuella()
        {
            //Conexion a Base de datos para obtener nombre y grupo de empleado
            Metodos = new ClsMetodos(TipoGestor.BODEGA);

        }

        /// <summary>
        /// funcion con la cual se pide la huella de empleado
        /// </summary>
        public bool SolicitarHuella()
        {
            bool bRegresa = false;
            int iVueltas = 1;
            try
            {
                if (BuscarHE())
                {
                    do
                    {
                        iVueltas++;

                        Huella = new Process();
                        Huella.StartInfo.FileName = sRutaHuella;
                        Huella.Start();
                        Huella.WaitForExit();
                        lNumEmpleado = Huella.ExitCode;

                        if (EsEmpleado())
                        {
                            if (ObtieneGrupoEmp())
                            {
                                bRegresa = ObtieneNombreEmp();
                            }
                        }

                    } while (iVueltas <= 3 && bRegresa == false);
                }
            }
            catch(Exception e)
            {
                Metodos.Mensaje(e.Message.ToString(), TipoMensaje.Informativo);
                if (!Metodos.GrabarError("BR050.exe", "clsHuella.cs", "SolicitarHuella()", " ", "1", e.Message.ToString()))
                {
                    Metodos.Mensaje("Error al grabar error.", TipoMensaje.Informativo);
                }
            }

            return bRegresa;
        }

        private bool BuscarHE()
        {
            bool bRegresa = File.Exists(sRutaHuella);

            if (!bRegresa)
            {
                MessageBox.Show(@"No se encontro el archivo '" + sRutaHuella + "' ", "Mensaje...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bRegresa;
        }

        private bool EsEmpleado()
        {
            bool bRegresa = false;

            if (lNumEmpleado > 9000000 && lNumEmpleado < 100000000L)
            {
                bRegresa = true;
            }
            return bRegresa;
        }

        private bool ObtieneGrupoEmp()
        {
            bool bRegresa = false;
            string sConsulta = string.Empty;
            DataTable Resultado = new DataTable();
            sConsulta = string.Format("SELECT numgrupo FROM maeempleados WHERE numempleado = {0}", lNumEmpleado);
            try
            {
                Resultado = Metodos.Consultar(sConsulta);
                if (Resultado.Rows.Count != 0)
                {
                    foreach (DataRow item in Resultado.Rows)
                    {
                        if (item["numgrupo"] != null)
                        {
                            iNumGrupo = Convert.ToInt16(item["numgrupo"]);
                            bRegresa = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Metodos.Mensaje(e.Message.ToString(), TipoMensaje.Informativo);
                if (!Metodos.GrabarError("BR050.exe", "clsHuella.cs", "ObtieneGrupoEmp()", sConsulta, "1", e.Message.ToString()))
                {
                    Metodos.Mensaje("Error al grabar error.", TipoMensaje.Informativo);
                }
            }

            return bRegresa;
        }

        private bool ObtieneNombreEmp()
        {
            bool bRegresa = false;
            string sConsulta = string.Empty;
            DataTable Resultado = new DataTable();
            sConsulta = string.Format("SELECT nomempleado FROM maeempleados WHERE numempleado = {0}", lNumEmpleado);
            try
            {
                Resultado = Metodos.Consultar(sConsulta);
                if (Resultado.Rows.Count != 0)
                {
                    foreach (DataRow item in Resultado.Rows)
                    {
                        if (item["nomempleado"] != null)
                        {
                            sNomEmpleado = item["nomempleado"].ToString();
                            if (sNomEmpleado.Length > 0)
                            {
                                bRegresa = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Metodos.Mensaje(e.Message.ToString(), TipoMensaje.Informativo);
                if (!Metodos.GrabarError("BR050.exe", "clsHuella.cs", "ObtieneNombreEmp()", sConsulta, "1", e.Message.ToString()))
                {
                    Metodos.Mensaje("Error al grabar error.", TipoMensaje.Informativo);
                }
            }

            return bRegresa;
        }

    }
}