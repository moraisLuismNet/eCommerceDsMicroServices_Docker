using ShoppingService.DTOs;

namespace ShoppingService.Services
{
    public class RecordService : IRecordService
    {
        private readonly HttpClient _httpClient;
        public RecordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RecordDTO> GetRecordByIdRecordService(int id)
        {
            // Make the call to CdService
            var response = await _httpClient.GetAsync($"/api/records/{id}");
            response.EnsureSuccessStatusCode();

            // Deserialize the response to RecordDTO
            return await response.Content.ReadFromJsonAsync<RecordDTO>();
        }


        public async Task UpdateRecordRecordService(RecordDTO record)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/records/{record.IdRecord}", record);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error updating record: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateRecordRecordService: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateStockRecordService(int id, int amount)
        {
            try
            {
                var response = await _httpClient.PutAsync(
                    $"/api/records/{id}/updateStock/{amount}",
                    null
                );

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException(
                        $"Error updating stock: {response.StatusCode} - {errorContent}"
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateStockRecordService: {ex.Message}");
                throw;
            }
        }
    }
}
