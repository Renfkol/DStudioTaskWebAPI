﻿using CsvHelper.Configuration.Attributes;
using System;

namespace WebAPI.Core.Entities
{
    public class Invoice
    {
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
