using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ImageUploadController(CloudinaryService cloudinary) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        var imageUrl = await cloudinary.UploadImageAsync(file);
        if (string.IsNullOrEmpty(imageUrl))
            return BadRequest("Upload failed");

        return Ok(new { imageUrl });
    }
}
