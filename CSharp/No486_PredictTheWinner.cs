using System.Linq.Expressions;
using System;
using System.Text.Json;
namespace LeetCode;

// https://leetcode.com/problems/predict-the-winner/
public class No486_PredictTheWinner
{
    [SetUp]
    public void Setup()
    {
    }

    public bool PredictTheWinner(int[] nums)
    {
        var visted_matrix = new int?[nums.Length, nums.Length + 2, 2, 2, 2];

        Draw(nums, 0, nums.Length - 1, true, true, out var player1, out var player2, visted_matrix);
        if (player1 >= player2)
            return true;
        else
        {
            Draw(nums, 0, nums.Length - 1, false, true, out var player12, out var player22, visted_matrix);
            if (player12 >= player22)
                return true;
        }

        return false;
    }

    public void
        Draw(int[] nums, int start, int end, bool takeFirst, bool firstPlayer,
        out int player1, out int player2, int?[,,,,] visted_matrix)
    {

        player1 = 0; player2 = 0;
        if (start > end)
            return;

        if (visted_matrix[start, end, takeFirst ? 0 : 1, firstPlayer ? 0 : 1, 0] != null)
        {
            player1 = visted_matrix[start, end, takeFirst ? 0 : 1, firstPlayer ? 0 : 1, 0]!.Value;
            player2 = visted_matrix[start, end, takeFirst ? 0 : 1, firstPlayer ? 0 : 1, 1]!.Value;

            return;
        }

        var at = takeFirst ? start : end;


        // Console.WriteLine($"{(firstPlayer ? "Player 1" : "Player 2")}-{at} with val {nums[at]}");

        var next_start = start + (takeFirst ? 1 : 0);
        var next_end = end - (takeFirst ? 0 : 1);

        Draw(nums, next_start, next_end, true, !firstPlayer, out var player1ValLeft, out var player2ValLeft, visted_matrix);
        Draw(nums, next_start, next_end, false, !firstPlayer, out var player1ValRight, out var player2ValRight, visted_matrix);

        if (firstPlayer)
        {
            if (player2ValLeft >= player2ValRight)
            {
                player1 = nums[at] + player1ValLeft;
                player2 = player2ValLeft;
            }
            else
            {
                player1 = nums[at] + player1ValRight;
                player2 = player2ValRight;
            }
        }
        else
        {
            if (player1ValLeft >= player1ValRight)
            {
                player1 = player1ValLeft;
                player2 = nums[at] + player2ValLeft;
            }
            else
            {
                player1 = player1ValRight;
                player2 = nums[at] + player2ValRight;
            }

        }

        visted_matrix[start, end, takeFirst ? 0 : 1, firstPlayer ? 0 : 1, 0] = player1;
        visted_matrix[start, end, takeFirst ? 0 : 1, firstPlayer ? 0 : 1, 1] = player2;

        // Console.WriteLine($"{"Player 1"}->{player1}");
        // Console.WriteLine($"{"Player 2"}->{player2}");
    }


    [TestCaseSource(nameof(DivideCases))]
    public void Test1(int[] nums, bool expectedOutput)
    {
        var output = PredictTheWinner(nums);

        Assert.AreEqual(expectedOutput, output);
    }

    static IEnumerable<object> DivideCases()
    {
        yield return new object[] { JsonSerializer.Deserialize<int[]>("[1, 5, 2]")!, false };
        yield return new object[] { JsonSerializer.Deserialize<int[]>("[1,5,233,7]")!, true };
        yield return new object[] { JsonSerializer.Deserialize<int[]>("[1, 5, 2, 3]")!, true };
        yield return new object[] { JsonSerializer.Deserialize<int[]>("[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20]")!, true };
    }
}