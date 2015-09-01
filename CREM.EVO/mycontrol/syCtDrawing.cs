using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;

namespace WpfEToolkits.ShapeDrawings
{
    public class syCtDrawing
    {

        public syCtDrawing()
        {
            
        }

        public void LoadXWide()
        {

        }

        public delegate void deleMouseEvents(MouseEventArgs e);
        public event deleMouseEvents EventMouseOver;
        public List<syPoRelation> MainRelation = new List<syPoRelation>();

        public List<Point> List_PoChildToPoint(List<syPoChild> syPoChilds, syPoDrawing refCurrPoDrawing)
        {
            List<Point> rePoints = new List<Point>();


            foreach (syPoChild spc in syPoChilds)
            {
                //Math.Round
                syPoRelation spr = new syPoRelation();
                spr.SyGuid = System.Guid.NewGuid().ToString();

                Point p_temp = PoChildToPoint(spc, refCurrPoDrawing);
                spr.SyPoint = p_temp;
                spr.SyChild = spc;
                MainRelation.Add(spr);
                rePoints.Add(p_temp); 
            }

            if (rePoints.Count > 0)
                return rePoints;
            else
                return null;

        }

        public Point PoChildToPoint(syPoChild syChild, syPoDrawing refCurrPoDrawing)
        {
            //Point p=new Point();
            double xWide = refCurrPoDrawing.SyXWide;
            double yWide = refCurrPoDrawing.SyYWide;
            double DanWeiX = (double)refCurrPoDrawing.SyXValue[1] - (double)refCurrPoDrawing.SyXValue[0];
            double DanWeiY = (double)refCurrPoDrawing.SyYValue[1] - (double)refCurrPoDrawing.SyYValue[0];
            double NxWide = xWide / DanWeiX;
            double NyWide = yWide / DanWeiY;

            double syX = Math.Round(syChild.SyX, 4);
            double syY = Math.Round(syChild.SyY, 4);

            int ZhengX = (int)Math.Round(syX, 0);
            int ZhengY = (int)Math.Round(syY, 0);

            int Tx = 0;
            int Ty = 0;

            for (int i = 0; i < refCurrPoDrawing.SyXValue.Count - 1; i++)
            {

                if ((double)refCurrPoDrawing.SyXValue[i] <= ZhengX && ZhengX <= (double)refCurrPoDrawing.SyXValue[i + 1])
                {
                    Tx = i +1;
                    break;
                }
            }
            for (int i = 0; i < refCurrPoDrawing.SyYValue.Count - 1; i++)
            {
                if ((double)refCurrPoDrawing.SyYValue[i] <= ZhengY && ZhengY <= (double)refCurrPoDrawing.SyYValue[i + 1])
                {
                    Ty = i +1 ;
                    break;
                }
            }

            
            double LingX = 0;
            try
            {
                LingX = syX - (double)refCurrPoDrawing.SyXValue[Tx - 1];
            }
            catch { }
            double HouJX = Math.Round( NxWide * LingX,4);

            
            double LingY = 0;
            try
            {
                LingY = syY - (double)refCurrPoDrawing.SyYValue[Ty - 1];
            }
            catch { }
            double HouJY = Math.Round(NyWide * LingY,4);

            //y是反数
            double TeY = refCurrPoDrawing.SyYValue.Count - Ty;


            double QuX = Tx * xWide;
            double QuY = TeY * yWide;

            double ZuoX = QuX + HouJX;
            double ZuoY = QuY - HouJY;

            ZuoY += refCurrPoDrawing.SyYMarin;
            ZuoX += refCurrPoDrawing.SyXMarin;

            ZuoX -= xWide;
            ZuoY += yWide;

            return new Point(ZuoX, ZuoY);
        }

        public void DrawingText(string strText, TextBlock tbStyle, Point p, Canvas DesignerPanel)
        {
            TextBlock tb = new TextBlock();
            tb.Text = strText;
            tb.Foreground = tbStyle.Foreground;

            Canvas.SetLeft(tb, p.X);
            Canvas.SetTop(tb, p.Y);
            DesignerPanel.Children.Add(tb);

        }

        void DrawingPoint(List<Point> Points, Canvas DesignerPanel, Ellipse refEllipse)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Ellipse ea = new Ellipse();
                ea.Visibility = refEllipse.Visibility;
                ea.Stroke = refEllipse.Stroke;
                ea.StrokeThickness = refEllipse.StrokeThickness;
                ea.Fill = refEllipse.Fill;
                ea.Opacity = refEllipse.Opacity;
                ea.Width = refEllipse.Width;
                ea.Height = refEllipse.Height;
                ea.Cursor = Cursors.Hand;
                foreach (syPoRelation spr in MainRelation)
                {
                    if (spr.SyPoint == Points[i])
                    {
                        ea.Tag = spr.SyGuid;
                        break;
                    }

                }
                ea.MouseEnter += new MouseEventHandler(ea_MouseEnter);
                ea.MouseLeave += new MouseEventHandler(ea_MouseLeave);
                Canvas.SetLeft(ea, ((Point)Points[i]).X - ea.Width / 2);
                Canvas.SetTop(ea, ((Point)Points[i]).Y - ea.Height / 2);
                Canvas.SetZIndex(ea, int.MaxValue - 1);
                DesignerPanel.Children.Add(ea);
            }
        }

        void ea_MouseEnter(object sender, MouseEventArgs e)
        {
            EventMouseOver(e);
        }

        void ea_MouseLeave(object sender, MouseEventArgs e)
        {
            EventMouseOver(null);
        }

        void DrawingLine(List<Point> LinesPoint, Canvas DesignerPanel, Path refPath)
        {
            for (int i = 0; i < LinesPoint.Count - 1; i++)
            {
                LineGeometry lg = new LineGeometry();
                try
                {
                    lg = new LineGeometry((Point)LinesPoint[i], (Point)LinesPoint[i + 1]);
                }
                catch { }
                Path p = new Path();
                p.Stroke = refPath.Stroke;
                p.StrokeThickness = refPath.StrokeThickness;
                p.Data = lg;
                DesignerPanel.Children.Add(p);
            }
        }

        public void DrawingLine(List<Point> LinePoint, Canvas DesignerPanel, Ellipse refEllipse, Path refPath)
        {
            DrawingPoint(LinePoint, DesignerPanel, refEllipse);

            DrawingLine(LinePoint, DesignerPanel, refPath);


        }
    }
}
