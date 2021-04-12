using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ez.Numerics.Bounding
{
    public struct BoundingBox : IBoundingVolume
    {

        /// <summary>
        /// The maximum point the <see cref="BoundingBox"/> contains.
        /// </summary>
        public Vector3 Max { get; set; }

        /// <summary>
        /// The minimum point the <see cref="BoundingBox"/> contains.
        /// </summary>
        public Vector3 Min { get; set; }
        
        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public ContainmentType Contains(IBoundingVolume bounding)
        {
            return ContainmentType.Disjoint;
        }

        public ContainmentType Contains(in BoundingBox boundingBox)
        {
            if ((Max.X >= boundingBox.Min.X) && (Min.X <= boundingBox.Max.X) && 
                (Max.Y >= boundingBox.Min.Y) && (Min.Y <= boundingBox.Max.Y) && 
                (Max.Z >= boundingBox.Min.Z) && (Min.Z <= boundingBox.Max.Z))
            {
                return ((Min.X <= boundingBox.Min.X) && (Max.X >= boundingBox.Max.X ) && 
                    (Min.Y <= boundingBox.Min.Y) && (Max.Y >= boundingBox.Max.Y) && 
                    (Min.Z <= boundingBox.Min.Z) && (Max.Z >= boundingBox.Max.Z)) 
                    ? ContainmentType.Contains : ContainmentType.Intersects;
            }
            return ContainmentType.Disjoint;
        }

        public bool Contains(Vector3 point)
        {
            return ((Max.X >= point.X) && (Min.X <= point.X) &&
                (Max.Y >= point.Y) && (Min.Y <= point.Y) &&
                (Max.Z >= point.Z) && (Min.Z <= point.Z));
        }

    }
}
