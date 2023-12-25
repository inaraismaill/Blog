using Blog.Contexts;
using Blog.Helpers;
using Blog.Models;
using Blog.ViewModel.SliderVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewPustok.Helpers;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class SliderController : Controller
    {
        BlogDbContext _db { get; }
        IWebHostEnvironment _environment {  get; }
        public SliderController(BlogDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var slider=await _db.Sliders.Select(s=>new SliderListItemVM
            {
                ImageUrl = s.ImageUrl,
                Id = s.Id,
                MainText = s.MainText,
                Text = s.Text,
            }).ToListAsync();
            return View(slider);
        }
        
        public async Task<IActionResult> Create()
        {
            var phrasesCount = await _db.Sliders.CountAsync();
            if (phrasesCount==0) return View(); 
            return BadRequest();
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        { 
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Slider slider = new Slider
            {
                Text = vm.Text,
                MainText    = vm.MainText,
                ImageUrl = await vm.ImageFile.SaveAsync(PathConstants.Product)
            };
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var vm = await _db.Sliders.FindAsync(id);
            if (vm == null) return RedirectToAction(nameof(Index));
            return View(new SliderUpdateVM
            {
                Text = vm.Text,
                MainText =vm.MainText
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,SliderUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data= await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.MainText = vm.MainText;
            data.Text = vm.Text;
            if (data.ImageUrl != null && data.ImageUrl.Length > 0)
            {
                data.ImageUrl = await vm.ImageFile.SaveAsync(PathConstants.Product);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return RedirectToAction(nameof(Index));
            _db.Sliders.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
