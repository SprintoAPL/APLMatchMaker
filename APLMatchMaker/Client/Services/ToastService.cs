namespace APLMatchMaker.Client.Services
{
    public class ToastService
    {
        public event Action<string, ToastLevel>? OnShow;

        public void ShowToast(string message, ToastLevel level)
        {
            OnShow!.Invoke(message, level);
        }
    }

}
