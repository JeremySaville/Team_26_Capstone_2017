using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DiabetesApp.Models {
    public class BSLGraph {
        public PlotModel BSLModel { get; set; }

        public BSLGraph(DateTime start, DateTime end, ObservableCollection<LogbookListItem> logs) {
            BSLModel = CreateBSLChart(start, end, logs);
        }

        private PlotModel CreateBSLChart(DateTime start, DateTime end, ObservableCollection<LogbookListItem> logs) {
            PlotModel model = new PlotModel { Title = "BG Beginning " + start.ToString("ddd d MMM, yyyy") };
            double earliestDate = DateTimeAxis.ToDouble(start);
            double latestDate = DateTimeAxis.ToDouble(end);

            var highBSSeries = new AreaSeries();
            highBSSeries.Points.Add(new DataPoint(earliestDate, 8));
            highBSSeries.Points.Add(new DataPoint(latestDate, 8));
            highBSSeries.Points.Add(new DataPoint(latestDate, 15));
            highBSSeries.Points.Add(new DataPoint(earliestDate, 15));
            highBSSeries.Points.Add(new DataPoint(earliestDate, 8));
            highBSSeries.Color = OxyColor.FromRgb(255, 163, 102);

            var lowBSSeries = new AreaSeries();
            lowBSSeries.Points.Add(new DataPoint(earliestDate, 4));
            lowBSSeries.Points.Add(new DataPoint(latestDate, 4));
            lowBSSeries.Points.Add(new DataPoint(latestDate, -1));
            lowBSSeries.Points.Add(new DataPoint(earliestDate, -1));
            lowBSSeries.Points.Add(new DataPoint(earliestDate, 4));
            lowBSSeries.Color = OxyColor.FromRgb(255, 128, 128);

            var lineSeries = new LineSeries() { MarkerType = MarkerType.Circle, MarkerSize = 3, MarkerStroke = OxyColor.FromRgb(0, 61, 153) };
            foreach(LogbookListItem l in logs) {
                if (l.entryTime.CompareTo(start) >= 0 && l.entryTime.CompareTo(end) <= 0)
                    lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(l.entryTime), l.BG));
            }
            lineSeries.Color = OxyColor.FromRgb(0, 61, 153);

            model.Series.Add(highBSSeries);
            model.Series.Add(lowBSSeries);
            model.Series.Add(lineSeries);

            if (start.Day.Equals(end.Day) && start.Month.Equals(end.Month) && start.Year.Equals(end.Year)) {
                model.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, AbsoluteMinimum = earliestDate, AbsoluteMaximum = latestDate, StringFormat = "hhtt" });
            } else {
                model.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, AbsoluteMinimum = earliestDate, AbsoluteMaximum = latestDate, StringFormat = "MMM d" });
            }
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, AbsoluteMinimum = 0, AbsoluteMaximum = 14, Title = "Blood Glucose" });
            model.PlotAreaBackground = OxyColor.FromRgb(255, 255, 255);
            return model;
        }
    }
}
