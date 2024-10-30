using System.IO;
using MyGeometryKernel;

using Color = MyGeometryKernel.Vector;
using Point = MyGeometryKernel.Vector;
using MangoRT;

class Program
{
    static void Main()
    {
        // Calculate image height, and ensure it's at least 1
        double aspectRatio = 16.0 / 9.0;
        int image_width = 400;
        int image_height = (int)(image_width / aspectRatio);
        if (image_height < 1) image_height = 1;

        //Camera
        double focal_length = 1.0;
        double viewport_height = 2.0;
        double viewport_width = viewport_height * (double)(image_width)/image_height;
        Point camera_center = new Point();

        //Calculate the vectors across the horizontal and down the vertical viewport edges
        Vector viewport_u = new Vector(viewport_width, 0, 0);
        Vector viewport_v = new Vector(0, -viewport_height, 0);

        //Calculate the horizontal and vertical delta vectors from pixel to pixel
        Vector pixel_delta_u = viewport_u / image_width;
        Vector pixel_delta_v = viewport_v / image_height;


        //Calculate the location of the upper left pixel
        Vector focalVector = new Vector(0, 0, focal_length);
        Vector viewport_upper_left = camera_center - focalVector - viewport_u/2 - viewport_v/2;
        Point pixel00_loc = viewport_upper_left + 0.5 * (pixel_delta_u + pixel_delta_v);

        // File setup
        string docPath = @"C:\Users\wTxT\Dropbox\Programming\RT\Mango\tests";


        // Render
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Image.ppm")))
        {
            outputFile.WriteLine("P3");
            outputFile.WriteLine($"{image_width} {image_height}");
            outputFile.WriteLine("255");
            for (int j = 0; j < image_height; ++j)
            {
                Console.Error.Write("\rScanlines remaining: " + (image_height - j) + " ");
                for (int i = 0; i < image_width; ++i)
                {
                    Point pixel_center = pixel00_loc + (i * pixel_delta_u) + (j * pixel_delta_v);
                    Vector ray_direction = pixel_center - camera_center;
                    Ray r = new Ray(camera_center, ray_direction);

                    Color pixel_color = r.RayColor();
                    pixel_color.WriteColor(outputFile, pixel_color);
                }
            }
        }

        Console.Error.WriteLine("\rDone.                 ");  
    }
    
}