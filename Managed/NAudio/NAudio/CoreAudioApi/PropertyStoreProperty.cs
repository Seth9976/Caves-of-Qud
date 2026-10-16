using System;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200012D RID: 301
	public class PropertyStoreProperty
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x00013EF5 File Offset: 0x000120F5
		internal PropertyStoreProperty(PropertyKey key, PropVariant value)
		{
			this.propertyKey = key;
			this.propertyValue = value;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00013F0B File Offset: 0x0001210B
		public PropertyKey Key
		{
			get
			{
				return this.propertyKey;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00013F13 File Offset: 0x00012113
		public object Value
		{
			get
			{
				return this.propertyValue.Value;
			}
		}

		// Token: 0x0400071F RID: 1823
		private readonly PropertyKey propertyKey;

		// Token: 0x04000720 RID: 1824
		private PropVariant propertyValue;
	}
}
