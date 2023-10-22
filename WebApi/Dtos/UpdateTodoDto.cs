
using System.Text.Json.Serialization;

namespace WebApi.Dtos
{
    public class UpdateTodoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
