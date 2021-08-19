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
using System.IO;
using AForge.Video;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Video.DirectShow;
using System.Threading;

namespace ProyectoCDM.MVM.View
{
    /// <summary>
    /// Lógica de interacción para ViewSecurityView.xaml
    /// </summary>
    public partial class ViewSecurityView : UserControl
    {
        public ViewSecurityView()
        {
            InitializeComponent();
            loadd();
        }
        private FilterInfoCollection Dispositivos;
        private VideoCaptureDevice FuenteVideo;
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {

                System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();

                MemoryStream memorystream = new MemoryStream();
                img.Save(memorystream, ImageFormat.Bmp);
                memorystream.Seek(0, SeekOrigin.Begin);

                BitmapImage BitMapAux = new BitmapImage();

                BitMapAux.BeginInit();
                BitMapAux.StreamSource = memorystream;
                BitMapAux.EndInit();

                BitMapAux.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                   
                    img1.Source = (BitMapAux);


                }));
            }
            catch (Exception ex)
            {
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)//on
        {
            FuenteVideo = new VideoCaptureDevice(Dispositivos[cbxmascota.SelectedIndex].MonikerString);
            FuenteVideo.NewFrame += new NewFrameEventHandler(video_NewFrame);
            FuenteVideo.Start();
        }
        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
        public static BitmapImage ToBitmapImage(System.Drawing.Image bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;



                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();


                return bitmapImage;
            }
        }
        public void loadd()
        {
            
            Dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            foreach (FilterInfo x in Dispositivos)
            {
                cbxmascota.Items.Add(x.Name);
            }
            cbxmascota.SelectedIndex = 0;
        }

    }
}
