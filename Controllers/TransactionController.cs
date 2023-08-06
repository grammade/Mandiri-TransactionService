using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginationHelper;
using TransactionService.Dtos;
using TransactionService.Entities;
using TransactionService.Persistence;

namespace TransactionService.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly Context _context;
        private readonly IPageHelper _pageHelper;
        public TransactionController(Context context,
            IPageHelper pageHelper)
        {
            _context = context;
            _pageHelper = pageHelper;
        }
        [HttpPost]
        public async Task<ActionResult<Transaction>> checkout(FoodRecOrder model)
        {
            if (model.foods.Count == 0)
                return BadRequest("No food ordered");

            var entity = new Transaction()
            {
                userId = (int)model.userId,
                totalItem = model.foods.Count(),
                Items = model.foods
                    .Select(f => new TransactionItem() { FoodId = f.id, FoodName = f.name })
                    .ToList()
            };

            await _context.Transactions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        [HttpGet]
        public async Task<ActionResult<Envelope<Transaction>>> getListTrans([FromQuery] PaginationDto paginationDto)
        {
            return Ok(await _pageHelper.GetPageAsync(
                 _context.Transactions
                    .Include(t => t.Items)
                    .AsNoTracking(),
                 paginationDto));
        }
    }
}
