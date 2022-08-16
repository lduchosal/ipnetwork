namespace System.Net
{
     /// <summary>
     /// 
     /// </summary>
    public interface ICidrGuess {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cidr"></param>
        /// <returns></returns>
        bool TryGuessCidr(string ip, out byte cidr);

    }
    /// <summary>
    /// 
    /// </summary>
    public static class CidrGuess
    {
        /// <summary>
        /// 
        /// </summary>
        public static ICidrGuess ClassFull { get => _cidr_classfull.Value; }
        /// <summary>
        /// 
        /// </summary>
        public static ICidrGuess ClassLess { get => _cidr_classless.Value; }

        private static readonly Lazy<ICidrGuess> _cidr_classless = new Lazy<ICidrGuess>(() => new CidrClassLess());
        private static readonly Lazy<ICidrGuess> _cidr_classfull = new Lazy<ICidrGuess>(() => new CidrClassFull());

    }

}
