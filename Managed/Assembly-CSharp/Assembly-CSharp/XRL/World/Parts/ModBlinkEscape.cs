using System;
using System.Collections.Generic;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x0200112B RID: 4395
	[Serializable]
	public class ModBlinkEscape : IModification
	{
		// Token: 0x0600ADE5 RID: 44517 RVA: 0x0040528D File Offset: 0x0040348D
		public ModBlinkEscape()
		{
		}

		// Token: 0x0600ADE6 RID: 44518 RVA: 0x00405295 File Offset: 0x00403495
		public ModBlinkEscape(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600ADE7 RID: 44519 RVA: 0x0040529E File Offset: 0x0040349E
		public override void Configure()
		{
			this.WorksOnEquipper = true;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "EmergencyTeleporter";
		}

		// Token: 0x0600ADE8 RID: 44520 RVA: 0x004052C7 File Offset: 0x004034C7
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexity(3, 2, null);
		}

		// Token: 0x0600ADE9 RID: 44521 RVA: 0x004052D2 File Offset: 0x004034D2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ADEA RID: 44522 RVA: 0x004052F0 File Offset: 0x004034F0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder || this.WorksOnCarrier)
			{
				E.Postfix.AppendRules("Whenever you're about to take avoidable damage, there's " + Grammar.A(this.GetActivationChance(), false) + "% chance you blink away instead.", base.GetEventSensitiveAddStatusSummary(E));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADEB RID: 44523 RVA: 0x00405352 File Offset: 0x00403552
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Object))
			{
				this.CheckBlinkEscape(E.Object, E.Actor, E.Damage);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADEC RID: 44524 RVA: 0x00405382 File Offset: 0x00403582
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600ADED RID: 44525 RVA: 0x00405385 File Offset: 0x00403585
		public static int GetActivationChance(int Tier)
		{
			return 5 + Tier;
		}

		// Token: 0x0600ADEE RID: 44526 RVA: 0x0040538A File Offset: 0x0040358A
		public int GetActivationChance()
		{
			return ModBlinkEscape.GetActivationChance(this.Tier);
		}

		// Token: 0x0600ADEF RID: 44527 RVA: 0x00405398 File Offset: 0x00403598
		public int CheckBlinkEscape(GameObject source, Damage damage)
		{
			int num = 0;
			if (this.ActivePartHasMultipleSubjects())
			{
				using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						if (this.CheckBlinkEscape(gameObject, source, damage))
						{
							num++;
						}
					}
					return num;
				}
			}
			if (this.CheckBlinkEscape(this.GetActivePartFirstSubject(), source, damage))
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600ADF0 RID: 44528 RVA: 0x00405414 File Offset: 0x00403614
		public bool CheckBlinkEscape(GameObject who, GameObject source, Damage damage)
		{
			if (who == null)
			{
				return false;
			}
			if (who.OnWorldMap())
			{
				return false;
			}
			if (damage.HasAttribute("Unavoidable"))
			{
				return false;
			}
			if (!IComponent<GameObject>.CheckRealityDistortionUsability(who, null, who, this.ParentObject, null, null, 0))
			{
				return false;
			}
			if (!this.GetActivationChance().in100() || !base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			if (source != null && source.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("Fate intervenes and you deal no damage to " + who.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
			}
			damage.Amount = 0;
			who.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
			return true;
		}
	}
}
