using RapidBot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidBot.Data.Repositories
{
    public class RapidBotUoW : IRapidBotUoW, IDisposable
    {
        readonly RapidBotDataEntities _context = new RapidBotDataEntities();
        private bool disposed = false;
        private BaseRepository<Customer> _customer;
        private BaseRepository<Order> _order;
        private BaseRepository<OrderItem> _orderItem;
        private BaseRepository<Product> _product;
        private BaseRepository<Supplier> _supplier;

        #region Properties
        public BaseRepository<Customer> CustomerRepository
        {
            get
            {
                return this._customer ?? (this._customer = new BaseRepository<Customer>(_context));
            }
        }

        public BaseRepository<Order> OrderRepository
        {
            get
            {
                return this._order ?? (this._order = new BaseRepository<Order>(_context));
            }
        }

        public BaseRepository<OrderItem> OrderItemRepository
        {
            get
            {
                return this._orderItem ?? (this._orderItem = new BaseRepository<OrderItem>(_context));
            }
        }

        public BaseRepository<Product> ProductRepository
        {
            get
            {
                return this._product ?? (this._product = new BaseRepository<Product>(_context));
            }
        }

        public BaseRepository<Supplier> SupplierRepository
        {
            get
            {
                return this._supplier ?? (this._supplier = new BaseRepository<Supplier>(_context));
            }
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        #region GC
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
