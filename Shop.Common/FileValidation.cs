using Microsoft.AspNetCore.Http;

namespace Shop.Common
{
    public static class FileValidation
    {
        private static readonly HashSet<string> ValidImageExtensions = new()
        {
            ".jpg", ".jpeg", ".png", ".webp", ".gif", ".bmp", ".tiff", ".svg", ".ico", ".jfif", ".heic"
        };

        private static readonly HashSet<string> ValidVideoExtensions = new()
        {
            ".mp4", ".avi", ".mov", ".wmv", ".mkv", ".flv", ".webm"
        };

        private static readonly HashSet<string> ValidFileExtensions = new()
        {
            ".zip", ".rar", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
            ".txt", ".log", ".mp3", ".wav", ".m4a", ".ogg", ".mmf", ".xla"
        };

        public static bool IsValidFile(this IFormFile file)
        {
            if (file == null) return false;
            var ext = Path.GetExtension(file.FileName)?.ToLower();
            return ext != null && (ValidImageExtensions.Contains(ext) || ValidVideoExtensions.Contains(ext) || ValidFileExtensions.Contains(ext));
        }

        public static bool IsVideoFile(this IFormFile file)
        {
            if (file == null) return false;
            var ext = Path.GetExtension(file.FileName)?.ToLower();
            return ext != null && ValidVideoExtensions.Contains(ext);
        }

        public static bool IsValidImageFile(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            var ext = Path.GetExtension(fileName)?.ToLower();
            return ext != null && ValidImageExtensions.Contains(ext);
        }
    }
}
