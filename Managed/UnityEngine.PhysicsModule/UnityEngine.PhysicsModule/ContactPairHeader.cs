using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[UsedByNativeCode]
	public readonly struct ContactPairHeader
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000088C3 File Offset: 0x00006AC3
		public int bodyInstanceID
		{
			get
			{
				return this.m_BodyID;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000088CB File Offset: 0x00006ACB
		public int otherBodyInstanceID
		{
			get
			{
				return this.m_OtherBodyID;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000088D3 File Offset: 0x00006AD3
		public Component body
		{
			get
			{
				return Physics.GetBodyByInstanceID(this.m_BodyID);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x000088E0 File Offset: 0x00006AE0
		public Component otherBody
		{
			get
			{
				return Physics.GetBodyByInstanceID(this.m_OtherBodyID);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000088ED File Offset: 0x00006AED
		public int pairCount
		{
			get
			{
				return (int)this.m_NbPairs;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x000088F5 File Offset: 0x00006AF5
		internal bool hasRemovedBody
		{
			get
			{
				return (this.m_Flags & CollisionPairHeaderFlags.RemovedActor) != (CollisionPairHeaderFlags)0 || (this.m_Flags & CollisionPairHeaderFlags.RemovedOtherActor) > (CollisionPairHeaderFlags)0;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00008910 File Offset: 0x00006B10
		public readonly ref ContactPair GetContactPair(int index)
		{
			return this.GetContactPair_Internal(index);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000892C File Offset: 0x00006B2C
		internal unsafe ContactPair* GetContactPair_Internal(int index)
		{
			bool flag = (long)index >= (long)((ulong)this.m_NbPairs);
			if (flag)
			{
				throw new IndexOutOfRangeException("Invalid ContactPair index. Index should be greater than 0 and less than ContactPairHeader.PairCount");
			}
			return this.m_StartPtr.ToInt64() / (long)sizeof(ContactPair) + index * sizeof(ContactPair);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00008971 File Offset: 0x00006B71
		[Obsolete("Please use ContactPairHeader.bodyInstanceID instead. (UnityUpgradable) -> bodyInstanceID", false)]
		public int BodyInstanceID
		{
			get
			{
				return this.bodyInstanceID;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00008979 File Offset: 0x00006B79
		[Obsolete("Please use ContactPairHeader.otherBodyInstanceID instead. (UnityUpgradable) -> otherBodyInstanceID", false)]
		public int OtherBodyInstanceID
		{
			get
			{
				return this.otherBodyInstanceID;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00008981 File Offset: 0x00006B81
		[Obsolete("Please use ContactPairHeader.body instead. (UnityUpgradable) -> body", false)]
		public Component Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00008989 File Offset: 0x00006B89
		[Obsolete("Please use ContactPairHeader.otherBody instead. (UnityUpgradable) -> otherBody", false)]
		public Component OtherBody
		{
			get
			{
				return this.otherBody;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00008991 File Offset: 0x00006B91
		[Obsolete("Please use ContactPairHeader.pairCount instead. (UnityUpgradable) -> pairCount", false)]
		public int PairCount
		{
			get
			{
				return this.pairCount;
			}
		}

		// Token: 0x040000D6 RID: 214
		internal readonly int m_BodyID;

		// Token: 0x040000D7 RID: 215
		internal readonly int m_OtherBodyID;

		// Token: 0x040000D8 RID: 216
		internal readonly IntPtr m_StartPtr;

		// Token: 0x040000D9 RID: 217
		internal readonly uint m_NbPairs;

		// Token: 0x040000DA RID: 218
		internal readonly CollisionPairHeaderFlags m_Flags;

		// Token: 0x040000DB RID: 219
		internal readonly Vector3 m_RelativeVelocity;
	}
}
