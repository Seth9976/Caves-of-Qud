using System;
using System.Collections.Generic;
using System.Globalization;

namespace NUnit.Framework
{
	// Token: 0x0200005E RID: 94
	public class TestParameters
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00007118 File Offset: 0x00005318
		public int Count
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00007125 File Offset: 0x00005325
		public ICollection<string> Names
		{
			get
			{
				return this._parameters.Keys;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00007132 File Offset: 0x00005332
		public bool Exists(string name)
		{
			return this._parameters.ContainsKey(name);
		}

		// Token: 0x1700005C RID: 92
		public string this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00007149 File Offset: 0x00005349
		public string Get(string name)
		{
			if (!this.Exists(name))
			{
				return null;
			}
			return this._parameters[name];
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00007162 File Offset: 0x00005362
		public string Get(string name, string defaultValue)
		{
			return this.Get(name) ?? defaultValue;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00007170 File Offset: 0x00005370
		public T Get<T>(string name, T defaultValue)
		{
			string text = this.Get(name);
			if (text == null)
			{
				return defaultValue;
			}
			return (T)((object)Convert.ChangeType(text, typeof(T), TestParameters.MODIFIED_INVARIANT_CULTURE));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000071A4 File Offset: 0x000053A4
		internal void Add(string name, string value)
		{
			this._parameters[name] = value;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000071B4 File Offset: 0x000053B4
		private static IFormatProvider CreateModifiedInvariantCulture()
		{
			CultureInfo cultureInfo = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			cultureInfo.NumberFormat.CurrencyGroupSeparator = string.Empty;
			cultureInfo.NumberFormat.NumberGroupSeparator = string.Empty;
			cultureInfo.NumberFormat.PercentGroupSeparator = string.Empty;
			return cultureInfo;
		}

		// Token: 0x04000066 RID: 102
		private static readonly IFormatProvider MODIFIED_INVARIANT_CULTURE = TestParameters.CreateModifiedInvariantCulture();

		// Token: 0x04000067 RID: 103
		private readonly Dictionary<string, string> _parameters = new Dictionary<string, string>();
	}
}
