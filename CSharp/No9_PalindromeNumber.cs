namespace LeetCode;

// https://leetcode.com/problems/palindrome-number/
public class No9_PalindromeNumber
{
    [SetUp]
    public void Setup()
    {
    }

    public bool IsPalindrome(int x)
    {
        var arr = x.ToString();
        if(arr.Length > 0 && arr[0] == '-')
            return false;

        for (int i = 0; i < arr.Length/2; i++)
        {
            if(arr[i] != arr[arr.Length - i - 1])
                return false;
        }

        return true;
    }

    [TestCaseSource(nameof(DivideCases))]
    public void Test1(int input, bool expectedOutput)
    {

        var output = IsPalindrome(input);

        Assert.AreEqual(expectedOutput, output);
    }

    static IEnumerable<object> DivideCases()
    {
        yield return new object[] { 1, true };
        yield return new object[] { 121, true };
        yield return new object[] { -121, false };
        yield return new object[] { 10, false };
    }
}