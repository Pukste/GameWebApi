using System;
using System.ComponentModel.DataAnnotations;

namespace gamewebapi{
    public class NewerThanAttribute : ValidationAttribute {
        private DateTime _time = DateTime.Now;
        int timedifference;
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            
            DateTime _objectTime = (DateTime)validationContext.ObjectInstance;
            timedifference = DateTime.Compare(_objectTime, _time);
            if(timedifference<2 ){
                return new ValidationResult("Creation time is in the past!");
            }



            return ValidationResult.Success;

            
            
            
            
        }
    }
}