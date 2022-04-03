using System.Text.Json.Serialization;
using account_service.DTO.Registration;

namespace account_service.DTO.Current;


    public class ReadCurrentLoggedInUserDto
    {
        [JsonPropertyName("userInfo")]
        public ReadClientDto? ReadClientDto { get; set; }
        public string? Role { get; set; }
      
     
    
}