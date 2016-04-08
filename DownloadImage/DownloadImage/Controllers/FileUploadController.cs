using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DownloadImage.Hubs;
using DownloadImage.Models;
using DownloadImage.Services;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;

namespace DownloadImage.Controllers
{
    public class FileUploadController : ApiController
    {
        private void SendMessage(ImageModel message)
        {
            var context =
                GlobalHost.ConnectionManager.GetHubContext<AppHub>();

            context.Clients.All.displayMessage(message);
        }

        public async Task<IHttpActionResult> PostFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/DownloadedImages");

            var provider = new CustomMultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var imgData = File.ReadAllBytes(file.LocalFileName);

                    var message = new ImageModel
                    {
                        Name = file.Headers.ContentDisposition.FileName,
                        StatusCode = HttpContext.Current.Response.StatusCode.ToString(),
                        ImageData = imgData
                    };
                    SendMessage(message);
                }

                return Json(new EmptyResult());
            }
            catch (Exception ex)
            {
                var message = new ImageModel
                {
                    StatusCode = HttpContext.Current.Response.StatusCode.ToString()
                };
                string errResponse = "{'error': 'InternalServerError.'}";
                JObject json = JObject.Parse(errResponse);
                SendMessage(message);
                return Json(json);
            }
        }
    }
}