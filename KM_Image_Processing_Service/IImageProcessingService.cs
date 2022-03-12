using System.IO;
using System.ServiceModel;

namespace KM_Image_Processing_Service
{
    [ServiceContract]
    public interface IImageProcessingService
    {
        [OperationContract]
        void GetProcessingData(bool? isHorizontalFlip,
                               bool? isVerticalFlip, bool? isGrayScale,
                               bool? isEntropyCrop);
        [OperationContract]
        Stream ProcessImage(Stream imgStream);

    }
}
