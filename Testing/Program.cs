using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Addons.Controller;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {

            IAddonController controller = new ControllerFactory().CreateAddonController();

            Console.Write("Insert the path to your WoW root directory: ");
            string rootDir = Console.ReadLine();

            Console.WriteLine("working............\n\n\n");

            var test = controller.GetAddonsAsync($@"{rootDir}\Interface\AddOns");

            test.ForEach(addon => Console.WriteLine($"Addon Title: {addon.Title} \n Addon Description: {addon.Description} \n ---------------------------------------------"));

            Console.ReadKey();
        }
    }
}
