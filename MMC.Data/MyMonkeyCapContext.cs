﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MMC.Business.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Serialization;
using Core.Common.Contracts;

namespace MMC.Data
{
    public class MyMonkeyCapContext: DbContext
    {
        public MyMonkeyCapContext()
            : base("name=monkeycapEntities")
        {
            Database.SetInitializer<MyMonkeyCapContext>(null);
        }
        /// <summary>
        /// DbSet represents the collection of entities that we want to query
        /// </summary>
        public DbSet<ActivitiesMaster> ActivitiesMasterSet { get; set; }
        public DbSet<ActivityCategoryMaster> ActivityCategoryMasterSet { get; set; }
        public DbSet<LocationsMaster> LocationMasterSet { get; set; }
        public DbSet<ActivityBooking> ActivityBookingSet { get; set; }
        public DbSet<ActivityCompany> ActivityCompanySet { get; set; }
        public DbSet<ActivityDayScheduler> ActivityDaySchedulerSet { get; set; }
        public DbSet<ActivityDiscount> ActivityDiscountSet { get; set; }
        public DbSet<ActivityHolidays> ActivityHolidaysSet { get; set; }
        public DbSet<ActivityLocation> ActivityLocationSet { get; set; }
        public DbSet<ActivityRates> ActivityRatesSet { get; set; }
        public DbSet<ActivityTimeScheduler> ActivityTimeSchedulerSet { get; set; }
        public DbSet<ActivityTypeMaster> ActivityTypeMasterSet { get; set; }
        public DbSet<CompanyMaster> CompanyMasterSet { get; set; }
        public DbSet<TouristSeasons> TouristSeasonsSet { get; set; }
        public DbSet<GuestInformationMaster> GuestInformationMasterSet { get; set; }
        public DbSet<Account> AccountSet { get; set; }
        public DbSet<TopOffers> TopOffersSet { get; set; }
        public DbSet<ActivityImages> ActivityImagesSet { get; set; }
        public DbSet<UserApplicationActivityDetails> UserApplicationActivityDetailsSet { get; set; }
        public DbSet<ActivityDates> ActivityDatesSet { get; set; }
        public DbSet<GuestFavorites> GuestFavoritesSet { get; set; }
        public DbSet<ActivityTypeCategory> ActivityTypeCategorySet { get; set; }
        public DbSet<LocationDetails> LocationDetailsSet { get; set; }
        public DbSet<TopOfferMapping> TopOfferMappingSet { get; set; }
        public DbSet<ActivityPriceMapping> ActivityPriceMappingSet { get; set; }
        public DbSet<UserCompanyMapping> UserCompanyMappingSet { get; set; }

        public DbSet<ActivityTagMapping> ActivityTagMappingSet { get; set; }

        public DbSet<RegisteredUsers> RegisteredUsersSet { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();
            ///Setting primary keys. Also, ignoring attributes that 
            ///are not mapped to DB entities
            modelBuilder.Entity<ActivitiesMaster>().HasKey<string>(e => e.ActivitesKey).Ignore<string>(e => e.EntityId);            
            modelBuilder.Entity<ActivityBooking>().HasKey<string>(e => e.ActivityBookingKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityCompany>().HasKey<string>(e => e.ActivityCompanyKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityDayScheduler>().HasKey<string>(e => e.ActivityDaySchedulerKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityDiscount>().HasKey<string>(e => e.ActivityDiscountKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityHolidays>().HasKey<string>(e => e.ActivityHolidayKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityLocation>().HasKey<string>(e => e.ActivityLocationKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityRates>().HasKey<string>(e => e.ActivityRatesKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityTimeScheduler>().HasKey<string>(e => e.ActivityTimeSchedulerKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityTypeMaster>().HasKey<string>(e => e.ActivityTypeKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityCategoryMaster>().HasKey<string>(e => e.ActivityCategoryKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<CompanyMaster>().HasKey<string>(e => e.CompanyKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<GuestInformationMaster>().HasKey<string>(e => e.GuestKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<LocationsMaster>().HasKey<string>(e => e.LocationKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<TouristSeasons>().HasKey<string>(e => e.TouristSeasonKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<Account>().HasKey<string>(e => e.AccountKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<TopOffers>().HasKey<string>(e => e.TopOffersKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<ActivityImages>().HasKey<string>(e => e.ActivityImageKey).Ignore<string>(e => e.EntityId);
            modelBuilder.Entity<UserApplicationActivityDetails>().HasKey<string>(e => e.SessionKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<ActivityDates>().HasKey<string>(e => e.ActivityDatesKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<GuestFavorites>().HasKey<string>(e => e.GuestFavouritesKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<ActivityTypeCategory>().HasKey<string>(e => e.ActivityTypeCategoryKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<LocationDetails>().HasKey<string>(e => e.LocationDetailsKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<TopOfferMapping>().HasKey<string>(e => e.TopOfferMappingKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<ActivityPriceMapping>().HasKey<string>(e => e.ActivityPricingKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<UserCompanyMapping>().HasKey<string>(e => e.UserCompanyKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<ActivityTagMapping>().HasKey<string>(e => e.ActivityTagKey).Ignore(e => e.EntityId);
            modelBuilder.Entity<RegisteredUsers>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
