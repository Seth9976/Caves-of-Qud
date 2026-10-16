using System;
using Qud.API;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D27 RID: 3367
	[Serializable]
	public class Monochrome : Effect, ITierInitialized
	{
		// Token: 0x060084B8 RID: 33976 RVA: 0x002FF5DC File Offset: 0x002FD7DC
		public Monochrome()
		{
			this.Duration = 1;
			this.DisplayName = "monochrome";
		}

		// Token: 0x060084B9 RID: 33977 RVA: 0x002FF5F6 File Offset: 0x002FD7F6
		public override string GetDetails()
		{
			return "Sees shades of only a single color.";
		}

		// Token: 0x060084BA RID: 33978 RVA: 0x002FF5FD File Offset: 0x002FD7FD
		public override int GetEffectType()
		{
			return 100679700;
		}

		// Token: 0x060084BB RID: 33979 RVA: 0x002FF604 File Offset: 0x002FD804
		public override string GetDescription()
		{
			return "monochrome";
		}

		// Token: 0x060084BC RID: 33980 RVA: 0x002FF60B File Offset: 0x002FD80B
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060084BD RID: 33981 RVA: 0x002FF610 File Offset: 0x002FD810
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent("ApplyDisease"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Disease", this, null))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyMonochrome"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Monochrome", this, null))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_diseased", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Monochrome effect = Object.GetEffect<Monochrome>();
			if (effect != null)
			{
				effect.Duration += this.Duration;
				return false;
			}
			if (Object.IsPlayer())
			{
				int num = Stat.Random(2, 6);
				Achievement.GET_MONOCHROME.Unlock();
				Popup.Show("You have contracted monochrome! Color starts to seep out of the world.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment("You contracted monochrome.", "Woe to the scroundrels and dastards who conspired to have =name= contract univision!", string.Format("Near the location of Omonporch, =name= was captured by bandits. {0} languished in captivity for {1} years, eventually contracting glotrot before escaping to {2}.", The.Player.GetPronounProvider(false).CapitalizedSubjective, num, The.Player.CurrentZone.GetTerrainDisplayName(true)), null, "general", MuralCategory.BodyExperienceBad, MuralWeight.Medium, null, -1L, true);
				this.monochromeApplied = true;
				GameManager.Instance.GreyscaleLevel++;
				The.Game.SetIntGameState("HasMonochrome", 1);
			}
			MonochromeOnset effect2 = Object.GetEffect<MonochromeOnset>();
			if (effect2 != null && effect2.Duration > 0)
			{
				effect2.Duration = 0;
			}
			return true;
		}

		// Token: 0x060084BE RID: 33982 RVA: 0x002FF76B File Offset: 0x002FD96B
		public override void Remove(GameObject Object)
		{
			if (this.monochromeApplied)
			{
				GameManager.Instance.GreyscaleLevel--;
				The.Game.RemoveIntGameState("HasMonochrome");
			}
		}

		// Token: 0x060084BF RID: 33983 RVA: 0x002FF796 File Offset: 0x002FD996
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("FlashbangHit");
			Registrar.Register("DrinkingFrom");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x060084C0 RID: 33984 RVA: 0x002FF7C1 File Offset: 0x002FD9C1
		public override bool Render(RenderEvent E)
		{
			return true;
		}

		// Token: 0x060084C1 RID: 33985 RVA: 0x002FF7C4 File Offset: 0x002FD9C4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.monochromeApplied && !base.Object.IsPlayer() && this.Duration == 1)
				{
					this.Duration = 2;
					GameManager.Instance.GreyscaleLevel--;
				}
				if (this.monochromeApplied && base.Object.IsPlayer() && this.Duration == 2)
				{
					this.Duration = 1;
					GameManager.Instance.GreyscaleLevel++;
				}
				if (this.DrankCure > 0)
				{
					this.DrankCure--;
				}
			}
			else if (E.ID == "FlashbangHit")
			{
				if (this.DrankCure > 0)
				{
					this.Duration = 0;
					this.DrankCure = 0;
					if (this.monochromeApplied && base.Object.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("Color starts to seep into the world.", null, true);
						Achievement.CURE_MONOCHROME.Unlock();
						Popup.Show("You are cured of monochrome.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						JournalAPI.AddAccomplishment("You were cured of monochrome and your vision returned to color.", string.Concat(new string[]
						{
							"Blessed was the ",
							Calendar.GetDay(),
							" of ",
							Calendar.GetMonth(),
							", in the year of ",
							Calendar.GetYear().ToString(),
							" AR, when =name= was cured of greying eye!"
						}), string.Concat(new string[]
						{
							"Some time in =year=, =name= came upon the bandits who had captured ",
							The.Player.GetPronounProvider(false).Objective,
							" and let ",
							The.Player.GetPronounProvider(false).Objective,
							" languish in captivity until ",
							The.Player.GetPronounProvider(false).Subjective,
							" contracted monochrome. =name= murdered their leader <spice.elements.",
							The.Player.GetMythicDomain(5, null),
							".murdermethods.!random> and cured ",
							The.Player.GetPronounProvider(false).Reflexive,
							" of the disease."
						}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Medium, null, -1L, true);
					}
				}
			}
			else if (E.ID == "DrinkingFrom")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Container");
				string stringGameState = The.Game.GetStringGameState("MonochromeCure", "");
				if (gameObjectParameter.LiquidVolume.IsPureLiquid(stringGameState, false))
				{
					this.DrankCure = 51;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003998 RID: 14744
		public bool monochromeApplied;

		// Token: 0x04003999 RID: 14745
		public int DrankCure;
	}
}
