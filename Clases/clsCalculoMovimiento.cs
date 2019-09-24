using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcCompleto
{
    class clsCalculoMovimiento : clsEmpleado
    {
        private Int32 numEmpleado;
        private double sueldoNto;
        private int sueldoBrto;
        private int vales;
        private double isr;

        public Int32 _NumEmpleado
        {
            get { return numEmpleado; }
            set { numEmpleado = value; }
        }
   
        public double _Isr
        {
            get { return isr; }
            set { isr = value; }
        }

        public int _Vales
        {
            get { return vales; }
            set { vales = value; }
        }
        
        public int _SueldoBrto
        {
            get { return sueldoBrto; }
            set { sueldoBrto = value; }
        }
    
        public double _SueldoNto
        {
            get { return sueldoNto; }
            set { sueldoNto = value; }
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
