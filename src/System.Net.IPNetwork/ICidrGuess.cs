// <copyright file="ICidrGuess.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace System.Net
{
    /// <summary>
    ///
    /// </summary>
    public interface ICidrGuess
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cidr"></param>
        /// <returns></returns>
        bool TryGuessCidr(string ip, out byte cidr);
    }
}
