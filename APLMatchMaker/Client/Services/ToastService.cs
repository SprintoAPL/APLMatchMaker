namespace APLMatchMaker.Client.Services
{
    public class ToastService
    {
        public event Action<string, ToastLevel>? Show;

        public void ShowToast(string message, ToastLevel level)
        {
            Show!.Invoke(message, level);
        }
    }
}
