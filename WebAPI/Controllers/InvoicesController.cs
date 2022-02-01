using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Core.Entities;
using WebAPI.Core.Interfaces;
using WebAPI.Infrastructure.Data;
using WebAPI.Infrastructure.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IRepository<Invoice> _repo;
        int pageSize = 6; //размер страницы для пагинации (кол-во invoice'ов), по идеи этот параметр должен отправляться вместе с запросом о странице с клиента

        public InvoicesController(IRepository<Invoice> repo)
        {
            _repo = repo;
        }


        //Пагинация реализована на стороне сервера - исходя из соображений быстродейтсвия на клиенте
        //Мог бы и реализовать и на клиенте через Angular Materials
        [HttpGet()]
        [Route("GetList/{page}")]
        public MainPageViewModel Get(int page = 1)
        {
            IEnumerable<Invoice> allRecords = _repo.GetInvoiceList();
            var curruntPageRecords = allRecords.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageInfoModel pageInfoModel = new PageInfoModel(allRecords.Count(), page, pageSize);
            MainPageViewModel viewModel = new MainPageViewModel
            {
                PageInfoModel = pageInfoModel,
                Invoices = curruntPageRecords
            };
            return viewModel;
        }

        [HttpGet()]
        [Route("GetById/{id}")]
        public Invoice GetById(int id)
        {

            Invoice result = _repo.GetInvoice(id);
            if(result != null)
                return result;
            else return null;
        }

        [HttpPost]
        public IActionResult Post(Invoice newInvoice)
        {
            if(ModelState.IsValid)
            {
                IEnumerable<Invoice> allRecords = _repo.GetInvoiceList();
                if(allRecords.FirstOrDefault(i=>i.Id==newInvoice.Id)!=null)
                    _repo.Update(newInvoice);
                else
                    _repo.Create(newInvoice);
                return Ok(newInvoice);
            }
            return BadRequest(ModelState);        
        }



        [HttpPut]
        public IActionResult Update(Invoice updatedInvoice)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(updatedInvoice);
                return Ok(updatedInvoice);
            }
            return BadRequest(ModelState);
        }
    }
}
