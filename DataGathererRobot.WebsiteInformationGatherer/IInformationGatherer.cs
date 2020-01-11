using System.Collections.Generic;

namespace DataGathererRobot.WebsiteInformationGatherer
{
    public interface IInformationGatherer
    {
        List<T> GatherData<T>();
    }
}
