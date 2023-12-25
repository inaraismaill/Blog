using Blog.Models;
using Blog.ViewModel.SliderVM;

namespace Blog.ViewModel.HomeVM
{
    public class HomeVM
    {
        public IEnumerable<SliderListItemVM> Sliders { get; set; }
    }
}
