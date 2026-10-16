using System;

namespace XRL.World.Parts
{
	/// <summary>
	/// Widget to listen for a player death in current zone by Killer, with Weapon.
	/// </summary>
	// Token: 0x02000DFA RID: 3578
	[Serializable]
	public class PlayerDeathAchievement : IPart
	{
		// Token: 0x060090D1 RID: 37073 RVA: 0x003433DE File Offset: 0x003415DE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060090D2 RID: 37074 RVA: 0x003433E1 File Offset: 0x003415E1
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060090D3 RID: 37075 RVA: 0x003433E4 File Offset: 0x003415E4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			GameObject player = The.Player;
			if (player != null)
			{
				Registrar.Register(player, AfterDieEvent.ID, 0, false);
			}
		}

		// Token: 0x060090D4 RID: 37076 RVA: 0x00343408 File Offset: 0x00341608
		public override bool HandleEvent(AfterDieEvent E)
		{
			if (this.ParentObject.IsValid())
			{
				GameObject dying = E.Dying;
				if (dying == null || !dying.IsPlayer())
				{
					return true;
				}
				if ((dying.CurrentZone ?? The.ActiveZone) != this.ParentObject.CurrentZone)
				{
					return true;
				}
				if (!this.Killer.IsNullOrEmpty())
				{
					GameObject killer = E.Killer;
					if (killer == null)
					{
						return true;
					}
					if (!killer.GetBlueprint(true).DescendsFrom(this.Killer))
					{
						return true;
					}
				}
				if (!this.Weapon.IsNullOrEmpty())
				{
					GameObject weapon = E.Weapon;
					if (weapon == null)
					{
						return true;
					}
					if (!weapon.GetBlueprint(true).DescendsFrom(this.Weapon))
					{
						return true;
					}
				}
				AchievementManager.SetAchievement(this.AchievementID);
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C64 RID: 15460
		public string AchievementID = "";

		// Token: 0x04003C65 RID: 15461
		public string Killer;

		// Token: 0x04003C66 RID: 15462
		public string Weapon;
	}
}
