namespace System.Net
{
    public interface ICidrGuess {
        bool TryGuessCidr(string ip, out byte cidr);

    }
    public static class CidrGuess
    {
        public static ICidrGuess ClassFull { get => _cidr_classfull.Value; }
        public static ICidrGuess ClassLess { get => _cidr_classless.Value; }

        private static readonly Lazy<ICidrGuess> _cidr_classless = new Lazy<ICidrGuess>(() => new CidrClassLess());
        private static readonly Lazy<ICidrGuess> _cidr_classfull = new Lazy<ICidrGuess>(() => new CidrClassFull());

    }

}
