using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CGLAB1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    enum Options
    {
        NewPoint,
        NewLine,
        DeletePoint,
        DeleteLine
    }

    public partial class MainWindow : Window
    {
        ObservableCollection<String> PointList = new ObservableCollection<String>();
        ObservableCollection<String> ChainList = new ObservableCollection<String>();
        List<WChain> WChains = new List<WChain>();

        DrawModule DM;

        public MainWindow()
        {
            InitializeComponent();

            CommandBox.Items.Add("New point");
            CommandBox.Items.Add("New chain");
            CommandBox.SelectedIndex = (int)Options.NewPoint;

            ChainBox.ItemsSource = ChainList;
            WChains.Add(new WChain());
            ChainList.Add("Chain 1");
            ChainBox.SelectedIndex = 0;

            PointBox.ItemsSource = PointList;
            DM = new DrawModule(canvas);
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WPoint wp = new WPoint(Mouse.GetPosition(canvas));

            switch (CommandBox.SelectedIndex)
            {
                case (int)Options.NewPoint:

                    WChains[ChainBox.SelectedIndex].List.Add(wp);
                    PointList.Add(wp.ToString());

                    if (WChains[ChainBox.SelectedIndex].List.Count > 1)
                        DM.DrawAdditionalPoint(WChains[ChainBox.SelectedIndex], wp);

                    break;

                case (int)Options.NewLine:

                    WChains.Add(new WChain(wp));
                    int last = ChainList.Count;
                    ChainList.Add(WChains[last].ToString(last));
                    
                    SetChainBoxIndex(last);
                    SetCommandBoxIndex((int)Options.NewPoint);

                    break;
            }

            DM.DrawPoint(wp, Brushes.DarkCyan);
        }

        private void UpdatePointList()
        {
            PointList.Clear();
            foreach (WPoint wp in WChains[ChainBox.SelectedIndex].List)
            {
                PointList.Add(wp.Ghost.X + ", " + wp.Ghost.Y);
            }
        }

        private void SetPointBoxSelectedIndex(int index)
        {
            PointBox.SelectedIndex = index;
        }

        private void SetCommandBoxIndex(int index)
        {
            CommandBox.SelectedIndex = index;
        }

        private void SetChainBoxIndex(int index)
        {
            ChainBox.SelectedIndex = index;
        }

        private void ChainBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePointList();
        }

        private void CubicBezierBtn_Click(object sender, RoutedEventArgs e)
        {
            DM.ClearConnections(WChains[ChainBox.SelectedIndex], WChains[ChainBox.SelectedIndex].Shell);
            DM.DrawCubicBezier(WChains[ChainBox.SelectedIndex]);
        }

        private void QuadraticBezierBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeletePointBtn_Click(object sender, RoutedEventArgs e)
        {
            WChain wc = WChains[ChainBox.SelectedIndex];
            DM.ClearPoint(wc.List[PointBox.SelectedIndex]);
            wc.List.RemoveAt(PointBox.SelectedIndex);
            DM.ClearConnections(wc);
            DM.DrawConnections(wc);
            UpdatePointList();
        }
    }
}