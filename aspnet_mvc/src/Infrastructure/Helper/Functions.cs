using System;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.IO;
using north.Domain;

namespace north
{
    public sealed class Functions
    {
        private Functions()
        {
        }

        private static string RemoveSpecialChars(string text)
        {
            Regex regex = new Regex("[^a-zA-Z0-9 -]");
            return regex.Replace(text, "");
        }

        public static string GetWhereValue(string fieldName, string data, FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.String:
                    return "[" + fieldName + "] LIKE '%" + data + "%'";
                case FieldType.Date:
                    return "[" + fieldName + "] = '" + data + "'";
                case FieldType.Boolean:
                    if (data == "false")
                        return "([" + fieldName + "] = 0 OR [" + fieldName + "] IS NULL)";
                    else
                        return "[" + fieldName + "] = 1";
                case FieldType.Numeric:
                    if (data == "0")
                        return "([" + fieldName + "] = " + data + " OR [" + fieldName + "] IS NULL)";
                    else
                        return "[" + fieldName + "] = " + data;
                case FieldType.Decimal:
                    if (data == "0" || data == "0.0" || data == "0.00")
                        return "([" + fieldName + "] = " + data + " OR [" + fieldName + "] IS NULL)";
                    else
                        return "[" + fieldName + "] = " + data;
                default:
                    return "[" + fieldName + "] = '" + data + "'";
            }
        }

        public static int GetPagerStartPage(int currentPage, int numberOfPagesToShow, int totalPages)
        {
            int startPage = 1;

            if (currentPage <= numberOfPagesToShow)
                startPage = 1;
            else if ((currentPage > numberOfPagesToShow) && (currentPage % numberOfPagesToShow == 0))
                startPage = ((currentPage / numberOfPagesToShow) - 1) * numberOfPagesToShow + 1;
            else if (currentPage > numberOfPagesToShow)
                startPage = (currentPage / numberOfPagesToShow) * numberOfPagesToShow + 1;

            return startPage;
        }

        public static int GetPagerEndPage(int startPage, int currentPage, int numberOfPagesToShow, int totalPages)
        {
            int endPage = startPage + (numberOfPagesToShow - 1);

            if (endPage >= totalPages)
                return totalPages;
            else
                return startPage + (numberOfPagesToShow - 1);
        }
    }
}
