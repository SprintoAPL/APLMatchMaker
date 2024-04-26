using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Services
{
    public interface IFeedBackService
    {
        event Action<FeedBack> OnFeedBackRecived;
        void PublishFeedBack(FeedBack feedBack);
    }

    public class FeedBackService : IFeedBackService
    {
        public event Action<FeedBack>? OnFeedBackRecived;

        public void PublishFeedBack(FeedBack feedBack)
        {
            OnFeedBackRecived?.Invoke(feedBack);
        }
    }

    public class FeedBack
    {
        public bool IsVisible { get; set; }

        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        //Add constructor
        public FeedBack(bool isVisible, bool isSuccess, string? message)
        {
            IsVisible = isVisible;
            IsSuccess = isSuccess;
            Message = message;
        }
    }


}
