using System;

namespace BookStore.DataAccessLayer.Entities.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsRemoved { get; set; }

        public BaseEntity()
        {
            CreationDate = DateTime.Now;
            IsRemoved = false;
        }
    }
}
