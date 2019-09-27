using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.Odbc;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO.Compression;

namespace System.Collections.Generic
{
    /*--------------------------------------------------------------------------------------------------|
      |             * Clase         : cMetodos.                                                           |
      |             * Funcionalidad : Simplifica el manejo de conexiones y la ejecucion de consultasa bd. | 
      |             * Autor         : Ing. Xicotencatl Lopez Baldenebro.                                  |
      |             * Fecha         : 2018-01-10.                                                         |
      ---------------------------------------------------------------------------------------------------*/

    #region Tipos Enum

    /// <summary>
    /// Se definen los tipos de mensaje que se pueden invocar.
    /// </summary>
    public enum TipoMensaje
    {
        Informativo = 0, Respuesta = 1, MultiResp = 3
    }
    /// <summary>
    /// Se definen los tipos de gestor para realizar conexion.
    /// </summary>
    public enum TipoGestor
    {
        COMPRAS = 1, BODEGA = 2, PROTHEUS = 3, POSTGRESGNL = 4, SQLSERVERGNL = 5
    }

    #endregion

    #region Estructuras

    /// <summary>
	/// Estructura de datos generales pasados como parametro para bodega.
	/// </summary>
    public struct Bodega
    {
        public static string sIp = string.Empty;
        public static string sUsuario = string.Empty;
        public static string sDB = string.Empty;
        public static string sContraseña = string.Empty;
        public static string sTerminal = string.Empty;
        public static string sSeccion = string.Empty;
        public static string sEmpleado = string.Empty;
    }
	
	/// <summary>
	/// Estructura de datos generales pasados como parametro para Compras.
	/// </summary>
    public struct Compras
    {
        public static string sIp = string.Empty;
        public static string sUsuario = string.Empty;
        public static string sDB = string.Empty;
        public static string sContraseña = string.Empty;
    }
	
	/// <summary>
	/// Estructura de datos generales pasados como parametro para Protheus.
	/// </summary>
    public struct Protheus
    {
        public static string sIp = string.Empty;
        public static string sUsuario = string.Empty;
        public static string sDB = string.Empty;
        public static string sPuerto = string.Empty;
        public static string sContraseña = string.Empty;
        public static string sFilial = string.Empty;
        public static string sEmpresa = string.Empty;
    }
	
	/// <summary>
	/// Estructura de datos generales pasados como parametro para General.
	/// </summary>
    public struct General
    {
        public static string sIp = string.Empty;
        public static string sUsuario = string.Empty;
        public static string sDB = string.Empty;
        public static string sContraseña = string.Empty;
    }
	
	#endregion

    public class ClsMetodos
    {
        #region Variables
        OdbcConnection oConexion;
        OdbcCommand command;
        OdbcTransaction transaction;
        public Label lblFondoD;
        private static Assembly aEnsamblado;
        private static Type[] tTipos;
        int iGestor;        
                
        private string sCadenaConexion, sPassword;
        /// <summary>
        /// Nombre del componente actual.
        /// </summary>
        public string sModuloActual { get { string sExe = System.IO.Path.GetFileName(Application.ExecutablePath); return sExe; } }

        #endregion

        #region ClsMetodos - Constructor

        /// <summary>
        /// Constructor para acceder a los metodos de ClsMetodos.
        /// </summary>
        public ClsMetodos()
        {

        }       

		/// <summary>
        /// Constructor que genera cadena de conexion.
        /// </summary>
        /// <param name="iGestor"><para>Manejador del servidor al que quieres conectarte. </para>
        /// <para>TipoGestor :</para>
        /// <para>COMPRAS</para>
        /// <para>BODEGA</para>
        /// <para>PROTHEUS</para>
        /// <para>POSTGRESQL GENERICO</para>
        /// <para>SQL SERVER GENERICO</para></param>
        /// <returns>sCadenaConexion</returns>
        public ClsMetodos( TipoGestor iGestor )
        {
            bool bEsWindows32 = true;
            clsMD5 MD5 = new clsMD5();
            this.iGestor = Convert.ToInt16(iGestor);
            string sMensajeError = string.Empty;
            
            try
            {
                sMensajeError = "No es posible realizar una conexión a base de datos debido a falta de datos, Favor de contactar con Mesa de Ayuda.";
                switch (iGestor)
                {
                    case TipoGestor.COMPRAS://COMPRAS
                        if (String.IsNullOrEmpty(Compras.sDB) || String.IsNullOrEmpty(Compras.sIp) || String.IsNullOrEmpty(Compras.sUsuario))
                        {
                            throw new Exception(sMensajeError);
                        }
                        sPassword = MD5.GenerarPassword(Compras.sUsuario.ToLower(), Compras.sDB.ToLower(), TipoPassword.Compras);
                        sCadenaConexion = "Driver={Sql Server};database=" + Compras.sDB + ";server=" + Compras.sIp + ";uid=" + Compras.sUsuario + ";pwd=" + sPassword + ";Connect Timeout=600";
                        break;

                    case TipoGestor.BODEGA://BEDEGA
                        if (String.IsNullOrEmpty(Bodega.sDB) || String.IsNullOrEmpty(Bodega.sIp) || String.IsNullOrEmpty(Bodega.sUsuario))
                        {
                            throw new Exception(sMensajeError);
                        }
                        sPassword = MD5.GenerarPassword(Bodega.sUsuario.ToLower(), Bodega.sDB.ToLower(), TipoPassword.Bodega);
                        bEsWindows32 = EsWindowXP();
                        Bodega.sContraseña = sPassword;
                        if (bEsWindows32)
                        {
                            sCadenaConexion = "Driver={PostgreSQL};database=" + Bodega.sDB + ";server=" + Bodega.sIp + ";uid=" + Bodega.sUsuario + ";pwd=" + sPassword + ";Connect Timeout=600";
                        }
                        else
                        {
                            sCadenaConexion = "Driver={PostgreSQL ANSI(x64)};database=" + Bodega.sDB + ";server=" + Bodega.sIp + ";uid=" + Bodega.sUsuario + ";pwd=" + sPassword + ";Connect Timeout=600";
                        }
                        break;

                    case TipoGestor.PROTHEUS://PROTHEUS
                        
                        if (String.IsNullOrEmpty(Protheus.sDB) || String.IsNullOrEmpty(Protheus.sIp))
                        { 
                            throw new Exception(sMensajeError);
                        }
                        Protheus.sUsuario = "sysinterfaz";
                        sPassword = MD5.GenerarPassword(Protheus.sUsuario.ToLower(), Protheus.sDB.ToLower(), TipoPassword.Protheus);
                        bEsWindows32 = EsWindowXP();
                        sCadenaConexion = "Driver={Sql Server};database=" + Protheus.sDB + ";server=" + Protheus.sIp + ";uid=" + Protheus.sUsuario + ";pwd=" + sPassword + ";Connect Timeout=600";
                        break;

                    case TipoGestor.POSTGRESGNL://POSTGRESQL GENERICO
                        if (String.IsNullOrEmpty(General.sDB) || String.IsNullOrEmpty(General.sIp) || String.IsNullOrEmpty(General.sUsuario))
                        {
                            throw new Exception(sMensajeError);
                        }
                        sPassword = MD5.GenerarPassword(General.sUsuario.ToLower(), General.sDB.ToLower(), TipoPassword.PostgreSQL);
                        bEsWindows32 = EsWindowXP();
                        if (bEsWindows32)
                        {
                            sCadenaConexion = "Driver={PostgreSQL};database=" + General.sDB + ";server=" + General.sIp + ";uid=" + General.sUsuario + ";pwd=" + sPassword + ";Connect Timeout=600";
                        }
                        else
                        {
                            sCadenaConexion = "Driver={PostgreSQL ANSI(x64)};database=" + General.sDB + ";server=" + General.sIp + ";uid=" + General.sUsuario + ";pwd=" + sPassword + ";Connect Timeout=600";
                        }
                        break;

                    case TipoGestor.SQLSERVERGNL://SQL SERVER GENERICO
                        if (String.IsNullOrEmpty(General.sDB) || String.IsNullOrEmpty(General.sIp) || String.IsNullOrEmpty(General.sUsuario))
                        {
                            throw new Exception(sMensajeError);
                        }
                        sPassword = MD5.GenerarPassword(General.sUsuario.ToLower(), General.sDB.ToLower(), TipoPassword.SqlServer);
                        sCadenaConexion = "Driver={Sql Server};database=" + General.sDB + ";server=" + General.sIp + ";uid=" + General.sUsuario + ";pwd=" + sPassword + ";Connect Timeout=600";
                        break;

                    default:
                        Mensaje("TipoGestor invalido.", TipoMensaje.Informativo);
                        break;
                }
            }
            catch(Exception e)
            {
                Mensaje(e.Message,TipoMensaje.Informativo);
            }
        }

        #endregion

        /// <summary>
        /// //Metodos de Conexion para el Gestor.
        /// </summary>
        #region Conexiones

        /// <summary>
        /// Abre la conexión al servidor.
        /// </summary>
        /// <returns>bool</returns>
        public bool AbrirConexion()
        {
            bool bRegresa = false;
            
            try
            {
                oConexion = new OdbcConnection(sCadenaConexion);//cadena conexion
                oConexion.Open();
                bRegresa = true;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, TipoMensaje.Informativo);
            }
            return bRegresa;
        }

        /// <summary>
        /// Cierra la conexión al servidor.
        /// </summary>
        /// <returns>void</returns>
        public void CerrarConexion(OdbcConnection Conexion)
        {
			try
			{
				if (Conexion.State == ConnectionState.Open) //Si la conexion esta cerrada no hace nada 
				{
					oConexion.Close();
				}
			}
			catch
			{
			}
        }

        /// <summary>
        /// Consulta una sentencia de base de datos esperando datos de retorno.
        /// </summary>
        /// <param name="sConsulta">Query de solo consulta que desea ejecutar.</param>
        /// <returns>DataTable</returns>
        public DataTable Consultar(string sConsulta)
        {
			DataTable dtTabla = new DataTable();

			if(AbrirConexion())
			{
				OdbcCommand comando = new OdbcCommand(sConsulta, oConexion);
				OdbcDataAdapter adap = new OdbcDataAdapter(comando);
				try
				{
					adap.Fill(dtTabla);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

				CerrarConexion(oConexion);
			}

            return dtTabla;
        }

        /// <summary>
        /// Ejecuta una sentencia en base de datos sin retorno de datos.
        /// </summary>
        /// <param name="sConsulta">Query que desea ejecutar | insert | update | delete.</param>
        /// <returns>bool</returns>
        public bool Ejecutar(string sConsulta)
        {
            bool bRegresa = false;

            if (AbrirConexion())
            {
                OdbcCommand comando = new OdbcCommand(sConsulta, oConexion);

                comando.CommandType = CommandType.Text;

                try
                {
                    comando.ExecuteNonQuery();
                    bRegresa = true;

                }
                catch (Exception error)
                {
                    Mensaje(error.Message, TipoMensaje.Informativo);
                    bRegresa = false;

                }
                finally
                {
                    CerrarConexion(oConexion);
                }
            }

            return bRegresa;
        }

        /// <summary>
        /// Abre Conexion a servidor y Ejecuta un BEGIN TRANSACTION.
        /// </summary>
        /// <returns>Bool</returns>
        public bool IniciarTransaccion()
        {
            bool bRegresa = false;
            try
            {
                oConexion = new OdbcConnection(sCadenaConexion);
                command = new OdbcCommand();
                transaction = null;

                // Se establece conexion
                command.Connection = oConexion;

                // Abre la conexcion y comienza el try
                oConexion.Open();

                // Comienzan las transaccion locales
                transaction = oConexion.BeginTransaction();

                // Se asignan ambas transacciones a objetos
                command.Connection = oConexion;
                command.Transaction = transaction;
                bRegresa = true;
            }
            catch (Exception e)
            {
                Mensaje(e.Message.ToString(),TipoMensaje.Informativo);
            }
            return bRegresa;
        }

        /// <summary>
        /// Ejcuta una sentencia de base de datos sin esperar datos de retorno(En transaccion).
        /// </summary>
        /// <param name="sConsulta">Sentencia que desea ejecutar.</param>
        /// <returns>Bool</returns>
        public bool ExecEnTransaccion(string sConsulta, ref bool bOK)
        {
            bool bRegresa = false;

            if (bOK)
            {
                try
                {
                    command.CommandText = sConsulta;
                    command.ExecuteNonQuery();
                    bRegresa = true;
                }
                catch (Exception ex)
                {
                    RollbackTransaccion(ref bOK);

                    Mensaje(ex.Message.ToString(), TipoMensaje.Informativo);

                    if (!GrabarError(GetCallerName(0), "clsBR050.cs", GetCallerName(1), sConsulta, "1", ex.Message.ToString()))
                    {
                        Mensaje("Error al grabar error.", TipoMensaje.Informativo);
                    }
                }
            }
            else
            {
                RollbackTransaccion(ref bOK);
            }

            return bRegresa;
        }

        private static string GetCallerName(int iOpcion)
        {
            string sRegresa = string.Empty;
            StackTrace trace = new StackTrace(StackTrace.METHODS_TO_SKIP + 2);
            StackFrame frame = trace.GetFrame(0);
            MethodBase caller = frame.GetMethod();

            if (caller == null)
            {
                throw new InvalidProgramException();
            }
            if (iOpcion == 1)
            {
                sRegresa = caller.Name;
            }
            else
            {
                sRegresa = caller.Module.ToString();
            }

            return sRegresa;

        }

        /// <summary>
        /// Ejcuta una sentencia de base de datos que devuelva datos (En transaccion).
        /// </summary>
        /// <param name="sConsulta">Sentencia que desea ejecutar.</param>
        /// <returns>DataTable</returns>
        public DataTable ConsultaEnTransaccion(string sConsulta, ref bool bOK)
        {
            DataTable dtTabla = new DataTable();
            if (bOK)
            {
                try
                {
                    command.CommandText = sConsulta;
                    OdbcDataAdapter adap = new OdbcDataAdapter(command);
                    adap.Fill(dtTabla);
                }
                catch (Exception e)
                {
                    Mensaje(e.Message.ToString(), TipoMensaje.Informativo);
                    RollbackTransaccion(ref bOK);
                }
            }
            else
            {
                RollbackTransaccion(ref bOK);
            }
            return dtTabla;
        }

        /// <summary>
        /// Realiza un ROLLBACK TRANSACTION y cierra conexión.
        /// </summary>
        /// <param name="sConsulta">Sentencia que desea ejecutar.</param>
        /// <returns>void</returns>
        public void RollbackTransaccion(ref bool bOK)
        {
            bOK = false;
            if (transaction != null)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {
                }
            }
            CerrarConexion(oConexion);
        }

        /// <summary>
        /// Realiza un COMMIT TRANSACTION y cierra conexión.
        /// </summary>
        /// <returns>Bool</returns>
        public bool CommitTransaccion(ref bool bOK)
        {
            bool bRegresa = false;
            if (transaction != null)
            {
                if (bOK)
                {
                    try
                    {
                        transaction.Commit();
                        bRegresa = true;
                        CerrarConexion(oConexion);
                    }
                    catch
                    {
                        RollbackTransaccion(ref bOK);
                        Mensaje("Es posible que no se hayan guardado los datos de la transacción, favor de revisar con Mesa de Ayuda.", TipoMensaje.Informativo);
                    }
                }
                else
                {
                    RollbackTransaccion(ref bOK);
                }
            }
            return bRegresa;
        }

        #endregion

        /// <summary>
        /// //Todos los Metodos Dinamicos.
        /// </summary>
        #region Metodos

        /// <summary>
        /// <para>Consulta la infraestructura del sistempa operativo.</para>
        /// <para>-TRUE Windows 32 bits</para>
        /// <para>-FALSE Windows 64 bits</para>
        /// </summary>
        /// <returns>bool</returns>
        bool EsWindowXP()
        {
            bool bRegresa = false;
            /* System.OperatingSystem osInfo = System.Environment.OSVersion;            
              if (osInfo.Version.Major < 6)
              {
                  bRegresa = true;
              }*/
            if (IntPtr.Size == 4)//Es Windows de 32
            {
                bRegresa = true;
            }
            else if (IntPtr.Size == 8)
            {
                bRegresa = false;
            }
            return bRegresa;
        }

        /// <summary>
        /// Limpia todos los controles que contengan la propiedad |TAG| igual a |1| de un dialogo.
        /// </summary>
        /// <param name="frmControles">Formulario al que se desea limpiar los controles.</param>
        /// <returns>void</returns>
        public void Limpiar(Form frmControles)
        {
            foreach (Control cnRecorrer in frmControles.Controls) // Se recorren todos los controles dentro del form
            {
                if (cnRecorrer.Tag != null)
                {
                    if (cnRecorrer.Tag.ToString() == "1" || cnRecorrer.Tag.ToString() == "1,2")
                    {
                        if (cnRecorrer is TextBox) // si un control es tipo textbox entra
                        {
                            cnRecorrer.ResetText(); // se limpian todos los campos tipo texto de form
                        }
                        else if (cnRecorrer is ComboBox) // entra cuando el tipo de control es combobox
                        {
                            if (((ComboBox)cnRecorrer).Items.Count > 0)
                            {
                                ((ComboBox)cnRecorrer).SelectedIndex = -1;// Selecciona el primer valor de cada combo
                                ((ComboBox)cnRecorrer).Items.Clear();
                            }
                        }
                        else if (cnRecorrer is CheckBox)
                        {
                            ((CheckBox)cnRecorrer).Checked = false;  // Des "Chequea" tdos los checkbox
                        }
                        else if (cnRecorrer is RichTextBox)
                        {
                            ((RichTextBox)cnRecorrer).Text = "";// limpia el texto
                        }

                        else if (cnRecorrer is MaskedTextBox)
                        {
                            ((MaskedTextBox)cnRecorrer).Text = ""; // limpia el texto
                        }
                        else if (cnRecorrer is MonthCalendar)
                        {
                            ((MonthCalendar)cnRecorrer).TodayDate = DateTime.Today; // asigna fecha actual falta probar
                        }
                        else if (cnRecorrer is Label)
                        {
                            ((Label)cnRecorrer).ResetText();
                        }
                        else if (cnRecorrer is DataGridView)
                        {
                            if (((DataGridView)cnRecorrer).Rows.Count > 0)
                            {
                                ((DataGridView)cnRecorrer).Rows.Clear();
                            }
                        }
                        else if (cnRecorrer is GroupBox)
                        {
                            LimpiarGroupBox(cnRecorrer);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Limpia todos los controles dentro de un GroupBox
        /// </summary>
        /// <param name="gbEntrada">GroupBox del cual se requieren limpiar todos los controles.</param>
        /// <returns>void</returns>
        private void LimpiarGroupBox(Control gbEntrada)
        {
            foreach (Control cnRecorrer in gbEntrada.Controls) // Se recorren todos los controles dentro del form
            {
                if (cnRecorrer.Tag != null)
                {
                    if (cnRecorrer.Tag.ToString() == "1" || cnRecorrer.Tag.ToString() == "1,2")
                    {
                        if (cnRecorrer is TextBox) // si un control es tipo textbox entra
                        {
                            cnRecorrer.ResetText(); // se limpian todos los campos tipo texto de form
                        }
                        else if (cnRecorrer is ComboBox) // entra cuando el tipo de control es combobox
                        {
                            if (((ComboBox)cnRecorrer).Items.Count > 0)
                            {
                                ((ComboBox)cnRecorrer).SelectedIndex = -1; // Selecciona el primer valor de cada combo
                                ((ComboBox)cnRecorrer).Items.Clear();
                            }
                        }
                        else if (cnRecorrer is CheckBox)
                        {
                            ((CheckBox)cnRecorrer).Checked = false;  // Des "Chequea" tdos los checkbox
                        }
                        else if (cnRecorrer is RichTextBox)
                        {
                            ((RichTextBox)cnRecorrer).Text = "";// limpia el texto
                        }
                        else if (cnRecorrer is MaskedTextBox)
                        {
                            ((MaskedTextBox)cnRecorrer).Text = ""; // limpia el texto
                        }
                        else if (cnRecorrer is MonthCalendar)
                        {
                            ((MonthCalendar)cnRecorrer).TodayDate = DateTime.Today; // asigna fecha actual falta probar
                        }
                        else if (cnRecorrer is Label)
                        {
                            ((Label)cnRecorrer).ResetText();
                        }
                        else if (cnRecorrer is DataGridView)
                        {
                            if (((DataGridView)cnRecorrer).Rows.Count > 0)
                            {
                                ((DataGridView)cnRecorrer).Rows.Clear();
                            }
                        }
                        else if (cnRecorrer is GroupBox)
                        {
                            LimpiarGroupBox(cnRecorrer);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Quita caracteres especiales de un string.
        /// </summary>
        /// <param name="sCadena">Cadena a la que se le quitaran los caracteres especiales.</param>
        /// <returns>string</returns>
        public string SinCaracteresEspeciales(string sCadena)
        {
            string sRegresa = string.Empty;
            sRegresa = Regex.Replace(sCadena, @"[!-/]|[:-@]|[[-` ]|[{- ]", "");
            return sRegresa;
        }

        /// <summary>
        /// Valida si todos los controles estan vacios en un formulario.
        /// </summary>
        /// <param name="Formulario">Formulario al cual se va acceder.</param>
        /// <returns>bool</returns>
        public bool ValidaVacios(Form Formulario)
        {
            bool bRegresa = true;
            foreach (Control item in Formulario.Controls)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString() == "1")
                    {
                        if (item is TextBox)
                        {
                            if (item.Text.Length > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is MaskedTextBox)
                        {
                            if (SinCaracteresEspeciales(item.Text).Length > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is DataGridView)
                        {
                            if (((DataGridView)item).RowCount > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is ComboBox) // entra cuando el tipo de control es combobox
                        {
                            if (((ComboBox)item).Items.Count > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is CheckBox)
                        {
                            if (((CheckBox)item).Checked == true)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is RichTextBox)
                        {
                            if (((RichTextBox)item).Text.Length > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is Label)
                        {
                            if (((Label)item).Text.Length > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is GroupBox)
                        {
                            if (!ValidaVaciosGroupB(item))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }
            return bRegresa;
        }

        /// <summary>
        /// Valida si todos los controles estan vacios en un groupbox del formulario.
        /// </summary>
        /// <param name="GroupBox">Control Group box al cual se va acceder</param>
        /// <returns>bool</returns>
        public bool ValidaVaciosGroupB(Control GroupBox)
        {
            bool bRegresa = true;
            foreach (Control item in GroupBox.Controls)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString() == "1")
                    {
                        if (item is TextBox)
                        {
                            if (item.Text.Length > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is MaskedTextBox)
                        {
                            if (SinCaracteresEspeciales(item.Text).Length > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is DataGridView)
                        {
                            if (((DataGridView)item).RowCount > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is ComboBox) // entra cuando el tipo de control es combobox
                        {
                            if (((ComboBox)item).Items.Count > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is CheckBox)
                        {
                            if (((CheckBox)item).Checked == true)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is RichTextBox)
                        {
                            if (((RichTextBox)item).Text.Length > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is Label)
                        {
                            if (((Label)item).Text.Length > 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                        else if (item is GroupBox)
                        {
                            if (!ValidaVaciosGroupB(item))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }
            return bRegresa;
        }

        /// <summary>
        /// Levanta un DLL.
        /// </summary>
        /// <param name="RutaDll">Directorio donde se encuentra el modulo a cargar</param>
        /// <param name="NombreDll">Nombre del modulo que se cargara</param>
        /// <param name="ObjetoInicio">Nombre del Form|Objeto a cargar</param>
        /// <param name="ObjetoCargado">Instancia del objeto a cargar</param>
        /// <returns>bool</returns>
        public bool CargarDll(string sRutaDll, string sNombreDll, string ObjetoInicio, ref object ObjetoCargado) //Metodo para cargar dll dinamicamente sin parametros
        {
            bool bRegresa = false;
            object objRegresar = null;


            string sRuta = sRutaDll + @"\" + sNombreDll;

            try
            {
                if (File.Exists(sRuta))
                {
                    aEnsamblado = Assembly.LoadFile(sRuta);
                    tTipos = aEnsamblado.GetTypes();

                    foreach (Type myObjeto in tTipos)
                    {
                        if (myObjeto.Name.ToUpper() == ObjetoInicio.ToUpper())
                        {
                            objRegresar = Activator.CreateInstance(myObjeto);
                            ObjetoCargado = objRegresar;
                            bRegresa = true;
                            break;
                        }
                    }

                    if (objRegresar == null)
                        Mensaje("No se encontro la opción solicitada, reportarlo a Sistemas programación", TipoMensaje.Informativo);
                }
                else
                {
                    Mensaje("No se pudo cargar el modulo " + sNombreDll + ", reportarlo a Sistemas programación", TipoMensaje.Informativo);
                }
            }
            catch
            {
            }

            return bRegresa;
        }

        /// <summary>
        /// Levanta un DLL.
        /// </summary>
        /// <param name="RutaDll">Directorio donde se encuentra el modulo a cargar</param>
        /// <param name="NombreDll">Nombre del modulo que se cargara</param>
        /// <param name="ObjetoInicio">Nombre del Form|Objeto a cargar</param>
        /// <param name="ObjetoCargado">Instancia del objeto a cargar</param>
        /// <param name="parametros">Objeto con parametros que se mandaran al DLL</param>
        /// <returns>bool</returns>
        public bool CargarDll(string sRutaDll, string sNombreDll, string sObjetoInicio, ref object sObjetoCargado, object[] oParametros) // Metodo sobrecargado para cargar dll dinamicamente con parametros
        {
            bool bRegresa = false;
            object objRegresa = null;
            
           // sRutaDll = Directory.GetCurrentDirectory();
            string sRuta = sRutaDll + @"\" + sNombreDll;
           
            try
            {
                if (File.Exists(sRuta))
                {
                    aEnsamblado = Assembly.LoadFile(sRuta);
                    tTipos = aEnsamblado.GetTypes();

                    foreach (Type myObjeto in tTipos)
                    {
                        if (myObjeto.Name.ToUpper() == sObjetoInicio.ToUpper())
                        {
                            objRegresa = Activator.CreateInstance(myObjeto, oParametros);
                            sObjetoCargado = objRegresa;
                            bRegresa = true;
                            break;
                        }
                    }

                    if (objRegresa == null)
                        Mensaje("No se encontro la opción solicitada, reportarlo a Sistemas programación", TipoMensaje.Informativo);
                }
                else
                {
                    Mensaje("No se pudo cargar el modulo " + sNombreDll + ", reportarlo a Sistemas programación", TipoMensaje.Informativo);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRegresa;
        }

        /// <summary>
        /// Regresar un Control
        /// </summary>
        /// <param name="frmInicio">Nombre del formulario al cual se desea acceder.</param>
        /// <param name="sNombre">Nombre del control que se esta llamando.</param>
        /// <returns>Control</returns>
        public Control RegresarControl(Form frmInicio, string sNombre) // Metodo para buscar un control de un formulario
        {
            Control cControl = new Control();
            object dato = frmInicio.Controls.Find(sNombre, true).GetValue(0);
            cControl = (Control)dato;
            return cControl;
        }

        /// <summary>
        /// Crea un control 8).
        /// </summary>
        /// <param name="sNombre">Nombre del control que se desea crear.</param>
        /// <returns>Control</returns>
        public Control CrearControl(string sNombre) //Metodo para crear un control del tipo label
        {
            Control cControl;
            cControl = new System.Windows.Forms.Label();
            cControl.Name = sNombre;
            cControl.Visible = true;
            return cControl;
        }

        /// <summary>
        /// Muestra mensaje preguntando si se desea salir de la ventana actual.
        /// </summary>
        /// <param name="frmEntrada">Nombre del formulario que se desea cerrar.</param>
        /// <returns>void</returns>
        public void Salir(Form frmEntrada) //Metodo salir de formulario
        {
            if (Mensaje("¿Desea Salir?", TipoMensaje.Respuesta) == 1)
            {
                frmEntrada.Close();
            }
        }

        /// <summary>
        /// Guarda cualquier error ocurrido en el sistema
        /// </summary>
        /// <param name="dll">Nombre de dll donde ocurrio el error</param>
        /// <param name="formulario">Nombre de la pantalla donde ocurrio el error</param>
        /// <param name="funcion">Nombre del metodo donde ocurrio el error</param>
        /// <param name="consulta">Consulta que causo el error</param>
        /// <param name="ip">Ip de servidor donde ocurrio el error</param>
        /// <param name="sqlnumerror">Numero del error</param>
        /// <param name="sqltxterror">Mensaje de excepción ocurrida</param>
        /// <returns>void</returns>
        public void mensajeError(string dll, string formulario, string funcion, string consulta, string ip, int sqlnumerror, string sqltxterror)
        {
            try
            {                
                AbrirConexion();
                if (iGestor == 1 && Bodega.sDB == "ropa")
                {
                    try
                    {
                        AbrirConexion();
                        consulta = consulta.Replace("'", "");
                        sqltxterror = sqltxterror.Replace("'", "");
                        //Se descometara despues de que podamos instalar el procedimiento para grabar error.
                        Ejecutar("exec proc_GrabarError '" + dll + "', '" + formulario + "', '" + funcion + "', '" + consulta + "', '" + ip + "', '" + sqlnumerror + "', '" + sqltxterror + "', '" + ObtenerIp() + "'");
                    }
                    catch
                    {
                        Mensaje("Error al grabar mensaje de error.", TipoMensaje.Informativo);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Guarda cualquier error ocurrido en el sistema
        /// </summary>
        /// <param name="dll">Nombre de dll donde ocurrio el error</param>
        /// <param name="formulario">Nombre de la pantalla donde ocurrio el error</param>
        /// <param name="funcion">Nombre del metodo donde ocurrio el error</param>
        /// <param name="consulta">Consulta que causo el error</param>
        /// <param name="ip">Ip de servidor donde ocurrio el error</param>
        /// <param name="sqlnumerror">Numero del error</param>
        /// <param name="sqltxterror">Mensaje de excepción ocurrida</param>
        /// <returns>void</returns>
        public bool GrabarError(string sComponente, string sClase, string sMetodo, string sConsulta, string sNumError, string sErrorSql)
        {
            bool bRegresa = false;
            try
            {
                AbrirConexion();
                if (iGestor == 2 && Bodega.sDB == "bodega")
                {
                    try
                    {
                        AbrirConexion();
                        sConsulta = sConsulta.Replace("'", "''");
                        sErrorSql = sErrorSql.Replace("'", "");
                        Ejecutar("INSERT INTO ctlerrores " +
                                        "(dll, clase, funcion, consulta, ip, fechasistema, sqlnumerror, sqltxterror) VALUES " +
                                        "('" + sComponente + "','" + sClase + "','" + sMetodo + "','" + sConsulta + "','" + ObtenerIp() + "'," +
                                        " current_date, '" + sNumError + "','" + sErrorSql + "')");
                        bRegresa = true;
                    }
                    catch
                    {
                        bRegresa = false;
                    }
                }
            }
            catch { }
            return bRegresa;
        }

        /// <summary>
        /// Obtiene el ip de la micro en cuestion
        /// </summary>
        /// <returns>string</returns>
        public string ObtenerIp()
        {
            IPHostEntry host;
            string sLocalIP = string.Empty;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    sLocalIP = ip.ToString();
                }
            }

            return sLocalIP;
        }


        /// <summary>
        /// Busca si en un String se encuentra Otro String.
        /// </summary>
        /// <param name="sCadena">Texto donde se quiere buscar informacion.</param>
        /// <param name="sSeBusca">Dato que se desea buscar</param>
        /// <returns>bool</returns>
        public bool BuscarEnString(string sCadena, string sSeBusca)
        {
            bool bRegresa = false;

            bRegresa = sCadena.Contains(sSeBusca);

            return bRegresa;
        }

        /// <summary>
        /// Despliega un dialogo dinamico.
        /// </summary>
        /// <param name="Mensaje">Mensaje que desea mostrar al usuario.</param>
        /// <param name="Tipo"><para>Tipo de mensaje que desea utilizar.</para>
        /// <para>TipoMensaje : Mensaje : Returns</para>
        /// <para>Informativo : F5 Continuar : 0</para>
        /// <para>Respuesta : F1 Aceptar F2 Cancelar : 1|2</para>
        /// <para>MultiResp : Usar sobrecarga de tres parametros para mandar las opciones a mostrar : 1|2|3</para></param>
        /// <returns>int</returns>
        public int Mensaje(string Mensaje, TipoMensaje Tipo)
        {
            Form MensajeInfo = new Form();
            Label lblMensaje, lblRespuesta;
            int iOpcion = 0, iTamañoMensaje = 0, iLongMensaje = 0;

            iLongMensaje = Mensaje.Length;
            if (iLongMensaje > 270)
            {
                iTamañoMensaje = 8;
            }
            else if (iLongMensaje > 180)
            {
                iTamañoMensaje = 9;
            }
            else
            {
                iTamañoMensaje = 11;
            }
            //Se establecen las propiedades con las que contara el formulario del reporte
            MensajeInfo.Text = "Mensaje Coppel";
            MensajeInfo.Size = new Size(400, 150);
            MensajeInfo.StartPosition = FormStartPosition.CenterScreen;
            MensajeInfo.MinimizeBox = false;
            MensajeInfo.MaximizeBox = false;
            MensajeInfo.ShowIcon = false;
            MensajeInfo.FormBorderStyle = FormBorderStyle.FixedSingle;
            MensajeInfo.KeyPreview = true;
            //Dispara evento para capturar teclas
            MensajeInfo.KeyDown += (Sender, e) => OnMensajekeyDown(Sender, e, ref iOpcion, Tipo, MensajeInfo);
            MensajeInfo.FormBorderStyle = FormBorderStyle.FixedDialog;
            MensajeInfo.ControlBox = false;

            //Se instancian Labels para formulario
            lblRespuesta = new Label();
            lblMensaje = new Label();

            //Se agregan Labels al formulario virtual
            MensajeInfo.Controls.Add(lblMensaje);
            MensajeInfo.Controls.Add(lblRespuesta);

            //Se establecen las propiedades con las que contara el Label que contiene el mensaje
            lblMensaje.Name = "lblMensaje";
            lblMensaje.AutoSize = false;
            lblMensaje.Location = new System.Drawing.Point(1, 5);
            lblMensaje.Size = new System.Drawing.Size(MensajeInfo.Width - 20, MensajeInfo.Height - 50);
            lblMensaje.Text = Mensaje;
            lblMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            lblMensaje.Font = new Font("Microsoft Sans Serif", iTamañoMensaje, FontStyle.Bold);

            //Dispara evento para copiar mensaje mostrado con 1 click
            lblMensaje.MouseClick += (sender, e) => CopyOnClick(sender, e, Mensaje);
            lblMensaje.BackColor = Color.Transparent;

            //Se establecen las propiedades con las que contara el Label que contiene el mensaje de respuesta
            lblRespuesta.AutoSize = false;
            lblRespuesta.Location = new System.Drawing.Point(1, 5);
            lblRespuesta.Size = new System.Drawing.Size(MensajeInfo.Width - 20, MensajeInfo.Height - 55);
            lblRespuesta.Text = Respuestas(Tipo, "");
            lblRespuesta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            lblRespuesta.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            lblMensaje.BackColor = Color.Transparent;
            lblMensaje.Parent = lblRespuesta;

            //Se muestra Formulario
            MensajeInfo.ShowDialog();

            return iOpcion;
        }

        /// <summary>
        /// Despliega un dialogo de mensaje dinamico.
        /// </summary>
        /// <param name="Mensaje">Mensaje que desea mostrar al usuario.</param>
        /// <param name="Tipo"><para>Tipo de mensaje que desea utilizar.</para>
        /// <para>MultiResp : Mensaje Dinamico : 1|2|3</para></param>
        /// <param name="sMultiRespuesta">Opciones de teclas que desea mostrar al usuario.</param>
        /// <returns>int</returns>
        public int Mensaje(string Mensaje, TipoMensaje Tipo, string sMultiRespuesta)
        {
            Form MensajeInfo = new Form();
            Label lblMensaje, lblRespuesta;
            int iOpcion = 0, iTamañoMensaje = 0, iLongMensaje = 0;

            iLongMensaje = Mensaje.Length;
            if (iLongMensaje > 270)
            {
                iTamañoMensaje = 8;
            }
            else if (iLongMensaje > 180)
            {
                iTamañoMensaje = 9;
            }
            else
            {
                iTamañoMensaje = 11;
            }
            //Se establecen las propiedades con las que contara el formulario del reporte
            MensajeInfo.Text = "Mensaje Coppel";
            MensajeInfo.Size = new Size(400, 150);
            MensajeInfo.StartPosition = FormStartPosition.CenterScreen;
            MensajeInfo.MinimizeBox = false;
            MensajeInfo.MaximizeBox = false;
            MensajeInfo.ShowIcon = false;
            MensajeInfo.FormBorderStyle = FormBorderStyle.FixedSingle;
            MensajeInfo.KeyPreview = true;
            //Dispara evento para capturar teclas
            MensajeInfo.KeyDown += (Sender, e) => OnMensajekeyDown(Sender, e, ref iOpcion, Tipo, MensajeInfo);
            MensajeInfo.FormBorderStyle = FormBorderStyle.FixedDialog;
            MensajeInfo.ControlBox = false;

            //Se instancian Labels para formulario
            lblRespuesta = new Label();
            lblMensaje = new Label();

            //Se agregan Labels al formulario virtual
            MensajeInfo.Controls.Add(lblMensaje);
            MensajeInfo.Controls.Add(lblRespuesta);

            //Se establecen las propiedades con las que contara el Label que contiene el mensaje
            lblMensaje.Name = "lblMensaje";
            lblMensaje.AutoSize = false;
            lblMensaje.Location = new System.Drawing.Point(1, 5);
            lblMensaje.Size = new System.Drawing.Size(MensajeInfo.Width - 20, MensajeInfo.Height - 50);
            lblMensaje.Text = Mensaje;
            lblMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            lblMensaje.Font = new Font("Microsoft Sans Serif", iTamañoMensaje, FontStyle.Bold);

            //Dispara evento para copiar mensaje mostrado con 1 click
            lblMensaje.MouseClick += (sender, e) => CopyOnClick(sender, e, Mensaje);
            lblMensaje.BackColor = Color.Transparent;

            //Se establecen las propiedades con las que contara el Label que contiene el mensaje de respuesta
            lblRespuesta.AutoSize = false;
            lblRespuesta.Location = new System.Drawing.Point(1, 5);
            lblRespuesta.Size = new System.Drawing.Size(MensajeInfo.Width - 20, MensajeInfo.Height - 55);
            lblRespuesta.Text = Respuestas(Tipo, sMultiRespuesta);
            lblRespuesta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            lblRespuesta.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            lblMensaje.BackColor = Color.Transparent;
            lblMensaje.Parent = lblRespuesta;

            //Se muestra Formulario
            MensajeInfo.ShowDialog();

            return iOpcion;
        }

        /// <summary>
        /// Devuelve opciones para mostrar como respuestas disponibles.
        /// </summary>
        /// <param name="Tipo">Tipo de mensaje para mostrar respuestas disponibles</param>
        /// <returns>string</returns>
        private string Respuestas(TipoMensaje Tipo, string sMostrar)
        {
            string sRespuesta = string.Empty;
            switch (Tipo)
            {
                case TipoMensaje.Informativo:
                    sRespuesta = "<F5> Continuar";
                    break;
                case TipoMensaje.Respuesta:
                    sRespuesta = "<F1> Aceptar  <F2> Cancelar";
                    break;
                case TipoMensaje.MultiResp:
                    //Respuesta dinamica Modificar segun la necesidad
                    sRespuesta = sMostrar;
                    break;
                default:
                    sRespuesta = "<F5> Continuar";
                    break;
            }

            return sRespuesta;
        }

        /// <summary>
        /// Evento que detecta las teclas precionadas en un formulario virtual
        /// </summary>
        /// <param name="iOpcion">Parametro para devolver la opcion elegida por el usuario</param>
        /// <param name="Tipo">Tipo de mensaje para mostrar respuestas disponibles</param>
        /// <param name="MensajeInfo">Formulario para cerrar una vez elegida la respuesta</param>
        /// <returns>void</returns>
        private void OnMensajekeyDown(object sender, KeyEventArgs e, ref int iOpcion, TipoMensaje Tipo, Form MensajeInfo)
        {
            switch (Tipo)
            {
                case TipoMensaje.Informativo:
                    //Si se preciona F5 Cierra la ventana
                    if (e.KeyCode == Keys.F5)
                    {
                        iOpcion = 0;
                        MensajeInfo.Close();
                    }
                    break;
                case TipoMensaje.Respuesta:
                    //Si se preciona F1 Aceptar F2 Cancelar
                    if (e.KeyCode == Keys.F1)
                    {
                        iOpcion = 1;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F2)
                    {
                        iOpcion = 2;
                        MensajeInfo.Close();
                    }
                    break;
                case TipoMensaje.MultiResp:
                    //Respuesta Dinamica que maneja 3 posibles, en Caso de necesitar más, agregarlas Aqui y En metodo Respuestas
                    if (e.KeyCode == Keys.F1)
                    {
                        iOpcion = 1;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F2)
                    {
                        iOpcion = 2;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F3)
                    {
                        iOpcion = 3;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F4)
                    {
                        iOpcion = 4;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F5)
                    {
                        iOpcion = 5;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F6)
                    {
                        iOpcion = 6;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F7)
                    {
                        iOpcion = 7;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F8)
                    {
                        iOpcion = 8;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F9)
                    {
                        iOpcion = 9;
                        MensajeInfo.Close();
                    }
                    if (e.KeyCode == Keys.F10)
                    {
                        iOpcion = 10;
                        MensajeInfo.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Evento que detecta clicks para copiar informacion de formulario virtual Mensaje
        /// </summary>
        /// <returns>void</returns>
        private void CopyOnClick(object sender, MouseEventArgs e, string Mensaje)
        {
            if (e.Clicks == 1)
            {
                try
                {
                    Clipboard.SetText(Mensaje);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Método para convertir un archivo a string
        /// </summary>
        /// <param name="sRutaArchivo">String que corresponde a la ruta del archivo.</param>
        /// <returns>string</returns>
        public string FileToString(string sRutaArchivo)
        {
            //verificamos que la longitud del string sea mayor que cero
            if (sRutaArchivo.Length > 0)
            {
                string sStringArchivo = "", sNombreArhivo = Path.GetFileName(sRutaArchivo);
                try
                {
                    FileStream fsArchivo = new FileStream(sRutaArchivo, FileMode.Open, FileAccess.Read);
                    //Validamos que el archivo pese menos de 10 mb de lo contrario seria muy pesado para manejarlo de manera de string
                    if (fsArchivo.Length <= 10485760)
                    {
                        byte[] buffer = new byte[fsArchivo.Length];
                        fsArchivo.Read(buffer, 0, buffer.Length);
                        // Convertimos el arreglo de bytes a un string en Base64 comprimiendolo
                        //para que el string no quede pesado y agregamos el nombre con su delimitador para que mantenga el nombre
                        sStringArchivo = Path.GetFileName(fsArchivo.Name) + "|" + Convert.ToBase64String(Zip(Encoding.Default.GetString(buffer)));
                    }
                    else
                    {
                        Mensaje("No se puéden convertir archivos mayores a 10 MB.\nPor favor verifique.", TipoMensaje.Informativo);
                    }
                    fsArchivo.Close();
                }
                catch (Exception ex)
                {
                    //en caso de exepcion regresamos el string limpio
                    Mensaje("Error al convertir el archivo:\n" + ex.Message, TipoMensaje.Informativo);
                }
                return sStringArchivo;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Método para convertir un archivo a string
        /// </summary>
        /// <param name="sRutaArchivo">String que corresponde a la ruta del archivo.</param>
        /// <param name="lTamanoArchivo">Variable numerica para recibir el tamaño del archivo.</param>
        /// <returns>string</returns>
        public string FileToString(string sRutaArchivo, ref long lTamanoArchivo)
        {
            //verificamos que la longitud del string sea mayor que cero
            if (sRutaArchivo.Length > 0)
            {
                string sStringArchivo = "", sNombreArhivo = Path.GetFileName(sRutaArchivo);
                try
                {
                    FileStream fsArchivo = new FileStream(sRutaArchivo, FileMode.Open, FileAccess.Read);
                    //Validamos que el archivo pese menos de 10 mb de lo contrario seria muy pesado para manejarlo de manera de string
                    if (fsArchivo.Length <= 10485760)
                    {
                        lTamanoArchivo = fsArchivo.Length;
                        byte[] buffer = new byte[fsArchivo.Length];
                        fsArchivo.Read(buffer, 0, buffer.Length);
                        // Convertimos el arreglo de bytes a un string en Base64 comprimiendolo
                        //para que el string no quede pesado y agregamos el nombre con su delimitador para que mantenga el nombre
                        sStringArchivo = Path.GetFileName(fsArchivo.Name) + "|" + Convert.ToBase64String(Zip(Encoding.Default.GetString(buffer)));
                    }
                    else
                    {
                        Mensaje("No se puéden convertir archivos mayores a 10 MB.\nPor favor verifique.", TipoMensaje.Informativo);
                    }
                    fsArchivo.Close();
                }
                catch (Exception ex)
                {
                    //en caso de exepcion regresamos el string limpio
                    Mensaje("Error al convertir el archivo:\n" + ex.Message, TipoMensaje.Informativo);
                }
                return sStringArchivo;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Método para convertir string a archivo
        /// </summary>
        /// <param name="sRutaGuardar">String que corresponde a la ruta donde se guardará el archivo temporalmente.(no requiere
        /// el nombre del archivo)</param>
        /// <param name="sArchivo">String que corresponde al string que contiene el archivo</param>
        /// <param name="bValidaNombre">Si es verdadero validará el nombre del archivo en el string generado, caso 
        /// contrario solo colocará la ruta a guardar en la cual será requerido el nombre</param>
        /// <returns>La ruta del archivo</returns>
        public string StringToFile(string sRutaGuardar, string sArchivo, bool bValidaNombre)
        {
            string sStringArchivo = "", sNombreArhivo = "";
            try
            {
                // verificamos que la longitud del string que contiene el archivo sea mayor a cero
                if (sArchivo.Length > 0)
                {
                    // verificamos si consultará el nombre desde el archivo o vendra con la ruta
                    // cuando se valida el nombre del archivo, el archivo se guarda en una ruta que se envie como temporal.
                    // cuando no se tiene que colocar la ruta completa, esto cuando es con un save file dialog
                    if (bValidaNombre)
                        sNombreArhivo = sRutaGuardar + sArchivo.Split('|')[0];
                    else
                        sNombreArhivo = sRutaGuardar;

                    FileStream fsArchivo = new FileStream(sNombreArhivo, FileMode.Create, FileAccess.Write);
                    byte[] buffer = Encoding.Default.GetBytes(UnZip(Convert.FromBase64String(sArchivo.Split('|')[1])));
                    fsArchivo.Write(buffer, 0, buffer.Length);
                    fsArchivo.Close();
                    sStringArchivo = sNombreArhivo;
                }
            }
            catch (Exception ex)
            {
                //en caso de exepcion regresamos el string limpio
                Mensaje("Error al convertir el archivo:\n" + ex.Message, TipoMensaje.Informativo);
            }
            return sStringArchivo;
        }

        /// <summary>
        /// Método para comprimir un string a bytes.
        /// </summary>
        /// <param name="str">String que se comprimira.</param>
        /// <returns>byte[]</returns>
        private static byte[] Zip(string str)
        {
            //obtenemos los bytes en utf8 para que se pueda comprimir correctamente
            var bytes = Encoding.UTF8.GetBytes(str);

            //Utilizamos un memorystream con los bytes y uno nuevo
            using (var msInicial = new MemoryStream(bytes))
            using (var msFinal = new MemoryStream())
            {
                //creamos la un objeto de tipo GZipStream para realizar la compresión el cual insertará
                //el buffer en el msFinal
                using (var gs = new GZipStream(msFinal, CompressionMode.Compress))
                {
                    //copiamos el msInicial al GZipStream para comprimirlo
                    CopyTo(msInicial, gs);
                }
                //regresamos el resultado del msFinal
                return msFinal.ToArray();
            }
        }

        /// <summary>
        /// Método para descomprimir una imagen en bytes.
        /// </summary>
        /// <param name="bytes">String que se descomprimira a string.</param>
        /// <returns>string</returns>
        private static string UnZip(byte[] bytes)
        {
            //Utilizamos un memorystream con los bytes y uno nuevo
            using (var msInicial = new MemoryStream(bytes))
            using (var msFinal = new MemoryStream())
            {
                //creamos la un objeto de tipo GZipStream para realizar la Descompresión el cual obtendrá
                //el buffer en el msInicial
                using (var gs = new GZipStream(msInicial, CompressionMode.Decompress))
                {
                    //copiamos el GZipStream al msFinal para recibir el buffer descomprimido
                    CopyTo(gs, msFinal);
                }
                //regresamos el resultado opteniendo el string en UTF8
                return Encoding.UTF8.GetString(msFinal.ToArray());
            }
        }

        /// <summary>
        /// Método para copiar archivo de una ruta a otra.
        /// </summary>
        /// <param name="streamOrigen">Ruta origen de archivo que se desea copiar.</param>
        /// <param name="stramDestino">Ruta destino a donde desea copiar el archivo.</param>
        /// <returns>void</returns>
        public static void CopyTo(Stream streamOrigen, Stream stramDestino)
        {
            byte[] bytes = new byte[4096];

            int cnt;
            //leemos la memoria del stream origen
            while ((cnt = streamOrigen.Read(bytes, 0, bytes.Length)) != 0)
            {
                //copiamos la memoria al stram destino
                stramDestino.Write(bytes, 0, cnt);
            }
        }

        /// <summary>
        /// Obtiene fecha del sistema.
        /// </summary>
        /// <param name="sFormatoFecha">Formato en el que se espera recibir la fecha.</param>
        /// <returns>string</returns>
        public string sFechaSistema(string sFormatoFecha)
        {
            string sFechaSis = string.Empty;
            string sConsulta = string.Empty;
            DataTable Resultado = new DataTable();

            try
            {
                AbrirConexion();
                if (oConexion.State == ConnectionState.Open) //Si la conexion esta cerrada no hace nada 
                {
                    //Si el gestor es de postgreSQL se podra ejecutar
                    if (iGestor == 2 || iGestor == 4)
                    {
                        sConsulta = string.Format("SELECT to_char(current_timestamp, '{0}') AS fecha", sFormatoFecha);

                        Resultado = Consultar(sConsulta);
                        if (Resultado.Rows.Count != 0)
                        {
                            foreach (DataRow item in Resultado.Rows)
                            {
                                sFechaSis = item["fecha"].ToString();
                                break;
                            }
                        }
                    }
                }               
            }
            catch (Exception e)
            {
                Mensaje(e.Message.ToString(), TipoMensaje.Informativo);
                //Si la base de datos es de bodega se podra ejecutar
                if (iGestor == 2)
                {
                    if (!GrabarError( sModuloActual, "ClsMetodos.cs", "sFechaSistema()", sConsulta, "1", e.Message.ToString()))
                    {
                        Mensaje("Error al grabar error.", TipoMensaje.Informativo);

                    }
                }
            }
            return sFechaSis;
        }

        /// <summary>
        /// Valida que la fecha introducida sea una fecha valida.
        /// </summary>
        /// <param name="sFecha">Fecha a validar.</param>
        /// <returns>bool</returns>
        public bool ValidaFecha(string sFecha)
        {
            bool bFehcaCorrecta = false;
            DateTime convertedDate;

            try
            {
                convertedDate = Convert.ToDateTime(sFecha);                
                bFehcaCorrecta = true;
            }
            catch (FormatException)
            {
                Mensaje("Fecha incorrecta :" + sFecha, TipoMensaje.Informativo);

            }
            return bFehcaCorrecta;
        }

        #endregion Metodos
    }
}
