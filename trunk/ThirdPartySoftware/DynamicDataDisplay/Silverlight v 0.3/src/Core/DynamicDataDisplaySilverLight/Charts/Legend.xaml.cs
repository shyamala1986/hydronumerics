﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Windows.Data;

namespace Microsoft.Research.DynamicDataDisplay.Charts
{
    public partial class Legend : UserControl
    {
        protected Plotter plotter = null;
        protected List<ILegendable> graphsInLegend = new List<ILegendable>();
        private int allowedDescriptionLength = 25;
        //private bool enableAutomaticDescriptionLength = true;
        private Size previousSize = new Size(Double.NaN,Double.NaN);


        ///// <summary>
        ///// Get or set whether to automatic control AllowedDescriptionLength property, according to current plotter size
        ///// </summary>
        //public bool EnableAutomaticDescriptionLength
        //{
        //    get {
        //        return enableAutomaticDescriptionLength;
        //    }
        //    set
        //    {
        //        enableAutomaticDescriptionLength = value;
        //    }
        //}

        /// <summary>
        /// Get or set the maximum permited graph description length, all descriptions with a longer length will be cut
        /// </summary>
        public int AllowedDescriptionLength
        {
            get {
                return allowedDescriptionLength;
            }

            set {
                if (value > 1)
                {
                    allowedDescriptionLength = value;
                    legendable_VisualizationChanged(this, EventArgs.Empty);
                }
            }
        
        }

        public Legend()
        {
            InitializeComponent();
        }

        public Legend(Plotter associatedPlotter) {
            InitializeComponent();
            AssociatePlotter(associatedPlotter);
        }

        public void AssociatePlotter(Plotter associatrdPlotter) {
            if (plotter != null)
            {
                Plotter.Children.CollectionChanged -= Children_CollectionChanged;
                foreach (IPlotterElement elem in Plotter.Children)
                {
                    if (elem is ILegendable)
                    {
                        (elem as ILegendable).VisualizationChanged -= legendable_VisualizationChanged;
                    }
                }
            }
            else {
                MouseLeftButtonDown += new MouseButtonEventHandler(Legend_MouseLeftButtonDown);
            }
            plotter = associatrdPlotter;
            IEnumerable<ILegendable> x = from child in plotter.Children where child is LineGraph select child as ILegendable;
            plotter = associatrdPlotter;
            graphsInLegend.AddRange(x);
            Plotter.Children.CollectionChanged+=new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Children_CollectionChanged);
            mainStackPanel.SizeChanged += new SizeChangedEventHandler(mainStackPanel_SizeChanged);
            RepopulateLegend();

        }

        void mainStackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            boundingRect.Width = mainStackPanel.ActualWidth + 10;
            boundingRect.Height = mainStackPanel.ActualHeight + 10;
        }

        void Legend_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        public void RepopulateLegend() {
            if (plotter != null)
            {
                mainStackPanel.Children.Clear();
                foreach (ILegendable legendable in graphsInLegend)
                {
                    legendable.VisualizationChanged -= legendable_VisualizationChanged;
                }
                graphsInLegend.Clear();

                int i = 0;

                foreach (ILegendable legendable in 
                    (from IPlotterElement graph in Plotter.Children where (graph is ILegendable) select graph))
                {
                    i++;
                    if (legendable.ShowInLegend)
                    {                        
                        legendable.VisualizationChanged += new EventHandler<EventArgs>(legendable_VisualizationChanged);
                        graphsInLegend.Add(legendable);
                    }
                }
                UpdateStackPanel();
                InvalidateMeasure();
                InvalidateArrange();
            }
        }

        void legendable_VisualizationChanged(object sender, EventArgs e)
        {
            UpdateStackPanel();
            InvalidateMeasure();
            InvalidateArrange();
        }

        void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (plotter != null)
            {
                bool changed = false;
                if (e.NewItems != null)
                {
                    foreach (object obj in e.NewItems)
                    {
                        if (obj is ILegendable)
                        {
                            ILegendable l = obj as ILegendable;
                            changed = true;
                            l.VisualizationChanged += new EventHandler<EventArgs>(legendable_VisualizationChanged);
                            graphsInLegend.Add(l);
                        }
                    }
                }
                if(e.OldItems!=null) {
                    foreach (object obj in e.OldItems)
                    {
                        if (obj is ILegendable)
                        {
                            ILegendable l = obj as ILegendable;
                            changed = true;
                            l.VisualizationChanged -= legendable_VisualizationChanged;
                            graphsInLegend.Remove(l);
                        }
                    }
                }
                if (changed) {
                    UpdateStackPanel();
                    InvalidateMeasure();
                    InvalidateArrange();
                }
            }
        }

        private void UpdateStackPanel()
        {

            mainStackPanel.Children.Clear();
            int longestDescr = 0;
            bool empty = true;
            foreach (ILegendable l in graphsInLegend) {
                if (!l.ShowInLegend) continue;
                empty = false;
                StackPanel legendItem = new StackPanel();
                ToolTip tooltip = new ToolTip() {  Content = l.Description};
                legendItem.Orientation = Orientation.Horizontal;
                Line line = new Line();
                line.Stroke = new SolidColorBrush(l.LineColor);
                line.StrokeThickness = l.LineThickness;
                line.X2 = 0; line.Y2 = 0;
                line.X1 = 15; line.Y1 = 15;
                ToolTipService.SetToolTip(legendItem, tooltip);
                legendItem.Children.Add(line);

                TextBlock textBlock = new TextBlock();
                textBlock.Margin = new Thickness(5.0);
                if (l.Description.Length > longestDescr)
                    longestDescr = l.Description.Length;
                if (l.Description.Length > AllowedDescriptionLength+1)
                    textBlock.Text = String.Format("{0}...", l.Description.Remove(AllowedDescriptionLength));
                else
                    textBlock.Text = l.Description;
                legendItem.Children.Add(textBlock);

              //If possible include a checkbox to switch the graph on and off
                var lgwp = l as LineGraphWithPoints;
                if (lgwp != null)
                {
                  legendItem.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                  CheckBox c = new CheckBox();
                  c.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                  Binding bd = new Binding();
                  bd.Source =  lgwp;
                  bd.Path = new PropertyPath("ShowInPlotter");
                  bd.Mode = BindingMode.TwoWay; 
                  c.SetBinding(CheckBox.IsCheckedProperty, bd);
                 
                  legendItem.Children.Add(c);
                }
                mainStackPanel.Children.Add(legendItem);
            }
            if (empty)
            {
                if (Height != 0 && Width != 0)
                {
                    previousSize = new Size(Width, Height);
                    Width = 0; Height = 0;
                }
            }
            else
            {
                    Width = previousSize.Width;
                    Height = previousSize.Height;
            }

            //if(EnableAutomaticDescriptionLength)
            //{
            //    bool changed = false;
            //    double centralGridWidth = Plotter.CentralGrid.ActualWidth;
            //    if (centralGridWidth / 3 < ActualWidth)
            //    {
            //        changed = true;
            //        if (AllowedDescriptionLength > longestDescr)
            //            AllowedDescriptionLength = longestDescr - 3;
            //        else AllowedDescriptionLength--;
            //    }
            //    else if((longestDescr>AllowedDescriptionLength+3)
            //        && (AllowedDescriptionLength<int.MaxValue-3)
            //        && centralGridWidth / 2.8 > ActualWidth)
            //    {
            //        AllowedDescriptionLength++;
            //        changed = true;
            //    }
            //    if (changed)
            //        legendable_VisualizationChanged(this, EventArgs.Empty);
            //}
        }

        
        public Plotter Plotter
        {
            get { return plotter; }
        }

    }

    public interface ILegendable {
        bool ShowInLegend { get; set; }
        bool IsTooltipEnabled { get; set; }

        event EventHandler<EventArgs> VisualizationChanged;

        string Description
        {
            get;
        }
        double LineThickness
        {
            get;
        }
        Color LineColor
        {
            get;
        }
    }
}
