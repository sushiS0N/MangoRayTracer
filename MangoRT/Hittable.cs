using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using MyGeometryKernel;

using Color = MyGeometryKernel.Vector;
using Point = MyGeometryKernel.Vector;

using MangoRT;

namespace MangoRT
{
    internal class HitRecord
    {
        public Point p { get; set; }
        public Vector normal { get; set; }
        public double t { get; set; }
    }

    internal class Hittable
    {
        //public virtual ~hittable() = default; c++
        public virtual bool Hit(Ray r, double ray_tmin, double ray_tmax, HitRecord rec)
        {
            return false;
        }
    }
}
