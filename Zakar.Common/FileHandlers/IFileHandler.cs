using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Zakar.Common.FileHandlers
{
    public interface IFileHandler
    {
        string HandleFile(string fileName, Stream fileStream);
    }
}