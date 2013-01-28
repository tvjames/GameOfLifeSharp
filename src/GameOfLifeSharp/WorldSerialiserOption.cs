// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldSerialiserOption.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The world serialiser option.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System;

    /// <summary>
    /// The world serialiser options.
    /// </summary>
    [Flags]
    public enum WorldSerialiserOption
    {
        /// <summary>
        /// Raw text
        /// </summary>
        Raw, 

        /// <summary>
        /// Pretty formatted text
        /// </summary>
        Pretty
    }
}