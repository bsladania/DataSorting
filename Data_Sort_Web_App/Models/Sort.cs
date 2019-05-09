using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Data_Sort_Web_App.Models
{
    //model class to bind the data
    public class Sort
    {
        [Required]
        public string input { get; set; }
        public DataType valueType { get; set; }
    }

    public enum DataType
    {
        Int,
        String,
    }
}