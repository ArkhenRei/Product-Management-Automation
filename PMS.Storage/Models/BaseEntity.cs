﻿namespace PMS.Storage.Models
{
    public class BaseEntity<TPrimaryKey>        
    {
        public virtual TPrimaryKey? Id { get; set; }
    }
}
