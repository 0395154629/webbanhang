using PNPStore.Models;
using System.ComponentModel.DataAnnotations;

namespace PNPStore.ModelView.Product
{
    public class Product
    {
        [Required(ErrorMessage = "Trường id loại sản phẩm không thể bỏ trống")]
        public int IdCate { get; set; }
        [Required(ErrorMessage = "Trường Tên sản phẩm không thể bỏ trống")]
        public string NameProduct { get; set; }
        public string? Color { get; set; }
        [Required(ErrorMessage = "Trường Price thể bỏ trống")]
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public string? CPU { get; set; }
        public string? RAM { get; set; }
        public string? Graphics { get; set; }
        public string? Disk { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Trường số lượng không thể bỏ trống")]
        public int Quantity { get; set; }
        public string? IdUserUpdate { get; set; }
    }
}
