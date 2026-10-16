using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004B5 RID: 1205
	public interface IEventSource : IEventHandler
	{
		// Token: 0x06003026 RID: 12326 RVA: 0x000FC970 File Offset: 0x000FAB70
		bool HandleEvent(AIAfterMissileEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000FC979 File Offset: 0x000FAB79
		bool HandleEvent(AIAfterThrowEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000FC982 File Offset: 0x000FAB82
		bool HandleEvent(AIBoredEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000FC98B File Offset: 0x000FAB8B
		bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000FC994 File Offset: 0x000FAB94
		bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000FC99D File Offset: 0x000FAB9D
		bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000FC9A6 File Offset: 0x000FABA6
		bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000FC9AF File Offset: 0x000FABAF
		bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000FC9B8 File Offset: 0x000FABB8
		bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000FC9C1 File Offset: 0x000FABC1
		bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000FC9CA File Offset: 0x000FABCA
		bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x000FC9D3 File Offset: 0x000FABD3
		bool HandleEvent(AIHelpBroadcastEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000FC9DC File Offset: 0x000FABDC
		bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000FC9E5 File Offset: 0x000FABE5
		bool HandleEvent(ActorGetNavigationWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000FC9EE File Offset: 0x000FABEE
		bool HandleEvent(AddedToInventoryEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000FC9F7 File Offset: 0x000FABF7
		bool HandleEvent(AdjustTotalWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000FCA00 File Offset: 0x000FAC00
		bool HandleEvent(AdjustValueEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000FCA09 File Offset: 0x000FAC09
		bool HandleEvent(AdjustVisibilityRadiusEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000FCA12 File Offset: 0x000FAC12
		bool HandleEvent(AdjustWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000FCA1B File Offset: 0x000FAC1B
		bool HandleEvent(AfterAddActiveObjectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000FCA24 File Offset: 0x000FAC24
		bool HandleEvent(AfterAddOpinionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000FCA2D File Offset: 0x000FAC2D
		bool HandleEvent(AfterAddSkillEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000FCA36 File Offset: 0x000FAC36
		bool HandleEvent(AfterAfterThrownEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000FCA3F File Offset: 0x000FAC3F
		bool HandleEvent(AfterBasicBestowalEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000FCA48 File Offset: 0x000FAC48
		bool HandleEvent(AfterChangePartyLeaderEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000FCA51 File Offset: 0x000FAC51
		bool HandleEvent(AfterConsumeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000FCA5A File Offset: 0x000FAC5A
		bool HandleEvent(AfterContentsTakenEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000FCA63 File Offset: 0x000FAC63
		bool HandleEvent(AfterConversationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000FCA6C File Offset: 0x000FAC6C
		bool HandleEvent(AfterDieEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000FCA75 File Offset: 0x000FAC75
		bool HandleEvent(AfterDismemberEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000FCA7E File Offset: 0x000FAC7E
		bool HandleEvent(AfterElementBestowalEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000FCA87 File Offset: 0x000FAC87
		bool HandleEvent(AfterExamineCriticalFailureEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000FCA90 File Offset: 0x000FAC90
		bool HandleEvent(AfterExamineFailureEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000FCA99 File Offset: 0x000FAC99
		bool HandleEvent(AfterGameLoadedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000FCAA2 File Offset: 0x000FACA2
		bool HandleEvent(AfterInventoryActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000FCAAB File Offset: 0x000FACAB
		bool HandleEvent(AfterLevelGainedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000FCAB4 File Offset: 0x000FACB4
		bool HandleEvent(AfterMentalAttackEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000FCABD File Offset: 0x000FACBD
		bool HandleEvent(AfterMentalDefendEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000FCAC6 File Offset: 0x000FACC6
		bool HandleEvent(AfterMoveFailedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000FCACF File Offset: 0x000FACCF
		bool HandleEvent(AfterObjectCreatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000FCAD8 File Offset: 0x000FACD8
		bool HandleEvent(AfterPetEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000FCAE1 File Offset: 0x000FACE1
		bool HandleEvent(AfterPilotChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000FCAEA File Offset: 0x000FACEA
		bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000FCAF3 File Offset: 0x000FACF3
		bool HandleEvent(AfterPseudoRelicGeneratedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000FCAFC File Offset: 0x000FACFC
		bool HandleEvent(AfterReadBookEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000FCB05 File Offset: 0x000FAD05
		bool HandleEvent(AfterRelicGeneratedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000FCB0E File Offset: 0x000FAD0E
		bool HandleEvent(AfterRemoveActiveObjectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000FCB17 File Offset: 0x000FAD17
		bool HandleEvent(AfterRemoveSkillEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000FCB20 File Offset: 0x000FAD20
		bool HandleEvent(AfterReputationChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000FCB29 File Offset: 0x000FAD29
		bool HandleEvent(AfterShieldBlockEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000FCB32 File Offset: 0x000FAD32
		bool HandleEvent(AfterThrownEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000FCB3B File Offset: 0x000FAD3B
		bool HandleEvent(AfterTravelEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000FCB44 File Offset: 0x000FAD44
		bool HandleEvent(AfterZoneBuiltEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000FCB4D File Offset: 0x000FAD4D
		bool HandleEvent(AllowInventoryStackEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000FCB56 File Offset: 0x000FAD56
		bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000FCB5F File Offset: 0x000FAD5F
		bool HandleEvent(AllowPolypPluckingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000FCB68 File Offset: 0x000FAD68
		bool HandleEvent(AllowTradeWithNoInventoryEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000FCB71 File Offset: 0x000FAD71
		bool HandleEvent(AnimateEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000FCB7A File Offset: 0x000FAD7A
		bool HandleEvent(AnyAutoCollectDramsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000FCB83 File Offset: 0x000FAD83
		bool HandleEvent(AnyRegenerableLimbsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000FCB8C File Offset: 0x000FAD8C
		bool HandleEvent(ApplyEffectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000FCB95 File Offset: 0x000FAD95
		bool HandleEvent(AttackerBeforeTemperatureChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000FCB9E File Offset: 0x000FAD9E
		bool HandleEvent(AttackerDealingDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000FCBA7 File Offset: 0x000FADA7
		bool HandleEvent(AttackerDealtDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000FCBB0 File Offset: 0x000FADB0
		bool HandleEvent(AttemptConversationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000FCBB9 File Offset: 0x000FADB9
		bool HandleEvent(AttemptToLandEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000FCBC2 File Offset: 0x000FADC2
		bool HandleEvent(AutoexploreObjectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000FCBCB File Offset: 0x000FADCB
		bool HandleEvent(AwardXPEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000FCBD4 File Offset: 0x000FADD4
		bool HandleEvent(AwardedXPEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000FCBDD File Offset: 0x000FADDD
		bool HandleEvent(AwardingXPEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000FCBE6 File Offset: 0x000FADE6
		bool HandleEvent(BeforeAITakingActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000FCBEF File Offset: 0x000FADEF
		bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000FCBF8 File Offset: 0x000FADF8
		bool HandleEvent(BeforeAddSkillEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000FCC01 File Offset: 0x000FAE01
		bool HandleEvent(BeforeAfterThrownEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000FCC0A File Offset: 0x000FAE0A
		bool HandleEvent(BeforeApplyDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000FCC13 File Offset: 0x000FAE13
		bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000FCC1C File Offset: 0x000FAE1C
		bool HandleEvent(BeforeConsumeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000FCC25 File Offset: 0x000FAE25
		bool HandleEvent(BeforeConversationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000FCC2E File Offset: 0x000FAE2E
		bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000FCC37 File Offset: 0x000FAE37
		bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000FCC40 File Offset: 0x000FAE40
		bool HandleEvent(BeforeDetonateEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000FCC49 File Offset: 0x000FAE49
		bool HandleEvent(BeforeDieEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000FCC52 File Offset: 0x000FAE52
		bool HandleEvent(BeforeDismemberEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000FCC5B File Offset: 0x000FAE5B
		bool HandleEvent(BeforeFireMissileWeaponsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000FCC64 File Offset: 0x000FAE64
		bool HandleEvent(BeforeInteriorCollapseEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000FCC6D File Offset: 0x000FAE6D
		bool HandleEvent(BeforeLevelGainedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000FCC76 File Offset: 0x000FAE76
		bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000FCC7F File Offset: 0x000FAE7F
		bool HandleEvent(BeforeMentalAttackEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000FCC88 File Offset: 0x000FAE88
		bool HandleEvent(BeforeMentalDefendEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000FCC91 File Offset: 0x000FAE91
		bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000FCC9A File Offset: 0x000FAE9A
		bool HandleEvent(BeforePilotChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000FCCA3 File Offset: 0x000FAEA3
		bool HandleEvent(BeforePlayMusicEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000FCCAC File Offset: 0x000FAEAC
		bool HandleEvent(BeforeProjectileHitEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000FCCB5 File Offset: 0x000FAEB5
		bool HandleEvent(BeforePullDownEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000FCCBE File Offset: 0x000FAEBE
		bool HandleEvent(BeforeRemoveSkillEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000FCCC7 File Offset: 0x000FAEC7
		bool HandleEvent(BeforeRenderEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000FCCD0 File Offset: 0x000FAED0
		bool HandleEvent(BeforeSetFeelingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000FCCD9 File Offset: 0x000FAED9
		bool HandleEvent(BeforeShieldBlockEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000FCCE2 File Offset: 0x000FAEE2
		bool HandleEvent(BeforeSlamEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000FCCEB File Offset: 0x000FAEEB
		bool HandleEvent(BeforeTakeActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000FCCF4 File Offset: 0x000FAEF4
		bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000FCCFD File Offset: 0x000FAEFD
		bool HandleEvent(BeforeTookDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000FCD06 File Offset: 0x000FAF06
		bool HandleEvent(BeforeTravelDownEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000FCD0F File Offset: 0x000FAF0F
		bool HandleEvent(BeforeUnequippedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000FCD18 File Offset: 0x000FAF18
		bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000FCD21 File Offset: 0x000FAF21
		bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000FCD2A File Offset: 0x000FAF2A
		bool HandleEvent(BeginConversationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000FCD33 File Offset: 0x000FAF33
		bool HandleEvent(BeginMentalAttackEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000FCD3C File Offset: 0x000FAF3C
		bool HandleEvent(BeginMentalDefendEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000FCD45 File Offset: 0x000FAF45
		bool HandleEvent(BeginTakeActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000FCD4E File Offset: 0x000FAF4E
		bool HandleEvent(BeingConsumedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000FCD57 File Offset: 0x000FAF57
		bool HandleEvent(BlasphemyPerformedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000FCD60 File Offset: 0x000FAF60
		bool HandleEvent(BlocksRadarEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000FCD69 File Offset: 0x000FAF69
		bool HandleEvent(BodyPositionChangedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000FCD72 File Offset: 0x000FAF72
		bool HandleEvent(BootSequenceAbortedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000FCD7B File Offset: 0x000FAF7B
		bool HandleEvent(BootSequenceDoneEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000FCD84 File Offset: 0x000FAF84
		bool HandleEvent(BootSequenceInitializedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000FCD8D File Offset: 0x000FAF8D
		bool HandleEvent(CanAcceptObjectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000FCD96 File Offset: 0x000FAF96
		bool HandleEvent(CanApplyEffectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000FCD9F File Offset: 0x000FAF9F
		bool HandleEvent(CanBeDismemberedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000FCDA8 File Offset: 0x000FAFA8
		bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000FCDB1 File Offset: 0x000FAFB1
		bool HandleEvent(CanBeMagneticallyManipulatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000FCDBA File Offset: 0x000FAFBA
		bool HandleEvent(CanBeModdedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000FCDC3 File Offset: 0x000FAFC3
		bool HandleEvent(CanBeNamedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000FCDCC File Offset: 0x000FAFCC
		bool HandleEvent(CanBeReplicatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000FCDD5 File Offset: 0x000FAFD5
		bool HandleEvent(CanBeSlottedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000FCDDE File Offset: 0x000FAFDE
		bool HandleEvent(CanBeTradedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000FCDE7 File Offset: 0x000FAFE7
		bool HandleEvent(CanBeUnequippedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000FCDF0 File Offset: 0x000FAFF0
		bool HandleEvent(CanChangeMovementModeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000FCDF9 File Offset: 0x000FAFF9
		bool HandleEvent(CanDrinkEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000FCE02 File Offset: 0x000FB002
		bool HandleEvent(CanEnterInteriorEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000FCE0B File Offset: 0x000FB00B
		bool HandleEvent(CanFallEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000FCE14 File Offset: 0x000FB014
		bool HandleEvent(CanFireAllMissileWeaponsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000FCE1D File Offset: 0x000FB01D
		bool HandleEvent(CanGiveDirectionsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000FCE26 File Offset: 0x000FB026
		bool HandleEvent(CanHaveConversationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000FCE2F File Offset: 0x000FB02F
		bool HandleEvent(CanJoinPartyLeaderEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000FCE38 File Offset: 0x000FB038
		bool HandleEvent(CanMissilePassForcefieldEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000FCE41 File Offset: 0x000FB041
		bool HandleEvent(CanReceiveEmpathyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000FCE4A File Offset: 0x000FB04A
		bool HandleEvent(CanReceiveTelepathyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000FCE53 File Offset: 0x000FB053
		bool HandleEvent(CanRefreshAbilityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000FCE5C File Offset: 0x000FB05C
		bool HandleEvent(CanSmartUseEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000FCE65 File Offset: 0x000FB065
		bool HandleEvent(CanStartConversationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000FCE6E File Offset: 0x000FB06E
		bool HandleEvent(CanTemperatureReturnToAmbientEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000FCE77 File Offset: 0x000FB077
		bool HandleEvent(CanTradeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000FCE80 File Offset: 0x000FB080
		bool HandleEvent(CanTravelEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000FCE89 File Offset: 0x000FB089
		bool HandleEvent(CarryingCapacityChangedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000FCE92 File Offset: 0x000FB092
		bool HandleEvent(CellChangedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000FCE9B File Offset: 0x000FB09B
		bool HandleEvent(CellDepletedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000FCEA4 File Offset: 0x000FB0A4
		bool HandleEvent(ChargeAvailableEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000FCEAD File Offset: 0x000FB0AD
		bool HandleEvent(ChargeUsedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000FCEB6 File Offset: 0x000FB0B6
		bool HandleEvent(CheckAnythingToCleanEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000FCEBF File Offset: 0x000FB0BF
		bool HandleEvent(CheckAnythingToCleanWithEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000FCEC8 File Offset: 0x000FB0C8
		bool HandleEvent(CheckAnythingToCleanWithNearbyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000FCED1 File Offset: 0x000FB0D1
		bool HandleEvent(CheckAttackableEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000FCEDA File Offset: 0x000FB0DA
		bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000FCEE3 File Offset: 0x000FB0E3
		bool HandleEvent(CheckGasCanAffectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000FCEEC File Offset: 0x000FB0EC
		bool HandleEvent(CheckLoadAmmoEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000FCEF5 File Offset: 0x000FB0F5
		bool HandleEvent(CheckOverburdenedOnStrengthUpdateEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000FCEFE File Offset: 0x000FB0FE
		bool HandleEvent(CheckPaintabilityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000FCF07 File Offset: 0x000FB107
		bool HandleEvent(CheckReadyToFireEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000FCF10 File Offset: 0x000FB110
		bool HandleEvent(CheckRealityDistortionAccessibilityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000FCF19 File Offset: 0x000FB119
		bool HandleEvent(CheckRealityDistortionAdvisabilityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000FCF22 File Offset: 0x000FB122
		bool HandleEvent(CheckRealityDistortionUsabilityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000FCF2B File Offset: 0x000FB12B
		bool HandleEvent(CheckSpawnMergeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000FCF34 File Offset: 0x000FB134
		bool HandleEvent(CheckTileChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000FCF3D File Offset: 0x000FB13D
		bool HandleEvent(CheckUsesChargeWhileEquippedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000FCF46 File Offset: 0x000FB146
		bool HandleEvent(CleanItemsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000FCF4F File Offset: 0x000FB14F
		bool HandleEvent(CollectBroadcastChargeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000FCF58 File Offset: 0x000FB158
		bool HandleEvent(CommandEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000FCF61 File Offset: 0x000FB161
		bool HandleEvent(CommandReloadEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000FCF6A File Offset: 0x000FB16A
		bool HandleEvent(CommandReplaceCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000FCF73 File Offset: 0x000FB173
		bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000FCF7C File Offset: 0x000FB17C
		bool HandleEvent(CommandSmartUseEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000FCF85 File Offset: 0x000FB185
		bool HandleEvent(CommandTakeActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000FCF8E File Offset: 0x000FB18E
		bool HandleEvent(ConfigureMissileVisualEffectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000FCF97 File Offset: 0x000FB197
		bool HandleEvent(ContainsAnyBlueprintEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000FCFA0 File Offset: 0x000FB1A0
		bool HandleEvent(ContainsBlueprintEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000FCFA9 File Offset: 0x000FB1A9
		bool HandleEvent(ContainsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000FCFB2 File Offset: 0x000FB1B2
		bool HandleEvent(DamageConstantAdjustedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000FCFBB File Offset: 0x000FB1BB
		bool HandleEvent(DamageDieSizeAdjustedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000FCFC4 File Offset: 0x000FB1C4
		bool HandleEvent(DeathEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000FCFCD File Offset: 0x000FB1CD
		bool HandleEvent(DecorateDefaultEquipmentEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000FCFD6 File Offset: 0x000FB1D6
		bool HandleEvent(DefendMeleeHitEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000FCFDF File Offset: 0x000FB1DF
		bool HandleEvent(DefenderMissileHitEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000FCFE8 File Offset: 0x000FB1E8
		bool HandleEvent(DerivationCreatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000FCFF1 File Offset: 0x000FB1F1
		bool HandleEvent(DidInitialEquipEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000FCFFA File Offset: 0x000FB1FA
		bool HandleEvent(DidReequipEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000FD003 File Offset: 0x000FB203
		bool HandleEvent(DropOnDeathEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000FD00C File Offset: 0x000FB20C
		bool HandleEvent(DroppedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000FD015 File Offset: 0x000FB215
		bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000FD01E File Offset: 0x000FB21E
		bool HandleEvent(EarlyBeforeDeathRemovalEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000FD027 File Offset: 0x000FB227
		bool HandleEvent(EffectAppliedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000FD030 File Offset: 0x000FB230
		bool HandleEvent(EffectForceAppliedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000FD039 File Offset: 0x000FB239
		bool HandleEvent(EffectRemovedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000FD042 File Offset: 0x000FB242
		bool HandleEvent(EmbarkEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000FD04B File Offset: 0x000FB24B
		bool HandleEvent(EncounterChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000FD054 File Offset: 0x000FB254
		bool HandleEvent(EncumbranceChangedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000FD05D File Offset: 0x000FB25D
		bool HandleEvent(EndActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000FD066 File Offset: 0x000FB266
		bool HandleEvent(EndTurnEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000FD06F File Offset: 0x000FB26F
		bool HandleEvent(EnterCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000FD078 File Offset: 0x000FB278
		bool HandleEvent(EnteredCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000FD081 File Offset: 0x000FB281
		bool HandleEvent(EnteringCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000FD08A File Offset: 0x000FB28A
		bool HandleEvent(EnteringZoneEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000FD093 File Offset: 0x000FB293
		bool HandleEvent(EnvironmentalUpdateEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000FD09C File Offset: 0x000FB29C
		bool HandleEvent(EquippedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000FD0A5 File Offset: 0x000FB2A5
		bool HandleEvent(EquipperEquippedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000FD0AE File Offset: 0x000FB2AE
		bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000FD0B7 File Offset: 0x000FB2B7
		bool HandleEvent(ExamineFailureEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000FD0C0 File Offset: 0x000FB2C0
		bool HandleEvent(ExamineSuccessEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000FD0C9 File Offset: 0x000FB2C9
		bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000FD0D2 File Offset: 0x000FB2D2
		bool HandleEvent(ExtremitiesMovedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000FD0DB File Offset: 0x000FB2DB
		bool HandleEvent(FellDownEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000FD0E4 File Offset: 0x000FB2E4
		bool HandleEvent(FindObjectByIdEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000FD0ED File Offset: 0x000FB2ED
		bool HandleEvent(FinishChargeAvailableEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000FD0F6 File Offset: 0x000FB2F6
		bool HandleEvent(FinishRechargeAvailableEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000FD0FF File Offset: 0x000FB2FF
		bool HandleEvent(FlushWeightCacheEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000FD108 File Offset: 0x000FB308
		bool HandleEvent(ForceApplyEffectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000FD111 File Offset: 0x000FB311
		bool HandleEvent(FrozeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000FD11A File Offset: 0x000FB31A
		bool HandleEvent(GaveDirectionsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000FD123 File Offset: 0x000FB323
		bool HandleEvent(GeneralAmnestyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000FD12C File Offset: 0x000FB32C
		bool HandleEvent(GenericCommandEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000FD135 File Offset: 0x000FB335
		bool HandleEvent(GenericDeepNotifyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000FD13E File Offset: 0x000FB33E
		bool HandleEvent(GenericDeepQueryEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000FD147 File Offset: 0x000FB347
		bool HandleEvent(GenericDeepRatingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000FD150 File Offset: 0x000FB350
		bool HandleEvent(GenericNotifyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000FD159 File Offset: 0x000FB359
		bool HandleEvent(GenericQueryEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000FD162 File Offset: 0x000FB362
		bool HandleEvent(GenericRatingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000FD16B File Offset: 0x000FB36B
		bool HandleEvent(GetActivationPhaseEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000FD174 File Offset: 0x000FB374
		bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000FD17D File Offset: 0x000FB37D
		bool HandleEvent(GetAmbientLightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000FD186 File Offset: 0x000FB386
		bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000FD18F File Offset: 0x000FB38F
		bool HandleEvent(GetApparentSpeciesEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000FD198 File Offset: 0x000FB398
		bool HandleEvent(GetAttackerHitDiceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000FD1A1 File Offset: 0x000FB3A1
		bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000FD1AA File Offset: 0x000FB3AA
		bool HandleEvent(GetAutoCollectDramsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000FD1B3 File Offset: 0x000FB3B3
		bool HandleEvent(GetAutoEquipPriorityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000FD1BC File Offset: 0x000FB3BC
		bool HandleEvent(GetAvailableComputePowerEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000FD1C5 File Offset: 0x000FB3C5
		bool HandleEvent(GetBandagePerformanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000FD1CE File Offset: 0x000FB3CE
		bool HandleEvent(GetBleedLiquidEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000FD1D7 File Offset: 0x000FB3D7
		bool HandleEvent(GetCarriedWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000FD1E0 File Offset: 0x000FB3E0
		bool HandleEvent(GetCleaningItemsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000FD1E9 File Offset: 0x000FB3E9
		bool HandleEvent(GetCleaningItemsNearbyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000FD1F2 File Offset: 0x000FB3F2
		bool HandleEvent(GetCleaveAmountEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000FD1FB File Offset: 0x000FB3FB
		bool HandleEvent(GetCleaveMaxPenaltyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000FD204 File Offset: 0x000FB404
		bool HandleEvent(GetCompanionLimitEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000FD20D File Offset: 0x000FB40D
		bool HandleEvent(GetCompanionStatusEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000FD216 File Offset: 0x000FB416
		bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000FD21F File Offset: 0x000FB41F
		bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000FD228 File Offset: 0x000FB428
		bool HandleEvent(GetContentsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000FD231 File Offset: 0x000FB431
		bool HandleEvent(GetContextEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000FD23A File Offset: 0x000FB43A
		bool HandleEvent(GetCookingActionsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000FD243 File Offset: 0x000FB443
		bool HandleEvent(GetCooldownEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000FD24C File Offset: 0x000FB44C
		bool HandleEvent(GetCriticalThresholdEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000FD255 File Offset: 0x000FB455
		bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000FD25E File Offset: 0x000FB45E
		bool HandleEvent(GetDebugInternalsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000FD267 File Offset: 0x000FB467
		bool HandleEvent(GetDefenderHitDiceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000FD270 File Offset: 0x000FB470
		bool HandleEvent(GetDefenderMeleePenetrationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000FD279 File Offset: 0x000FB479
		bool HandleEvent(GetDifficultyEvaluationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000FD282 File Offset: 0x000FB482
		bool HandleEvent(GetDiseaseOnsetEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000FD28B File Offset: 0x000FB48B
		bool HandleEvent(GetDisplayNameEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000FD294 File Offset: 0x000FB494
		bool HandleEvent(GetDisplayNamePenetrationColorEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000FD29D File Offset: 0x000FB49D
		bool HandleEvent(GetDisplayStatBonusEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000FD2A6 File Offset: 0x000FB4A6
		bool HandleEvent(GetElectricalConductivityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000FD2AF File Offset: 0x000FB4AF
		bool HandleEvent(GetEnergyCostEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000FD2B8 File Offset: 0x000FB4B8
		bool HandleEvent(GetExtraPhysicalFeaturesEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000FD2C1 File Offset: 0x000FB4C1
		bool HandleEvent(GetExtrinsicValueEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000FD2CA File Offset: 0x000FB4CA
		bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000FD2D3 File Offset: 0x000FB4D3
		bool HandleEvent(GetFactionRankEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000FD2DC File Offset: 0x000FB4DC
		bool HandleEvent(GetFeelingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000FD2E5 File Offset: 0x000FB4E5
		bool HandleEvent(GetFirefightingPerformanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000FD2EE File Offset: 0x000FB4EE
		bool HandleEvent(GetFixedMissileSpreadEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000FD2F7 File Offset: 0x000FB4F7
		bool HandleEvent(GetFreeDramsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000FD300 File Offset: 0x000FB500
		bool HandleEvent(GetGameObjectSortEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000FD309 File Offset: 0x000FB509
		bool HandleEvent(GetGenderEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000FD312 File Offset: 0x000FB512
		bool HandleEvent(GetHostileWalkRadiusEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000FD31B File Offset: 0x000FB51B
		bool HandleEvent(GetHostilityRecognitionLimitsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000FD324 File Offset: 0x000FB524
		bool HandleEvent(GetIdealTemperatureEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000FD32D File Offset: 0x000FB52D
		bool HandleEvent(GetIntrinsicValueEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000FD336 File Offset: 0x000FB536
		bool HandleEvent(GetIntrinsicWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000FD33F File Offset: 0x000FB53F
		bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000FD348 File Offset: 0x000FB548
		bool HandleEvent(GetInventoryActionsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000FD351 File Offset: 0x000FB551
		bool HandleEvent(GetInventoryCategoryEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000FD35A File Offset: 0x000FB55A
		bool HandleEvent(GetItemElementsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000FD363 File Offset: 0x000FB563
		bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000FD36C File Offset: 0x000FB56C
		bool HandleEvent(GetKineticResistanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000FD375 File Offset: 0x000FB575
		bool HandleEvent(GetLevelUpDiceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000FD37E File Offset: 0x000FB57E
		bool HandleEvent(GetLevelUpPointsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000FD387 File Offset: 0x000FB587
		bool HandleEvent(GetLevelUpSkillPointsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000FD390 File Offset: 0x000FB590
		bool HandleEvent(GetLostChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000FD399 File Offset: 0x000FB599
		bool HandleEvent(GetMatterPhaseEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000FD3A2 File Offset: 0x000FB5A2
		bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000FD3AB File Offset: 0x000FB5AB
		bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000FD3B4 File Offset: 0x000FB5B4
		bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000FD3BD File Offset: 0x000FB5BD
		bool HandleEvent(GetMeleeAttacksEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000FD3C6 File Offset: 0x000FB5C6
		bool HandleEvent(GetMissileCoverPercentageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000FD3CF File Offset: 0x000FB5CF
		bool HandleEvent(GetMissileStatusColorEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000FD3D8 File Offset: 0x000FB5D8
		bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000FD3E1 File Offset: 0x000FB5E1
		bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000FD3EA File Offset: 0x000FB5EA
		bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000FD3F3 File Offset: 0x000FB5F3
		bool HandleEvent(GetModRarityWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000FD3FC File Offset: 0x000FB5FC
		bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000FD405 File Offset: 0x000FB605
		bool HandleEvent(GetMutationTermEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000FD40E File Offset: 0x000FB60E
		bool HandleEvent(GetNamingBestowalChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000FD417 File Offset: 0x000FB617
		bool HandleEvent(GetNamingChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000FD420 File Offset: 0x000FB620
		bool HandleEvent(GetNavigationWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000FD429 File Offset: 0x000FB629
		bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000FD432 File Offset: 0x000FB632
		bool HandleEvent(GetOverloadChargeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000FD43B File Offset: 0x000FB63B
		bool HandleEvent(GetPartyLeaderFollowDistanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000FD444 File Offset: 0x000FB644
		bool HandleEvent(GetPointsOfInterestEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000FD44D File Offset: 0x000FB64D
		bool HandleEvent(GetPowerLoadLevelEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000FD456 File Offset: 0x000FB656
		bool HandleEvent(GetPrecognitionRestoreGameStateEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000FD45F File Offset: 0x000FB65F
		bool HandleEvent(GetPreferredLiquidEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000FD468 File Offset: 0x000FB668
		bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000FD471 File Offset: 0x000FB671
		bool HandleEvent(GetProjectileObjectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000FD47A File Offset: 0x000FB67A
		bool HandleEvent(GetPropertyModDescription E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000FD483 File Offset: 0x000FB683
		bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000FD48C File Offset: 0x000FB68C
		bool HandleEvent(GetPsychicGlimmerEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000FD495 File Offset: 0x000FB695
		bool HandleEvent(GetRandomBuyChimericBodyPartRollsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000FD49E File Offset: 0x000FB69E
		bool HandleEvent(GetRandomBuyMutationCountEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000FD4A7 File Offset: 0x000FB6A7
		bool HandleEvent(GetRealityStabilizationPenetrationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000FD4B0 File Offset: 0x000FB6B0
		bool HandleEvent(GetRebukeLevelEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000FD4B9 File Offset: 0x000FB6B9
		bool HandleEvent(GetRecoilersEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000FD4C2 File Offset: 0x000FB6C2
		bool HandleEvent(GetReplaceCellInteractionsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000FD4CB File Offset: 0x000FB6CB
		bool HandleEvent(GetRespiratoryAgentPerformanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000FD4D4 File Offset: 0x000FB6D4
		bool HandleEvent(GetRitualSifrahSetupEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000FD4DD File Offset: 0x000FB6DD
		bool HandleEvent(GetRunningBehaviorEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000FD4E6 File Offset: 0x000FB6E6
		bool HandleEvent(GetScanTypeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000FD4EF File Offset: 0x000FB6EF
		bool HandleEvent(GetShieldBlockPreferenceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000FD4F8 File Offset: 0x000FB6F8
		bool HandleEvent(GetShieldSlamDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000FD501 File Offset: 0x000FB701
		bool HandleEvent(GetShortDescriptionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000FD50A File Offset: 0x000FB70A
		bool HandleEvent(GetSkillEffectChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000FD513 File Offset: 0x000FB713
		bool HandleEvent(GetSlotsRequiredEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000FD51C File Offset: 0x000FB71C
		bool HandleEvent(GetSlottedInventoryActionsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000FD525 File Offset: 0x000FB725
		bool HandleEvent(GetSocialSifrahSetupEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000FD52E File Offset: 0x000FB72E
		bool HandleEvent(GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000FD537 File Offset: 0x000FB737
		bool HandleEvent(GetSpaceTimeAnomalyEmergencePermillageChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000FD540 File Offset: 0x000FB740
		bool HandleEvent(GetSpecialEffectChanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000FD549 File Offset: 0x000FB749
		bool HandleEvent(GetSpringinessEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000FD552 File Offset: 0x000FB752
		bool HandleEvent(GetSprintDurationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000FD55B File Offset: 0x000FB75B
		bool HandleEvent(GetStorableDramsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000FD564 File Offset: 0x000FB764
		bool HandleEvent(GetSwimmingPerformanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000FD56D File Offset: 0x000FB76D
		bool HandleEvent(GetThrowProfileEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000FD576 File Offset: 0x000FB776
		bool HandleEvent(GetThrownWeaponFlexPhaseProviderEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000FD57F File Offset: 0x000FB77F
		bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000FD588 File Offset: 0x000FB788
		bool HandleEvent(GetThrownWeaponRangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000FD591 File Offset: 0x000FB791
		bool HandleEvent(GetTinkeringBonusEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000FD59A File Offset: 0x000FB79A
		bool HandleEvent(GetToHitModifierEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000FD5A3 File Offset: 0x000FB7A3
		bool HandleEvent(GetTonicCapacityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000FD5AC File Offset: 0x000FB7AC
		bool HandleEvent(GetTonicDosageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000FD5B5 File Offset: 0x000FB7B5
		bool HandleEvent(GetTonicDurationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000FD5BE File Offset: 0x000FB7BE
		bool HandleEvent(GetTradePerformanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000FD5C7 File Offset: 0x000FB7C7
		bool HandleEvent(GetTransitiveLocationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000FD5D0 File Offset: 0x000FB7D0
		bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000FD5D9 File Offset: 0x000FB7D9
		bool HandleEvent(GetUtilityScoreEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000FD5E2 File Offset: 0x000FB7E2
		bool HandleEvent(GetWadingPerformanceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000FD5EB File Offset: 0x000FB7EB
		bool HandleEvent(GetWaterRitualCostEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000FD5F4 File Offset: 0x000FB7F4
		bool HandleEvent(GetWaterRitualLiquidEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000FD5FD File Offset: 0x000FB7FD
		bool HandleEvent(GetWaterRitualReputationAmountEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000FD606 File Offset: 0x000FB806
		bool HandleEvent(GetWaterRitualSecretWeightEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000FD60F File Offset: 0x000FB80F
		bool HandleEvent(GetWaterRitualSellSecretBehaviorEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000FD618 File Offset: 0x000FB818
		bool HandleEvent(GetWeaponHitDiceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000FD621 File Offset: 0x000FB821
		bool HandleEvent(GetWeaponMeleeAttacksEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000FD62A File Offset: 0x000FB82A
		bool HandleEvent(GetWeaponMeleePenetrationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000FD633 File Offset: 0x000FB833
		bool HandleEvent(GetZoneEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000FD63C File Offset: 0x000FB83C
		bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000FD645 File Offset: 0x000FB845
		bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000FD64E File Offset: 0x000FB84E
		bool HandleEvent(GiveDramsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000FD657 File Offset: 0x000FB857
		bool HandleEvent(GlimmerChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000FD660 File Offset: 0x000FB860
		bool HandleEvent(GravitationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000FD669 File Offset: 0x000FB869
		bool HandleEvent(HasBeenReadEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000FD672 File Offset: 0x000FB872
		bool HandleEvent(HasBlueprintEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000FD67B File Offset: 0x000FB87B
		bool HandleEvent(HasFlammableEquipmentEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000FD684 File Offset: 0x000FB884
		bool HandleEvent(HasFlammableEquipmentOrInventoryEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000FD68D File Offset: 0x000FB88D
		bool HandleEvent(HealsNaturallyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000FD696 File Offset: 0x000FB896
		bool HandleEvent(IAICommandListEvent E)
		{
			return true;
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000FD699 File Offset: 0x000FB899
		bool HandleEvent(IAIItemCommandListEvent E)
		{
			return true;
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000FD69C File Offset: 0x000FB89C
		bool HandleEvent(IAIMoveCommandListEvent E)
		{
			return true;
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000FD69F File Offset: 0x000FB89F
		bool HandleEvent(IActOnItemEvent E)
		{
			return true;
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000FD6A2 File Offset: 0x000FB8A2
		bool HandleEvent(IActualEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000FD6A5 File Offset: 0x000FB8A5
		bool HandleEvent(IAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000FD6A8 File Offset: 0x000FB8A8
		bool HandleEvent(IAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000FD6AB File Offset: 0x000FB8AB
		bool HandleEvent(IBootSequenceEvent E)
		{
			return true;
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000FD6AE File Offset: 0x000FB8AE
		bool HandleEvent(IChargeConsumptionEvent E)
		{
			return true;
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000FD6B1 File Offset: 0x000FB8B1
		bool HandleEvent(IChargeEvent E)
		{
			return true;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000FD6B4 File Offset: 0x000FB8B4
		bool HandleEvent(IChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000FD6B7 File Offset: 0x000FB8B7
		bool HandleEvent(IChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000FD6BA File Offset: 0x000FB8BA
		bool HandleEvent(IConsumeEvent E)
		{
			return true;
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000FD6BD File Offset: 0x000FB8BD
		bool HandleEvent(IConversationMinEvent E)
		{
			return true;
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000FD6C0 File Offset: 0x000FB8C0
		bool HandleEvent(IDamageEvent E)
		{
			return true;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000FD6C3 File Offset: 0x000FB8C3
		bool HandleEvent(IDeathEvent E)
		{
			return true;
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000FD6C6 File Offset: 0x000FB8C6
		bool HandleEvent(IDerivationEvent E)
		{
			return true;
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000FD6C9 File Offset: 0x000FB8C9
		bool HandleEvent(IDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000FD6CC File Offset: 0x000FB8CC
		bool HandleEvent(IEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000FD6CF File Offset: 0x000FB8CF
		bool HandleEvent(IExamineEvent E)
		{
			return true;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000FD6D2 File Offset: 0x000FB8D2
		bool HandleEvent(IFinalChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000FD6D5 File Offset: 0x000FB8D5
		bool HandleEvent(IHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000FD6D8 File Offset: 0x000FB8D8
		bool HandleEvent(IInitialChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000FD6DB File Offset: 0x000FB8DB
		bool HandleEvent(IInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000FD6DE File Offset: 0x000FB8DE
		bool HandleEvent(ILimbRegenerationEvent E)
		{
			return true;
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000FD6E1 File Offset: 0x000FB8E1
		bool HandleEvent(ILiquidEvent E)
		{
			return true;
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000FD6E4 File Offset: 0x000FB8E4
		bool HandleEvent(IMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000FD6E7 File Offset: 0x000FB8E7
		bool HandleEvent(IMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000FD6EA File Offset: 0x000FB8EA
		bool HandleEvent(IMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000FD6ED File Offset: 0x000FB8ED
		bool HandleEvent(INavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000FD6F0 File Offset: 0x000FB8F0
		bool HandleEvent(IObjectCellInteractionEvent E)
		{
			return true;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000FD6F3 File Offset: 0x000FB8F3
		bool HandleEvent(IObjectCreationEvent E)
		{
			return true;
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000FD6F6 File Offset: 0x000FB8F6
		bool HandleEvent(IObjectInventoryInteractionEvent E)
		{
			return true;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000FD6F9 File Offset: 0x000FB8F9
		bool HandleEvent(IQuestEvent E)
		{
			return true;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000FD6FC File Offset: 0x000FB8FC
		bool HandleEvent(IRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000FD6FF File Offset: 0x000FB8FF
		bool HandleEvent(IReplicationEvent E)
		{
			return true;
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000FD702 File Offset: 0x000FB902
		bool HandleEvent(ISaveEvent E)
		{
			return true;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000FD705 File Offset: 0x000FB905
		bool HandleEvent(IShallowZoneEvent E)
		{
			return true;
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000FD708 File Offset: 0x000FB908
		bool HandleEvent(IShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000FD70B File Offset: 0x000FB90B
		bool HandleEvent(ITravelEvent E)
		{
			return true;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000FD70E File Offset: 0x000FB90E
		bool HandleEvent(IValueEvent E)
		{
			return true;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000FD711 File Offset: 0x000FB911
		bool HandleEvent(IWeightEvent E)
		{
			return true;
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000FD714 File Offset: 0x000FB914
		bool HandleEvent(IXPEvent E)
		{
			return true;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000FD717 File Offset: 0x000FB917
		bool HandleEvent(IZoneEvent E)
		{
			return true;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000FD71A File Offset: 0x000FB91A
		bool HandleEvent(IZoneOnlyEvent E)
		{
			return true;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000FD71D File Offset: 0x000FB91D
		bool HandleEvent(IdleQueryEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000FD726 File Offset: 0x000FB926
		bool HandleEvent(ImplantAddedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000FD72F File Offset: 0x000FB92F
		bool HandleEvent(ImplantRemovedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000FD738 File Offset: 0x000FB938
		bool HandleEvent(ImplantedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000FD741 File Offset: 0x000FB941
		bool HandleEvent(InduceVomitingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000FD74A File Offset: 0x000FB94A
		bool HandleEvent(InductionChargeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000FD753 File Offset: 0x000FB953
		bool HandleEvent(InteriorZoneBuiltEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000FD75C File Offset: 0x000FB95C
		bool HandleEvent(InterruptAutowalkEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000FD765 File Offset: 0x000FB965
		bool HandleEvent(InventoryActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000FD76E File Offset: 0x000FB96E
		bool HandleEvent(IsAdaptivePenetrationActiveEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000FD777 File Offset: 0x000FB977
		bool HandleEvent(IsAfflictionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000FD780 File Offset: 0x000FB980
		bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000FD789 File Offset: 0x000FB989
		bool HandleEvent(IsExplosiveEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000FD792 File Offset: 0x000FB992
		bool HandleEvent(IsMutantEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000FD79B File Offset: 0x000FB99B
		bool HandleEvent(IsOverloadableEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000FD7A4 File Offset: 0x000FB9A4
		bool HandleEvent(IsRepairableEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000FD7AD File Offset: 0x000FB9AD
		bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000FD7B6 File Offset: 0x000FB9B6
		bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000FD7BF File Offset: 0x000FB9BF
		bool HandleEvent(IsTrueKinEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000FD7C8 File Offset: 0x000FB9C8
		bool HandleEvent(IsVehicleOperationalEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000FD7D1 File Offset: 0x000FB9D1
		bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000FD7DA File Offset: 0x000FB9DA
		bool HandleEvent(JoinedPartyLeaderEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000FD7E3 File Offset: 0x000FB9E3
		bool HandleEvent(JumpedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000FD7EC File Offset: 0x000FB9EC
		bool HandleEvent(KilledEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000FD7F5 File Offset: 0x000FB9F5
		bool HandleEvent(KilledPlayerEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000FD7FE File Offset: 0x000FB9FE
		bool HandleEvent(LateBeforeApplyDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000FD807 File Offset: 0x000FBA07
		bool HandleEvent(LeaveCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000FD810 File Offset: 0x000FBA10
		bool HandleEvent(LeavingCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000FD819 File Offset: 0x000FBA19
		bool HandleEvent(LeftCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000FD822 File Offset: 0x000FBA22
		bool HandleEvent(LiquidMixedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000FD82B File Offset: 0x000FBA2B
		bool HandleEvent(LoadAmmoEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000FD834 File Offset: 0x000FBA34
		bool HandleEvent(MakeTemporaryEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000FD83D File Offset: 0x000FBA3D
		bool HandleEvent(MentalAttackEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000FD846 File Offset: 0x000FBA46
		bool HandleEvent(MissilePenetrateEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000FD84F File Offset: 0x000FBA4F
		bool HandleEvent(MissileTraversingCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000FD858 File Offset: 0x000FBA58
		bool HandleEvent(ModificationAppliedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000FD861 File Offset: 0x000FBA61
		bool HandleEvent(ModifyAttackingSaveEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000FD86A File Offset: 0x000FBA6A
		bool HandleEvent(ModifyBitCostEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000FD873 File Offset: 0x000FBA73
		bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000FD87C File Offset: 0x000FBA7C
		bool HandleEvent(ModifyOriginatingSaveEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000FD885 File Offset: 0x000FBA85
		bool HandleEvent(MovementModeChangedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000FD88E File Offset: 0x000FBA8E
		bool HandleEvent(MutationsSubjectToEMPEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000FD897 File Offset: 0x000FBA97
		bool HandleEvent(NeedPartSupportEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000FD8A0 File Offset: 0x000FBAA0
		bool HandleEvent(NeedsReloadEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000FD8A9 File Offset: 0x000FBAA9
		bool HandleEvent(NeutronFluxPourExplodesEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000FD8B2 File Offset: 0x000FBAB2
		bool HandleEvent(NotifyTargetImmuneEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000FD8BB File Offset: 0x000FBABB
		bool HandleEvent(ObjectCreatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000FD8C4 File Offset: 0x000FBAC4
		bool HandleEvent(ObjectEnteredCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000FD8CD File Offset: 0x000FBACD
		bool HandleEvent(ObjectEnteringCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000FD8D6 File Offset: 0x000FBAD6
		bool HandleEvent(ObjectGoingProneEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000FD8DF File Offset: 0x000FBADF
		bool HandleEvent(ObjectLeavingCellEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000FD8E8 File Offset: 0x000FBAE8
		bool HandleEvent(ObjectStartedFlyingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000FD8F1 File Offset: 0x000FBAF1
		bool HandleEvent(ObjectStoppedFlyingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000FD8FA File Offset: 0x000FBAFA
		bool HandleEvent(OkayToDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000FD903 File Offset: 0x000FBB03
		bool HandleEvent(OnDeathRemovalEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000FD90C File Offset: 0x000FBB0C
		bool HandleEvent(OnDestroyObjectEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000FD915 File Offset: 0x000FBB15
		bool HandleEvent(OwnerAfterInventoryActionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000FD91E File Offset: 0x000FBB1E
		bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000FD927 File Offset: 0x000FBB27
		bool HandleEvent(OwnerGetShortDescriptionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000FD930 File Offset: 0x000FBB30
		bool HandleEvent(OwnerGetUnknownShortDescriptionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000FD939 File Offset: 0x000FBB39
		bool HandleEvent(PartAddedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000FD942 File Offset: 0x000FBB42
		bool HandleEvent(PartRemovedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000FD94B File Offset: 0x000FBB4B
		bool HandleEvent(PartSupportEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000FD954 File Offset: 0x000FBB54
		bool HandleEvent(PathAsBurrowerEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000FD95D File Offset: 0x000FBB5D
		bool HandleEvent(PhysicalContactEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000FD966 File Offset: 0x000FBB66
		bool HandleEvent(PointDefenseInterceptEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000FD96F File Offset: 0x000FBB6F
		bool HandleEvent(PollForHealingLocationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000FD978 File Offset: 0x000FBB78
		bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000FD981 File Offset: 0x000FBB81
		bool HandleEvent(PowerUpdatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000FD98A File Offset: 0x000FBB8A
		bool HandleEvent(PreferDefaultBehaviorEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000FD993 File Offset: 0x000FBB93
		bool HandleEvent(PreferTargetEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000FD99C File Offset: 0x000FBB9C
		bool HandleEvent(PreventSmartUseEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000FD9A5 File Offset: 0x000FBBA5
		bool HandleEvent(PrimePowerSystemsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000FD9AE File Offset: 0x000FBBAE
		bool HandleEvent(ProducesLiquidEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000FD9B7 File Offset: 0x000FBBB7
		bool HandleEvent(ProjectileMovingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000FD9C0 File Offset: 0x000FBBC0
		bool HandleEvent(QueryBroadcastDrawEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000FD9C9 File Offset: 0x000FBBC9
		bool HandleEvent(QueryChargeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000FD9D2 File Offset: 0x000FBBD2
		bool HandleEvent(QueryChargeProductionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000FD9DB File Offset: 0x000FBBDB
		bool HandleEvent(QueryChargeStorageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000FD9E4 File Offset: 0x000FBBE4
		bool HandleEvent(QueryDrawEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000FD9ED File Offset: 0x000FBBED
		bool HandleEvent(QueryEquippableListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000FD9F6 File Offset: 0x000FBBF6
		bool HandleEvent(QueryInductionChargeStorageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000FD9FF File Offset: 0x000FBBFF
		bool HandleEvent(QueryRechargeStorageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000FDA08 File Offset: 0x000FBC08
		bool HandleEvent(QuerySlotListEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000FDA11 File Offset: 0x000FBC11
		bool HandleEvent(QuestFinishedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000FDA1A File Offset: 0x000FBC1A
		bool HandleEvent(QuestStartedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000FDA23 File Offset: 0x000FBC23
		bool HandleEvent(QuestStepFinishedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000FDA2C File Offset: 0x000FBC2C
		bool HandleEvent(RadiatesHeatAdjacentEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000FDA35 File Offset: 0x000FBC35
		bool HandleEvent(RadiatesHeatEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000FDA3E File Offset: 0x000FBC3E
		bool HandleEvent(RealityStabilizeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000FDA47 File Offset: 0x000FBC47
		bool HandleEvent(RechargeAvailableEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000FDA50 File Offset: 0x000FBC50
		bool HandleEvent(RefreshTileEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000FDA59 File Offset: 0x000FBC59
		bool HandleEvent(RegenerateDefaultEquipmentEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000FDA62 File Offset: 0x000FBC62
		bool HandleEvent(RegenerateLimbEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000FDA6B File Offset: 0x000FBC6B
		bool HandleEvent(RemoveFromContextEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000FDA74 File Offset: 0x000FBC74
		bool HandleEvent(RepaintedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000FDA7D File Offset: 0x000FBC7D
		bool HandleEvent(RepairCriticalFailureEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000FDA86 File Offset: 0x000FBC86
		bool HandleEvent(RepairedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000FDA8F File Offset: 0x000FBC8F
		bool HandleEvent(ReplaceInContextEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000FDA98 File Offset: 0x000FBC98
		bool HandleEvent(ReplaceThrownWeaponEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000FDAA1 File Offset: 0x000FBCA1
		bool HandleEvent(ReplicaCreatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000FDAAA File Offset: 0x000FBCAA
		bool HandleEvent(ReputationChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000FDAB3 File Offset: 0x000FBCB3
		bool HandleEvent(RespiresEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000FDABC File Offset: 0x000FBCBC
		bool HandleEvent(SecretVisibilityChangedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000FDAC5 File Offset: 0x000FBCC5
		bool HandleEvent(ShieldSlamPerformedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000FDACE File Offset: 0x000FBCCE
		bool HandleEvent(ShotCompleteEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000FDAD7 File Offset: 0x000FBCD7
		bool HandleEvent(ShouldAttackToReachTargetEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000FDAE0 File Offset: 0x000FBCE0
		bool HandleEvent(ShouldDescribeStatBonusEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000FDAE9 File Offset: 0x000FBCE9
		bool HandleEvent(SlamEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000FDAF2 File Offset: 0x000FBCF2
		bool HandleEvent(StackCountChangedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000FDAFB File Offset: 0x000FBCFB
		bool HandleEvent(StartTradeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000FDB04 File Offset: 0x000FBD04
		bool HandleEvent(StatChangeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000FDB0D File Offset: 0x000FBD0D
		bool HandleEvent(StockedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000FDB16 File Offset: 0x000FBD16
		bool HandleEvent(StripContentsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000FDB1F File Offset: 0x000FBD1F
		bool HandleEvent(SubjectToGravityEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000FDB28 File Offset: 0x000FBD28
		bool HandleEvent(SuspendingEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000FDB31 File Offset: 0x000FBD31
		bool HandleEvent(SyncMutationLevelsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000FDB3A File Offset: 0x000FBD3A
		bool HandleEvent(SyncRenderEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000FDB43 File Offset: 0x000FBD43
		bool HandleEvent(SynchronizeExistenceEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000FDB4C File Offset: 0x000FBD4C
		bool HandleEvent(TakeOnRoleEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000FDB55 File Offset: 0x000FBD55
		bool HandleEvent(TakenEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000FDB5E File Offset: 0x000FBD5E
		bool HandleEvent(TestChargeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000FDB67 File Offset: 0x000FBD67
		bool HandleEvent(ThawedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000FDB70 File Offset: 0x000FBD70
		bool HandleEvent(TookDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000FDB79 File Offset: 0x000FBD79
		bool HandleEvent(TookEnvironmentalDamageEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000FDB82 File Offset: 0x000FBD82
		bool HandleEvent(TookEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000FDB8B File Offset: 0x000FBD8B
		bool HandleEvent(TransparentToEMPEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000FDB94 File Offset: 0x000FBD94
		bool HandleEvent(TravelSpeedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000FDB9D File Offset: 0x000FBD9D
		bool HandleEvent(TriggersMakersMarkCreationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000FDBA6 File Offset: 0x000FBDA6
		bool HandleEvent(TryRemoveFromContextEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000FDBAF File Offset: 0x000FBDAF
		bool HandleEvent(UnequippedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000FDBB8 File Offset: 0x000FBDB8
		bool HandleEvent(UnimplantedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000FDBC1 File Offset: 0x000FBDC1
		bool HandleEvent(UseChargeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000FDBCA File Offset: 0x000FBDCA
		bool HandleEvent(UseDramsEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000FDBD3 File Offset: 0x000FBDD3
		bool HandleEvent(UseEnergyEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000FDBDC File Offset: 0x000FBDDC
		bool HandleEvent(UseHealingLocationEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000FDBE5 File Offset: 0x000FBDE5
		bool HandleEvent(UsingChargeEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000FDBEE File Offset: 0x000FBDEE
		bool HandleEvent(VaporizedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000FDBF7 File Offset: 0x000FBDF7
		bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000FDC00 File Offset: 0x000FBE00
		bool HandleEvent(WasDerivedFromEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000FDC09 File Offset: 0x000FBE09
		bool HandleEvent(WasReplicatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000FDC12 File Offset: 0x000FBE12
		bool HandleEvent(WaterRitualStartEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000FDC1B File Offset: 0x000FBE1B
		bool HandleEvent(WorshipPerformedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000FDC24 File Offset: 0x000FBE24
		bool HandleEvent(ZoneActivatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000FDC2D File Offset: 0x000FBE2D
		bool HandleEvent(ZoneBuiltEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000FDC36 File Offset: 0x000FBE36
		bool HandleEvent(ZoneDeactivatedEvent E)
		{
			return this.HandleEvent(E);
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000FDC3F File Offset: 0x000FBE3F
		bool HandleEvent(ZoneThawedEvent E)
		{
			return this.HandleEvent(E);
		}

		/// <inheritdoc cref="M:XRL.Collections.EventRegistry.Register(XRL.IEventHandler,System.Int32,System.Int32,System.Boolean)" />
		// Token: 0x0600325C RID: 12892 RVA: 0x000FDC48 File Offset: 0x000FBE48
		void RegisterEvent(IEventHandler Handler, int EventID, int Order = 0, bool Serialize = false)
		{
		}

		/// <inheritdoc cref="M:XRL.Collections.EventRegistry.Unregister(XRL.IEventHandler,System.Int32)" />
		// Token: 0x0600325D RID: 12893 RVA: 0x000FDC4A File Offset: 0x000FBE4A
		void UnregisterEvent(IEventHandler Handler, int EventID)
		{
		}
	}
}
