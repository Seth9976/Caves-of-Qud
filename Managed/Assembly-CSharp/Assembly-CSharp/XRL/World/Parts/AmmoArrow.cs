using System;
using System.Text;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001107 RID: 4359
	[Serializable]
	public class AmmoArrow : IAmmo
	{
		// Token: 0x0600ACD5 RID: 44245 RVA: 0x003F7D13 File Offset: 0x003F5F13
		public override bool SameAs(IPart p)
		{
			return !((p as AmmoArrow).ProjectileObject != this.ProjectileObject) && base.SameAs(p);
		}

		// Token: 0x0600ACD6 RID: 44246 RVA: 0x003F7D36 File Offset: 0x003F5F36
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetProjectileObjectEvent>.ID || ID == QueryEquippableListEvent.ID || (Options.AutogetPrimitiveAmmo && ID == AutoexploreObjectEvent.ID);
		}

		// Token: 0x0600ACD7 RID: 44247 RVA: 0x003F7D6D File Offset: 0x003F5F6D
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (E.Command == null && Options.AutogetPrimitiveAmmo)
			{
				E.Command = ((this.LoadedIn != null) ? "UnloadMagazineAmmo" : "Autoget");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACD8 RID: 44248 RVA: 0x003F7DA0 File Offset: 0x003F5FA0
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood())
			{
				GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(this.ProjectileObject);
				if (blueprintIfExists != null)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("(").Append(blueprintIfExists.GetPartParameter<int>("Projectile", "BasePenetration", 0) + RuleSettings.VISUAL_PENETRATION_BONUS).Append("/")
						.Append(blueprintIfExists.GetPartParameter<string>("Projectile", "BaseDamage", null))
						.Append(")");
					E.AddTag(stringBuilder.ToString(), 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACD9 RID: 44249 RVA: 0x003F7E36 File Offset: 0x003F6036
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType.Contains("AmmoArrow") && !E.List.Contains(this.ParentObject))
			{
				E.List.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACDA RID: 44250 RVA: 0x003F7E75 File Offset: 0x003F6075
		public override bool HandleEvent(GetProjectileObjectEvent E)
		{
			if (!string.IsNullOrEmpty(this.ProjectileObject))
			{
				E.Projectile = GameObject.Create(this.ProjectileObject);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACDB RID: 44251 RVA: 0x003F7E9E File Offset: 0x003F609E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004769 RID: 18281
		public string ProjectileObject;
	}
}
