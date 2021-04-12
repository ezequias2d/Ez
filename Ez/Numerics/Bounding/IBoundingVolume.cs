using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Numerics.Bounding
{
    public interface IBoundingVolume
    {

        ContainmentType Contains(IBoundingVolume bounding);
    }
}
