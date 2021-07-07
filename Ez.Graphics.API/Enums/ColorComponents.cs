using System;
using Ez.Graphics.API.Resources;

namespace Ez.Graphics.API
{
    /// <summary>
    /// Describes which components are written to a <see cref="IFramebuffer"/>
    /// </summary>
    [Flags]
    public enum ColorComponents
    {
        /// <summary>
        /// Specific no component.
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies that the R value is written to the color attachment
        /// for the appropriate sample. Otherwise, the value in memory is 
        /// unmodified.
        /// </summary>
        R = 1,
        /// <summary>
        /// Specifies that the G value is written to the color attachment 
        /// for the appropriate sample. Otherwise, the value in memory is 
        /// unmodified.
        /// </summary>
        G = 1 << 1,
        /// <summary>
        /// Specifies that the B value is written to the color attachment 
        /// for the appropriate sample. Otherwise, the value in memory is
        /// unmodified.
        /// </summary>
        B = 1 << 2,
        /// <summary>
        /// Specifies that the A value is written to the color attachment 
        /// for the appropriate sample. Otherwise, the value in memory is 
        /// unmodified.
        /// </summary>
        A = 1 << 3,

        /// <summary>
        /// Combination of <see cref="ColorComponents.R"/> and <see cref="ColorComponents.G"/>.
        /// </summary>
        RG = R | G,
        /// <summary>
        /// Combination of <see cref="ColorComponents.R"/>, <see cref="ColorComponents.G"/>
        /// and <see cref="ColorComponents.B"/>.
        /// </summary>
        RGB = R | G | B,
        /// <summary>
        /// Combination of <see cref="ColorComponents.R"/>, <see cref="ColorComponents.G"/>
        /// and <see cref="ColorComponents.A"/>.
        /// </summary>
        RGA = R | G | A,
        /// <summary>
        /// Combination of <see cref="ColorComponents.R"/>, <see cref="ColorComponents.G"/>,
        /// <see cref="ColorComponents.B"/> and <see cref="ColorComponents.A"/>.
        /// </summary>
        RGBA = R | G | B | A,
        /// <summary>
        /// Combination of <see cref="ColorComponents.R"/> and <see cref="ColorComponents.B"/>.
        /// </summary>
        RB = R | B,
        /// <summary>
        /// Combination of <see cref="ColorComponents.R"/> and <see cref="ColorComponents.A"/>.
        /// </summary>
        RA = R | A,
        /// <summary>
        /// Combination of <see cref="ColorComponents.G"/> and <see cref="ColorComponents.B"/>.
        /// </summary>
        GB = G | B,
        /// <summary>
        /// Combination of <see cref="ColorComponents.G"/>, <see cref="ColorComponents.B"/>
        /// and <see cref="ColorComponents.A"/>.
        /// </summary>
        GBA = G | B | A,
        /// <summary>
        /// Combination of <see cref="ColorComponents.G"/> and <see cref="ColorComponents.A"/>.
        /// </summary>
        GA = G | A,
        /// <summary>
        /// Combination of <see cref="ColorComponents.B"/> and <see cref="ColorComponents.A"/>.
        /// </summary>
        BA = B | A,
        /// <summary>
        /// Combination of <see cref="ColorComponents.R"/>, <see cref="ColorComponents.B"/>
        /// and <see cref="ColorComponents.A"/>.
        /// </summary>
        RBA = R | B | A,
    }
}
