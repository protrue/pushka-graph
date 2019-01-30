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
using PushkaGraphCore;

namespace PushkaGraphGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Graph _graph;
        private Dictionary<IVertex, VertexControl> _vertices;
        private int _vertexSize;
        
        public MainWindow()
        {
            InitializeComponent();
            _graph = new Graph();
            _vertexSize = 32;
            _vertices = new Dictionary<IVertex, VertexControl>();
        }

        private void CreateVertex(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(this);
            point.Y -= _vertexSize / 2;
            point.X -= _vertexSize / 2;

            var vertexControl = new VertexControl();
            Canvas.SetTop(vertexControl, point.Y);
            Canvas.SetLeft(vertexControl, point.X);
            Container.Children.Add(vertexControl);

            var vertex = _graph.AddVertex();
            _vertices[vertex] = vertexControl;
        }
    }
}
