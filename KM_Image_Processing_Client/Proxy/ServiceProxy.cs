﻿using System;
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
        private ServiceReference.ImageProcessingServiceClient _proxy;

        public ServiceProxy()
        {
            _proxy = new ServiceReference.ImageProcessingServiceClient();   
        }

        public void sendImageProcessingData(Stream image, bool? isHorizontalFlip,
                                            bool? isVerticalFlip, bool? isGrayScale,
                                            bool? isEntropyCrop)
        {
            _proxy.GetImageProcessingData(image, isHorizontalFlip, isVerticalFlip,
                                          isGrayScale, isEntropyCrop);
        }
    }
}
