using System.IO;
using System.ServiceModel;

namespace KM_Image_Processing_Service
{
    [ServiceContract]
    public interface IImageProcessingService
    {
        [OperationContract]
        byte[] ProcessImage(byte[] imgStream, bool? isHorizontalFlip,
                               bool? isVerticalFlip, bool? isGrayScale,
                               bool? isEntropyCrop);

    }
}
