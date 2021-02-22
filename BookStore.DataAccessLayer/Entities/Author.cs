using BookStore.DataAccessLayer.Entities.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookStore.DataAccessLayer.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public List<PrintingEdition> PrintingEditions { get; set; } = new List<PrintingEdition>();
    }
}
