using System;
using Microsoft.AspNetCore.Hosting;
using MyRazorPagesApp;

[assembly: HostingStartup(typeof(MyHostingStartup))]

namespace MyRazorPagesApp
{
    public class MyHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            
        }
    }
}