using System.Data.Common;
using System.Data.Entity;
using RadioDJManager.Data.Mapping;
using RadioDJManager.Models;

namespace RadioDJManager.Data
{
    //[Serializable]
    public  class RadioDbContext : DbContext
    {
        static RadioDbContext()
        {
            Database.SetInitializer<RadioDbContext>(null);
        }

        public RadioDbContext(DbConnection connection,bool owns)//string connectionString =string.Format("")
            : base(connection, owns)//"Name=radiodj161Context"
        {
        }

       
        public DbSet<RadioEvent> events { get; set; }
       
        public DbSet<EventCategories> events_categories { get; set; }
     
        public DbSet<RadioRotation> rotations { get; set; }
 
        public DbSet<RotationsList> rotations_list { get; set; }

        public DbSet<Song> songs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RadioDjEventMap());
            modelBuilder.Configurations.Add(new EventCategoriesMap());
            modelBuilder.Configurations.Add(new RotationMap());
            modelBuilder.Configurations.Add(new RotationsListMap());
            modelBuilder.Configurations.Add(new SongMap());
        }
    }
}
