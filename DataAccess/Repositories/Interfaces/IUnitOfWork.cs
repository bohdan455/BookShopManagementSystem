using DataAccess.Repositories.Realizations.Main;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorRepository Author { get; }
        IBookRepository Book { get; }
        IDiscountRepository Discount { get; }
        IGenreRepository Genre { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderPartRepository OrderPart { get; }
        IPublisherRepository Publisher { get; }
        IReservationRepository Reservation { get; }
        IReservationPartRepository ReservationPart { get; }

        void Commit();
        void CreateTransaction();
        void Rollback();
        void Save();
        Task SaveAsync();
    }
}