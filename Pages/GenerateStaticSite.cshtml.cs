using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CareToLearnUI.Classes;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;

namespace CareToLearnUI.Pages
{
    public class GenerateStaticSiteModel : PageModel
    {
        private IHostingEnvironment _hostingEnvironment;

        [BindProperty]
        public String BaseURL { get; set; }
        [BindProperty]
        public string SiteSaveLocation { get; set; }
        [BindProperty]
        public String Root { get; set; }
        [BindProperty]
        public string Exclusions { get; set; }

        public string Messages { get; set; }

        public String[] FilesDownloaded { get; set; }

        public GenerateStaticSiteModel(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            Exclusions = "_,cshtml.cs,Partials,GenerateStaticSite";
            SiteSaveLocation = _hostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + "static" + Path.DirectorySeparatorChar;
            Root = _hostingEnvironment.WebRootPath;
        }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            IList<String> filesDld = new List<String>();

            using (WebClient client = new WebClient())
            {
                //verify locations and build folders
                KeyValuePair<bool, string> status = StaticSiteGeneration.VerifyLocations(new string[] { SiteSaveLocation, Root });

                if (status.Key)
                {
                    if (StaticSiteGeneration.CopyDirectory(Root, SiteSaveLocation))
                    {
                        foreach (string f in StaticSiteGeneration.GetFiles(Exclusions.Split(",")))
                        {
                            var file = f.Split("Pages")[1].Replace("/", "").Replace(@"\", "").Replace(".cshtml", "");
                            var url = "http:" + BaseURL + file;
                            var saveLocationFolder = SiteSaveLocation + file;
                            var saveLocationIndex = saveLocationFolder + "/index.html";

                            try
                            {
                                string save = StaticSiteGeneration.PrepForDownload(SiteSaveLocation, file);
                                client.DownloadFile(url, save);
                                filesDld.Add(file);
                            }
                            catch (Exception ex)
                            {
                                var i = ex;
                            }
                        }
                    }
                }
            }

            return Page();
        }
    }
}