using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020011F0 RID: 4592
	[Serializable]
	public class Hidden : IPart
	{
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600B6D6 RID: 46806 RVA: 0x0042A7CD File Offset: 0x004289CD
		// (set) Token: 0x0600B6D7 RID: 46807 RVA: 0x0042A7D8 File Offset: 0x004289D8
		public bool Found
		{
			get
			{
				return this._Found;
			}
			set
			{
				if (this._Found != value)
				{
					bool flag = false;
					if (!this._Found && value)
					{
						flag = true;
					}
					this._Found = value;
					if (flag)
					{
						this.RevealInternal(false);
					}
				}
			}
		}

		// Token: 0x0600B6D8 RID: 46808 RVA: 0x0042A810 File Offset: 0x00428A10
		public Hidden()
		{
		}

		// Token: 0x0600B6D9 RID: 46809 RVA: 0x0042A820 File Offset: 0x00428A20
		public Hidden(int Difficulty = 15, bool Silent = false)
			: this()
		{
			this.Difficulty = Difficulty;
			this.Silent = Silent;
		}

		// Token: 0x0600B6DA RID: 46810 RVA: 0x0042A836 File Offset: 0x00428A36
		public Hidden(Hidden source)
			: this()
		{
			this.Difficulty = source.Difficulty;
			this._Found = source._Found;
			this.Silent = source.Silent;
		}

		// Token: 0x0600B6DB RID: 46811 RVA: 0x0042A864 File Offset: 0x00428A64
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

		// Token: 0x0600B6DC RID: 46812 RVA: 0x0042A8DC File Offset: 0x00428ADC
		public override bool SameAs(IPart p)
		{
			Hidden hidden = p as Hidden;
			return hidden._Found == this._Found && hidden.FoundNavigationWeight == this.FoundNavigationWeight && hidden.FoundAdjacentNavigationWeight == this.FoundAdjacentNavigationWeight && hidden.Difficulty == this.Difficulty && hidden.Silent == this.Silent && base.SameAs(p);
		}

		// Token: 0x0600B6DD RID: 46813 RVA: 0x0042A948 File Offset: 0x00428B48
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || (ID == GetAdjacentNavigationWeightEvent.ID && (!this._Found || this.FoundAdjacentNavigationWeight != 0)) || (ID == GetNavigationWeightEvent.ID && (!this._Found || this.FoundNavigationWeight != 0)) || ID == PooledEvent<TakeOnRoleEvent>.ID;
		}

		// Token: 0x0600B6DE RID: 46814 RVA: 0x0042A9A1 File Offset: 0x00428BA1
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (this._Found)
			{
				E.MinWeight(this.FoundNavigationWeight);
				return base.HandleEvent(E);
			}
			E.Weight = E.PriorWeight;
			return false;
		}

		// Token: 0x0600B6DF RID: 46815 RVA: 0x0042A9CE File Offset: 0x00428BCE
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (this._Found)
			{
				E.MinWeight(this.FoundAdjacentNavigationWeight);
				return base.HandleEvent(E);
			}
			E.Weight = E.PriorWeight;
			return false;
		}

		// Token: 0x0600B6E0 RID: 46816 RVA: 0x0042A9FB File Offset: 0x00428BFB
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (!this.Found)
			{
				this.ParentObject.Render.Visible = false;
			}
			else
			{
				this.ParentObject.Render.Visible = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6E1 RID: 46817 RVA: 0x0042AA30 File Offset: 0x00428C30
		public override bool HandleEvent(TakeOnRoleEvent E)
		{
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6E2 RID: 46818 RVA: 0x0042AA45 File Offset: 0x00428C45
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CustomRender");
			Registrar.Register("Searched");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B6E3 RID: 46819 RVA: 0x0042AA68 File Offset: 0x00428C68
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CustomRender")
			{
				if (!this.Found)
				{
					RenderEvent renderEvent = E.GetParameter("RenderEvent") as RenderEvent;
					if (renderEvent != null && (renderEvent.Lit == LightLevel.Radar || renderEvent.Lit == LightLevel.LitRadar))
					{
						this.Found = true;
					}
				}
			}
			else if (E.ID == "Searched" && !this.Found)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Searcher");
				if (E.GetIntParameter("Bonus", 0) + Stat.Random(1, gameObjectParameter.Stat("Intelligence", 0)) >= this.Difficulty)
				{
					this.Found = true;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B6E4 RID: 46820 RVA: 0x0042AB24 File Offset: 0x00428D24
		public new void Reveal()
		{
			this.Found = true;
		}

		// Token: 0x0600B6E5 RID: 46821 RVA: 0x0042AB2D File Offset: 0x00428D2D
		public void Hide()
		{
			this.Found = false;
		}

		// Token: 0x0600B6E6 RID: 46822 RVA: 0x0042AB36 File Offset: 0x00428D36
		public void Reveal(bool Silent = false)
		{
			if (Silent && !this.Found)
			{
				this._Found = true;
				this.RevealInternal(true);
				return;
			}
			this.Reveal();
		}

		// Token: 0x0600B6E7 RID: 46823 RVA: 0x0042AB58 File Offset: 0x00428D58
		private void RevealInternal(bool Silent = false)
		{
			if (this.ParentObject.Render != null)
			{
				this.ParentObject.Render.Visible = true;
				this.ParentObject.ModIntProperty("CustomRenderSources", -1, false);
				if (this.ParentObject.GetIntProperty("CustomRenderSources", 0) <= 0)
				{
					this.ParentObject.Render.CustomRender = false;
				}
			}
			if (!Silent && !this.Silent && base.Visible())
			{
				base.DidX("are", "revealed", "!", null, null, null, null, false, true, null, null, false, true, false, false, false, null);
			}
			this.ParentObject.FireEvent("Found");
		}

		// Token: 0x0600B6E8 RID: 46824 RVA: 0x0042AC04 File Offset: 0x00428E04
		private void HideInternal(bool Silent = false)
		{
			if (this.ParentObject.Render != null)
			{
				this.ParentObject.Render.Visible = false;
				this.ParentObject.ModIntProperty("CustomRenderSources", 1, false);
				if (this.ParentObject.GetIntProperty("CustomRenderSources", 0) > 0)
				{
					this.ParentObject.Render.CustomRender = true;
				}
			}
			if (!Silent && !this.Silent && base.Visible())
			{
				base.DidX("disappear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
			}
			this.ParentObject.FireEvent("Hidden");
		}

		// Token: 0x0600B6E9 RID: 46825 RVA: 0x0042ACA8 File Offset: 0x00428EA8
		public static void CarryOver(GameObject src, GameObject dest)
		{
			Hidden hidden;
			if (src.TryGetPart<Hidden>(out hidden))
			{
				dest.RemovePart<Hidden>();
				dest.AddPart<Hidden>(new Hidden(hidden), true, false);
			}
		}

		// Token: 0x040049B6 RID: 18870
		public bool _Found;

		// Token: 0x040049B7 RID: 18871
		public int Difficulty = 15;

		// Token: 0x040049B8 RID: 18872
		public int FoundNavigationWeight;

		// Token: 0x040049B9 RID: 18873
		public int FoundAdjacentNavigationWeight;

		// Token: 0x040049BA RID: 18874
		public bool Silent;
	}
}
