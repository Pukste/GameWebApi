using System;
using System.ComponentModel.DataAnnotations;

namespace gamewebapi
{
    public class Item
    {
        
        public Guid Id { get; set; }
        [Range(1,99)]
        public int Level {get; set;}
        public int Price { get; set; }
        [Range(0,2)]
        public ItemType ItemType { get; set; }
        
        [NewerThan]
        public DateTime CreationTime { get; set; }
    }
}