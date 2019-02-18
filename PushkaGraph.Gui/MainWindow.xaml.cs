using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.Win32;
using PushkaGraph.Core;
using PushkaGraph.Tools;

namespace PushkaGraph.Gui
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Graph _graph;
        private InterfaceAction _currentAction;

        private const string SelectedTag = "Selected";

        public MainWindow()
        {
            InitializeComponent();
            MinHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MinWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            _graph = new Graph();
            _ellipses = new Dictionary<VertexControl, Vertex>();
            _vertices = new Dictionary<Vertex, VertexControl>(); 
            _edges = new Dictionary<Edge, Line>();
            _lines = new Dictionary<Line, Edge>();
            _edgeWeightMapping = new Dictionary<Edge, TextBox>();
            _weightEdgeMapping = new Dictionary<TextBox, Edge>();
            _currentAction = InterfaceAction.VertexEdit;
            CreateVertexButton.Tag = SelectedTag;
            _currentCreateEdgeActionState = CreateEdgeActionState.SelectFirstVertex;
        }

        private void ClearStructures()
        {
            Container.Children.Clear();
            _graph = new Graph();
            _ellipses.Clear();
            _vertices.Clear();
            _edges.Clear();
            _lines.Clear();
            _edgeWeightMapping.Clear();
            _weightEdgeMapping.Clear();
            _currentAction = InterfaceAction.VertexEdit;
            CreateVertexButton.Tag = SelectedTag;
            _currentCreateEdgeActionState = CreateEdgeActionState.SelectFirstVertex;
        }

        /// <summary>
        /// Обрабатывает событие нажатия левой кнопкой мыши на поле.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContainerLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_currentAction)
            {
                case InterfaceAction.VertexEdit:
                    if (_isVertexMoving) break;
                    CreateVertex(sender, e);
                    break;
                case InterfaceAction.EdgeEdit:
                    break;
                case InterfaceAction.AlgorithmA:
                    break;
                case InterfaceAction.AlgorithmB:
                    break;
                case InterfaceAction.AlgorithmC:
                    break;
                case InterfaceAction.AlgorithmD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия правой кнопкой мыши на поле.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContainerRightMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_currentAction)
            {
                case InterfaceAction.VertexEdit:
                    break;
                case InterfaceAction.EdgeEdit:
                    if (_currentCreateEdgeActionState == CreateEdgeActionState.SelectSecondVertex)
                        AbortCreateEdgeAction();
                    break;
                case InterfaceAction.AlgorithmA:
                    break;
                case InterfaceAction.AlgorithmB:
                    break;
                case InterfaceAction.AlgorithmC:
                    break;
                case InterfaceAction.AlgorithmD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Обрабатывает событие движения мыши над полем, где рисуется граф.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContainerMouseMove(object sender, MouseEventArgs e)
        {
            var cursorPosition = e.GetPosition(Container);
            switch (_currentAction)
            {
                case InterfaceAction.VertexEdit:
                    if (!_isVertexMoving) break;
                    UpdateMovingVertexPosition(cursorPosition);
                    UpdateEdgesPosition(_ellipses[_movingEllipse].IncidentEdges);
                    break;
                case InterfaceAction.EdgeEdit:
                    if (_currentCreateEdgeActionState == CreateEdgeActionState.SelectSecondVertex)
                        UpdateEdgeEndPosition(cursorPosition);
                    break;
                case InterfaceAction.AlgorithmA:
                    break;
                case InterfaceAction.AlgorithmB:
                    break;
                case InterfaceAction.AlgorithmC:
                    break;
                case InterfaceAction.AlgorithmD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия на вершину.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEllipseMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ellipse = (VertexControl) sender;
            switch (_currentAction)
            {
                case InterfaceAction.VertexEdit:
                    StartMovingVertex(ellipse);
                    break;
                case InterfaceAction.EdgeEdit:
                    CreateEdge(ellipse, e.GetPosition(Container));
                    break;
                case InterfaceAction.AlgorithmA:
                    break;
                case InterfaceAction.AlgorithmB:
                    break;
                case InterfaceAction.AlgorithmC:
                    break;
                case InterfaceAction.AlgorithmD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnContainerMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (_currentAction)
            {
                case InterfaceAction.VertexEdit:
                    StopMovingVertex();
                    break;
                case InterfaceAction.EdgeEdit:
                    break;
                case InterfaceAction.AlgorithmA:
                    break;
                case InterfaceAction.AlgorithmB:
                    break;
                case InterfaceAction.AlgorithmC:
                    break;
                case InterfaceAction.AlgorithmD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия правой кнопкой мыши на вершину.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEllipseMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_currentAction)
            {
                case InterfaceAction.VertexEdit:
                    var ellipse = (VertexControl)sender;
                    RemoveVertexIncidentEdges(ellipse);
                    RemoveVertex(ellipse);
                    break;
                case InterfaceAction.EdgeEdit:
                    break;
                case InterfaceAction.AlgorithmA:
                    break;
                case InterfaceAction.AlgorithmB:
                    break;
                case InterfaceAction.AlgorithmC:
                    break;
                case InterfaceAction.AlgorithmD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия правой кнопкой мыши на ребро.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLineMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_currentAction)
            {
                case InterfaceAction.VertexEdit:
                    break;
                case InterfaceAction.EdgeEdit:
                    if (_currentCreateEdgeActionState == CreateEdgeActionState.SelectSecondVertex)
                    {
                        AbortCreateEdgeAction();
                        break;
                    }
                    RemoveEdge((Line)sender);
                    break;
                case InterfaceAction.AlgorithmA:
                    break;
                case InterfaceAction.AlgorithmB:
                    break;
                case InterfaceAction.AlgorithmC:
                    break;
                case InterfaceAction.AlgorithmD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ImportGraph(string filePath)
        {
            var matrix = _graph.GetAdjacencyMatrix();
            
            using (var writer = new StreamWriter(filePath))
            {
                for (var i = 0; i < _graph.Vertices.Length; i++)
                {
                    for (var j = 0; j < _graph.Vertices.Length; j++)
                    {
                        writer.Write(matrix[i, j]);
                        writer.Write(" ");
                    }
                    writer.Write("\n");
                }

                foreach (var vertex in _graph.Vertices)
                {
                    var control = _vertices[vertex];
                    writer.WriteLine($"{Canvas.GetLeft(control)} {Canvas.GetTop(control)}");
                }
            }
        }

        private void ExportGraph(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                try
                {
                    var line = reader.ReadLine();
                    var weights = line.Split();
                    var vertexCount = weights.Length;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Неверный формат файла", "Error");
                }
                ClearStructures();
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия на кнопку тулбара.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolbarButtonClick(object sender, EventArgs e)
        {
            foreach (var toolbarChild in Toolbar.Children)
                ((Button) toolbarChild).Tag = null;
            var button = (Button)sender;
            button.Tag = "Selected";
            if (Equals(sender, CreateVertexButton))
                _currentAction = InterfaceAction.VertexEdit;
            if (Equals(sender, CreateEdgeButton))
                _currentAction = InterfaceAction.EdgeEdit;
            // TODO: алгоритмы
            if (Equals(sender, ImportButton))
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Graph Files (*.gr)|*.gr",
                    DefaultExt = "gr",
                    FileName = "MyGraph",
                    AddExtension = true
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    ImportGraph(saveFileDialog.FileName);
                }
            }

            if (Equals(sender, ExportButton))
            {
                // TODO: спросить прежде чем удалять
                var saveFileDialog = new OpenFileDialog
                {
                    Filter = "Graph Files (*.gr)|*.gr",
                    DefaultExt = "gr",
                    AddExtension = true
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    var filePath = saveFileDialog.FileName;
                }
            }
        }
    }
}
