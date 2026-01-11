using System.Threading.Tasks;

namespace lib_domain_context
{
    public enum Message { MESSAGE, QUESTION };

    public interface IMessage
    {
        object? Show(object? message, Message type = Message.MESSAGE);
        Task<object?> AsyncShow(object? message, Message type = Message.MESSAGE);
    }

    public class MessagesHelper
    {
        private static IMessage? IMessage;

        public static void Set(IMessage? iMessage) { IMessage = iMessage; }

        public static object? Show(object? message, Message type = Message.MESSAGE)
        {
            if (IMessage == null)
                return false;
            return IMessage.Show(message, type);
        }

        public static async Task<object?> AsyncShow(object? message, Message type = Message.MESSAGE)
        {
            if (IMessage == null)
                return false;
            return await IMessage.AsyncShow(message, type);
        }
    }
}