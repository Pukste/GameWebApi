using System;

namespace gamewebapi
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Player not found")
        {
            
        }
    }
}