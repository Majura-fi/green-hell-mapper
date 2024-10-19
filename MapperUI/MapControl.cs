using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
    private bool isMouseOver = false;

    private readonly static Font font = new(new FontFamily("Times New Roman"), 12, FontStyle.Regular, GraphicsUnit.Pixel);

    public MapControl()
    {
        InitializeComponent();
        DoubleBuffered = true;

        MouseWheel += OnMouseWheel;
        MouseDown += OnMouseDown;
        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;
        MouseEnter += OnMouseEnter;
        MouseLeave += OnMouseLeave;

        offset = new(0, 0);
    }

    private void OnMouseLeave(object? sender, EventArgs e)
    {
        isMouseOver = false;
    }

    private void OnMouseEnter(object? sender, EventArgs e)
    {
        isMouseOver = true;
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
        if (isDraggingMap)
        {
            Point delta = new(
                e.Location.X - previousMousePosition.X,
                e.Location.Y - previousMousePosition.Y
            );
        
            offset.X += delta.X / currentZoom;
            offset.Y += delta.Y / currentZoom;

            previousMousePosition = e.Location;
            Invalidate();
        } 
        else if (isMouseOver)
        {
            Invalidate();
        }
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

        if (DrawCurrentLocations)
        {
            PlayerInfo[] tmpLatestInfos;
            lock (latestLocations)
            {
                tmpLatestInfos = latestLocations.Select(p => p.Value).ToArray();
            }

            // Draw current player locations.
            foreach (PlayerInfo info in tmpLatestInfos)
            {
                DrawLocation(g, info);
                DrawViewCone(g, info);
            }
        }

    }

    private static void DrawViewCone(Graphics g, PlayerInfo info)
    {
        float angle = MathF.Atan2(info.MapForward.X, info.MapForward.Y);

        PointF[] points = VectorsToPointArray(
            info.MapLocation,
            // Left edge
            MathUtils.MoveAlongAngle(info.MapLocation, angle - MathUtils.Deg2Rad(45f), 100f),
            // Right edge
            MathUtils.MoveAlongAngle(info.MapLocation, angle + MathUtils.Deg2Rad(45f), 100f)
        );

        using GraphicsPath path = new();
        path.AddPolygon(points);

        using PathGradientBrush viewConeBrush = new(path);
        viewConeBrush.CenterPoint = points[0];
        viewConeBrush.CenterColor = Color.FromArgb(127, 255, 255, 0);
        viewConeBrush.SurroundColors = [ Color.FromArgb(0, 255, 255, 0) ];

        g.FillPath(viewConeBrush, path);
    }

    private static PointF[] VectorsToPointArray(params Vector2[] vectors)
    {
        return vectors.Select(v => new PointF(v.X, v.Y)).ToArray();
    }


    private void DrawLocation(Graphics g, PlayerInfo info)
    {
        RectangleF point = new(
                            info.MapLocation.X - PointSize * 2f,
                            info.MapLocation.Y - PointSize * 2f,
                            PointSize * 4f,
                            PointSize * 4f
                        );
        g.FillEllipse(Brushes.Red, point);

        point = new(
            info.MapLocation.X - 20f,
            info.MapLocation.Y - 20f,
            40f,
            40f
        );
        g.DrawEllipse(RedPen, point);
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
