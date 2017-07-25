using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App1{
    class GraphViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public PlotModel BSLModel { get; set; }

        public GraphViewModel() {
            BSLModel = CreateBSLChart();
        }

        private PlotModel CreateBSLChart() {
            PlotModel model = new PlotModel { Title = "BSL Levels 27 May 2017" };
            double earliestDate = DateTimeAxis.ToDouble(DateTime.Parse("Sat 27 May 2017 08:23:00"));
            double latestDate = DateTimeAxis.ToDouble(DateTime.Parse("Sat 27 May 2017 19:48:00"));

            var highBSSeries = new AreaSeries();
            highBSSeries.Points.Add(new DataPoint(earliestDate,9));
            highBSSeries.Points.Add(new DataPoint(latestDate, 9));
            highBSSeries.Points.Add(new DataPoint(latestDate, 15));
            highBSSeries.Points.Add(new DataPoint(earliestDate, 15));
            highBSSeries.Points.Add(new DataPoint(earliestDate, 9));
            highBSSeries.Color = OxyColor.FromRgb(255, 128, 128);
            
            var lowBSSeries = new AreaSeries();
            lowBSSeries.Points.Add(new DataPoint(earliestDate, 5));
            lowBSSeries.Points.Add(new DataPoint(latestDate, 5));
            lowBSSeries.Points.Add(new DataPoint(latestDate, -1));
            lowBSSeries.Points.Add(new DataPoint(earliestDate, -1));
            lowBSSeries.Points.Add(new DataPoint(earliestDate, 5));
            lowBSSeries.Color = OxyColor.FromRgb(255, 163, 102);

            var lineSeries = new LineSeries();
            lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Parse("Sat 27 May 2017 08:23:00")), 8));
            lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Parse("Sat 27 May 2017 10:14:00")), 11));
            lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Parse("Sat 27 May 2017 13:04:00")), 7));
            lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Parse("Sat 27 May 2017 17:22:00")), 9));
            lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Parse("Sat 27 May 2017 19:48:00")), 6));
            lineSeries.Color = OxyColor.FromRgb(0, 61, 153);

            model.Series.Add(highBSSeries);
            model.Series.Add(lowBSSeries);
            model.Series.Add(lineSeries);

            model.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, AbsoluteMinimum = earliestDate, AbsoluteMaximum = latestDate, Title = "DateTime" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, AbsoluteMinimum = 0, AbsoluteMaximum = 14, Title = "BSL" });
            model.PlotAreaBackground = OxyColor.FromRgb(255, 255, 255);
            return model;
        }
    }
}
