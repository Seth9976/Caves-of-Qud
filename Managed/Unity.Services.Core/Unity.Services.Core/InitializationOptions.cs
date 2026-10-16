using System;
using System.Collections.Generic;

namespace Unity.Services.Core
{
	// Token: 0x02000008 RID: 8
	public class InitializationOptions
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000218B File Offset: 0x0000038B
		internal IDictionary<string, object> Values { get; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002193 File Offset: 0x00000393
		public InitializationOptions()
			: this(new Dictionary<string, object>())
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021A0 File Offset: 0x000003A0
		internal InitializationOptions(IDictionary<string, object> values)
		{
			this.Values = values;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021AF File Offset: 0x000003AF
		internal InitializationOptions(InitializationOptions source)
			: this(new Dictionary<string, object>(source.Values))
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021C2 File Offset: 0x000003C2
		public bool TryGetOption(string key, out bool option)
		{
			return this.TryGetOption<bool>(key, out option);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021CC File Offset: 0x000003CC
		public bool TryGetOption(string key, out int option)
		{
			return this.TryGetOption<int>(key, out option);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021D6 File Offset: 0x000003D6
		public bool TryGetOption(string key, out float option)
		{
			return this.TryGetOption<float>(key, out option);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021E0 File Offset: 0x000003E0
		public bool TryGetOption(string key, out string option)
		{
			return this.TryGetOption<string>(key, out option);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021EC File Offset: 0x000003EC
		private bool TryGetOption<T>(string key, out T option)
		{
			option = default(T);
			object obj;
			if (this.Values.TryGetValue(key, out obj) && obj is T)
			{
				T t = (T)((object)obj);
				option = t;
				return true;
			}
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002229 File Offset: 0x00000429
		public InitializationOptions SetOption(string key, bool value)
		{
			this.Values[key] = value;
			return this;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000223E File Offset: 0x0000043E
		public InitializationOptions SetOption(string key, int value)
		{
			this.Values[key] = value;
			return this;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002253 File Offset: 0x00000453
		public InitializationOptions SetOption(string key, float value)
		{
			this.Values[key] = value;
			return this;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002268 File Offset: 0x00000468
		public InitializationOptions SetOption(string key, string value)
		{
			this.Values[key] = value;
			return this;
		}
	}
}
