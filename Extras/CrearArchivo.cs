using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;

namespace BR050POLIZAMOVTOSVARIOS
{
    class CrearArchivo
    {
        ClsMetodos cmetsinCnx = new ClsMetodos();
        StringBuilder buildText = new StringBuilder();
        StringBuilder ItemsTabla = new StringBuilder();
        StreamReader Lectura;

        public string sMensajeInfo = string.Empty, sImagen = string.Empty;
        string Direc = @"c:\sys\mem\movvarios\";

        public string salta(Int16 CantSaltos)
        {
            string aux = string.Empty;

            for (int i = 0; i < CantSaltos; i++)
            {
                aux += "\r";
            }
            hoja(aux, 0);
            return aux;
        }

        /// <summary> CaracterSeparador
        /// Agrega caracteres que separan las lineas de texto.
        /// </summary>
        /// <param name="caracter">Simbolo que se va a agregar entre cadenas de texto. ej: -, *</param>
        /// <param name="TotalCaracteres">Cantidad de simbolos que se agregan.</param>
        /// <returns>string</returns>
        public string CaracterSeparador(string caracter, Int16 TotalCaracteres)
        {
            string sAux = string.Empty;

            for (Int16 i = 0; i < TotalCaracteres; i++)
            {
                sAux += caracter;
            }
            return sAux;
        }

        /// <summary>
        /// Agrega simbolos a la derecha para separar cadenas de texto alineadas
        /// a las columnas como una tabla.
        /// </summary>
        /// <param name="caracter">Simbolo que se va a agregar entre cadenas de texto. ej: [-], [*], [espacio].</param>
        /// <param name="TotalCaracteres">Cantidad total de simbolos que se agregan.</param>
        /// <param name="ResCadena">Cadena que se va a escribir a la izquierda de los simbolos.</param>
        /// <returns>Cadena separada con simbolos a la derecha.</returns>
        public string CaracterSeparador(string caracter, Int16 TotalCaracteres, string ResCadena)
        {
            string sAux = string.Empty;
            Int16 AuxCont = (Int16)(ResCadena.Length - 1);

            for (Int16 i = 0; (i < (TotalCaracteres - AuxCont)); i++)
            {
                sAux += caracter;
            }
            return ResCadena + sAux;
        }

        /// <summary>hoja
        /// Escribe en la memoria el dato.
        /// </summary>
        /// <param name="Datos">Dato que se va a imprimir.</param>
        /// <param name="columna">Cantidad de tabulaciones que se dan entre el borde del documento y el texto.</param>
        /// <returns></returns>
        public StringBuilder hoja(string Datos, int columna)
        {
            string aux = string.Empty;

            for (int i = 0; i < columna; i++)
            {
                aux += "\t";
            }
            buildText.AppendFormat(aux + Datos + "\n");
            return buildText;
        }

        /// <summary> DatosTabla.
        /// Escribe los datos de la tabla.
        /// </summary>
        /// <param name="contadorcols">Indica la fila en la que se va a escribir.</param>
        /// <param name="columna">tabs que separan los textos del mismo renglon.</param>
        /// <param name="datos"></param>
        /// <param name="EsEncab">mandar falso cuando se escriba en la tabla.</param>
        public void DatosTabla(ref Int16 contadorFilas, Int16 columnaTab, string datos, bool EsEncab)
        {
            try
            {
                string[] Tabs = buildText.ToString().Split('\t');
                Int16 contadorTabs = 0;

                string aux = string.Empty,
                       aux2 = string.Empty;

                for (int i = 0; i < columnaTab; i++)
                {
                    aux += "\t ";
                }

                contadorTabs = (Int16)Tabs.Length;
                
                if (EsEncab)
                {
                    contadorFilas += (Int16)(contadorTabs * 2);

                    aux2 = datos.ToString() + aux;
                    buildText.Insert(contadorFilas, aux2 + "\n");
                }
                else
                {
                    aux2 = aux + datos + "\n";
                    ItemsTabla.Insert(0, aux2);

                    contadorFilas = (Int16)buildText.Length;
                    
                    buildText.Insert(contadorFilas, ItemsTabla.ToString());
                    buildText.Remove(0, 8);
                }
            }
            catch
            {
                sMensajeInfo = "No se pudo guardar, intenta de nuevo.";
            }
        }

        /// <summary> CopiarArchivo.
        /// Crea un archivo de texto en la ruta especifica.
        /// </summary>
        /// <param name="sNombreArchivo"> Ruta donde se va a guardar el archivo de texto.</param>
        /// <param name="iImprime"> Opcion para imprimir el archivo. 1 imprime, 0 no imprime.</param>
        /// <returns>tipo StreamWriter papel</returns>
        public StreamWriter CopiarArchivo(string sNombreArchivo, int iImprime, ref bool bRegresa)
        {
            StreamWriter papel;
            DirectoryInfo dCarpeta = (Directory.Exists(Direc)) ? null : Directory.CreateDirectory(Direc);

            Direc += sNombreArchivo;
            string sRutaAux = Direc + ".txt";

            FileStream filstream = new FileStream(sRutaAux, FileMode.Create, FileAccess.Write);
            using (papel = new StreamWriter(filstream, Encoding.UTF8))
            {
                papel.Flush();
                filstream.Flush();

                papel.WriteLine(buildText);
                papel.Close();

                if (iImprime == 1)
                {
                    Imprimir();
                }
                GuardarPDF();
                bRegresa = true;
            }

            sImagen = buildText.ToString();

            if (buildText.Length > 0)
            {
                buildText.Remove(0, buildText.Length);
            }
            if (ItemsTabla.Length > 0)
            {
                ItemsTabla.Remove(0, ItemsTabla.Length);
            }

            return papel;
        }

        /// <summary> Imprimir.
        /// Lee un archivo de texto desde una ruta especifica para mandarlo imprimir.
        /// </summary>
        /// <param name="Direc">ruta de donde se leerá el archivo de texto.</param>
        private void Imprimir()
        {
            CImprimir hojaImp = new CImprimir();
            string sLinea = string.Empty;
            string sRutaAux = Direc + ".txt";

            hojaImp.bImprimirHorizontal = false;
            hojaImp.bPintarPaginado = true;

            using (Lectura = new StreamReader(sRutaAux, System.Text.Encoding.Default))
            {
                string[] TotLineas = File.ReadAllLines(sRutaAux);
                int iRenglonesTotales = TotLineas.Length;
                
                for (int iRen = 0; iRen <= iRenglonesTotales; iRen++)
                {
                    sLinea = Lectura.ReadLine();
                    hojaImp.poner(sLinea, iRen, 0, 7);

                    if (iRen > 94)
                    {
                        iRenglonesTotales -= iRen + 1;
                        iRen = 1;
                        hojaImp.SaltoDePagina();
                    }
                }
                hojaImp.Imprimir();
            }
        }

        #region Metodos para la generacion de documentos pdf

        /// <summary>
        /// Genera un PDF a partir del stringBuild en una hoja A3 standard.
        /// </summary>
        public void GuardarPDF()
        {
            double dPageWidth = 0, dPageHeigth = 0;
            Direc += ".pdf";

            try
            {
                PdfDocument document = new PdfDocument();
                PageSize[] pageSizes = (PageSize[])Enum.GetValues(typeof(PageSize));

                //Llenar lista con el stringbuilder.
                List<string> lHojas = SepararHojasTexto();

                //Se recorren y pintan todas las hojas de la lista en el PDF
                foreach (string hoja in lHojas)
                {
                    PdfPage page = document.AddPage();

                    page.Size = PageSize.A3;
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont font = new XFont("Lucida Console", 9, XFontStyle.Regular);
                    XTextFormatter tf = new XTextFormatter(gfx);
                    dPageWidth = page.Width * .90;
                    dPageHeigth = page.Height * .80;
                    XRect MarcoRect = new XRect(70, 50, dPageWidth, dPageHeigth);
                    gfx.DrawRectangle(XBrushes.White, MarcoRect);
                    tf.DrawString(hoja, font, XBrushes.Black, MarcoRect, XStringFormats.TopLeft);
                }
                document.Save(Direc);

                if (File.Exists(Direc))
                {
                    Process.Start(Direc);
                }
            }
            catch
            {
                cmetsinCnx.Mensaje("Ocurrió un error al intentar generar el archivo PDF.", TipoMensaje.Informativo);
                
                if (File.Exists(Direc))
                {
                    File.Delete(Direc);
                }
            }
        }

        private List<string> SepararHojasTexto()
        {
            int iRenglon = 0;
            List<string> lHojas = new List<string>();
            string sHoja = string.Empty;

            foreach (string sRenglon in buildText.ToString().Split('\n'))
            {
                sHoja = sHoja + "\n" + sRenglon;
                if (iRenglon == 110)
                {
                    lHojas.Add(sHoja);
                    sHoja = string.Empty;
                    iRenglon = 0;
                }
                iRenglon++;
            }
            if (iRenglon > 0)
            {
                sHoja = sHoja.Trim();
                if (sHoja.Length > 0)
                {
                    lHojas.Add(sHoja);
                }
            }
            return lHojas;
        }
        #endregion
    }
}
