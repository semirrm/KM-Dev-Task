﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KM_Image_Processing_Client.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IImageProcessingService")]
    public interface IImageProcessingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImageProcessingService/ProcessImage", ReplyAction="http://tempuri.org/IImageProcessingService/ProcessImageResponse")]
        byte[] ProcessImage(byte[] imgStream, System.Nullable<bool> isHorizontalFlip, System.Nullable<bool> isVerticalFlip, System.Nullable<bool> isGrayScale, System.Nullable<bool> isEntropyCrop);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IImageProcessingService/ProcessImage", ReplyAction="http://tempuri.org/IImageProcessingService/ProcessImageResponse")]
        System.Threading.Tasks.Task<byte[]> ProcessImageAsync(byte[] imgStream, System.Nullable<bool> isHorizontalFlip, System.Nullable<bool> isVerticalFlip, System.Nullable<bool> isGrayScale, System.Nullable<bool> isEntropyCrop);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IImageProcessingServiceChannel : KM_Image_Processing_Client.ServiceReference1.IImageProcessingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ImageProcessingServiceClient : System.ServiceModel.ClientBase<KM_Image_Processing_Client.ServiceReference1.IImageProcessingService>, KM_Image_Processing_Client.ServiceReference1.IImageProcessingService {
        
        public ImageProcessingServiceClient() {
        }
        
        public ImageProcessingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ImageProcessingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImageProcessingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImageProcessingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public byte[] ProcessImage(byte[] imgStream, System.Nullable<bool> isHorizontalFlip, System.Nullable<bool> isVerticalFlip, System.Nullable<bool> isGrayScale, System.Nullable<bool> isEntropyCrop) {
            return base.Channel.ProcessImage(imgStream, isHorizontalFlip, isVerticalFlip, isGrayScale, isEntropyCrop);
        }
        
        public System.Threading.Tasks.Task<byte[]> ProcessImageAsync(byte[] imgStream, System.Nullable<bool> isHorizontalFlip, System.Nullable<bool> isVerticalFlip, System.Nullable<bool> isGrayScale, System.Nullable<bool> isEntropyCrop) {
            return base.Channel.ProcessImageAsync(imgStream, isHorizontalFlip, isVerticalFlip, isGrayScale, isEntropyCrop);
        }
    }
}
