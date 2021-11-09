using System;

namespace Bidtellect.Tcf.Serialization
{
    public class TcStringParserException : Exception
    {
        public ExceptionType Type { get; set; }

        public TcStringParserException(ExceptionType type)
        {
            Type = type;
        }

        public enum ExceptionType
        {
            InvalidVersion,
            InvalidVendorId,
        }
    }
}
