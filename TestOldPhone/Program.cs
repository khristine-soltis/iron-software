using System;
using System.Text;

public class OldPhonePadConverter
{
    // Converts keypad input to text
    public static string OldPhonePad(string input)
    {
        StringBuilder result = new StringBuilder();  // Final text output
        StringBuilder buffer = new StringBuilder();  // Holds repeated digit presses

        // Map digits to letters (like old phone keypads)
        string[] mapping = {
            "", "", "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        char prevChar = '\0'; // Tracks the last digit entered

        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                if (c == prevChar)
                {
                    buffer.Append(c); // Same key pressed again
                }
                else
                {
                    ProcessBuffer(buffer, mapping, result); // Convert previous key
                    buffer.Clear();
                    buffer.Append(c); // Start new key buffer
                }
            }
            else if (c == ' ')
            {
                ProcessBuffer(buffer, mapping, result); // Convert key before space
                buffer.Clear();
                prevChar = '\0'; // Reset
            }
            else if (c == '*')
            {
                ProcessBuffer(buffer, mapping, result); // Finish last key
                buffer.Clear();
                if (result.Length > 0)
                    result.Remove(result.Length - 1, 1); // Remove last letter
            }
            else if (c == '#')
            {
                ProcessBuffer(buffer, mapping, result); // Final conversion
                break; // End input
            }

            prevChar = c; // Save this key
        }

        return result.ToString(); // Return final message
    }

    // Converts digit buffer (e.g., "777") into the correct letter
    private static void ProcessBuffer(StringBuilder buffer, string[] mapping, StringBuilder result)
    {
        if (buffer.Length == 0) return;

        char key = buffer[0];
        int pressCount = buffer.Length;
        int digit = key - '0';

        if (digit < 2 || digit > 9) return;

        string letters = mapping[digit];
        int index = (pressCount - 1) % letters.Length;
        result.Append(letters[index]);
    }

    // For manual testing
    public static void Main()
    {
        Console.Write("Enter input:\n");
        string input = Console.ReadLine();
        string output = OldPhonePad(input);
        Console.WriteLine($"Output: {output}");
    }
}
