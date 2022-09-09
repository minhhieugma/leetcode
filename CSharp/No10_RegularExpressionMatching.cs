using System;
namespace LeetCode;

// https://leetcode.com/problems/regular-expression-matching/
public class No10_RegularExpressionMatching
{
    [SetUp]
    public void Setup()
    {
    }

    public char[][] ExtractPatterns(string pattern)
    {
        var pattern_arr = pattern.ToArray();

        var patterns = new List<char[]>();

        while (pattern_arr.Length != 0)
        {
            if (pattern_arr[^1] == '*')
            {
                if (patterns.Count == 0
                 || string.Join("", patterns[^1]) != string.Join("", pattern_arr[^2..]))
                {
                    patterns.Add(pattern_arr[^2..]);
                }

                // Remove characters what we have visted
                pattern_arr = pattern_arr[..^2];
            }
            else
            {
                patterns.Add(pattern_arr[^1..]);

                // Remove characters what we have visted
                pattern_arr = pattern_arr[..^1];
            }
        }

        patterns.Reverse();

        Console.WriteLine($"Extracted patterns:{patterns.Count}");
        foreach (var row in patterns)
        {
            Console.WriteLine(string.Join("", row));
        }
        return patterns.ToArray();
    }

    public (bool, char[]) IsSubMatch(char[] arr, char[][] patterns)
    {
        Console.WriteLine($"========================");
        Console.WriteLine($"Remaining patterns: {patterns.Length}");
        Console.WriteLine($"Remaining arr:{arr.Length}");

        if (patterns.Length == 0)
        {
            if (arr.Length == 0)
            {
                return (true, arr);
            }
            else
            {
                return (false, arr);
            }
        }

        var pattern = patterns[0];
        var multiple_times = pattern[^1] == '*';
        Console.WriteLine($"Pattern Length:{pattern.Length}");
        Console.WriteLine($"Pattern::{string.Join("", pattern)}");
        Console.WriteLine($"Input: {string.Join("", arr)}");

        if (multiple_times)
        {
            // Remove * at the end of the pattern
            var matching_char = pattern[0];
            Console.WriteLine($"Matching char:{matching_char}");

            var skipping_result = IsSubMatch(arr[..], patterns[1..]);
            Console.WriteLine($"In case of skipping this star pattern");
            if (skipping_result.Item1)
            {
                return (skipping_result.Item1, skipping_result.Item2);
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"Iterate at:{i}");
                if (arr[i] == matching_char || matching_char == '.')
                {
                    var (result, remaining_arr) = IsSubMatch(arr[(i + 1)..], patterns[1..]);
                    Console.WriteLine($"Matched:{matching_char}. Result:{result}");
                    if (result)
                    {
                        return (result, remaining_arr);
                    }

                }
                else
                {
                    Console.WriteLine($"Break at {i}");
                    break;
                }
            }

            Console.WriteLine("Return false here");
            return (false, arr);
        }
        else
        {
            Console.WriteLine($"String Matching. Pattern:{pattern[0]}. Arr:{string.Join("", arr)}");
            if (pattern.Length > arr.Length)
                return (false, arr);

            if (pattern[0] == '.' || pattern[0] == arr[0])
                return IsSubMatch(arr[1..], patterns[1..]);

            return (false, arr);
        }
    }

    public bool IsMatch(string s, string p)
    {
        var s_arr = s.ToArray();
        var pattern_arr = ExtractPatterns(p);
        Console.WriteLine($"-----------------:{pattern_arr.Length}");

        var (result, remaining_arr) = IsSubMatch(s_arr, pattern_arr);

        return result;
    }

    [TestCaseSource(nameof(DivideCases))]
    public void Test1(string input, string pattern, bool expectedOutput)
    {
        var output = IsMatch(input, pattern);

        Assert.AreEqual(expectedOutput, output);
    }

    static IEnumerable<object> DivideCases()
    {
        // yield return new object[] { "aa", "a", false };
        // yield return new object[] { "aa", "aa", true };
        // yield return new object[] { "aa", "..", true };
        // yield return new object[] { "aa", "a*", true };
        // yield return new object[] { "ab", ".*", true };
        // yield return new object[] { "aab", "c*a*b", true };
        // yield return new object[] { "mississippi", "mis*is*ip*.", true };
        // yield return new object[] { "ab", ".*c", false };
        // yield return new object[] { "aaa", "a*a", true };
        // yield return new object[] { "baaaac", "ba*c", true };
        // yield return new object[] { "aaa", "a*aaa*", true };
        yield return new object[] { "aaaaaaaaaaaaab", "a*a*a*a*a*a*a*a*a*a*b", true };
        yield return new object[] { "aaaaaaaaaaaaab", "a*a*a*a*a*a*a*a*a*a*c", false };
    }
}