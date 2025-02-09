using System.Data.Entity.ModelConfiguration;
using RadioDJManager.Models;

namespace RadioDJManager.Data.Mapping
{
    public class SongMap : EntityTypeConfiguration<Song>
    {
        public SongMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.path)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.cue_times)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.artist)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.original_artist)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.album)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.composer)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.year)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.publisher)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.copyright)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.isrc)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.comments)
                .HasMaxLength(65535);

            this.Property(t => t.sweepers)
                .HasMaxLength(250);

            this.Property(t => t.album_art)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.buy_link)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("songs", "radiodj161");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.path).HasColumnName("path");
            this.Property(t => t.enabled).HasColumnName("enabled");
            this.Property(t => t.date_played).HasColumnName("date_played");
            this.Property(t => t.artist_played).HasColumnName("artist_played");
            this.Property(t => t.count_played).HasColumnName("count_played");
            this.Property(t => t.play_limit).HasColumnName("play_limit");
            this.Property(t => t.limit_action).HasColumnName("limit_action");
            this.Property(t => t.start_date).HasColumnName("start_date");
            this.Property(t => t.end_date).HasColumnName("end_date");
            this.Property(t => t.song_type).HasColumnName("song_type");
            this.Property(t => t.id_subcat).HasColumnName("id_subcat");
            this.Property(t => t.id_genre).HasColumnName("id_genre");
            this.Property(t => t.weight).HasColumnName("weight");
            this.Property(t => t.duration).HasColumnName("duration");
            this.Property(t => t.cue_times).HasColumnName("cue_times");
            this.Property(t => t.precise_cue).HasColumnName("precise_cue");
            this.Property(t => t.fade_type).HasColumnName("fade_type");
            this.Property(t => t.end_type).HasColumnName("end_type");
            this.Property(t => t.overlay).HasColumnName("overlay");
            this.Property(t => t.artist).HasColumnName("artist");
            this.Property(t => t.original_artist).HasColumnName("original_artist");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.album).HasColumnName("album");
            this.Property(t => t.composer).HasColumnName("composer");
            this.Property(t => t.year).HasColumnName("year");
            this.Property(t => t.track_no).HasColumnName("track_no");
            this.Property(t => t.disc_no).HasColumnName("disc_no");
            this.Property(t => t.publisher).HasColumnName("publisher");
            this.Property(t => t.copyright).HasColumnName("copyright");
            this.Property(t => t.isrc).HasColumnName("isrc");
            this.Property(t => t.bpm).HasColumnName("bpm");
            this.Property(t => t.comments).HasColumnName("comments");
            this.Property(t => t.sweepers).HasColumnName("sweepers");
            this.Property(t => t.album_art).HasColumnName("album_art");
            this.Property(t => t.buy_link).HasColumnName("buy_link");
            this.Property(t => t.tdate_played).HasColumnName("tdate_played");
            this.Property(t => t.tartist_played).HasColumnName("tartist_played");
        }
    }
}
