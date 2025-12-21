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
    public partial class Custom_Checkbox  : UserControl
    {
        public event EventHandler<bool> CheckedClick;

        public Custom_Checkbox()
        {
            InitializeComponent();
            Loaded += (s, e) => UpdateVisual();
        }

        private bool _isChecked = false;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;

                    UpdateVisual();
                    // Solleva l'evento quando il valore cambia
                   
                }
            }
        }

        //mettere questa variabile a false per un normale comportamento del checker
        // a true non può essere Deselezionato ed è fatto per l'utilizzo di mutua esclusione in modo che non possano essere deselezionati contemporaneamente
        // ma appunto si vada a false il checker solo se l'altro viene cliccato a true.
        public bool isDeselectionDisabled { get; set; } = true;


        // Proprietà accessibili da XAML
        public string LabelText
        {
            get => Label.Text;
            set => Label.Text = value;
        }

        public double LabelFontSize
        {
            get => Label.FontSize;
            set => Label.FontSize = value;
        }

        public Brush LabelColor
        {
            get => Label.Foreground;
            set => Label.Foreground = value;
        }

        public double CheckerSize
        {
            get => Checker.Width;
            set { Checker.Width = value; Checker.Height = value; }
        }

        public Brush CheckerBorderColor
        {
            get => Checker.BorderBrush;
            set => Checker.BorderBrush = value;
        }

        public Brush CheckedColor { get; set; } = Brushes.DodgerBlue;
        public Brush UncheckedColor { get; set; } = Brushes.Transparent;

      

        // Personalizza i colori
        public void SetColors(string checkedColor, string uncheckedColor, string borderColor)
        {
            Checker.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom(borderColor);
            if (IsChecked)
                Checker.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(checkedColor);
            else
                Checker.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(uncheckedColor);
        }

        // Personalizza dimensione
        public void SetSize(double size)
        {
            Checker.Width = size;
            Checker.Height = size;
        }

        // Personalizza testo
        public void SetLabel(string text, double fontSize = 14, string color = "Black")
        {
            Label.Text = text;
            Label.FontSize = fontSize;
            Label.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom(color);
        }

        // Click sul checker
        private void Checker_Click(object sender, MouseButtonEventArgs e)
        {
            //mettere questa variabile a false per un normale comportamento del checker
            // a true non può essere Deselezionato ed è fatto per l'utilizzo di mutua esclusione in modo che non possano essere deselezionati contemporaneamente
            // ma appunto si vada a false il checker solo se l'altro viene cliccato a true.
            if (isDeselectionDisabled)
            {
                IsChecked = true;
            }
            else
            {
                IsChecked = !IsChecked;
            }
           
            CheckedClick?.Invoke(this, IsChecked);
        }

        // Aggiorna l'aspetto visuale
        private void UpdateVisual()
        {
            if (CheckerFill != null)
            {
                CheckerFill.Fill = IsChecked ? CheckedColor : UncheckedColor;
                CheckerFill.Opacity = IsChecked ? 1 : 0;
            }
        }
    }
}

