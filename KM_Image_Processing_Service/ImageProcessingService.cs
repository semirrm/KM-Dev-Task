using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using System;
using System.IO;

namespace KM_Image_Processing_Service
{
    public class ImageProcessingService : IImageProcessingService
    {
        public byte[] ProcessImage(byte[] imgStream, bool? isHorizontalFlip, bool? isVerticalFlip, bool? isGrayScale, bool? isEntropyCrop)
        {
            MemoryStream convertedImageStream;

            using(convertedImageStream = new MemoryStream(imgStream)) { 
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(imgStream);
                    if (isHorizontalFlip == true)
                    {
                        WriteToFile("Image flipped horizontally");
                        if (isVerticalFlip == false)
                            imageFactory.Flip(false);
                        else
                        {
                            imageFactory.Flip(true);
                            WriteToFile("Image flipped vertically");
                        }
                    
                    }
                    else
                    {
                        if (isVerticalFlip == true) { 
                            imageFactory.Flip(true);
                            imageFactory.Flip(false);
                            WriteToFile("Image flipped vertically");
                        }
                    }

                    if (isGrayScale == true) { 
                        imageFactory.Filter(MatrixFilters.GreyScale);
                        WriteToFile("Grey Scale filter applied to image.");
                    }
                    if (isEntropyCrop == true) { 
                        imageFactory.EntropyCrop();
                        WriteToFile("Entropy crop applied to image.");
                    }
                    imageFactory.Save(convertedImageStream);
                }
            }
            WriteToFile("Processed image sent to client.");
            return convertedImageStream.ToArray();
        }

        private void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";  
            if (!Directory.Exists(path)) {  
                Directory.CreateDirectory(path);  
            }  
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";  
            if (!File.Exists(filepath)) {  
                using(StreamWriter sw = File.CreateText(filepath)) {  
                    sw.WriteLine(Message);  
                }  
            } else {  
                using(StreamWriter sw = File.AppendText(filepath)) {  
                    sw.WriteLine(Message);  
                }  
            }
        }
    }
}
