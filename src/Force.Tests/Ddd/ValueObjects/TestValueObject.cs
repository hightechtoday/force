using Force.Ddd;

namespace Force.Tests.Ddd.ValueObjects
{
    public class TestValueObject: ValueObject<string>
    {
        public TestValueObject(string value) : base(value)
        {
        }
    }
}