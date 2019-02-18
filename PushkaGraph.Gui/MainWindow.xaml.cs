using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using PushkaGraph.Core;

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
        }
    }
}
