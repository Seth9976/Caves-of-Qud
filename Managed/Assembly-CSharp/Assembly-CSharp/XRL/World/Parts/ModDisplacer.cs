using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the maximum teleport distance is increased
	/// by the standard power load bonus, i.e. 2 for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x02001132 RID: 4402
	[Serializable]
	public class ModDisplacer : IModification
	{
		// Token: 0x0600AE59 RID: 44633 RVA: 0x004069E6 File Offset: 0x00404BE6
		public ModDisplacer()
		{
		}

		// Token: 0x0600AE5A RID: 44634 RVA: 0x004069EE File Offset: 0x00404BEE
		public ModDisplacer(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE5B RID: 44635 RVA: 0x004069F7 File Offset: 0x00404BF7
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.ChargeUse = 250;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			this.IsPowerLoadSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "SpatialTransposer";
		}

		// Token: 0x0600AE5C RID: 44636 RVA: 0x00406A32 File Offset: 0x00404C32
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(2, 1, null);
		}

		// Token: 0x0600AE5D RID: 44637 RVA: 0x00406A45 File Offset: 0x00404C45
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AE5E RID: 44638 RVA: 0x00406A7B File Offset: 0x00404C7B
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{displacer|displacer}}", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE5F RID: 44639 RVA: 0x00406AA5 File Offset: 0x00404CA5
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE60 RID: 44640 RVA: 0x00406AC7 File Offset: 0x00404CC7
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600AE61 RID: 44641 RVA: 0x00406ADD File Offset: 0x00404CDD
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600AE62 RID: 44642 RVA: 0x00406AF3 File Offset: 0x00404CF3
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE63 RID: 44643 RVA: 0x00406B16 File Offset: 0x00404D16
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AE64 RID: 44644 RVA: 0x00406B19 File Offset: 0x00404D19
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LauncherProjectileHit");
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponPseudoThrowHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AE65 RID: 44645 RVA: 0x00406B50 File Offset: 0x00404D50
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "LauncherProjectileHit" || E.ID == "WeaponThrowHit" || E.ID == "WeaponPseudoThrowHit")
			{
				this.PerformTeleport(E.GetGameObjectParameter("Defender"), E.GetGameObjectParameter("Attacker"), null, null, false, false, E);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AE66 RID: 44646 RVA: 0x00406BE0 File Offset: 0x00404DE0
		public static string GetDescription(int Tier)
		{
			return string.Concat(new string[]
			{
				"Displacer: When powered, this weapon randomly teleports its target ",
				1.ToString(),
				"-",
				4.ToString(),
				" tiles away on a successful hit."
			});
		}

		// Token: 0x0600AE67 RID: 44647 RVA: 0x00406C28 File Offset: 0x00404E28
		public string GetInstanceDescription()
		{
			int num = this.MyPowerLoadLevel();
			return string.Concat(new string[]
			{
				"Displacer: When powered, this weapon randomly teleports its target ",
				1.ToString(),
				"-",
				(4 + IComponent<GameObject>.PowerLoadBonus(num, 100, 150)).ToString(),
				" tiles away on a successful hit."
			});
		}

		// Token: 0x0600AE68 RID: 44648 RVA: 0x00406C88 File Offset: 0x00404E88
		private bool PerformTeleport(GameObject Subject, GameObject Actor = null, int? UseMaxDistance = null, int? PowerLoad = null, bool IgnoreSubject = false, bool UsePopups = false, IEvent FromEvent = null)
		{
			if (!GameObject.Validate(ref Subject))
			{
				return false;
			}
			int num = PowerLoad ?? this.MyPowerLoadLevel();
			int num2 = UseMaxDistance ?? (4 + IComponent<GameObject>.PowerLoadBonus(num, 100, 150));
			int num3 = Stat.Random(1, num2);
			if (num3 <= 0)
			{
				return false;
			}
			if (!base.IsReady(true, false, false, false, false, false, false, false, IgnoreSubject, false, 1, null, false, 0L, PowerLoad))
			{
				return false;
			}
			GameObject gameObject = Subject;
			int num4 = num3;
			bool flag = !Subject.IsPlayer();
			GameObject parentObject = this.ParentObject;
			GameObject gameObject2 = Actor ?? Subject;
			return gameObject.RandomTeleport(IComponent<GameObject>.Visible(Subject), null, parentObject, gameObject2, FromEvent, 0, num4, flag, null, false, true, false, UsePopups);
		}

		// Token: 0x0600AE69 RID: 44649 RVA: 0x00406D58 File Offset: 0x00404F58
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && !this.ParentObject.HasPart<MissileWeapon>())
			{
				IComponent<GameObject>.XDidYToZ(E.Actor, "bump", "into", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, E.Actor.IsPlayer(), null);
				if (this.PerformTeleport(E.Actor, E.Actor, null, null, true, true, E))
				{
					if (E.Actor.IsPlayer())
					{
						Popup.Show("You are suddenly elsewhere!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					E.Identify = true;
					E.IdentifyIfDestroyed = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040047EA RID: 18410
		public const int MIN_DISTANCE = 1;

		// Token: 0x040047EB RID: 18411
		public const int MAX_DISTANCE = 4;
	}
}
