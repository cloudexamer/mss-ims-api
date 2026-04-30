namespace mss.ims.domain.Shared
{
    public class PhoneNumber
    {
        public string Value { get; private set; }

        private PhoneNumber()
        {
        }

        public PhoneNumber(string value)
        {
            Value = value == null ? string.Empty : value.Trim();
        }

        public void Update(string value)
        {
            Value = value == null ? string.Empty : value.Trim();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}