using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProgressBarExample.Models;

namespace ProgressBarExample.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            //await ImportFile();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        private async Task ImportFile()
        {
            ProgressBarData pgData = new ProgressBarData();
            pgData.IsComplete = false;
            pgData.Percentage = 0;
            pgData.Status = "Starting";
            pgData.Total = 0;
            string strToSave;
            int size = 10;
            
            await Task.Run( ()=>{
                for (int i = 0; i < size; i++)
                {
                    strToSave = Newtonsoft.Json.JsonConvert.SerializeObject(pgData);
                    Console.WriteLine(strToSave);
                    pgData.Total++;
                    pgData.Percentage = (int)((double) pgData.Total / (double) size * 100);
                    pgData.Status = "Processing";
                    // Сохранить строку в общую область
                    Thread.Sleep(1000);
                }
                pgData.Percentage = (pgData.Total * 100) / pgData.Total;
                pgData.Status = "Completed";
                pgData.IsComplete = true;                
                
                strToSave = Newtonsoft.Json.JsonConvert.SerializeObject(pgData);
                Console.WriteLine(strToSave);
                // Сохранить строку в общую область
            });
        }
        
        [HttpPost]
        public string GetProgress()
        {    
            // Получить текущий прогресс из общей области
            return Newtonsoft.Json.JsonConvert.SerializeObject(new ProgressBarData()
            {
                Percentage = 25,
                Status = "Processing",
                Total = 5,
                IsComplete = false
            });
        }
    }
}