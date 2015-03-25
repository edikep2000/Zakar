using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zakar.ViewModels;

namespace Zakar.Common.Messaging
{

    public interface IMessageSender
    {
        string SendMessage(ISMSMessage message);
    }
}
