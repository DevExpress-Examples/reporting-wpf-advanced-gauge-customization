using System.Windows;
using System.Drawing;
using DevExpress.XtraGauges.Core.Customization;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Color = System.Drawing.Color;
using System.Linq;

namespace Reporting_Advanced_Gauge_Customization {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            XtraReport1 rep = new XtraReport1();
            XRGauge gauge = CreateGauge();
            rep.Bands[BandKind.Detail].Controls.Add(gauge);
            viewer.DocumentSource = rep;
        }

        static T GetOrAdd<T>(List<ISerizalizeableElement> elements) where T:
            ISerizalizeableElement, new() {
            var element = elements.OfType<T>().FirstOrDefault();
            if (element != null) return element;

            T newElement = new T();
            elements.Add(newElement);
            return newElement;
        }

        XRGauge CreateGauge() {
            XRGauge gauge = new XRGauge() {
                ViewType = DashboardGaugeType.Linear,
                ViewStyle = DashboardGaugeStyle.Full,
                SizeF = new SizeF(500, 200)
            };

            IDashboardGauge gaugeControl = gauge.Gauge;
            var gaugeElements = gaugeControl.Elements;

            LinearScale linearScale = GetOrAdd<LinearScale>(gaugeElements);
            SetupLinearScale(linearScale);

            LinearScaleRangeBar rangeBar = GetOrAdd<LinearScaleRangeBar>(gaugeElements);
            SetupRangeBar(rangeBar);

            AddMarker(gaugeControl as DashboardGauge, 50f);

            return gauge;
        }

        static void SetupLinearScale(LinearScale linearScale) {
            linearScale.BeginUpdate();

            AddScaleRanges(linearScale);
            linearScale.MinValue = 0;
            linearScale.MaxValue = 100;

            linearScale.Appearance.Brush = new SolidBrushObject(Color.Transparent);

            linearScale.MinorTickmark.ShowTick = false;
            linearScale.MajorTickmark.ShowTick = false;
            linearScale.MajorTickmark.ShowText = false;

            linearScale.EndUpdate();
        }

        static void SetupRangeBar(LinearScaleRangeBar rangeBar) {
            rangeBar.Appearance.ContentBrush = new SolidBrushObject(Color.Transparent);
        }

        static void AddMarker(DashboardGauge gauge, float value) {
            LinearScaleProvider linearScaleComponent1 = gauge.Scale as LinearScaleProvider;
            LinearScaleMarker marker = new LinearScaleMarker("marker");
            marker.ShapeType = MarkerPointerShapeType.Diamond;
            marker.Shader = new StyleShader() { StyleColor1 = Color.Blue, StyleColor2 = Color.Blue };
            marker.ShapeOffset = -20.0f;
            marker.Value = value;
            marker.LinearScale = linearScaleComponent1;
            ModelRoot root = gauge.Model.Composite[PredefinedCoreNames.LinearGaugeRotationNode] as ModelRoot;
            root.Composite.Add(marker);
        }

        static void AddScaleRanges(LinearScale scale) {
            LinearScaleRange range1 = new LinearScaleRange();
            range1.AppearanceRange.ContentBrush = new SolidBrushObject(Color.Green);
            range1.StartValue = 0;
            range1.EndValue = 20;

            LinearScaleRange range2 = new LinearScaleRange();
            range2.AppearanceRange.ContentBrush = new SolidBrushObject(Color.Yellow);
            range2.StartValue = 20;
            range2.EndValue = 40;

            LinearScaleRange range3 = new LinearScaleRange();
            range3.AppearanceRange.ContentBrush = new SolidBrushObject(Color.Red);
            range3.StartValue = 40;
            range3.EndValue = 100;

            scale.Ranges.Clear();
            scale.Ranges.AddRange(new IRange[] { range1, range2, range3 });
        }
    }
}

