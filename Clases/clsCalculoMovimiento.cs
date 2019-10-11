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
        private Int32 iNumEmpleado;
        private string sSueldoNto;
        private string sSueldoBrto;
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

        internal List<clsCalculoMovimiento> traerDatos() 
        {
            List<SalarioABC> salarios = BuscarSalarios();
            List<clsCalculoMovimiento> movimientos = new List<clsCalculoMovimiento>();
            foreach (SalarioABC salario in salarios)
            {
                int valesEmpleado = retornarVales((int)salario.idNumEmpleado);
                float fTotalBonos = CalcularTotalBono((int)salario.idNumEmpleado);
                clsCalculoMovimiento movimiento = new clsCalculoMovimiento
                {
                    _NumEmpleado = (int)salario.idNumEmpleado,
                    _Isr = salario.salario_mensual >= 16000 ? 0.12 : 0.09,
                    _Vales = valesEmpleado,
                    _SueldoBrto = ((int)salario.salario_mensual).ToString("$#,###.###"),
                    _SueldoNto = (((int)salario.salario_mensual + (valesEmpleado * 5)) - (((int)salario.salario_mensual) * (salario.salario_mensual >= 16000 ? 0.12 : 0.09)) + fTotalBonos).ToString("$#,###.###")
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
                float fTotalBonos = CalcularTotalBono(iNumEmpleado);
                clsCalculoMovimiento movimiento = new clsCalculoMovimiento
                {
                    _NumEmpleado = (int)salario.idNumEmpleado,
                    _Isr = salario.salario_mensual >= 16000 ? 0.12 : 0.09,
                    _Vales = valesEmpleado,
                    _SueldoBrto = ((int)salario.salario_mensual).ToString("$#,###.###"),
                    _SueldoNto = ((((int)salario.salario_mensual + (valesEmpleado * 5)) - (((int)salario.salario_mensual) * (salario.salario_mensual >= 16000 ? 0.12 : 0.09))) + fTotalBonos).ToString("$#,###.###")
                };
                movimientos.Add(movimiento);
            }

            return movimientos;
        }
         /*
          * Metodo Double
          Tenemos que consultar primero por empleado, Verificamos que sea interno para el bono
          * si es interno sacamos el idRol, y los metemos a un arreglo.
          * Una vez que tenemos visto los roles registrados, vamos a ir sumando en una variable
          * fTotal += BonoDelRol;
          * al final del foreach, vamos a tener la sumatoria total de los bonos.
          * Los Auxiliares son los unicos que tendran en la pangalla de Movimientos un apartado
          * para registrar que cubrieron turno.
          */
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
