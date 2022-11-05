// <copyright file="CidrGuess.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace System.Net
{
    /// <summary>
    /// A static helper CidrGuess class.
    /// </summary>
    public static class CidrGuess
    {
        /// <summary>
        /// Gets classFull guess.
        /// </summary>
        public static ICidrGuess ClassFull { get => _cidr_classfull.Value; }

        /// <summary>
        /// Gets classLess guess.
        /// </summary>
        public static ICidrGuess ClassLess { get => _cidr_classless.Value; }

        private static readonly Lazy<ICidrGuess> _cidr_classless = new Lazy<ICidrGuess>(() => new CidrClassLess());
        private static readonly Lazy<ICidrGuess> _cidr_classfull = new Lazy<ICidrGuess>(() => new CidrClassFull());
    }
}
