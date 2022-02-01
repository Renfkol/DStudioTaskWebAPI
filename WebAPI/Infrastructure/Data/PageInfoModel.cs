﻿using System;

namespace WebAPI.Infrastructure.Data
{
    public class PageInfoModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageInfoModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}