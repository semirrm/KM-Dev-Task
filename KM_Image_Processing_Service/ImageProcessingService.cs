using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace KM_Image_Processing_Service
{
    public class ImageProcessingService : IImageProcessingService
    {
        private bool? _isHorizontalFlip { get; set; }
        private bool? _isVerticalFlip { get; set; }
        private bool? _isGrayScale { get; set; }
        private bool? _isEntropyCrop { get; set; }

        public void GetProcessingData(bool? isHorizontalFlip, bool? isVerticalFlip, bool? isGrayScale, bool? isEntropyCrop)
        {
            _isHorizontalFlip = isHorizontalFlip;
            _isVerticalFlip = isVerticalFlip;
            _isGrayScale = isGrayScale;
            _isEntropyCrop = isEntropyCrop;
        }

        public Stream ProcessImage(Stream imgStream)
        {
            MemoryStream convertedImageStream;
            using(convertedImageStream = new MemoryStream()) { 
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(imgStream).Save(convertedImageStream);
                    if (_isHorizontalFlip == true)
                    {
                        if(_isVerticalFlip == false)
                            imageFactory.Flip(false);
                        else
                            imageFactory.Flip(true);
                    
                    }
                    if (_isGrayScale == true)
                        imageFactory.Filter(MatrixFilters.GreyScale);
                    if (_isEntropyCrop == true)
                        imageFactory.EntropyCrop();
                }
            }

            return convertedImageStream;
        }
    }
}
