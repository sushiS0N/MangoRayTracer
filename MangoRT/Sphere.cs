using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyGeometryKernel;

using Color = MyGeometryKernel.Vector;
using Point = MyGeometryKernel.Vector;

namespace MangoRT
{
    internal class Sphere : Hittable
    {
        public Point _center { get; set; }
        public double _radius { get; set; }

        private Point center;
        private double radius;

        public bool Hit(Ray r, double ray_tmin, double ray_tmax, HitRecord rec)
        {
            Vector oc = r.origin - _center;
            double a = r.direction.LengthSquared;
            double half_b = Vector.DotProd(oc, r.direction);
            double c = oc.LengthSquared - _radius * _radius;

            double discriminant = half_b * half_b - a * c;
            if (discriminant < 0) return false;
            double sqrtd = Math.Sqrt(discriminant);

            // Find the nearest root that lies in the acceptable range
            double root = (-half_b - sqrtd) / a;
            if (root <= ray_tmin || ray_tmax <= root)
            {
                root = (-half_b + sqrtd) / a;
                if (root <= ray_tmin || ray_tmax <= root)
                    return false;
            }

            rec.t = root;
            rec.p = r.PointAt(rec.t);
            rec.normal = (rec.p - _center) / _radius;

            return true;
        }
    }
}
