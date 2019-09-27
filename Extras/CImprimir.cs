using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Management;

namespace BR050POLIZAMOVTOSVARIOS
{
    public class CImprimir
    {
        public struct Linea
        {
            public string linea;
            public int iXPos;
            public int fYpos;
            public bool impreso;
            public bool salto;
            public int tamano;
            public FontStyle estilo;
            public int alto;
            public int ancho;
            public bool flagimagen;
            public bool flaglinea;
            public bool flagHorizontal;
        };

        public struct Paginado
        {
            public int iXPos;
            public int fYpos;
            public int tamano;
            public string sMensaje;
            public FontStyle estilo;
        };

        #region Declaracion de Variables

        Font printFont = null;
        int fontSize = 8;
        Linea linea = new Linea();
        Paginado contpag = new Paginado();
        FontStyle estiloFuente = FontStyle.Regular;
        ClsMetodos clsMetodos = new ClsMetodos();
        float leftMargin = 2;
        float topMargin = 30;
        int iPrintedLines = -1;

        int NumPaginas = 1;
        int ContPagina = 0;

        PrintDocument pr;

        ArrayList renglones = new ArrayList();
        ArrayList imagenes = new ArrayList();
        SolidBrush myBrush = new SolidBrush(Color.Black);

        bool bPaginado = false;
        #endregion

        #region Declaracion de Propiedades

        public bool bImprimirHorizontal
        {
            get { return pr.DefaultPageSettings.Landscape; }
            set { pr.DefaultPageSettings.Landscape = value; }
        }

        public bool bPintarPaginado
        {
            get { return bPaginado; }
            set { bPaginado = value; }
        }

        public int AjustarTamano
        {
            set { fontSize = value; }
        }

        public FontStyle AjustarEstilo
        {
            set { estiloFuente = value; }
        }

        #endregion
        #region Metodos

        public void Preview()
        {
            PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
            MyPrintPreviewDialog.Document = pr;
            MyPrintPreviewDialog.WindowState = FormWindowState.Maximized;
            MyPrintPreviewDialog.ShowDialog();
        }

        public void SaltoDePagina()
        {
            if (renglones.Count > 0)
            {
                Linea my_value = (Linea)renglones[renglones.Count - 1];
                my_value.salto = true;
                renglones[renglones.Count - 1] = my_value;
                NumPaginas++;
            }
        }

        public CImprimir()
        {
            //inicializaDatos(9, FontStyle.Regular);
            inicializaDatos();
        }

        void inicializaDatos()
        {
            contpag.estilo = estiloFuente;
            contpag.iXPos = 0;
            contpag.fYpos = 0;
            contpag.tamano = fontSize;

            pr = new PrintDocument();
            pr.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
            pr.PrintPage += new PrintPageEventHandler(impresion_PrintPage);
        }

        public void poner(string sTexto, int iRen, int iCol)
        {
            poner(sTexto, iRen, iCol, fontSize, FontStyle.Regular);
        }

        public void poner(string sTexto, int iRen, int iCol, int iTamano)
        {
            poner(sTexto, iRen, iCol, iTamano, FontStyle.Regular);
        }

        public void poner(string sTexto, int iRen, int iCol, FontStyle style)
        {
            poner(sTexto, iRen, iCol, fontSize, style);
        }

        public void poner(string sTexto, int iRen, int iCol, int iTamano, FontStyle style)
        {
            linea.linea = sTexto;
            linea.iXPos = iCol;
            linea.fYpos = iRen;
            linea.impreso = false;
            linea.salto = false;
            linea.tamano = iTamano;
            linea.estilo = style;
            linea.alto = 0;
            linea.ancho = 0;
            linea.flagimagen = false;
            linea.flaglinea = false;
            linea.flagHorizontal = false;
            renglones.Add(linea);
        }

        public void ponerImagen(string sRuta, int iXPos, int iYPos)
        {
            ponerImagen(sRuta, iXPos, iYPos, 0, 0);
        }

        public void ponerImagen(string sRuta, int iXPos, int iYPos, int iAlto, int iAncho)
        {
            linea.linea = sRuta;
            linea.iXPos = iXPos;
            linea.fYpos = iYPos;
            linea.impreso = false;
            linea.salto = false;
            linea.tamano = 0;
            linea.estilo = FontStyle.Regular;
            linea.alto = iAlto;
            linea.ancho = iAncho;
            linea.flagimagen = true;
            linea.flaglinea = false;
            linea.flagHorizontal = false;
            renglones.Add(linea);
        }

        public void dibujarLinea(int iXPos, int iYPos, int iLongitud)
        {
            dibujarLinea(iXPos, iYPos, iLongitud, false);
        }

        public void dibujarLinea(int iXPos, int iYPos, int iLongitud, bool bHorizontal)
        {
            linea.linea = "";
            linea.iXPos = iXPos;
            linea.fYpos = iYPos;
            linea.impreso = false;
            linea.salto = false;
            linea.tamano = fontSize;
            linea.estilo = FontStyle.Regular;
            linea.alto = iLongitud;
            linea.ancho = iLongitud;
            linea.flagimagen = false;
            linea.flaglinea = true;
            linea.flagHorizontal = bHorizontal;
            renglones.Add(linea);
        }

        private void impresion_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int Ypos = 0, XPos = 0;
            string sMensaje = "";

            // Si se Activo el Flag del Paginado se imprime el numero de pagina 
            // al final de la hoja o en la posicion indicada por el programador.
            if (bPaginado)
            {
                ContPagina++;

                if (contpag.iXPos == 0 && contpag.fYpos == 0)
                {
                    if (bImprimirHorizontal)
                    {
                        Ypos = 800;
                        XPos = 920;
                    }
                    else
                    {
                        Ypos = 1030;
                        XPos = 650;
                    }

                    sMensaje = "Páginas: ";
                }
                else
                {
                    Ypos = contpag.fYpos;
                    XPos = contpag.iXPos;
                    sMensaje = contpag.sMensaje;
                }

                printFont = new Font("Lucida Console", contpag.tamano, contpag.estilo);
                e.Graphics.DrawString(sMensaje + " " + ContPagina.ToString() + " / " + NumPaginas.ToString(), printFont, myBrush, XPos, Ypos);
                printFont.Dispose();
            }

            // Se imprimen todos los renglones que se hayan agregado.
            if (DrawTextLines(e))
            {
                e.HasMorePages = true;
            }
        }

        private bool DrawTextLines(System.Drawing.Printing.PrintPageEventArgs e)
        {
            float fRen = 0, fCol = 0, fPuntoX = 0, fPuntoY = 0, fPuntoX1 = 0, fPuntoY1 = 0;
            int i = 0;
            bool bRet = false;
            int iRenglon = 0;
            Image img;

            for (i = iPrintedLines; i < renglones.Count; i++)
            {
                iPrintedLines++;
                if (iPrintedLines < renglones.Count)
                {
                    Linea my_value = (Linea)renglones[iPrintedLines];

                    if (my_value.flagimagen)
                    {
                        if (File.Exists(my_value.linea))
                        {
                            img = new Bitmap(my_value.linea);

                            fCol = leftMargin + my_value.iXPos;
                            fRen = topMargin + my_value.fYpos;
                            if (my_value.ancho == 0 && my_value.alto == 0)
                            {
                                e.Graphics.DrawImage(img, my_value.iXPos, my_value.fYpos);
                            }
                            else
                            {
                                e.Graphics.DrawImage(img, my_value.iXPos, my_value.fYpos, my_value.ancho, my_value.alto);
                            }
                        }
                    }
                    else if (my_value.flaglinea)
                    {
                        printFont = new Font("Lucida Console", my_value.tamano, my_value.estilo);
                        fCol = (leftMargin * (printFont.Size - 2.0F)) + (my_value.iXPos * (printFont.Size - 2.0F));
                        fRen = topMargin + (my_value.fYpos * printFont.GetHeight(e.Graphics));

                        fPuntoX = fCol;//leftMargin + my_value.iXPos;
                        fPuntoY = fRen;//topMargin + my_value.fYpos;

                        if (my_value.flagHorizontal)
                        {
                            //fPuntoX1 = fPuntoX + my_value.ancho;
                            fPuntoX1 = (fPuntoX + (printFont.Size - 2.0F)) + (my_value.ancho * (printFont.Size - 2.0F));
                            fPuntoY1 = fPuntoY;
                            e.Graphics.DrawLine(Pens.Black, fPuntoX, fPuntoY, fPuntoX1, fPuntoY1);
                        }
                        else
                        {
                            fPuntoX1 = fPuntoX;
                            //fPuntoY1 = fPuntoY + my_value.alto;
                            fPuntoY1 = (fPuntoY + (printFont.Size - 2.0F)) + (my_value.alto * (printFont.Size - 2.0F));
                            e.Graphics.DrawLine(Pens.Black, fPuntoX, fPuntoY, fPuntoX1, fPuntoY1);
                        }
                    }
                    else
                    {
                        printFont = new Font("Lucida Console", my_value.tamano, my_value.estilo);
                        fCol = (leftMargin * (printFont.Size - 2.0F)) + (my_value.iXPos * (printFont.Size - 2.0F));
                        fRen = topMargin + (my_value.fYpos * printFont.GetHeight(e.Graphics));
                        e.Graphics.DrawString(my_value.linea, printFont, myBrush, fCol, fRen);

                        my_value.impreso = true;
                        renglones[iPrintedLines] = my_value;

                        printFont.Dispose();
                    }

                    if (my_value.salto)
                    {
                        my_value.salto = false;
                        renglones[iPrintedLines] = my_value;
                        bRet = true;
                        /*
                         La razon de usar el return dentro del ciclo es para darle una ruta de salida
                         ya que esta funcion es ascincrona y depende de los eventos de C#
                        */
                        return true;
                    }
                }
                iRenglon++;
            }

            return bRet;
        }

        public void ponerPaginado(string sMensaje, int iXPos, int iYPos, int iTamano, FontStyle estilo)
        {
            contpag.sMensaje = sMensaje;
            contpag.iXPos = iXPos;
            contpag.fYpos = iYPos;
            contpag.tamano = iTamano;
            contpag.estilo = estilo;
        }

        public void Imprimir()
        {
            if (renglones.Count > 0)
            {

                string s_Default_Printer = pr.PrinterSettings.PrinterName;
                if (IsPrinterOnline(s_Default_Printer))
                {
                    pr.Print();
                }
            }
        }

        /// <summary>
        ///Metodo para comprobar si una impresora esta online o offline
        /// </summary>
        /// <param name="printerName">nombre de la impresora</param>
        /// <returns></returns>
        public bool IsPrinterOnline(string printerName)
        {
            string str = "";
            bool online = false;

            //set the scope of this search to the local machine
            ManagementScope scope = new ManagementScope(ManagementPath.DefaultPath);
            //connect to the machine
            scope.Connect();

            //query for the ManagementObjectSearcher
            SelectQuery query = new SelectQuery("select * from Win32_Printer");

            ManagementClass m = new ManagementClass("Win32_Printer");

            ManagementObjectSearcher obj = new ManagementObjectSearcher(scope, query);

            //get each instance from the ManagementObjectSearcher object
            using (ManagementObjectCollection printers = m.GetInstances())
                //now loop through each printer instance returned
                foreach (ManagementObject printer in printers)
                {
                    //first make sure we got something back
                    if (printer != null)
                    {
                        //get the current printer name in the loop
                        str = printer["Name"].ToString().ToLower();

                        //check if it matches the name provided
                        if (str.Equals(printerName.ToLower()))
                        {
                            //since we found a match check it's status
                            if (printer["WorkOffline"].ToString().ToLower().Equals("true") || printer["PrinterStatus"].Equals(7))
                                //it's offline
                                online = false;
                            else
                                //it's online
                                online = true;
                        }
                    }
                    else
                        clsMetodos.Mensaje("No se encontraron impresoras.", TipoMensaje.Informativo);  
                }
            return online;
        }


        #endregion
    }

}