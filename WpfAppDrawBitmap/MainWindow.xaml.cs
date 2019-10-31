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
            using var bitmap = new Bitmap((int)Width, (int)Height);
            using var g = Graphics.FromImage(bitmap);
            g.Clear(Color.Red);
            g.FillRectangle(Brushes.Green, 0, 0, (float)Width / 2, (float)Height / 2);
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
