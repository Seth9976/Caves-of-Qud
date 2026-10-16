using System;
using Qud.API;
using XRL.Language;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001033 RID: 4147
	[Serializable]
	public class FungalInfection : IPart
	{
		// Token: 0x0600A52D RID: 42285 RVA: 0x003C63C2 File Offset: 0x003C45C2
		public override bool SameAs(IPart p)
		{
			return (p as FungalInfection).Curable == this.Curable && base.SameAs(p);
		}

		// Token: 0x0600A52E RID: 42286 RVA: 0x003C63E0 File Offset: 0x003C45E0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<IsAfflictionEvent>.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A52F RID: 42287 RVA: 0x003C6406 File Offset: 0x003C4606
		public override bool HandleEvent(IsAfflictionEvent E)
		{
			return false;
		}

		// Token: 0x0600A530 RID: 42288 RVA: 0x003C6409 File Offset: 0x003C4609
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "Eating");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A531 RID: 42289 RVA: 0x003C6423 File Offset: 0x003C4623
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "Eating");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A532 RID: 42290 RVA: 0x003C643D File Offset: 0x003C463D
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A533 RID: 42291 RVA: 0x003C6440 File Offset: 0x003C4640
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AppliedLiquidCovered");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A534 RID: 42292 RVA: 0x003C6458 File Offset: 0x003C4658
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AppliedLiquidCovered")
			{
				GameObject equipped = this.ParentObject.Equipped;
				if (equipped != null)
				{
					if (IComponent<GameObject>.TheGame.GetStringGameState("FungalCureLiquid", "") == "")
					{
						IComponent<GameObject>.TheCore.GenerateFungalCure();
					}
					string text = "gel";
					string stringGameState = IComponent<GameObject>.TheGame.GetStringGameState("FungalCureLiquid", "");
					LiquidVolume liquidVolume = E.GetParameter("Liquid") as LiquidVolume;
					if (liquidVolume.ContainsLiquid(text) && liquidVolume.ContainsLiquid(stringGameState))
					{
						if (!this.Curable)
						{
							IComponent<GameObject>.EmitMessage(equipped, this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " immune to conventional treatments.", ' ', false, true, false, null, null);
						}
						else if (!equipped.HasEffect(typeof(FungalCureQueasy)))
						{
							IComponent<GameObject>.EmitMessage(equipped, equipped.Poss("skin", true) + " itches furiously.", ' ', false, true, false, null, null);
						}
						else
						{
							this.Cure();
						}
					}
				}
			}
			else if (E.ID == "Eating")
			{
				string text2 = IComponent<GameObject>.TheGame.GetStringGameState("FungalCureWorm", "");
				if (text2.IsNullOrEmpty())
				{
					IComponent<GameObject>.TheCore.GenerateFungalCure();
					text2 = IComponent<GameObject>.TheGame.GetStringGameState("FungalCureWorm", "");
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Food");
				if (gameObjectParameter != null && gameObjectParameter.Blueprint == text2)
				{
					GameObject equipped2 = this.ParentObject.Equipped;
					if (equipped2 != null && !equipped2.HasEffect(typeof(FungalCureQueasy)))
					{
						equipped2.ApplyEffect(new FungalCureQueasy(100), null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A535 RID: 42293 RVA: 0x003C6648 File Offset: 0x003C4848
		public bool Cure()
		{
			GameObject equipped = this.ParentObject.Equipped;
			BodyPart bodyPart = this.ParentObject.EquippedOn();
			string text = ((bodyPart != null) ? bodyPart.GetOrdinalName() : null) ?? "body";
			if (!this.ParentObject.Destroy(null, true, false, null))
			{
				return false;
			}
			if (equipped != null)
			{
				IComponent<GameObject>.EmitMessage(equipped, "The infected crust of skin on " + equipped.poss(text, true) + " loosens and breaks away.", ' ', false, true, false, null, null);
				if (equipped.IsPlayer())
				{
					JournalAPI.AddAccomplishment(string.Concat(new string[]
					{
						"To the dismay of fungi everywhere, you cured the ",
						this.ParentObject.DisplayNameOnlyStripped,
						" infection on your ",
						text,
						"."
					}), string.Concat(new string[]
					{
						"Bless the ",
						Calendar.GetDay(),
						" of ",
						Calendar.GetMonth(),
						", when =name= dissolved a sham alliance with the treacherous fungi by curing the ",
						this.ParentObject.DisplayNameOnlyStripped,
						" infection on ",
						equipped.GetPronounProvider(false).PossessiveAdjective,
						" ",
						text,
						"."
					}), string.Concat(new string[]
					{
						"While traveling around ",
						The.Player.CurrentZone.GetTerrainDisplayName(true),
						", =name= stumbled upon a clan of fungi performing a secret ritual. Because of  ",
						Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
						" <spice.elements.",
						The.Player.GetMythicDomain(5, null),
						".quality.!random>, they furiously rebuked ",
						The.Player.GetPronounProvider(false).Objective,
						" and dissolved the ",
						this.ParentObject.DisplayNameOnlyStripped,
						" on ",
						Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
						" ",
						text,
						"."
					}), null, "general", MuralCategory.BodyExperienceNeutral, MuralWeight.Medium, null, -1L, true);
				}
			}
			return true;
		}

		// Token: 0x04004451 RID: 17489
		public bool Curable = true;
	}
}
