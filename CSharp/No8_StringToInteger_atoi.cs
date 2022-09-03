namespace LeetCode;

public class No8_StringToInteger_atoi
{
    [SetUp]
    public void Setup()
    {
    }

    public string Bound(string s, bool is_negative)
    {
        var limited_val = is_negative ? int.MinValue.ToString()[1..] : int.MaxValue.ToString();
        if (s.Length > limited_val.Length)
            return limited_val;
        else if (s.Length < limited_val.Length)
            return s;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] > limited_val[i])
                return limited_val;
            else if (s[i] < limited_val[i])
                return s;
        }

        return s;
    }

    public int MyAtoi(string s)
    {
        if (s.Length == 0)
            return 0;

        // Console.WriteLine(s);
        var arr = s.ToArray();

        while (arr.Length > 0 && arr[0] == ' ')
        {
            // Console.WriteLine($"Removing leading space");
            arr = arr[1..];
        }

        var is_negative = false;
        if (arr.Length > 0 && arr[0] == '-')
        {
            arr = arr[1..];
            is_negative = true;
        }
        else if (arr.Length > 0 && arr[0] == '+')
        {
            arr = arr[1..];
            is_negative = false;
        }

        while (arr.Length > 1 && arr[0] == '0')
        {
            // Console.WriteLine($"Removing leading 0");
            arr = arr[1..];
        }

        var new_arr = new List<char>();

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < '0' || arr[i] > '9')
                break;

            new_arr.Add(arr[i]);
        }

        var new_string = string.Join("", new_arr);
        var val = Bound(new_string, is_negative);

        // Console.WriteLine(val);
        if (new_string.Length == 0)
        {
            return 0;
        }

        return int.Parse($"{(is_negative ? "-" : "")}{val}");
    }

    [TestCaseSource(nameof(DivideCases))]
    public void Test1(string input, int expectedOutput)
    {

        var output = MyAtoi(input);

        Assert.AreEqual(expectedOutput, output);
    }

    static IEnumerable<object> DivideCases()
    {
        yield return new object[] { "words and 987", 0 };
        yield return new object[] { "123", 123 };
        yield return new object[] { "-123", -123 };
        yield return new object[] { "     -120", -120 };
        yield return new object[] { "     2147483647", 2147483647 };
        yield return new object[] { "     214748364711111", 2147483647 };
        yield return new object[] { "-91283472332", -2147483648 };
        yield return new object[] { "", 0 };
        yield return new object[] { "+1", 1 };
        yield return new object[] { "  0000000000012345678", 12345678 };
        yield return new object[] { "+", 0 };
        yield return new object[] { " ", 0 };
    }
}