using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PushkaGraphGUI
{
    /// <summary>
    /// Interaction logic for VertexControl.xaml
    /// </summary>
    public partial class VertexControl : UserControl
    {
        public Brush Color { get; set; }
        public VertexControl()
        {
            InitializeComponent();
            Width = 32;
            Height = 32;
            Color = Brushes.White;
            Ellipse.SetBinding(Shape.FillProperty, new Binding {Source = Color});
        }
    }
}
