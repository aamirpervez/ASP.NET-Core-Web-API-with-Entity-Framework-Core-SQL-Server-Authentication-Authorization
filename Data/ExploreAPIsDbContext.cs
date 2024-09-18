using ExploreAPIs.API.Modals.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExploreAPIs.API.Data
{
    public class ExploreAPIsDbContext : IdentityDbContext //DbContext
    {
        public ExploreAPIsDbContext(DbContextOptions<ExploreAPIsDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        //All these properities represets tables/domains inside Database...
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Data for Difficulities
            //Easy, Medium, Hard

            var difficulities = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id=Guid.Parse("66c35d15-6073-41af-aef2-9f0e9e568483"),
                    Name="Easy"
                },
                 new Difficulty()
                {
                    Id=Guid.Parse("cff873cd-312b-4f69-a8ca-dfbf1f3b1107"),
                    Name="Medium"
                },
                  new Difficulty()
                {
                    Id=Guid.Parse("2fdfee01-7e11-465b-af07-566fb4d9996e"),
                    Name="Hard"
                }
            };

            //Seed Difficulities to the Database..
            modelBuilder.Entity<Difficulty>().HasData(difficulities);

            //Seed Data for Regions..
            var regions = new List<Region>()
            {
                new Region()
                {
                    ID=Guid.Parse("d61f51dd-6c63-42d8-939c-659e43149bde"),
                    Code="NY",
                    Name="New York",
                    RegionImgUrl="New York Pic.png"
                },
                 new Region()
                {
                    ID=Guid.Parse("1d67382d-a7a9-4cfa-ab26-911c2d6ff6cc"),
                    Code="CH",
                    Name="Chicago",
                    RegionImgUrl="Chicago Pic.png"
                },
                  new Region()
                {
                    ID=Guid.Parse("384ddf4b-ce21-4858-b428-df09ca54181d"),
                    Code="HO",
                    Name="Houston",
                    RegionImgUrl="Houston Pic.png"
                }
            };

            //Seed Regions Data to Database..
            modelBuilder.Entity<Region>().HasData(regions);


            var readerRoleId = "f4a67e5b-f36c-4052-a273-430672da64af";
            var writerRoleId = "48c17fed-6717-4331-beae-6f125aeb189b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
