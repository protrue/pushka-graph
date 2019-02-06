using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using PushkaGraphCore;

namespace PushkaGraphGUI
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
        private readonly Dictionary<Ellipse, Vertex> _vertices;
        private readonly Dictionary<Edge, Line> _edges;
        private InterfaceAction _currentAction;
        private CreateEdgeActionState _currentCreateEdgeActionState;

        private Ellipse _edgeStart;
        private Ellipse _edgeFinish;
        private Line _movingLine;

        private bool _isVertexMoving;
        private Ellipse _movingVertex;

        private const string SelectedTag = "Selected";

        public MainWindow()
        {
            InitializeComponent();
            _graph = new Graph();
            _vertices = new Dictionary<Ellipse, Vertex>();
            _edges = new Dictionary<Edge, Line>();
            _currentAction = InterfaceAction.CreateVertex;
            CreateVertexButton.Tag = SelectedTag;
            _currentCreateEdgeActionState = CreateEdgeActionState.SelectFirstVertex;
        }

        /// <summary>
        /// Обрабатывает событие нажатия на поле.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContainerClick(object sender, MouseButtonEventArgs e)
        {
            switch (_currentAction)
            {
                case InterfaceAction.CreateVertex:
                    if (_isVertexMoving) break;
                    CreateVertex(sender, e);
                    break;
                case InterfaceAction.CreateEdge:
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
            var ellipse = (Ellipse) sender;
            switch (_currentAction)
            {
                case InterfaceAction.CreateVertex:
                    _isVertexMoving = true;
                    Panel.SetZIndex(ellipse, VertexSettings.MovingZIndex);
                    _movingVertex = ellipse;
                    break;
                case InterfaceAction.CreateEdge:
                    CreateEdge(sender, e);
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
        /// Обрабатывает событие поднятия левой кнопки мыши над вершиной.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEllipseMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var ellipse = (Ellipse)sender;
            switch (_currentAction)
            {
                case InterfaceAction.CreateVertex:
                    _isVertexMoving = false;
                    Panel.SetZIndex(ellipse, VertexSettings.ZIndex);
                    break;
                case InterfaceAction.CreateEdge:
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
                case InterfaceAction.CreateVertex:
                    if (!_isVertexMoving) break;
                    foreach (var edge in _vertices[_movingVertex].IncidentEdges)
                    {
                        var line = _edges[edge];
                        if (Math.Abs(line.X1 - (Canvas.GetLeft(_movingVertex) + VertexSettings.Size / 2)) < 0.001 &&
                            Math.Abs(line.Y1 - (Canvas.GetTop(_movingVertex) + VertexSettings.Size / 2)) < 0.001)
                        {
                            line.X1 = cursorPosition.X;
                            line.Y1 = cursorPosition.Y;
                        }
                        if (Math.Abs(line.X2 - (Canvas.GetLeft(_movingVertex) + VertexSettings.Size / 2)) < 0.001 &&
                            Math.Abs(line.Y2 - (Canvas.GetTop(_movingVertex) + VertexSettings.Size / 2)) < 0.001)
                        {
                            line.X2 = cursorPosition.X;
                            line.Y2 = cursorPosition.Y;
                        }
                    }

                    Canvas.SetTop(_movingVertex, cursorPosition.Y - VertexSettings.Size / 2);
                    Canvas.SetLeft(_movingVertex, cursorPosition.X - VertexSettings.Size / 2);
                    break;
                case InterfaceAction.CreateEdge:
                    if (_currentCreateEdgeActionState == CreateEdgeActionState.SelectSecondVertex)
                    {
                        _movingLine.X2 = cursorPosition.X;
                        _movingLine.Y2 = cursorPosition.Y;
                    }
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
        /// Создает контрол ребра с требуемыми параметрами.
        /// </summary>
        /// <param name="start">Начальная вершина.</param>
        /// <param name="finish">Конечная вершина.</param>
        /// <returns></returns>
        private Line InitializeLine(Point start, Point finish)
        {
            var line = new Line
            {
                Style = FindResource("Edge") as Style,
                X1 = start.X,
                Y1 = start.Y,
                X2 = finish.X,
                Y2 = finish.Y
            };

            return line;
        }

        private void OnEllipseMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ellipse = (Ellipse)sender;
            switch (_currentAction)
            {
                case InterfaceAction.CreateVertex:
                    Container.Children.Remove(ellipse);
                    var vertex = _vertices[ellipse];

                    foreach (var edge in vertex.IncidentEdges)
                    {
                        var line = _edges[edge];
                        Container.Children.Remove(line);
                    }

                    _graph.DeleteVertex(vertex);
                    break;
                case InterfaceAction.CreateEdge:
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

        private void OnLineMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var line = (Line)sender;
            switch (_currentAction)
            {
                case InterfaceAction.CreateVertex:
                    break;
                case InterfaceAction.CreateEdge:
                    var edgeLinePair = _edges.First(x => Equals(x.Value, line));
                    Container.Children.Remove(edgeLinePair.Value);
                    _graph.DeleteEdge(edgeLinePair.Key);
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
        /// Создает контрол вершины с требуемыми параметрами.
        /// </summary>
        /// <param name="point">Координаты вершины.</param>
        /// <returns></returns>
        private Ellipse InitializeEllipse(Point point)
        {
            var ellipse = new Ellipse
            {
                Style = FindResource("Vertex") as Style,
                Width = VertexSettings.Size,
                Height = VertexSettings.Size,
            };
            Panel.SetZIndex(ellipse, VertexSettings.ZIndex);
            Canvas.SetTop(ellipse, point.Y);
            Canvas.SetLeft(ellipse, point.X);

            return ellipse;
        }

        /// <summary>
        /// Обрабатывает событие нажатия на поле, при создании новой вершины.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateVertex(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition((Canvas)sender);
            point.Y -= VertexSettings.Size / 2;
            point.X -= VertexSettings.Size / 2;

            var ellipse = InitializeEllipse(point);
            Container.Children.Add(ellipse);

            var vertex = _graph.AddVertex();
            _vertices[ellipse] = vertex;
        }

        /// <summary>
        /// Обрабатывает событие нажатия на вершину, при добавлении нового ребра.
        /// </summary>
        private void CreateEdge(object sender, MouseEventArgs e)
        {
            var ellipse = (Ellipse) sender;
            switch (_currentCreateEdgeActionState)
            {
                case CreateEdgeActionState.SelectFirstVertex:
                    _edgeStart = ellipse;
                    var startPointX = Canvas.GetLeft(_edgeStart) + VertexSettings.Size / 2;
                    var startPointY = Canvas.GetTop(_edgeStart) + VertexSettings.Size / 2;
                    var finishPointX = e.GetPosition(Container).X + VertexSettings.Size / 2;
                    var finishPointY = e.GetPosition(Container).Y + VertexSettings.Size / 2;

                    var startPoint = new Point(startPointX, startPointY);
                    var endPoint = new Point(finishPointX, finishPointY);

                    _movingLine = InitializeLine(startPoint, endPoint);
                    Container.Children.Add(_movingLine);

                    _currentCreateEdgeActionState = CreateEdgeActionState.SelectSecondVertex;
                    break;
                case CreateEdgeActionState.SelectSecondVertex:
                    _edgeFinish = ellipse;
                    if (_vertices[_edgeStart].AdjacentVertices.Contains(_vertices[_edgeFinish]))
                    {
                        Container.Children.Remove(_movingLine);
                        _currentCreateEdgeActionState = CreateEdgeActionState.SelectFirstVertex;
                        break;
                    }
                    var finishX = Canvas.GetLeft(_edgeFinish) + VertexSettings.Size / 2;
                    var finishY = Canvas.GetTop(_edgeFinish) + VertexSettings.Size / 2;

                    _movingLine.X2 = finishX;
                    _movingLine.Y2 = finishY;
                    
                    var edge = _graph.AddEdge(_vertices[_edgeStart], _vertices[_edgeFinish]);
                    _edges[edge] = _movingLine;
                    _currentCreateEdgeActionState = CreateEdgeActionState.SelectFirstVertex;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
                _currentAction = InterfaceAction.CreateVertex;
            if (Equals(sender, CreateEdgeButton))
                _currentAction = InterfaceAction.CreateEdge;
            // TODO: алгоритмы
        }
    }
}
