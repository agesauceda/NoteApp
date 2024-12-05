namespace NoteApp.Models.common
{
    public class ApiResponse
    {
        public bool? status { get; set; }
        public int? statusCode { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }
        public bool? isLogin { get; set; }
        public string? timestamps { get; set; } 
    }
}
