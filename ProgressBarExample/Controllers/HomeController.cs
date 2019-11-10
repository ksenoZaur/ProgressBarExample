using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
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

        [HttpPost]
        public async Task ImportFile()
        {
            ProgressBarData pgData = new ProgressBarData()
            {
                IsComplete = false,
                Percentage = 0,
                Status = "Starting",
                Total = 0,
            };
            
            string strToSave;
            int size = 10;
            
            // Вычислительная операция, которая занимает много времени
            await Task.Run( ()=>{
                for (int i = 0; i < size; i++)
                {
                    strToSave = Newtonsoft.Json.JsonConvert.SerializeObject(pgData);
                    // Сохранить строку в общую область
                    HttpContext.Cache.Insert("Progress", strToSave);
                    
                    pgData.Total++;
                    pgData.Percentage = (int)((double) pgData.Total / (double) size * 100);
                    pgData.Status = "Processing";
                    // Имитация длительного процесса
                    Thread.Sleep(1000);
                }
                
                pgData.Percentage = (pgData.Total * 100) / pgData.Total;
                pgData.Status = "Completed";
                pgData.IsComplete = true;                
                
                // Сохранить строку в общую область
                strToSave = Newtonsoft.Json.JsonConvert.SerializeObject(pgData);
                HttpContext.Cache.Insert("Progress", strToSave);
             });
        }
        
        [HttpPost]
        public string GetProgress()
        {
            string progress = null;
            // Получить текущий прогресс из общей области
            try
            {
                progress = HttpContext.Cache.Get("Progress").ToString(); 
            }
            catch (Exception e)
            {
                progress = "Не удалось получить информацию о прогрессе!!!";
            }
            return progress;
        }
    }
}