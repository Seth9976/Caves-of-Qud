using System;
using System.Text;
using Newtonsoft.Json;

namespace XRL.World.Parts
{
	// Token: 0x020010BF RID: 4287
	[Serializable]
	public class Wire : IPart
	{
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600AAB5 RID: 43701 RVA: 0x003E6F4A File Offset: 0x003E514A
		[JsonIgnore]
		public int Length
		{
			get
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject == null)
				{
					return 0;
				}
				return parentObject.Count;
			}
		}

		// Token: 0x0600AAB6 RID: 43702 RVA: 0x003E6F60 File Offset: 0x003E5160
		public override bool SameAs(IPart p)
		{
			Wire wire = p as Wire;
			return !(wire.Material != this.Material) && !(wire.NameColor != this.NameColor) && !(wire.ColorString1 != this.ColorString1) && !(wire.ColorString2 != this.ColorString2) && wire.ColorStringThreshold == this.ColorStringThreshold && base.SameAs(p);
		}

		// Token: 0x0600AAB7 RID: 43703 RVA: 0x003E6FDF File Offset: 0x003E51DF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<StackCountChangedEvent>.ID;
		}

		// Token: 0x0600AAB8 RID: 43704 RVA: 0x003E7005 File Offset: 0x003E5205
		public override bool HandleEvent(StackCountChangedEvent E)
		{
			this.CheckCount(E.NewValue);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAB9 RID: 43705 RVA: 0x003E701A File Offset: 0x003E521A
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			this.CheckCount(this.ParentObject.Count);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AABA RID: 43706 RVA: 0x003E7034 File Offset: 0x003E5234
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Single && E.Understood())
			{
				Wire.SB.Clear();
				if (!string.IsNullOrEmpty(this.NameColor))
				{
					Wire.SB.Append("{{").Append(this.NameColor).Append('|');
				}
				Wire.SB.Append('(').Append(this.ParentObject.Count).Append("')");
				if (!string.IsNullOrEmpty(this.NameColor))
				{
					Wire.SB.Append("}}");
				}
				E.AddClause(Wire.SB.ToString(), -60);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AABB RID: 43707 RVA: 0x003E70F0 File Offset: 0x003E52F0
		private void CheckCount(int Count)
		{
			if (!string.IsNullOrEmpty(this.ColorString1) && !string.IsNullOrEmpty(this.ColorString2))
			{
				GameObject parentObject = this.ParentObject;
				if (((parentObject != null) ? parentObject.Render : null) != null)
				{
					if (Count >= this.ColorStringThreshold)
					{
						this.ParentObject.Render.ColorString = this.ColorString2;
					}
					else
					{
						this.ParentObject.Render.ColorString = this.ColorString1;
					}
					this.ParentObject.Render.IgnoreColorForStack = true;
				}
			}
		}

		// Token: 0x0400465A RID: 18010
		public string Material = "copper";

		// Token: 0x0400465B RID: 18011
		public string NameColor;

		// Token: 0x0400465C RID: 18012
		public string ColorString1;

		// Token: 0x0400465D RID: 18013
		public string ColorString2;

		// Token: 0x0400465E RID: 18014
		public int ColorStringThreshold;

		// Token: 0x0400465F RID: 18015
		[NonSerialized]
		private static StringBuilder SB = new StringBuilder();
	}
}
