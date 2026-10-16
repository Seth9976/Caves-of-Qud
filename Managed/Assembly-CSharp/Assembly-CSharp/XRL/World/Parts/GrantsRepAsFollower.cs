using System;
using System.Collections.Generic;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x020011A8 RID: 4520
	[Serializable]
	public class GrantsRepAsFollower : IPart
	{
		// Token: 0x0600B3A5 RID: 45989 RVA: 0x00418B5A File Offset: 0x00416D5A
		public GrantsRepAsFollower()
		{
		}

		// Token: 0x0600B3A6 RID: 45990 RVA: 0x00418B6D File Offset: 0x00416D6D
		public GrantsRepAsFollower(string _Faction, int Value)
			: this()
		{
			this.Faction = _Faction;
			this.Value = Value;
		}

		// Token: 0x0600B3A7 RID: 45991 RVA: 0x00418B83 File Offset: 0x00416D83
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			GrantsRepAsFollower grantsRepAsFollower = base.DeepCopy(Parent, MapInv) as GrantsRepAsFollower;
			grantsRepAsFollower.AppliedBonus = false;
			return grantsRepAsFollower;
		}

		// Token: 0x0600B3A8 RID: 45992 RVA: 0x00418B9C File Offset: 0x00416D9C
		public override bool SameAs(IPart p)
		{
			GrantsRepAsFollower grantsRepAsFollower = p as GrantsRepAsFollower;
			return !(grantsRepAsFollower.Faction != this.Faction) && grantsRepAsFollower.Value == this.Value && grantsRepAsFollower.AppliedBonus == this.AppliedBonus && base.SameAs(p);
		}

		// Token: 0x0600B3A9 RID: 45993 RVA: 0x00418BEC File Offset: 0x00416DEC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B3AA RID: 45994 RVA: 0x00418BEF File Offset: 0x00416DEF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B3AB RID: 45995 RVA: 0x00418C04 File Offset: 0x00416E04
		private void ApplyBonus(GameObject who)
		{
			if (!this.AppliedBonus && who != null && who.IsPlayer())
			{
				this.AppliedBonus = true;
				if (this.Faction.StartsWith("*allvisiblefactions:"))
				{
					int num = Convert.ToInt32(this.Faction.Split(':', StringSplitOptions.None)[1]);
					using (List<string>.Enumerator enumerator = Factions.GetVisibleFactionNames().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							The.Game.PlayerReputation.Modify(text, num, "GrantsRepAsFollowerApply", null, null, true, true);
						}
						return;
					}
				}
				foreach (string text2 in this.Faction.Split(',', StringSplitOptions.None))
				{
					string text3 = text2;
					int num2 = this.Value;
					if (text2.Contains(':'))
					{
						string[] array2 = text2.Split(':', StringSplitOptions.None);
						text3 = array2[0];
						num2 = Convert.ToInt32(array2[1]);
					}
					The.Game.PlayerReputation.Modify(text3, num2, "GrantsRepAsFollowerApply", null, null, true, true);
				}
			}
		}

		// Token: 0x0600B3AC RID: 45996 RVA: 0x00418D2C File Offset: 0x00416F2C
		private void UnapplyBonus()
		{
			if (this.AppliedBonus)
			{
				this.AppliedBonus = false;
				if (this.Faction.StartsWith("*allvisiblefactions:"))
				{
					int num = Convert.ToInt32(this.Faction.Split(':', StringSplitOptions.None)[1]);
					using (List<string>.Enumerator enumerator = Factions.GetVisibleFactionNames().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							The.Game.PlayerReputation.Modify(text, -num, "GrantsRepAsFollowerUnapply", null, null, true, true);
						}
						return;
					}
				}
				foreach (string text2 in this.Faction.Split(',', StringSplitOptions.None))
				{
					string text3 = text2;
					int num2 = this.Value;
					if (text2.Contains(':'))
					{
						string[] array2 = text2.Split(':', StringSplitOptions.None);
						text3 = array2[0];
						num2 = Convert.ToInt32(array2[1]);
					}
					XRLCore.Core.Game.PlayerReputation.Modify(text3, -num2, "GrantsRepAsFollowerUnapply", null, null, true, true);
				}
			}
		}

		// Token: 0x0600B3AD RID: 45997 RVA: 0x00418E4C File Offset: 0x0041704C
		public void CheckApplyBonus(GameObject who)
		{
			if (this.AppliedBonus)
			{
				if (who == null || !who.IsValid() || !who.IsPlayer() || who.CurrentZone != this.ParentObject.CurrentZone)
				{
					this.UnapplyBonus();
					return;
				}
			}
			else if (who != null && who.IsValid() && who.IsPlayer() && who.CurrentZone == this.ParentObject.CurrentZone)
			{
				this.ApplyBonus(who);
			}
		}

		// Token: 0x0600B3AE RID: 45998 RVA: 0x00418EBB File Offset: 0x004170BB
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == OnDestroyObjectEvent.ID || ID == SuspendingEvent.ID || base.WantEvent(ID, cascade);
		}

		// Token: 0x0600B3AF RID: 45999 RVA: 0x00418ED7 File Offset: 0x004170D7
		public override bool HandleEvent(SuspendingEvent E)
		{
			this.UnapplyBonus();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3B0 RID: 46000 RVA: 0x00418EE6 File Offset: 0x004170E6
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.UnapplyBonus();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3B1 RID: 46001 RVA: 0x00418EF5 File Offset: 0x004170F5
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				this.CheckApplyBonus(this.ParentObject.PartyLeader);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400487A RID: 18554
		public string Faction = "";

		// Token: 0x0400487B RID: 18555
		public int Value;

		// Token: 0x0400487C RID: 18556
		public bool AppliedBonus;
	}
}
