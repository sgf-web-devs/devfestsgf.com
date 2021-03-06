# HorseStrap

![alternativetext](https://horsestrap.com/images/nily.svg)

Requirements are

- .NET Core 2.x
- NodeJS(>=8)

To download .NET Core, grab the runtime

Mac - https://www.microsoft.com/net/download/macos
Windows - https://www.microsoft.com/net/download/windows/build

Install and confirm you have access to it globally by running `dotnet --version`
You shoudl be running 2.1 or greater.

As long as Node is installed as well, you shoudl be good to roll.

CD into the project folder and run `npm install`

Once all of the node modules are installed, run `dotnet run` which will start the kestrel web server. With this running you can run the website.

If you want to work on the site, in a second terminal window run `npm run watch` and this will proxy the kestrel site and launch a browser window with browsersync. This will allow live updates to cshtml, css, js, to be pushed to the browser. .cs changes will NOT livereload.

Static site builds are currently in the works. To utilize an early version of the feature, point your browser to http://localhost:5000/GenerateStaticSite - Click submit and a static directory will be created at the root of the project folder. IMPORTANT: When running through the browsersync proxy, the URL field used in the site generation does not format properly, so be sure to use the Kestrel served URL directly on port 5000.