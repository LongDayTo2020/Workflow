using Microsoft.AspNetCore.Http;

namespace Workflow.Library;

public static class FileLibrary
{
    public static async Task<string> SaveFileAsync(IFormFile file, string directoryPath)
    {
        // Generate a unique file name or use the original file name
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

        // Combine the directory path and the unique file name
        string filePath = Path.Combine(directoryPath, uniqueFileName);
        // If path is not exists then create the path
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        // Save the file to the specified path
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        // Return the file path or any other information you need
        return filePath;
    }
}