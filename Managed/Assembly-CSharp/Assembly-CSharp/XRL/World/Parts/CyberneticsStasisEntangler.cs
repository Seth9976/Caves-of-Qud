using System;
using ConsoleLib.Console;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F48 RID: 3912
	[Serializable]
	public class CyberneticsStasisEntangler : IPoweredPart
	{
		// Token: 0x06009C1D RID: 39965 RVA: 0x00389BC8 File Offset: 0x00387DC8
		public CyberneticsStasisEntangler()
		{
			this.ChargeUse = 0;
			this.WorksOnImplantee = true;
			this.NameForStatus = "StasisEntangler";
		}

		// Token: 0x06009C1E RID: 39966 RVA: 0x00389C20 File Offset: 0x00387E20
		public void CollectStats(Templates.StatCollector stats)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			GameObject parentObject = this.ParentObject;
			stats.CollectCooldownTurns(base.MyActivatedAbility(activatedAbilityID, (parentObject != null) ? parentObject.Implantee : null), this.GetCooldown(), 0);
			stats.Set("Duration", this.GetDuration(), !stats.mode.Contains("ability"), 0);
		}

		// Token: 0x06009C1F RID: 39967 RVA: 0x00389C7D File Offset: 0x00387E7D
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009C20 RID: 39968 RVA: 0x00389C80 File Offset: 0x00387E80
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009C21 RID: 39969 RVA: 0x00389CB6 File Offset: 0x00387EB6
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			Guid activatedAbilityID = this.ActivatedAbilityID;
			Action<Templates.StatCollector> action = new Action<Templates.StatCollector>(this.CollectStats);
			GameObject parentObject = this.ParentObject;
			base.DescribeMyActivatedAbility(activatedAbilityID, action, (parentObject != null) ? parentObject.Implantee : null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C22 RID: 39970 RVA: 0x00389CEC File Offset: 0x00387EEC
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (this.CommandID == null)
			{
				this.CommandID = Guid.NewGuid().ToString();
			}
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Stasis Entangler", this.CommandID, "Cybernetics", null, "é", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, "CommandStasisEntangler", null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C23 RID: 39971 RVA: 0x00389D5F File Offset: 0x00387F5F
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C24 RID: 39972 RVA: 0x00389D7A File Offset: 0x00387F7A
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == this.CommandID) || this.ActivateStasisEntangler(E.Actor, E.Target, E)) && base.HandleEvent(E);
		}

		// Token: 0x06009C25 RID: 39973 RVA: 0x00389DAD File Offset: 0x00387FAD
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			E.Description = this.GetBehaviorDescription();
			return base.HandleEvent(E);
		}

		// Token: 0x06009C26 RID: 39974 RVA: 0x00389DC2 File Offset: 0x00387FC2
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009C27 RID: 39975 RVA: 0x00389DC8 File Offset: 0x00387FC8
		public bool ActivateStasisEntangler(GameObject Actor, GameObject Target = null, IEvent FromEvent = null)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return false;
			}
			if (!this.IsObjectActivePartSubject(Actor))
			{
				return false;
			}
			if (Actor.OnWorldMap())
			{
				return Actor.Fail("You cannot do that on the world map.");
			}
			Zone currentZone = Actor.CurrentZone;
			if (currentZone == null)
			{
				return false;
			}
			if (!Actor.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				return false;
			}
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + ".");
			}
			Cell cell = null;
			if (Target == null)
			{
				cell = Actor.Physics.PickDestinationCell(9999, AllowVis.OnlyVisible, true, false, false, true, PickTarget.PickStyle.EmptyCell, "Stasis Entangler", false, null);
				if (cell == null)
				{
					return false;
				}
				Target = cell.GetCombatTarget(Actor, true, true, false, 5, null, null, null, Actor, null, false, false, null);
				if (Target == null)
				{
					return Actor.Fail("There is no one there for you to entangle.");
				}
			}
			if (cell == null)
			{
				cell = Target.CurrentCell;
			}
			int @for = GetAvailableComputePowerEvent.GetFor(this);
			int realityStabilizationPenetration = this.GetRealityStabilizationPenetration(@for);
			Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
			@event.SetParameter("Object", Actor);
			@event.SetParameter("Cell", cell);
			@event.SetParameter("Device", this.ParentObject);
			@event.SetParameter("RealityStabilizationPenetration", realityStabilizationPenetration);
			if (!Actor.FireEvent(@event, FromEvent) || !cell.FireEvent(@event, FromEvent))
			{
				return false;
			}
			Actor.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(@for), null, false);
			Actor.UseEnergy(1000, "Cybernetics StasisEntangler", null, null, false);
			this.DeployToCells(currentZone, Actor, Target, @for, realityStabilizationPenetration);
			if (FromEvent != null)
			{
				FromEvent.RequestInterfaceExit();
			}
			return true;
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x00389FA8 File Offset: 0x003881A8
		private GameObject DeployToCells(Zone Zone, GameObject Actor, GameObject Target, int ComputePower, int RealityStabilizationPenetration)
		{
			GameObject gameObject = null;
			int num = 0;
			int num2 = 0;
			Phased phased;
			Omniphase omniphase;
			Phase.carryOverPrep(Actor, out phased, out omniphase);
			TextConsole textConsole = Look._TextConsole;
			The.Core.RenderBase(true, false);
			for (int i = 0; i < Zone.Height; i++)
			{
				for (int j = 0; j < Zone.Width; j++)
				{
					Cell cell = Zone.GetCell(j, i);
					bool flag = false;
					bool flag2 = false;
					int k = 0;
					int count = cell.Objects.Count;
					while (k < count)
					{
						GameObject gameObject2 = cell.Objects[k];
						if (gameObject2 == Actor || gameObject2 == Target)
						{
							flag2 = true;
							break;
						}
						if (gameObject2.IsCreature)
						{
							flag = true;
						}
						k++;
					}
					if (flag && !flag2 && IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cell, Actor, this.ParentObject, null, RealityStabilizationPenetration))
					{
						GameObject gameObject3 = GameObject.Create("Stasisfield");
						gameObject3.GetPart<Forcefield>().Creator = Actor;
						gameObject3.AddPart<Temporary>(new Temporary(this.GetDuration(ComputePower) + 1), true, false);
						Phase.carryOver(Actor, gameObject3, phased, omniphase);
						cell.AddObject(gameObject3, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObject = gameObject3;
						num++;
						if (cell.IsVisible())
						{
							num2++;
							The.Core.RenderBase(true, false);
						}
					}
				}
			}
			if (num2 == 1)
			{
				IComponent<GameObject>.XDidY(gameObject, "appear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
			}
			else if (num2 >= 6)
			{
				IComponent<GameObject>.AddPlayerMessage(gameObject.ShortDisplayName.Pluralize().Capitalize() + " appear all around.", null, true);
			}
			else if (num2 >= 0)
			{
				IComponent<GameObject>.AddPlayerMessage("Several " + gameObject.ShortDisplayName.Pluralize() + " appear nearby.", null, true);
			}
			return gameObject;
		}

		// Token: 0x06009C29 RID: 39977 RVA: 0x0038A170 File Offset: 0x00388370
		public int GetCooldown(int ComputePower)
		{
			int num = this.BaseCooldown;
			if (ComputePower != 0)
			{
				num = Math.Max(num * (100 - ComputePower / 2) / 100, num / 2);
			}
			return num;
		}

		// Token: 0x06009C2A RID: 39978 RVA: 0x0038A19C File Offset: 0x0038839C
		public int GetCooldown()
		{
			return this.GetCooldown(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C2B RID: 39979 RVA: 0x0038A1AA File Offset: 0x003883AA
		public int GetDuration(int ComputePower)
		{
			return this.BaseDuration.RollCached();
		}

		// Token: 0x06009C2C RID: 39980 RVA: 0x0038A1B7 File Offset: 0x003883B7
		public int GetDuration()
		{
			return this.GetDuration(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C2D RID: 39981 RVA: 0x0038A1C5 File Offset: 0x003883C5
		public int GetMinDuration(int ComputePower)
		{
			return this.BaseDuration.RollMinCached();
		}

		// Token: 0x06009C2E RID: 39982 RVA: 0x0038A1D2 File Offset: 0x003883D2
		public int GetMinDuration()
		{
			return this.GetMinDuration(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C2F RID: 39983 RVA: 0x0038A1E0 File Offset: 0x003883E0
		public int GetMaxDuration(int ComputePower)
		{
			return this.BaseDuration.RollMaxCached();
		}

		// Token: 0x06009C30 RID: 39984 RVA: 0x0038A1ED File Offset: 0x003883ED
		public int GetMaxDuration()
		{
			return this.GetMaxDuration(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C31 RID: 39985 RVA: 0x0038A1FB File Offset: 0x003883FB
		public int GetRealityStabilizationPenetration(int ComputePower)
		{
			return this.BaseRealityStabilizationPenetration;
		}

		// Token: 0x06009C32 RID: 39986 RVA: 0x0038A203 File Offset: 0x00388403
		public int GetRealityStabilizationPenetration()
		{
			return this.GetRealityStabilizationPenetration(GetAvailableComputePowerEvent.GetFor(this));
		}

		// Token: 0x06009C33 RID: 39987 RVA: 0x0038A214 File Offset: 0x00388414
		public string GetBehaviorDescription()
		{
			int @for = GetAvailableComputePowerEvent.GetFor(this);
			int cooldown = this.GetCooldown(@for);
			int minDuration = this.GetMinDuration(@for);
			int maxDuration = this.GetMaxDuration(@for);
			return string.Concat(new string[]
			{
				"Activated. Cooldown ",
				cooldown.ToString(),
				".\nChoose one creature in sight. All creatures in the zone other than you and it, excepting creatures in the same square as one of you, are put in stasis for ",
				(minDuration == maxDuration) ? minDuration.Things("turn", null) : (minDuration.ToString() + "-" + maxDuration.ToString() + " turns"),
				"."
			});
		}

		// Token: 0x040040A2 RID: 16546
		public int BaseCooldown = 200;

		// Token: 0x040040A3 RID: 16547
		public int BaseRealityStabilizationPenetration = 30;

		// Token: 0x040040A4 RID: 16548
		public string BaseDuration = "15";

		// Token: 0x040040A5 RID: 16549
		public string CommandID;

		// Token: 0x040040A6 RID: 16550
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
