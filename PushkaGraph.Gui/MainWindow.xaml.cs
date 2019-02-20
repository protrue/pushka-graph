using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using PushkaGraph.Core;
using PushkaGraph.NewAlgorithms;
using PushkaGraph.NewAlgorithms.Wrapper;
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
        private readonly Graph _graph;
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
            InitializeAlgorithmButtons();
        }

        public string GetStringResource(string name) => GuiResources.ResourceManager.GetString(name);

        private void ColorizeEdges(IEnumerable<Edge> edges, Brush brush)
        {
            foreach (var edge in edges)
            {
                _edges[edge].Fill = brush;
            }
        }

        private void ColorizeVertices(IEnumerable<Vertex> vertices, Brush brush)
        {
            foreach (var vertex in vertices)
            {
                _vertices[vertex].Fill = brush;
            }
        }

        // TODO: алгоритм запускается в отдельном потоке, надо предусмотреть блокировку формы
        private void InitializeAlgorithmButtons()
        {
            var algorithms = GraphAlgorithmFactory.CreateAllGraphAlgorithms();
            var i = 1;
            foreach (var algorithm in algorithms)
            {
                algorithm.Performed += result =>
                {
                    MessageBox.Show(result.Number.HasValue ? result.Number.ToString() : result.StringResult,
                        "Результат");
                    if (result.Edges != null)
                        ColorizeEdges(result.Edges, Brushes.Blue);
                    if (result.Vertices != null)
                        ColorizeVertices(result.Vertices, Brushes.Red);
                };
                var button = new Button
                {
                    Style = FindResource("ToolbarButton") as Style,
                    Content = $"A{i++}",
                    ToolTip = algorithm.Name
                };
                button.Click += (sender, args) =>
                {
                    ColorizeEdges(_graph.Edges, Brushes.Black);
                    ColorizeVertices(_graph.Vertices, Brushes.White);
                    var parameters = new GraphAlgorithmParameters(_graph);
                    algorithm.PerformAlgorithmAsync(parameters);
                };
                Toolbar.Children.Add(button);
            }
        }

        private void CleanStructures(bool cleanGraph = false)
        {
            if (cleanGraph)
                _graph.CleanVertices();
            Container.Children.Clear();
            _ellipses.Clear();
            _vertices.Clear();
            _edges.Clear();
            _lines.Clear();
            _edgeWeightMapping.Clear();
            _weightEdgeMapping.Clear();
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // TODO: рефакторинг
        private void ExportGraph(string filePath)
        {
            var matrix = _graph.GetAdjacencyMatrix();
            
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(_graph.Vertices.Length);
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

        // TODO: рефакторинг
        private void ImportGraph(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                try
                {
                    var line = reader.ReadLine();
                    if (!int.TryParse(line, out var vertexCount))
                        throw new ArgumentException(GetStringResource("ImportFormatError"));

                    var matrix = new int[vertexCount, vertexCount];

                    for (var i = 0; i < vertexCount; i++)
                    {
                        line = reader.ReadLine();
                        if (line == null)
                            throw new ArgumentException(GetStringResource("ImportFormatError"));
                        var matrixRow = line.Trim().Split().Select(int.Parse).ToArray();
                        for (var j = 0; j < vertexCount; j++)
                            matrix[i, j] = matrixRow[j];
                    }

                    _graph.CreateFromAdjacencyMatrix(matrix);

                    if (MessageBox.Show(GetStringResource("RewriteFileWarning"), GetStringResource("WarningMessageBoxTitle"),
                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        return;

                    // Если граф создался и все ОК, то можно удалить старые контролы.
                    CleanStructures();

                    for (var i = 0; i < vertexCount; i++)
                    {
                        line = reader.ReadLine();
                        if (line == null)
                            throw new ArgumentException(GetStringResource("ImportFormatError"));
                        var point = Point.Parse(line.Trim());
                        CreateVertexControl(_graph.Vertices[i], point);
                    }

                    foreach (var edge in _graph.Edges)
                        CreateEdgeControl(edge);
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, GetStringResource("ErrorMessageBoxTitle"));
                }
                catch (Exception)
                {
                    MessageBox.Show(GetStringResource("ImportFormatError"), GetStringResource("ErrorMessageBoxTitle"));
                }
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия на кнопку тулбара.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolbarButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (Equals(sender, CreateVertexButton))
            {
                _currentAction = InterfaceAction.VertexEdit;
                CreateEdgeButton.Tag = null;
                button.Tag = "Selected";
            }

            if (Equals(sender, CreateEdgeButton))
            {
                _currentAction = InterfaceAction.EdgeEdit;
                CreateVertexButton.Tag = null;
                button.Tag = "Selected";
            }

            if (Equals(sender, ExportButton))
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Graph Files (*.gr)|*.gr",
                    DefaultExt = "gr",
                    FileName = "MyGraph",
                    AddExtension = true
                };
                if (saveFileDialog.ShowDialog() == true)
                    ExportGraph(saveFileDialog.FileName);
            }

            if (Equals(sender, ImportButton))
            {
                var saveFileDialog = new OpenFileDialog
                {
                    Filter = "Graph Files (*.gr)|*.gr",
                    DefaultExt = "gr",
                    AddExtension = true
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    var filePath = saveFileDialog.FileName;
                    ImportGraph(filePath);
                }
            }

            if (Equals(sender, CleanButton))
                CleanStructures(true);
        }
    }
}
