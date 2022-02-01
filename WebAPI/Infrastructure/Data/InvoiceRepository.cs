using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WebAPI.Core.Entities;
using WebAPI.Core.Interfaces;

namespace WebAPI.Infrastructure.Data
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private string connectionString;
        private CsvConfiguration csvConfig;

        public InvoiceRepository(IHostingEnvironment appEnvironment)
        {
            connectionString = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Invoices.csv"}";
            csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            }; 
        }

        public void Create(Invoice newInvoice)
        {
            List<Invoice> records = GetInvoiceList().ToList();
            newInvoice.Id = records.Max(invoice => invoice.Id) + 1;  //Некрасиво, но я не нашел способа автоикремента Id

            using (var stream = File.Open(connectionString, FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    using (var csv = new CsvWriter(writer, csvConfig))
                    {
                        var adding = new List<Invoice>
                        {
                            newInvoice
                        };
                        csv.WriteRecords(adding);
                    }
                }
            }
        }

        public Invoice GetInvoice(int id)
        {
            List<Invoice> records = GetInvoiceList().ToList();

            return records.FirstOrDefault(invoice => invoice.Id == id);
        }

        public IEnumerable<Invoice> GetInvoiceList()
        {
            using (var streamReader = new StreamReader(connectionString))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture, true))
                {
                    var records = csvReader.GetRecords<Invoice>().ToList();
                    if(records.Count>0)
                        return records;
                }
            }
            return Enumerable.Empty<Invoice>();
        }


        public void Update(Invoice updatedInvoice)
        {
            List<Invoice> records = GetInvoiceList().ToList();

            using (var writer = new StreamWriter(connectionString))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture, true))
                {
                    int index = records.FindIndex(invoice => invoice.Id == updatedInvoice.Id);
                    if (index != -1)
                    {
                        records[index] = updatedInvoice;
                        csv.WriteRecords(records);
                    }
                }
            }

            using (var stream = File.Open(connectionString, FileMode.Open))
            {
                
            }
        }
    }
}
