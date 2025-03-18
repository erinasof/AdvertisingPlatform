namespace AdvertisingPlatform.Exceptions
{
    public class ColonMissingException : Exception
    {
        public ColonMissingException(string Message) : base(Message) { }
    }

    public class EmptyLocationException : Exception
    {
        public EmptyLocationException(string Message) : base(Message) { }
    }

    public class WrongLocationStartException : Exception
    {
        public WrongLocationStartException(string Message) : base(Message) { }
    }

    public class EmptyPartOfLocationException : Exception
    {
        public EmptyPartOfLocationException(string Message) : base(Message) { }
    }

    public class NestingLocationsException : Exception
    {
        public NestingLocationsException(string Message) : base(Message) { }
    }

    public class EmptyPlatformNameException : Exception
    {
        public EmptyPlatformNameException(string Message) : base(Message) { }
    }

    public class PlatformDublicationException : Exception
    {
        public PlatformDublicationException(string Message) : base(Message) { }
    }

}
