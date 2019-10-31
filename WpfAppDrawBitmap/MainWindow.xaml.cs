using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfAppDrawBitmap
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Calculate pixel dimensions so we can draw pixel-perfect. https://stackoverflow.com/a/3450426
            var pixelDimensions = PresentationSource.FromVisual(image).CompositionTarget.TransformToDevice.Transform((Vector)image.DesiredSize);
            var width = (int)pixelDimensions.X;
            var height = (int)pixelDimensions.Y;
            using var bitmap = new Bitmap(width, height);
            using var g = Graphics.FromImage(bitmap);
            g.Clear(Color.Red);
            g.FillRectangle(Brushes.Green, 0, 0, width / 2, height / 2);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            using var pngStream = new MemoryStream();
            bitmap.Save(pngStream, ImageFormat.Png);
            pngStream.Position = 0;
            bitmapImage.StreamSource = pngStream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            image.Source = bitmapImage;
        }
    }
}
