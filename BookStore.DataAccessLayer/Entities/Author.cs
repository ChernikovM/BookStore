﻿using BookStore.DataAccessLayer.Entities.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookStore.DataAccessLayer.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public List<PrintingEdition> PrintingEditions { get; set; }

        public Author()
        {
            PrintingEditions = new List<PrintingEdition>();
        }

    }
}
