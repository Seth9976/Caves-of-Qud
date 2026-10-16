using System;
using ConsoleLib.Console;
using HistoryKit;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D8C RID: 3468
	[Serializable]
	public class Skulk_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x06008A08 RID: 35336 RVA: 0x00314631 File Offset: 0x00312831
		public Skulk_Tonic()
		{
		}

		// Token: 0x06008A09 RID: 35337 RVA: 0x00314639 File Offset: 0x00312839
		public Skulk_Tonic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008A0A RID: 35338 RVA: 0x00314648 File Offset: 0x00312848
		public void Initialize(int Tier)
		{
			if (If.CoinFlip(null, null))
			{
				this.bOverdose = true;
			}
			this.Duration = Stat.Roll(1000, 1200);
		}

		// Token: 0x06008A0B RID: 35339 RVA: 0x0031466F File Offset: 0x0031286F
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008A0C RID: 35340 RVA: 0x00314672 File Offset: 0x00312872
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008A0D RID: 35341 RVA: 0x00314675 File Offset: 0x00312875
		public override string GetDescription()
		{
			return "{{skulk|skulk}} tonic";
		}

		// Token: 0x06008A0E RID: 35342 RVA: 0x0031467C File Offset: 0x0031287C
		public override string GetStateDescription()
		{
			return "under the effects of {{skulk|skulk}} tonic";
		}

		// Token: 0x06008A0F RID: 35343 RVA: 0x00314683 File Offset: 0x00312883
		public override string GetDetails()
		{
			if (base.Object.IsTrueKin())
			{
				return "+40% Move Speed at night and underground.\n+4 Agility at night and underground.\n-20% Move Speed in the daylight.\n-3 Agility in the daylight.\nCan see in the dark (radius 8).\nSuffers double damage from light-based attacks.";
			}
			return "Has grown burrowing claws.\n+25% Move Speed at night and underground.\n+3 Agility at night and underground.\n-20% Move Speed in the daylight.\n-3 Agility in the daylight.\nCan see in the dark (radius 8).\nSuffers double damage from light-based attacks.";
		}

		// Token: 0x06008A10 RID: 35344 RVA: 0x0031469D File Offset: 0x0031289D
		public void RemoveBonus()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008A11 RID: 35345 RVA: 0x003146AC File Offset: 0x003128AC
		public void ApplyBonus()
		{
			if (base.Object.CurrentCell == null)
			{
				return;
			}
			int num;
			int num2;
			if (base.Object.CurrentCell.ParentZone.Z <= 10 && Calendar.IsDay())
			{
				num = (int)((double)(-(double)base.Object.BaseStat("MoveSpeed", 0)) * 0.2);
				num2 = -3;
			}
			else if (base.Object.IsTrueKin())
			{
				num = (int)((float)base.Object.BaseStat("MoveSpeed", 0) * 0.4f);
				num2 = 4;
			}
			else
			{
				num = (int)((float)base.Object.BaseStat("MoveSpeed", 0) * 0.25f);
				num2 = 3;
			}
			base.StatShifter.SetStatShift("MoveSpeed", -num, false);
			base.StatShifter.SetStatShift("Agility", num2, false);
		}

		// Token: 0x06008A12 RID: 35346 RVA: 0x0031477C File Offset: 0x0031297C
		private void ApplyChanges()
		{
			this.ApplyBonus();
			Mutations mutations = base.Object.RequirePart<Mutations>(false);
			if (base.Object.IsMutant())
			{
				int num = (base.Object.HasPart(typeof(BurrowingClaws)) ? "2-3".RollCached() : 6);
				this.BurrowingClawsID = mutations.AddMutationMod("BurrowingClaws", null, num, Mutations.MutationModifierTracker.SourceType.Tonic, null);
			}
			this.DarkVisionID = mutations.AddMutationMod("DarkVision", null, 1, Mutations.MutationModifierTracker.SourceType.Tonic, null);
			base.Object.GetPart<DarkVision>().Radius += 3;
		}

		// Token: 0x06008A13 RID: 35347 RVA: 0x00314810 File Offset: 0x00312A10
		private void UnapplyChanges()
		{
			this.RemoveBonus();
			Mutations mutations = base.Object.RequirePart<Mutations>(false);
			if (this.BurrowingClawsID != Guid.Empty)
			{
				mutations.RemoveMutationMod(this.BurrowingClawsID);
			}
			base.Object.GetPart<DarkVision>().Radius -= 3;
			if (this.DarkVisionID != Guid.Empty)
			{
				mutations.RemoveMutationMod(this.DarkVisionID);
			}
		}

		// Token: 0x06008A14 RID: 35348 RVA: 0x00314884 File Offset: 0x00312A84
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object.GetHeartCount() > 1)
				{
					Popup.Show("Your hearts begin to beat faster and your pupils dilate.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("Your heart begins to beat faster and your pupils dilate.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			if (Object.GetLongProperty("Overdosing", 0L) == 1L || this.bOverdose)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			Skulk_Tonic skulk_Tonic;
			if (Object.TryGetEffect<Skulk_Tonic>(out skulk_Tonic))
			{
				skulk_Tonic.Duration += this.Duration;
				return false;
			}
			this.ApplyChanges();
			return true;
		}

		// Token: 0x06008A15 RID: 35349 RVA: 0x00314921 File Offset: 0x00312B21
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("Your heart rate returns to normal and your pupils shrink.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.UnapplyChanges();
		}

		// Token: 0x06008A16 RID: 35350 RVA: 0x00314946 File Offset: 0x00312B46
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06008A17 RID: 35351 RVA: 0x0031495C File Offset: 0x00312B5C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				this.ApplyBonus();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A18 RID: 35352 RVA: 0x00314974 File Offset: 0x00312B74
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeApplyDamage");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("Overdose");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008A19 RID: 35353 RVA: 0x003149AC File Offset: 0x00312BAC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage.IsLightDamage())
				{
					damage.Amount *= 2;
				}
			}
			else if (E.ID == "Overdose")
			{
				if (this.Duration > 0)
				{
					this.Duration = 0;
					Skulk_Tonic.ApplyOverdose(base.Object);
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.RemoveBonus();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyBonus();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008A1A RID: 35354 RVA: 0x00314A5C File Offset: 0x00312C5C
		public override void ApplyAllergy(GameObject Object)
		{
			Skulk_Tonic.ApplyOverdose(Object);
		}

		// Token: 0x06008A1B RID: 35355 RVA: 0x00314A64 File Offset: 0x00312C64
		public static void ApplyOverdose(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object.GetLongProperty("Overdosing", 0L) == 1L)
				{
					Popup.Show("Your mutant physiology reacts adversely to the tonic. Your field of vision erupts into a plane of blinding, white light.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("The tonics you ingested react adversely to each other. Your field of vision erupts into a plane of blinding, white light.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			Object.ApplyEffect(new Blind(Stat.Random(1, 10) + 20), null);
		}

		// Token: 0x06008A1C RID: 35356 RVA: 0x00314AD0 File Offset: 0x00312CD0
		public override bool Render(RenderEvent E)
		{
			E.WantsToPaint = !E.DisableFullscreenColorEffects && !E.Alt && base.Object.IsPlayer();
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 15 && num < 25)
			{
				E.Tile = null;
				E.RenderString = "|";
				E.ColorString = "&K";
			}
			return true;
		}

		// Token: 0x06008A1D RID: 35357 RVA: 0x00314B3C File Offset: 0x00312D3C
		public override void OnPaint(ScreenBuffer Buffer)
		{
			Zone currentZone = base.Object.CurrentZone;
			Color blue = The.Color.Blue;
			Color darkBlue = The.Color.DarkBlue;
			Color darkBlack = The.Color.DarkBlack;
			int i = 0;
			int num = 0;
			int height = Buffer.Height;
			while (i < height)
			{
				int j = 0;
				int width = Buffer.Width;
				while (j < width)
				{
					ConsoleChar consoleChar = Buffer[j, i];
					consoleChar._Background = darkBlack;
					if (currentZone.VisibilityMap[num])
					{
						consoleChar._Foreground = blue;
						consoleChar._TileForeground = Skulk_Tonic.ColorBrightBlue;
						consoleChar._Detail = Skulk_Tonic.ColorDarkBlue;
					}
					else
					{
						consoleChar._Foreground = darkBlue;
						consoleChar._TileForeground = Skulk_Tonic.ColorDarkBlue;
						consoleChar._Detail = Skulk_Tonic.ColorDarkBlue;
					}
					j++;
					num++;
				}
				i++;
			}
		}

		// Token: 0x04003A8D RID: 14989
		public Guid BurrowingClawsID;

		// Token: 0x04003A8E RID: 14990
		public Guid DarkVisionID;

		// Token: 0x04003A8F RID: 14991
		public bool bOverdose;

		// Token: 0x04003A90 RID: 14992
		[NonSerialized]
		private static readonly Color ColorBrightBlue = new Color(0f, 0f, 1f);

		// Token: 0x04003A91 RID: 14993
		[NonSerialized]
		private static readonly Color ColorDarkBlue = new Color(0f, 0f, 0.5f);
	}
}
