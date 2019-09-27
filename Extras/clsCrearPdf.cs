using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace BR050POLIZAMOVTOSVARIOS
{
    class clsCrearPdf
    {
        public CrearArchivo clArch;
        ClsMetodos objMetodos = new ClsMetodos();


        private Int16  iOpcion = 0,
                       iAlineaGeneral = 1,
                       iAlineaTipoReporte = 3,
                       iCantidadLineas = 0;

        private int    iPoliza = 0,
                       iCtapoliza = 0,
                       iSubctapoliza = 0,
                       iCtaconcepto = 0,
                       iSubctaconcepto = 0;

        private double dTotalVtaSinIva = 0,
                       dTotalCosto = 0,
                       dTotalUtilidad = 0,
                       dTotalUnidades,
                       dUnidadesSurtibles = 0,
                       dUnidadesNoSurtibles = 0;

        private string sNomBodega = string.Empty,
                       sFecha = string.Empty,
                       sNomctapoliza = string.Empty,
                       sNomctaconcepto = string.Empty,
                       sNumCentro = string.Empty,
                       sObs = string.Empty,
                       sAux = string.Empty,
                       sDestino = string.Empty,
                       sInstitucion = string.Empty,
                       sPersona = string.Empty;
                        

        private string sConsulta = string.Empty,
                       sNombreArchivo = string.Empty;

        private DataTable dtCodigosPoliza = new DataTable();

        public clsCrearPdf
            (Int16 iOpcion, string sDestino, string sFecha, string sNomBodega, string sNomctapoliza, string sNomctaconcepto,
            string sNumCentro, string sObs, int iTotalVtaSinIva, int iTotalCosto, int iTotalUtilidad,
            int iTotalUnidades, int iUnidadesSurtibles, int iUnidadesNoSurtibles, int iPoliza, int iCtapoliza,
            int iSubctapoliza, int iCtaconcepto, int iSubctaconcepto, string sInstitucion, string sPersona, DataTable dtCodigosPoliza)
        {
            this.iOpcion = iOpcion;

            this.sFecha = sFecha;
            this.sNomBodega = sNomBodega;//"BODEGA ROPA SAN FERNANDO";
            this.sNomctapoliza = sNomctapoliza;//"GASTOS UNIFORMES";
            this.sNomctaconcepto = sNomctaconcepto;//"OTRAS SALIDAS MOV VARIOS ROPA";
            this.sNumCentro = sNumCentro;// "230501";
            this.sObs = sObs;
            this.sInstitucion = sInstitucion;
            this.sPersona = sPersona;

            this.dTotalVtaSinIva = iTotalVtaSinIva;
            this.dTotalCosto = iTotalCosto;
            this.dTotalUtilidad = iTotalUtilidad;
            this.dTotalUnidades = iTotalUnidades;
            this.dUnidadesSurtibles = iUnidadesSurtibles;
            this.dUnidadesNoSurtibles = iUnidadesNoSurtibles;

            this.iPoliza = iPoliza;//1362;
            this.iCtapoliza = iCtapoliza;//5241;
            this.iSubctapoliza = iSubctapoliza;//1041;
            this.iCtaconcepto = iCtaconcepto;//1686;
            this.iSubctaconcepto = iSubctaconcepto;//1147;
            this.sDestino = sDestino;

            this.dtCodigosPoliza = dtCodigosPoliza;

        }

        public bool imprimirPoliza()
        {
            bool bRegresa = false;
            clArch = new CrearArchivo();

            Encabezado();
            llenarPrimeraTabla();
            llenarSegundaTabla(dtCodigosPoliza);

            objMetodos.Mensaje("Se genero póliza con éxito.", TipoMensaje.Informativo);
            
            clArch.CopiarArchivo(sNombreArchivo, 0, ref bRegresa);

            return bRegresa;
        }

        private void Encabezado()
        {
            string sOpcionSecundaria = string.Empty,
            sPonerLineas = string.Empty;
            iCantidadLineas = 109;
           
            if (iOpcion == 1)
            {
                sNombreArchivo = "mvpolent" + iPoliza;
            }
            else if (iOpcion == 2) 
            {
                sNombreArchivo = "mvpolsal" + iPoliza;
            }
            sPonerLineas = clArch.CaracterSeparador("-", iCantidadLineas);
            iAlineaGeneral = 3;
            iAlineaTipoReporte = 6;
            sAux = string.Empty;

            #region Opciones de tipo reporte.
            if (iOpcion == 1)
            {
                sPonerLineas = clArch.CaracterSeparador("-", iCantidadLineas);
                clArch.hoja("", iAlineaGeneral);

                sAux = "    POLIZA ROPA DE MOVIMIENTOS VARIOS  No. " + iPoliza + "        " + sNomBodega;
                clArch.hoja(sAux, iAlineaTipoReporte);
                clArch.salta(1);
                sAux = "    Fecha : " + sFecha + "";
                clArch.salta(1);
                clArch.hoja(sAux, iAlineaTipoReporte);
                clArch.salta(1);
                iAlineaTipoReporte = 1;
               
                clArch.hoja("CTA         SUB-CTA     CENTRO  NOMBRE SUB-CUENTA                                         CARGO      CREDITO", iAlineaGeneral);
                clArch.hoja(sPonerLineas, iAlineaGeneral);
            }

            if (iOpcion == 2)
            {
                sPonerLineas = clArch.CaracterSeparador("-", iCantidadLineas);
                clArch.hoja("", iAlineaGeneral);

                sAux = "    POLIZA ROPA DE MOVIMIENTOS VARIOS  No. " + iPoliza + "        " + sNomBodega;
                clArch.hoja(sAux, iAlineaTipoReporte);
                clArch.salta(1);
                sAux = "    Fecha : " + sFecha + "";
                clArch.salta(1);
                clArch.hoja(sAux, iAlineaTipoReporte);
                clArch.salta(1);
                iAlineaTipoReporte = 1;

                clArch.hoja("CTA         SUB-CTA     CENTRO  NOMBRE SUB-CUENTA                                         CARGO      CREDITO", iAlineaGeneral);
                clArch.hoja(sPonerLineas, iAlineaGeneral);
            }
            #endregion
        }

        private void llenarPrimeraTabla()
        {
            sAux = string.Empty;
            iAlineaGeneral = 3;
            
            /*
             * Ejemplo de formato que se forma:
             CTA         SUB-CTA     CENTRO  NOMBRE SUB-CUENTA                                         CARGO      CREDITO
             ------------------------------------------------------------------------------------------------------------
             5241        1041        230501  GASTOS UNIFORMES                                          -6.00
             1686        1147                OTRAS SALIDAS MOV VARIOS ROPA                                        -6.00             
             */

            switch (iOpcion)
            {
                case 1:
                    #region TRANSFERENCIAS [ENTRADAS] ej: #     CTA         SUB-CTA     CENTRO  NOMBRE SUB-CUENTA        CARGO      CREDITO
                    
                    dTotalCosto = dTotalCosto * -1;
                    sAux = string.Format(clArch.CaracterSeparador(" ", 11, iCtapoliza.ToString()) +
                                         clArch.CaracterSeparador(" ", 11, iSubctapoliza.ToString()) +
                                         clArch.CaracterSeparador(" ", 7, sNumCentro) +
                                         clArch.CaracterSeparador(" ", 57, sNomctapoliza) +
                                         clArch.CaracterSeparador(" ", 14, dTotalCosto.ToString("N2")) +
                                         clArch.CaracterSeparador(" ", 6, ""));
                    
                    clArch.hoja(sAux, iAlineaGeneral);

                    sAux = string.Format(clArch.CaracterSeparador(" ", 11, iCtaconcepto.ToString()) +
                                         clArch.CaracterSeparador(" ", 11, iSubctaconcepto.ToString()) +
                                         clArch.CaracterSeparador(" ", 7, " ") +
                                         clArch.CaracterSeparador(" ", 57, sNomctaconcepto)+
                                         clArch.CaracterSeparador(" ", 10, " ") +
                                         clArch.CaracterSeparador(" ", 6, (dTotalCosto).ToString("N2")));
                    #endregion
                    break;

                case 2:
                    #region TRANSFERENCIAS [SALIDAS] ej: #     CTA         SUB-CTA     CENTRO  NOMBRE SUB-CUENTA        CARGO      CREDITO
                    
                    sAux = string.Format(clArch.CaracterSeparador(" ", 11, iCtapoliza.ToString()) +
                                         clArch.CaracterSeparador(" ", 11, iSubctapoliza.ToString()) +
                                         clArch.CaracterSeparador(" ", 7, sNumCentro) +
                                         clArch.CaracterSeparador(" ", 57, sNomctapoliza) +
                                         clArch.CaracterSeparador(" ", 14, dTotalCosto.ToString("N2")) +
                                         clArch.CaracterSeparador(" ", 6, ""));

                    clArch.hoja(sAux, iAlineaGeneral);

                    sAux = string.Format(clArch.CaracterSeparador(" ", 11, iCtaconcepto.ToString()) +
                                         clArch.CaracterSeparador(" ", 11, iSubctaconcepto.ToString()) +
                                         clArch.CaracterSeparador(" ", 7, " ") +
                                         clArch.CaracterSeparador(" ", 57, sNomctaconcepto) +
                                         clArch.CaracterSeparador(" ", 10, " ") +
                                         clArch.CaracterSeparador(" ", 6, (dTotalCosto).ToString("N2")));
                    #endregion
                    break;
            }
            clArch.hoja(sAux, iAlineaGeneral);

            sAux = "OBSERVACIONES: " + sObs;
            clArch.salta(1);
            clArch.hoja(sAux, iAlineaGeneral);

            iAlineaGeneral = 0;

        }

        public void llenarSegundaTabla(DataTable dtTabla)
        {
            string sPonerLineas = string.Empty;
           
            sPonerLineas = clArch.CaracterSeparador("-", iCantidadLineas);
            iAlineaGeneral = 3;
            clArch.salta(1);

            if (iOpcion == 1)
            {
                sAux = clArch.CaracterSeparador(" ", 20) + "E N T R A D A   D E   I N V E N T A R I O S";
                iCantidadLineas = 109;
                dTotalCosto = dTotalCosto * -1;
            }
            else if (iOpcion == 2)
            {
                sAux = clArch.CaracterSeparador(" ", 20) + "S A L I D A   D E   I N V E N T A R I O S";
                iCantidadLineas = 125;
            }

           
            clArch.hoja(sAux, 5);
            clArch.salta(1);

            if (iOpcion == 1)
            {
                sAux = "CODIGO     TALLA   ARTICULO                                            FABRICA    VENTA    CANTIDAD  SURTIBLE";
            }
            else if (iOpcion == 2)
            {
                sAux = "CODIGO TALLA ARTICULO                                            COSTO  VTA   CANT SURT  PROV    FECHA          FACTURA";
            }

            clArch.hoja(sAux, 3);
            sPonerLineas = clArch.CaracterSeparador("-", iCantidadLineas);
            clArch.hoja(sPonerLineas, iAlineaGeneral);

            foreach (DataRow item in dtTabla.Rows)
            {
                switch (iOpcion)
                {
                    case 1:/*CODIGO     TALLA   ARTICULO           FABRICA    VENTA    CANTIDAD  SURTIBLE*/
                        #region TRANSFERENCIAS [jabas] ej: CODIGO TALLA ARTICULO             COSTO  VTA   CANT SURT  PROV    FECHA          FACTURA

                        string sSurtible = (item["tipomercancia"].ToString() == "1") ? "SI" : "NO";
                        
                        
                        sAux = string.Format(clArch.CaracterSeparador(" ", 10, item["Codigo"].ToString()) +
                                           clArch.CaracterSeparador(" ", 7, item["Talla"].ToString()) +
                                           clArch.CaracterSeparador(" ", 52, item["Desccorta"].ToString()) +
                                           clArch.CaracterSeparador(" ", 11, item["Fabrica"].ToString()) +
                                           clArch.CaracterSeparador(" ", 11, item["Venta"].ToString()) +
                                           clArch.CaracterSeparador(" ", 7, item["Cantidad"].ToString()) +
                                           clArch.CaracterSeparador(" ", 7, sSurtible));
                        
                        #endregion
                        break;
                    case 2:
                        #region Salidas

                        string sSurtible2 = (item["Tipomercancia"].ToString() == "1") ? "SI" : "NO";

                        sAux = string.Format(clArch.CaracterSeparador(" ", 7, item["numcodigo"].ToString()) +
                                             clArch.CaracterSeparador(" ", 4, item["numtalla"].ToString()) +
                                             clArch.CaracterSeparador(" ", 52, item["desccorta"].ToString()) +
                                             clArch.CaracterSeparador(" ", 6, item["costo"].ToString()) +
                                             clArch.CaracterSeparador(" ", 5, item["precio"].ToString()) +
                                             clArch.CaracterSeparador(" ", 5, item["cantidad"].ToString()) +
                                             clArch.CaracterSeparador(" ", 4, sSurtible2) +
                                             clArch.CaracterSeparador(" ", 7, item["numproveedor"].ToString()) +
                                             clArch.CaracterSeparador(" ", 14, Convert.ToDateTime( item["fechafactura"].ToString()).ToString("yyyy/MM/dd")) +
                                             clArch.CaracterSeparador(" ", 12, item["numfactura"].ToString()));

                      
                        #endregion
                        break;
                }
                clArch.hoja("" + sAux, iAlineaGeneral);
            }

            if (iOpcion == 2) 
            {
                dTotalVtaSinIva = dTotalVtaSinIva * -1;
                dTotalCosto = dTotalCosto * -1;
                dTotalUtilidad = dTotalUtilidad * -1;
            }

            clArch.salta(2);
            sAux = clArch.CaracterSeparador(" ", 42) + "TOTAL VTA S/IVA = " + dTotalVtaSinIva.ToString("N2");
            clArch.hoja(sAux, 0);
            sAux = clArch.CaracterSeparador(" ", 42) + "TOTAL COSTO     = " + dTotalCosto.ToString("N2");
            clArch.hoja(sAux, 0);
            sAux = clArch.CaracterSeparador(" ", 42) + "TOTAL UTILIDAD  = " + dTotalUtilidad.ToString("N2");
            clArch.hoja(sAux, 0);
            sAux = clArch.CaracterSeparador(" ", 42) + "TOTAL UNIDADES  = " + dTotalUnidades.ToString("N2");
            clArch.hoja(sAux, 0);

            clArch.salta(1);
            sAux = "Unidades SURTIBLES : " + dUnidadesSurtibles + "             Unidades NO SURTIBLES : " + dUnidadesNoSurtibles;
            clArch.hoja(sAux, 3);

            if (sDestino == "1")
            {
                clArch.salta(2);

                sAux = "Institucion que recibira la mercancia : " + sInstitucion;
                clArch.hoja(sAux, 3);

                sAux = "Persona que recibira la mercancia : " + sPersona;
                clArch.hoja(sAux, 3);
            }

        }
    }
}
