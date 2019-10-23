using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcCompleto
{
     class clsCalculoMovimiento : clsEmpleado
     {
         private int iNumEmpleado;
        private string sSueldoNto;
        private string sSueldoBrto;
        private int iVales;
        private double dIsr;
        private int iBono;

        public int _NumEmpleado
        {
            get { return iNumEmpleado; }
            set { iNumEmpleado = value; }
        }
   
        public double _Isr
        {
            get { return dIsr; }
            set { dIsr = value; }
        }

        public int _Vales
        {
            get { return iVales; }
            set { iVales = value; }
        }
        
        public string _SueldoBrto
        {
            get { return sSueldoBrto; }
            set { sSueldoBrto = value; }
        }

        public string _SueldoNto
        {
            get { return sSueldoNto; }
            set { sSueldoNto = value; }
        }

        public int _IBono
        {
            get { return iBono; }
            set { iBono = value; }
        }

        public clsCalculoMovimiento() 
        {
        
        }

        /// <summary>
        /// Retorna los calculos de la nomina de todos los empleados.
        /// </summary>
        /// <returns>List</returns>
        internal List<clsCalculoMovimiento> traerDatos() 
        {
            List<SalarioABC> salarios = BuscarSalarios();
            List<clsCalculoMovimiento> movimientos = new List<clsCalculoMovimiento>();
            foreach (SalarioABC salario in salarios)
            {
                int valesEmpleado = retornarVales((int)salario.idNumEmpleado);
                float fTotalBonos = CalcularTotalBono((int)salario.idNumEmpleado);
                int iTipoEmpleado = RetornaTipoEmpleado((int)salario.idNumEmpleado);
                double dBonoDespensa = iTipoEmpleado == 1 ? (double)salario.salario_mensual * 0.04 : 0;
                clsCalculoMovimiento movimiento = new clsCalculoMovimiento
                {
                    _NumEmpleado = (int)salario.idNumEmpleado,
                    _Isr = salario.salario_mensual >= 16000 ? 0.12 : 0.09,
                    _Vales = valesEmpleado,
                    _SueldoBrto = ((double)salario.salario_mensual).ToString("$#,###.###"),
                    _SueldoNto = (((double)salario.salario_mensual + (valesEmpleado * 5)) - (((double)salario.salario_mensual) * (salario.salario_mensual >= 16000 ? 0.12 : 0.09)) + fTotalBonos + dBonoDespensa).ToString("$#,###.###")
                };
                movimientos.Add(movimiento);
            }

            return movimientos;
        }

        /// <summary>
        /// Retorna el calculo de la nomina de un empleado en especifico.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <returns>List</returns>
        internal List<clsCalculoMovimiento> traerDatos(int iNumEmpleado)
        {
            List<SalarioABC> salarios = BuscarSalario(iNumEmpleado);
            List<clsCalculoMovimiento> movimientos = new List<clsCalculoMovimiento>();
            foreach (SalarioABC salario in salarios)
            {
                int valesEmpleado = retornarVales((int)salario.idNumEmpleado);
                float fTotalBonos = CalcularTotalBono(iNumEmpleado);
                int iTipoEmpleado = RetornaTipoEmpleado((int)salario.idNumEmpleado);
                double dBonoDespensa = iTipoEmpleado == 1 ? (double)salario.salario_mensual * 0.04 : 0;
                clsCalculoMovimiento movimiento = new clsCalculoMovimiento
                {
                    _NumEmpleado = (int)salario.idNumEmpleado,
                    _Isr = salario.salario_mensual >= 16000 ? 0.12 : 0.09,
                    _Vales = valesEmpleado,
                    _SueldoBrto = ((double)salario.salario_mensual).ToString("$#,###.###"),
                    _SueldoNto = ((((double)salario.salario_mensual + (valesEmpleado * 5)) - (((double)salario.salario_mensual) * (salario.salario_mensual >= 16000 ? 0.12 : 0.09))) + fTotalBonos + dBonoDespensa).ToString("$#,###.###")
                };
                movimientos.Add(movimiento);
            }

            return movimientos;
        }

        /// <summary>
        /// Calcula el total de bonos para un empleado.
        /// </summary>
        /// <param name="iNumEmpleado">Numero del empleado.</param>
        /// <returns>void</returns>
        internal float CalcularTotalBono(int iNumEmpleado) 
        {
            List<HorariosABC> horariosEmpleado = BuscarHorarios(iNumEmpleado);
            ArrayList alListaRoles = new ArrayList();
            float fTotalBono = 0;
            foreach (HorariosABC horario in horariosEmpleado)
            {
                alListaRoles.Add(horario.idrol);
            }

            foreach (int Rol in alListaRoles)
            {
                fTotalBono += RetornarBonoRol(Rol) * 8;
            }
            return fTotalBono;
            
        }

    }
}
