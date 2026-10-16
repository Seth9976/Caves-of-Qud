using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, maximum teleport distance is increased by
	/// the standard power load bonus, i.e. 2 for the standard overload power
	/// load of 400.
	/// </remarks>
	// Token: 0x0200101D RID: 4125
	[Serializable]
	public class Displacer : IPoweredPart
	{
		// Token: 0x0600A446 RID: 42054 RVA: 0x003C1279 File Offset: 0x003BF479
		public Displacer()
		{
			this.WorksOnEquipper = true;
			this.ChargeUse = 1;
			this.IsPowerLoadSensitive = true;
			this.NameForStatus = "SpatialTransposer";
		}

		// Token: 0x0600A447 RID: 42055 RVA: 0x003C12B0 File Offset: 0x003BF4B0
		public override bool SameAs(IPart p)
		{
			Displacer displacer = p as Displacer;
			return displacer.MinDistance == this.MinDistance && displacer.MaxDistance == this.MaxDistance && displacer.Chance == this.Chance && base.SameAs(p);
		}

		// Token: 0x0600A448 RID: 42056 RVA: 0x003C12FB File Offset: 0x003BF4FB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID;
		}

		// Token: 0x0600A449 RID: 42057 RVA: 0x003C1324 File Offset: 0x003BF524
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			if (this.IsPowerSwitchSensitive && base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) == ActivePartStatus.SwitchedOff)
			{
				E.Add("PowerSwitchOn", 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A44A RID: 42058 RVA: 0x003C137F File Offset: 0x003BF57F
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600A44B RID: 42059 RVA: 0x003C1395 File Offset: 0x003BF595
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A44C RID: 42060 RVA: 0x003C13AB File Offset: 0x003BF5AB
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A44D RID: 42061 RVA: 0x003C13B0 File Offset: 0x003BF5B0
		public override void TurnTick(long TimeTick, int Amount)
		{
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null || currentCell.OnWorldMap())
			{
				return;
			}
			if (this.Chance.in100())
			{
				int num = this.MyPowerLoadLevel();
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num2 = 1;
				int? num3 = new int?(num);
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
				{
					int num4 = this.MaxDistance + IComponent<GameObject>.PowerLoadBonus(num, 100, 150);
					if (this.ActivePartHasMultipleSubjects())
					{
						using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								GameObject gameObject = enumerator.Current;
								this.PerformTeleport(gameObject, null, new int?(num4), new int?(num), false, true, true, false, null);
							}
							return;
						}
					}
					this.PerformTeleport(this.GetActivePartFirstSubject(), null, new int?(num4), new int?(num), false, true, true, false, null);
				}
			}
		}

		// Token: 0x0600A44E RID: 42062 RVA: 0x003C14B4 File Offset: 0x003BF6B4
		private bool PerformTeleport(GameObject Subject, GameObject Actor = null, int? UseMaxDistance = null, int? PowerLoad = null, bool IgnoreSubject = false, bool Voluntary = true, bool DoAIEvaluate = false, bool UsePopups = false, IEvent FromEvent = null)
		{
			if (!GameObject.Validate(ref Subject))
			{
				return false;
			}
			int num = PowerLoad ?? this.MyPowerLoadLevel();
			int num2 = UseMaxDistance ?? (this.MaxDistance + IComponent<GameObject>.PowerLoadBonus(num, 100, 150));
			int num3 = Stat.Random(this.MinDistance, num2);
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
			bool flag2 = gameObject.RandomTeleport(IComponent<GameObject>.Visible(Subject), null, parentObject, gameObject2, FromEvent, 0, num4, flag, null, false, true, Voluntary, UsePopups);
			if (flag2 && DoAIEvaluate)
			{
				this.AIEvaluate(null);
			}
			return flag2;
		}

		// Token: 0x0600A44F RID: 42063 RVA: 0x003C15A0 File Offset: 0x003BF7A0
		public void AIEvaluate(GameObject Actor = null)
		{
			if (!this.IsPowerSwitchSensitive)
			{
				return;
			}
			if (Actor == null)
			{
				Actor = this.ParentObject.Equipped ?? this.ParentObject.Implantee;
			}
			if (!GameObject.Validate(ref Actor))
			{
				return;
			}
			if (Actor.IsPlayer())
			{
				return;
			}
			if (Actor.Target != null)
			{
				return;
			}
			InventoryActionEvent.Check(this.ParentObject, Actor, this.ParentObject, "PowerSwitchOff", false, false, false, false, false, 0, 0, 0, null, null, null, null);
		}

		// Token: 0x0600A450 RID: 42064 RVA: 0x003C1615 File Offset: 0x003BF815
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A451 RID: 42065 RVA: 0x003C1618 File Offset: 0x003BF818
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && this.PerformTeleport(this.ParentObject.Holder ?? this.ParentObject, E.Actor, null, null, true, false, false, true, E))
			{
				E.Identify = true;
				E.IdentifyIfDestroyed = true;
				return true;
			}
			return false;
		}

		// Token: 0x0400441D RID: 17437
		public int MinDistance;

		// Token: 0x0400441E RID: 17438
		public int MaxDistance = 2;

		// Token: 0x0400441F RID: 17439
		public int Chance = 100;
	}
}
