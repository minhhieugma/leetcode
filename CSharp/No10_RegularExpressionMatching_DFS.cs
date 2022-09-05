using System;
namespace LeetCode;

// https://leetcode.com/problems/regular-expression-matching/
public class No10_RegularExpressionMatching_DFS
{
    [SetUp]
    public void Setup()
    {
    }

    public bool IsMatch(string s, string p)
    {
        var visted_matrix = new bool?[s.Length + 1, p.Length + 1];

        return DepthFirstSearch(s, p, visted_matrix, 0, 0);
    }

    // yield return new object[] { "ab", ".*", true };
    public bool DepthFirstSearch(string str, string patterns,
        bool?[,] visted_arr, int str_at, int patterns_at)
    {
        Console.WriteLine($"{str_at} {patterns_at}");

        // When both str index and patterns index are out of scope, it means we have checked all
        // It means we need to stop and tell it is correct
        if (str_at >= str.Length && patterns_at >= patterns.Length)
        {
            return true;
        }

        // string still has something to check but the patterns is out of scope
        // It means to be correct, the pattern needs to be more
        if (patterns_at >= patterns.Length)
            return false;

        // Make sure that we dont check the same path twice
        if (visted_arr[str_at, patterns_at] is not null)
            return visted_arr[str_at, patterns_at]!.Value;

        var is_met_requirements = str_at < str.Length && (patterns[patterns_at] == '.' || str[str_at] == patterns[patterns_at]);
        var is_star_matching = patterns_at + 1 < patterns.Length && patterns[patterns_at + 1] == '*';

        // Star matching
        if (is_star_matching)
        {
            visted_arr[str_at, patterns_at] = DepthFirstSearch(str, patterns, visted_arr, str_at, patterns_at + 2);

            if (visted_arr[str_at, patterns_at] == false)
                if (is_met_requirements)
                    visted_arr[str_at, patterns_at] = DepthFirstSearch(str, patterns, visted_arr, str_at + 1, patterns_at);

            return visted_arr[str_at, patterns_at]!.Value;
        }

        // Normal matching
        if (is_met_requirements)
        {
            visted_arr[str_at, patterns_at] = DepthFirstSearch(str, patterns, visted_arr, str_at + 1, patterns_at + 1);

            return visted_arr[str_at, patterns_at].Value;
        }

        visted_arr[str_at, patterns_at] = false;
        return false;
    }

    [TestCaseSource(nameof(DivideCases))]
    public void Test1(string input, string pattern, bool expectedOutput)
    {
        var output = IsMatch(input, pattern);

        Assert.AreEqual(expectedOutput, output);
    }

    static IEnumerable<object> DivideCases()
    {
        yield return new object[] { "aa", "a", false };
        yield return new object[] { "aa", "aa", true };
        yield return new object[] { "aa", "..", true };
        yield return new object[] { "aa", "a*", true };
        yield return new object[] { "ab", ".*", true };
        yield return new object[] { "aab", "c*a*b", true };
        yield return new object[] { "mississippi", "mis*is*ip*.", true };
        yield return new object[] { "ab", ".*c", false };
        yield return new object[] { "aaa", "a*a", true };
        yield return new object[] { "baaaac", "ba*c", true };
        yield return new object[] { "aaa", "a*aaa*", true };
        yield return new object[] { "aaaaaaaaaaaaab", "a*a*a*a*a*a*a*a*a*a*b", true };
        yield return new object[] { "aaaaaaaaaaaaab", "a*a*a*a*a*a*a*a*a*a*c", false };
        yield return new object[] { "ab", "a*a*c", false };
    }
}