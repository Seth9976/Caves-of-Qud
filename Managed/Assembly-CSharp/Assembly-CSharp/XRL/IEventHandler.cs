using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004B4 RID: 1204
	public interface IEventHandler
	{
		// Token: 0x06002DEA RID: 11754 RVA: 0x000FC2BD File Offset: 0x000FA4BD
		bool HandleEvent(AIAfterMissileEvent E)
		{
			return true;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000FC2C0 File Offset: 0x000FA4C0
		bool HandleEvent(AIAfterThrowEvent E)
		{
			return true;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000FC2C3 File Offset: 0x000FA4C3
		bool HandleEvent(AIBoredEvent E)
		{
			return true;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000FC2C6 File Offset: 0x000FA4C6
		bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000FC2C9 File Offset: 0x000FA4C9
		bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000FC2CC File Offset: 0x000FA4CC
		bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000FC2CF File Offset: 0x000FA4CF
		bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000FC2D2 File Offset: 0x000FA4D2
		bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000FC2D5 File Offset: 0x000FA4D5
		bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000FC2D8 File Offset: 0x000FA4D8
		bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000FC2DB File Offset: 0x000FA4DB
		bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000FC2DE File Offset: 0x000FA4DE
		bool HandleEvent(AIHelpBroadcastEvent E)
		{
			return true;
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000FC2E1 File Offset: 0x000FA4E1
		bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return true;
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000FC2E4 File Offset: 0x000FA4E4
		bool HandleEvent(ActorGetNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000FC2E7 File Offset: 0x000FA4E7
		bool HandleEvent(AddedToInventoryEvent E)
		{
			return true;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000FC2EA File Offset: 0x000FA4EA
		bool HandleEvent(AdjustTotalWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000FC2ED File Offset: 0x000FA4ED
		bool HandleEvent(AdjustValueEvent E)
		{
			return true;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000FC2F0 File Offset: 0x000FA4F0
		bool HandleEvent(AdjustVisibilityRadiusEvent E)
		{
			return true;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000FC2F3 File Offset: 0x000FA4F3
		bool HandleEvent(AdjustWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000FC2F6 File Offset: 0x000FA4F6
		bool HandleEvent(AfterAddActiveObjectEvent E)
		{
			return true;
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000FC2F9 File Offset: 0x000FA4F9
		bool HandleEvent(AfterAddOpinionEvent E)
		{
			return true;
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000FC2FC File Offset: 0x000FA4FC
		bool HandleEvent(AfterAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000FC2FF File Offset: 0x000FA4FF
		bool HandleEvent(AfterAfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000FC302 File Offset: 0x000FA502
		bool HandleEvent(AfterBasicBestowalEvent E)
		{
			return true;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000FC305 File Offset: 0x000FA505
		bool HandleEvent(AfterChangePartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000FC308 File Offset: 0x000FA508
		bool HandleEvent(AfterConsumeEvent E)
		{
			return true;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000FC30B File Offset: 0x000FA50B
		bool HandleEvent(AfterContentsTakenEvent E)
		{
			return true;
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000FC30E File Offset: 0x000FA50E
		bool HandleEvent(AfterConversationEvent E)
		{
			return true;
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000FC311 File Offset: 0x000FA511
		bool HandleEvent(AfterDieEvent E)
		{
			return true;
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000FC314 File Offset: 0x000FA514
		bool HandleEvent(AfterDismemberEvent E)
		{
			return true;
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000FC317 File Offset: 0x000FA517
		bool HandleEvent(AfterElementBestowalEvent E)
		{
			return true;
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000FC31A File Offset: 0x000FA51A
		bool HandleEvent(AfterExamineCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000FC31D File Offset: 0x000FA51D
		bool HandleEvent(AfterExamineFailureEvent E)
		{
			return true;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000FC320 File Offset: 0x000FA520
		bool HandleEvent(AfterGameLoadedEvent E)
		{
			return true;
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000FC323 File Offset: 0x000FA523
		bool HandleEvent(AfterInventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000FC326 File Offset: 0x000FA526
		bool HandleEvent(AfterLevelGainedEvent E)
		{
			return true;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000FC329 File Offset: 0x000FA529
		bool HandleEvent(AfterMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000FC32C File Offset: 0x000FA52C
		bool HandleEvent(AfterMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000FC32F File Offset: 0x000FA52F
		bool HandleEvent(AfterMoveFailedEvent E)
		{
			return true;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000FC332 File Offset: 0x000FA532
		bool HandleEvent(AfterObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000FC335 File Offset: 0x000FA535
		bool HandleEvent(AfterPetEvent E)
		{
			return true;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000FC338 File Offset: 0x000FA538
		bool HandleEvent(AfterPilotChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000FC33B File Offset: 0x000FA53B
		bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000FC33E File Offset: 0x000FA53E
		bool HandleEvent(AfterPseudoRelicGeneratedEvent E)
		{
			return true;
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000FC341 File Offset: 0x000FA541
		bool HandleEvent(AfterReadBookEvent E)
		{
			return true;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000FC344 File Offset: 0x000FA544
		bool HandleEvent(AfterRelicGeneratedEvent E)
		{
			return true;
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000FC347 File Offset: 0x000FA547
		bool HandleEvent(AfterRemoveActiveObjectEvent E)
		{
			return true;
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000FC34A File Offset: 0x000FA54A
		bool HandleEvent(AfterRemoveSkillEvent E)
		{
			return true;
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000FC34D File Offset: 0x000FA54D
		bool HandleEvent(AfterReputationChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000FC350 File Offset: 0x000FA550
		bool HandleEvent(AfterShieldBlockEvent E)
		{
			return true;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000FC353 File Offset: 0x000FA553
		bool HandleEvent(AfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000FC356 File Offset: 0x000FA556
		bool HandleEvent(AfterTravelEvent E)
		{
			return true;
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000FC359 File Offset: 0x000FA559
		bool HandleEvent(AfterZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000FC35C File Offset: 0x000FA55C
		bool HandleEvent(AllowInventoryStackEvent E)
		{
			return true;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000FC35F File Offset: 0x000FA55F
		bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return true;
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000FC362 File Offset: 0x000FA562
		bool HandleEvent(AllowPolypPluckingEvent E)
		{
			return true;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000FC365 File Offset: 0x000FA565
		bool HandleEvent(AllowTradeWithNoInventoryEvent E)
		{
			return true;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000FC368 File Offset: 0x000FA568
		bool HandleEvent(AnimateEvent E)
		{
			return true;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000FC36B File Offset: 0x000FA56B
		bool HandleEvent(AnyAutoCollectDramsEvent E)
		{
			return true;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000FC36E File Offset: 0x000FA56E
		bool HandleEvent(AnyRegenerableLimbsEvent E)
		{
			return true;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000FC371 File Offset: 0x000FA571
		bool HandleEvent(ApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000FC374 File Offset: 0x000FA574
		bool HandleEvent(AttackerBeforeTemperatureChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000FC377 File Offset: 0x000FA577
		bool HandleEvent(AttackerDealingDamageEvent E)
		{
			return true;
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000FC37A File Offset: 0x000FA57A
		bool HandleEvent(AttackerDealtDamageEvent E)
		{
			return true;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000FC37D File Offset: 0x000FA57D
		bool HandleEvent(AttemptConversationEvent E)
		{
			return true;
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000FC380 File Offset: 0x000FA580
		bool HandleEvent(AttemptToLandEvent E)
		{
			return true;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000FC383 File Offset: 0x000FA583
		bool HandleEvent(AutoexploreObjectEvent E)
		{
			return true;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000FC386 File Offset: 0x000FA586
		bool HandleEvent(AwardXPEvent E)
		{
			return true;
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000FC389 File Offset: 0x000FA589
		bool HandleEvent(AwardedXPEvent E)
		{
			return true;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000FC38C File Offset: 0x000FA58C
		bool HandleEvent(AwardingXPEvent E)
		{
			return true;
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000FC38F File Offset: 0x000FA58F
		bool HandleEvent(BeforeAITakingActionEvent E)
		{
			return true;
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000FC392 File Offset: 0x000FA592
		bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			return true;
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000FC395 File Offset: 0x000FA595
		bool HandleEvent(BeforeAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000FC398 File Offset: 0x000FA598
		bool HandleEvent(BeforeAfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000FC39B File Offset: 0x000FA59B
		bool HandleEvent(BeforeApplyDamageEvent E)
		{
			return true;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000FC39E File Offset: 0x000FA59E
		bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000FC3A1 File Offset: 0x000FA5A1
		bool HandleEvent(BeforeConsumeEvent E)
		{
			return true;
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000FC3A4 File Offset: 0x000FA5A4
		bool HandleEvent(BeforeConversationEvent E)
		{
			return true;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000FC3A7 File Offset: 0x000FA5A7
		bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000FC3AA File Offset: 0x000FA5AA
		bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000FC3AD File Offset: 0x000FA5AD
		bool HandleEvent(BeforeDetonateEvent E)
		{
			return true;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000FC3B0 File Offset: 0x000FA5B0
		bool HandleEvent(BeforeDieEvent E)
		{
			return true;
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000FC3B3 File Offset: 0x000FA5B3
		bool HandleEvent(BeforeDismemberEvent E)
		{
			return true;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000FC3B6 File Offset: 0x000FA5B6
		bool HandleEvent(BeforeFireMissileWeaponsEvent E)
		{
			return true;
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000FC3B9 File Offset: 0x000FA5B9
		bool HandleEvent(BeforeInteriorCollapseEvent E)
		{
			return true;
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000FC3BC File Offset: 0x000FA5BC
		bool HandleEvent(BeforeLevelGainedEvent E)
		{
			return true;
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000FC3BF File Offset: 0x000FA5BF
		bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000FC3C2 File Offset: 0x000FA5C2
		bool HandleEvent(BeforeMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000FC3C5 File Offset: 0x000FA5C5
		bool HandleEvent(BeforeMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000FC3C8 File Offset: 0x000FA5C8
		bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000FC3CB File Offset: 0x000FA5CB
		bool HandleEvent(BeforePilotChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000FC3CE File Offset: 0x000FA5CE
		bool HandleEvent(BeforePlayMusicEvent E)
		{
			return true;
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000FC3D1 File Offset: 0x000FA5D1
		bool HandleEvent(BeforeProjectileHitEvent E)
		{
			return true;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000FC3D4 File Offset: 0x000FA5D4
		bool HandleEvent(BeforePullDownEvent E)
		{
			return true;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000FC3D7 File Offset: 0x000FA5D7
		bool HandleEvent(BeforeRemoveSkillEvent E)
		{
			return true;
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000FC3DA File Offset: 0x000FA5DA
		bool HandleEvent(BeforeRenderEvent E)
		{
			return true;
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000FC3DD File Offset: 0x000FA5DD
		bool HandleEvent(BeforeSetFeelingEvent E)
		{
			return true;
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000FC3E0 File Offset: 0x000FA5E0
		bool HandleEvent(BeforeShieldBlockEvent E)
		{
			return true;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000FC3E3 File Offset: 0x000FA5E3
		bool HandleEvent(BeforeSlamEvent E)
		{
			return true;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000FC3E6 File Offset: 0x000FA5E6
		bool HandleEvent(BeforeTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000FC3E9 File Offset: 0x000FA5E9
		bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000FC3EC File Offset: 0x000FA5EC
		bool HandleEvent(BeforeTookDamageEvent E)
		{
			return true;
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000FC3EF File Offset: 0x000FA5EF
		bool HandleEvent(BeforeTravelDownEvent E)
		{
			return true;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000FC3F2 File Offset: 0x000FA5F2
		bool HandleEvent(BeforeUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000FC3F5 File Offset: 0x000FA5F5
		bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000FC3F8 File Offset: 0x000FA5F8
		bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000FC3FB File Offset: 0x000FA5FB
		bool HandleEvent(BeginConversationEvent E)
		{
			return true;
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000FC3FE File Offset: 0x000FA5FE
		bool HandleEvent(BeginMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000FC401 File Offset: 0x000FA601
		bool HandleEvent(BeginMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000FC404 File Offset: 0x000FA604
		bool HandleEvent(BeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000FC407 File Offset: 0x000FA607
		bool HandleEvent(BeingConsumedEvent E)
		{
			return true;
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000FC40A File Offset: 0x000FA60A
		bool HandleEvent(BlasphemyPerformedEvent E)
		{
			return true;
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000FC40D File Offset: 0x000FA60D
		bool HandleEvent(BlocksRadarEvent E)
		{
			return true;
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000FC410 File Offset: 0x000FA610
		bool HandleEvent(BodyPositionChangedEvent E)
		{
			return true;
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000FC413 File Offset: 0x000FA613
		bool HandleEvent(BootSequenceAbortedEvent E)
		{
			return true;
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000FC416 File Offset: 0x000FA616
		bool HandleEvent(BootSequenceDoneEvent E)
		{
			return true;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000FC419 File Offset: 0x000FA619
		bool HandleEvent(BootSequenceInitializedEvent E)
		{
			return true;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000FC41C File Offset: 0x000FA61C
		bool HandleEvent(CanAcceptObjectEvent E)
		{
			return true;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000FC41F File Offset: 0x000FA61F
		bool HandleEvent(CanApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000FC422 File Offset: 0x000FA622
		bool HandleEvent(CanBeDismemberedEvent E)
		{
			return true;
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x000FC425 File Offset: 0x000FA625
		bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return true;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000FC428 File Offset: 0x000FA628
		bool HandleEvent(CanBeMagneticallyManipulatedEvent E)
		{
			return true;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000FC42B File Offset: 0x000FA62B
		bool HandleEvent(CanBeModdedEvent E)
		{
			return true;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000FC42E File Offset: 0x000FA62E
		bool HandleEvent(CanBeNamedEvent E)
		{
			return true;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000FC431 File Offset: 0x000FA631
		bool HandleEvent(CanBeReplicatedEvent E)
		{
			return true;
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000FC434 File Offset: 0x000FA634
		bool HandleEvent(CanBeSlottedEvent E)
		{
			return true;
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000FC437 File Offset: 0x000FA637
		bool HandleEvent(CanBeTradedEvent E)
		{
			return true;
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000FC43A File Offset: 0x000FA63A
		bool HandleEvent(CanBeUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000FC43D File Offset: 0x000FA63D
		bool HandleEvent(CanChangeMovementModeEvent E)
		{
			return true;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000FC440 File Offset: 0x000FA640
		bool HandleEvent(CanDrinkEvent E)
		{
			return true;
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000FC443 File Offset: 0x000FA643
		bool HandleEvent(CanEnterInteriorEvent E)
		{
			return true;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000FC446 File Offset: 0x000FA646
		bool HandleEvent(CanFallEvent E)
		{
			return true;
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000FC449 File Offset: 0x000FA649
		bool HandleEvent(CanFireAllMissileWeaponsEvent E)
		{
			return true;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000FC44C File Offset: 0x000FA64C
		bool HandleEvent(CanGiveDirectionsEvent E)
		{
			return true;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000FC44F File Offset: 0x000FA64F
		bool HandleEvent(CanHaveConversationEvent E)
		{
			return true;
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000FC452 File Offset: 0x000FA652
		bool HandleEvent(CanJoinPartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000FC455 File Offset: 0x000FA655
		bool HandleEvent(CanMissilePassForcefieldEvent E)
		{
			return true;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000FC458 File Offset: 0x000FA658
		bool HandleEvent(CanReceiveEmpathyEvent E)
		{
			return true;
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000FC45B File Offset: 0x000FA65B
		bool HandleEvent(CanReceiveTelepathyEvent E)
		{
			return true;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000FC45E File Offset: 0x000FA65E
		bool HandleEvent(CanRefreshAbilityEvent E)
		{
			return true;
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000FC461 File Offset: 0x000FA661
		bool HandleEvent(CanSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000FC464 File Offset: 0x000FA664
		bool HandleEvent(CanStartConversationEvent E)
		{
			return true;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000FC467 File Offset: 0x000FA667
		bool HandleEvent(CanTemperatureReturnToAmbientEvent E)
		{
			return true;
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000FC46A File Offset: 0x000FA66A
		bool HandleEvent(CanTradeEvent E)
		{
			return true;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000FC46D File Offset: 0x000FA66D
		bool HandleEvent(CanTravelEvent E)
		{
			return true;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000FC470 File Offset: 0x000FA670
		bool HandleEvent(CarryingCapacityChangedEvent E)
		{
			return true;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000FC473 File Offset: 0x000FA673
		bool HandleEvent(CellChangedEvent E)
		{
			return true;
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000FC476 File Offset: 0x000FA676
		bool HandleEvent(CellDepletedEvent E)
		{
			return true;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000FC479 File Offset: 0x000FA679
		bool HandleEvent(ChargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000FC47C File Offset: 0x000FA67C
		bool HandleEvent(ChargeUsedEvent E)
		{
			return true;
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000FC47F File Offset: 0x000FA67F
		bool HandleEvent(CheckAnythingToCleanEvent E)
		{
			return true;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000FC482 File Offset: 0x000FA682
		bool HandleEvent(CheckAnythingToCleanWithEvent E)
		{
			return true;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000FC485 File Offset: 0x000FA685
		bool HandleEvent(CheckAnythingToCleanWithNearbyEvent E)
		{
			return true;
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000FC488 File Offset: 0x000FA688
		bool HandleEvent(CheckAttackableEvent E)
		{
			return true;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000FC48B File Offset: 0x000FA68B
		bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return true;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000FC48E File Offset: 0x000FA68E
		bool HandleEvent(CheckGasCanAffectEvent E)
		{
			return true;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000FC491 File Offset: 0x000FA691
		bool HandleEvent(CheckLoadAmmoEvent E)
		{
			return true;
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000FC494 File Offset: 0x000FA694
		bool HandleEvent(CheckOverburdenedOnStrengthUpdateEvent E)
		{
			return true;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000FC497 File Offset: 0x000FA697
		bool HandleEvent(CheckPaintabilityEvent E)
		{
			return true;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000FC49A File Offset: 0x000FA69A
		bool HandleEvent(CheckReadyToFireEvent E)
		{
			return true;
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000FC49D File Offset: 0x000FA69D
		bool HandleEvent(CheckRealityDistortionAccessibilityEvent E)
		{
			return true;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000FC4A0 File Offset: 0x000FA6A0
		bool HandleEvent(CheckRealityDistortionAdvisabilityEvent E)
		{
			return true;
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000FC4A3 File Offset: 0x000FA6A3
		bool HandleEvent(CheckRealityDistortionUsabilityEvent E)
		{
			return true;
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000FC4A6 File Offset: 0x000FA6A6
		bool HandleEvent(CheckSpawnMergeEvent E)
		{
			return true;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000FC4A9 File Offset: 0x000FA6A9
		bool HandleEvent(CheckTileChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000FC4AC File Offset: 0x000FA6AC
		bool HandleEvent(CheckUsesChargeWhileEquippedEvent E)
		{
			return true;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000FC4AF File Offset: 0x000FA6AF
		bool HandleEvent(CleanItemsEvent E)
		{
			return true;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000FC4B2 File Offset: 0x000FA6B2
		bool HandleEvent(CollectBroadcastChargeEvent E)
		{
			return true;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000FC4B5 File Offset: 0x000FA6B5
		bool HandleEvent(CommandEvent E)
		{
			return true;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000FC4B8 File Offset: 0x000FA6B8
		bool HandleEvent(CommandReloadEvent E)
		{
			return true;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000FC4BB File Offset: 0x000FA6BB
		bool HandleEvent(CommandReplaceCellEvent E)
		{
			return true;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000FC4BE File Offset: 0x000FA6BE
		bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			return true;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000FC4C1 File Offset: 0x000FA6C1
		bool HandleEvent(CommandSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000FC4C4 File Offset: 0x000FA6C4
		bool HandleEvent(CommandTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000FC4C7 File Offset: 0x000FA6C7
		bool HandleEvent(ConfigureMissileVisualEffectEvent E)
		{
			return true;
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000FC4CA File Offset: 0x000FA6CA
		bool HandleEvent(ContainsAnyBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000FC4CD File Offset: 0x000FA6CD
		bool HandleEvent(ContainsBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000FC4D0 File Offset: 0x000FA6D0
		bool HandleEvent(ContainsEvent E)
		{
			return true;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000FC4D3 File Offset: 0x000FA6D3
		bool HandleEvent(DamageConstantAdjustedEvent E)
		{
			return true;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000FC4D6 File Offset: 0x000FA6D6
		bool HandleEvent(DamageDieSizeAdjustedEvent E)
		{
			return true;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000FC4D9 File Offset: 0x000FA6D9
		bool HandleEvent(DeathEvent E)
		{
			return true;
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000FC4DC File Offset: 0x000FA6DC
		bool HandleEvent(DecorateDefaultEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000FC4DF File Offset: 0x000FA6DF
		bool HandleEvent(DefendMeleeHitEvent E)
		{
			return true;
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000FC4E2 File Offset: 0x000FA6E2
		bool HandleEvent(DefenderMissileHitEvent E)
		{
			return true;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000FC4E5 File Offset: 0x000FA6E5
		bool HandleEvent(DerivationCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000FC4E8 File Offset: 0x000FA6E8
		bool HandleEvent(DidInitialEquipEvent E)
		{
			return true;
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000FC4EB File Offset: 0x000FA6EB
		bool HandleEvent(DidReequipEvent E)
		{
			return true;
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000FC4EE File Offset: 0x000FA6EE
		bool HandleEvent(DropOnDeathEvent E)
		{
			return true;
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000FC4F1 File Offset: 0x000FA6F1
		bool HandleEvent(DroppedEvent E)
		{
			return true;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000FC4F4 File Offset: 0x000FA6F4
		bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000FC4F7 File Offset: 0x000FA6F7
		bool HandleEvent(EarlyBeforeDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000FC4FA File Offset: 0x000FA6FA
		bool HandleEvent(EffectAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000FC4FD File Offset: 0x000FA6FD
		bool HandleEvent(EffectForceAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000FC500 File Offset: 0x000FA700
		bool HandleEvent(EffectRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000FC503 File Offset: 0x000FA703
		bool HandleEvent(EmbarkEvent E)
		{
			return true;
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000FC506 File Offset: 0x000FA706
		bool HandleEvent(EncounterChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000FC509 File Offset: 0x000FA709
		bool HandleEvent(EncumbranceChangedEvent E)
		{
			return true;
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000FC50C File Offset: 0x000FA70C
		bool HandleEvent(EndActionEvent E)
		{
			return true;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000FC50F File Offset: 0x000FA70F
		bool HandleEvent(EndTurnEvent E)
		{
			return true;
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000FC512 File Offset: 0x000FA712
		bool HandleEvent(EnterCellEvent E)
		{
			return true;
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000FC515 File Offset: 0x000FA715
		bool HandleEvent(EnteredCellEvent E)
		{
			return true;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000FC518 File Offset: 0x000FA718
		bool HandleEvent(EnteringCellEvent E)
		{
			return true;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000FC51B File Offset: 0x000FA71B
		bool HandleEvent(EnteringZoneEvent E)
		{
			return true;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000FC51E File Offset: 0x000FA71E
		bool HandleEvent(EnvironmentalUpdateEvent E)
		{
			return true;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000FC521 File Offset: 0x000FA721
		bool HandleEvent(EquippedEvent E)
		{
			return true;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000FC524 File Offset: 0x000FA724
		bool HandleEvent(EquipperEquippedEvent E)
		{
			return true;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000FC527 File Offset: 0x000FA727
		bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000FC52A File Offset: 0x000FA72A
		bool HandleEvent(ExamineFailureEvent E)
		{
			return true;
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000FC52D File Offset: 0x000FA72D
		bool HandleEvent(ExamineSuccessEvent E)
		{
			return true;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000FC530 File Offset: 0x000FA730
		bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			return true;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000FC533 File Offset: 0x000FA733
		bool HandleEvent(ExtremitiesMovedEvent E)
		{
			return true;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000FC536 File Offset: 0x000FA736
		bool HandleEvent(FellDownEvent E)
		{
			return true;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000FC539 File Offset: 0x000FA739
		bool HandleEvent(FindObjectByIdEvent E)
		{
			return true;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000FC53C File Offset: 0x000FA73C
		bool HandleEvent(FinishChargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000FC53F File Offset: 0x000FA73F
		bool HandleEvent(FinishRechargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000FC542 File Offset: 0x000FA742
		bool HandleEvent(FlushWeightCacheEvent E)
		{
			return true;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000FC545 File Offset: 0x000FA745
		bool HandleEvent(ForceApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000FC548 File Offset: 0x000FA748
		bool HandleEvent(FrozeEvent E)
		{
			return true;
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000FC54B File Offset: 0x000FA74B
		bool HandleEvent(GaveDirectionsEvent E)
		{
			return true;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000FC54E File Offset: 0x000FA74E
		bool HandleEvent(GeneralAmnestyEvent E)
		{
			return true;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000FC551 File Offset: 0x000FA751
		bool HandleEvent(GenericCommandEvent E)
		{
			return true;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000FC554 File Offset: 0x000FA754
		bool HandleEvent(GenericDeepNotifyEvent E)
		{
			return true;
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000FC557 File Offset: 0x000FA757
		bool HandleEvent(GenericDeepQueryEvent E)
		{
			return true;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000FC55A File Offset: 0x000FA75A
		bool HandleEvent(GenericDeepRatingEvent E)
		{
			return true;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000FC55D File Offset: 0x000FA75D
		bool HandleEvent(GenericNotifyEvent E)
		{
			return true;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000FC560 File Offset: 0x000FA760
		bool HandleEvent(GenericQueryEvent E)
		{
			return true;
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000FC563 File Offset: 0x000FA763
		bool HandleEvent(GenericRatingEvent E)
		{
			return true;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000FC566 File Offset: 0x000FA766
		bool HandleEvent(GetActivationPhaseEvent E)
		{
			return true;
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000FC569 File Offset: 0x000FA769
		bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000FC56C File Offset: 0x000FA76C
		bool HandleEvent(GetAmbientLightEvent E)
		{
			return true;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000FC56F File Offset: 0x000FA76F
		bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000FC572 File Offset: 0x000FA772
		bool HandleEvent(GetApparentSpeciesEvent E)
		{
			return true;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000FC575 File Offset: 0x000FA775
		bool HandleEvent(GetAttackerHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000FC578 File Offset: 0x000FA778
		bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000FC57B File Offset: 0x000FA77B
		bool HandleEvent(GetAutoCollectDramsEvent E)
		{
			return true;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000FC57E File Offset: 0x000FA77E
		bool HandleEvent(GetAutoEquipPriorityEvent E)
		{
			return true;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000FC581 File Offset: 0x000FA781
		bool HandleEvent(GetAvailableComputePowerEvent E)
		{
			return true;
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000FC584 File Offset: 0x000FA784
		bool HandleEvent(GetBandagePerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000FC587 File Offset: 0x000FA787
		bool HandleEvent(GetBleedLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000FC58A File Offset: 0x000FA78A
		bool HandleEvent(GetCarriedWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000FC58D File Offset: 0x000FA78D
		bool HandleEvent(GetCleaningItemsEvent E)
		{
			return true;
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000FC590 File Offset: 0x000FA790
		bool HandleEvent(GetCleaningItemsNearbyEvent E)
		{
			return true;
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000FC593 File Offset: 0x000FA793
		bool HandleEvent(GetCleaveAmountEvent E)
		{
			return true;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000FC596 File Offset: 0x000FA796
		bool HandleEvent(GetCleaveMaxPenaltyEvent E)
		{
			return true;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000FC599 File Offset: 0x000FA799
		bool HandleEvent(GetCompanionLimitEvent E)
		{
			return true;
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000FC59C File Offset: 0x000FA79C
		bool HandleEvent(GetCompanionStatusEvent E)
		{
			return true;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000FC59F File Offset: 0x000FA79F
		bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000FC5A2 File Offset: 0x000FA7A2
		bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000FC5A5 File Offset: 0x000FA7A5
		bool HandleEvent(GetContentsEvent E)
		{
			return true;
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000FC5A8 File Offset: 0x000FA7A8
		bool HandleEvent(GetContextEvent E)
		{
			return true;
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000FC5AB File Offset: 0x000FA7AB
		bool HandleEvent(GetCookingActionsEvent E)
		{
			return true;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000FC5AE File Offset: 0x000FA7AE
		bool HandleEvent(GetCooldownEvent E)
		{
			return true;
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000FC5B1 File Offset: 0x000FA7B1
		bool HandleEvent(GetCriticalThresholdEvent E)
		{
			return true;
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000FC5B4 File Offset: 0x000FA7B4
		bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000FC5B7 File Offset: 0x000FA7B7
		bool HandleEvent(GetDebugInternalsEvent E)
		{
			return true;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000FC5BA File Offset: 0x000FA7BA
		bool HandleEvent(GetDefenderHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000FC5BD File Offset: 0x000FA7BD
		bool HandleEvent(GetDefenderMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000FC5C0 File Offset: 0x000FA7C0
		bool HandleEvent(GetDifficultyEvaluationEvent E)
		{
			return true;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000FC5C3 File Offset: 0x000FA7C3
		bool HandleEvent(GetDiseaseOnsetEvent E)
		{
			return true;
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000FC5C6 File Offset: 0x000FA7C6
		bool HandleEvent(GetDisplayNameEvent E)
		{
			return true;
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000FC5C9 File Offset: 0x000FA7C9
		bool HandleEvent(GetDisplayNamePenetrationColorEvent E)
		{
			return true;
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000FC5CC File Offset: 0x000FA7CC
		bool HandleEvent(GetDisplayStatBonusEvent E)
		{
			return true;
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000FC5CF File Offset: 0x000FA7CF
		bool HandleEvent(GetElectricalConductivityEvent E)
		{
			return true;
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000FC5D2 File Offset: 0x000FA7D2
		bool HandleEvent(GetEnergyCostEvent E)
		{
			return true;
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000FC5D5 File Offset: 0x000FA7D5
		bool HandleEvent(GetExtraPhysicalFeaturesEvent E)
		{
			return true;
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000FC5D8 File Offset: 0x000FA7D8
		bool HandleEvent(GetExtrinsicValueEvent E)
		{
			return true;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000FC5DB File Offset: 0x000FA7DB
		bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000FC5DE File Offset: 0x000FA7DE
		bool HandleEvent(GetFactionRankEvent E)
		{
			return true;
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000FC5E1 File Offset: 0x000FA7E1
		bool HandleEvent(GetFeelingEvent E)
		{
			return true;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000FC5E4 File Offset: 0x000FA7E4
		bool HandleEvent(GetFirefightingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000FC5E7 File Offset: 0x000FA7E7
		bool HandleEvent(GetFixedMissileSpreadEvent E)
		{
			return true;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000FC5EA File Offset: 0x000FA7EA
		bool HandleEvent(GetFreeDramsEvent E)
		{
			return true;
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000FC5ED File Offset: 0x000FA7ED
		bool HandleEvent(GetGameObjectSortEvent E)
		{
			return true;
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000FC5F0 File Offset: 0x000FA7F0
		bool HandleEvent(GetGenderEvent E)
		{
			return true;
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000FC5F3 File Offset: 0x000FA7F3
		bool HandleEvent(GetHostileWalkRadiusEvent E)
		{
			return true;
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000FC5F6 File Offset: 0x000FA7F6
		bool HandleEvent(GetHostilityRecognitionLimitsEvent E)
		{
			return true;
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000FC5F9 File Offset: 0x000FA7F9
		bool HandleEvent(GetIdealTemperatureEvent E)
		{
			return true;
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000FC5FC File Offset: 0x000FA7FC
		bool HandleEvent(GetIntrinsicValueEvent E)
		{
			return true;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000FC5FF File Offset: 0x000FA7FF
		bool HandleEvent(GetIntrinsicWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000FC602 File Offset: 0x000FA802
		bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			return true;
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000FC605 File Offset: 0x000FA805
		bool HandleEvent(GetInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000FC608 File Offset: 0x000FA808
		bool HandleEvent(GetInventoryCategoryEvent E)
		{
			return true;
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000FC60B File Offset: 0x000FA80B
		bool HandleEvent(GetItemElementsEvent E)
		{
			return true;
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000FC60E File Offset: 0x000FA80E
		bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000FC611 File Offset: 0x000FA811
		bool HandleEvent(GetKineticResistanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000FC614 File Offset: 0x000FA814
		bool HandleEvent(GetLevelUpDiceEvent E)
		{
			return true;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000FC617 File Offset: 0x000FA817
		bool HandleEvent(GetLevelUpPointsEvent E)
		{
			return true;
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000FC61A File Offset: 0x000FA81A
		bool HandleEvent(GetLevelUpSkillPointsEvent E)
		{
			return true;
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000FC61D File Offset: 0x000FA81D
		bool HandleEvent(GetLostChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000FC620 File Offset: 0x000FA820
		bool HandleEvent(GetMatterPhaseEvent E)
		{
			return true;
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000FC623 File Offset: 0x000FA823
		bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000FC626 File Offset: 0x000FA826
		bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			return true;
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000FC629 File Offset: 0x000FA829
		bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000FC62C File Offset: 0x000FA82C
		bool HandleEvent(GetMeleeAttacksEvent E)
		{
			return true;
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000FC62F File Offset: 0x000FA82F
		bool HandleEvent(GetMissileCoverPercentageEvent E)
		{
			return true;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000FC632 File Offset: 0x000FA832
		bool HandleEvent(GetMissileStatusColorEvent E)
		{
			return true;
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000FC635 File Offset: 0x000FA835
		bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000FC638 File Offset: 0x000FA838
		bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			return true;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000FC63B File Offset: 0x000FA83B
		bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			return true;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000FC63E File Offset: 0x000FA83E
		bool HandleEvent(GetModRarityWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000FC641 File Offset: 0x000FA841
		bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			return true;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000FC644 File Offset: 0x000FA844
		bool HandleEvent(GetMutationTermEvent E)
		{
			return true;
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000FC647 File Offset: 0x000FA847
		bool HandleEvent(GetNamingBestowalChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000FC64A File Offset: 0x000FA84A
		bool HandleEvent(GetNamingChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000FC64D File Offset: 0x000FA84D
		bool HandleEvent(GetNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000FC650 File Offset: 0x000FA850
		bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			return true;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000FC653 File Offset: 0x000FA853
		bool HandleEvent(GetOverloadChargeEvent E)
		{
			return true;
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000FC656 File Offset: 0x000FA856
		bool HandleEvent(GetPartyLeaderFollowDistanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000FC659 File Offset: 0x000FA859
		bool HandleEvent(GetPointsOfInterestEvent E)
		{
			return true;
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000FC65C File Offset: 0x000FA85C
		bool HandleEvent(GetPowerLoadLevelEvent E)
		{
			return true;
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000FC65F File Offset: 0x000FA85F
		bool HandleEvent(GetPrecognitionRestoreGameStateEvent E)
		{
			return true;
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000FC662 File Offset: 0x000FA862
		bool HandleEvent(GetPreferredLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000FC665 File Offset: 0x000FA865
		bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000FC668 File Offset: 0x000FA868
		bool HandleEvent(GetProjectileObjectEvent E)
		{
			return true;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000FC66B File Offset: 0x000FA86B
		bool HandleEvent(GetPropertyModDescription E)
		{
			return true;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000FC66E File Offset: 0x000FA86E
		bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000FC671 File Offset: 0x000FA871
		bool HandleEvent(GetPsychicGlimmerEvent E)
		{
			return true;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000FC674 File Offset: 0x000FA874
		bool HandleEvent(GetRandomBuyChimericBodyPartRollsEvent E)
		{
			return true;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000FC677 File Offset: 0x000FA877
		bool HandleEvent(GetRandomBuyMutationCountEvent E)
		{
			return true;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000FC67A File Offset: 0x000FA87A
		bool HandleEvent(GetRealityStabilizationPenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000FC67D File Offset: 0x000FA87D
		bool HandleEvent(GetRebukeLevelEvent E)
		{
			return true;
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000FC680 File Offset: 0x000FA880
		bool HandleEvent(GetRecoilersEvent E)
		{
			return true;
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000FC683 File Offset: 0x000FA883
		bool HandleEvent(GetReplaceCellInteractionsEvent E)
		{
			return true;
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000FC686 File Offset: 0x000FA886
		bool HandleEvent(GetRespiratoryAgentPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000FC689 File Offset: 0x000FA889
		bool HandleEvent(GetRitualSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000FC68C File Offset: 0x000FA88C
		bool HandleEvent(GetRunningBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000FC68F File Offset: 0x000FA88F
		bool HandleEvent(GetScanTypeEvent E)
		{
			return true;
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000FC692 File Offset: 0x000FA892
		bool HandleEvent(GetShieldBlockPreferenceEvent E)
		{
			return true;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000FC695 File Offset: 0x000FA895
		bool HandleEvent(GetShieldSlamDamageEvent E)
		{
			return true;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000FC698 File Offset: 0x000FA898
		bool HandleEvent(GetShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000FC69B File Offset: 0x000FA89B
		bool HandleEvent(GetSkillEffectChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000FC69E File Offset: 0x000FA89E
		bool HandleEvent(GetSlotsRequiredEvent E)
		{
			return true;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000FC6A1 File Offset: 0x000FA8A1
		bool HandleEvent(GetSlottedInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000FC6A4 File Offset: 0x000FA8A4
		bool HandleEvent(GetSocialSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000FC6A7 File Offset: 0x000FA8A7
		bool HandleEvent(GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000FC6AA File Offset: 0x000FA8AA
		bool HandleEvent(GetSpaceTimeAnomalyEmergencePermillageChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000FC6AD File Offset: 0x000FA8AD
		bool HandleEvent(GetSpecialEffectChanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000FC6B0 File Offset: 0x000FA8B0
		bool HandleEvent(GetSpringinessEvent E)
		{
			return true;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000FC6B3 File Offset: 0x000FA8B3
		bool HandleEvent(GetSprintDurationEvent E)
		{
			return true;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000FC6B6 File Offset: 0x000FA8B6
		bool HandleEvent(GetStorableDramsEvent E)
		{
			return true;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000FC6B9 File Offset: 0x000FA8B9
		bool HandleEvent(GetSwimmingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000FC6BC File Offset: 0x000FA8BC
		bool HandleEvent(GetThrowProfileEvent E)
		{
			return true;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000FC6BF File Offset: 0x000FA8BF
		bool HandleEvent(GetThrownWeaponFlexPhaseProviderEvent E)
		{
			return true;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000FC6C2 File Offset: 0x000FA8C2
		bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000FC6C5 File Offset: 0x000FA8C5
		bool HandleEvent(GetThrownWeaponRangeEvent E)
		{
			return true;
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000FC6C8 File Offset: 0x000FA8C8
		bool HandleEvent(GetTinkeringBonusEvent E)
		{
			return true;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000FC6CB File Offset: 0x000FA8CB
		bool HandleEvent(GetToHitModifierEvent E)
		{
			return true;
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000FC6CE File Offset: 0x000FA8CE
		bool HandleEvent(GetTonicCapacityEvent E)
		{
			return true;
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000FC6D1 File Offset: 0x000FA8D1
		bool HandleEvent(GetTonicDosageEvent E)
		{
			return true;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000FC6D4 File Offset: 0x000FA8D4
		bool HandleEvent(GetTonicDurationEvent E)
		{
			return true;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000FC6D7 File Offset: 0x000FA8D7
		bool HandleEvent(GetTradePerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000FC6DA File Offset: 0x000FA8DA
		bool HandleEvent(GetTransitiveLocationEvent E)
		{
			return true;
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000FC6DD File Offset: 0x000FA8DD
		bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000FC6E0 File Offset: 0x000FA8E0
		bool HandleEvent(GetUtilityScoreEvent E)
		{
			return true;
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000FC6E3 File Offset: 0x000FA8E3
		bool HandleEvent(GetWadingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000FC6E6 File Offset: 0x000FA8E6
		bool HandleEvent(GetWaterRitualCostEvent E)
		{
			return true;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000FC6E9 File Offset: 0x000FA8E9
		bool HandleEvent(GetWaterRitualLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000FC6EC File Offset: 0x000FA8EC
		bool HandleEvent(GetWaterRitualReputationAmountEvent E)
		{
			return true;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000FC6EF File Offset: 0x000FA8EF
		bool HandleEvent(GetWaterRitualSecretWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000FC6F2 File Offset: 0x000FA8F2
		bool HandleEvent(GetWaterRitualSellSecretBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000FC6F5 File Offset: 0x000FA8F5
		bool HandleEvent(GetWeaponHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000FC6F8 File Offset: 0x000FA8F8
		bool HandleEvent(GetWeaponMeleeAttacksEvent E)
		{
			return true;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000FC6FB File Offset: 0x000FA8FB
		bool HandleEvent(GetWeaponMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000FC6FE File Offset: 0x000FA8FE
		bool HandleEvent(GetZoneEvent E)
		{
			return true;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000FC701 File Offset: 0x000FA901
		bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			return true;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000FC704 File Offset: 0x000FA904
		bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			return true;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000FC707 File Offset: 0x000FA907
		bool HandleEvent(GiveDramsEvent E)
		{
			return true;
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000FC70A File Offset: 0x000FA90A
		bool HandleEvent(GlimmerChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000FC70D File Offset: 0x000FA90D
		bool HandleEvent(GravitationEvent E)
		{
			return true;
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000FC710 File Offset: 0x000FA910
		bool HandleEvent(HasBeenReadEvent E)
		{
			return true;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000FC713 File Offset: 0x000FA913
		bool HandleEvent(HasBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000FC716 File Offset: 0x000FA916
		bool HandleEvent(HasFlammableEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000FC719 File Offset: 0x000FA919
		bool HandleEvent(HasFlammableEquipmentOrInventoryEvent E)
		{
			return true;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000FC71C File Offset: 0x000FA91C
		bool HandleEvent(HealsNaturallyEvent E)
		{
			return true;
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000FC71F File Offset: 0x000FA91F
		bool HandleEvent(IAICommandListEvent E)
		{
			return true;
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000FC722 File Offset: 0x000FA922
		bool HandleEvent(IAIItemCommandListEvent E)
		{
			return true;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000FC725 File Offset: 0x000FA925
		bool HandleEvent(IAIMoveCommandListEvent E)
		{
			return true;
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000FC728 File Offset: 0x000FA928
		bool HandleEvent(IActOnItemEvent E)
		{
			return true;
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000FC72B File Offset: 0x000FA92B
		bool HandleEvent(IActualEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000FC72E File Offset: 0x000FA92E
		bool HandleEvent(IAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000FC731 File Offset: 0x000FA931
		bool HandleEvent(IAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000FC734 File Offset: 0x000FA934
		bool HandleEvent(IBootSequenceEvent E)
		{
			return true;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000FC737 File Offset: 0x000FA937
		bool HandleEvent(IChargeConsumptionEvent E)
		{
			return true;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000FC73A File Offset: 0x000FA93A
		bool HandleEvent(IChargeEvent E)
		{
			return true;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000FC73D File Offset: 0x000FA93D
		bool HandleEvent(IChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000FC740 File Offset: 0x000FA940
		bool HandleEvent(IChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000FC743 File Offset: 0x000FA943
		bool HandleEvent(IConsumeEvent E)
		{
			return true;
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000FC746 File Offset: 0x000FA946
		bool HandleEvent(IConversationMinEvent E)
		{
			return true;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x000FC749 File Offset: 0x000FA949
		bool HandleEvent(IDamageEvent E)
		{
			return true;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000FC74C File Offset: 0x000FA94C
		bool HandleEvent(IDeathEvent E)
		{
			return true;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000FC74F File Offset: 0x000FA94F
		bool HandleEvent(IDerivationEvent E)
		{
			return true;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000FC752 File Offset: 0x000FA952
		bool HandleEvent(IDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000FC755 File Offset: 0x000FA955
		bool HandleEvent(IEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000FC758 File Offset: 0x000FA958
		bool HandleEvent(IExamineEvent E)
		{
			return true;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000FC75B File Offset: 0x000FA95B
		bool HandleEvent(IFinalChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000FC75E File Offset: 0x000FA95E
		bool HandleEvent(IHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000FC761 File Offset: 0x000FA961
		bool HandleEvent(IInitialChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000FC764 File Offset: 0x000FA964
		bool HandleEvent(IInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000FC767 File Offset: 0x000FA967
		bool HandleEvent(ILimbRegenerationEvent E)
		{
			return true;
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000FC76A File Offset: 0x000FA96A
		bool HandleEvent(ILiquidEvent E)
		{
			return true;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000FC76D File Offset: 0x000FA96D
		bool HandleEvent(IMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000FC770 File Offset: 0x000FA970
		bool HandleEvent(IMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000FC773 File Offset: 0x000FA973
		bool HandleEvent(IMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000FC776 File Offset: 0x000FA976
		bool HandleEvent(INavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000FC779 File Offset: 0x000FA979
		bool HandleEvent(IObjectCellInteractionEvent E)
		{
			return true;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000FC77C File Offset: 0x000FA97C
		bool HandleEvent(IObjectCreationEvent E)
		{
			return true;
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000FC77F File Offset: 0x000FA97F
		bool HandleEvent(IObjectInventoryInteractionEvent E)
		{
			return true;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000FC782 File Offset: 0x000FA982
		bool HandleEvent(IQuestEvent E)
		{
			return true;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000FC785 File Offset: 0x000FA985
		bool HandleEvent(IRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000FC788 File Offset: 0x000FA988
		bool HandleEvent(IReplicationEvent E)
		{
			return true;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000FC78B File Offset: 0x000FA98B
		bool HandleEvent(ISaveEvent E)
		{
			return true;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000FC78E File Offset: 0x000FA98E
		bool HandleEvent(IShallowZoneEvent E)
		{
			return true;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000FC791 File Offset: 0x000FA991
		bool HandleEvent(IShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000FC794 File Offset: 0x000FA994
		bool HandleEvent(ITravelEvent E)
		{
			return true;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000FC797 File Offset: 0x000FA997
		bool HandleEvent(IValueEvent E)
		{
			return true;
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000FC79A File Offset: 0x000FA99A
		bool HandleEvent(IWeightEvent E)
		{
			return true;
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000FC79D File Offset: 0x000FA99D
		bool HandleEvent(IXPEvent E)
		{
			return true;
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000FC7A0 File Offset: 0x000FA9A0
		bool HandleEvent(IZoneEvent E)
		{
			return true;
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000FC7A3 File Offset: 0x000FA9A3
		bool HandleEvent(IZoneOnlyEvent E)
		{
			return true;
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000FC7A6 File Offset: 0x000FA9A6
		bool HandleEvent(IdleQueryEvent E)
		{
			return true;
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000FC7A9 File Offset: 0x000FA9A9
		bool HandleEvent(ImplantAddedEvent E)
		{
			return true;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000FC7AC File Offset: 0x000FA9AC
		bool HandleEvent(ImplantRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000FC7AF File Offset: 0x000FA9AF
		bool HandleEvent(ImplantedEvent E)
		{
			return true;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000FC7B2 File Offset: 0x000FA9B2
		bool HandleEvent(InduceVomitingEvent E)
		{
			return true;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000FC7B5 File Offset: 0x000FA9B5
		bool HandleEvent(InductionChargeEvent E)
		{
			return true;
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000FC7B8 File Offset: 0x000FA9B8
		bool HandleEvent(InteriorZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000FC7BB File Offset: 0x000FA9BB
		bool HandleEvent(InterruptAutowalkEvent E)
		{
			return true;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000FC7BE File Offset: 0x000FA9BE
		bool HandleEvent(InventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000FC7C1 File Offset: 0x000FA9C1
		bool HandleEvent(IsAdaptivePenetrationActiveEvent E)
		{
			return true;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000FC7C4 File Offset: 0x000FA9C4
		bool HandleEvent(IsAfflictionEvent E)
		{
			return true;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000FC7C7 File Offset: 0x000FA9C7
		bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			return true;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000FC7CA File Offset: 0x000FA9CA
		bool HandleEvent(IsExplosiveEvent E)
		{
			return true;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000FC7CD File Offset: 0x000FA9CD
		bool HandleEvent(IsMutantEvent E)
		{
			return true;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000FC7D0 File Offset: 0x000FA9D0
		bool HandleEvent(IsOverloadableEvent E)
		{
			return true;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000FC7D3 File Offset: 0x000FA9D3
		bool HandleEvent(IsRepairableEvent E)
		{
			return true;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000FC7D6 File Offset: 0x000FA9D6
		bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return true;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000FC7D9 File Offset: 0x000FA9D9
		bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			return true;
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000FC7DC File Offset: 0x000FA9DC
		bool HandleEvent(IsTrueKinEvent E)
		{
			return true;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000FC7DF File Offset: 0x000FA9DF
		bool HandleEvent(IsVehicleOperationalEvent E)
		{
			return true;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000FC7E2 File Offset: 0x000FA9E2
		bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			return true;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000FC7E5 File Offset: 0x000FA9E5
		bool HandleEvent(JoinedPartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000FC7E8 File Offset: 0x000FA9E8
		bool HandleEvent(JumpedEvent E)
		{
			return true;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000FC7EB File Offset: 0x000FA9EB
		bool HandleEvent(KilledEvent E)
		{
			return true;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000FC7EE File Offset: 0x000FA9EE
		bool HandleEvent(KilledPlayerEvent E)
		{
			return true;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000FC7F1 File Offset: 0x000FA9F1
		bool HandleEvent(LateBeforeApplyDamageEvent E)
		{
			return true;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000FC7F4 File Offset: 0x000FA9F4
		bool HandleEvent(LeaveCellEvent E)
		{
			return true;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000FC7F7 File Offset: 0x000FA9F7
		bool HandleEvent(LeavingCellEvent E)
		{
			return true;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000FC7FA File Offset: 0x000FA9FA
		bool HandleEvent(LeftCellEvent E)
		{
			return true;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000FC7FD File Offset: 0x000FA9FD
		bool HandleEvent(LiquidMixedEvent E)
		{
			return true;
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000FC800 File Offset: 0x000FAA00
		bool HandleEvent(LoadAmmoEvent E)
		{
			return true;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000FC803 File Offset: 0x000FAA03
		bool HandleEvent(MakeTemporaryEvent E)
		{
			return true;
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000FC806 File Offset: 0x000FAA06
		bool HandleEvent(MentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000FC809 File Offset: 0x000FAA09
		bool HandleEvent(MissilePenetrateEvent E)
		{
			return true;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000FC80C File Offset: 0x000FAA0C
		bool HandleEvent(MissileTraversingCellEvent E)
		{
			return true;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000FC80F File Offset: 0x000FAA0F
		bool HandleEvent(ModificationAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000FC812 File Offset: 0x000FAA12
		bool HandleEvent(ModifyAttackingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000FC815 File Offset: 0x000FAA15
		bool HandleEvent(ModifyBitCostEvent E)
		{
			return true;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000FC818 File Offset: 0x000FAA18
		bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000FC81B File Offset: 0x000FAA1B
		bool HandleEvent(ModifyOriginatingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000FC81E File Offset: 0x000FAA1E
		bool HandleEvent(MovementModeChangedEvent E)
		{
			return true;
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000FC821 File Offset: 0x000FAA21
		bool HandleEvent(MutationsSubjectToEMPEvent E)
		{
			return true;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000FC824 File Offset: 0x000FAA24
		bool HandleEvent(NeedPartSupportEvent E)
		{
			return true;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000FC827 File Offset: 0x000FAA27
		bool HandleEvent(NeedsReloadEvent E)
		{
			return true;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000FC82A File Offset: 0x000FAA2A
		bool HandleEvent(NeutronFluxPourExplodesEvent E)
		{
			return true;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000FC82D File Offset: 0x000FAA2D
		bool HandleEvent(NotifyTargetImmuneEvent E)
		{
			return true;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000FC830 File Offset: 0x000FAA30
		bool HandleEvent(ObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000FC833 File Offset: 0x000FAA33
		bool HandleEvent(ObjectEnteredCellEvent E)
		{
			return true;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000FC836 File Offset: 0x000FAA36
		bool HandleEvent(ObjectEnteringCellEvent E)
		{
			return true;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000FC839 File Offset: 0x000FAA39
		bool HandleEvent(ObjectGoingProneEvent E)
		{
			return true;
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000FC83C File Offset: 0x000FAA3C
		bool HandleEvent(ObjectLeavingCellEvent E)
		{
			return true;
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000FC83F File Offset: 0x000FAA3F
		bool HandleEvent(ObjectStartedFlyingEvent E)
		{
			return true;
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000FC842 File Offset: 0x000FAA42
		bool HandleEvent(ObjectStoppedFlyingEvent E)
		{
			return true;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000FC845 File Offset: 0x000FAA45
		bool HandleEvent(OkayToDamageEvent E)
		{
			return true;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000FC848 File Offset: 0x000FAA48
		bool HandleEvent(OnDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000FC84B File Offset: 0x000FAA4B
		bool HandleEvent(OnDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000FC84E File Offset: 0x000FAA4E
		bool HandleEvent(OnQuestAddedEvent E)
		{
			return true;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000FC851 File Offset: 0x000FAA51
		bool HandleEvent(OwnerAfterInventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000FC854 File Offset: 0x000FAA54
		bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000FC857 File Offset: 0x000FAA57
		bool HandleEvent(OwnerGetShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000FC85A File Offset: 0x000FAA5A
		bool HandleEvent(OwnerGetUnknownShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000FC85D File Offset: 0x000FAA5D
		bool HandleEvent(PartAddedEvent E)
		{
			return true;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000FC860 File Offset: 0x000FAA60
		bool HandleEvent(PartRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000FC863 File Offset: 0x000FAA63
		bool HandleEvent(PartSupportEvent E)
		{
			return true;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000FC866 File Offset: 0x000FAA66
		bool HandleEvent(PathAsBurrowerEvent E)
		{
			return true;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000FC869 File Offset: 0x000FAA69
		bool HandleEvent(PhysicalContactEvent E)
		{
			return true;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000FC86C File Offset: 0x000FAA6C
		bool HandleEvent(PointDefenseInterceptEvent E)
		{
			return true;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000FC86F File Offset: 0x000FAA6F
		bool HandleEvent(PollForHealingLocationEvent E)
		{
			return true;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000FC872 File Offset: 0x000FAA72
		bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			return true;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000FC875 File Offset: 0x000FAA75
		bool HandleEvent(PowerUpdatedEvent E)
		{
			return true;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000FC878 File Offset: 0x000FAA78
		bool HandleEvent(PreferDefaultBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000FC87B File Offset: 0x000FAA7B
		bool HandleEvent(PreferTargetEvent E)
		{
			return true;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000FC87E File Offset: 0x000FAA7E
		bool HandleEvent(PreventSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000FC881 File Offset: 0x000FAA81
		bool HandleEvent(PrimePowerSystemsEvent E)
		{
			return true;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000FC884 File Offset: 0x000FAA84
		bool HandleEvent(ProducesLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000FC887 File Offset: 0x000FAA87
		bool HandleEvent(ProjectileMovingEvent E)
		{
			return true;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000FC88A File Offset: 0x000FAA8A
		bool HandleEvent(QueryBroadcastDrawEvent E)
		{
			return true;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000FC88D File Offset: 0x000FAA8D
		bool HandleEvent(QueryChargeEvent E)
		{
			return true;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000FC890 File Offset: 0x000FAA90
		bool HandleEvent(QueryChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000FC893 File Offset: 0x000FAA93
		bool HandleEvent(QueryChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000FC896 File Offset: 0x000FAA96
		bool HandleEvent(QueryDrawEvent E)
		{
			return true;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000FC899 File Offset: 0x000FAA99
		bool HandleEvent(QueryEquippableListEvent E)
		{
			return true;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000FC89C File Offset: 0x000FAA9C
		bool HandleEvent(QueryInductionChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000FC89F File Offset: 0x000FAA9F
		bool HandleEvent(QueryRechargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000FC8A2 File Offset: 0x000FAAA2
		bool HandleEvent(QuerySlotListEvent E)
		{
			return true;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000FC8A5 File Offset: 0x000FAAA5
		bool HandleEvent(QuestFinishedEvent E)
		{
			return true;
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000FC8A8 File Offset: 0x000FAAA8
		bool HandleEvent(QuestStartedEvent E)
		{
			return true;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000FC8AB File Offset: 0x000FAAAB
		bool HandleEvent(QuestStepFinishedEvent E)
		{
			return true;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000FC8AE File Offset: 0x000FAAAE
		bool HandleEvent(RadiatesHeatAdjacentEvent E)
		{
			return true;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000FC8B1 File Offset: 0x000FAAB1
		bool HandleEvent(RadiatesHeatEvent E)
		{
			return true;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000FC8B4 File Offset: 0x000FAAB4
		bool HandleEvent(RealityStabilizeEvent E)
		{
			return true;
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000FC8B7 File Offset: 0x000FAAB7
		bool HandleEvent(RechargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000FC8BA File Offset: 0x000FAABA
		bool HandleEvent(RefreshTileEvent E)
		{
			return true;
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000FC8BD File Offset: 0x000FAABD
		bool HandleEvent(RegenerateDefaultEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000FC8C0 File Offset: 0x000FAAC0
		bool HandleEvent(RegenerateLimbEvent E)
		{
			return true;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000FC8C3 File Offset: 0x000FAAC3
		bool HandleEvent(RemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000FC8C6 File Offset: 0x000FAAC6
		bool HandleEvent(RepaintedEvent E)
		{
			return true;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000FC8C9 File Offset: 0x000FAAC9
		bool HandleEvent(RepairCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000FC8CC File Offset: 0x000FAACC
		bool HandleEvent(RepairedEvent E)
		{
			return true;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000FC8CF File Offset: 0x000FAACF
		bool HandleEvent(ReplaceInContextEvent E)
		{
			return true;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000FC8D2 File Offset: 0x000FAAD2
		bool HandleEvent(ReplaceThrownWeaponEvent E)
		{
			return true;
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000FC8D5 File Offset: 0x000FAAD5
		bool HandleEvent(ReplicaCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000FC8D8 File Offset: 0x000FAAD8
		bool HandleEvent(ReputationChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000FC8DB File Offset: 0x000FAADB
		bool HandleEvent(RespiresEvent E)
		{
			return true;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000FC8DE File Offset: 0x000FAADE
		bool HandleEvent(SecretVisibilityChangedEvent E)
		{
			return true;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000FC8E1 File Offset: 0x000FAAE1
		bool HandleEvent(ShieldSlamPerformedEvent E)
		{
			return true;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000FC8E4 File Offset: 0x000FAAE4
		bool HandleEvent(ShotCompleteEvent E)
		{
			return true;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000FC8E7 File Offset: 0x000FAAE7
		bool HandleEvent(ShouldAttackToReachTargetEvent E)
		{
			return true;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000FC8EA File Offset: 0x000FAAEA
		bool HandleEvent(ShouldDescribeStatBonusEvent E)
		{
			return true;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000FC8ED File Offset: 0x000FAAED
		bool HandleEvent(SlamEvent E)
		{
			return true;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000FC8F0 File Offset: 0x000FAAF0
		bool HandleEvent(StackCountChangedEvent E)
		{
			return true;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000FC8F3 File Offset: 0x000FAAF3
		bool HandleEvent(StartTradeEvent E)
		{
			return true;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000FC8F6 File Offset: 0x000FAAF6
		bool HandleEvent(StatChangeEvent E)
		{
			return true;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000FC8F9 File Offset: 0x000FAAF9
		bool HandleEvent(StockedEvent E)
		{
			return true;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000FC8FC File Offset: 0x000FAAFC
		bool HandleEvent(StripContentsEvent E)
		{
			return true;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000FC8FF File Offset: 0x000FAAFF
		bool HandleEvent(SubjectToGravityEvent E)
		{
			return true;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000FC902 File Offset: 0x000FAB02
		bool HandleEvent(SuspendingEvent E)
		{
			return true;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000FC905 File Offset: 0x000FAB05
		bool HandleEvent(SyncMutationLevelsEvent E)
		{
			return true;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000FC908 File Offset: 0x000FAB08
		bool HandleEvent(SyncRenderEvent E)
		{
			return true;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000FC90B File Offset: 0x000FAB0B
		bool HandleEvent(SynchronizeExistenceEvent E)
		{
			return true;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000FC90E File Offset: 0x000FAB0E
		bool HandleEvent(TakeOnRoleEvent E)
		{
			return true;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000FC911 File Offset: 0x000FAB11
		bool HandleEvent(TakenEvent E)
		{
			return true;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000FC914 File Offset: 0x000FAB14
		bool HandleEvent(TestChargeEvent E)
		{
			return true;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000FC917 File Offset: 0x000FAB17
		bool HandleEvent(ThawedEvent E)
		{
			return true;
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000FC91A File Offset: 0x000FAB1A
		bool HandleEvent(TookDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000FC91D File Offset: 0x000FAB1D
		bool HandleEvent(TookEnvironmentalDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000FC920 File Offset: 0x000FAB20
		bool HandleEvent(TookEvent E)
		{
			return true;
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000FC923 File Offset: 0x000FAB23
		bool HandleEvent(TransparentToEMPEvent E)
		{
			return true;
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000FC926 File Offset: 0x000FAB26
		bool HandleEvent(TravelSpeedEvent E)
		{
			return true;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000FC929 File Offset: 0x000FAB29
		bool HandleEvent(TriggersMakersMarkCreationEvent E)
		{
			return true;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000FC92C File Offset: 0x000FAB2C
		bool HandleEvent(TryRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000FC92F File Offset: 0x000FAB2F
		bool HandleEvent(UnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000FC932 File Offset: 0x000FAB32
		bool HandleEvent(UnimplantedEvent E)
		{
			return true;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000FC935 File Offset: 0x000FAB35
		bool HandleEvent(UseChargeEvent E)
		{
			return true;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000FC938 File Offset: 0x000FAB38
		bool HandleEvent(UseDramsEvent E)
		{
			return true;
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000FC93B File Offset: 0x000FAB3B
		bool HandleEvent(UseEnergyEvent E)
		{
			return true;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000FC93E File Offset: 0x000FAB3E
		bool HandleEvent(UseHealingLocationEvent E)
		{
			return true;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000FC941 File Offset: 0x000FAB41
		bool HandleEvent(UsingChargeEvent E)
		{
			return true;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000FC944 File Offset: 0x000FAB44
		bool HandleEvent(VaporizedEvent E)
		{
			return true;
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000FC947 File Offset: 0x000FAB47
		bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return true;
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000FC94A File Offset: 0x000FAB4A
		bool HandleEvent(WasDerivedFromEvent E)
		{
			return true;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000FC94D File Offset: 0x000FAB4D
		bool HandleEvent(WasReplicatedEvent E)
		{
			return true;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000FC950 File Offset: 0x000FAB50
		bool HandleEvent(WaterRitualStartEvent E)
		{
			return true;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000FC953 File Offset: 0x000FAB53
		bool HandleEvent(WorshipPerformedEvent E)
		{
			return true;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000FC956 File Offset: 0x000FAB56
		bool HandleEvent(ZoneActivatedEvent E)
		{
			return true;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000FC959 File Offset: 0x000FAB59
		bool HandleEvent(ZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000FC95C File Offset: 0x000FAB5C
		bool HandleEvent(ZoneDeactivatedEvent E)
		{
			return true;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000FC95F File Offset: 0x000FAB5F
		bool HandleEvent(ZoneThawedEvent E)
		{
			return true;
		}

		/// <inheritdoc cref="T:XRL.IEventBinder" />
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000FC962 File Offset: 0x000FAB62
		IEventBinder Binder
		{
			get
			{
				return null;
			}
		}

		/// <summary>Check if the current event handler is still valid for receiving events.</summary>
		/// <remarks>
		/// Usually this is invalid when the event handler has been removed.
		/// E.g. when an <see cref="T:XRL.World.IPart" /> has been removed from its parent <see cref="T:XRL.World.GameObject" />.
		/// </remarks>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000FC965 File Offset: 0x000FAB65
		bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000FC968 File Offset: 0x000FAB68
		bool WantEvent(int ID, int Cascade)
		{
			return false;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000FC96B File Offset: 0x000FAB6B
		bool HandleEvent(MinEvent E)
		{
			return true;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000FC96E File Offset: 0x000FAB6E
		void TurnTick(long TimeTick, int Amount)
		{
		}
	}
}
