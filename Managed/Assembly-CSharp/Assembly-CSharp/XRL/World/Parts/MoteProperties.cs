using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001068 RID: 4200
	[Serializable]
	public class MoteProperties : IPart
	{
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600A73A RID: 42810 RVA: 0x003D1DDE File Offset: 0x003CFFDE
		public LightSource pLight
		{
			get
			{
				if (this._pLight == null)
				{
					this._pLight = this.ParentObject.GetPart<LightSource>();
				}
				return this._pLight;
			}
		}

		// Token: 0x0600A73B RID: 42811 RVA: 0x003D1DFF File Offset: 0x003CFFFF
		public override bool SameAs(IPart p)
		{
			return (p as MoteProperties).Fuel == this.Fuel;
		}

		// Token: 0x0600A73C RID: 42812 RVA: 0x003D1E17 File Offset: 0x003D0017
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AddedToInventoryEvent.ID || ID == AfterThrownEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A73D RID: 42813 RVA: 0x003D1E58 File Offset: 0x003D0058
		public override bool HandleEvent(EndTurnEvent E)
		{
			LightSource pLight = this.pLight;
			if (pLight != null && pLight.Lit)
			{
				if (this.Fuel > 3500)
				{
					this.pLight.Radius = 6;
				}
				else if (this.Fuel > 2000)
				{
					this.pLight.Radius = 5;
				}
				else if (this.Fuel > 500)
				{
					this.pLight.Radius = 4;
				}
				else
				{
					this.pLight.Radius = 3;
				}
				if (this.Fuel > 0)
				{
					this.Fuel--;
				}
				if (this.Fuel <= 0)
				{
					GameObject equipped = this.ParentObject.Equipped;
					if (equipped != null && equipped.IsPlayer())
					{
						this.pLight.Lit = false;
						IComponent<GameObject>.AddPlayerMessage(The.Player.Poss(this.ParentObject, true, null) + this.ParentObject.GetVerb("dissipate", true, false, false, true, false) + ".", null, true);
					}
					this.ParentObject.Destroy(null, false, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A73E RID: 42814 RVA: 0x003D1F75 File Offset: 0x003D0175
		public override bool HandleEvent(EquippedEvent E)
		{
			this.Light();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A73F RID: 42815 RVA: 0x003D1F84 File Offset: 0x003D0184
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.Light();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A740 RID: 42816 RVA: 0x003D1F93 File Offset: 0x003D0193
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			this.Light();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A741 RID: 42817 RVA: 0x003D1FA2 File Offset: 0x003D01A2
		public override bool HandleEvent(AfterThrownEvent E)
		{
			this.Light();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A742 RID: 42818 RVA: 0x003D1FB4 File Offset: 0x003D01B4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			LightSource pLight = this.pLight;
			if (pLight != null && pLight.Lit && this.ParentObject.IsReal && this.ParentObject.IsTakeable())
			{
				E.AddTag("(" + this.getLitDescription() + ")", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A743 RID: 42819 RVA: 0x003D2012 File Offset: 0x003D0212
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A744 RID: 42820 RVA: 0x003D2015 File Offset: 0x003D0215
		public void Light()
		{
			if (this.Fuel > 0 && this.pLight != null)
			{
				this.pLight.Lit = true;
			}
		}

		// Token: 0x0600A745 RID: 42821 RVA: 0x003D2034 File Offset: 0x003D0234
		private string getLitDescription()
		{
			if (this.Fuel > 3500)
			{
				return "{{Y|blazing}}";
			}
			if (this.Fuel > 2000)
			{
				return "{{W|bright}}";
			}
			if (this.Fuel > 500)
			{
				return "dim";
			}
			return "{{K|faint}}";
		}

		// Token: 0x0600A746 RID: 42822 RVA: 0x003D2074 File Offset: 0x003D0274
		public override bool Render(RenderEvent E)
		{
			LightSource pLight = this.pLight;
			if (pLight != null && pLight.Lit)
			{
				int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
				if (!Options.DisableTextAnimationEffects)
				{
					this.FrameOffset += Stat.Random(1, 5);
				}
				string text;
				if (num < 15)
				{
					text = "&Y";
				}
				else if (num < 30)
				{
					text = "&W";
				}
				else if (num < 45)
				{
					text = "&C";
				}
				else
				{
					text = "&W";
				}
				E.ApplyColors(text, MoteProperties.ICON_COLOR_PRIORITY);
			}
			return base.Render(E);
		}

		// Token: 0x0400450D RID: 17677
		public static readonly int ICON_COLOR_PRIORITY = 50;

		// Token: 0x0400450E RID: 17678
		public int Fuel = 5000;

		// Token: 0x0400450F RID: 17679
		public int FrameOffset;

		// Token: 0x04004510 RID: 17680
		[NonSerialized]
		private LightSource _pLight;
	}
}
