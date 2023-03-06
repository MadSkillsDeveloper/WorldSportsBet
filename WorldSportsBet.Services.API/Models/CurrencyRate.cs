using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorldSportsBetting.Services.API.Models
{
    public class CurrencyRate
    {
        #region Fields
        #endregion

        #region Properties
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("")]
        public string Base { get; set; }
        public DateTime CreatedDate { get; set; }
        public IDictionary<string, decimal> Rates { get; set; }
        #endregion

        #region Methods
        #endregion

        #region Constructors
        #endregion
    }
}
