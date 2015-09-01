using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections;


namespace WpfEToolkits.ShapeDrawings
{
    public class syPoDrawing
    {
        
        public syPoDrawing()
        {

        }

        private ArrayList syXName;
        private ArrayList syYName;
        public List<Point> syYPoint;
        public List<Point> syXPoint;
        private double syXWide;
        private double syYWide;
        private ArrayList syXValue;
        private ArrayList syYValue;

        private double syXMarin = 40;
        private double syYMarin = 40;

        /// <summary>
        /// 设置图纵向空白边距值,默认40
        /// </summary>
        public double SyYMarin
        {
            get { return syYMarin; }
            set { syYMarin = value; }
        }

        /// <summary>
        /// 设置图横向空白边距值，默认40
        /// </summary>
        public double SyXMarin
        {
            get { return syXMarin; }
            set { syXMarin = value; }
        }

        /// <summary>
        /// 每Y轴的刻度值(int)
        /// </summary>
        public ArrayList SyYValue
        {
            get { return syYValue; }
            set { syYValue = value; }
        }

        /// <summary>
        /// 每X轴的刻度值(int)
        /// </summary>
        public ArrayList SyXValue
        {
            get { return syXValue; }
            set { syXValue = value; }
        }

        /// <summary>
        /// 每Y刻度size(double)
        /// </summary>
        public double SyYWide
        {
            get { return syYWide; }
            set { syYWide = value; }
        }

        /// <summary>
        /// 每X刻度Size(double)
        /// </summary>
        public double SyXWide
        {
            get { return syXWide; }
            set { syXWide = value; }
        }

        /// <summary>
        /// 每Y轴的名字(string)
        /// </summary>
        public ArrayList SyYName
        {
            get { return syYName; }
            set { syYName = value; }
        }

        /// <summary>
        /// 每X轴的名字(string)
        /// </summary>
        public ArrayList SyXName
        {
            get { return syXName; }
            set { syXName = value; }
        }

    }

    /// <summary>
    /// 关系类
    /// </summary>
    public class syPoRelation
    {
        private Point syPoint;
        private syPoChild syChild;
        private string syGuid;

        public string SyGuid
        {
            get { return syGuid; }
            set { syGuid = value; }
        }

        public syPoChild SyChild
        {
            get { return syChild; }
            set { syChild = value; }
        }

        public Point SyPoint
        {
            get { return syPoint; }
            set { syPoint = value; }
        }

        public syPoRelation() { }
        public syPoRelation(syPoChild child, Point p)
        {
            syChild = child;
            syPoint = p;
            syGuid = System.Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// 子项，可改
    /// </summary>
    public class syPoChild
    {
        public syPoChild() { }
        public syPoChild(double x, double y)
        {
            syX = x; syY = y;

        }
        private double syX;

        public double SyX
        {
            get { return syX; }
            set { syX = value; }
        }
        private double syY;

        public double SyY
        {
            get { return syY; }
            set { syY = value; }
        }


    }
}
