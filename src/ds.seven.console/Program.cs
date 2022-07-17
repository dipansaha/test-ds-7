using ds.seven.console;
using ds.seven.core.DI;
using ds.seven.core.Service.Writer;


Console.WriteLine("Welcome to my test 👋");

Utility.Setup();

var writer = DependencyResolver.GetService<IWriter<string>>();

try
{
    Console.WriteLine(await writer.Write());
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.ReadLine();
