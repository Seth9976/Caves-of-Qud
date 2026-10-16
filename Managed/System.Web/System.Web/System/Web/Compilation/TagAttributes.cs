using System;
using System.Collections;
using System.Text;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000666 RID: 1638
	internal sealed class TagAttributes
	{
		// Token: 0x0600460B RID: 17931 RVA: 0x000BEC6C File Offset: 0x000BCE6C
		public TagAttributes()
		{
			this.got_hashed = false;
			this.keys = new ArrayList();
			this.values = new ArrayList();
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x000BEC94 File Offset: 0x000BCE94
		private void MakeHash()
		{
			this.atts_hash = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			for (int i = 0; i < this.keys.Count; i++)
			{
				this.CheckServerKey(this.keys[i]);
				this.atts_hash.Add(this.keys[i], this.values[i]);
			}
			this.got_hashed = true;
			this.keys = null;
			this.values = null;
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x000BED11 File Offset: 0x000BCF11
		public bool IsRunAtServer()
		{
			return this.got_hashed;
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x000BED1C File Offset: 0x000BCF1C
		public void Add(object key, object value)
		{
			if (key != null && value != null && string.Compare((string)key, "runat", true, Helpers.InvariantCulture) == 0)
			{
				if (string.Compare((string)value, "server", true) != 0)
				{
					throw new HttpException("runat attribute must have a 'server' value");
				}
				if (this.got_hashed)
				{
					return;
				}
				this.MakeHash();
			}
			if (value != null)
			{
				value = HttpUtility.HtmlDecode(value.ToString());
			}
			if (!this.got_hashed)
			{
				this.keys.Add(key);
				this.values.Add(value);
				return;
			}
			this.CheckServerKey(key);
			if (this.atts_hash.ContainsKey(key))
			{
				throw new HttpException("Tag contains duplicated '" + ((key != null) ? key.ToString() : null) + "' attributes.");
			}
			this.atts_hash.Add(key, value);
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x000BEDEC File Offset: 0x000BCFEC
		public ICollection Keys
		{
			get
			{
				if (!this.got_hashed)
				{
					return this.keys;
				}
				return this.atts_hash.Keys;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x000BEE18 File Offset: 0x000BD018
		public ICollection Values
		{
			get
			{
				if (!this.got_hashed)
				{
					return this.values;
				}
				return this.atts_hash.Values;
			}
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x000BEE44 File Offset: 0x000BD044
		private int CaseInsensitiveSearch(string key)
		{
			for (int i = 0; i < this.keys.Count; i++)
			{
				if (string.Compare((string)this.keys[i], key, true, Helpers.InvariantCulture) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x170015E8 RID: 5608
		public object this[object key]
		{
			get
			{
				if (this.got_hashed)
				{
					return this.atts_hash[key];
				}
				int num = this.CaseInsensitiveSearch((string)key);
				if (num == -1)
				{
					return null;
				}
				return this.values[num];
			}
			set
			{
				if (this.got_hashed)
				{
					this.CheckServerKey(key);
					this.atts_hash[key] = value;
					return;
				}
				int num = this.CaseInsensitiveSearch((string)key);
				this.keys[num] = value;
			}
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06004614 RID: 17940 RVA: 0x000BEF14 File Offset: 0x000BD114
		public int Count
		{
			get
			{
				if (!this.got_hashed)
				{
					return this.keys.Count;
				}
				return this.atts_hash.Count;
			}
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x000BEF35 File Offset: 0x000BD135
		public bool IsDataBound(string att)
		{
			return att != null && this.got_hashed && StrUtils.StartsWith(att, "<%#") && StrUtils.EndsWith(att, "%>");
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x000BEF60 File Offset: 0x000BD160
		public IDictionary GetDictionary(string key)
		{
			if (this.got_hashed)
			{
				return this.atts_hash;
			}
			if (this.tmp_hash == null)
			{
				this.tmp_hash = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			}
			this.tmp_hash.Clear();
			for (int i = this.keys.Count - 1; i >= 0; i--)
			{
				if (key == null || string.Compare(key, (string)this.keys[i], true, Helpers.InvariantCulture) == 0)
				{
					this.tmp_hash[this.keys[i]] = this.values[i];
				}
			}
			return this.tmp_hash;
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x000BF004 File Offset: 0x000BD204
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("TagAttributes {");
			foreach (object obj in this.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append('[');
				stringBuilder.Append(text);
				string text2 = this[text] as string;
				if (text2 != null)
				{
					stringBuilder.AppendFormat("=\"{0}\"", text2);
				}
				stringBuilder.Append("] ");
			}
			if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == ' ')
			{
				StringBuilder stringBuilder2 = stringBuilder;
				int length = stringBuilder2.Length;
				stringBuilder2.Length = length - 1;
			}
			stringBuilder.Append('}');
			if (this.IsRunAtServer())
			{
				stringBuilder.Append(" @Server");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x000BF0F0 File Offset: 0x000BD2F0
		private void CheckServerKey(object key)
		{
			if (key == null || ((string)key).Length == 0)
			{
				throw new HttpException("The server tag is not well formed.");
			}
		}

		// Token: 0x04002527 RID: 9511
		private Hashtable atts_hash;

		// Token: 0x04002528 RID: 9512
		private Hashtable tmp_hash;

		// Token: 0x04002529 RID: 9513
		private ArrayList keys;

		// Token: 0x0400252A RID: 9514
		private ArrayList values;

		// Token: 0x0400252B RID: 9515
		private bool got_hashed;
	}
}
