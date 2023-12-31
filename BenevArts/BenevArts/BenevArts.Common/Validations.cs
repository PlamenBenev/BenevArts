﻿using System.Text.RegularExpressions;

namespace BenevArts.Common
{
	public class Validations
	{
		public static bool IsValidQuery(string query)
		{
			if (query == "Approved" || query == "Pending" || query == "Declined" || query == "All" || query == null)
			{
				return true;
			}

			string pattern = @"^[a-zA-Z0-9 ]+$";
			return Regex.IsMatch(query, pattern);
		}
	}
}