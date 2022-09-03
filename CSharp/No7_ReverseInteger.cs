using System.Diagnostics;
namespace LeetCode;

public class No7_ReverseInteger
{
    [SetUp]
    public void Setup()
    {
    }

    public bool IsGreater(string num1, string num2)
    {
        // Console.WriteLine($"Greater:{num1.Length} {num2.Length}");
        if (num1.Length > num2.Length)
            return true;
        else if (num1.Length < num2.Length)
            return false;

        while (num1.Length > num2.Length)
        {
            num2 += "0" + num2;
        }

        for (var i = 0; i < num1.Length; i++)
        {
            if (num1[i] > num2[i])
            {
                return true;
            }
            else if (num1[i] < num2[i])
            {
                return false;
            }
        }

        return false;
    }

    public int Reverse(int x) {
        var arr = x.ToString().ToArray();

        var is_negative = x < 0;
        if (is_negative)
            arr = arr[1..];

        for (var i = 0; i < arr.Length / 2; i++)
        {
            var tmp = arr[i];
            arr[i] = arr[arr.Length - i - 1];

            arr[arr.Length - i - 1] = tmp;
        }

        while (arr[0] == 0)
        {
            arr = arr[1..];
        }

        var s = string.Join("", arr);
        // Console.WriteLine(s);

        if (IsGreater(s, int.MaxValue.ToString()))
        {
            s = "0";
        }
        else
        {
            if (is_negative)
                s = "-" + s;
        }

        var output = int.Parse(s);
        
        return output;
    }

    [TestCaseSource(nameof(DivideCases))]
    public void Test1(int input, int expectedOutput)
    {
        
        var output = Reverse(input);

        Assert.AreEqual(expectedOutput, output);
    }

    static IEnumerable<object> DivideCases()
    {
        yield return new object[] { 123, 321 };
        yield return new object[] { -123, -321 };
        yield return new object[] { 120, 21 };
        yield return new object[] { 2147483647, 0 };
    }
}