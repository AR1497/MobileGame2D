using System.Collections.Generic;

namespace Model.Analytic
{
    public interface IAnalyticTools
    {
        void SendMessage(string alias, IDictionary<string, object> eventData = null);
    }
}
