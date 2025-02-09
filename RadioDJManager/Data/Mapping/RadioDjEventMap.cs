using System.Data.Entity.ModelConfiguration;
using RadioDJManager.Models;

namespace RadioDJManager.Data.Mapping
{
    public class RadioDjEventMap : EntityTypeConfiguration<RadioEvent>
    {
        public RadioDjEventMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.time)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.day)
                .HasMaxLength(30);

            this.Property(t => t.hours)
                .HasMaxLength(100);

            this.Property(t => t.data)
                .HasMaxLength(65535);

            this.Property(t => t.enabled)
                .HasMaxLength(65532);

            // Table & Column Mappings
            this.ToTable("events", "radiodj161");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.type).HasColumnName("type");
            this.Property(t => t.time).HasColumnName("time");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.date).HasColumnName("date");
            this.Property(t => t.day).HasColumnName("day");
            this.Property(t => t.hours).HasColumnName("hours");
            this.Property(t => t.data).HasColumnName("data");
            this.Property(t => t.enabled).HasColumnName("enabled");
            this.Property(t => t.catID).HasColumnName("catID");
        }
    }
}
