using CsvHelper.Configuration.Attributes;
using System;

namespace WebAPI.Core.Entities
{
    public class Invoice
    {

        //В таске было указало, что модель содержит две даты,
        //но что в указаниях Клиентского Приложения, что исходя из контекста в CSV файла, логично, что используется только одна и она перезаписывается при обновлении?
        [Index(0)]
        public DateTime CreateUpdateDateTime { get; set; }
        [Index(1)]
        public int Id { get; set; }
        [Index(2)]
        public ProcessingStatus ProcessingStatus { get; set; }
        [Index(3)]
        public decimal Sum { get; set; }
        [Index(4)]
        public PaymentMethod PaymentMethod { get; set; }

    }

    public enum ProcessingStatus
    {
        New = 1,
        Paid = 2,
        Canceled = 3
    }

    public enum PaymentMethod
    {
        CreditCard = 1,
        DebitCard = 2,
        ElectronicСheck = 3
    }
}
