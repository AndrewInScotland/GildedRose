namespace GildedRose.IdentityServer
{
	using System;
	using Microsoft.Owin.Hosting;
	using Serilog;

	/// <summary>
	/// The Identity Server startup class.
	/// </summary>
	public class Program
    {
		/// <summary>
		/// Main method of the Startup class.
		/// </summary>
		/// <param name="args">The command-line arguments, if any.</param>
		public static void Main(string[] args)
        {
			const string IdentityServerUrl = "http://localhost:5000";

			// setup up Identity Server's default Serilog logging - in this to the console
			Log.Logger = new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            // start up the identityserver host
	        using (WebApp.Start<Startup>(IdentityServerUrl))
            {
                Console.WriteLine("server running...");
                Console.ReadLine();
            }
        }
    }
}