using System;
using XRL.Messages;
using XRL.Rules;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.Capabilities
{
	// Token: 0x020009EA RID: 2538
	public static class Flight
	{
		// Token: 0x06006C3B RID: 27707 RVA: 0x00230380 File Offset: 0x0022E580
		public static bool IsFlying(GameObject obj)
		{
			return obj != null && obj.IsFlying;
		}

		// Token: 0x06006C3C RID: 27708 RVA: 0x00230390 File Offset: 0x0022E590
		private static void CollectBestFlyingLevel(Effect RFX)
		{
			Flying flying = RFX as Flying;
			if (flying != null && flying.Level > Flight.BestFlyingLevel)
			{
				Flight.BestFlyingLevel = flying.Level;
			}
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x002303BF File Offset: 0x0022E5BF
		public static int GetBestFlyingLevel(GameObject obj)
		{
			Flight.BestFlyingLevel = 0;
			obj.ForeachEffect(new Action<Effect>(Flight.CollectBestFlyingLevel));
			return Flight.BestFlyingLevel;
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x002303DE File Offset: 0x0022E5DE
		public static void SuspendFlight(GameObject obj)
		{
			obj.ModIntProperty("SuspendFlight", 1, false);
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x002303EE File Offset: 0x0022E5EE
		public static void DesuspendFlight(GameObject obj)
		{
			obj.ModIntProperty("SuspendFlight", -1, true);
		}

		// Token: 0x06006C40 RID: 27712 RVA: 0x002303FE File Offset: 0x0022E5FE
		public static bool EnvironmentAllowsFlight(Zone Z)
		{
			return Z != null && (Z.IsWorldMap() || Z.GetZoneZ() <= 10);
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x0023041C File Offset: 0x0022E61C
		public static bool EnvironmentAllowsFlight(Cell C)
		{
			return C != null && (C.HasObjectWithTag("FlyingWhitelistArea") || Flight.EnvironmentAllowsFlight(C.ParentZone));
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x0023043D File Offset: 0x0022E63D
		public static bool EnvironmentAllowsFlight(GameObject GO)
		{
			return Flight.EnvironmentAllowsFlight((GO != null) ? GO.CurrentCell : null);
		}

		// Token: 0x06006C43 RID: 27715 RVA: 0x00230450 File Offset: 0x0022E650
		private static IFlightSource FlightSource(GameObject Source)
		{
			if (Source == null)
			{
				return null;
			}
			return Source.GetFirstFlightSourcePart();
		}

		// Token: 0x06006C44 RID: 27716 RVA: 0x0023045D File Offset: 0x0022E65D
		private static IFlightSource FlightSource(GameObject Source, Predicate<IFlightSource> Filter)
		{
			if (Source == null)
			{
				return null;
			}
			return Source.GetFirstFlightSourcePart(Filter);
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x0023046B File Offset: 0x0022E66B
		private static bool HasActivatedAbilityID(IFlightSource FS)
		{
			return FS.FlightActivatedAbilityID != Guid.Empty;
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x0023047D File Offset: 0x0022E67D
		public static bool IsAbilityUsable(IFlightSource FS, GameObject Object)
		{
			return FS != null && Object.IsActivatedAbilityUsable(FS.FlightActivatedAbilityID);
		}

		// Token: 0x06006C47 RID: 27719 RVA: 0x00230490 File Offset: 0x0022E690
		public static bool IsAbilityUsable(GameObject Source, GameObject Object)
		{
			return Flight.IsAbilityUsable(Flight.FlightSource(Source, new Predicate<IFlightSource>(Flight.HasActivatedAbilityID)), Object);
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x002304AA File Offset: 0x0022E6AA
		public static bool IsAbilityAIUsable(IFlightSource FS, GameObject Object)
		{
			return FS != null && Object.IsActivatedAbilityAIUsable(FS.FlightActivatedAbilityID);
		}

		// Token: 0x06006C49 RID: 27721 RVA: 0x002304BD File Offset: 0x0022E6BD
		public static bool IsAbilityAIUsable(GameObject Source, GameObject Object)
		{
			return Flight.IsAbilityAIUsable(Flight.FlightSource(Source, new Predicate<IFlightSource>(Flight.HasActivatedAbilityID)), Object);
		}

		// Token: 0x06006C4A RID: 27722 RVA: 0x002304D8 File Offset: 0x0022E6D8
		public static bool AbilitySetup(GameObject Source, GameObject Object, IFlightSource FS = null)
		{
			if (FS == null)
			{
				FS = Flight.FlightSource(Source);
				if (FS == null)
				{
					return false;
				}
			}
			string text = "Fly";
			if (FS.FlightSourceDescription != null)
			{
				text = text + " (" + FS.FlightSourceDescription + ")";
			}
			FS.FlightActivatedAbilityID = Object.AddActivatedAbility(text, FS.FlightEvent, FS.FlightActivatedAbilityClass, null, "\u009d", null, true, FS.FlightFlying, true, false, false, false, false, false, true, true, false, false, -1, "CommandFlyToggle", null, null, null, null);
			return true;
		}

		// Token: 0x06006C4B RID: 27723 RVA: 0x00230558 File Offset: 0x0022E758
		public static bool AbilityTeardown(GameObject Source, GameObject Object, IFlightSource FS = null)
		{
			IFlightSource flightSource = FS ?? Flight.FlightSource(Source, new Predicate<IFlightSource>(Flight.HasActivatedAbilityID));
			if (flightSource != null)
			{
				Guid flightActivatedAbilityID = flightSource.FlightActivatedAbilityID;
				Object.RemoveActivatedAbility(ref flightActivatedAbilityID);
				flightSource.FlightActivatedAbilityID = flightActivatedAbilityID;
			}
			return true;
		}

		// Token: 0x06006C4C RID: 27724 RVA: 0x00230598 File Offset: 0x0022E798
		public static int GetMoveFallChance(GameObject Object, IFlightSource FS)
		{
			int num = ((FS == null) ? int.MaxValue : (FS.FlightBaseFallChance - FS.FlightLevel));
			foreach (Flying flying in Object.YieldEffects<Flying>())
			{
				Flying flying2 = flying as Flying;
				if (flying2 != null && flying2.Source != null)
				{
					IFlightSource flightSource = Flight.FlightSource(flying2.Source);
					int num2 = flightSource.FlightBaseFallChance - flightSource.FlightLevel;
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return Math.Max(0, (num == int.MaxValue) ? 0 : num);
		}

		// Token: 0x06006C4D RID: 27725 RVA: 0x0023063C File Offset: 0x0022E83C
		public static int GetMoveFallChance(GameObject Object, GameObject Source = null)
		{
			return Flight.GetMoveFallChance(Object, (Source == null) ? null : Flight.FlightSource(Source));
		}

		// Token: 0x06006C4E RID: 27726 RVA: 0x00230650 File Offset: 0x0022E850
		public static int GetSwoopFallChance(GameObject Object, IFlightSource FS)
		{
			int num = ((FS == null) ? int.MaxValue : (FS.FlightBaseFallChance * 4 - FS.FlightLevel));
			foreach (Flying flying in Object.YieldEffects<Flying>())
			{
				Flying flying2 = flying as Flying;
				if (flying2 != null && flying2.Source != null)
				{
					IFlightSource flightSource = Flight.FlightSource(flying2.Source);
					int num2 = flightSource.FlightBaseFallChance * 4 - flightSource.FlightLevel;
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return Math.Max(0, (num == int.MaxValue) ? 0 : num);
		}

		// Token: 0x06006C4F RID: 27727 RVA: 0x002306F8 File Offset: 0x0022E8F8
		public static int GetSwoopFallChance(GameObject Object, GameObject Source = null)
		{
			return Flight.GetSwoopFallChance(Object, (Source == null) ? null : Flight.FlightSource(Source));
		}

		// Token: 0x06006C50 RID: 27728 RVA: 0x0023070C File Offset: 0x0022E90C
		public static bool MaintainFlight(GameObject Source, GameObject Object, IFlightSource FS = null)
		{
			if (FS == null)
			{
				FS = Flight.FlightSource(Source);
			}
			if (FS == null)
			{
				return Flight.FailFlying(Source, Object, null);
			}
			if (FS.FlightFlying)
			{
				if (FS.FlightRequiresOngoingEffort)
				{
					if (!Object.CanMoveExtremities("Fly", false, false, false))
					{
						return Flight.FailFlying(Source, Object, null);
					}
					PowerSwitch part = Object.GetPart<PowerSwitch>();
					if (part != null && !part.Active)
					{
						return Flight.FailFlying(Source, Object, null);
					}
				}
				if (!Object.OnWorldMap())
				{
					int num = FS.FlightBaseFallChance - FS.FlightLevel;
					if (num > 0 && Stat.Random(1, 200) <= num)
					{
						return Flight.FailFlying(Source, Object, FS);
					}
				}
			}
			return true;
		}

		// Token: 0x06006C51 RID: 27729 RVA: 0x002307A8 File Offset: 0x0022E9A8
		public static bool CheckFlight(GameObject Source, GameObject Object, IFlightSource FS = null)
		{
			if (FS == null)
			{
				FS = Flight.FlightSource(Source);
				if (FS == null)
				{
					return Flight.StopFlying(Source, Object, null, false, false);
				}
			}
			return !FS.FlightFlying || Object.OnWorldMap() || Flight.EnvironmentAllowsFlight(Object) || Object.CurrentCell.HasObjectWithPart("StairsDown") || Flight.StopFlying(Source, Object, FS, false, false);
		}

		// Token: 0x06006C52 RID: 27730 RVA: 0x00230804 File Offset: 0x0022EA04
		public static Flying GetFlyingEffectFromSource(GameObject Source, GameObject Object)
		{
			if (GameObject.Validate(ref Source) && GameObject.Validate(ref Object))
			{
				foreach (Flying flying in Object.YieldEffects<Flying>())
				{
					Flying flying2 = flying as Flying;
					if (flying2 != null && flying2.Source == Source)
					{
						return flying2;
					}
				}
			}
			return null;
		}

		// Token: 0x06006C53 RID: 27731 RVA: 0x00230874 File Offset: 0x0022EA74
		public static bool StartFlying(GameObject Source, GameObject Object, IFlightSource FS = null)
		{
			if (FS == null)
			{
				FS = Flight.FlightSource(Source);
				if (FS == null)
				{
					return false;
				}
			}
			if (FS.FlightFlying)
			{
				return false;
			}
			if (!Flight.EnvironmentAllowsFlight(Object))
			{
				return Object.Fail("You can't fly underground!");
			}
			if (!Object.CanChangeMovementMode("Flying", true, false, false, false))
			{
				return false;
			}
			Object.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_wings_fly_move", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (Object.GetEffectCount(typeof(Flying)) == 0)
			{
				if (Object.IsPlayer())
				{
					MessageQueue.AddPlayerMessage("You begin flying!", null, true);
				}
				else if (Object.IsVisible())
				{
					MessageQueue.AddPlayerMessage(Object.Does("begin", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " flying.", null, true);
				}
				Object.AddActivatedAbility("Swoop", Flight.SWOOP_ATTACK_COMMAND_NAME, "Maneuvers", null, "û", null, false, false, false, false, false, false, The.Game.HasBooleanGameState(Flight.SWOOP_ATTACK_COMMAND_NAME), false, true, true, false, false, -1, null, null, null, null, null);
				The.Game.SetBooleanGameState(Flight.SWOOP_ATTACK_COMMAND_NAME, true);
				Object.MovementModeChanged("Flying", false);
			}
			else if (Object.IsPlayer())
			{
				MessageQueue.AddPlayerMessage("You begin using an additional flight capability.", null, true);
			}
			FS.FlightFlying = true;
			Object.ApplyEffect(new Flying(FS.FlightLevel, Source), null);
			Object.RemoveEffect<Prone>();
			Object.ToggleActivatedAbility(FS.FlightActivatedAbilityID, false, null);
			Object.FireEvent("FlightStarted");
			ObjectStartedFlyingEvent.SendFor(Object, null);
			return true;
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x00230A19 File Offset: 0x0022EC19
		private static void NoLongerFlying(GameObject Object)
		{
			ActivatedAbilities abilities = Object.Abilities;
			if (abilities == null)
			{
				return;
			}
			abilities.RemoveAbilityByCommand(Flight.SWOOP_ATTACK_COMMAND_NAME);
		}

		// Token: 0x06006C55 RID: 27733 RVA: 0x00230A34 File Offset: 0x0022EC34
		public static bool StopFlying(GameObject Source, GameObject Object, IFlightSource FS = null, bool Silent = false, bool FromFail = false)
		{
			if (FS == null)
			{
				FS = Flight.FlightSource(Source);
				if (FS == null)
				{
					return false;
				}
			}
			if (!FS.FlightFlying)
			{
				return false;
			}
			if (Object == null)
			{
				FS.FlightFlying = false;
				return false;
			}
			int effectCount = Object.GetEffectCount(typeof(Flying));
			Object.GetCurrentCell();
			if (!Silent)
			{
				if (effectCount <= 1)
				{
					if (Object.IsPlayer())
					{
						MessageQueue.AddPlayerMessage("You return to the ground.", null, true);
					}
					else if (Object.IsVisible())
					{
						MessageQueue.AddPlayerMessage(Object.Does("return", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to the ground.", null, true);
					}
				}
				else if (Object.IsPlayer())
				{
					MessageQueue.AddPlayerMessage("You cease using one of your flight capabilities.", null, true);
				}
			}
			FS.FlightFlying = false;
			if (!Object.RemoveEffect(typeof(Flying), (Effect FX) => (FX as Flying).Source == Source))
			{
				Object.RemoveEffect<Flying>();
			}
			Object.ToggleActivatedAbility(FS.FlightActivatedAbilityID, false, null);
			Object.FireEvent("FlightStoppedFromOneSource");
			if (effectCount <= 1)
			{
				Object.FireEvent("FlightStopped");
				if (!FromFail)
				{
					Flight.NoLongerFlying(Object);
					Object.MovementModeChanged("NotFlying", false);
				}
				ObjectStoppedFlyingEvent.SendFor(Object, null);
			}
			Object.Gravitate();
			return true;
		}

		// Token: 0x06006C56 RID: 27734 RVA: 0x00230B8D File Offset: 0x0022ED8D
		private static bool HasSource(Flying FX)
		{
			return FX.Source != null;
		}

		// Token: 0x06006C57 RID: 27735 RVA: 0x00230B98 File Offset: 0x0022ED98
		public static void Land(GameObject Object, bool Silent = false)
		{
			if (!Object.HasEffect<Flying>(new Predicate<Flying>(Flight.HasSource)))
			{
				return;
			}
			if (!Silent)
			{
				if (Object.IsPlayer())
				{
					MessageQueue.AddPlayerMessage("You return to the ground.", null, true);
				}
				else if (Object.IsVisible())
				{
					MessageQueue.AddPlayerMessage(Object.Does("return", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to the ground.", null, true);
				}
			}
			foreach (Flying flying in Object.YieldEffects<Flying>(new Predicate<Flying>(Flight.HasSource)))
			{
				Flight.StopFlying(flying.Source, Object, null, true, false);
			}
		}

		// Token: 0x06006C58 RID: 27736 RVA: 0x00230C6C File Offset: 0x0022EE6C
		public static bool FailFlying(GameObject Source, GameObject Object, IFlightSource FS = null)
		{
			if (FS == null)
			{
				FS = Flight.FlightSource(Source);
				if (FS == null)
				{
					return false;
				}
			}
			if (!FS.FlightFlying)
			{
				return false;
			}
			if (Object == null)
			{
				FS.FlightFlying = false;
				return false;
			}
			Object.StopMoving();
			int effectCount = Object.GetEffectCount(typeof(Flying));
			if (effectCount <= 1)
			{
				Object.PlayWorldSound("fly_generic_fall", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (Object.IsPlayer())
				{
					MessageQueue.AddPlayerMessage("You fall to the ground!", 'R', true);
				}
				else if (Object.IsVisible())
				{
					MessageQueue.AddPlayerMessage(Object.Does("fall", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to the ground.", null, true);
				}
			}
			else if (Object.IsPlayer())
			{
				MessageQueue.AddPlayerMessage("One of your flight capabilities fails.", 'R', true);
			}
			bool flag = Flight.StopFlying(Source, Object, FS, true, true);
			Object.CooldownActivatedAbility(FS.FlightActivatedAbilityID, 5, null, true);
			Object.FireEvent("FlightFailedFromOneSource");
			if (effectCount <= 1)
			{
				Object.FireEvent("FlightFailed");
				Flight.NoLongerFlying(Object);
				Object.MovementModeChanged("NotFlying", true);
				ObjectStoppedFlyingEvent.SendFor(Object, null);
				Object.ApplyEffect(new Prone(), null);
			}
			return flag;
		}

		// Token: 0x06006C59 RID: 27737 RVA: 0x00230DB8 File Offset: 0x0022EFB8
		public static void Fall(GameObject Object)
		{
			foreach (Flying flying in Object.YieldEffects<Flying>())
			{
				Flying flying2 = flying as Flying;
				if (((flying2 != null) ? flying2.Source : null) != null)
				{
					Flight.FailFlying(flying2.Source, Object, null);
				}
			}
		}

		// Token: 0x06006C5A RID: 27738 RVA: 0x00230E20 File Offset: 0x0022F020
		public static void SyncFlying(GameObject Source, GameObject Object, IFlightSource FS = null)
		{
			if (Source == null || Object == null)
			{
				return;
			}
			if (FS == null)
			{
				FS = Flight.FlightSource(Source);
				if (FS == null)
				{
					return;
				}
			}
			bool flag = false;
			if (FS.FlightFlying)
			{
				flag = true;
				bool flag2 = false;
				foreach (Flying flying in Object.YieldEffects<Flying>())
				{
					Flying flying2 = flying as Flying;
					if (((flying2 != null) ? flying2.Source : null) == Source)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					FS.FlightFlying = false;
					if (Object.IsActivatedAbilityToggledOn(FS.FlightActivatedAbilityID))
					{
						Object.ToggleActivatedAbility(FS.FlightActivatedAbilityID, false, null);
					}
				}
			}
			else
			{
				foreach (Flying flying3 in Object.YieldEffects<Flying>())
				{
					flag = true;
					Flying flying4 = flying3 as Flying;
					if (((flying4 != null) ? flying4.Source : null) == Source)
					{
						Object.RemoveEffect(flying4, true);
					}
				}
			}
			int effectCount = Object.GetEffectCount(typeof(Flying));
			if (flag && effectCount <= 0)
			{
				Flight.NoLongerFlying(Object);
			}
		}

		// Token: 0x0400319A RID: 12698
		public static readonly string SWOOP_ATTACK_COMMAND_NAME = "CommandSwoopAttack";

		// Token: 0x0400319B RID: 12699
		[NonSerialized]
		private static int BestFlyingLevel;
	}
}
