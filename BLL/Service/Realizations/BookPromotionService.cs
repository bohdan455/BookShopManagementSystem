using BLL.Dto.Promotion;
using BLL.Service.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Realizations
{
    public class BookPromotionService : IBookPromotionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookPromotionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(PromotionDto promotionDto)
        {
            var discount = new Discount
            {
                ExpirationTime = promotionDto.ExpirationTime,
                AmountInPercent = promotionDto.AmountInPercent,
                Name = promotionDto.Name,
                UserId = promotionDto.UserId,
            };

            await _unitOfWork.Discount.CreateAsync(discount);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.Book.FindByCondition(b => promotionDto.IncludedBooks.Contains(b.Id)).ExecuteUpdateAsync(u =>
                u.SetProperty(b => b.DiscountId,discount.Id));
        }

        public async Task Delete(int id,string userId)
        {
            await _unitOfWork.Discount.FindByCondition(d => d.Id == id && d.UserId == userId).ExecuteDeleteAsync();
        }

        public async Task<decimal> CalculateDiscountOfBook(Book book)
        {
            if (book.Discount == null)
            {
                return book.SellingPrice;
            }

            if (book.Discount.ExpirationTime < DateTime.UtcNow)
            {
                await _unitOfWork.Discount.FindByCondition(d => d.Id == book.DiscountId).ExecuteDeleteAsync();
                return book.SellingPrice;
            }

            return CalculateDiscount(book.SellingPrice, book.Discount.AmountInPercent);
        }

        public async Task<IEnumerable<PromotionBriefInformation>> GetAll(string userId)
        {
            var result = await _unitOfWork.Discount.GetAllAsync(d => d.UserId == userId);

            return result.Select(d => new PromotionBriefInformation
            {
                Id = d.Id,
                Name = d.Name,
                Percent = d.AmountInPercent,
            });
        }

        private decimal CalculateDiscount(decimal price, decimal discount)
        {
            return price * (100 - discount) / 100;
        }


    }
}
