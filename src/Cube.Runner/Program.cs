using Cube.Api.Network;
using Cube.Runner.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cube.Runner;

class Program
{
    private static readonly String _asciiArt = @"
 _______           ______   _______ 
(  ____ \|\     /|(  ___ \ (  ____ \
| (    \/| )   ( || (   ) )| (    \/
| |      | |   | || (__/ / | (__    
| |      | |   | ||  __ (  |  __)   
| |      | |   | || (  \ \ | (      
| (____/\| (___) || )___) )| (____/\
(_______/(_______)|/ \___/ (_______/
                                    ";

    private static readonly String _developers = "Merijn#5647";
    
    static Task Main(string[] args)
    {
        Console.WriteLine(_asciiArt);
        Console.WriteLine($"Cube Emulator is written by {_developers}");
        
        using IHost host = CreateHostBuilder(args).Build();
        
        // Get ServerRunner from DI container
        var serverRunner = host.Services.GetRequiredService<IServerRunner>();
        
        // Start the server
        serverRunner.StartAsync().Wait();
        
        return Task.CompletedTask;
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => AppServiceCollection.Load(services));
}