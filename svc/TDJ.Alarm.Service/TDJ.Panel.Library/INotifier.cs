using Newtonsoft.Json.Linq;

namespace TDJ.Panel.Library
{
    public interface INotifier
    {
        JObject SendMessage<T>(T jsonString);
    }
}