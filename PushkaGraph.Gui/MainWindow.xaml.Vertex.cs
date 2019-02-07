using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using PushkaGraph.Core;

namespace PushkaGraph.Gui
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<Ellipse, Vertex> _ellipses;
        private readonly Dictionary<Vertex, Ellipse> _vertices;

        private bool _isVertexMoving;
        private Ellipse _movingEllipse;

        private void RemoveVertex(Ellipse ellipse)
        {
            var vertex = _ellipses[ellipse];
            _vertices.Remove(vertex);
            _ellipses.Remove(ellipse);
            Container.Children.Remove(ellipse);
            _graph.DeleteVertex(vertex);
        }

        private void UpdateMovingVertexPosition(Point cursorPosition)
        {
            Canvas.SetTop(_movingEllipse, cursorPosition.Y - VertexSettings.Size / 2);
            Canvas.SetLeft(_movingEllipse, cursorPosition.X - VertexSettings.Size / 2);
        }

        private void StartMovingVertex(Ellipse ellipse)
        {
            _isVertexMoving = true;
            Panel.SetZIndex(ellipse, VertexSettings.MovingZIndex);
            _movingEllipse = ellipse;
        }

        private void StopMovingVertex()
        {
            _isVertexMoving = false;
            if (_movingEllipse != null)
                Panel.SetZIndex(_movingEllipse, VertexSettings.ZIndex);
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
            _ellipses[ellipse] = vertex;
            _vertices[vertex] = ellipse;
        }
    }
}
