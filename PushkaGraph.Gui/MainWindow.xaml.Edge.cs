using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PushkaGraph.Core;

namespace PushkaGraph.Gui
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<Edge, Line> _edges;
        private readonly Dictionary<Line, Edge> _lines;
        private readonly Dictionary<Edge, TextBox> _edgeWeightMapping;
        private readonly Dictionary<TextBox, Edge> _weightEdgeMapping;
        private CreateEdgeActionState _currentCreateEdgeActionState;
        private VertexControl _edgeStart;
        private VertexControl _edgeFinish;
        private Line _movingLine;

        private void AbortCreateEdgeAction()
        {
            _currentCreateEdgeActionState = CreateEdgeActionState.SelectFirstVertex;
            Container.Children.Remove(_movingLine);
        }
        
        private void RemoveEdge(Line line)
        {
            var edge = _lines[line];
            var weight = _edgeWeightMapping[edge];
            _edges.Remove(edge);
            _lines.Remove(line);
            _edgeWeightMapping.Remove(edge);
            _weightEdgeMapping.Remove(weight);
            Container.Children.Remove(line);
            Container.Children.Remove(weight);
            _graph.DeleteEdge(edge);
        }

        private void RemoveEdge(Edge edge)
        {
            RemoveEdge(_edges[edge]);
        }

        private void UpdateEdgeEndPosition(Point position)
        {
            _movingLine.X2 = position.X;
            _movingLine.Y2 = position.Y;
        }

        private void UpdateWeightTextBoxPosition(Line line)
        {
            var weight = _edgeWeightMapping[_lines[line]];
            Canvas.SetTop(weight, (line.Y1 + line.Y2) / 2 - 15);
            Canvas.SetLeft(weight, (line.X1 + line.X2) / 2 - 15);
        }

        private void UpdateEdgePosition(Edge edge)
        {
            var line = _edges[edge];
            var firstEllipse = _vertices[edge.FirstVertex];
            var secondEllipse = _vertices[edge.SecondVertex];

            line.X1 = Canvas.GetLeft(firstEllipse) + VertexSettings.Size / 2;
            line.Y1 = Canvas.GetTop(firstEllipse) + VertexSettings.Size / 2;
            line.X2 = Canvas.GetLeft(secondEllipse) + VertexSettings.Size / 2;
            line.Y2 = Canvas.GetTop(secondEllipse) + VertexSettings.Size / 2;

            UpdateWeightTextBoxPosition(line);
        }

        private void UpdateEdgesPosition(IEnumerable<Edge> edges)
        {
            foreach (var edge in edges)
                UpdateEdgePosition(edge);
        }

        private void RemoveVertexIncidentEdges(VertexControl ellipse)
        {
            foreach (var edge in _ellipses[ellipse].IncidentEdges)
                RemoveEdge(edge);
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

        /// <summary>
        /// Создает контрол для веса ребра с требуемыми параметрами.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private TextBox InitializeWeightTextBox(Line line)
        {
            var textBox = new TextBox
            {
                Style = FindResource("Weight") as Style,
                Text = _lines[line].Weight.ToString()
            };
            Panel.SetZIndex(textBox, 3);

            return textBox;
        }

        private void CreateEdgeControl(Edge edge)
        {
            var firstVertex = _vertices[edge.FirstVertex];
            var secondVertex = _vertices[edge.SecondVertex];

            var startX = Canvas.GetLeft(firstVertex) + VertexSettings.Size / 2;
            var startY = Canvas.GetTop(firstVertex) + VertexSettings.Size / 2;
            var finishX = Canvas.GetLeft(secondVertex) + VertexSettings.Size / 2;
            var finishY = Canvas.GetTop(secondVertex) + VertexSettings.Size / 2;

            var startPoint = new Point(startX, startY);
            var endPoint = new Point(finishX, finishY);

            var line = InitializeLine(startPoint, endPoint);
            Container.Children.Add(line);

            _edges[edge] = line;
            _lines[line] = edge;
            line.MouseEnter += (sender, args) => ((Line)sender).StrokeThickness = 10;
            line.MouseLeave += (sender, args) => ((Line)sender).StrokeThickness = 5;

            var weightTextBox = InitializeWeightTextBox(line);
            _edgeWeightMapping[edge] = weightTextBox;
            _weightEdgeMapping[weightTextBox] = edge;
            UpdateWeightTextBoxPosition(line);
            Container.Children.Add(weightTextBox);
        }

        /// <summary>
        /// Обрабатывает событие нажатия на вершину, при добавлении нового ребра.
        /// </summary>
        private void CreateEdge(VertexControl ellipse, Point mousePosition)
        {
            switch (_currentCreateEdgeActionState)
            {
                case CreateEdgeActionState.SelectFirstVertex:
                    _edgeStart = ellipse;
                    var startPointX = Canvas.GetLeft(_edgeStart) + VertexSettings.Size / 2;
                    var startPointY = Canvas.GetTop(_edgeStart) + VertexSettings.Size / 2;
                    var finishPointX = mousePosition.X + VertexSettings.Size / 2;
                    var finishPointY = mousePosition.Y + VertexSettings.Size / 2;

                    var startPoint = new Point(startPointX, startPointY);
                    var endPoint = new Point(finishPointX, finishPointY);

                    _movingLine = InitializeLine(startPoint, endPoint);
                    Container.Children.Add(_movingLine);

                    _currentCreateEdgeActionState = CreateEdgeActionState.SelectSecondVertex;
                    break;
                case CreateEdgeActionState.SelectSecondVertex:
                    _edgeFinish = ellipse;
                    if (Equals(_edgeFinish, _edgeStart) ||
                        _ellipses[_edgeStart].AdjacentVertices.Contains(_ellipses[_edgeFinish]))
                    {
                        break;
                    }
                    var finishX = Canvas.GetLeft(_edgeFinish) + VertexSettings.Size / 2;
                    var finishY = Canvas.GetTop(_edgeFinish) + VertexSettings.Size / 2;

                    _movingLine.X2 = finishX;
                    _movingLine.Y2 = finishY;

                    var edge = _graph.AddEdge(_ellipses[_edgeStart], _ellipses[_edgeFinish]);
                    _edges[edge] = _movingLine;
                    _lines[_movingLine] = edge;
                    _movingLine.MouseEnter += (sender, args) => ((Line) sender).StrokeThickness = 10;
                    _movingLine.MouseLeave += (sender, args) => ((Line)sender).StrokeThickness = 5;

                    var weightTextBox = InitializeWeightTextBox(_movingLine);
                    _edgeWeightMapping[edge] = weightTextBox;
                    _weightEdgeMapping[weightTextBox] = edge;
                    UpdateWeightTextBoxPosition(_movingLine);
                    Container.Children.Add(weightTextBox);

                    _currentCreateEdgeActionState = CreateEdgeActionState.SelectFirstVertex;
                    _movingLine = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnWeightTextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            var currentCaretIndex = textBox.CaretIndex - 1;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = 1.ToString();
                textBox.CaretIndex = textBox.Text.Length;
            }
            textBox.Text = textBox.Text.Trim(' ');
            textBox.Text = textBox.Text.TrimStart('0');
            var isNumeric = int.TryParse(textBox.Text, out var number);
            if (isNumeric && _weightEdgeMapping.ContainsKey(textBox))
                _weightEdgeMapping[textBox].Weight = number;
            if (!isNumeric)
            {
                textBox.Text = _weightEdgeMapping[textBox].Weight.ToString();
                textBox.CaretIndex = currentCaretIndex;
            }
        }
    }
}
