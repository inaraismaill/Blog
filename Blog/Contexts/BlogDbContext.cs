using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Contexts
{
    public class BlogDbContext:IdentityDbContext
    {
        public BlogDbContext(DbContextOptions opt ):base( opt) { }
        public DbSet<Slider> Sliders { get; set; }
    }
}