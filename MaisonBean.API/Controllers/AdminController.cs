//using MaisonBean.Infrastructure.Persistence;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace MaisonBean.API.Controllers;

//[ApiController]
//[Route("api/admin")]
//[Authorize(Roles = "ADMIN")]
//public class AdminController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;

//    public AdminController(
//        ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // ========================================
//    // DASHBOARD STATS
//    // ========================================

//    [HttpGet("dashboard")]
//    public async Task<IActionResult> Dashboard()
//    {
//        var users =
//            await _context.Users.CountAsync();

//        var products =
//            await _context.Products.CountAsync();

//        var orders =
//            await _context.Orders.CountAsync();

//        var revenue =
//            await _context.Orders
//                .SumAsync(x => x.TotalAmount);

//        return Ok(new
//        {
//            users,
//            products,
//            orders,
//            revenue
//        });
//    }

//    // ========================================
//    // USERS
//    // ========================================

//    [HttpGet("users")]
//    public async Task<IActionResult> Users()
//    {
//        var users =
//            await _context.Users
//                .Select(x => new
//                {
//                    x.Id,
//                    x.FirstName,
//                    x.LastName,
//                    x.Email
//                })
//                .ToListAsync();

//        return Ok(users);
//    }

//    // ========================================
//    // ORDERS
//    // ========================================

//    [HttpGet("orders")]
//    public async Task<IActionResult> Orders()
//    {
//        var orders =
//            await _context.Orders
//                .Include(x => x.Items)
//                .OrderByDescending(x => x.CreatedAt)
//                .ToListAsync();

//        return Ok(orders);
//    }
//}