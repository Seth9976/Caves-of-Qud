using System;

namespace System.Collections.Specialized
{
	// Token: 0x020004BB RID: 1211
	internal class CaseSensitiveStringDictionary : StringDictionary
	{
		// Token: 0x170007F5 RID: 2037
		public override string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key] = value;
			}
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x00087B68 File Offset: 0x00085D68
		public override void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key, value);
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00087B85 File Offset: 0x00085D85
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00087BA1 File Offset: 0x00085DA1
		public override void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key);
		}
	}
}
