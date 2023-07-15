

namespace BenevArts.Common.Exeptions
{
    public class AssetNullException : Exception
    {
        public AssetNullException() : this("Asset not Found")
        {
        }

        public AssetNullException(string message)
            : base(message)
        {
        }

        public AssetNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class UserNullException : Exception 
    {
        public UserNullException() : this("User not Found")
        {
        }

        public UserNullException(string message)
            : base(message)
        {
        }

        public UserNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
