using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KM_Image_Processing_Client.Proxy
{
    class ServiceProxy
    {

        private ServiceReference1.ImageProcessingServiceClient _proxy;

        public ServiceProxy()
        {
            _proxy = new ServiceReference1.ImageProcessingServiceClient();
        }

        public byte[] sendImageProcessingData(byte[] imageStream, bool? isHorizontalFlip,
                                            bool? isVerticalFlip, bool? isGrayScale,
                                            bool? isEntropyCrop)
        {

            byte[] str = _proxy.ProcessImage(imageStream, isHorizontalFlip, isVerticalFlip,
                                      isGrayScale, isEntropyCrop);
            
            return str;
        }
    }
}
