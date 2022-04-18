/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore.Configuration;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore
{
    public abstract class CommonDataContext : DbContext
    {
        public ContextSession Session { get; set; }

        public DbSet<UserPhoto> UserPhotos { get; set; }

        public DbSet<Settings> Settings { get; set; }

        protected CommonDataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserPhotoConfig());
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            FillTrackingData();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            FillTrackingData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void FillTrackingData()
        {
            foreach (var item in ChangeTracker.Entries<BaseTrackableEntity>()
                .Where(item => item.State == EntityState.Added || item.State == EntityState.Modified))
            {
                FillTrackingData(item);
            }
        }

        protected void FillTrackingData(EntityEntry<BaseTrackableEntity> entry)
        {
            var now = DateTime.Now;

            entry.Entity.UpdatedByUserId = Session.UserId;
            entry.Entity.UpdatedDate = now;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedByUserId = Session.UserId;
                entry.Entity.CreatedDate = now;
            }
            else
            {
                entry.Property(p => p.CreatedDate).IsModified = false;
                entry.Property(p => p.CreatedByUserId).IsModified = false;
            }
        }
    }
}
