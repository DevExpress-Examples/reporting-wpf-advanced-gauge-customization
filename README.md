<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T951947)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
Files to look at:

* [MainWindow.xaml.cs](https://github.com/DevExpress-Examples/Reporting-Advanced-Gauge-Customization/blob/2020.2/CS/MainWindow.xaml.cs) ([MainWindow.xaml.vb](https://github.com/DevExpress-Examples/Reporting-Advanced-Gauge-Customization/blob/2020.2/VB/MainWindow.xaml.vb))

![](https://raw.githubusercontent.com/DevExpress-Examples/Reporting-Advanced-Gauge-Customization/2020.2/CS/gauge-progress-bar.png)

# Advanced Gauge Customization

In the **Report Designer**, you can set up the basic [XRGauge](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRGauge)
settings: view, style, theme, actual and target values, minimum and maximum values. For advanced customization, you can use **XRGauge**'s
**Gauge** property that gives you access to the **XRGauge**'s internal model. The following example demonstrates how to use the **Gauge** property
to create a gauge with formatted values:

```
XRGauge gaugeControl = new XRGauge();
IDashboardGauge gauge = gaugeControl.Gauge;
gauge.Scale.MajorTickmark.FormatString = "{0}%";
```

![](https://raw.githubusercontent.com/DevExpress-Examples/Reporting-Advanced-Gauge-Customization/2020.2/CS/create-gauge-in-code-example-2.png)

The example in this repository shows how to customize the **XRGauge** control to create a progress bar:

1. Create a linear gauge:
    ```
    XRGauge gauge = new XRGauge() {
        ViewType = DashboardGaugeType.Linear,
        ViewStyle = DashboardGaugeStyle.Full,
        SizeF = new SizeF(500, 200)
    };
    ```
2. Customize the gauge's elements:
    ```
    IDashboardGauge gaugeControl = gauge.Gauge;
    var gaugeElements = gaugeControl.Elements;

    LinearScale linearScale = GetOrAdd<LinearScale>(gaugeElements);
    SetupLinearScale(linearScale);

    LinearScaleRangeBar rangeBar = GetOrAdd<LinearScaleRangeBar>(gaugeElements);
    SetupRangeBar(rangeBar);

    AddMarker(gaugeControl as DashboardGauge, 50f);
    ```
Refer to the [Visual Elements](https://docs.devexpress.com/WindowsForms/18208/controls-and-libraries/gauges/concepts/visual-elements?p=netframework)
article for information on visual elements and their properties. See the following examples for instructions on how to customize WinForms gauge elements at runtime:
[Customize Gauges At Runtime](https://docs.devexpress.com/WindowsForms/18249/controls-and-libraries/gauges/examples#at-runtime).
