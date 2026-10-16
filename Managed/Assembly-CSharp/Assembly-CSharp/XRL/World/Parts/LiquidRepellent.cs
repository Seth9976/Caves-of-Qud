using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200105F RID: 4191
	[Serializable]
	public class LiquidRepellent : IActivePart
	{
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600A6C5 RID: 42693 RVA: 0x003CFF4A File Offset: 0x003CE14A
		// (set) Token: 0x0600A6C6 RID: 42694 RVA: 0x003CFF52 File Offset: 0x003CE152
		public string Liquids
		{
			get
			{
				return this._Liquids;
			}
			set
			{
				this._Liquids = value;
				this.LiquidList = ((this._Liquids == null) ? null : this.Liquids.CachedCommaExpansion());
			}
		}

		// Token: 0x0600A6C7 RID: 42695 RVA: 0x003CFF77 File Offset: 0x003CE177
		public LiquidRepellent()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A6C8 RID: 42696 RVA: 0x003CFF86 File Offset: 0x003CE186
		public override bool SameAs(IPart p)
		{
			return !((p as LiquidRepellent)._Liquids != this._Liquids) && base.SameAs(p);
		}

		// Token: 0x0600A6C9 RID: 42697 RVA: 0x003CFFA9 File Offset: 0x003CE1A9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID || ID == CanApplyEffectEvent.ID || ID == ForceApplyEffectEvent.ID;
		}

		// Token: 0x0600A6CA RID: 42698 RVA: 0x003CFFCF File Offset: 0x003CE1CF
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600A6CB RID: 42699 RVA: 0x003CFFE3 File Offset: 0x003CE1E3
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600A6CC RID: 42700 RVA: 0x003CFFF7 File Offset: 0x003CE1F7
		public override bool HandleEvent(ForceApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600A6CD RID: 42701 RVA: 0x003D000B File Offset: 0x003CE20B
		private bool Check(IEffectCheckEvent E)
		{
			return !(E.Name == "LiquidCovered") || this.LiquidList != null;
		}

		// Token: 0x0600A6CE RID: 42702 RVA: 0x003D002C File Offset: 0x003CE22C
		private bool Check(IActualEffectCheckEvent E)
		{
			LiquidCovered liquidCovered = E.Effect as LiquidCovered;
			if (liquidCovered != null)
			{
				if (this.LiquidList == null)
				{
					return false;
				}
				if (liquidCovered.Liquid != null)
				{
					foreach (string text in liquidCovered.Liquid.ComponentLiquids.Keys)
					{
						if (this.LiquidList.Contains(text))
						{
							return false;
						}
					}
					return true;
				}
			}
			return true;
		}

		// Token: 0x040044EF RID: 17647
		public string _Liquids;

		// Token: 0x040044F0 RID: 17648
		[NonSerialized]
		private List<string> LiquidList;
	}
}
