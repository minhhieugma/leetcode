using System.Linq.Expressions;
using System;
using System.Text.Json;
namespace LeetCode;

// https://leetcode.com/problems/can-i-win/
public class No464_CanIWin
{
    [SetUp]
    public void Setup()
    {
    }

    public bool CanIWin(int maxChoosableInteger, int desiredTotal)
    {
        var all_nums = Enumerable.Range(1, maxChoosableInteger).ToArray();

        var mask = 1 << maxChoosableInteger;
        for (int i = 0; i < maxChoosableInteger; i++)
        {
            mask = mask ^ (1 << i);
        }

        var visted_matrix = new bool?[mask + 1];

        if (desiredTotal == 0)
            return true;

        if (all_nums.Sum() < desiredTotal)
            return false;

        var winner = Draw(desiredTotal, all_nums, mask, 0, visted_matrix);
        return winner;
    }

    public bool
        Draw(int desiredTotal, int[] all_nums, int remainder, int score,
         bool?[] visted_matrix)
    {
        if (visted_matrix[remainder] != null)
        {
            return visted_matrix[remainder]!.Value;
        }

        if (score >= desiredTotal)
        {
            visted_matrix[remainder] = false;
            return false;
        }

        for (int i = 0; i < all_nums.Length; i++)
        {
            var cur_mask = 1 << i;
            if ((remainder & cur_mask) == 0)
            {
                continue;
            }

            var cur_remainder = remainder ^ cur_mask;

            var winner = Draw(desiredTotal, all_nums, cur_remainder, score + all_nums[i], visted_matrix);

            if (winner == false)
            {
                visted_matrix[remainder] = true;
                return true;
            }
        }

        visted_matrix[remainder] = false;
        return false;
    }

    [TestCaseSource(nameof(DivideCases))]
    public void Test1(int maxChoosableInteger, int desiredTotal, bool expectedOutput)
    {
        var output = CanIWin(maxChoosableInteger, desiredTotal);

        Assert.AreEqual(expectedOutput, output);
    }

    static IEnumerable<object> DivideCases()
    {
        yield return new object[] { 20, 152, false };
        yield return new object[] { 19, 190, true };
        yield return new object[] { 20, 210, false };
        yield return new object[] { 5, 12, true };
        yield return new object[] { 18, 79, true };
        yield return new object[] { 4, 6, true };
        yield return new object[] { 10, 11, false };
        yield return new object[] { 10, 0, true };
        yield return new object[] { 10, 1, true };
    }
}