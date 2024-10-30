using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using MyGeometryKernel;

using Color = MyGeometryKernel.Vector;
using Point = MyGeometryKernel.Vector;

namespace MangoRT
{
    public class Ray
    {
        public Point origin { get; set; }
        public Vector direction { get; set; }
        public Ray(Point orig, Vector dir)
        {
            origin = orig;
            direction = dir;
        }

        public Point PointAt(double t)
        {
            return origin + t * direction;
        }

        public Color RayColor()
        {
            double t = (hit_sphere(new Point(0, 0, -1), 0.5, this));

            if (t > 0)
            {
                Vector N = PointAt(t) - new Vector(0, 0, -1);
                N.Unitize();
                return 0.5 * new Color(N.x + 1, N.y + 1, N.z + 1);
            }


            Vector unitDir = direction;
            unitDir.Unitize();
            double a = 0.5 * (unitDir.y + 1);
            Color white = new Color(1, 1, 1);
            Color blue = new Color(0.5, 0.7, 1.0);
            return (1.0 - a) * white + a * blue;
        }
        /*
        public double hit_sphere(Point center, double radius, Ray r)
        {
            Vector oc = r.origin - center;
            double a = r.direction.LengthSquared;
            double half_b = Vector.DotProd(oc, r.direction);
            double c = oc.LengthSquared - radius * radius;
            double discriminant = half_b * half_b - a * c;

            if (discriminant < 0)
            {
                return -1.0;
            }
            else
            {
                return (-half_b - Math.Sqrt(discriminant) / a);
            }
        }
        */
    }
}
