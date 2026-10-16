using System;

namespace XRL.World.AI
{
	/// <remarks>
	/// As of yet only for hostility debugging purposes, may be leveraged somewhere player-facing in the future.
	/// </remarks>
	// Token: 0x02000B53 RID: 2899
	public abstract class IAllyReason : IComposite
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060074EF RID: 29935 RVA: 0x002BEF36 File Offset: 0x002BD136
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		/// <summary>Replace other allegiances with this type of reason.</summary>
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060074F0 RID: 29936 RVA: 0x002BEF39 File Offset: 0x002BD139
		public virtual IAllyReason.ReplaceTarget Replace
		{
			get
			{
				return IAllyReason.ReplaceTarget.None;
			}
		}

		// Token: 0x060074F1 RID: 29937 RVA: 0x002BEF3C File Offset: 0x002BD13C
		public virtual void Initialize(GameObject Actor, GameObject Source, AllegianceSet Set)
		{
		}

		// Token: 0x060074F2 RID: 29938 RVA: 0x002BEF3E File Offset: 0x002BD13E
		public virtual string GetText(GameObject Actor)
		{
			return null;
		}

		// Token: 0x060074F3 RID: 29939 RVA: 0x002BEF41 File Offset: 0x002BD141
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x060074F4 RID: 29940 RVA: 0x002BEF4F File Offset: 0x002BD14F
		public virtual void Read(SerializationReader Reader)
		{
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x04003592 RID: 13714
		public long Time;

		// Token: 0x02001BF4 RID: 7156
		public enum ReplaceTarget
		{
			// Token: 0x04006EC2 RID: 28354
			None,
			// Token: 0x04006EC3 RID: 28355
			Source,
			// Token: 0x04006EC4 RID: 28356
			Type
		}
	}
}
