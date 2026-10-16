using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200001B RID: 27
	public abstract class ChangeForPlatform<T> : MonoBehaviour, IEqualityComparer<Platform>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000022DC File Offset: 0x000004DC
		public ChangeForPlatform()
		{
			this.m_platformData = new SerializableDictionary<Platform, T>(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000022F0 File Offset: 0x000004F0
		protected bool GetPlatformSpecificObject(Platform platform, out T obj)
		{
			foreach (KeyValuePair<Platform, T> keyValuePair in this.m_platformData)
			{
				if (this.Equals(keyValuePair.Key, platform))
				{
					obj = keyValuePair.Value;
					return true;
				}
			}
			obj = default(T);
			return false;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002368 File Offset: 0x00000568
		public bool Equals(Platform x, Platform y)
		{
			return (x | y) > Platform.None;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002370 File Offset: 0x00000570
		public int GetHashCode(Platform obj)
		{
			return (int)obj;
		}

		// Token: 0x04000032 RID: 50
		[SerializeField]
		protected SerializableDictionary<Platform, T> m_platformData;
	}
}
