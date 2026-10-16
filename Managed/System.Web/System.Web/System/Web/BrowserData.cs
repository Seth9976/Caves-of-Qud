using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000065 RID: 101
	internal sealed class BrowserData
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00007678 File Offset: 0x00005878
		public BrowserData(string pattern)
		{
			int num = pattern.IndexOfAny(BrowserData.wildchars);
			if (num == -1)
			{
				this.text = pattern;
				return;
			}
			this.pattern = pattern.Substring(num);
			this.text = pattern.Substring(0, num);
			if (this.text.Length == 0)
			{
				this.text = null;
			}
			this.pattern = this.pattern.Replace(".", "\\.");
			this.pattern = this.pattern.Replace("(", "\\(");
			this.pattern = this.pattern.Replace(")", "\\)");
			this.pattern = this.pattern.Replace("[", "\\[");
			this.pattern = this.pattern.Replace("]", "\\]");
			this.pattern = this.pattern.Replace('?', '.');
			this.pattern = this.pattern.Replace("*", ".*");
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00007794 File Offset: 0x00005994
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000779C File Offset: 0x0000599C
		public BrowserData Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000077A5 File Offset: 0x000059A5
		public void Add(string key, string value)
		{
			if (this.data == null)
			{
				this.data = new ListDictionary();
			}
			this.data.Add(key, value);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000077C8 File Offset: 0x000059C8
		public Hashtable GetProperties(Hashtable tbl)
		{
			if (this.parent != null)
			{
				this.parent.GetProperties(tbl);
			}
			if (this.data["browser"] != null)
			{
				tbl["browser"] = this.data["browser"];
			}
			else if (tbl["browser"] == null)
			{
				tbl["browser"] = "*";
			}
			if (!tbl.ContainsKey("browsers"))
			{
				tbl["browsers"] = new ArrayList();
			}
			((ArrayList)tbl["browsers"]).Add(tbl["browser"]);
			foreach (object obj in this.data.Keys)
			{
				string text = (string)obj;
				tbl[text.ToLower(Helpers.InvariantCulture).Trim()] = this.data[text];
			}
			return tbl;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000078E4 File Offset: 0x00005AE4
		public string GetParentName()
		{
			return (string)(this.data.Contains("parent") ? this.data["parent"] : null);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00007910 File Offset: 0x00005B10
		public string GetAlternateBrowser()
		{
			if (this.pattern != null)
			{
				return null;
			}
			return this.text;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00007922 File Offset: 0x00005B22
		public string GetBrowser()
		{
			if (this.pattern == null)
			{
				return this.text;
			}
			return (string)this.data["browser"];
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00007948 File Offset: 0x00005B48
		public bool IsMatch(string expression)
		{
			if (expression == null || expression.Length == 0)
			{
				return false;
			}
			if (this.text != null)
			{
				if (this.text[0] != expression[0] || string.Compare(this.text, 1, expression, 1, this.text.Length - 1, false, Helpers.InvariantCulture) != 0)
				{
					return false;
				}
				expression = expression.Substring(this.text.Length);
			}
			if (this.pattern == null)
			{
				return expression.Length == 0;
			}
			object obj = this.this_lock;
			lock (obj)
			{
				if (this.regex == null)
				{
					this.regex = new Regex(this.pattern);
				}
			}
			return this.regex.Match(expression).Success;
		}

		// Token: 0x04000E4C RID: 3660
		private static char[] wildchars = new char[] { '*', '?' };

		// Token: 0x04000E4D RID: 3661
		private object this_lock = new object();

		// Token: 0x04000E4E RID: 3662
		private BrowserData parent;

		// Token: 0x04000E4F RID: 3663
		private string text;

		// Token: 0x04000E50 RID: 3664
		private string pattern;

		// Token: 0x04000E51 RID: 3665
		private Regex regex;

		// Token: 0x04000E52 RID: 3666
		private ListDictionary data;
	}
}
