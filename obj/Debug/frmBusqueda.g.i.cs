﻿#pragma checksum "..\..\frmBusqueda.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0FBB2031F817CD4C7DD702FCE092EE1A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using abcCompleto;


namespace abcCompleto {
    
    
    /// <summary>
    /// frmBusqueda
    /// </summary>
    public partial class frmBusqueda : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\frmBusqueda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBusquedaEmp;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\frmBusqueda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblBusqueda;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\frmBusqueda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtgEmpleados;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/abcCompleto;component/frmbusqueda.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmBusqueda.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 6 "..\..\frmBusqueda.xaml"
            ((System.Windows.Controls.Grid)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Grid_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtBusquedaEmp = ((System.Windows.Controls.TextBox)(target));
            
            #line 11 "..\..\frmBusqueda.xaml"
            this.txtBusquedaEmp.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtBusquedaEmp_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lblBusqueda = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.dtgEmpleados = ((System.Windows.Controls.DataGrid)(target));
            
            #line 13 "..\..\frmBusqueda.xaml"
            this.dtgEmpleados.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dtgEmpleados_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

