using System.Text.Json.Serialization;

namespace BlazorEksiSozluk.Common.Infrastructure.Results
{
    public class ValidationResponseModel
    {
        public ValidationResponseModel()
        {

        }

        public ValidationResponseModel(string message) : this(new List<string>() { message })
        {

        }

        public ValidationResponseModel(IEnumerable<string> errors)
        {
            this.errors = errors;
        }

        public IEnumerable<string> errors { get; set; }

        [JsonIgnore]
        public string FlattenErrors => errors != null ? string.Join(Environment.NewLine, errors) : string.Empty;
    }
}
