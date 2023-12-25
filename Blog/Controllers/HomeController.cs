using Blog.Contexts;
using Blog.Models;
using Blog.ViewModel.HomeVM;
using Blog.ViewModel.SliderVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        BlogDbContext _db { get; }
        public HomeController(BlogDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {

            HomeVM vm = new HomeVM
            {

                Sliders = await _db.Sliders.Select(s => new SliderListItemVM
                {
                    Id = s.Id,
                    Text=s.Text,
                    MainText = s.MainText,
                    ImageUrl = s.ImageUrl
                }).ToListAsync()
            };
            return View(vm);
        }
    }
}