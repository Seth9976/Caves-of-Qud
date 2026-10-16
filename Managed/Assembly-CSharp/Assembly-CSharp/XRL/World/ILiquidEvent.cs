using System;
using System.Collections.Generic;
using XRL.Liquids;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020007CF RID: 1999
	[GameEvent(Cascade = 135, Base = true)]
	public abstract class ILiquidEvent : MinEvent
	{
		// Token: 0x06005096 RID: 20630 RVA: 0x001871C3 File Offset: 0x001853C3
		public override int GetCascadeLevel()
		{
			return ILiquidEvent.CascadeLevel;
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x001871CA File Offset: 0x001853CA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x001871D4 File Offset: 0x001853D4
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Liquid = null;
			this.LiquidVolume = null;
			this.Drams = 0;
			this.Skip = null;
			this.SkipList = null;
			this.Filter = null;
			this.Auto = false;
			this.ImpureOkay = false;
			this.SafeOnly = false;
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x00187230 File Offset: 0x00185430
		public bool ApplyTo(GameObject obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj == this.Skip)
			{
				return false;
			}
			if (this.SkipList != null && this.SkipList.Contains(obj))
			{
				return false;
			}
			if (this.Filter != null && !this.Filter(obj))
			{
				return false;
			}
			if (this.SafeOnly)
			{
				if (this.LiquidVolume == null)
				{
					BaseLiquid liquid = LiquidVolume.GetLiquid(this.Liquid);
					if (liquid != null)
					{
						if (!liquid.SafeContainer(obj))
						{
							return false;
						}
					}
					else
					{
						this.LiquidVolume = new LiquidVolume(this.Liquid, 0);
					}
				}
				if (this.LiquidVolume != null && !this.LiquidVolume.SafeContainer(obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002821 RID: 10273
		public new static readonly int CascadeLevel = 135;

		// Token: 0x04002822 RID: 10274
		public GameObject Actor;

		// Token: 0x04002823 RID: 10275
		public string Liquid;

		// Token: 0x04002824 RID: 10276
		public LiquidVolume LiquidVolume;

		// Token: 0x04002825 RID: 10277
		public int Drams;

		// Token: 0x04002826 RID: 10278
		public GameObject Skip;

		// Token: 0x04002827 RID: 10279
		public List<GameObject> SkipList;

		// Token: 0x04002828 RID: 10280
		public Predicate<GameObject> Filter;

		// Token: 0x04002829 RID: 10281
		public bool Auto;

		// Token: 0x0400282A RID: 10282
		public bool ImpureOkay;

		// Token: 0x0400282B RID: 10283
		public bool SafeOnly;
	}
}
