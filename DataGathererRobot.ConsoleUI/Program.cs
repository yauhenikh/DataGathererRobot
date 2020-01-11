using System;
using Autofac;
using DataGathererRobot.WebsiteInformationGatherer;
using DataGathererRobot.WebsiteInformationGatherer.Entities;

namespace DataGathererRobot.ConsoleUI
{
    class Program
    {
        static IContainer Container { get; set; }
        
        static void Main(string[] args)
        {
            Container = BuildContainer();
            var informationGatherer = Container.Resolve<IInformationGatherer>();
            var microwaves = informationGatherer.GatherData<Microwave>();

            Console.ReadLine();
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MicrowavesInformationGatherer>().As<IInformationGatherer>();
            return builder.Build();
        }
    }
}
