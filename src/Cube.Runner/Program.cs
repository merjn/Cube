using Cube.Api.Network;
using Cube.Network;
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

CubeEmu is a free and open source emulator for Habbo Hotel.
It's used for educational purposes only and is not affiliated with Sulake Corporation Oy.
";
    
    static Task Main(string[] args)
    {
        Console.WriteLine(_asciiArt);
        
        using IHost host = CreateHostBuilder(args).Build();
        
        // Get ServerRunner from DI container
        var serverRunner = host.Services.GetRequiredService<ServerBooter>();
        
        // Start the server
        serverRunner.RunAsync().Wait();
        
        return Task.CompletedTask;
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => AppServiceCollection.Load(services));
}