using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Realizations.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Realizations.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IAuthorRepository? _authorRepository;
        private IBookRepository? _bookRepository;
        private IDiscountRepository? _discountRepository;
        private IGenreRepository? _genreRepository;
        private IOrderDetailsRepository? _orderDetailsRepository;
        private IOrderPartRepository? _orderPartRepository;
        private IPublisherRepository? _publisherRepository;
        private IReservationRepository? _reservationRepository;
        private IReservationPartRepository? _reservationPartRepository;

        public IAuthorRepository Author
        {
            get
            {
                _authorRepository ??= new AuthorRepository(_context);
                return _authorRepository;
            }
        }

        public IBookRepository Book
        {
            get
            {
                _bookRepository ??= new BookRepository(_context);
                return _bookRepository;
            }
        }

        public IDiscountRepository Discount
        {
            get
            {
                _discountRepository ??= new DiscountRepository(_context);
                return _discountRepository;
            }
        }

        public IGenreRepository Genre
        {
            get
            {
                _genreRepository ??= new GenreRepository(_context);
                return _genreRepository;
            }
        }

        public IOrderDetailsRepository OrderDetails
        {
            get
            {
                _orderDetailsRepository ??= new OrderDetailsRepository(_context);
                return _orderDetailsRepository;
            }
        }

        public IOrderPartRepository OrderPart
        {
            get
            {
                _orderPartRepository ??= new OrderPartRepository(_context);
                return _orderPartRepository;
            }
        }

        public IPublisherRepository Publisher
        {
            get
            {
                _publisherRepository ??= new PublisherRepository(_context);
                return _publisherRepository;
            }
        }

        public IReservationRepository Reservation
        {
            get
            {
                _reservationRepository ??= new ReservationRepository(_context);
                return _reservationRepository;
            }
        }

        public IReservationPartRepository ReservationPart
        {
            get
            {
                _reservationPartRepository ??= new ReservationPartRepository(_context);
                return _reservationPartRepository;
            }
        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
