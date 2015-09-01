using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.ComponentModel;
using WpfEToolkits.StructUIs;


namespace WpfEToolkits.ShapeDrawings
{
    /// <summary>
    /// ShapeDrawing.xaml 的交互逻辑
    /// </summary>
    public partial class ShapeDrawing : UserControl
    {


        public ShapeDrawing()
        {
            InitializeComponent();
        }

        List<UIElement> NewClearTemplate = new List<UIElement>();

        syCtStruct scs = new syCtStruct();

        /// <summary>
        /// 当前控件的线
        /// </summary>
        public Path CurrPath = new Path();
        /// <summary>
        /// 当前控件中的点
        /// </summary>
        public Ellipse CurrEllipse = new Ellipse();

        /// <summary>
        /// 当前控件画线的点
        /// </summary>
        public Ellipse CurrDrawEllipse = new Ellipse();
        /// <summary>
        /// 当前控件的许多调用方法
        /// </summary>
        public syCtDrawing CurrCtDrawing = new syCtDrawing();
        /// <summary>
        /// 当前控件的许多参数
        /// </summary>
        public syPoDrawing CurrPoDrawing = new syPoDrawing();
        /// <summary>
        /// 设置全图文字的颜色
        /// </summary>
        public TextBlock CurrTextBlock = new TextBlock();

        /// <summary>
        /// 当前所有点的绝对关系信息
        /// </summary>
        public List<syPoRelation> CurrAllRelation = new List<syPoRelation>();

        /// <summary>
        /// 标尺和标签检查
        /// </summary>
        public bool boolScaleAndLabel = false;
        /// <summary>
        /// 设置当前ItemSource
        /// </summary>
        public List<syPoChild> ItemSource = new List<syPoChild>();

        /// <summary>
        /// 是否启用默认鼠标弹出标签
        /// </summary>
        public bool boolRunDefaultText = true;

        public delegate void eDeleMouseOver(object sender, MouseEventArgs e);
        public event eDeleMouseOver EventDeleMouser;
        public string VisibleText = "{0}是x值,{1}是y值";
        public delegate void eDeleMouseChange(syPoChild spc, Point Position, bool boolVisible);

        public eDeleMouseChange eTestDelegate;

        private void ucMain_Loaded(object sender, RoutedEventArgs e)
        {
            eTestDelegate = new eDeleMouseChange(SetTextBlock);
            //tbTemp.Background = Brushes.LightBlue;

            tbTemp.Foreground = Brushes.Red;
            //tbTemp.Width = 120;

            CurrCtDrawing.EventMouseOver += new syCtDrawing.deleMouseEvents(CurrCtDrawing_EventMouseOver);
        }

        void SetTextBlock(syPoChild spc, Point position, bool boolvisible)
        {
            if (!boolRunDefaultText) return;
            if (boolvisible)
            {
                tbTemp.Text = string.Format(VisibleText, spc.SyX.ToString(), spc.SyY.ToString());
                Canvas.SetLeft(tbTemp, position.X - tbTemp.Width / 2);
                Canvas.SetTop(tbTemp, position.Y / 2 - tbTemp.Height / 2);
                Canvas.SetZIndex(tbTemp, int.MaxValue);
                tbTemp.Visibility = Visibility.Visible;
            }
            else
            {
                tbTemp.Visibility = Visibility.Hidden;
            }
        }
        void CurrCtDrawing_EventMouseOver(MouseEventArgs e)
        {
            if (e == null)
            {
                Dispatcher.Invoke(eTestDelegate, new object[] { null, null, false });
                EventDeleMouser(null, null);
                return;
            }
            Ellipse ea = e.Source as Ellipse;

            foreach (syPoRelation spr in CurrCtDrawing.MainRelation)
            {
                if (spr.SyGuid == ea.Tag.ToString())
                {
                    Dispatcher.Invoke(eTestDelegate, new object[] { (object)spr.SyChild, (object)spr.SyPoint, true });
                    EventDeleMouser(spr, e);
                    break;
                }
            }
        }

        public void DrawingNewSource(List<syPoChild> CurrItemsource)
        {
            if (!boolScaleAndLabel) return;
            List<syPoChild> collections = (List<syPoChild>)CurrItemsource;
            ICollectionView view = CollectionViewSource.GetDefaultView(collections);
            view.SortDescriptions.Clear();
            if (view.CanSort)
            {
                view.SortDescriptions.Add(new SortDescription("SyX", ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription("SyY", ListSortDirection.Ascending));
            }
            view.MoveCurrentToFirst();
            ItemSource.Clear();
            do
            {
                ItemSource.Add(view.CurrentItem as syPoChild);
            } while (view.MoveCurrentToNext());

            List<Point> lTemp = CurrCtDrawing.List_PoChildToPoint(ItemSource, CurrPoDrawing);

            CurrCtDrawing.DrawingLine(lTemp, DesignerPane, CurrDrawEllipse, CurrPath);

        }

        /// <summary>
        /// 记录原始模型
        /// </summary>
        public void LoadClearTemplate()
        {
            foreach (UIElement u in DesignerPane.Children)
            {
                NewClearTemplate.Add(u);
            }
        }

        /// <summary>
        /// 重载后新模型
        /// </summary>
        public void ReLoadClearTemplate()
        {
            this.boolScaleAndLabel = false;
            tbTemp.Visibility = Visibility.Hidden;
            CurrCtDrawing.MainRelation = new List<syPoRelation>();
            DesignerPane.Children.Clear();
            foreach (UIElement u in NewClearTemplate)
            {
                DesignerPane.Children.Add(u);
            }
        }

        /// <summary>
        /// 构造笔刷和标尺、点的属性值，可外部直接设置
        /// </summary>
        /// <param name="t">false为外部设置，true为默认构造</param>
        public void structProperty(bool t)
        {
            if (t)
            {
                //坐标轴的点
                CurrEllipse.Visibility = Visibility.Hidden;
                CurrEllipse.Stroke = Brushes.Black;
                CurrEllipse.StrokeThickness = 1;
                CurrEllipse.Fill = Brushes.White;
                CurrEllipse.Opacity = 0.5;
                CurrEllipse.Width = 3;
                CurrEllipse.Height = 3;

                //画线的点
                CurrDrawEllipse.Visibility = Visibility.Hidden;
                CurrDrawEllipse.Stroke = Brushes.Black;
                CurrDrawEllipse.StrokeThickness = 1;
                CurrDrawEllipse.Fill = Brushes.White;
                CurrDrawEllipse.Opacity = 0.5;
                CurrDrawEllipse.Width = 3;
                CurrDrawEllipse.Height = 3;

                CurrPath.Stroke = Brushes.Black;
                CurrPath.StrokeThickness = 2;
            }
        }

        /// <summary>
        /// 构造标尺数组
        /// </summary>
        /// <param name="dmax"></param>
        /// <param name="dmin"></param>
        /// <param name="dProp"></param>
        /// <returns></returns>
        public ArrayList funStructLable(double dnum1, double dnum2, double dProp)
        {


            if (dnum1 > dnum2)
            {
                if (dnum2 < dProp && dnum2 != 0)
                    throw new Exception("数组传参有误！最小值不可低于间距");
                else if (Math.Round( dnum1 % dProp,4) != 0)
                    throw new Exception("数组传参有误！最大值必须被间距整除");
                else
                    return getDoubleArr(dnum2, dnum1, dProp);
            }
            else if (dnum2 > dnum1)
            {
                if (dnum1 < dProp && dnum1 != 0)
                    throw new Exception("数组传参有误！最小值不可低于间距");
                else if (Math.Round( dnum2 % dProp,4) != 0)
                    throw new Exception("数组传参有误！最大值必须被间距整除");
                else
                    return getDoubleArr(dnum1, dnum2, dProp);

            }
            else
                throw new Exception("数组传参有误！请检查");


        }

        private ArrayList getDoubleArr(double dMin, double dMax, double dProp)
        {
            ArrayList objs = new ArrayList();
            for (double i = dMin; i <= dMax; i += dProp)
            {
                objs.Add(i);
            }
            return objs;
        }
        /// <summary>
        /// 构造标尺图和标尺文字，并且在构造完成时对其xWide,yWide每点刻度值进行设置
        /// </summary>
        public void structScaleAndLabel()
        {
            double tuWidth = this.Width - CurrPoDrawing.SyXMarin * 2;
            CurrPoDrawing.SyXWide = tuWidth / CurrPoDrawing.SyXValue.Count;
            double tuHeight = this.Height - CurrPoDrawing.SyYMarin * 2;
            CurrPoDrawing.SyYWide = tuHeight / CurrPoDrawing.SyYValue.Count;

            List<Point> tempPoints = new List<Point>();
            //画x,y轴
            Point YZhou1 = new Point(CurrPoDrawing.SyXMarin, 0);
            tempPoints.Add(YZhou1);
            Point YZhou2 = new Point(CurrPoDrawing.SyXMarin, tuHeight + CurrPoDrawing.SyYMarin);
            tempPoints.Add(YZhou2);
            Point XZhou2 = new Point(this.Width, tuHeight + CurrPoDrawing.SyYMarin);
            tempPoints.Add(XZhou2);
            CurrCtDrawing.DrawingLine(tempPoints, DesignerPane, CurrEllipse, CurrPath);

            //画x坐标点
            for (int i = 0; i < CurrPoDrawing.SyXName.Count; i++)
            {
                //画X轴坐标
                List<Point> tempKPoints = new List<Point>();
                Point TempPoint1 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (i), YZhou2.Y - 5);
                tempKPoints.Add(TempPoint1);
                Point TempPoint2 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (i), YZhou2.Y + 5);
                tempKPoints.Add(TempPoint2);

                CurrCtDrawing.DrawingLine(tempKPoints, DesignerPane, CurrEllipse, CurrPath);

                //画X坐标文字
                CurrCtDrawing.DrawingText(CurrPoDrawing.SyXName[i].ToString(), CurrTextBlock, new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (i), YZhou2.Y + (CurrPoDrawing.SyYMarin / 2) / 2), DesignerPane);
            }
            //画y坐标点
            for (int i = 0; i < CurrPoDrawing.SyYName.Count; i++)
            {
                //画y轴坐标
                List<Point> tempKPoints = new List<Point>();
                Point TempPoint1 = new Point(CurrPoDrawing.SyXMarin - 5, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (i + 1));
                tempKPoints.Add(TempPoint1);
                Point TempPoint2 = new Point(CurrPoDrawing.SyXMarin + 5, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (i + 1));
                tempKPoints.Add(TempPoint2);

                CurrCtDrawing.DrawingLine(tempKPoints, DesignerPane, CurrEllipse, CurrPath);
                //画y轴文字
                CurrCtDrawing.DrawingText(CurrPoDrawing.SyYName[CurrPoDrawing.SyYName.Count - i - 1].ToString(), CurrTextBlock, new Point(CurrPoDrawing.SyXMarin - CurrPoDrawing.SyXMarin / 2 - (CurrPoDrawing.SyXMarin / 2) / 2, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (i + 1)), DesignerPane);

            }

            boolScaleAndLabel = true;
        }

        public int getArrFunIndex(ArrayList al, double dnum)
        {
            for (int i = 0; i < al.Count; i++)
            {
                if ((double)al[i] == dnum)
                    return i;
            }

            return 0;
        }
        public void structScaleAndLabel(double dProPortion)
        {
            double tuWidth = this.Width - CurrPoDrawing.SyXMarin * 2;
            CurrPoDrawing.SyXWide = tuWidth / CurrPoDrawing.SyXValue.Count;
            double tuHeight = this.Height - CurrPoDrawing.SyYMarin * 2;
            CurrPoDrawing.SyYWide = tuHeight / CurrPoDrawing.SyYValue.Count;

            double dXCha = (double)CurrPoDrawing.SyXValue[1] - (double)CurrPoDrawing.SyXValue[0];
            double dYCha = (double)CurrPoDrawing.SyYValue[1] - (double)CurrPoDrawing.SyYValue[0];

            double dXKeDu = dXCha * dProPortion;
            double dYKeDu = dYCha * dProPortion;


            List<Point> tempPoints = new List<Point>();
            //画x,y轴
            Point YZhou1 = new Point(CurrPoDrawing.SyXMarin, 0);
            tempPoints.Add(YZhou1);
            Point YZhou2 = new Point(CurrPoDrawing.SyXMarin, tuHeight + CurrPoDrawing.SyYMarin);
            tempPoints.Add(YZhou2);
            Point XZhou2 = new Point(this.Width, tuHeight + CurrPoDrawing.SyYMarin);
            tempPoints.Add(XZhou2);
            CurrCtDrawing.DrawingLine(tempPoints, DesignerPane, CurrEllipse, CurrPath);

            //画x坐标点
            for (double i = (double)CurrPoDrawing.SyXValue[0]; i <= (double)CurrPoDrawing.SyXValue[CurrPoDrawing.SyXName.Count - 1]; i += dXKeDu)
            {

                //画X轴坐标
                List<Point> tempKPoints = new List<Point>();
                Point TempPoint1, TempPoint2;
                if (Math.Round(i, 4) % dXCha == 0)
                {
                    TempPoint1 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (getArrFunIndex(CurrPoDrawing.SyXValue, Math.Round(i, 4))), YZhou2.Y - 5);
                    tempKPoints.Add(TempPoint1);
                    TempPoint2 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (getArrFunIndex(CurrPoDrawing.SyXValue, Math.Round(i, 4))), YZhou2.Y + 5);
                    tempKPoints.Add(TempPoint2);
                    //画X坐标文字
                    CurrCtDrawing.DrawingText(CurrPoDrawing.SyXName[getArrFunIndex(CurrPoDrawing.SyXValue, Math.Round(i, 4))].ToString(), CurrTextBlock, new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (getArrFunIndex(CurrPoDrawing.SyXValue, Math.Round(i, 4))), YZhou2.Y + (CurrPoDrawing.SyYMarin / 2) / 2), DesignerPane);
                }
                else
                {
                    TempPoint1 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (Math.Round(i, 4) - (double)CurrPoDrawing.SyXValue[0]) / dXCha, YZhou2.Y - 3);
                    tempKPoints.Add(TempPoint1);
                    TempPoint2 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (Math.Round(i, 4) - (double)CurrPoDrawing.SyXValue[0]) / dXCha , YZhou2.Y + 3);
                    tempKPoints.Add(TempPoint2);
                }

                CurrCtDrawing.DrawingLine(tempKPoints, DesignerPane, CurrEllipse, CurrPath);


            }
            //画y坐标点
            for (double i = (double)CurrPoDrawing.SyYValue[0]; i <= (double)CurrPoDrawing.SyYValue[CurrPoDrawing.SyYName.Count-1]; i += dYKeDu)
            {
                //画y轴坐标
                List<Point> tempKPoints = new List<Point>();
                Point TempPoint1, TempPoint2;
                if (Math.Round(i, 4) % dYCha == 0)
                {

                    TempPoint1 = new Point(CurrPoDrawing.SyXMarin - 5, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (getArrFunIndex(CurrPoDrawing.SyYValue, Math.Round(i, 4)) + 1));
                    tempKPoints.Add(TempPoint1);
                    TempPoint2 = new Point(CurrPoDrawing.SyXMarin + 5, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (getArrFunIndex(CurrPoDrawing.SyYValue, Math.Round(i, 4)) + 1));
                    tempKPoints.Add(TempPoint2);
                    //画y轴文字
                    CurrCtDrawing.DrawingText(CurrPoDrawing.SyYName[CurrPoDrawing.SyYName.Count - getArrFunIndex(CurrPoDrawing.SyYValue, Math.Round(i, 4)) - 1].ToString(), CurrTextBlock, new Point(CurrPoDrawing.SyXMarin - CurrPoDrawing.SyXMarin / 2 - (CurrPoDrawing.SyXMarin / 2) / 2, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (getArrFunIndex(CurrPoDrawing.SyYValue, Math.Round(i, 4)) + 1)), DesignerPane);

                }
                else
                {
                    TempPoint1 = new Point(CurrPoDrawing.SyXMarin - 3, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (Math.Round((i - (double)CurrPoDrawing.SyYValue[0]) / dYCha, 4) + 1));
                    tempKPoints.Add(TempPoint1);
                    TempPoint2 = new Point(CurrPoDrawing.SyXMarin + 3, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (Math.Round((i - (double)CurrPoDrawing.SyYValue[0]) /dYCha, 4) + 1));
                    tempKPoints.Add(TempPoint2);
                }
                CurrCtDrawing.DrawingLine(tempKPoints, DesignerPane, CurrEllipse, CurrPath);

            }

            boolScaleAndLabel = true;
        }

        public void structScaleAndLabel(double dXProPortion, double dYProPortion)
        {
            double tuWidth = this.Width - CurrPoDrawing.SyXMarin * 2;
            CurrPoDrawing.SyXWide = tuWidth / CurrPoDrawing.SyXValue.Count;
            double tuHeight = this.Height - CurrPoDrawing.SyYMarin * 2;
            CurrPoDrawing.SyYWide = tuHeight / CurrPoDrawing.SyYValue.Count;

            double dXCha = (double)CurrPoDrawing.SyXValue[1] - (double)CurrPoDrawing.SyXValue[0];
            double dYCha = (double)CurrPoDrawing.SyYValue[1] - (double)CurrPoDrawing.SyYValue[0];

            double dXKeDu = dXCha * dXProPortion;
            double dYKeDu = dYCha * dYProPortion;


            List<Point> tempPoints = new List<Point>();
            //画x,y轴
            Point YZhou1 = new Point(CurrPoDrawing.SyXMarin, 0);
            tempPoints.Add(YZhou1);
            Point YZhou2 = new Point(CurrPoDrawing.SyXMarin, tuHeight + CurrPoDrawing.SyYMarin);
            tempPoints.Add(YZhou2);
            Point XZhou2 = new Point(this.Width, tuHeight + CurrPoDrawing.SyYMarin);
            tempPoints.Add(XZhou2);
            CurrCtDrawing.DrawingLine(tempPoints, DesignerPane, CurrEllipse, CurrPath);

            //画x坐标点
            for (double i = (double)CurrPoDrawing.SyXValue[0]; i <= (double)CurrPoDrawing.SyXValue[CurrPoDrawing.SyXName.Count - 1]; i += dXKeDu)
            {

                //画X轴坐标
                List<Point> tempKPoints = new List<Point>();
                Point TempPoint1, TempPoint2;
                if (Math.Round(i, 4) % dXCha == 0)
                {
                    TempPoint1 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (getArrFunIndex(CurrPoDrawing.SyXValue, Math.Round(i, 4))), YZhou2.Y - 5);
                    tempKPoints.Add(TempPoint1);
                    TempPoint2 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (getArrFunIndex(CurrPoDrawing.SyXValue, Math.Round(i, 4))), YZhou2.Y + 5);
                    tempKPoints.Add(TempPoint2);
                    //画X坐标文字
                    CurrCtDrawing.DrawingText(CurrPoDrawing.SyXName[getArrFunIndex(CurrPoDrawing.SyXValue, Math.Round(i, 4))].ToString(), CurrTextBlock, new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (getArrFunIndex(CurrPoDrawing.SyXValue, Math.Round(i, 4))), YZhou2.Y + (CurrPoDrawing.SyYMarin / 2) / 2), DesignerPane);
                }
                else
                {
                    TempPoint1 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (Math.Round(i, 4) - (double)CurrPoDrawing.SyXValue[0]) / dXCha, YZhou2.Y - 3);
                    tempKPoints.Add(TempPoint1);
                    TempPoint2 = new Point(CurrPoDrawing.SyXMarin + CurrPoDrawing.SyXWide * (Math.Round(i, 4) - (double)CurrPoDrawing.SyXValue[0]) / dXCha, YZhou2.Y + 3);
                    tempKPoints.Add(TempPoint2);
                }

                CurrCtDrawing.DrawingLine(tempKPoints, DesignerPane, CurrEllipse, CurrPath);


            }
            //画y坐标点
            for (double i = (double)CurrPoDrawing.SyYValue[0]; i <= (double)CurrPoDrawing.SyYValue[CurrPoDrawing.SyYName.Count - 1]; i += dYKeDu)
            {
                //画y轴坐标
                List<Point> tempKPoints = new List<Point>();
                Point TempPoint1, TempPoint2;
                if (Math.Round(i, 4) % dYCha == 0)
                {

                    TempPoint1 = new Point(CurrPoDrawing.SyXMarin - 5, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (getArrFunIndex(CurrPoDrawing.SyYValue, Math.Round(i, 4)) + 1));
                    tempKPoints.Add(TempPoint1);
                    TempPoint2 = new Point(CurrPoDrawing.SyXMarin + 5, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (getArrFunIndex(CurrPoDrawing.SyYValue, Math.Round(i, 4)) + 1));
                    tempKPoints.Add(TempPoint2);
                    //画y轴文字
                    CurrCtDrawing.DrawingText(CurrPoDrawing.SyYName[CurrPoDrawing.SyYName.Count - getArrFunIndex(CurrPoDrawing.SyYValue, Math.Round(i, 4)) - 1].ToString(), CurrTextBlock, new Point(CurrPoDrawing.SyXMarin - CurrPoDrawing.SyXMarin / 2 - (CurrPoDrawing.SyXMarin / 2) / 2, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (getArrFunIndex(CurrPoDrawing.SyYValue, Math.Round(i, 4)) + 1)), DesignerPane);

                }
                else
                {
                    TempPoint1 = new Point(CurrPoDrawing.SyXMarin - 3, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (Math.Round((i - (double)CurrPoDrawing.SyYValue[0]) / dYCha, 4) + 1));
                    tempKPoints.Add(TempPoint1);
                    TempPoint2 = new Point(CurrPoDrawing.SyXMarin + 3, CurrPoDrawing.SyYMarin + CurrPoDrawing.SyYWide * (Math.Round((i - (double)CurrPoDrawing.SyYValue[0]) / dYCha, 4) + 1));
                    tempKPoints.Add(TempPoint2);
                }
                CurrCtDrawing.DrawingLine(tempKPoints, DesignerPane, CurrEllipse, CurrPath);

            }

            boolScaleAndLabel = true;
        }

        /// <summary>
        /// 设置动画修改控件宽度
        /// </summary>
        /// <param name="iWidth"></param>
        /// <param name="bB"></param>
        /// <param name="dG"></param>
        public void funSetWight(int iWidth, Border bB,double dG)
        {
            this.Width = iWidth;
            scs.UIChange(bB, iWidth, scs.syWidthProperty, dG);
            
        }

        /// <summary>
        /// 设置动画修改控件高度
        /// </summary>
        /// <param name="iHeight"></param>
        /// <param name="bB"></param>
        /// <param name="dG"></param>
        public void funSetHeight(int iHeight, Border bB, double dG)
        {
            this.Height = iHeight;
            scs.UIChange(bB, iHeight, scs.syHeightProperty, dG);
        }
        public List<List<syPoChild>> funCutSource(List<syPoChild> _inSources, Point dmin, Point dmax)
        {
            List<List<syPoChild>> reSource = new List<List<syPoChild>>();
            List<syPoChild> lSource = new List<syPoChild>();

            foreach (syPoChild spc in _inSources)
            {
                double num = -1;

                num = spc.SyY;

                if (spc.SyX > dmax.X || spc.SyX < dmin.X)
                    continue;

                if (num < dmin.Y)
                {
                    if (lSource.Count > 1)
                    {
                        reSource.Add(lSource);
                        lSource = new List<syPoChild>();
                    }
                    else if (lSource.Count == 1)
                    {
                        lSource.Add(lSource[0]);
                        reSource.Add(lSource);
                        lSource = new List<syPoChild>();
                    }

                    continue;
                }
                else if (num > dmax.Y)
                {
                    if (lSource.Count > 1)
                    {
                        reSource.Add(lSource);
                        lSource = new List<syPoChild>();
                    }
                    else if (lSource.Count == 1)
                    {
                        lSource.Add(lSource[0]);
                        reSource.Add(lSource);
                        lSource = new List<syPoChild>();
                    }

                    continue;
                }
                else
                    if (num != -1)
                        lSource.Add(spc);
                    else
                        continue;
            }
            if (lSource.Count > 0)
                reSource.Add(lSource);

            if (reSource.Count > 0)
                return reSource;
            else return null;
            
        }
        public List<List<syPoChild>> funCutSource(List<syPoChild> _inSources,double dmin,double dmax,string strXorY)
        {
            List<List<syPoChild>> reSource=new List<List<syPoChild>>();

            List<syPoChild> lSource=new List<syPoChild>();
            foreach(syPoChild spc in _inSources)
            {
                double num = -1;
                if (strXorY.ToUpper().Trim() == "X")
                    num = spc.SyX;
                else if (strXorY.ToUpper().Trim() == "Y")
                    num = spc.SyY;
                else
                    throw new Exception("strXorY 值错误");

                if (num < dmin)
                {
                    if (lSource.Count > 1)
                    {
                        reSource.Add(lSource);
                        lSource = new List<syPoChild>();
                    }
                    else if (lSource.Count == 1)
                    {
                        lSource.Add(lSource[0]);
                        reSource.Add(lSource);
                        lSource = new List<syPoChild>();
                    }

                    continue;
                }
                else if (num > dmax)
                {
                    if (lSource.Count > 1)
                    {
                        reSource.Add(lSource);
                        lSource = new List<syPoChild>();
                    }
                    else if (lSource.Count == 1)
                    {
                        lSource.Add(lSource[0]);
                        reSource.Add(lSource);
                        lSource = new List<syPoChild>();
                    }

                    continue;
                }
                else
                    if (num != -1)
                        lSource.Add(spc);
                    else
                        continue;
            }

            if (lSource.Count > 0)
                reSource.Add(lSource);

            if (reSource.Count > 0)
                return reSource;
            else return null;
        }

        public void testLine()
        {
            List<Point> controlPoints = new List<Point>();
            controlPoints.Add(new Point(10, 15));
            controlPoints.Add(new Point(60, 80));
            controlPoints.Add(new Point(32, 60));

            CurrCtDrawing.DrawingLine(controlPoints, DesignerPane, CurrEllipse, CurrPath);
        }

    }
}
