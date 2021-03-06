using System.Collections.Generic;
using WebAPI.Core.Entities;
using WebAPI.Infrastructure.Data;

namespace WebAPI.Infrastructure.ViewModel
{
    //Вьюмодель, отправляемая на клиент 
    public class MainPageViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }
        public PageInfoModel PageInfoModel { get; set; }
    }
}
