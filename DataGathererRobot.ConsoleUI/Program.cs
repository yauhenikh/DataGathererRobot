using System;
using System.Collections.Generic;
using Autofac;
using DataGathererRobot.EmailFileSender;
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

            List<Microwave> microwaves = new List<Microwave>();

            var informationGatherer = Container.Resolve<IInformationGatherer>();
            try
            {
                microwaves = informationGatherer.GatherData<Microwave>();
            }
            catch (Exception ex)
            {
                HandleError("Impossible to gather data from website.", ex.Message);
            }

            Console.WriteLine("Microwaves gathered from website.");

            var excelFileCreator = Container.Resolve<IExcelFileCreator>();
            try
            {
                excelFileCreator.CreateFile(microwaves);
            }
            catch (Exception ex)
            {
                HandleError("Impossible to create excel file.", ex.Message);
            }

            Console.WriteLine("Excel file with microwaves created.");

            var emailExcelFileSender = Container.Resolve<IEmailFileSender>();
            try
            {
                emailExcelFileSender.SendFile(args[0]);
            }
            catch (Exception ex)
            {
                HandleError("Impossible to send file via email.", ex.Message);
            }

            Console.WriteLine($"Excel file sent to {args[0]}.");
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MicrowavesInformationGatherer>().As<IInformationGatherer>();
            builder.RegisterType<MicrowavesExcelFileCreator>().As<IExcelFileCreator>();
            builder.RegisterType<EmailExcelFileSender>().As<IEmailFileSender>();
            return builder.Build();
        }

        private static void HandleError(string errorMessage, string exceptionMessage)
        {
            Console.WriteLine(errorMessage);
            Console.WriteLine(exceptionMessage);
            Environment.Exit(1);
        }
    }
}
