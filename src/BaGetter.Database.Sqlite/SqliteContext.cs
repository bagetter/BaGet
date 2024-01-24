using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BaGetter.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BaGetter.Database.Sqlite
{
    public class SqliteContext : AbstractContext<SqliteContext>
    {
        /// <summary>
        /// The Sqlite error code for when a unique constraint is violated.
        /// </summary>
        private const int SqliteUniqueConstraintViolationErrorCode = 19;

        public SqliteContext(DbContextOptions<SqliteContext> options)
            : base(options)
        { }

        public override bool IsUniqueConstraintViolationException(DbUpdateException exception)
        {
            return exception.InnerException is SqliteException sqliteException &&
                sqliteException.SqliteErrorCode == SqliteUniqueConstraintViolationErrorCode;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Package>()
                .Property(p => p.Id)
                .HasColumnType("TEXT COLLATE NOCASE");

            builder.Entity<Package>()
                .Property(p => p.NormalizedVersionString)
                .HasColumnType("TEXT COLLATE NOCASE");

            builder.Entity<PackageDependency>()
                .Property(d => d.Id)
                .HasColumnType("TEXT COLLATE NOCASE");

            builder.Entity<PackageType>()
                .Property(t => t.Name)
                .HasColumnType("TEXT COLLATE NOCASE");

            builder.Entity<TargetFramework>()
                .Property(f => f.Moniker)
                .HasColumnType("TEXT COLLATE NOCASE");
        }

        public override async Task RunMigrationsAsync(CancellationToken cancellationToken)
        {
            if (Database.GetDbConnection() is SqliteConnection connection)
            {
                /* Create the folder of the Sqlite blob if it does not exist. */
                CreateSqliteDataSourceDirectory(connection);
            }

            await base.RunMigrationsAsync(cancellationToken);
        }

        /// <summary>
        /// Creates directories specified in the Database::ConnectionString config for the Sqlite database file.
        /// Respects the difference of root and relative paths.
        /// </summary>
        /// <param name="connection">Instance of the <see cref="SqliteConnection"/>.</param>
        private static void CreateSqliteDataSourceDirectory(SqliteConnection connection)
        {
            var configDataSource = connection.DataSource;
            var pathToCreate = Path.IsPathRooted(configDataSource)
                // root path
                ? Path.GetDirectoryName(configDataSource)
                // relative path
                : CreateFullDataSourcePath();

            if (pathToCreate is not null)
            {
                Directory.CreateDirectory(pathToCreate);
            }

            return;

            string CreateFullDataSourcePath()
            {
                var exeDir = Directory.GetCurrentDirectory();
                var relativePath = Path.GetDirectoryName(configDataSource);
                var relativeDirectoriesArray = relativePath?.Split(Path.DirectorySeparatorChar) ?? [];
                var relativeDirectoriesCombined = Path.Combine(relativeDirectoriesArray);
                var fullPath = Path.Combine(exeDir, relativeDirectoriesCombined);

                return fullPath;
            }
        }
    }
}
