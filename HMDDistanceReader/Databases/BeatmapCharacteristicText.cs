using HMDDistanceReader.Databases.Interfaces;
using HMDDistanceReader.Jsons;

namespace HMDDistanceReader.Databases
{
    public class BeatmapCharacteristicText : IIdEntity
    {
        public int ID { get; set; }
        public BeatmapCharacteristic BeatmapCharacteristicEnumValue { get; set; }
        public string Key { get; set; }
        public string DisplayName { get; set; }
    }
}
