using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioDJManager.Models
{
    [Table("song")]
    public partial class Song
    {
        public int ID { get; set; }
        public string path { get; set; }
        public int enabled { get; set; }
        public Nullable<System.DateTime> date_played { get; set; }
        public Nullable<System.DateTime> artist_played { get; set; }
        public int count_played { get; set; }
        public int play_limit { get; set; }
        public int limit_action { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public sbyte song_type { get; set; }
        public int id_subcat { get; set; }
        public int id_genre { get; set; }
        public double weight { get; set; }
        public double duration { get; set; }
        public string cue_times { get; set; }
        public bool precise_cue { get; set; }
        public bool fade_type { get; set; }
        public bool end_type { get; set; }
        public bool overlay { get; set; }
        public string artist { get; set; }
        public string original_artist { get; set; }
        public string title { get; set; }
        public string album { get; set; }
        public string composer { get; set; }
        public string year { get; set; }
        public short track_no { get; set; }
        public short disc_no { get; set; }
        public string publisher { get; set; }
        public string copyright { get; set; }
        public string isrc { get; set; }
        public double bpm { get; set; }
        public string comments { get; set; }
        public string sweepers { get; set; }
        public string album_art { get; set; }
        public string buy_link { get; set; }
        public Nullable<System.DateTime> tdate_played { get; set; }
        public Nullable<System.DateTime> tartist_played { get; set; }
    }
}
