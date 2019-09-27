using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcCompleto
{
    class clsCalculoMovimiento : clsEmpleado
    {
        private Int32 iNumEmpleado;
        private double iSueldoNto;
        private int iSueldoBrto;
        private int iVales;
        private double dIsr;
        private int iBono;

        public Int32 _NumEmpleado
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
        
        public int _SueldoBrto
        {
            get { return iSueldoBrto; }
            set { iSueldoBrto = value; }
        }
    
        public double _SueldoNto
        {
            get { return iSueldoNto; }
            set { iSueldoNto = value; }
        }

        public int _IBono
        {
            get { return iBono; }
            set { iBono = value; }
        }

        public clsCalculoMovimiento() 
        {
        
        }

        internal List<clsCalculoMovimiento> traerDatos() 
        {
            List<SalarioABC> salarios = BuscarSalariosTotalesRN();
            List<clsCalculoMovimiento> movimientos = new List<clsCalculoMovimiento>();
            foreach (SalarioABC salario in salarios)
            {
                int valesEmpleado = retornarVales((int)salario.idNumEmpleado);
                clsCalculoMovimiento movimiento = new clsCalculoMovimiento
                {
                    _NumEmpleado = (int)salario.idNumEmpleado,
                    _Isr = salario.salario_mensual >= 16000 ? 0.12 : 0.09,
                    _Vales = valesEmpleado,
                    _SueldoBrto = (int)salario.salario_mensual,
                    _SueldoNto = ((int)salario.salario_mensual + (valesEmpleado * 5)) - (((int)salario.salario_mensual) * (salario.salario_mensual >= 16000 ? 0.12 : 0.09))
                };
                movimientos.Add(movimiento);
            }

            return movimientos;
        }

        internal List<clsCalculoMovimiento> traerDatos(int iNumEmpleado)
        {
            List<SalarioABC> salarios = BuscarSalario(iNumEmpleado);
            List<clsCalculoMovimiento> movimientos = new List<clsCalculoMovimiento>();
            foreach (SalarioABC salario in salarios)
            {
                int valesEmpleado = retornarVales((int)salario.idNumEmpleado);
                clsCalculoMovimiento movimiento = new clsCalculoMovimiento
                {
                    _NumEmpleado = (int)salario.idNumEmpleado,
                    _Isr = salario.salario_mensual >= 16000 ? 0.12 : 0.09,
                    _Vales = valesEmpleado,
                    _SueldoBrto = (int)salario.salario_mensual,
                    _SueldoNto = ((int)salario.salario_mensual + (valesEmpleado * 5)) - (((int)salario.salario_mensual) * (salario.salario_mensual >= 16000 ? 0.12 : 0.09))
                };
                movimientos.Add(movimiento);
            }

            return movimientos;
        }

    }
}
