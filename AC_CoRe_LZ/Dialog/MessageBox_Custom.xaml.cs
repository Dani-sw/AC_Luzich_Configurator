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

namespace AC_CoRe.Dialog
{
    /// <summary>
    /// UserControl personalizzato per messaggi di errore/informazione in stile Material Design Dark
    /// </summary>
    public partial class MessageBox_Custom : UserControl
    {
        public MessageBox_Custom()
        {
            InitializeComponent();
        }

        public enum MessageType
        {
            Error,
            Warning,
            Info,
            Success
        }

        // Proprietà personalizzabili
        public string Title { get; set; } = "Messaggio";
        public string Message { get; set; } = "";
        public MessageType Type { get; set; } = MessageType.Info;
        public double BoxWidth { get; set; } = 400;
        public double BoxHeight { get; set; } = 250;
        public string CustomIcon { get; set; } = null;

        /// <summary>
        /// Metodo statico per mostrare il MessageBox
        /// </summary>
        public static void Show(string message, string title = "Messaggio",
            MessageType type = MessageType.Info, double width = 400, double height = 250,
            string customIcon = null)
        {
            var window = new Window
            {
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                Width = width,
                Height = height,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
                AllowsTransparency = true,
                ShowInTaskbar = false
            };

            var msgBox = new MessageBox_Custom
            {
                Title = title,
                Message = message,
                Type = type,
                BoxWidth = width,
                BoxHeight = height,
                CustomIcon = customIcon
            };

            msgBox.BuildUI(window);
            window.Content = msgBox;
            window.ShowDialog();
        }

        /// <summary>
        /// Costruisce l'interfaccia del MessageBox
        /// </summary>
        public void BuildUI(Window parentWindow)
        {
            // Grid principale
            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(60) });

            // Barra del titolo
            var titleBar = CreateTitleBar(parentWindow);
            Grid.SetRow(titleBar, 0);
            mainGrid.Children.Add(titleBar);

            // Area messaggio
            var messageArea = CreateMessageArea();
            Grid.SetRow(messageArea, 1);
            mainGrid.Children.Add(messageArea);

            // Area pulsanti
            var buttonArea = CreateButtonArea(parentWindow);
            Grid.SetRow(buttonArea, 2);
            mainGrid.Children.Add(buttonArea);

            this.Content = mainGrid;
        }

        /// <summary>
        /// Crea la barra del titolo con icona e testo
        /// </summary>
        private Border CreateTitleBar(Window parentWindow)
        {
            var titleBorder = new Border
            {
                Background = GetColorByType(),
                CornerRadius = new CornerRadius(8, 8, 0, 0)
            };

            var titleGrid = new Grid();
            titleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
            titleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            titleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });

            // Icona
            var iconText = new TextBlock
            {
                Text = GetIconByType(),
                FontSize = 24,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontFamily = new FontFamily("Segoe UI Symbol")
            };
            Grid.SetColumn(iconText, 0);
            titleGrid.Children.Add(iconText);

            // Titolo
            var titleText = new TextBlock
            {
                Text = Title,
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                Foreground = Brushes.White,
                FontFamily = new FontFamily("#3ds Light"),
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };
            Grid.SetColumn(titleText, 1);
            titleGrid.Children.Add(titleText);

            // Pulsante chiudi
            var closeButton = new Button
            {
                Content = "✕",
                FontSize = 18,
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
                BorderThickness = new Thickness(0),
                Cursor = System.Windows.Input.Cursors.Hand,
                FontFamily = new FontFamily("Segoe UI Symbol"),
                Style = CreateHoverButtonStyle()
            };
            closeButton.Click += (s, e) => parentWindow.Close();
            Grid.SetColumn(closeButton, 2);
            titleGrid.Children.Add(closeButton);

            titleBorder.Child = titleGrid;
            return titleBorder;
        }

        /// <summary>
        /// Crea l'area del messaggio
        /// </summary>
        private Border CreateMessageArea()
        {
            var messageBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(40, 40, 40)),
                Padding = new Thickness(20)
            };

            var messageScroll = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
            };

            var messageText = new TextBlock
            {
                Text = Message,
                FontSize = 14,
                FontFamily = new FontFamily("#3ds Light"),
                Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220)),
                TextWrapping = TextWrapping.Wrap,
                LineHeight = 22
            };

            messageScroll.Content = messageText;
            messageBorder.Child = messageScroll;
            return messageBorder;
        }

        /// <summary>
        /// Crea l'area dei pulsanti
        /// </summary>
        private Border CreateButtonArea(Window parentWindow)
        {
            var buttonBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(35, 35, 35)),
                CornerRadius = new CornerRadius(0, 0, 8, 8),
                Padding = new Thickness(20, 10, 20, 10)
            };

            var okButton = new Button
            {
                Content = "OK",
                Width = 100,
                Height = 35,
                Background = GetColorByType(),
                Foreground = Brushes.White,
                BorderThickness = new Thickness(0),
                FontSize = 14,
                FontFamily = new FontFamily("#3ds Light"),
                FontWeight = FontWeights.Medium,
                Cursor = System.Windows.Input.Cursors.Hand,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            okButton.Click += (s, e) => parentWindow.Close();
            buttonBorder.Child = okButton;
            return buttonBorder;
        }

        /// <summary>
        /// Crea uno style per l'hover del pulsante
        /// </summary>
        private Style CreateHoverButtonStyle()
        {
            var style = new Style(typeof(Button));
            var template = new ControlTemplate(typeof(Button));

            return style;
        }

        /// <summary>
        /// Restituisce il colore in base al tipo di messaggio
        /// </summary>
        private Brush GetColorByType()
        {
            switch (Type)
            {
                case MessageType.Error:
                    return new SolidColorBrush(Color.FromRgb(211, 47, 47));
                case MessageType.Warning:
                    return new SolidColorBrush(Color.FromRgb(245, 124, 0));
                case MessageType.Success:
                    return new SolidColorBrush(Color.FromRgb(56, 142, 60));
                case MessageType.Info:
                default:
                    return new SolidColorBrush(Color.FromRgb(25, 118, 210));
            }
        }

        /// <summary>
        /// Restituisce l'icona in base al tipo di messaggio
        /// </summary>
        private string GetIconByType()
        {
            if (!string.IsNullOrEmpty(CustomIcon))
                return CustomIcon;

            switch (Type)
            {
                case MessageType.Error:
                    return "⚠";
                case MessageType.Warning:
                    return "⚡";
                case MessageType.Success:
                    return "✓";
                case MessageType.Info:
                default:
                    return "ℹ";
            }
        }
    }
}

// ===== ESEMPIO DI UTILIZZO =====
/*

// Aggiungi nel tuo progetto:
// 1. Crea una cartella "Controls"
// 2. Aggiungi CustomMessageBox.xaml
// 3. Aggiungi CustomMessageBox.xaml.cs

// Poi usa così:

using YourApp.Controls;

// Messaggio di errore
CustomMessageBox.Show(
    "Si è verificato un errore durante l'operazione.",
    "Errore",
    CustomMessageBox.MessageType.Error
);

// Messaggio di warning
CustomMessageBox.Show(
    "Attenzione: questa operazione non può essere annullata.",
    "Attenzione",
    CustomMessageBox.MessageType.Warning,
    450,
    200
);

// Messaggio di successo
CustomMessageBox.Show(
    "Operazione completata con successo!",
    "Successo",
    CustomMessageBox.MessageType.Success
);

// Gestione Exception
try
{
    // Il tuo codice...
}
catch (Exception ex)
{
    CustomMessageBox.Show(
        $"Errore: {ex.Message}",
        "Errore Applicazione",
        CustomMessageBox.MessageType.Error,
        500,
        300
    );
}

*/
