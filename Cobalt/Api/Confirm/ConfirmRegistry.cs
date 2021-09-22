using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cobalt.Api.Model;

namespace Cobalt.Api.Confirm
{
    public class ConfirmRegistry
    {
        private static ConfirmRegistry _instance;

        public static ConfirmRegistry Instance => _instance ?? (_instance = new ConfirmRegistry());
        
        private readonly Dictionary<IChatSender, IConfirmable> _confirmables = new Dictionary<IChatSender, IConfirmable>();
        private readonly Dictionary<IChatSender, CancellationTokenSource> _cancelTokens = new Dictionary<IChatSender, CancellationTokenSource>();

        public void Register<T>(IChatSender sender, T confirmable) where T : IConfirmable
        {
            if (Has(sender)) Invalidate(sender);
            _confirmables.Add(sender, confirmable);

            var tokenSource = new CancellationTokenSource();
            _ = Task.Delay(confirmable.Timeout*1000, tokenSource.Token).ContinueWith(t => Abort(sender), tokenSource.Token);
            _cancelTokens.Add(sender, tokenSource);
        }
        
        public bool Has(IChatSender sender)
        {
            return _confirmables.ContainsKey(sender);
        }
        
        public IConfirmable Get(IChatSender sender)
        {
            return _confirmables[sender];
        }
        
        public void Call(IChatSender sender)
        {
            if (!Has(sender)) return;
            Get(sender).Call();
            Invalidate(sender);
        }
        
        public void Abort(IChatSender sender)
        {
            if (!Has(sender)) return;
            Get(sender)?.Abort();
            Invalidate(sender);
        }

        private void Invalidate(IChatSender sender)
        {
            _cancelTokens[sender].Cancel();
            _cancelTokens.Remove(sender);
            _confirmables.Remove(sender);
        }
    }
}