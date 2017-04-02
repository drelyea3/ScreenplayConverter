using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayConverter
{
    public static class ScriptItemFactory
    {
        private static readonly char[] WhiteSpace = { ' ', '\t' };
        private static readonly char[] DirectionSplitters = { ' ', '\t', '\'', '’', ',', '.' };
        private static readonly char[] LineSeparators = { ' ', '\t', '(', ')' };
        private static readonly string[] TextNumbers = { "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN" };

        private static readonly HashSet<string> AllowedWords = new HashSet<string>(new string[] { "AND", "EXCEPT" });
        private static readonly HashSet<string> DisallowedWords = new HashSet<string>(new string[] { "OK" });

        public static ScriptItem Create(string text)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var item = CreateAct(text) ?? CreateScene(text) ?? CreateCharacter(text) ?? CreateDialogue(text) ?? CreateParenthetical(text);

            return item;
        }

        private static ScriptItem CreateAct(string text)
        {
            if (text.StartsWith("ACT"))
            {
                var parts = text.Split(WhiteSpace, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] == "ACT")
                {
                    if (parts.Length == 1)
                    {
                        return new ScriptItem() { ItemType = ScriptItemType.Act, OriginalText = text, Text = text };
                    }
                    if (parts.Length == 2 && parts[0] == "ACT")
                    {
                        return new ScriptItem() { ItemType = ScriptItemType.Act, OriginalText = text, Text = text };
                    }
                }
            }

            return null;
        }

        private static ScriptItem CreateScene(string text)
        {
            if (text.StartsWith("SCENE"))
            {
                var parts = text.Split(WhiteSpace, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] == "SCENE")
                {
                    if (parts.Length == 1)
                    {
                        return new ScriptItem() { ItemType = ScriptItemType.Scene, OriginalText = text, Text = text };
                    }
                    if (parts.Length == 2 && parts[0] == "SCENE")
                    {
                        return new ScriptItem() { ItemType = ScriptItemType.Scene, OriginalText = text, Text = text };
                    }
                }
            }

            return null;
        }

        private static ScriptItem CreateCharacter(string text)
        {
            bool somethingIsValid = false;

            if (text.StartsWith("OK"))
            {
                return null;
            }

            var parts = text.Split(LineSeparators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (AllowedWords.Contains(part.ToUpper()))
                {
                    continue;
                }

                if (DisallowedWords.Contains(part))
                {
                    return null;
                }

                if (part.IsLowerOrMixedCase())
                {
                    return null;
                }
                else
                {
                    somethingIsValid = true;
                }
            }

            return somethingIsValid ? new ScriptItem() { ItemType = ScriptItemType.Character, OriginalText = text, Text = string.Join(" ", parts) } : null;
        }

        private static ScriptItem CreateDialogue(string text)
        {
            if (!text.StartsWith("(") || !text.EndsWith(")"))
            {
                return new ScriptItem() { ItemType = ScriptItemType.Dialogue, OriginalText = text, Text = text };
            }

            return null;
        }

        private static ScriptItem CreateParenthetical(string text)
        {
            if (text.StartsWith("(") && text.EndsWith(")"))
            {
                return new ScriptItem() { ItemType = ScriptItemType.Parenthetical, OriginalText = text, Text = text };
            }

            return null;
        }

        public static double TextToNumber(string text)
        {
            double value;
            if (double.TryParse(text, out value))
            {
                return value;
            }

            int count = TextNumbers.Length;
            for (int index = 0; index < count; ++index)
            {
                if (TextNumbers[index] == text)
                {
                    return index + 1;
                }
            }

            return -1;
        }

        public static string NumberToText(double number)
        {
            if (number != Math.Truncate(number) || number <= 0 || number >= TextNumbers.Length)
            {
                return number.ToString();
            }

            return TextNumbers[(int)number - 1];
        }
    }

    public static class Extensions
    {
        public static bool IsLowerOrMixedCase(this string s)
        {
            foreach (char c in s)
            {
                if (char.IsLower(c))
                {
                    return true;
                }
            }

            return false;
        }
    }

}
