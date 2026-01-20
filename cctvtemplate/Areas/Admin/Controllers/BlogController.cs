using cctvtemplate.Context;
using cctvtemplate.Helpers;
using cctvtemplate.Models;
using cctvtemplate.ViewModels.BlogViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace cctvtemplate.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]

public class BlogController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public BlogController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "img");
    }
    public async Task<IActionResult> Index()
    {
        var blogs = await _context.Blogs.Select(b => new BlogGetVm()
        {
            Id = b.Id,
            Description = b.Description,
            PostedDate = b.PostedDate,
            ImageUrl = b.ImageUrl,
            TagName = b.Tag.Name
        }).ToListAsync();

        return View(blogs);
    }

    public async Task<IActionResult> Create()
    {
        await SendTagWithViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BlogCreateVm vm)
    {
        await SendTagWithViewBag();
        if(!ModelState.IsValid)
        {
            return View(vm);
        }

        var isExistTag = await _context.Tags.AnyAsync(t => t.Id == vm.TagId);
        if(!isExistTag)
        {
            ModelState.AddModelError("TagId", "Tag not found!");
            return View(vm);
        }

        if(!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Max size 2mb!!");
            return View(vm);
        }

        if(!vm.Image?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("Image", "Just image type");
            return View(vm);
        }

        string uniqueImagePath = await vm.Image.FileUpload(_folderPath);

        Blog blog = new()
        {
            Description = vm.Description,
            PostedDate = vm.PostedDate,
            ImageUrl = uniqueImagePath,
            TagId = vm.TagId,
        };

        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");

    }

    public async Task<IActionResult> Update(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        await SendTagWithViewBag();

        BlogUpdateVm vm = new()
        {
            Id = blog.Id,
            Description = blog.Description,
            PostedDate = blog.PostedDate,
            TagId = blog.TagId,
        };

        return View(vm);
    }


    [HttpPost]
    public async Task<IActionResult> Update(BlogUpdateVm vm)
    {
        await SendTagWithViewBag();
        if(!ModelState.IsValid)
        {
            return View(vm);
        }

        var existBlog = await _context.Blogs.FindAsync(vm.Id);
        if (existBlog == null)
        {
            return BadRequest();
        }

        var isExistTag =  await _context.Tags.AnyAsync(t => t.Id == vm.TagId);
        if(!isExistTag)
        {
            ModelState.AddModelError("TagId", "This Tag not found");
            return View(vm);
        }


        if (!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Max size 2mb!!");
            return View(vm);
        }

        if (!vm.Image?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("Image", "Just image type");
            return View(vm);
        }

        existBlog.Description = vm.Description;
        existBlog.PostedDate = vm.PostedDate;
        existBlog.TagId = vm.TagId;

        if(vm.Image is { })
        {
            string updateNewImgPath = await vm.Image.FileUpload(_folderPath);

            string deletePath = Path.Combine(_folderPath, existBlog.ImageUrl);


            FileHelper.DeleteFile(deletePath);

            existBlog.ImageUrl = updateNewImgPath;


        }

         _context.Blogs.Update(existBlog);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");



    }


    public async Task<IActionResult> Delete(int id)
    {
        var deletedBlog = await _context.Blogs.FindAsync(id);
        if(deletedBlog == null)
        {
            return NotFound();
        }

        _context.Blogs.Remove(deletedBlog);
        await _context.SaveChangesAsync();

        string deletedPath = Path.Combine(_folderPath, deletedBlog.ImageUrl);

        FileHelper.DeleteFile(deletedPath);

        return RedirectToAction("Index");

    }

    private async Task SendTagWithViewBag()
    {
        var tags = await _context.Tags.Select(t => new SelectListItem() { Text = t.Name ,  Value = t.Id.ToString() }).ToListAsync();
        ViewBag.Tags = tags;
    }
}
