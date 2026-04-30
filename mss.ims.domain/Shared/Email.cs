namespace mss.ims.domain.Shared
{
    public class Email
    {
        public string Value { get; private set; }

        private Email()
        {
        }

        public Email(string value)
        {
            Value = value == null ? string.Empty : value.Trim().ToLowerInvariant();
        }

        public void Update(string value)
        {
            Value = value == null ? string.Empty : value.Trim().ToLowerInvariant();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}