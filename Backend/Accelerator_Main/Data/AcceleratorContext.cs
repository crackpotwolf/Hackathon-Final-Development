using Data.Models.DB.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Context Db
    /// </summary>
    public class AcceleratorContext : DbContext
    {
        /// <inheritdoc />
        public AcceleratorContext(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc />
        public AcceleratorContext()
        {
        }

        #region Таблицы в БД

        #region Account
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        #endregion

        #endregion

        /// <summary>
        /// Связи
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Связи: один к одному
            #endregion

            #region Связи: многие ко многим

            #region Пользователи и роли

            modelBuilder.Entity<UserRoles>()
                .HasKey(p => new { p.UserGuid, p.RoleGuid });

            modelBuilder.Entity<UserRoles>()
                .HasOne(p => p.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(p => p.RoleGuid);

            modelBuilder.Entity<UserRoles>()
                .HasOne(p => p.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(p => p.UserGuid);

            #endregion

            #endregion
        }
    }
}