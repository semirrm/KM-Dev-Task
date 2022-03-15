using System.ServiceModel;

namespace KM_Image_Processing_Service
{
    [ServiceContract]
    public interface IImageProcessingService
    {
        /// <summary>
        /// Service  processing a image in the particular options.
        /// </summary>
        /// <param name="imgStream">The image stream to be processed.</param>
        /// <param name="isHorizontalFlip">If true image will be flipped horizontally.</param>
        /// <param name="isVerticalFlip">If true image will be flipped vertically.</param>
        /// <param name="isGrayScale">If true gray scale filter will be applied to the image.</param>
        /// <param name="isEntropyCrop">If true entropy crop will be applied to the image.</param>
        /// <returns>The image stream of the processed image.</returns>
        [OperationContract]
        byte[] ProcessImage(byte[] imgStream, bool? isHorizontalFlip,
                               bool? isVerticalFlip, bool? isGrayScale,
                               bool? isEntropyCrop);

    }
}
