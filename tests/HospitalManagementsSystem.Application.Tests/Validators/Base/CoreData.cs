namespace HospitalManagementsSystem.Application.Tests.Validators.Base;

public class CoreData
{
    public static IEnumerable<object[]> Data
    {
        get
        {
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null };
            yield return new object[] { new String('a', 1) };
            yield return new object[] { new String('a', 51) };
        }
    }
}
