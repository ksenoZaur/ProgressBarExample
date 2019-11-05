using System;

namespace ProgressBarExample.Models
{
    // Хранит инфомрацию о ходе выполнения процесса
    public class ProgressBarData
    {
        public int Percentage { get; set; }       // Процент выполнения
        public bool IsComplete { get; set; }      // Состояние: выполняется\завершен
        public string Status { get; set; }        // Статус
        public int Total { get; set; }            // Общее число
    }
}