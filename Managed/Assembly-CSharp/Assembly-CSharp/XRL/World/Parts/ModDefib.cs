using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// This part is not used in the base game.
	///
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is not by default, chance to activate is increased by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400, and damage is increased
	/// by the standard power load bonus, i.e. 2 for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x02001185 RID: 4485
	[Serializable]
	public class ModDefib : IModification
	{
		// Token: 0x0600B24D RID: 45645 RVA: 0x00413ABC File Offset: 0x00411CBC
		public ModDefib()
		{
		}

		// Token: 0x0600B24E RID: 45646 RVA: 0x00413AC4 File Offset: 0x00411CC4
		public ModDefib(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B24F RID: 45647 RVA: 0x00413ACD File Offset: 0x00411CCD
		public override void Configure()
		{
			this.WorksOnEquipper = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600B250 RID: 45648 RVA: 0x00413AE0 File Offset: 0x00411CE0
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Body", "Back", null, null, true, true, true, false);
		}

		// Token: 0x0600B251 RID: 45649 RVA: 0x00413B08 File Offset: 0x00411D08
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600B252 RID: 45650 RVA: 0x00413B1B File Offset: 0x00411D1B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B253 RID: 45651 RVA: 0x00413B41 File Offset: 0x00411D41
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{love|defib}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B254 RID: 45652 RVA: 0x00413B6B File Offset: 0x00411D6B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModDefib.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B255 RID: 45653 RVA: 0x00413B8B File Offset: 0x00411D8B
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckDefib(this.GetActivePartFirstSubject());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B256 RID: 45654 RVA: 0x00413BA1 File Offset: 0x00411DA1
		public static string GetDescription(int Tier)
		{
			return "Defib: When powered, if the wearer enters cardiac arrest, this armor will deliver an electrical shock to the wearer every turn until their heart restarts.";
		}

		// Token: 0x0600B257 RID: 45655 RVA: 0x00413BA8 File Offset: 0x00411DA8
		public bool? CheckDefib(GameObject Subject)
		{
			if (!GameObject.Validate(ref Subject))
			{
				return new bool?(false);
			}
			if (!Subject.HasEffect<CardiacArrest>())
			{
				return new bool?(false);
			}
			int num = this.MyPowerLoadLevel();
			int num2 = Stat.Random(1, 4) + IComponent<GameObject>.PowerLoadBonus(num, 100, 150);
			GameObject gameObject = Subject;
			int num3 = num2;
			GameObject parentObject = this.ParentObject;
			gameObject.TakeDamage(num3, "from an {{electrical|electrical shock}} delivered by " + Subject.poss(this.ParentObject, true, null) + ".", "Electric", null, null, parentObject, null, null, null, null, true, false, false, false, false, false, false, false, false, 5, null);
			if (GameObject.Validate(ref Subject) && Subject.HasEffect<CardiacArrest>())
			{
				int num4 = 30 + this.Tier * 5;
				int num5 = IComponent<GameObject>.PowerLoadBonus(num, 100, 10);
				if (num5 != 0)
				{
					num4 = num4 * (100 + num5) / 100;
				}
				if (num4.in100())
				{
					Subject.RemoveEffect<CardiacArrest>();
				}
			}
			if (!Subject.HasEffect<CardiacArrest>())
			{
				return new bool?(true);
			}
			return null;
		}
	}
}
