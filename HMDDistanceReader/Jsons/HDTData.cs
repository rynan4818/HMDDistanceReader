using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HMDDistanceReader.Jsons
{
    public enum BeatmapCharacteristic
    {
        [Description(null)]
        UnknownValue = -1,
        [Description("Standard")]
        LEVEL_STANDARD,
        [Description("OneSaber")]
        LEVEL_ONE_SABER,
        [Description("360Degree")]
        LEVEL_360DEGREE,
        [Description("90Degree")]
        LEVEL_90DEGREE,
        [Description(nameof(Lawless))]
        Lawless,
        [Description(nameof(Lightshow))]
        Lightshow
    }
    public class HDTData
    {
        public class BeatmapResult
        {
            public string LevelID { get; set; }
            public string SongName { get; set; }
            public string Difficurity { get; set; }
            public string BeatmapCharacteristic { get; set; }
            public float Distance { get; set; }
            public DateTime CreatedAt { get; set; }
        }
        public float HeadDistanceTravelled { get; set; }
        public List<BeatmapResult> BeatmapResults { get; set; }
    }
}
