using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class GuestFavoritesRepository : DataRepositoryBase<GuestFavorites>, IGuestFavoritesRepository
    {
        protected override GuestFavorites AddEntity(MyMonkeyCapContext entityContext, GuestFavorites entity)
        {
            return entityContext.GuestFavoritesSet.Add(entity);
        }

        protected override GuestFavorites UpdateEntity(MyMonkeyCapContext entityContext, GuestFavorites entity)
        {
            return (from e in entityContext.GuestFavoritesSet
                    where e.GuestFavouritesKey == entity.GuestFavouritesKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GuestFavorites> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.GuestFavoritesSet
                    select e);
        }

        protected override GuestFavorites GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.GuestFavoritesSet
                         where e.GuestFavouritesKey == key
                         select e);

            var results = query.FirstOrDefault();
            return results;
        }
        public IEnumerable<GuestFavorites> GetAllFavorites(string guestKey)
        {
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                return (from e in entityContext.GuestFavoritesSet
                        where e.GuestKey == guestKey
                        && e.IsDeleted == false
                        select e).ToList();
            }
        }
    }
}
