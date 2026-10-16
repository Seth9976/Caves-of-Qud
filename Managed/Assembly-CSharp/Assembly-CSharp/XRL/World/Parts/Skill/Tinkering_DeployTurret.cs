using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013B8 RID: 5048
	[Serializable]
	public class Tinkering_DeployTurret : BaseSkill
	{
		// Token: 0x0600C2CF RID: 49871 RVA: 0x0047238A File Offset: 0x0047058A
		public static void Init()
		{
		}

		// Token: 0x0600C2D0 RID: 49872 RVA: 0x0047238C File Offset: 0x0047058C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C2D1 RID: 49873 RVA: 0x0047238F File Offset: 0x0047058F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C2D2 RID: 49874 RVA: 0x004723A5 File Offset: 0x004705A5
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2D3 RID: 49875 RVA: 0x004723C8 File Offset: 0x004705C8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandDeployTurret");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C2D4 RID: 49876 RVA: 0x004723E0 File Offset: 0x004705E0
		private bool UsableForTurret(GameObject obj)
		{
			if (obj == this.ParentObject)
			{
				return false;
			}
			MissileWeapon part = obj.GetPart<MissileWeapon>();
			return part != null && part.FiresManually;
		}

		// Token: 0x0600C2D5 RID: 49877 RVA: 0x00472410 File Offset: 0x00470610
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandDeployTurret")
			{
				if (base.OnWorldMap)
				{
					this.ParentObject.Fail("You cannot do that on the world map.");
					return false;
				}
				if (!this.ParentObject.CanMoveExtremities("Deploy Turret", true, false, true))
				{
					return false;
				}
				List<GameObject> objects = this.ParentObject.Inventory.GetObjects(new Predicate<GameObject>(this.UsableForTurret));
				if (objects.Count == 0)
				{
					this.ParentObject.Fail("You have no missile weapons to deploy.");
					return false;
				}
				GameObject gameObject = Popup.PickGameObject("[Select a weapon to deploy on the turret]\n\n", objects, true, false, true, null, 0, false);
				if (gameObject == null)
				{
					return false;
				}
				Cell cell = base.PickDirection("Deploy " + gameObject.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, true, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true), null);
				if (cell == null)
				{
					return false;
				}
				if (!cell.IsPassable(null, true) || cell.HasObjectWithTag("ExcavatoryTerrainFeature"))
				{
					this.ParentObject.Fail("You can't deploy there!");
					return false;
				}
				gameObject = gameObject.RemoveOne();
				GameObject gameObject2 = IntegratedWeaponHosts.GenerateTurret(gameObject, this.ParentObject, false);
				cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				gameObject2.MakeActive(false);
				gameObject2.PlayWorldSound("Sounds/Robot/sfx_turret_deploy", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				base.DidXToY("deploy", gameObject2, null, null, null, null, this.ParentObject, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
				this.ParentObject.UseEnergy(2000, "Skill Tinkering DeployTurret", null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C2D6 RID: 49878 RVA: 0x004725CC File Offset: 0x004707CC
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Deploy Turret", "CommandDeployTurret", "Tinkering", null, "\u009d", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C2D7 RID: 49879 RVA: 0x00472614 File Offset: 0x00470814
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004DA9 RID: 19881
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
