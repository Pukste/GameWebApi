using System;

namespace gamewebapi
{
    public class NewItem
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public ItemType ItemType { get; set; }
    }
}