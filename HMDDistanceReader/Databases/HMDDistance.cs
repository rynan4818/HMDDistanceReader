using HMDDistanceReader.Databases.Interfaces;
using HMDDistanceReader.Jsons;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace HMDDistanceReader.Databases
{
    public class HMDDistance
    {
        private ILiteDbReader _db;

        public HMDDistance(ILiteDbReader db)
        {
            this._db = db;
        }
        public string HMDDistanceJsonGet()
        {
            var data = new HDTData();
            data.BeatmapResults = new List<HDTData.BeatmapResult>();
            var headDistanceTravelled = 0f;
            foreach (var results in this._db.Find<DistanceInformation>(x => true))
            {
                headDistanceTravelled += results.Distance;
                var beatmapCharacteristicText = this._db.Find<BeatmapCharacteristicText>(x => x.ID == results.BeatmapCharacteristicTextId).FirstOrDefault();
                if (beatmapCharacteristicText == null)
                    beatmapCharacteristicText = this._db.Find<BeatmapCharacteristicText>(x => x.BeatmapCharacteristicEnumValue == BeatmapCharacteristic.UnknownValue).FirstOrDefault();
                data.BeatmapResults.Add(new HDTData.BeatmapResult
                {
                    LevelID = results.LevelID,
                    SongName = results.SongName,
                    Difficurity = results.Difficurity,
                    BeatmapCharacteristic = beatmapCharacteristicText.DisplayName,
                    Distance = results.Distance,
                    CreatedAt = results.CreatedAt
                });
            }
            data.HeadDistanceTravelled = headDistanceTravelled;
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
