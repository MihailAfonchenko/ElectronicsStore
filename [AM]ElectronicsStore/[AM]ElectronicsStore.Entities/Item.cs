using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _AM_ElectronicsStore.Entities
{
    public class Item
    {
        [Display(Name = "№")]
        public int Id { get; set; }

        [Display(Name = "Категория")]
        public Category Category { get; set; }

        [Required(ErrorMessage ="Укажите название")]
        [Display(Name = "Краткое название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Укажите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество в магазине")]
        public int Count { get; set; }

        public string ImagePath { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public Item() {}

        public Item(Category category, string name, string description, decimal price, int count, string imagePath, int id = 0)
        {
            Category = category;
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Count = count;
            ImagePath = imagePath;
        }
    }
}
