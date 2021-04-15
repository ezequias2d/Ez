// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Ez.Numerics.Bounding
{
    /// <summary>
    /// Represents a bounding box aligned to the X and Y axes .
    /// </summary>
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

        /// <summary>
        /// Creates a new <see cref="BoundingBox"/> with the minimum point and maximum point.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public BoundingBox(Vector3 min, Vector3 max)
        {            
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Returns the <see cref="ContainmentType"/> between this and another <see cref="IBoundingVolume"/>.
        /// </summary>
        /// <param name="bounding">The other <see cref="IBoundingVolume"/> to compare.</param>
        /// <returns>
        /// <see cref="ContainmentType.Disjoint"/>, if there is no overlap between the bounding volumes.<br/>
        /// <see cref="ContainmentType.Contains"/>, if the instance fully contains the volume of <see cref="IBoundingVolume"/>.<br/>
        /// <see cref="ContainmentType.Intersects"/>, if only part of the instance contains at least part of
        /// <paramref name="bounding"/> volume.</returns>
        public ContainmentType Contains(IBoundingVolume bounding)
        {
            if (bounding is BoundingBox boundingBox)
                Contains(boundingBox);
            return ContainmentType.Disjoint;
        }

        /// <summary>
        /// Returns the <see cref="ContainmentType"/> between this and another <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="bounding">The other <see cref="BoundingBox"/> to compare.</param>
        /// <returns>
        /// <see cref="ContainmentType.Disjoint"/>, if there is no overlap between the bounding volumes.<br/>
        /// <see cref="ContainmentType.Contains"/>, if the instance fully contains the volume of <see cref="BoundingBox"/>.<br/>
        /// <see cref="ContainmentType.Intersects"/>, if only part of the instance contains at least part of
        /// <paramref name="bounding"/> volume.</returns>
        public ContainmentType Contains(in BoundingBox bounding)
        {
            if ((Max.X >= bounding.Min.X) && (Min.X <= bounding.Max.X) && 
                (Max.Y >= bounding.Min.Y) && (Min.Y <= bounding.Max.Y) && 
                (Max.Z >= bounding.Min.Z) && (Min.Z <= bounding.Max.Z))
            {
                return ((Min.X <= bounding.Min.X) && (Max.X >= bounding.Max.X ) && 
                    (Min.Y <= bounding.Min.Y) && (Max.Y >= bounding.Max.Y) && 
                    (Min.Z <= bounding.Min.Z) && (Max.Z >= bounding.Max.Z)) 
                    ? ContainmentType.Contains : ContainmentType.Intersects;
            }
            return ContainmentType.Disjoint;
        }

        /// <summary>
        /// Returns a value indicating whether the point is within the bounding.
        /// </summary>
        /// <param name="point">The pointer to evaluate.</param>
        /// <returns><see langword="true"/> if the point is inside the bounding box; otherwise, <see langword="false"/>.</returns>
        public bool Contains(Vector3 point)
        {
            return ((Max.X >= point.X) && (Min.X <= point.X) &&
                (Max.Y >= point.Y) && (Min.Y <= point.Y) &&
                (Max.Z >= point.Z) && (Min.Z <= point.Z));
        }

    }
}
