using System;
using System.Collections.Generic;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000E4 RID: 228
	public class AttributeDictionary : Dictionary<string, string>
	{
		// Token: 0x1700019B RID: 411
		public string this[string key]
		{
			get
			{
				string text;
				if (base.TryGetValue(key, out text))
				{
					return text;
				}
				return null;
			}
		}
	}
}
