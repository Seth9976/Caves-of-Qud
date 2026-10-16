using System;

namespace UnityEngine
{
	// Token: 0x0200029F RID: 671
	[Serializable]
	public struct LazyLoadReference<T> where T : Object
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x00040847 File Offset: 0x0003EA47
		public bool isSet
		{
			get
			{
				return this.m_InstanceID != 0;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x00040852 File Offset: 0x0003EA52
		public bool isBroken
		{
			get
			{
				return this.m_InstanceID != 0 && !Object.DoesObjectWithInstanceIDExist(this.m_InstanceID);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x00040870 File Offset: 0x0003EA70
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x000408B0 File Offset: 0x0003EAB0
		public T asset
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				T t;
				if (flag)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)Object.ForceLoadFromInstanceID(this.m_InstanceID));
				}
				return t;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.m_InstanceID = 0;
				}
				else
				{
					bool flag2 = !Object.IsPersistent(value);
					if (flag2)
					{
						throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
					}
					this.m_InstanceID = value.GetInstanceID();
				}
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0004090A File Offset: 0x0003EB0A
		// (set) Token: 0x06002143 RID: 8515 RVA: 0x00040912 File Offset: 0x0003EB12
		public int instanceID
		{
			get
			{
				return this.m_InstanceID;
			}
			set
			{
				this.m_InstanceID = value;
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0004091C File Offset: 0x0003EB1C
		public LazyLoadReference(T asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				this.m_InstanceID = 0;
			}
			else
			{
				bool flag2 = !Object.IsPersistent(asset);
				if (flag2)
				{
					throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
				}
				this.m_InstanceID = asset.GetInstanceID();
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00040976 File Offset: 0x0003EB76
		public LazyLoadReference(int instanceID)
		{
			this.m_InstanceID = instanceID;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x00040980 File Offset: 0x0003EB80
		public static implicit operator LazyLoadReference<T>(T asset)
		{
			return new LazyLoadReference<T>
			{
				asset = asset
			};
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000409A4 File Offset: 0x0003EBA4
		public static implicit operator LazyLoadReference<T>(int instanceID)
		{
			return new LazyLoadReference<T>
			{
				instanceID = instanceID
			};
		}

		// Token: 0x040009C3 RID: 2499
		private const int kInstanceID_None = 0;

		// Token: 0x040009C4 RID: 2500
		[SerializeField]
		private int m_InstanceID;
	}
}
