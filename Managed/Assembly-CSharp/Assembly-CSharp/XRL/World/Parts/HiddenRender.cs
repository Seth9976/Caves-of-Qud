using System;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000FDA RID: 4058
	[Serializable]
	public class HiddenRender : IPart
	{
		// Token: 0x0600A133 RID: 41267 RVA: 0x003AAB0E File Offset: 0x003A8D0E
		public HiddenRender()
		{
			this.RenderString = "?";
			this.ColorString = "&y";
			this.Found = false;
		}

		// Token: 0x0600A134 RID: 41268 RVA: 0x003AAB3C File Offset: 0x003A8D3C
		public override bool SameAs(IPart p)
		{
			HiddenRender hiddenRender = p as HiddenRender;
			return !(hiddenRender.DisplayName != this.DisplayName) && !(hiddenRender.RenderString != this.RenderString) && !(hiddenRender.ColorString != this.ColorString) && !(hiddenRender.Tile != this.Tile) && hiddenRender.RenderLayer == this.RenderLayer && hiddenRender.Visible == this.Visible && !(hiddenRender.LiquidNamePreposition != this.LiquidNamePreposition) && hiddenRender.Found == this.Found && hiddenRender.Difficulty == this.Difficulty && base.SameAs(p);
		}

		// Token: 0x0600A135 RID: 41269 RVA: 0x003AAC00 File Offset: 0x003A8E00
		public override void Initialize()
		{
			base.Initialize();
			if (!this.Found)
			{
				if (this.ParentObject.Render.CustomRender && !this.ParentObject.HasIntProperty("CustomRenderSources"))
				{
					this.ParentObject.ModIntProperty("CustomRenderSources", 1, false);
				}
				this.ParentObject.Render.CustomRender = true;
				this.ParentObject.ModIntProperty("CustomRenderSources", 1, false);
			}
		}

		// Token: 0x0600A136 RID: 41270 RVA: 0x003AAC76 File Offset: 0x003A8E76
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CheckTileChangeEvent>.ID || (ID == GetAdjacentNavigationWeightEvent.ID && !this.Found) || (ID == GetNavigationWeightEvent.ID && !this.Found);
		}

		// Token: 0x0600A137 RID: 41271 RVA: 0x003AACAF File Offset: 0x003A8EAF
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!this.Found)
			{
				E.Weight = E.PriorWeight;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A138 RID: 41272 RVA: 0x003AACCE File Offset: 0x003A8ECE
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!this.Found)
			{
				E.Weight = E.PriorWeight;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A139 RID: 41273 RVA: 0x003AACED File Offset: 0x003A8EED
		public override bool HandleEvent(CheckTileChangeEvent E)
		{
			return !this.Found && base.HandleEvent(E);
		}

		// Token: 0x0600A13A RID: 41274 RVA: 0x003AAD00 File Offset: 0x003A8F00
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CustomRender");
			Registrar.Register("Searched");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A13B RID: 41275 RVA: 0x003AAD20 File Offset: 0x003A8F20
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CustomRender")
			{
				if (!this.Found)
				{
					RenderEvent renderEvent = E.GetParameter("RenderEvent") as RenderEvent;
					if (renderEvent != null && (renderEvent.Lit == LightLevel.Radar || renderEvent.Lit == LightLevel.LitRadar))
					{
						this.Reveal();
					}
				}
			}
			else if (E.ID == "Searched" && !this.Found)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Searcher");
				int intParameter = E.GetIntParameter("Bonus", 0);
				if (Stat.RollResult(gameObjectParameter.Stat("Intelligence", 0)) + intParameter >= this.Difficulty)
				{
					this.Reveal();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A13C RID: 41276 RVA: 0x003AADDC File Offset: 0x003A8FDC
		public new void Reveal()
		{
			if (!this.Found)
			{
				this.Found = true;
				this.ParentObject.ModIntProperty("CustomRenderSources", -1, false);
				if (this.ParentObject.GetIntProperty("CustomRenderSources", 0) <= 0)
				{
					this.ParentObject.Render.CustomRender = false;
				}
				this.ParentObject.Render.DisplayName = this.DisplayName;
				this.ParentObject.Render.RenderString = this.RenderString;
				this.ParentObject.Render.ColorString = this.ColorString;
				this.ParentObject.Render.Tile = this.Tile;
				this.ParentObject.Render.RenderLayer = this.RenderLayer;
				this.ParentObject.Render.Visible = this.Visible;
				if (this.AddStairHighlight)
				{
					this.ParentObject.RequirePart<StairHighlight>(false);
				}
				if (!string.IsNullOrEmpty(this.LiquidNamePreposition))
				{
					LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
					if (liquidVolume != null)
					{
						liquidVolume.NamePreposition = this.LiquidNamePreposition;
					}
				}
				IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false) + " revealed " + The.Player.DescribeDirectionToward(this.ParentObject, false, false) + "!", null, true);
				if (AutoAct.IsInterruptable() && (!this.ParentObject.HasTag("Creature") || The.Player.IsRelevantHostile(this.ParentObject)))
				{
					AutoAct.Interrupt(null, null, this.ParentObject, true);
				}
				this.ParentObject.RemovePart<AnimatedMaterialWater>();
			}
		}

		// Token: 0x040042F1 RID: 17137
		public string DisplayName;

		// Token: 0x040042F2 RID: 17138
		public string RenderString;

		// Token: 0x040042F3 RID: 17139
		public string ColorString;

		// Token: 0x040042F4 RID: 17140
		public string Tile;

		// Token: 0x040042F5 RID: 17141
		public int RenderLayer;

		// Token: 0x040042F6 RID: 17142
		public new bool Visible = true;

		// Token: 0x040042F7 RID: 17143
		public string LiquidNamePreposition;

		// Token: 0x040042F8 RID: 17144
		public bool AddStairHighlight;

		// Token: 0x040042F9 RID: 17145
		public bool Found;

		// Token: 0x040042FA RID: 17146
		public int Difficulty;
	}
}
