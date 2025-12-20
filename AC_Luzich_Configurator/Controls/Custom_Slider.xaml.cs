using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AC_Configurator_STDL.Controls
{
    /// <summary>
    /// Interaction logic for Custom_Slider.xaml
    /// </summary>
    public partial class Custom_Slider : UserControl
    {
        private bool isDragging = false;

        public Custom_Slider()
        {
            InitializeComponent();
            UpdateThumbPosition();
        }

        public event EventHandler<double> ValueChanged;

        private double _value = 0;
        public double Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    // Solleva l'evento quando il valore cambia
                    ValueChanged?.Invoke(this, _value);
                }
            }
        }
        public double MinValue { get; set; } = 0;
        public double MaxValue { get; set; } = 100;


        // Lunghezza barra
        public double TrackSize
        {
            get => Track.Width;
            set => Track.Width = value;
        }

        public double Value_init
        {
            get => Fill.Width;
            set
            {
               Fill.Width = value / 100 * Track.Width;
               UpdateValueFromPosition(Fill.Width);
            }
        }


        // Personalizza i colori
        public void SetColors(string trackColor, string fillColor, string thumbColor)
        {
            Track.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(trackColor);
            Fill.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(fillColor);
            Thumb.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(thumbColor);
        }

        // Personalizza dimensioni
        public void SetSize(double width, double height, double thumbSize = 20)
        {
            Track.Width = width;
            Track.Height = height;
            Fill.Height = height;
            Thumb.Width = thumbSize;
            Thumb.Height = thumbSize;
           /* Track.RadiusX = height / 2;
            Track.RadiusY = height / 2;
            Fill.RadiusX = height / 2;
            Fill.RadiusY = height / 2;*/
            UpdateThumbPosition();
        }

        // Personalizza bordo
        public void SetBorder(string borderColor, double thickness = 1)
        {
            Track.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom(borderColor);
            Track.StrokeThickness = thickness;
        }

        // Imposta valore
        public void SetValue(double value)
        {
            Value = Math.Max(MinValue, Math.Min(MaxValue, value));
            UpdateThumbPosition();
        }

        // Aggiorna posizione thumb
        private void UpdateThumbPosition()
        {
            if (Track.Width == 0) return;

            double percentage = (Value - MinValue) / (MaxValue - MinValue);
            double thumbRadius = Thumb.Width / 2;
            double availableWidth = Track.Width;

            Canvas.SetLeft(Thumb, availableWidth * percentage);
            Fill.Width = (availableWidth * percentage) + thumbRadius;
        }

        // Mouse down sul thumb
        private void Thumb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            Thumb.CaptureMouse();
            e.Handled = true;
        }

        // Mouse move
        private void Thumb_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point position = e.GetPosition(SliderCanvas);
                UpdateValueFromPosition(position.X);
            }
        }

        // Mouse up
        private void Thumb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                Thumb.ReleaseMouseCapture();
            }
        }

        // Click sulla track
        private void Track_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(SliderCanvas);
            UpdateValueFromPosition(position.X);
        }

        // Aggiorna valore da posizione mouse
        private void UpdateValueFromPosition(double x)
        {
            double thumbRadius = Thumb.Width / 2;
            double availableWidth = Track.Width - Thumb.Width;
            double clampedX = Math.Max(0, Math.Min(availableWidth, x - thumbRadius));

            double percentage = clampedX / availableWidth;
            Value = MinValue + (percentage * (MaxValue - MinValue));

            UpdateThumbPosition();
        }
    }

}
