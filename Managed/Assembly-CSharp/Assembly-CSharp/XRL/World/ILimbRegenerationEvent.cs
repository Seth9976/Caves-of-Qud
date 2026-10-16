using System;

namespace XRL.World
{
	// Token: 0x020007CE RID: 1998
	[GameEvent(Base = true, Cascade = 17)]
	public abstract class ILimbRegenerationEvent : MinEvent
	{
		// Token: 0x06005091 RID: 20625 RVA: 0x0018712B File Offset: 0x0018532B
		public override int GetCascadeLevel()
		{
			return ILimbRegenerationEvent.CascadeLevel;
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x00187132 File Offset: 0x00185332
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x0018713C File Offset: 0x0018533C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Source = null;
			this.Whole = false;
			this.All = false;
			this.IncludeMinor = false;
			this.Voluntary = false;
			this.ParentID = null;
			this.Category = null;
			this.Categories = null;
			this.ExceptCategory = null;
			this.ExceptCategories = null;
		}

		// Token: 0x04002814 RID: 10260
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002815 RID: 10261
		public GameObject Object;

		// Token: 0x04002816 RID: 10262
		public GameObject Actor;

		// Token: 0x04002817 RID: 10263
		public GameObject Source;

		// Token: 0x04002818 RID: 10264
		public bool Whole;

		// Token: 0x04002819 RID: 10265
		public bool All;

		// Token: 0x0400281A RID: 10266
		public bool IncludeMinor;

		// Token: 0x0400281B RID: 10267
		public bool Voluntary;

		// Token: 0x0400281C RID: 10268
		public int? ParentID;

		// Token: 0x0400281D RID: 10269
		public int? Category;

		// Token: 0x0400281E RID: 10270
		public int[] Categories;

		// Token: 0x0400281F RID: 10271
		public int? ExceptCategory;

		// Token: 0x04002820 RID: 10272
		public int[] ExceptCategories;
	}
}
