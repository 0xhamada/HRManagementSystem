namespace HR.PL.Helper
{
    public class Upload
    {
        public static string UploadFile(IFormFile file, string folder)
        {
            // Build a unique file name to avoid overwriting existing files
            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            // Build the full physical path: wwwroot/Images/filename.jpg
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);

            // Make sure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }
    }
}
