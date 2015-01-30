using System.Text.RegularExpressions;

namespace Dblp.Helper
{
    public static class TextHelper
    {
        public static string ExtractYear(this string nameAndYear)
        {
            var regex4 = new Regex(@"\d{4}");
            var year = regex4.Matches(nameAndYear);
            if (year.Count == 1)
            {
                return year[0].ToString();
            }
            var regex2 = new Regex(@"\d{2}");
            var year2 = regex2.Matches(nameAndYear);
            if (year2.Count == 1)
            {
                return "19" + year2[0];
            }
            return "";
        }

        public static string ExtractCiteKeyFromFileName(this string fileName)
        {
            var splittedFileName = fileName.Split('\\');
            var splittedFile = splittedFileName[splittedFileName.Length - 1].Split('.');
            return splittedFileName[splittedFileName.Length - 3] + "\\" + splittedFileName[splittedFileName.Length - 2] + "\\" + splittedFile[0];
        }


        public static string ExtractAbbreviationFromConferenceKey(this string conferenceKey)
        {
            return conferenceKey.Split('/')[2];
        }

    }
}
