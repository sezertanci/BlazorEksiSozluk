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
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }

        [JsonIgnore]
        public string FlattenErrors => Errors != null ? string.Join(Environment.NewLine, Errors) : string.Empty;
    }
}
