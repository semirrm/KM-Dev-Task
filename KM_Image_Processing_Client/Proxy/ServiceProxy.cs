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

        #region Constructor
        public ServiceProxy()
        {
            _proxy = new ServiceReference1.ImageProcessingServiceClient();
        }
        #endregion

        #region Public
        /// <summary>
        /// Sends the processing data to the service.
        /// </summary>
        /// <param name="imageStream">byte[] of the image, which will be processed.</param>
        /// <param name="isHorizontalFlip">If true image will be horizontally flipped.</param>
        /// <param name="isVerticalFlip">If true image will be vertically flipped.</param>
        /// <param name="isGrayScale">If true grey scale filter will be applied to the image.</param>
        /// <param name="isEntropyCrop">If true entropy crop will be applied to the image.</param>
        /// <returns>byte[] of the processed image.</returns>
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
