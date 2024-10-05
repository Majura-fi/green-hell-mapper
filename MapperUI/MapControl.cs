using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapperUI.Properties;

namespace MapperUI;
public partial class MapControl : UserControl
{
    public bool DrawCurrentLocations { get; set; } = true;
    public int PointSize { get; set; } = 3;

    private Bitmap map = (Bitmap)Resources.map.Clone();

    private float currentZoom = 1f;
    private readonly float zoomStep = 0.1f;
    private readonly float zoomMin = 0.2f;
    private readonly float zoomMax = 4f;

    private static readonly Pen RedPen = new(Color.Red, 5f);
    private static readonly float RedPenWidth = 2.5f;

    private readonly Dictionary<int, PlayerInfo> latestLocations = [];

    private PointF offset;


    private Point previousMousePosition = Point.Empty;
    private bool isDraggingMap = false;


    public MapControl()
    {
        InitializeComponent();
        DoubleBuffered = true;

        MouseWheel += OnMouseWheel;
        MouseDown += OnMouseDown;
        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;

        offset = new(0, 0);
    }

    private void OnMouseWheel(object? sender, MouseEventArgs e)
    {
        float zoomFactor = (e.Delta > 0) ? 1.25f : 0.75f;
        float oldZoom = currentZoom;

        currentZoom *= zoomFactor;
        currentZoom = MathF.Max(zoomMin, MathF.Min(zoomMax, currentZoom));

        PointF oldPos = new(e.X / oldZoom, e.Y / oldZoom);
        PointF newPos = new(e.X / currentZoom, e.Y / currentZoom);

        offset.X += newPos.X - oldPos.X;
        offset.Y += newPos.Y - oldPos.Y;

        Invalidate();
    }

    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDraggingMap = true;
            previousMousePosition = e.Location;
        }
    }

    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        if (!isDraggingMap)
        {
            return;
        }

        Point delta = new(
            e.Location.X - previousMousePosition.X,
            e.Location.Y - previousMousePosition.Y
        );
        
        offset.X += delta.X / currentZoom;
        offset.Y += delta.Y / currentZoom;

        previousMousePosition = e.Location;
        Invalidate();
    }

    private void OnMouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDraggingMap = false;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.ScaleTransform(currentZoom, currentZoom);
        g.TranslateTransform(offset.X, offset.Y);
        g.DrawImage(map, 0, 0, map.Width, map.Height);

        g.DrawLine(
            RedPen, 
            PointF.Empty, 
            new PointF(
                map.Width * 0.5f,
                map.Height * 0.5f
            )
        );

        if (DrawCurrentLocations)
        {
            Vector3[] tmpLatestLocations;
            lock (latestLocations)
            {
                tmpLatestLocations = latestLocations
                    .Select(i => i.Value.MapLocation)
                    .ToArray();
            }

            // Draw current player locations.
            foreach (Vector3 location in tmpLatestLocations)
            {
                RectangleF point = new(
                    (location.X - PointSize * 2f), 
                    (location.Y - PointSize * 2f), 
                    PointSize * 4f, 
                    PointSize * 4f
                );
                g.FillEllipse(Brushes.Red, point);

                point = new(
                    (location.X - 20f), 
                    (location.Y - 20f), 
                    40f, 
                    40f);
                g.DrawEllipse(RedPen, point);
            }
        }
    }

    /// <summary>
    /// Draws location history directly on the map. 
    /// These locations cannot be changed later.
    /// </summary>
    /// <param name="info">Player info with a new location.</param>
    /// <param name="updateLatestLocation">Should the location be considered as a latest location.</param>
    public void DrawMark(PlayerInfo info, bool updateLatestLocation = false)
    {
        if (updateLatestLocation && !latestLocations.TryAdd(info.PlayerId, info))
        {
            latestLocations[info.PlayerId] = info;
        }

        using Graphics g = Graphics.FromImage(map);

        RectangleF point = new(
            info.MapLocation.X - PointSize, 
            info.MapLocation.Y - PointSize, 
            PointSize * 2f, 
            PointSize * 2f
        );

        g.FillEllipse(Brushes.Blue, point);
        Invalidate();
    }
}
