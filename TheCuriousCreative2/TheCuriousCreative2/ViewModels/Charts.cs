using System;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TheCuriousCreative2.ViewModels
{

    public class ViewModel
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                Fill = null
            }
            };
    }

}
