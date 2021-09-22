namespace Cobalt.Api.Confirm
{
    public interface IConfirmable
    {
        int Timeout { get; }
        void Call();
        
        void Abort();
    }
}