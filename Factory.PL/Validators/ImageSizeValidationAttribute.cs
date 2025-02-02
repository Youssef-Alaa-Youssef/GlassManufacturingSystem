using System.ComponentModel.DataAnnotations;
using System.Drawing;

public class ImageSizeValidationAttribute : ValidationAttribute
{
    private readonly int _width;
    private readonly int _height;

    public ImageSizeValidationAttribute(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public override bool IsValid(object? value)
    {
        if (value is IFormFile[] files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    try
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            ErrorMessage = "Only image files (JPG, JPEG, PNG, GIF) are allowed.";
                            return false;
                        }

                        using (var image = Image.FromStream(file.OpenReadStream()))
                        {
                            if (image.Width != _width || image.Height != _height)
                            {
                                ErrorMessage = $"Image must be {_width}x{_height} pixels. Current size is {image.Width}x{image.Height}.";
                                return false;
                            }
                        }
                    }
                    catch
                    {
                        ErrorMessage = "The file is not a valid image or cannot be processed.";
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
