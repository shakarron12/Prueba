﻿#pragma checksum "..\..\frmModificarMovimientos.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7DBF6D4700E9FCA24F6C1BDEF87BD226"
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
    /// frmModificarMovimientos
    /// </summary>
    public partial class frmModificarMovimientos : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\frmModificarMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMovimiento;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\frmModificarMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCantidad;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\frmModificarMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbRol;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\frmModificarMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbTipo;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\frmModificarMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGuardar;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\frmModificarMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEliminar;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\frmModificarMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLimpiar;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\frmModificarMovimientos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtFecha;
        
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
            System.Uri resourceLocater = new System.Uri("/abcCompleto;component/frmmodificarmovimientos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmModificarMovimientos.xaml"
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
            
            #line 4 "..\..\frmModificarMovimientos.xaml"
            ((abcCompleto.frmModificarMovimientos)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtMovimiento = ((System.Windows.Controls.TextBox)(target));
            
            #line 7 "..\..\frmModificarMovimientos.xaml"
            this.txtMovimiento.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtMovimiento_KeyDown);
            
            #line default
            #line hidden
            
            #line 7 "..\..\frmModificarMovimientos.xaml"
            this.txtMovimiento.LostFocus += new System.Windows.RoutedEventHandler(this.txtMovimiento_LostFocus);
            
            #line default
            #line hidden
            
            #line 7 "..\..\frmModificarMovimientos.xaml"
            this.txtMovimiento.PreviewLostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.txtMovimiento_PreviewLostKeyboardFocus);
            
            #line default
            #line hidden
            
            #line 7 "..\..\frmModificarMovimientos.xaml"
            this.txtMovimiento.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.txtMovimiento_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtCantidad = ((System.Windows.Controls.TextBox)(target));
            
            #line 9 "..\..\frmModificarMovimientos.xaml"
            this.txtCantidad.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.txtCantidad_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 9 "..\..\frmModificarMovimientos.xaml"
            this.txtCantidad.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtCantidad_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbRol = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.cbTipo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.btnGuardar = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\frmModificarMovimientos.xaml"
            this.btnGuardar.Click += new System.Windows.RoutedEventHandler(this.btnGuardar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnEliminar = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\frmModificarMovimientos.xaml"
            this.btnEliminar.Click += new System.Windows.RoutedEventHandler(this.btnEliminar_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnLimpiar = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\frmModificarMovimientos.xaml"
            this.btnLimpiar.Click += new System.Windows.RoutedEventHandler(this.btnLimpiar_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dtFecha = ((System.Windows.Controls.DatePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

