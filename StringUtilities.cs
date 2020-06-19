using System.Text;

namespace UnityTextureRgbPacker
{
    public class StringUtilities
    {
        public static string GetCommonPrefix(string[] names)
        {
            var commonPrefix = new StringBuilder();
            var checker = new StringBuilder();

            var shortestString = GetShortestString(names);

            foreach (var letter in shortestString)
            {
                checker.Append(letter);
                foreach (var eachName in names)
                {
                    if (!eachName.StartsWith(checker.ToString()))
                    {
                        return commonPrefix.ToString();
                    }
                }

                commonPrefix.Append(letter);
            }

            return commonPrefix.ToString();
        }

        private static string GetShortestString(string[] names)
        {
            var shortestString = names[0];
            var shortestStringLength = names[0].Length;

            foreach (var eachName in names)
            {
                if (eachName.Length < shortestStringLength)
                {
                    shortestString = eachName;
                }
            }
            return shortestString;
        }
    }
}

