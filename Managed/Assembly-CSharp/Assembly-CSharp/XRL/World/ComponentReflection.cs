using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Genkit;
using XRL.Collections;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
using XRL.World.Quests;
using XRL.World.Text;

namespace XRL.World
{
	// Token: 0x020006B1 RID: 1713
	public static class ComponentReflection
	{
		// Token: 0x060048D4 RID: 18644 RVA: 0x0016D714 File Offset: 0x0016B914
		public static void Initialize()
		{
			if (ModManager.HasActiveScripts())
			{
				Rack<int[]> rack;
				ulong num;
				ComponentReflection.Evaluate(ModManager.ActiveTypes, out rack, out num);
				return;
			}
			ComponentReflection.Thaw();
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x0016D73C File Offset: 0x0016B93C
		private static void Thaw()
		{
			ComponentReflection.PartEvents.EnsureCapacity(289);
			ComponentReflection.EffectEvents.EnsureCapacity(100);
			int[] array = new int[] { 737650316, -912057913 };
			int[] array2 = new int[] { -912057913, -1345588527 };
			int[] array3 = new int[] { 737650316 };
			int[] array4 = new int[] { -912057913 };
			int[] array5 = new int[] { 737650316, 1583436105 };
			int[] array6 = new int[] { 737650316, -1345588527 };
			int[] array7 = new int[] { -1345588527 };
			int[] array8 = new int[] { 1583436105 };
			ComponentReflection.PartEvents[typeof(IPartWithPrefabImposter)] = array;
			ComponentReflection.PartEvents[typeof(AscensionRenderer)] = array2;
			ComponentReflection.PartEvents[typeof(GraftekGraft)] = array3;
			ComponentReflection.PartEvents[typeof(Brain)] = array3;
			ComponentReflection.PartEvents[typeof(LiquidVolume)] = array6;
			ComponentReflection.PartEvents[typeof(Physics)] = array7;
			ComponentReflection.PartEvents[typeof(TerrainNotes)] = array;
			ComponentReflection.PartEvents[typeof(BaetylHostility)] = array7;
			ComponentReflection.PartEvents[typeof(RandomAltarBaetyl)] = array3;
			ComponentReflection.PartEvents[typeof(Body)] = array7;
			ComponentReflection.PartEvents[typeof(Tattoos)] = array3;
			ComponentReflection.PartEvents[typeof(AIPassenger)] = array7;
			ComponentReflection.PartEvents[typeof(AIUrnDuster)] = array7;
			ComponentReflection.PartEvents[typeof(AIWorldMapTravel)] = array7;
			ComponentReflection.PartEvents[typeof(Distraction)] = array7;
			ComponentReflection.PartEvents[typeof(Breeder)] = array7;
			ComponentReflection.PartEvents[typeof(ConfuseOnSight)] = array7;
			ComponentReflection.PartEvents[typeof(CrossFlameOnStep)] = array7;
			ComponentReflection.PartEvents[typeof(CrypticParticleText)] = array7;
			ComponentReflection.PartEvents[typeof(LavaSludge)] = array7;
			ComponentReflection.PartEvents[typeof(MamonPart)] = array3;
			ComponentReflection.PartEvents[typeof(Slumberling)] = array7;
			ComponentReflection.PartEvents[typeof(SoupSludge)] = array3;
			ComponentReflection.PartEvents[typeof(Spawner)] = array7;
			ComponentReflection.PartEvents[typeof(SpawnVessel)] = array7;
			ComponentReflection.PartEvents[typeof(TrollKing)] = array7;
			ComponentReflection.PartEvents[typeof(DischargeOnStep)] = array7;
			ComponentReflection.PartEvents[typeof(DromadCaravan)] = array3;
			ComponentReflection.PartEvents[typeof(ElementalJelly)] = array3;
			ComponentReflection.PartEvents[typeof(Engulfing)] = array3;
			ComponentReflection.PartEvents[typeof(ExtradimensionalHunterSummoner)] = array7;
			ComponentReflection.PartEvents[typeof(FabricateFromSelf)] = array7;
			ComponentReflection.PartEvents[typeof(FallsApart)] = array7;
			ComponentReflection.PartEvents[typeof(FireSuppressionSystem)] = array7;
			ComponentReflection.PartEvents[typeof(FugueOnStep)] = array7;
			ComponentReflection.PartEvents[typeof(GenericInventoryRestocker)] = array7;
			ComponentReflection.PartEvents[typeof(LiquidProximityFrenzies)] = array7;
			ComponentReflection.PartEvents[typeof(LiquidProximityTriggersEffect)] = array7;
			ComponentReflection.PartEvents[typeof(Mimic)] = array3;
			ComponentReflection.PartEvents[typeof(NephalVFX)] = array4;
			ComponentReflection.PartEvents[typeof(Nest)] = array7;
			ComponentReflection.PartEvents[typeof(Pounder)] = array7;
			ComponentReflection.PartEvents[typeof(RunOver)] = array3;
			ComponentReflection.PartEvents[typeof(SetStateOnSeen)] = array3;
			ComponentReflection.PartEvents[typeof(SlowDangerousMovement)] = array4;
			ComponentReflection.PartEvents[typeof(TemperatureVenting)] = array7;
			ComponentReflection.PartEvents[typeof(WallColor)] = array3;
			ComponentReflection.PartEvents[typeof(CyberneticsAnomalyFumigator)] = array7;
			ComponentReflection.PartEvents[typeof(CyberneticsAutomatedInternalDefibrillator)] = array7;
			ComponentReflection.PartEvents[typeof(CyberneticsElectromagneticSensor)] = array7;
			ComponentReflection.PartEvents[typeof(CyberneticsFireSuppressionSystem)] = array7;
			ComponentReflection.PartEvents[typeof(CyberneticsHasRandomImplants)] = array3;
			ComponentReflection.PartEvents[typeof(CyberneticsMedassistModule)] = array7;
			ComponentReflection.PartEvents[typeof(CyberneticsPenetratingRadar)] = array7;
			ComponentReflection.PartEvents[typeof(PaintTest)] = array3;
			ComponentReflection.PartEvents[typeof(MessageWhenFirstSeen)] = array3;
			ComponentReflection.PartEvents[typeof(SkrefCorpseLoot)] = array3;
			ComponentReflection.PartEvents[typeof(Interior)] = array7;
			ComponentReflection.PartEvents[typeof(InteriorPortal)] = array7;
			ComponentReflection.PartEvents[typeof(XRL.World.Parts.MotedAir)] = array3;
			ComponentReflection.PartEvents[typeof(ItemConvertor)] = array7;
			ComponentReflection.PartEvents[typeof(LiquidPump)] = array7;
			ComponentReflection.PartEvents[typeof(RepairBay)] = array7;
			ComponentReflection.PartEvents[typeof(TeleportGate)] = array7;
			ComponentReflection.PartEvents[typeof(Gas)] = array6;
			ComponentReflection.PartEvents[typeof(GasAsh)] = array7;
			ComponentReflection.PartEvents[typeof(GasConfusion)] = array7;
			ComponentReflection.PartEvents[typeof(GasDamaging)] = array7;
			ComponentReflection.PartEvents[typeof(GasDisease)] = array7;
			ComponentReflection.PartEvents[typeof(GasFungalSpores)] = array7;
			ComponentReflection.PartEvents[typeof(GasPlasma)] = array7;
			ComponentReflection.PartEvents[typeof(GasPoison)] = array7;
			ComponentReflection.PartEvents[typeof(GasShame)] = array7;
			ComponentReflection.PartEvents[typeof(GasSleep)] = array7;
			ComponentReflection.PartEvents[typeof(HindrenMysteryExile)] = array7;
			ComponentReflection.PartEvents[typeof(Inventory)] = array7;
			ComponentReflection.PartEvents[typeof(ActiveLightSource)] = array7;
			ComponentReflection.PartEvents[typeof(ActiveStatPercent)] = array7;
			ComponentReflection.PartEvents[typeof(AddsRep)] = array7;
			ComponentReflection.PartEvents[typeof(AdjustSpecialEffectChances)] = array7;
			ComponentReflection.PartEvents[typeof(AilingQuickness)] = array7;
			ComponentReflection.PartEvents[typeof(ArtifactDetection)] = array7;
			ComponentReflection.PartEvents[typeof(AttributeMultiplier)] = array7;
			ComponentReflection.PartEvents[typeof(BootSequence)] = array7;
			ComponentReflection.PartEvents[typeof(ComputeNode)] = array7;
			ComponentReflection.PartEvents[typeof(Cursed)] = array7;
			ComponentReflection.PartEvents[typeof(DamageContents)] = array7;
			ComponentReflection.PartEvents[typeof(DismemberAdjacentHostiles)] = array7;
			ComponentReflection.PartEvents[typeof(Displacer)] = array7;
			ComponentReflection.PartEvents[typeof(EquipIntProperties)] = array7;
			ComponentReflection.PartEvents[typeof(EquipStatBoost)] = array7;
			ComponentReflection.PartEvents[typeof(FlareCompensation)] = array7;
			ComponentReflection.PartEvents[typeof(FoliageCamouflage)] = array7;
			ComponentReflection.PartEvents[typeof(FungalFortitude)] = array7;
			ComponentReflection.PartEvents[typeof(GasTumbler)] = array7;
			ComponentReflection.PartEvents[typeof(Gaslight)] = array7;
			ComponentReflection.PartEvents[typeof(GlimmerAlteration)] = array7;
			ComponentReflection.PartEvents[typeof(Hangable)] = array3;
			ComponentReflection.PartEvents[typeof(HologramProjector)] = array7;
			ComponentReflection.PartEvents[typeof(IntPropertyChanger)] = array7;
			ComponentReflection.PartEvents[typeof(InventoryItemConvertor)] = array7;
			ComponentReflection.PartEvents[typeof(KindrishProperties)] = array7;
			ComponentReflection.PartEvents[typeof(LeakWhenBroken)] = array7;
			ComponentReflection.PartEvents[typeof(LiquidProducer)] = array7;
			ComponentReflection.PartEvents[typeof(LowStatBooster)] = array7;
			ComponentReflection.PartEvents[typeof(MechanicalWings)] = array7;
			ComponentReflection.PartEvents[typeof(MentalScreen)] = array7;
			ComponentReflection.PartEvents[typeof(MoteProperties)] = array3;
			ComponentReflection.PartEvents[typeof(MultiIntPropertyChanger)] = array7;
			ComponentReflection.PartEvents[typeof(MultiNavigationBonus)] = array7;
			ComponentReflection.PartEvents[typeof(NavigationBonus)] = array7;
			ComponentReflection.PartEvents[typeof(NeutronFluxContainment)] = array7;
			ComponentReflection.PartEvents[typeof(PartsGas)] = array7;
			ComponentReflection.PartEvents[typeof(PointDefense)] = array7;
			ComponentReflection.PartEvents[typeof(PoweredFloating)] = array7;
			ComponentReflection.PartEvents[typeof(PsychicMeridian)] = array7;
			ComponentReflection.PartEvents[typeof(RealityStabilization)] = array7;
			ComponentReflection.PartEvents[typeof(ReduceCooldowns)] = array7;
			ComponentReflection.PartEvents[typeof(ReduceEnergyCosts)] = array7;
			ComponentReflection.PartEvents[typeof(ReshephProjectionWidget)] = array7;
			ComponentReflection.PartEvents[typeof(RestoreOnDeath)] = array7;
			ComponentReflection.PartEvents[typeof(RocketSkates)] = array7;
			ComponentReflection.PartEvents[typeof(SaveModifier)] = array7;
			ComponentReflection.PartEvents[typeof(SaveModifiers)] = array7;
			ComponentReflection.PartEvents[typeof(SlaveMask)] = array7;
			ComponentReflection.PartEvents[typeof(SlipRing)] = array7;
			ComponentReflection.PartEvents[typeof(Suspensor)] = array7;
			ComponentReflection.PartEvents[typeof(TemperatureAdjuster)] = array7;
			ComponentReflection.PartEvents[typeof(TemperatureController)] = array7;
			ComponentReflection.PartEvents[typeof(ThermalAmp)] = array7;
			ComponentReflection.PartEvents[typeof(TorchProperties)] = array3;
			ComponentReflection.PartEvents[typeof(UrbanCamouflage)] = array7;
			ComponentReflection.PartEvents[typeof(Waldopack)] = array7;
			ComponentReflection.PartEvents[typeof(DrawInTheDark)] = array3;
			ComponentReflection.PartEvents[typeof(LeaksFluid)] = array7;
			ComponentReflection.PartEvents[typeof(AIShootRound)] = array7;
			ComponentReflection.PartEvents[typeof(MagazineAmmoLoader)] = array7;
			ComponentReflection.PartEvents[typeof(ExplodeAfterTurns)] = array4;
			ComponentReflection.PartEvents[typeof(ModCoProcessor)] = array7;
			ComponentReflection.PartEvents[typeof(ModDesecrated)] = array3;
			ComponentReflection.PartEvents[typeof(ModMagnetized)] = array7;
			ComponentReflection.PartEvents[typeof(ModCamo)] = array3;
			ComponentReflection.PartEvents[typeof(ModMetallized)] = array3;
			ComponentReflection.PartEvents[typeof(ModNanochelated)] = array3;
			ComponentReflection.PartEvents[typeof(ModUrbanCamo)] = array3;
			ComponentReflection.PartEvents[typeof(DisperseEMP)] = array3;
			ComponentReflection.PartEvents[typeof(Explores)] = array7;
			ComponentReflection.PartEvents[typeof(PetGloaming)] = array3;
			ComponentReflection.PartEvents[typeof(LightDimmer)] = array7;
			ComponentReflection.PartEvents[typeof(MechaPlayer)] = array3;
			ComponentReflection.PartEvents[typeof(PetEitherOr)] = array3;
			ComponentReflection.PartEvents[typeof(AmbientCollector)] = array7;
			ComponentReflection.PartEvents[typeof(AmbientPowerReceiver)] = array7;
			ComponentReflection.PartEvents[typeof(BiomechanicalPowerTransmission)] = array6;
			ComponentReflection.PartEvents[typeof(BroadcastPowerReceiver)] = array7;
			ComponentReflection.PartEvents[typeof(ChargeSink)] = array7;
			ComponentReflection.PartEvents[typeof(ElectricalPowerTransmission)] = array6;
			ComponentReflection.PartEvents[typeof(EnergyCellChargeLevel)] = array7;
			ComponentReflection.PartEvents[typeof(EnergyCellSocket)] = array7;
			ComponentReflection.PartEvents[typeof(EquipCharge)] = array7;
			ComponentReflection.PartEvents[typeof(FusionReactor)] = array7;
			ComponentReflection.PartEvents[typeof(GenericPowerTransmission)] = array6;
			ComponentReflection.PartEvents[typeof(HydraulicPowerTransmission)] = array6;
			ComponentReflection.PartEvents[typeof(HydroTurbine)] = array7;
			ComponentReflection.PartEvents[typeof(LiquidFueledPowerPlant)] = array7;
			ComponentReflection.PartEvents[typeof(MechanicalPowerTransmission)] = array6;
			ComponentReflection.PartEvents[typeof(RequiresPowerToEquip)] = array7;
			ComponentReflection.PartEvents[typeof(SolarArray)] = array7;
			ComponentReflection.PartEvents[typeof(WindTurbine)] = array7;
			ComponentReflection.PartEvents[typeof(ZeroPointEnergyCollector)] = array7;
			ComponentReflection.PartEvents[typeof(PrefabImposter)] = array3;
			ComponentReflection.PartEvents[typeof(PrefabImposterFinal)] = array4;
			ComponentReflection.PartEvents[typeof(ExistenceSupport)] = array7;
			ComponentReflection.PartEvents[typeof(XRL.World.Parts.Temporary)] = array7;
			ComponentReflection.PartEvents[typeof(AIBarathrumShuttle)] = array7;
			ComponentReflection.PartEvents[typeof(QuestGiverImposter)] = array;
			ComponentReflection.PartEvents[typeof(QuestStarter)] = array3;
			ComponentReflection.PartEvents[typeof(QuestStepFinisher)] = array3;
			ComponentReflection.PartEvents[typeof(Reconstitution)] = array7;
			ComponentReflection.PartEvents[typeof(RespondToEvent)] = array7;
			ComponentReflection.PartEvents[typeof(SecretObject)] = array3;
			ComponentReflection.PartEvents[typeof(Tinkering_Mine)] = array7;
			ComponentReflection.PartEvents[typeof(SoundRender)] = array3;
			ComponentReflection.PartEvents[typeof(AlternateOverlayRender)] = array8;
			ComponentReflection.PartEvents[typeof(CryoZone)] = array;
			ComponentReflection.PartEvents[typeof(FactoryArm)] = array3;
			ComponentReflection.PartEvents[typeof(Fan)] = array6;
			ComponentReflection.PartEvents[typeof(GrabberArm)] = array3;
			ComponentReflection.PartEvents[typeof(PistonPressController)] = array3;
			ComponentReflection.PartEvents[typeof(PistonPressElement)] = array3;
			ComponentReflection.PartEvents[typeof(LocationFinder)] = array3;
			ComponentReflection.PartEvents[typeof(PhaseSticky)] = array3;
			ComponentReflection.PartEvents[typeof(PluckablePolyp)] = array7;
			ComponentReflection.PartEvents[typeof(PyroZone)] = array;
			ComponentReflection.PartEvents[typeof(SpaceTimeRift)] = array3;
			ComponentReflection.PartEvents[typeof(SpaceTimeVortex)] = array3;
			ComponentReflection.PartEvents[typeof(StairHighlight)] = array4;
			ComponentReflection.PartEvents[typeof(WorldTeleporter)] = array7;
			ComponentReflection.PartEvents[typeof(VillageSurface)] = array7;
			ComponentReflection.PartEvents[typeof(Walltrap)] = array7;
			ComponentReflection.PartEvents[typeof(Wormhole)] = array3;
			ComponentReflection.PartEvents[typeof(ThinWorld)] = array4;
			ComponentReflection.PartEvents[typeof(AIVehiclePilot)] = array7;
			ComponentReflection.PartEvents[typeof(ShevaStarshipControl)] = array7;
			ComponentReflection.PartEvents[typeof(Vehicle)] = array7;
			ComponentReflection.PartEvents[typeof(VehiclePairBonus)] = array7;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialElectric)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialExtradimensional)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialFire)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialForcefield)] = array;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialForcefieldColors)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialGeneric)] = array6;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialGenericAlternate)] = array6;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialGenericSecondAlternate)] = array6;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialGreenFire)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialHighlyEntropic)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialLuminous)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialMagentaFire)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialMainframeTapeDrive)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialRealityStabilizationField)] = array;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialSaltDunes)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialStasisfield)] = array;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialTechlight)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedWallGeneric)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialWater)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialOverlandWater)] = array3;
			ComponentReflection.PartEvents[typeof(AnimatedMaterialWhiteFire)] = array3;
			ComponentReflection.PartEvents[typeof(ApothecaryIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(BackdropWhenVisible)] = array4;
			ComponentReflection.PartEvents[typeof(ConcealedHologramMaterial)] = array3;
			ComponentReflection.PartEvents[typeof(FirethumbedIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(FungiFriendIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(FungusRiddenIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(HeroIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(HologramMaterial)] = array3;
			ComponentReflection.PartEvents[typeof(HologramMaterialPrimary)] = array3;
			ComponentReflection.PartEvents[typeof(KindlethumbedIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(MerchantIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(MossyUnityMaterial)] = array;
			ComponentReflection.PartEvents[typeof(MossyUnityMaterial2)] = array3;
			ComponentReflection.PartEvents[typeof(NephilimCultistIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(QudzuSymbioteIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(RenderLiquidBackground)] = array3;
			ComponentReflection.PartEvents[typeof(SlimespitterIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(SlimewalkerIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(ThinWorldMaterial)] = array3;
			ComponentReflection.PartEvents[typeof(TinkerIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(UnityPrefabImposter)] = array;
			ComponentReflection.PartEvents[typeof(WantsBackdrop)] = array4;
			ComponentReflection.PartEvents[typeof(NoBackdropBleedthrough)] = array4;
			ComponentReflection.PartEvents[typeof(WardenIconColor)] = array3;
			ComponentReflection.PartEvents[typeof(AlarmCircle)] = array3;
			ComponentReflection.PartEvents[typeof(GritGatePowerDisplay)] = array3;
			ComponentReflection.PartEvents[typeof(IfThenElseQuestWidget)] = array7;
			ComponentReflection.PartEvents[typeof(GolemQuestMound)] = array7;
			ComponentReflection.PartEvents[typeof(ConveyorPad)] = array3;
			ComponentReflection.PartEvents[typeof(Smash_Floor)] = array3;
			ComponentReflection.PartEvents[typeof(Endurance_ShakeItOff)] = array7;
			ComponentReflection.PartEvents[typeof(Discipline_Lionheart)] = array7;
			ComponentReflection.PartEvents[typeof(SpontaneousCombustion)] = array7;
			ComponentReflection.PartEvents[typeof(WeakHeart)] = array7;
			ComponentReflection.PartEvents[typeof(MentalMirror)] = array7;
			ComponentReflection.PartEvents[typeof(SensePsychic)] = array7;
			ComponentReflection.PartEvents[typeof(TimeDilation)] = array3;
			ComponentReflection.PartEvents[typeof(CrungleGaze)] = array;
			ComponentReflection.PartEvents[typeof(Decarbonizer)] = array;
			ComponentReflection.PartEvents[typeof(Infiltrate)] = array;
			ComponentReflection.PartEvents[typeof(Interdiction)] = array3;
			ComponentReflection.PartEvents[typeof(IrisdualBeam)] = array4;
			ComponentReflection.PartEvents[typeof(LiquidFont)] = array7;
			ComponentReflection.PartEvents[typeof(MagneticPulse)] = array3;
			ComponentReflection.PartEvents[typeof(MultiHorns)] = array3;
			ComponentReflection.PartEvents[typeof(QuantumFugue)] = array7;
			ComponentReflection.PartEvents[typeof(StickyTongue)] = array3;
			ComponentReflection.PartEvents[typeof(StoneGaze)] = array;
			ComponentReflection.PartEvents[typeof(AdrenalControl2)] = array3;
			ComponentReflection.PartEvents[typeof(ElectricalGeneration)] = array7;
			ComponentReflection.PartEvents[typeof(SleepGasGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(CorrosiveGasGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(PlasmaGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(PoisonGasGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(DefoliantGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(FungicideGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(ConfusionGasGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(NormalityGasGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(GlitterDustGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(GasGeneration)] = array3;
			ComponentReflection.PartEvents[typeof(PhotosyntheticSkin)] = array3;
			ComponentReflection.PartEvents[typeof(ReflectShame)] = array7;
			ComponentReflection.EffectEvents[typeof(Adjusted)] = array3;
			ComponentReflection.EffectEvents[typeof(ArtifactDetectionEffect)] = array4;
			ComponentReflection.EffectEvents[typeof(AshPoison)] = array3;
			ComponentReflection.EffectEvents[typeof(Asleep)] = array3;
			ComponentReflection.EffectEvents[typeof(AxonsInflated)] = array3;
			ComponentReflection.EffectEvents[typeof(AxonsDeflated)] = array3;
			ComponentReflection.EffectEvents[typeof(BasiliskPoison)] = array3;
			ComponentReflection.EffectEvents[typeof(Berserk)] = array3;
			ComponentReflection.EffectEvents[typeof(Blaze_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(Bleeding)] = array3;
			ComponentReflection.EffectEvents[typeof(Burning)] = array3;
			ComponentReflection.EffectEvents[typeof(CardiacArrest)] = array3;
			ComponentReflection.EffectEvents[typeof(XRL.World.Effects.Confused)] = array3;
			ComponentReflection.EffectEvents[typeof(CorpseTethered)] = array3;
			ComponentReflection.EffectEvents[typeof(Cripple)] = array3;
			ComponentReflection.EffectEvents[typeof(Dashing)] = array3;
			ComponentReflection.EffectEvents[typeof(Dazed)] = array3;
			ComponentReflection.EffectEvents[typeof(Monochrome)] = array3;
			ComponentReflection.EffectEvents[typeof(Disguised)] = array5;
			ComponentReflection.EffectEvents[typeof(Ecstatic)] = array3;
			ComponentReflection.EffectEvents[typeof(ElectromagneticPulsed)] = array3;
			ComponentReflection.EffectEvents[typeof(EmptyTheClips)] = array3;
			ComponentReflection.EffectEvents[typeof(Engulfed)] = array3;
			ComponentReflection.EffectEvents[typeof(EpistemicDisguise)] = array5;
			ComponentReflection.EffectEvents[typeof(Exhausted)] = array3;
			ComponentReflection.EffectEvents[typeof(Flagging)] = array3;
			ComponentReflection.EffectEvents[typeof(Flying)] = array3;
			ComponentReflection.EffectEvents[typeof(FoliageCamouflaged)] = array3;
			ComponentReflection.EffectEvents[typeof(Freezing)] = array3;
			ComponentReflection.EffectEvents[typeof(Frenzied)] = array3;
			ComponentReflection.EffectEvents[typeof(Frozen)] = array3;
			ComponentReflection.EffectEvents[typeof(FuriouslyConfused)] = array3;
			ComponentReflection.EffectEvents[typeof(GeometricHeal)] = array3;
			ComponentReflection.EffectEvents[typeof(Healing)] = array3;
			ComponentReflection.EffectEvents[typeof(HeightenedHearingEffect)] = array4;
			ComponentReflection.EffectEvents[typeof(HeightenedSmellEffect)] = array4;
			ComponentReflection.EffectEvents[typeof(Hoarshroom_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(HolographicBleeding)] = array3;
			ComponentReflection.EffectEvents[typeof(Hooked)] = array3;
			ComponentReflection.EffectEvents[typeof(HulkHoney_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(Immobilized)] = array3;
			ComponentReflection.EffectEvents[typeof(Interdicted)] = array3;
			ComponentReflection.EffectEvents[typeof(LatchedOnto)] = array3;
			ComponentReflection.EffectEvents[typeof(XRL.World.Effects.LifeDrain)] = array3;
			ComponentReflection.EffectEvents[typeof(LiquidCovered)] = array3;
			ComponentReflection.EffectEvents[typeof(LiquidStained)] = array3;
			ComponentReflection.EffectEvents[typeof(LongbladeEffect_EnGarde)] = array3;
			ComponentReflection.EffectEvents[typeof(LoveTonic)] = array3;
			ComponentReflection.EffectEvents[typeof(Lovesick)] = array3;
			ComponentReflection.EffectEvents[typeof(Meditating)] = array3;
			ComponentReflection.EffectEvents[typeof(NocturnalApexed)] = array3;
			ComponentReflection.EffectEvents[typeof(Nosebleed)] = array3;
			ComponentReflection.EffectEvents[typeof(Nullphased)] = array3;
			ComponentReflection.EffectEvents[typeof(Omniphase)] = array3;
			ComponentReflection.EffectEvents[typeof(Paralyzed)] = array3;
			ComponentReflection.EffectEvents[typeof(PhasePoisoned)] = array3;
			ComponentReflection.EffectEvents[typeof(Phased)] = array3;
			ComponentReflection.EffectEvents[typeof(PoisonGasPoison)] = array3;
			ComponentReflection.EffectEvents[typeof(Poisoned)] = array3;
			ComponentReflection.EffectEvents[typeof(Prone)] = array3;
			ComponentReflection.EffectEvents[typeof(RealityStabilized)] = array;
			ComponentReflection.EffectEvents[typeof(Refresh)] = array3;
			ComponentReflection.EffectEvents[typeof(RifleMark)] = array3;
			ComponentReflection.EffectEvents[typeof(Rubbergum_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(Running)] = array3;
			ComponentReflection.EffectEvents[typeof(Rusted)] = array3;
			ComponentReflection.EffectEvents[typeof(Salve_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(Scintillating)] = array3;
			ComponentReflection.EffectEvents[typeof(SensePsychicEffect)] = array4;
			ComponentReflection.EffectEvents[typeof(SenseRobotEffect)] = array4;
			ComponentReflection.EffectEvents[typeof(ShadeOil_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(Shamed)] = array3;
			ComponentReflection.EffectEvents[typeof(ShatterArmor)] = array3;
			ComponentReflection.EffectEvents[typeof(ShatterMentalArmor)] = array3;
			ComponentReflection.EffectEvents[typeof(ShieldWall)] = array3;
			ComponentReflection.EffectEvents[typeof(Skulk_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(SphynxSalt_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(SporeCloudPoison)] = array3;
			ComponentReflection.EffectEvents[typeof(StingerPoisoned)] = array3;
			ComponentReflection.EffectEvents[typeof(Stressed)] = array3;
			ComponentReflection.EffectEvents[typeof(Stuck)] = array3;
			ComponentReflection.EffectEvents[typeof(Stun)] = array3;
			ComponentReflection.EffectEvents[typeof(StunGasStun)] = array3;
			ComponentReflection.EffectEvents[typeof(Suppressed)] = array3;
			ComponentReflection.EffectEvents[typeof(Swimming)] = array3;
			ComponentReflection.EffectEvents[typeof(SynapseSnap)] = array3;
			ComponentReflection.EffectEvents[typeof(TimeDilated)] = array3;
			ComponentReflection.EffectEvents[typeof(TimeDilatedIndependent)] = array3;
			ComponentReflection.EffectEvents[typeof(ToughnessDrained)] = array3;
			ComponentReflection.EffectEvents[typeof(Trance)] = array6;
			ComponentReflection.EffectEvents[typeof(Ubernostrum_Tonic)] = array3;
			ComponentReflection.EffectEvents[typeof(UrbanCamouflaged)] = array3;
			ComponentReflection.EffectEvents[typeof(VehicleUnpowered)] = array3;
			ComponentReflection.EffectEvents[typeof(Wading)] = array3;
			ComponentReflection.EffectEvents[typeof(WarTrance)] = array3;
			ComponentReflection.EffectEvents[typeof(WarmingUp)] = array3;
			ComponentReflection.EffectEvents[typeof(ProjectileReflectionShield)] = array3;
			ComponentReflection.EffectEvents[typeof(IrisdualMolting)] = array;
			ComponentReflection.EffectEvents[typeof(Cudgel_SmashingUp)] = array3;
			ComponentReflection.EffectEvents[typeof(Hobbled)] = array3;
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x0016F894 File Offset: 0x0016DA94
		public static void Evaluate(IEnumerable<Type> Types, out Rack<int[]> Arrays, out ulong Hash)
		{
			ComponentReflection.PartEvents.Clear();
			ComponentReflection.EffectEvents.Clear();
			ComponentReflection.PartExtensions.Clear();
			ComponentReflection.EffectExtensions.Clear();
			Arrays = new Rack<int[]>();
			Hash = 14695981039346656037UL;
			Rack<int> rack = new Rack<int>();
			Dictionary<Type, Rack<Type>> dictionary = new Dictionary<Type, Rack<Type>>();
			Type typeFromHandle = typeof(IComponent<GameObject>);
			Type[] array = new Type[] { typeof(RenderEvent) };
			Type[] array2 = new Type[]
			{
				typeof(long),
				typeof(int)
			};
			string text = "Render";
			string text2 = "OverlayRender";
			string text3 = "FinalRender";
			string text4 = "TurnTick";
			foreach (Type type in Types)
			{
				if (!type.IsAbstract && typeof(IComponent<GameObject>).IsAssignableFrom(type))
				{
					if (type.GetMethod(text, array).DeclaringType != typeFromHandle)
					{
						rack.Add(RenderEvent.ID);
					}
					if (type.GetMethod(text2, array).DeclaringType != typeFromHandle)
					{
						rack.Add(RenderEvent.OverlayID);
					}
					if (type.GetMethod(text3, array).DeclaringType != typeFromHandle)
					{
						rack.Add(RenderEvent.FinalID);
					}
					if (type.GetMethod(text4, array2).DeclaringType != typeFromHandle)
					{
						rack.Add(IComponent<GameObject>.TurnTickID);
					}
					int[] array3 = null;
					if (rack.Count > 0)
					{
						int num = 0;
						int count = Arrays.Count;
						while (num < count && array3 == null)
						{
							array3 = Arrays[num];
							if (array3.Length != rack.Count)
							{
								array3 = null;
							}
							else
							{
								int i = 0;
								int num2 = array3.Length;
								while (i < num2)
								{
									if (array3[i] != rack[i])
									{
										array3 = null;
										break;
									}
									i++;
								}
							}
							num++;
						}
						if (array3 == null)
						{
							array3 = rack.ToArray();
							Arrays.Add(array3);
						}
						if (typeof(IPart).IsAssignableFrom(type))
						{
							ComponentReflection.PartEvents[type] = array3;
						}
						else
						{
							ComponentReflection.EffectEvents[type] = array3;
						}
						rack.Clear();
					}
					if (type.IsDefined(typeof(ComponentExtensionAttribute)))
					{
						for (Type type2 = type.BaseType; type2 != null; type2 = type2.BaseType)
						{
							if (type2.IsAbstract && type2.IsGenericType)
							{
								Type genericTypeDefinition = type2.GetGenericTypeDefinition();
								if (genericTypeDefinition == typeof(IPartExtension<>) || genericTypeDefinition == typeof(IEffectExtension<>))
								{
									Type type3 = type2.GetGenericArguments().FirstOrDefault<Type>();
									if (type3 != null && !type3.IsAbstract)
									{
										Rack<Type> rack2;
										if (!dictionary.TryGetValue(type3, out rack2))
										{
											rack2 = (dictionary[type3] = new Rack<Type>());
										}
										rack2.Add(type);
										break;
									}
									break;
								}
							}
						}
					}
				}
			}
			using (TextBuilder textBuilder = TextBuilder.Get(64))
			{
				Rack<string> rack3 = new Rack<string>();
				foreach (KeyValuePair<Type, Rack<Type>> keyValuePair in dictionary)
				{
					Type type4;
					Rack<Type> rack4;
					keyValuePair.Deconstruct(out type4, out rack4);
					Type type5 = type4;
					Rack<Type> rack5 = rack4;
					ComponentReflection.TargetSignature[0] = type5;
					bool flag = typeof(IPart).IsAssignableFrom(type5);
					if (flag || typeof(Effect).IsAssignableFrom(type5))
					{
						ComponentReflection.Add add = ComponentReflection.CreateAddDelegate(type5, rack5, flag);
						if (add != null)
						{
							ComponentReflection.FinalizeRead finalizeRead = ComponentReflection.CreateFinalizeReadDelegate(type5, rack5, flag);
							if (finalizeRead != null)
							{
								if (flag)
								{
									ComponentReflection.PartExtensions[type5] = new ComponentReflection.ExtensionRecord(add, finalizeRead);
								}
								else
								{
									ComponentReflection.EffectExtensions[type5] = new ComponentReflection.ExtensionRecord(add, finalizeRead);
								}
								foreach (Type type6 in rack5)
								{
									textBuilder.Clear();
									ModInfo modInfo;
									if (ModManager.ModAssemblyMap.TryGetValue(type6.Assembly, out modInfo))
									{
										textBuilder.Append(modInfo.ID).Append('.');
									}
									type6.AppendName(textBuilder, true);
									rack3.Add(textBuilder.ToString());
								}
							}
						}
					}
				}
				rack3.Sort();
				ComponentReflection.ExtensionHash = Hash.FNV1A64(rack3, 14695981039346656037UL);
			}
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x0016FD78 File Offset: 0x0016DF78
		private static ComponentReflection.Add CreateAddDelegate(Type Target, Rack<Type> Extensions, bool Part)
		{
			DynamicMethod dynamicMethod = new DynamicMethod("OnAddComponentDelegate_" + Target.Name, typeof(void), ComponentReflection.AddSignature, Target, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.DeclareLocal(typeof(IComponent<GameObject>));
			foreach (Type type in Extensions)
			{
				ComponentReflection.EmitBody(ilgenerator, type, false, Part);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return (ComponentReflection.Add)dynamicMethod.CreateDelegate(typeof(ComponentReflection.Add));
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x0016FE28 File Offset: 0x0016E028
		private static ComponentReflection.FinalizeRead CreateFinalizeReadDelegate(Type Target, Rack<Type> Extensions, bool Part)
		{
			DynamicMethod dynamicMethod = new DynamicMethod("OnFinalizeReadComponentDelegate_" + Target.Name, typeof(void), ComponentReflection.FinalizeReadSignature, Target, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.DeclareLocal(typeof(IComponent<GameObject>));
			foreach (Type type in Extensions)
			{
				ComponentReflection.EmitBody(ilgenerator, type, true, Part);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return (ComponentReflection.FinalizeRead)dynamicMethod.CreateDelegate(typeof(ComponentReflection.FinalizeRead));
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x0016FED8 File Offset: 0x0016E0D8
		private static void EmitBody(ILGenerator Generator, Type Extension, bool Conditional, bool Part)
		{
			FieldInfo field = Extension.GetField(Part ? "ParentPart" : "ParentEffect", ComponentReflection.Flags);
			if (field == null)
			{
				return;
			}
			Type[] array = ComponentReflection.TargetSignature;
			ConstructorInfo constructorInfo = Extension.GetConstructor(ComponentReflection.Flags, null, array, null);
			if (constructorInfo == null)
			{
				array = Type.EmptyTypes;
				constructorInfo = Extension.GetConstructor(ComponentReflection.Flags, null, array, null);
				if (constructorInfo == null)
				{
					return;
				}
			}
			Label label = default(Label);
			if (Conditional)
			{
				label = Generator.DefineLabel();
				Generator.Emit(OpCodes.Ldarg_0);
				Generator.Emit(OpCodes.Ldtoken, Extension);
				Generator.Emit(OpCodes.Call, ComponentReflection.GetTypeFromHandle);
				Generator.Emit(OpCodes.Call, Part ? ComponentReflection.HasPart : ComponentReflection.HasEffect);
				Generator.Emit(OpCodes.Brtrue_S, label);
			}
			Generator.Emit(OpCodes.Ldarg_0);
			if (array.Length == 1)
			{
				Generator.Emit(OpCodes.Ldarg_1);
			}
			Generator.Emit(OpCodes.Newobj, constructorInfo);
			Generator.Emit(OpCodes.Stloc_0);
			Generator.Emit(OpCodes.Ldloc_0);
			Generator.Emit(OpCodes.Ldarg_1);
			Generator.Emit(OpCodes.Stfld, field);
			Generator.Emit(OpCodes.Ldloc_0);
			if (Part)
			{
				Generator.Emit(Conditional ? OpCodes.Ldc_I4_1 : OpCodes.Ldarg_2);
				Generator.Emit(OpCodes.Ldc_I4_1);
				Generator.Emit(Conditional ? OpCodes.Ldc_I4_0 : OpCodes.Ldarg_3);
				Generator.Emit(OpCodes.Call, ComponentReflection.AddPartInternals);
			}
			else
			{
				Generator.Emit(OpCodes.Ldloc_0);
				Generator.Emit(OpCodes.Ldarg_1);
				Generator.Emit(OpCodes.Ldfld, ComponentReflection.EffectID);
				Generator.Emit(OpCodes.Stfld, Extension.GetField("ParentEffectID", ComponentReflection.Flags));
				Generator.Emit(OpCodes.Ldnull);
				Generator.Emit(OpCodes.Call, ComponentReflection.ApplyEffect);
				Generator.Emit(OpCodes.Pop);
			}
			if (Conditional)
			{
				Generator.MarkLabel(label);
			}
		}

		// Token: 0x040022E9 RID: 8937
		public static readonly Dictionary<Type, int[]> PartEvents = new Dictionary<Type, int[]>();

		// Token: 0x040022EA RID: 8938
		public static readonly Dictionary<Type, int[]> EffectEvents = new Dictionary<Type, int[]>();

		// Token: 0x040022EB RID: 8939
		public static readonly Dictionary<Type, ComponentReflection.ExtensionRecord> PartExtensions = new Dictionary<Type, ComponentReflection.ExtensionRecord>();

		// Token: 0x040022EC RID: 8940
		public static readonly Dictionary<Type, ComponentReflection.ExtensionRecord> EffectExtensions = new Dictionary<Type, ComponentReflection.ExtensionRecord>();

		// Token: 0x040022ED RID: 8941
		public static ulong ExtensionHash;

		// Token: 0x040022EE RID: 8942
		private static readonly BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040022EF RID: 8943
		private static Type[] AddSignature = new Type[]
		{
			typeof(GameObject),
			typeof(IComponent<GameObject>),
			typeof(bool),
			typeof(bool)
		};

		// Token: 0x040022F0 RID: 8944
		private static Type[] FinalizeReadSignature = new Type[]
		{
			typeof(GameObject),
			typeof(IComponent<GameObject>)
		};

		// Token: 0x040022F1 RID: 8945
		private static Type[] TargetSignature = new Type[1];

		// Token: 0x040022F2 RID: 8946
		private static MethodInfo AddPartInternals = typeof(GameObject).GetMethod("AddPartInternals", ComponentReflection.Flags, null, new Type[]
		{
			typeof(IPart),
			typeof(bool),
			typeof(bool),
			typeof(bool)
		}, null);

		// Token: 0x040022F3 RID: 8947
		private static MethodInfo HasPart = typeof(GameObject).GetMethod("HasPart", ComponentReflection.Flags, null, new Type[] { typeof(Type) }, null);

		// Token: 0x040022F4 RID: 8948
		private static MethodInfo ApplyEffect = typeof(GameObject).GetMethod("ApplyEffect", new Type[]
		{
			typeof(Effect),
			typeof(GameObject)
		});

		// Token: 0x040022F5 RID: 8949
		private static MethodInfo HasEffect = typeof(GameObject).GetMethod("HasEffect", ComponentReflection.Flags, null, new Type[] { typeof(Type) }, null);

		// Token: 0x040022F6 RID: 8950
		private static MethodInfo GetTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040022F7 RID: 8951
		private static FieldInfo EffectID = typeof(Effect).GetField("ID", ComponentReflection.Flags);

		// Token: 0x02001A18 RID: 6680
		// (Invoke) Token: 0x0600E627 RID: 58919
		public delegate void Add(GameObject Object, IComponent<GameObject> Component, bool Registration, bool Creation);

		// Token: 0x02001A19 RID: 6681
		// (Invoke) Token: 0x0600E62B RID: 58923
		public delegate void FinalizeRead(GameObject Object, IComponent<GameObject> Component);

		// Token: 0x02001A1A RID: 6682
		public class ExtensionRecord
		{
			// Token: 0x0600E62E RID: 58926 RVA: 0x00531C09 File Offset: 0x0052FE09
			public ExtensionRecord(ComponentReflection.Add Add, ComponentReflection.FinalizeRead FinalizeRead)
			{
				this.Add = Add;
				this.FinalizeRead = FinalizeRead;
			}

			// Token: 0x04006818 RID: 26648
			public ComponentReflection.Add Add;

			// Token: 0x04006819 RID: 26649
			public ComponentReflection.FinalizeRead FinalizeRead;
		}
	}
}
