using System.Text.Json.Serialization;

namespace WebApi.Dtos
{
    public class AddTodoDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
