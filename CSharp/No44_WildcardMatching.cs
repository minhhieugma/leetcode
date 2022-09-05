using System.Linq.Expressions;
using System;
namespace LeetCode;

// https://leetcode.com/problems/wildcard-matching/
public class No44_WildcardMatching
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

    public bool DepthFirstSearch(string str, string patterns, bool?[,] visted_matrix
    , int str_at, int patterns_at)
    {
        if (patterns_at >= patterns.Length && str_at >= str.Length)
            return true;

        if (patterns_at >= patterns.Length)
            return false;

        if (visted_matrix[str_at, patterns_at] != null)
            return visted_matrix[str_at, patterns_at]!.Value;

        var is_met_requirements = str_at < str.Length && (str[str_at] == patterns[patterns_at]
        || (patterns[patterns_at] is '?' or '*'));

        var is_star_matching = patterns[patterns_at] is '*';

        if (is_star_matching)
        {
            // Consider the star is skipped
            visted_matrix[str_at, patterns_at] = DepthFirstSearch(str, patterns, visted_matrix,
                str_at, patterns_at + 1);

            
            if (visted_matrix[str_at, patterns_at] == false && str_at < str.Length)
            {
                visted_matrix[str_at, patterns_at] = DepthFirstSearch(str, patterns, visted_matrix,
                    str_at + 1, patterns_at);
            }

            return visted_matrix[str_at, patterns_at]!.Value;
        }

        if (is_met_requirements)
        {
            visted_matrix[str_at, patterns_at] = DepthFirstSearch(str, patterns, visted_matrix,
                            str_at + 1, patterns_at + 1);

            return visted_matrix[str_at, patterns_at]!.Value;
        }

        visted_matrix[str_at, patterns_at] = false;
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
        yield return new object[] { "aa", "*", true };
        yield return new object[] { "cb", "?a", false };
        yield return new object[] { "ab", "?b", true };
        yield return new object[] { "ab", "?a", false };
        yield return new object[] { "abc", "?*c", true };
        yield return new object[] { "abc", "?*e", false };

        yield return new object[] { "abc", "?********c", true };
        yield return new object[] { "abc", "?****b***c", true };
        yield return new object[] { "abc", "?****c***c", false };
    }
}