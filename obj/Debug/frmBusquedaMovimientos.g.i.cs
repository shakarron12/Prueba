﻿#pragma checksum "..\..\frmBusquedaMovimientos.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F70ABCBCD329EA1D2D2F0096BFC4E817"
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


namespace abcCompleto {
    
    
    /// <summary>
    /// frmBusquedaMovimientos
    /// </summary>
    public partial class frmBusquedaMovimientos : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\frmBusquedaMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal abcCompleto.frmBusquedaMovimientos frmBusqueda;
        
        #line default
        #line hidden
        
        
        #line 5 "..\..\frmBusquedaMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gdControles;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\frmBusquedaMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblBusqueda;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\frmBusquedaMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtgMovimientos;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\frmBusquedaMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtFechaInicio;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\frmBusquedaMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtFechaFin;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\frmBusquedaMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblBusqueda_Copy;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\frmBusquedaMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBuscar;
        
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
            System.Uri resourceLocater = new System.Uri("/abcCompleto;component/frmbusquedamovimientos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmBusquedaMovimientos.xaml"
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
            this.frmBusqueda = ((abcCompleto.frmBusquedaMovimientos)(target));
            
            #line 4 "..\..\frmBusquedaMovimientos.xaml"
            this.frmBusqueda.KeyDown += new System.Windows.Input.KeyEventHandler(this.frmBusqueda_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.gdControles = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.lblBusqueda = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.dtgMovimientos = ((System.Windows.Controls.DataGrid)(target));
            
            #line 11 "..\..\frmBusquedaMovimientos.xaml"
            this.dtgMovimientos.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dtgMovimientos_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dtFechaInicio = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.dtFechaFin = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.lblBusqueda_Copy = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnBuscar = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\frmBusquedaMovimientos.xaml"
            this.btnBuscar.Click += new System.Windows.RoutedEventHandler(this.btnBuscar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

