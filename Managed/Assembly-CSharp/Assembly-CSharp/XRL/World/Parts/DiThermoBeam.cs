using System;

namespace XRL.World.Parts
{
	// Token: 0x02001018 RID: 4120
	[Serializable]
	public class DiThermoBeam : IPart
	{
		// Token: 0x0600A423 RID: 42019 RVA: 0x003C06FD File Offset: 0x003BE8FD
		public override void Attach()
		{
			this.ProjectileBlueprint = GameObjectFactory.Factory.GetBlueprint(this.ProjectileObject);
			base.Attach();
		}

		// Token: 0x0600A424 RID: 42020 RVA: 0x003C071B File Offset: 0x003BE91B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<GetMissileWeaponPerformanceEvent>.ID;
		}

		// Token: 0x0600A425 RID: 42021 RVA: 0x003C0744 File Offset: 0x003BE944
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Flip", "flip", "FlipBeam", null, 'f', false, 5, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A426 RID: 42022 RVA: 0x003C077B File Offset: 0x003BE97B
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "FlipBeam")
			{
				this.FlipBeam(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A427 RID: 42023 RVA: 0x003C07A2 File Offset: 0x003BE9A2
		public override bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			E.DamageColor = (this.Flipped ? "C" : "R");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A428 RID: 42024 RVA: 0x003C07C8 File Offset: 0x003BE9C8
		public void FlipBeam(GameObject Actor)
		{
			this.Flipped = !this.Flipped;
			Render render = this.ParentObject.Render;
			if (this.Flipped && render.DisplayName == this.HeatName)
			{
				render.DisplayName = this.ColdName;
			}
			else if (!this.Flipped && render.DisplayName == this.ColdName)
			{
				render.DisplayName = this.HeatName;
			}
			if (this.Flipped && render.ColorString == this.HeatColorString)
			{
				render.ColorString = this.ColdColorString;
				render.DetailColor = this.ColdDetailColor;
			}
			else if (!this.Flipped && render.ColorString == this.ColdColorString)
			{
				render.ColorString = this.HeatColorString;
				render.DetailColor = this.HeatDetailColor;
			}
			if (Actor.IsPlayer())
			{
				base.PlayWorldSound("Sounds/Interact/sfx_interact_thermobeamGun_setting_flip", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				IComponent<GameObject>.XDidYToZ(Actor, "flip", "the thermal polarity on the", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
			}
		}

		// Token: 0x0600A429 RID: 42025 RVA: 0x003C0905 File Offset: 0x003BEB05
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A42A RID: 42026 RVA: 0x003C0908 File Offset: 0x003BEB08
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ModifyMissileWeaponAngle");
			Registrar.Register("ProjectileSetup");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A42B RID: 42027 RVA: 0x003C0928 File Offset: 0x003BEB28
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ModifyMissileWeaponAngle")
			{
				if (this.LastMod == -2147483648)
				{
					this.LastAngle = (double)E.GetParameter("Angle");
					this.LastMod = E.GetIntParameter("Mod", 0);
				}
				else
				{
					E.SetParameter("Angle", this.LastAngle);
					E.SetParameter("Mod", this.LastMod + 180);
					this.LastAngle = 0.0;
					this.LastMod = int.MinValue;
				}
			}
			else if (E.ID == "ProjectileSetup")
			{
				if (this.Switch == this.Flipped)
				{
					foreach (IPart part in E.GetGameObjectParameter("Projectile").PartsList)
					{
						this.ProjectileBlueprint.ReinitializePart(part);
					}
				}
				this.Switch = !this.Switch;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004402 RID: 17410
		public bool Flipped;

		// Token: 0x04004403 RID: 17411
		public string HeatName = "R";

		// Token: 0x04004404 RID: 17412
		public string ColdName = "C";

		// Token: 0x04004405 RID: 17413
		public string HeatColorString = "&R";

		// Token: 0x04004406 RID: 17414
		public string ColdColorString = "&C";

		// Token: 0x04004407 RID: 17415
		public string HeatDetailColor = "C";

		// Token: 0x04004408 RID: 17416
		public string ColdDetailColor = "R";

		// Token: 0x04004409 RID: 17417
		public string ProjectileObject = "ProjectileDiThermoBeamCold";

		// Token: 0x0400440A RID: 17418
		[NonSerialized]
		public bool Switch;

		// Token: 0x0400440B RID: 17419
		[NonSerialized]
		public double LastAngle;

		// Token: 0x0400440C RID: 17420
		[NonSerialized]
		public int LastMod = int.MinValue;

		// Token: 0x0400440D RID: 17421
		[NonSerialized]
		public GameObjectBlueprint ProjectileBlueprint;
	}
}
