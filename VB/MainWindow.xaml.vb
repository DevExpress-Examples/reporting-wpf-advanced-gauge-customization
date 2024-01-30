Imports System.Windows
Imports System.Drawing
Imports DevExpress.XtraGauges.Core.Customization
Imports DevExpress.XtraGauges.Core.Drawing
Imports DevExpress.XtraGauges.Core.Model
Imports DevExpress.XtraGauges.Core.Base
Imports DevExpress.XtraReports.UI
Imports System.Collections.Generic
Imports Color = System.Drawing.Color
Imports System.Linq

Namespace Reporting_Advanced_Gauge_Customization

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Dim rep As XtraReport1 = New XtraReport1()
            Dim gauge As XRGauge = CreateGauge()
            rep.Bands(BandKind.Detail).Controls.Add(gauge)
            Me.viewer.DocumentSource = rep
        End Sub

        Private Shared Function GetOrAdd(Of T As {ISerizalizeableElement, New})(ByVal elements As List(Of ISerizalizeableElement)) As T
            Dim element = elements.OfType(Of T)().FirstOrDefault()
            If element IsNot Nothing Then Return element
            Dim newElement As T = New T()
            elements.Add(newElement)
            Return newElement
        End Function

        Private Function CreateGauge() As XRGauge
            Dim gauge As XRGauge = New XRGauge() With {.ViewType = DashboardGaugeType.Linear, .ViewStyle = DashboardGaugeStyle.Full, .SizeF = New SizeF(500, 200)}
            Dim gaugeControl As IDashboardGauge = gauge.Gauge
            Dim gaugeElements = gaugeControl.Elements
            Dim linearScale As LinearScale = GetOrAdd(Of LinearScale)(gaugeElements)
            Call SetupLinearScale(linearScale)
            Dim rangeBar As LinearScaleRangeBar = GetOrAdd(Of LinearScaleRangeBar)(gaugeElements)
            Call SetupRangeBar(rangeBar)
            Call AddMarker(TryCast(gaugeControl, DashboardGauge), 50F)
            Return gauge
        End Function

        Private Shared Sub SetupLinearScale(ByVal linearScale As LinearScale)
            linearScale.BeginUpdate()
            Call AddScaleRanges(linearScale)
            linearScale.MinValue = 0
            linearScale.MaxValue = 100
            linearScale.Appearance.Brush = New SolidBrushObject(Color.Transparent)
            linearScale.MinorTickmark.ShowTick = False
            linearScale.MajorTickmark.ShowTick = False
            linearScale.MajorTickmark.ShowText = False
            linearScale.EndUpdate()
        End Sub

        Private Shared Sub SetupRangeBar(ByVal rangeBar As LinearScaleRangeBar)
            rangeBar.Appearance.ContentBrush = New SolidBrushObject(Color.Transparent)
        End Sub

        Private Shared Sub AddMarker(ByVal gauge As DashboardGauge, ByVal value As Single)
            Dim linearScaleComponent1 As LinearScaleProvider = TryCast(gauge.Scale, LinearScaleProvider)
            Dim marker As LinearScaleMarker = New LinearScaleMarker("marker")
            marker.ShapeType = MarkerPointerShapeType.Diamond
            marker.Shader = New StyleShader() With {.StyleColor1 = Color.Blue, .StyleColor2 = Color.Blue}
            marker.ShapeOffset = -20.0F
            marker.Value = value
            marker.LinearScale = linearScaleComponent1
            Dim root As ModelRoot = TryCast(gauge.Model.Composite(PredefinedCoreNames.LinearGaugeRotationNode), ModelRoot)
            root.Composite.Add(marker)
        End Sub

        Private Shared Sub AddScaleRanges(ByVal scale As LinearScale)
            Dim range1 As LinearScaleRange = New LinearScaleRange()
            range1.AppearanceRange.ContentBrush = New SolidBrushObject(Color.Green)
            range1.StartValue = 0
            range1.EndValue = 20
            Dim range2 As LinearScaleRange = New LinearScaleRange()
            range2.AppearanceRange.ContentBrush = New SolidBrushObject(Color.Yellow)
            range2.StartValue = 20
            range2.EndValue = 40
            Dim range3 As LinearScaleRange = New LinearScaleRange()
            range3.AppearanceRange.ContentBrush = New SolidBrushObject(Color.Red)
            range3.StartValue = 40
            range3.EndValue = 100
            scale.Ranges.Clear()
            scale.Ranges.AddRange(New IRange() {range1, range2, range3})
        End Sub
    End Class
End Namespace
