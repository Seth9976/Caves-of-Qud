using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal class ConfigurationEntry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021BC File Offset: 0x000003BC
		[JsonIgnore]
		public string Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021C4 File Offset: 0x000003C4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021CC File Offset: 0x000003CC
		[JsonIgnore]
		public bool IsReadOnly
		{
			get
			{
				return this.m_IsReadOnly;
			}
			internal set
			{
				this.m_IsReadOnly = value;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D5 File Offset: 0x000003D5
		public ConfigurationEntry()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DD File Offset: 0x000003DD
		public ConfigurationEntry(string value, bool isReadOnly = false)
		{
			this.m_Value = value;
			this.m_IsReadOnly = isReadOnly;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021F3 File Offset: 0x000003F3
		public bool TrySetValue(string value)
		{
			if (this.IsReadOnly)
			{
				return false;
			}
			this.m_Value = value;
			return true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002207 File Offset: 0x00000407
		public static implicit operator string(ConfigurationEntry entry)
		{
			return entry.Value;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000220F File Offset: 0x0000040F
		public static implicit operator ConfigurationEntry(string value)
		{
			return new ConfigurationEntry(value, false);
		}

		// Token: 0x04000001 RID: 1
		[JsonRequired]
		[SerializeField]
		private string m_Value;

		// Token: 0x04000002 RID: 2
		[JsonRequired]
		[SerializeField]
		private bool m_IsReadOnly;
	}
}
