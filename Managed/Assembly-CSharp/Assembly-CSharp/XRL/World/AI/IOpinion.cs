using System;
using UnityEngine;

namespace XRL.World.AI
{
	/// <remarks>
	/// As of yet only for hostility debugging purposes, may be leveraged somewhere player-facing in the future.
	/// Current feeling values and attitude thresholds are subject to change.
	/// </remarks>
	// Token: 0x02000B66 RID: 2918
	public abstract class IOpinion : IComposite
	{
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06007520 RID: 29984 RVA: 0x002BF270 File Offset: 0x002BD470
		public int Value
		{
			get
			{
				return Mathf.RoundToInt((float)this.BaseValue * this.Magnitude);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06007521 RID: 29985 RVA: 0x002BF285 File Offset: 0x002BD485
		public virtual bool WantFieldReflection
		{
			get
			{
				return true;
			}
		}

		/// <summary>The base feeling change from this opinion.</summary>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06007522 RID: 29986
		public abstract int BaseValue { get; }

		/// <summary>The turns it takes for this opinion to abate.</summary>
		/// <remarks>Counts from this opinion's <see cref="F:XRL.World.AI.IOpinion.Time" />.</remarks>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06007523 RID: 29987 RVA: 0x002BF288 File Offset: 0x002BD488
		public virtual int Duration
		{
			get
			{
				if (this.BaseValue >= 0)
				{
					return 0;
				}
				return 16800;
			}
		}

		/// <summary>The turns that must elapse before the opinion can be re-applied.</summary>
		/// <remarks>Counts from this opinion's <see cref="F:XRL.World.AI.IOpinion.Time" />.</remarks>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06007524 RID: 29988 RVA: 0x002BF29A File Offset: 0x002BD49A
		public virtual int Cooldown
		{
			get
			{
				return 1200;
			}
		}

		/// <summary>The maximum magnitude for this opinion.</summary>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06007525 RID: 29989 RVA: 0x002BF2A1 File Offset: 0x002BD4A1
		public virtual float Limit
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06007526 RID: 29990 RVA: 0x002BF2A8 File Offset: 0x002BD4A8
		public virtual void Write(SerializationWriter Writer)
		{
		}

		// Token: 0x06007527 RID: 29991 RVA: 0x002BF2AA File Offset: 0x002BD4AA
		public virtual void Read(SerializationReader Reader)
		{
		}

		// Token: 0x06007528 RID: 29992 RVA: 0x002BF2AC File Offset: 0x002BD4AC
		public virtual string GetText(GameObject Actor)
		{
			return null;
		}

		// Token: 0x06007529 RID: 29993 RVA: 0x002BF2AF File Offset: 0x002BD4AF
		public virtual bool HandleEvent(AfterAddOpinionEvent E)
		{
			return true;
		}

		/// <summary>The current magnitude of the opinion, acting as a multiplier for its feeling value.</summary>
		// Token: 0x04003593 RID: 13715
		public float Magnitude;

		/// <summary>When this type of opinion was last added.</summary>
		// Token: 0x04003594 RID: 13716
		public long Time;
	}
}
