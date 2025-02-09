using System.Data.Entity.ModelConfiguration;
using RadioDJManager.Models;

namespace RadioDJManager.Data.Mapping
{
    public class RotationsListMap : EntityTypeConfiguration<RotationsList>
    {
        public RotationsListMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.repeatRule)
                .IsRequired()
                .HasMaxLength(65531);

            // Table & Column Mappings
            this.ToTable("rotations_list", "radiodj161");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.pID).HasColumnName("pID");
            this.Property(t => t.catID).HasColumnName("catID");
            this.Property(t => t.subID).HasColumnName("subID");
            this.Property(t => t.genID).HasColumnName("genID");
            this.Property(t => t.selType).HasColumnName("selType");
            this.Property(t => t.sweeper).HasColumnName("sweeper");
            this.Property(t => t.repeatRule).HasColumnName("repeatRule");
            this.Property(t => t.ord).HasColumnName("ord");
        }
    }
}
