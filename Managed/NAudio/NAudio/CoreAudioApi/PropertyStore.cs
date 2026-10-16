using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200012C RID: 300
	public class PropertyStore
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00013D90 File Offset: 0x00011F90
		public int Count
		{
			get
			{
				int num;
				Marshal.ThrowExceptionForHR(this.storeInterface.GetCount(out num));
				return num;
			}
		}

		// Token: 0x17000162 RID: 354
		public PropertyStoreProperty this[int index]
		{
			get
			{
				PropertyKey propertyKey = this.Get(index);
				PropVariant propVariant;
				Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref propertyKey, out propVariant));
				return new PropertyStoreProperty(propertyKey, propVariant);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00013DE0 File Offset: 0x00011FE0
		public bool Contains(PropertyKey key)
		{
			for (int i = 0; i < this.Count; i++)
			{
				PropertyKey propertyKey = this.Get(i);
				if (propertyKey.formatId == key.formatId && propertyKey.propertyId == key.propertyId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000163 RID: 355
		public PropertyStoreProperty this[PropertyKey key]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					PropertyKey propertyKey = this.Get(i);
					if (propertyKey.formatId == key.formatId && propertyKey.propertyId == key.propertyId)
					{
						PropVariant propVariant;
						Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref propertyKey, out propVariant));
						return new PropertyStoreProperty(propertyKey, propVariant);
					}
				}
				return null;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00013E98 File Offset: 0x00012098
		public PropertyKey Get(int index)
		{
			PropertyKey propertyKey;
			Marshal.ThrowExceptionForHR(this.storeInterface.GetAt(index, out propertyKey));
			return propertyKey;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00013EBC File Offset: 0x000120BC
		public PropVariant GetValue(int index)
		{
			PropertyKey propertyKey = this.Get(index);
			PropVariant propVariant;
			Marshal.ThrowExceptionForHR(this.storeInterface.GetValue(ref propertyKey, out propVariant));
			return propVariant;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00013EE6 File Offset: 0x000120E6
		internal PropertyStore(IPropertyStore store)
		{
			this.storeInterface = store;
		}

		// Token: 0x0400071E RID: 1822
		private readonly IPropertyStore storeInterface;
	}
}
