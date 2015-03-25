using System;
using System.Threading.Tasks;

namespace Zakar.Common.Builders
{
    public interface IMessageBuilder<T, U>
    {
        U BuildMessage(T messageRecipient);
    }
}
