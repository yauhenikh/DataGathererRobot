using System;
using Autofac;
using DataGathererRobot.ExcelDataFileCreator;
using DataGathererRobot.WebsiteInformationGatherer;
using DataGathererRobot.WebsiteInformationGatherer.Entities;

namespace DataGathererRobot.ConsoleUI
{
    internal class Program
    {
        private static IContainer Container { get; set; }

        private static void Main(string[] args)
        {
            Container = BuildContainer();

            var informationGatherer = Container.Resolve<IInformationGatherer>();
            var microwaves = informationGatherer.GatherData<Microwave>();
            Console.WriteLine("Microwaves gathered from website.");

            var excelFileCreator = Container.Resolve<IExcelFileCreator>();
            excelFileCreator.CreateFile(microwaves);
            Console.WriteLine("Excel file with microwaves created.");
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MicrowavesInformationGatherer>().As<IInformationGatherer>();
            builder.RegisterType<MicrowavesExcelFileCreator>().As<IExcelFileCreator>();
            return builder.Build();
        }
    }
}
