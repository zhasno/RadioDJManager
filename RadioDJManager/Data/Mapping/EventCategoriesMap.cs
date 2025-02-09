using System.Data.Entity.ModelConfiguration;
using RadioDJManager.Models;

namespace RadioDJManager.Data.Mapping
{
    public class EventCategoriesMap : EntityTypeConfiguration<EventCategories>
    {
        public EventCategoriesMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("events_categories", "radiodj161");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.name).HasColumnName("name");
        }
    }
}
