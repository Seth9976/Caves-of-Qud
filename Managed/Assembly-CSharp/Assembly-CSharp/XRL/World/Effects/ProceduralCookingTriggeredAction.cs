using System;
using System.Reflection;

namespace XRL.World.Effects
{
	// Token: 0x02000C2E RID: 3118
	[Serializable]
	public class ProceduralCookingTriggeredAction : IEventHandler, IComposite
	{
		// Token: 0x06007CA9 RID: 31913 RVA: 0x002ED463 File Offset: 0x002EB663
		public virtual bool HandleEvent(AIAfterMissileEvent E)
		{
			return true;
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x002ED466 File Offset: 0x002EB666
		public virtual bool HandleEvent(AIAfterThrowEvent E)
		{
			return true;
		}

		// Token: 0x06007CAB RID: 31915 RVA: 0x002ED469 File Offset: 0x002EB669
		public virtual bool HandleEvent(AIBoredEvent E)
		{
			return true;
		}

		// Token: 0x06007CAC RID: 31916 RVA: 0x002ED46C File Offset: 0x002EB66C
		public virtual bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06007CAD RID: 31917 RVA: 0x002ED46F File Offset: 0x002EB66F
		public virtual bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06007CAE RID: 31918 RVA: 0x002ED472 File Offset: 0x002EB672
		public virtual bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06007CAF RID: 31919 RVA: 0x002ED475 File Offset: 0x002EB675
		public virtual bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06007CB0 RID: 31920 RVA: 0x002ED478 File Offset: 0x002EB678
		public virtual bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06007CB1 RID: 31921 RVA: 0x002ED47B File Offset: 0x002EB67B
		public virtual bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06007CB2 RID: 31922 RVA: 0x002ED47E File Offset: 0x002EB67E
		public virtual bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x002ED481 File Offset: 0x002EB681
		public virtual bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06007CB4 RID: 31924 RVA: 0x002ED484 File Offset: 0x002EB684
		public virtual bool HandleEvent(AIHelpBroadcastEvent E)
		{
			return true;
		}

		// Token: 0x06007CB5 RID: 31925 RVA: 0x002ED487 File Offset: 0x002EB687
		public virtual bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return true;
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x002ED48A File Offset: 0x002EB68A
		public virtual bool HandleEvent(ActorGetNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007CB7 RID: 31927 RVA: 0x002ED48D File Offset: 0x002EB68D
		public virtual bool HandleEvent(AddedToInventoryEvent E)
		{
			return true;
		}

		// Token: 0x06007CB8 RID: 31928 RVA: 0x002ED490 File Offset: 0x002EB690
		public virtual bool HandleEvent(AdjustTotalWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007CB9 RID: 31929 RVA: 0x002ED493 File Offset: 0x002EB693
		public virtual bool HandleEvent(AdjustValueEvent E)
		{
			return true;
		}

		// Token: 0x06007CBA RID: 31930 RVA: 0x002ED496 File Offset: 0x002EB696
		public virtual bool HandleEvent(AdjustVisibilityRadiusEvent E)
		{
			return true;
		}

		// Token: 0x06007CBB RID: 31931 RVA: 0x002ED499 File Offset: 0x002EB699
		public virtual bool HandleEvent(AdjustWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007CBC RID: 31932 RVA: 0x002ED49C File Offset: 0x002EB69C
		public virtual bool HandleEvent(AfterAddActiveObjectEvent E)
		{
			return true;
		}

		// Token: 0x06007CBD RID: 31933 RVA: 0x002ED49F File Offset: 0x002EB69F
		public virtual bool HandleEvent(AfterAddOpinionEvent E)
		{
			return true;
		}

		// Token: 0x06007CBE RID: 31934 RVA: 0x002ED4A2 File Offset: 0x002EB6A2
		public virtual bool HandleEvent(AfterAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x06007CBF RID: 31935 RVA: 0x002ED4A5 File Offset: 0x002EB6A5
		public virtual bool HandleEvent(AfterAfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x06007CC0 RID: 31936 RVA: 0x002ED4A8 File Offset: 0x002EB6A8
		public virtual bool HandleEvent(AfterBasicBestowalEvent E)
		{
			return true;
		}

		// Token: 0x06007CC1 RID: 31937 RVA: 0x002ED4AB File Offset: 0x002EB6AB
		public virtual bool HandleEvent(AfterChangePartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06007CC2 RID: 31938 RVA: 0x002ED4AE File Offset: 0x002EB6AE
		public virtual bool HandleEvent(AfterConsumeEvent E)
		{
			return true;
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x002ED4B1 File Offset: 0x002EB6B1
		public virtual bool HandleEvent(AfterContentsTakenEvent E)
		{
			return true;
		}

		// Token: 0x06007CC4 RID: 31940 RVA: 0x002ED4B4 File Offset: 0x002EB6B4
		public virtual bool HandleEvent(AfterConversationEvent E)
		{
			return true;
		}

		// Token: 0x06007CC5 RID: 31941 RVA: 0x002ED4B7 File Offset: 0x002EB6B7
		public virtual bool HandleEvent(AfterDieEvent E)
		{
			return true;
		}

		// Token: 0x06007CC6 RID: 31942 RVA: 0x002ED4BA File Offset: 0x002EB6BA
		public virtual bool HandleEvent(AfterDismemberEvent E)
		{
			return true;
		}

		// Token: 0x06007CC7 RID: 31943 RVA: 0x002ED4BD File Offset: 0x002EB6BD
		public virtual bool HandleEvent(AfterElementBestowalEvent E)
		{
			return true;
		}

		// Token: 0x06007CC8 RID: 31944 RVA: 0x002ED4C0 File Offset: 0x002EB6C0
		public virtual bool HandleEvent(AfterExamineCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x06007CC9 RID: 31945 RVA: 0x002ED4C3 File Offset: 0x002EB6C3
		public virtual bool HandleEvent(AfterExamineFailureEvent E)
		{
			return true;
		}

		// Token: 0x06007CCA RID: 31946 RVA: 0x002ED4C6 File Offset: 0x002EB6C6
		public virtual bool HandleEvent(AfterGameLoadedEvent E)
		{
			return true;
		}

		// Token: 0x06007CCB RID: 31947 RVA: 0x002ED4C9 File Offset: 0x002EB6C9
		public virtual bool HandleEvent(AfterInventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06007CCC RID: 31948 RVA: 0x002ED4CC File Offset: 0x002EB6CC
		public virtual bool HandleEvent(AfterLevelGainedEvent E)
		{
			return true;
		}

		// Token: 0x06007CCD RID: 31949 RVA: 0x002ED4CF File Offset: 0x002EB6CF
		public virtual bool HandleEvent(AfterMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06007CCE RID: 31950 RVA: 0x002ED4D2 File Offset: 0x002EB6D2
		public virtual bool HandleEvent(AfterMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06007CCF RID: 31951 RVA: 0x002ED4D5 File Offset: 0x002EB6D5
		public virtual bool HandleEvent(AfterMoveFailedEvent E)
		{
			return true;
		}

		// Token: 0x06007CD0 RID: 31952 RVA: 0x002ED4D8 File Offset: 0x002EB6D8
		public virtual bool HandleEvent(AfterObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06007CD1 RID: 31953 RVA: 0x002ED4DB File Offset: 0x002EB6DB
		public virtual bool HandleEvent(AfterPetEvent E)
		{
			return true;
		}

		// Token: 0x06007CD2 RID: 31954 RVA: 0x002ED4DE File Offset: 0x002EB6DE
		public virtual bool HandleEvent(AfterPilotChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007CD3 RID: 31955 RVA: 0x002ED4E1 File Offset: 0x002EB6E1
		public virtual bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007CD4 RID: 31956 RVA: 0x002ED4E4 File Offset: 0x002EB6E4
		public virtual bool HandleEvent(AfterPseudoRelicGeneratedEvent E)
		{
			return true;
		}

		// Token: 0x06007CD5 RID: 31957 RVA: 0x002ED4E7 File Offset: 0x002EB6E7
		public virtual bool HandleEvent(AfterReadBookEvent E)
		{
			return true;
		}

		// Token: 0x06007CD6 RID: 31958 RVA: 0x002ED4EA File Offset: 0x002EB6EA
		public virtual bool HandleEvent(AfterRelicGeneratedEvent E)
		{
			return true;
		}

		// Token: 0x06007CD7 RID: 31959 RVA: 0x002ED4ED File Offset: 0x002EB6ED
		public virtual bool HandleEvent(AfterRemoveActiveObjectEvent E)
		{
			return true;
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x002ED4F0 File Offset: 0x002EB6F0
		public virtual bool HandleEvent(AfterRemoveSkillEvent E)
		{
			return true;
		}

		// Token: 0x06007CD9 RID: 31961 RVA: 0x002ED4F3 File Offset: 0x002EB6F3
		public virtual bool HandleEvent(AfterReputationChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x002ED4F6 File Offset: 0x002EB6F6
		public virtual bool HandleEvent(AfterShieldBlockEvent E)
		{
			return true;
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x002ED4F9 File Offset: 0x002EB6F9
		public virtual bool HandleEvent(AfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x002ED4FC File Offset: 0x002EB6FC
		public virtual bool HandleEvent(AfterTravelEvent E)
		{
			return true;
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x002ED4FF File Offset: 0x002EB6FF
		public virtual bool HandleEvent(AfterZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06007CDE RID: 31966 RVA: 0x002ED502 File Offset: 0x002EB702
		public virtual bool HandleEvent(AllowInventoryStackEvent E)
		{
			return true;
		}

		// Token: 0x06007CDF RID: 31967 RVA: 0x002ED505 File Offset: 0x002EB705
		public virtual bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return true;
		}

		// Token: 0x06007CE0 RID: 31968 RVA: 0x002ED508 File Offset: 0x002EB708
		public virtual bool HandleEvent(AllowPolypPluckingEvent E)
		{
			return true;
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x002ED50B File Offset: 0x002EB70B
		public virtual bool HandleEvent(AllowTradeWithNoInventoryEvent E)
		{
			return true;
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x002ED50E File Offset: 0x002EB70E
		public virtual bool HandleEvent(AnimateEvent E)
		{
			return true;
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x002ED511 File Offset: 0x002EB711
		public virtual bool HandleEvent(AnyAutoCollectDramsEvent E)
		{
			return true;
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x002ED514 File Offset: 0x002EB714
		public virtual bool HandleEvent(AnyRegenerableLimbsEvent E)
		{
			return true;
		}

		// Token: 0x06007CE5 RID: 31973 RVA: 0x002ED517 File Offset: 0x002EB717
		public virtual bool HandleEvent(ApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x06007CE6 RID: 31974 RVA: 0x002ED51A File Offset: 0x002EB71A
		public virtual bool HandleEvent(AttackerBeforeTemperatureChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007CE7 RID: 31975 RVA: 0x002ED51D File Offset: 0x002EB71D
		public virtual bool HandleEvent(AttackerDealingDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007CE8 RID: 31976 RVA: 0x002ED520 File Offset: 0x002EB720
		public virtual bool HandleEvent(AttackerDealtDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007CE9 RID: 31977 RVA: 0x002ED523 File Offset: 0x002EB723
		public virtual bool HandleEvent(AttemptConversationEvent E)
		{
			return true;
		}

		// Token: 0x06007CEA RID: 31978 RVA: 0x002ED526 File Offset: 0x002EB726
		public virtual bool HandleEvent(AttemptToLandEvent E)
		{
			return true;
		}

		// Token: 0x06007CEB RID: 31979 RVA: 0x002ED529 File Offset: 0x002EB729
		public virtual bool HandleEvent(AutoexploreObjectEvent E)
		{
			return true;
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x002ED52C File Offset: 0x002EB72C
		public virtual bool HandleEvent(AwardXPEvent E)
		{
			return true;
		}

		// Token: 0x06007CED RID: 31981 RVA: 0x002ED52F File Offset: 0x002EB72F
		public virtual bool HandleEvent(AwardedXPEvent E)
		{
			return true;
		}

		// Token: 0x06007CEE RID: 31982 RVA: 0x002ED532 File Offset: 0x002EB732
		public virtual bool HandleEvent(AwardingXPEvent E)
		{
			return true;
		}

		// Token: 0x06007CEF RID: 31983 RVA: 0x002ED535 File Offset: 0x002EB735
		public virtual bool HandleEvent(BeforeAITakingActionEvent E)
		{
			return true;
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x002ED538 File Offset: 0x002EB738
		public virtual bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			return true;
		}

		// Token: 0x06007CF1 RID: 31985 RVA: 0x002ED53B File Offset: 0x002EB73B
		public virtual bool HandleEvent(BeforeAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x06007CF2 RID: 31986 RVA: 0x002ED53E File Offset: 0x002EB73E
		public virtual bool HandleEvent(BeforeAfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x002ED541 File Offset: 0x002EB741
		public virtual bool HandleEvent(BeforeApplyDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007CF4 RID: 31988 RVA: 0x002ED544 File Offset: 0x002EB744
		public virtual bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06007CF5 RID: 31989 RVA: 0x002ED547 File Offset: 0x002EB747
		public virtual bool HandleEvent(BeforeConsumeEvent E)
		{
			return true;
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x002ED54A File Offset: 0x002EB74A
		public virtual bool HandleEvent(BeforeConversationEvent E)
		{
			return true;
		}

		// Token: 0x06007CF7 RID: 31991 RVA: 0x002ED54D File Offset: 0x002EB74D
		public virtual bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x002ED550 File Offset: 0x002EB750
		public virtual bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x06007CF9 RID: 31993 RVA: 0x002ED553 File Offset: 0x002EB753
		public virtual bool HandleEvent(BeforeDetonateEvent E)
		{
			return true;
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x002ED556 File Offset: 0x002EB756
		public virtual bool HandleEvent(BeforeDieEvent E)
		{
			return true;
		}

		// Token: 0x06007CFB RID: 31995 RVA: 0x002ED559 File Offset: 0x002EB759
		public virtual bool HandleEvent(BeforeDismemberEvent E)
		{
			return true;
		}

		// Token: 0x06007CFC RID: 31996 RVA: 0x002ED55C File Offset: 0x002EB75C
		public virtual bool HandleEvent(BeforeFireMissileWeaponsEvent E)
		{
			return true;
		}

		// Token: 0x06007CFD RID: 31997 RVA: 0x002ED55F File Offset: 0x002EB75F
		public virtual bool HandleEvent(BeforeInteriorCollapseEvent E)
		{
			return true;
		}

		// Token: 0x06007CFE RID: 31998 RVA: 0x002ED562 File Offset: 0x002EB762
		public virtual bool HandleEvent(BeforeLevelGainedEvent E)
		{
			return true;
		}

		// Token: 0x06007CFF RID: 31999 RVA: 0x002ED565 File Offset: 0x002EB765
		public virtual bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x06007D00 RID: 32000 RVA: 0x002ED568 File Offset: 0x002EB768
		public virtual bool HandleEvent(BeforeMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06007D01 RID: 32001 RVA: 0x002ED56B File Offset: 0x002EB76B
		public virtual bool HandleEvent(BeforeMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x002ED56E File Offset: 0x002EB76E
		public virtual bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06007D03 RID: 32003 RVA: 0x002ED571 File Offset: 0x002EB771
		public virtual bool HandleEvent(BeforePilotChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007D04 RID: 32004 RVA: 0x002ED574 File Offset: 0x002EB774
		public virtual bool HandleEvent(BeforePlayMusicEvent E)
		{
			return true;
		}

		// Token: 0x06007D05 RID: 32005 RVA: 0x002ED577 File Offset: 0x002EB777
		public virtual bool HandleEvent(BeforeProjectileHitEvent E)
		{
			return true;
		}

		// Token: 0x06007D06 RID: 32006 RVA: 0x002ED57A File Offset: 0x002EB77A
		public virtual bool HandleEvent(BeforePullDownEvent E)
		{
			return true;
		}

		// Token: 0x06007D07 RID: 32007 RVA: 0x002ED57D File Offset: 0x002EB77D
		public virtual bool HandleEvent(BeforeRemoveSkillEvent E)
		{
			return true;
		}

		// Token: 0x06007D08 RID: 32008 RVA: 0x002ED580 File Offset: 0x002EB780
		public virtual bool HandleEvent(BeforeRenderEvent E)
		{
			return true;
		}

		// Token: 0x06007D09 RID: 32009 RVA: 0x002ED583 File Offset: 0x002EB783
		public virtual bool HandleEvent(BeforeSetFeelingEvent E)
		{
			return true;
		}

		// Token: 0x06007D0A RID: 32010 RVA: 0x002ED586 File Offset: 0x002EB786
		public virtual bool HandleEvent(BeforeShieldBlockEvent E)
		{
			return true;
		}

		// Token: 0x06007D0B RID: 32011 RVA: 0x002ED589 File Offset: 0x002EB789
		public virtual bool HandleEvent(BeforeSlamEvent E)
		{
			return true;
		}

		// Token: 0x06007D0C RID: 32012 RVA: 0x002ED58C File Offset: 0x002EB78C
		public virtual bool HandleEvent(BeforeTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06007D0D RID: 32013 RVA: 0x002ED58F File Offset: 0x002EB78F
		public virtual bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007D0E RID: 32014 RVA: 0x002ED592 File Offset: 0x002EB792
		public virtual bool HandleEvent(BeforeTookDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007D0F RID: 32015 RVA: 0x002ED595 File Offset: 0x002EB795
		public virtual bool HandleEvent(BeforeTravelDownEvent E)
		{
			return true;
		}

		// Token: 0x06007D10 RID: 32016 RVA: 0x002ED598 File Offset: 0x002EB798
		public virtual bool HandleEvent(BeforeUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06007D11 RID: 32017 RVA: 0x002ED59B File Offset: 0x002EB79B
		public virtual bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06007D12 RID: 32018 RVA: 0x002ED59E File Offset: 0x002EB79E
		public virtual bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06007D13 RID: 32019 RVA: 0x002ED5A1 File Offset: 0x002EB7A1
		public virtual bool HandleEvent(BeginConversationEvent E)
		{
			return true;
		}

		// Token: 0x06007D14 RID: 32020 RVA: 0x002ED5A4 File Offset: 0x002EB7A4
		public virtual bool HandleEvent(BeginMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06007D15 RID: 32021 RVA: 0x002ED5A7 File Offset: 0x002EB7A7
		public virtual bool HandleEvent(BeginMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06007D16 RID: 32022 RVA: 0x002ED5AA File Offset: 0x002EB7AA
		public virtual bool HandleEvent(BeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x002ED5AD File Offset: 0x002EB7AD
		public virtual bool HandleEvent(BeingConsumedEvent E)
		{
			return true;
		}

		// Token: 0x06007D18 RID: 32024 RVA: 0x002ED5B0 File Offset: 0x002EB7B0
		public virtual bool HandleEvent(BlasphemyPerformedEvent E)
		{
			return true;
		}

		// Token: 0x06007D19 RID: 32025 RVA: 0x002ED5B3 File Offset: 0x002EB7B3
		public virtual bool HandleEvent(BlocksRadarEvent E)
		{
			return true;
		}

		// Token: 0x06007D1A RID: 32026 RVA: 0x002ED5B6 File Offset: 0x002EB7B6
		public virtual bool HandleEvent(BodyPositionChangedEvent E)
		{
			return true;
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x002ED5B9 File Offset: 0x002EB7B9
		public virtual bool HandleEvent(BootSequenceAbortedEvent E)
		{
			return true;
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x002ED5BC File Offset: 0x002EB7BC
		public virtual bool HandleEvent(BootSequenceDoneEvent E)
		{
			return true;
		}

		// Token: 0x06007D1D RID: 32029 RVA: 0x002ED5BF File Offset: 0x002EB7BF
		public virtual bool HandleEvent(BootSequenceInitializedEvent E)
		{
			return true;
		}

		// Token: 0x06007D1E RID: 32030 RVA: 0x002ED5C2 File Offset: 0x002EB7C2
		public virtual bool HandleEvent(CanAcceptObjectEvent E)
		{
			return true;
		}

		// Token: 0x06007D1F RID: 32031 RVA: 0x002ED5C5 File Offset: 0x002EB7C5
		public virtual bool HandleEvent(CanApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x06007D20 RID: 32032 RVA: 0x002ED5C8 File Offset: 0x002EB7C8
		public virtual bool HandleEvent(CanBeDismemberedEvent E)
		{
			return true;
		}

		// Token: 0x06007D21 RID: 32033 RVA: 0x002ED5CB File Offset: 0x002EB7CB
		public virtual bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return true;
		}

		// Token: 0x06007D22 RID: 32034 RVA: 0x002ED5CE File Offset: 0x002EB7CE
		public virtual bool HandleEvent(CanBeMagneticallyManipulatedEvent E)
		{
			return true;
		}

		// Token: 0x06007D23 RID: 32035 RVA: 0x002ED5D1 File Offset: 0x002EB7D1
		public virtual bool HandleEvent(CanBeModdedEvent E)
		{
			return true;
		}

		// Token: 0x06007D24 RID: 32036 RVA: 0x002ED5D4 File Offset: 0x002EB7D4
		public virtual bool HandleEvent(CanBeNamedEvent E)
		{
			return true;
		}

		// Token: 0x06007D25 RID: 32037 RVA: 0x002ED5D7 File Offset: 0x002EB7D7
		public virtual bool HandleEvent(CanBeReplicatedEvent E)
		{
			return true;
		}

		// Token: 0x06007D26 RID: 32038 RVA: 0x002ED5DA File Offset: 0x002EB7DA
		public virtual bool HandleEvent(CanBeSlottedEvent E)
		{
			return true;
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x002ED5DD File Offset: 0x002EB7DD
		public virtual bool HandleEvent(CanBeTradedEvent E)
		{
			return true;
		}

		// Token: 0x06007D28 RID: 32040 RVA: 0x002ED5E0 File Offset: 0x002EB7E0
		public virtual bool HandleEvent(CanBeUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06007D29 RID: 32041 RVA: 0x002ED5E3 File Offset: 0x002EB7E3
		public virtual bool HandleEvent(CanChangeMovementModeEvent E)
		{
			return true;
		}

		// Token: 0x06007D2A RID: 32042 RVA: 0x002ED5E6 File Offset: 0x002EB7E6
		public virtual bool HandleEvent(CanDrinkEvent E)
		{
			return true;
		}

		// Token: 0x06007D2B RID: 32043 RVA: 0x002ED5E9 File Offset: 0x002EB7E9
		public virtual bool HandleEvent(CanEnterInteriorEvent E)
		{
			return true;
		}

		// Token: 0x06007D2C RID: 32044 RVA: 0x002ED5EC File Offset: 0x002EB7EC
		public virtual bool HandleEvent(CanFallEvent E)
		{
			return true;
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x002ED5EF File Offset: 0x002EB7EF
		public virtual bool HandleEvent(CanFireAllMissileWeaponsEvent E)
		{
			return true;
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x002ED5F2 File Offset: 0x002EB7F2
		public virtual bool HandleEvent(CanGiveDirectionsEvent E)
		{
			return true;
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x002ED5F5 File Offset: 0x002EB7F5
		public virtual bool HandleEvent(CanHaveConversationEvent E)
		{
			return true;
		}

		// Token: 0x06007D30 RID: 32048 RVA: 0x002ED5F8 File Offset: 0x002EB7F8
		public virtual bool HandleEvent(CanJoinPartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06007D31 RID: 32049 RVA: 0x002ED5FB File Offset: 0x002EB7FB
		public virtual bool HandleEvent(CanMissilePassForcefieldEvent E)
		{
			return true;
		}

		// Token: 0x06007D32 RID: 32050 RVA: 0x002ED5FE File Offset: 0x002EB7FE
		public virtual bool HandleEvent(CanReceiveEmpathyEvent E)
		{
			return true;
		}

		// Token: 0x06007D33 RID: 32051 RVA: 0x002ED601 File Offset: 0x002EB801
		public virtual bool HandleEvent(CanReceiveTelepathyEvent E)
		{
			return true;
		}

		// Token: 0x06007D34 RID: 32052 RVA: 0x002ED604 File Offset: 0x002EB804
		public virtual bool HandleEvent(CanRefreshAbilityEvent E)
		{
			return true;
		}

		// Token: 0x06007D35 RID: 32053 RVA: 0x002ED607 File Offset: 0x002EB807
		public virtual bool HandleEvent(CanSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06007D36 RID: 32054 RVA: 0x002ED60A File Offset: 0x002EB80A
		public virtual bool HandleEvent(CanStartConversationEvent E)
		{
			return true;
		}

		// Token: 0x06007D37 RID: 32055 RVA: 0x002ED60D File Offset: 0x002EB80D
		public virtual bool HandleEvent(CanTemperatureReturnToAmbientEvent E)
		{
			return true;
		}

		// Token: 0x06007D38 RID: 32056 RVA: 0x002ED610 File Offset: 0x002EB810
		public virtual bool HandleEvent(CanTradeEvent E)
		{
			return true;
		}

		// Token: 0x06007D39 RID: 32057 RVA: 0x002ED613 File Offset: 0x002EB813
		public virtual bool HandleEvent(CanTravelEvent E)
		{
			return true;
		}

		// Token: 0x06007D3A RID: 32058 RVA: 0x002ED616 File Offset: 0x002EB816
		public virtual bool HandleEvent(CarryingCapacityChangedEvent E)
		{
			return true;
		}

		// Token: 0x06007D3B RID: 32059 RVA: 0x002ED619 File Offset: 0x002EB819
		public virtual bool HandleEvent(CellChangedEvent E)
		{
			return true;
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x002ED61C File Offset: 0x002EB81C
		public virtual bool HandleEvent(CellDepletedEvent E)
		{
			return true;
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x002ED61F File Offset: 0x002EB81F
		public virtual bool HandleEvent(ChargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x002ED622 File Offset: 0x002EB822
		public virtual bool HandleEvent(ChargeUsedEvent E)
		{
			return true;
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x002ED625 File Offset: 0x002EB825
		public virtual bool HandleEvent(CheckAnythingToCleanEvent E)
		{
			return true;
		}

		// Token: 0x06007D40 RID: 32064 RVA: 0x002ED628 File Offset: 0x002EB828
		public virtual bool HandleEvent(CheckAnythingToCleanWithEvent E)
		{
			return true;
		}

		// Token: 0x06007D41 RID: 32065 RVA: 0x002ED62B File Offset: 0x002EB82B
		public virtual bool HandleEvent(CheckAnythingToCleanWithNearbyEvent E)
		{
			return true;
		}

		// Token: 0x06007D42 RID: 32066 RVA: 0x002ED62E File Offset: 0x002EB82E
		public virtual bool HandleEvent(CheckAttackableEvent E)
		{
			return true;
		}

		// Token: 0x06007D43 RID: 32067 RVA: 0x002ED631 File Offset: 0x002EB831
		public virtual bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return true;
		}

		// Token: 0x06007D44 RID: 32068 RVA: 0x002ED634 File Offset: 0x002EB834
		public virtual bool HandleEvent(CheckGasCanAffectEvent E)
		{
			return true;
		}

		// Token: 0x06007D45 RID: 32069 RVA: 0x002ED637 File Offset: 0x002EB837
		public virtual bool HandleEvent(CheckLoadAmmoEvent E)
		{
			return true;
		}

		// Token: 0x06007D46 RID: 32070 RVA: 0x002ED63A File Offset: 0x002EB83A
		public virtual bool HandleEvent(CheckOverburdenedOnStrengthUpdateEvent E)
		{
			return true;
		}

		// Token: 0x06007D47 RID: 32071 RVA: 0x002ED63D File Offset: 0x002EB83D
		public virtual bool HandleEvent(CheckPaintabilityEvent E)
		{
			return true;
		}

		// Token: 0x06007D48 RID: 32072 RVA: 0x002ED640 File Offset: 0x002EB840
		public virtual bool HandleEvent(CheckReadyToFireEvent E)
		{
			return true;
		}

		// Token: 0x06007D49 RID: 32073 RVA: 0x002ED643 File Offset: 0x002EB843
		public virtual bool HandleEvent(CheckRealityDistortionAccessibilityEvent E)
		{
			return true;
		}

		// Token: 0x06007D4A RID: 32074 RVA: 0x002ED646 File Offset: 0x002EB846
		public virtual bool HandleEvent(CheckRealityDistortionAdvisabilityEvent E)
		{
			return true;
		}

		// Token: 0x06007D4B RID: 32075 RVA: 0x002ED649 File Offset: 0x002EB849
		public virtual bool HandleEvent(CheckRealityDistortionUsabilityEvent E)
		{
			return true;
		}

		// Token: 0x06007D4C RID: 32076 RVA: 0x002ED64C File Offset: 0x002EB84C
		public virtual bool HandleEvent(CheckSpawnMergeEvent E)
		{
			return true;
		}

		// Token: 0x06007D4D RID: 32077 RVA: 0x002ED64F File Offset: 0x002EB84F
		public virtual bool HandleEvent(CheckTileChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x002ED652 File Offset: 0x002EB852
		public virtual bool HandleEvent(CheckUsesChargeWhileEquippedEvent E)
		{
			return true;
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x002ED655 File Offset: 0x002EB855
		public virtual bool HandleEvent(CleanItemsEvent E)
		{
			return true;
		}

		// Token: 0x06007D50 RID: 32080 RVA: 0x002ED658 File Offset: 0x002EB858
		public virtual bool HandleEvent(CollectBroadcastChargeEvent E)
		{
			return true;
		}

		// Token: 0x06007D51 RID: 32081 RVA: 0x002ED65B File Offset: 0x002EB85B
		public virtual bool HandleEvent(CommandEvent E)
		{
			return true;
		}

		// Token: 0x06007D52 RID: 32082 RVA: 0x002ED65E File Offset: 0x002EB85E
		public virtual bool HandleEvent(CommandReloadEvent E)
		{
			return true;
		}

		// Token: 0x06007D53 RID: 32083 RVA: 0x002ED661 File Offset: 0x002EB861
		public virtual bool HandleEvent(CommandReplaceCellEvent E)
		{
			return true;
		}

		// Token: 0x06007D54 RID: 32084 RVA: 0x002ED664 File Offset: 0x002EB864
		public virtual bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			return true;
		}

		// Token: 0x06007D55 RID: 32085 RVA: 0x002ED667 File Offset: 0x002EB867
		public virtual bool HandleEvent(CommandSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x002ED66A File Offset: 0x002EB86A
		public virtual bool HandleEvent(CommandTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x002ED66D File Offset: 0x002EB86D
		public virtual bool HandleEvent(ConfigureMissileVisualEffectEvent E)
		{
			return true;
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x002ED670 File Offset: 0x002EB870
		public virtual bool HandleEvent(ContainsAnyBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x002ED673 File Offset: 0x002EB873
		public virtual bool HandleEvent(ContainsBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x002ED676 File Offset: 0x002EB876
		public virtual bool HandleEvent(ContainsEvent E)
		{
			return true;
		}

		// Token: 0x06007D5B RID: 32091 RVA: 0x002ED679 File Offset: 0x002EB879
		public virtual bool HandleEvent(DamageConstantAdjustedEvent E)
		{
			return true;
		}

		// Token: 0x06007D5C RID: 32092 RVA: 0x002ED67C File Offset: 0x002EB87C
		public virtual bool HandleEvent(DamageDieSizeAdjustedEvent E)
		{
			return true;
		}

		// Token: 0x06007D5D RID: 32093 RVA: 0x002ED67F File Offset: 0x002EB87F
		public virtual bool HandleEvent(DeathEvent E)
		{
			return true;
		}

		// Token: 0x06007D5E RID: 32094 RVA: 0x002ED682 File Offset: 0x002EB882
		public virtual bool HandleEvent(DecorateDefaultEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x06007D5F RID: 32095 RVA: 0x002ED685 File Offset: 0x002EB885
		public virtual bool HandleEvent(DefendMeleeHitEvent E)
		{
			return true;
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x002ED688 File Offset: 0x002EB888
		public virtual bool HandleEvent(DefenderMissileHitEvent E)
		{
			return true;
		}

		// Token: 0x06007D61 RID: 32097 RVA: 0x002ED68B File Offset: 0x002EB88B
		public virtual bool HandleEvent(DerivationCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06007D62 RID: 32098 RVA: 0x002ED68E File Offset: 0x002EB88E
		public virtual bool HandleEvent(DidInitialEquipEvent E)
		{
			return true;
		}

		// Token: 0x06007D63 RID: 32099 RVA: 0x002ED691 File Offset: 0x002EB891
		public virtual bool HandleEvent(DidReequipEvent E)
		{
			return true;
		}

		// Token: 0x06007D64 RID: 32100 RVA: 0x002ED694 File Offset: 0x002EB894
		public virtual bool HandleEvent(DropOnDeathEvent E)
		{
			return true;
		}

		// Token: 0x06007D65 RID: 32101 RVA: 0x002ED697 File Offset: 0x002EB897
		public virtual bool HandleEvent(DroppedEvent E)
		{
			return true;
		}

		// Token: 0x06007D66 RID: 32102 RVA: 0x002ED69A File Offset: 0x002EB89A
		public virtual bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06007D67 RID: 32103 RVA: 0x002ED69D File Offset: 0x002EB89D
		public virtual bool HandleEvent(EarlyBeforeDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06007D68 RID: 32104 RVA: 0x002ED6A0 File Offset: 0x002EB8A0
		public virtual bool HandleEvent(EffectAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06007D69 RID: 32105 RVA: 0x002ED6A3 File Offset: 0x002EB8A3
		public virtual bool HandleEvent(EffectForceAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06007D6A RID: 32106 RVA: 0x002ED6A6 File Offset: 0x002EB8A6
		public virtual bool HandleEvent(EffectRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06007D6B RID: 32107 RVA: 0x002ED6A9 File Offset: 0x002EB8A9
		public virtual bool HandleEvent(EmbarkEvent E)
		{
			return true;
		}

		// Token: 0x06007D6C RID: 32108 RVA: 0x002ED6AC File Offset: 0x002EB8AC
		public virtual bool HandleEvent(EncounterChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007D6D RID: 32109 RVA: 0x002ED6AF File Offset: 0x002EB8AF
		public virtual bool HandleEvent(EncumbranceChangedEvent E)
		{
			return true;
		}

		// Token: 0x06007D6E RID: 32110 RVA: 0x002ED6B2 File Offset: 0x002EB8B2
		public virtual bool HandleEvent(EndActionEvent E)
		{
			return true;
		}

		// Token: 0x06007D6F RID: 32111 RVA: 0x002ED6B5 File Offset: 0x002EB8B5
		public virtual bool HandleEvent(EndTurnEvent E)
		{
			return true;
		}

		// Token: 0x06007D70 RID: 32112 RVA: 0x002ED6B8 File Offset: 0x002EB8B8
		public virtual bool HandleEvent(EnterCellEvent E)
		{
			return true;
		}

		// Token: 0x06007D71 RID: 32113 RVA: 0x002ED6BB File Offset: 0x002EB8BB
		public virtual bool HandleEvent(EnteredCellEvent E)
		{
			return true;
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x002ED6BE File Offset: 0x002EB8BE
		public virtual bool HandleEvent(EnteringCellEvent E)
		{
			return true;
		}

		// Token: 0x06007D73 RID: 32115 RVA: 0x002ED6C1 File Offset: 0x002EB8C1
		public virtual bool HandleEvent(EnteringZoneEvent E)
		{
			return true;
		}

		// Token: 0x06007D74 RID: 32116 RVA: 0x002ED6C4 File Offset: 0x002EB8C4
		public virtual bool HandleEvent(EnvironmentalUpdateEvent E)
		{
			return true;
		}

		// Token: 0x06007D75 RID: 32117 RVA: 0x002ED6C7 File Offset: 0x002EB8C7
		public virtual bool HandleEvent(EquippedEvent E)
		{
			return true;
		}

		// Token: 0x06007D76 RID: 32118 RVA: 0x002ED6CA File Offset: 0x002EB8CA
		public virtual bool HandleEvent(EquipperEquippedEvent E)
		{
			return true;
		}

		// Token: 0x06007D77 RID: 32119 RVA: 0x002ED6CD File Offset: 0x002EB8CD
		public virtual bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x002ED6D0 File Offset: 0x002EB8D0
		public virtual bool HandleEvent(ExamineFailureEvent E)
		{
			return true;
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x002ED6D3 File Offset: 0x002EB8D3
		public virtual bool HandleEvent(ExamineSuccessEvent E)
		{
			return true;
		}

		// Token: 0x06007D7A RID: 32122 RVA: 0x002ED6D6 File Offset: 0x002EB8D6
		public virtual bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			return true;
		}

		// Token: 0x06007D7B RID: 32123 RVA: 0x002ED6D9 File Offset: 0x002EB8D9
		public virtual bool HandleEvent(ExtremitiesMovedEvent E)
		{
			return true;
		}

		// Token: 0x06007D7C RID: 32124 RVA: 0x002ED6DC File Offset: 0x002EB8DC
		public virtual bool HandleEvent(FellDownEvent E)
		{
			return true;
		}

		// Token: 0x06007D7D RID: 32125 RVA: 0x002ED6DF File Offset: 0x002EB8DF
		public virtual bool HandleEvent(FindObjectByIdEvent E)
		{
			return true;
		}

		// Token: 0x06007D7E RID: 32126 RVA: 0x002ED6E2 File Offset: 0x002EB8E2
		public virtual bool HandleEvent(FinishChargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06007D7F RID: 32127 RVA: 0x002ED6E5 File Offset: 0x002EB8E5
		public virtual bool HandleEvent(FinishRechargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06007D80 RID: 32128 RVA: 0x002ED6E8 File Offset: 0x002EB8E8
		public virtual bool HandleEvent(FlushWeightCacheEvent E)
		{
			return true;
		}

		// Token: 0x06007D81 RID: 32129 RVA: 0x002ED6EB File Offset: 0x002EB8EB
		public virtual bool HandleEvent(ForceApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x06007D82 RID: 32130 RVA: 0x002ED6EE File Offset: 0x002EB8EE
		public virtual bool HandleEvent(FrozeEvent E)
		{
			return true;
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x002ED6F1 File Offset: 0x002EB8F1
		public virtual bool HandleEvent(GaveDirectionsEvent E)
		{
			return true;
		}

		// Token: 0x06007D84 RID: 32132 RVA: 0x002ED6F4 File Offset: 0x002EB8F4
		public virtual bool HandleEvent(GeneralAmnestyEvent E)
		{
			return true;
		}

		// Token: 0x06007D85 RID: 32133 RVA: 0x002ED6F7 File Offset: 0x002EB8F7
		public virtual bool HandleEvent(GenericCommandEvent E)
		{
			return true;
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x002ED6FA File Offset: 0x002EB8FA
		public virtual bool HandleEvent(GenericDeepNotifyEvent E)
		{
			return true;
		}

		// Token: 0x06007D87 RID: 32135 RVA: 0x002ED6FD File Offset: 0x002EB8FD
		public virtual bool HandleEvent(GenericDeepQueryEvent E)
		{
			return true;
		}

		// Token: 0x06007D88 RID: 32136 RVA: 0x002ED700 File Offset: 0x002EB900
		public virtual bool HandleEvent(GenericDeepRatingEvent E)
		{
			return true;
		}

		// Token: 0x06007D89 RID: 32137 RVA: 0x002ED703 File Offset: 0x002EB903
		public virtual bool HandleEvent(GenericNotifyEvent E)
		{
			return true;
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x002ED706 File Offset: 0x002EB906
		public virtual bool HandleEvent(GenericQueryEvent E)
		{
			return true;
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x002ED709 File Offset: 0x002EB909
		public virtual bool HandleEvent(GenericRatingEvent E)
		{
			return true;
		}

		// Token: 0x06007D8C RID: 32140 RVA: 0x002ED70C File Offset: 0x002EB90C
		public virtual bool HandleEvent(GetActivationPhaseEvent E)
		{
			return true;
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x002ED70F File Offset: 0x002EB90F
		public virtual bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007D8E RID: 32142 RVA: 0x002ED712 File Offset: 0x002EB912
		public virtual bool HandleEvent(GetAmbientLightEvent E)
		{
			return true;
		}

		// Token: 0x06007D8F RID: 32143 RVA: 0x002ED715 File Offset: 0x002EB915
		public virtual bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06007D90 RID: 32144 RVA: 0x002ED718 File Offset: 0x002EB918
		public virtual bool HandleEvent(GetApparentSpeciesEvent E)
		{
			return true;
		}

		// Token: 0x06007D91 RID: 32145 RVA: 0x002ED71B File Offset: 0x002EB91B
		public virtual bool HandleEvent(GetAttackerHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06007D92 RID: 32146 RVA: 0x002ED71E File Offset: 0x002EB91E
		public virtual bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06007D93 RID: 32147 RVA: 0x002ED721 File Offset: 0x002EB921
		public virtual bool HandleEvent(GetAutoCollectDramsEvent E)
		{
			return true;
		}

		// Token: 0x06007D94 RID: 32148 RVA: 0x002ED724 File Offset: 0x002EB924
		public virtual bool HandleEvent(GetAutoEquipPriorityEvent E)
		{
			return true;
		}

		// Token: 0x06007D95 RID: 32149 RVA: 0x002ED727 File Offset: 0x002EB927
		public virtual bool HandleEvent(GetAvailableComputePowerEvent E)
		{
			return true;
		}

		// Token: 0x06007D96 RID: 32150 RVA: 0x002ED72A File Offset: 0x002EB92A
		public virtual bool HandleEvent(GetBandagePerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06007D97 RID: 32151 RVA: 0x002ED72D File Offset: 0x002EB92D
		public virtual bool HandleEvent(GetBleedLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06007D98 RID: 32152 RVA: 0x002ED730 File Offset: 0x002EB930
		public virtual bool HandleEvent(GetCarriedWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007D99 RID: 32153 RVA: 0x002ED733 File Offset: 0x002EB933
		public virtual bool HandleEvent(GetCleaningItemsEvent E)
		{
			return true;
		}

		// Token: 0x06007D9A RID: 32154 RVA: 0x002ED736 File Offset: 0x002EB936
		public virtual bool HandleEvent(GetCleaningItemsNearbyEvent E)
		{
			return true;
		}

		// Token: 0x06007D9B RID: 32155 RVA: 0x002ED739 File Offset: 0x002EB939
		public virtual bool HandleEvent(GetCleaveAmountEvent E)
		{
			return true;
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x002ED73C File Offset: 0x002EB93C
		public virtual bool HandleEvent(GetCleaveMaxPenaltyEvent E)
		{
			return true;
		}

		// Token: 0x06007D9D RID: 32157 RVA: 0x002ED73F File Offset: 0x002EB93F
		public virtual bool HandleEvent(GetCompanionLimitEvent E)
		{
			return true;
		}

		// Token: 0x06007D9E RID: 32158 RVA: 0x002ED742 File Offset: 0x002EB942
		public virtual bool HandleEvent(GetCompanionStatusEvent E)
		{
			return true;
		}

		// Token: 0x06007D9F RID: 32159 RVA: 0x002ED745 File Offset: 0x002EB945
		public virtual bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x002ED748 File Offset: 0x002EB948
		public virtual bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007DA1 RID: 32161 RVA: 0x002ED74B File Offset: 0x002EB94B
		public virtual bool HandleEvent(GetContentsEvent E)
		{
			return true;
		}

		// Token: 0x06007DA2 RID: 32162 RVA: 0x002ED74E File Offset: 0x002EB94E
		public virtual bool HandleEvent(GetContextEvent E)
		{
			return true;
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x002ED751 File Offset: 0x002EB951
		public virtual bool HandleEvent(GetCookingActionsEvent E)
		{
			return true;
		}

		// Token: 0x06007DA4 RID: 32164 RVA: 0x002ED754 File Offset: 0x002EB954
		public virtual bool HandleEvent(GetCooldownEvent E)
		{
			return true;
		}

		// Token: 0x06007DA5 RID: 32165 RVA: 0x002ED757 File Offset: 0x002EB957
		public virtual bool HandleEvent(GetCriticalThresholdEvent E)
		{
			return true;
		}

		// Token: 0x06007DA6 RID: 32166 RVA: 0x002ED75A File Offset: 0x002EB95A
		public virtual bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06007DA7 RID: 32167 RVA: 0x002ED75D File Offset: 0x002EB95D
		public virtual bool HandleEvent(GetDebugInternalsEvent E)
		{
			return true;
		}

		// Token: 0x06007DA8 RID: 32168 RVA: 0x002ED760 File Offset: 0x002EB960
		public virtual bool HandleEvent(GetDefenderHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06007DA9 RID: 32169 RVA: 0x002ED763 File Offset: 0x002EB963
		public virtual bool HandleEvent(GetDefenderMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06007DAA RID: 32170 RVA: 0x002ED766 File Offset: 0x002EB966
		public virtual bool HandleEvent(GetDifficultyEvaluationEvent E)
		{
			return true;
		}

		// Token: 0x06007DAB RID: 32171 RVA: 0x002ED769 File Offset: 0x002EB969
		public virtual bool HandleEvent(GetDiseaseOnsetEvent E)
		{
			return true;
		}

		// Token: 0x06007DAC RID: 32172 RVA: 0x002ED76C File Offset: 0x002EB96C
		public virtual bool HandleEvent(GetDisplayNameEvent E)
		{
			return true;
		}

		// Token: 0x06007DAD RID: 32173 RVA: 0x002ED76F File Offset: 0x002EB96F
		public virtual bool HandleEvent(GetDisplayNamePenetrationColorEvent E)
		{
			return true;
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x002ED772 File Offset: 0x002EB972
		public virtual bool HandleEvent(GetDisplayStatBonusEvent E)
		{
			return true;
		}

		// Token: 0x06007DAF RID: 32175 RVA: 0x002ED775 File Offset: 0x002EB975
		public virtual bool HandleEvent(GetElectricalConductivityEvent E)
		{
			return true;
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x002ED778 File Offset: 0x002EB978
		public virtual bool HandleEvent(GetEnergyCostEvent E)
		{
			return true;
		}

		// Token: 0x06007DB1 RID: 32177 RVA: 0x002ED77B File Offset: 0x002EB97B
		public virtual bool HandleEvent(GetExtraPhysicalFeaturesEvent E)
		{
			return true;
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x002ED77E File Offset: 0x002EB97E
		public virtual bool HandleEvent(GetExtrinsicValueEvent E)
		{
			return true;
		}

		// Token: 0x06007DB3 RID: 32179 RVA: 0x002ED781 File Offset: 0x002EB981
		public virtual bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007DB4 RID: 32180 RVA: 0x002ED784 File Offset: 0x002EB984
		public virtual bool HandleEvent(GetFactionRankEvent E)
		{
			return true;
		}

		// Token: 0x06007DB5 RID: 32181 RVA: 0x002ED787 File Offset: 0x002EB987
		public virtual bool HandleEvent(GetFeelingEvent E)
		{
			return true;
		}

		// Token: 0x06007DB6 RID: 32182 RVA: 0x002ED78A File Offset: 0x002EB98A
		public virtual bool HandleEvent(GetFirefightingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DB7 RID: 32183 RVA: 0x002ED78D File Offset: 0x002EB98D
		public virtual bool HandleEvent(GetFixedMissileSpreadEvent E)
		{
			return true;
		}

		// Token: 0x06007DB8 RID: 32184 RVA: 0x002ED790 File Offset: 0x002EB990
		public virtual bool HandleEvent(GetFreeDramsEvent E)
		{
			return true;
		}

		// Token: 0x06007DB9 RID: 32185 RVA: 0x002ED793 File Offset: 0x002EB993
		public virtual bool HandleEvent(GetGameObjectSortEvent E)
		{
			return true;
		}

		// Token: 0x06007DBA RID: 32186 RVA: 0x002ED796 File Offset: 0x002EB996
		public virtual bool HandleEvent(GetGenderEvent E)
		{
			return true;
		}

		// Token: 0x06007DBB RID: 32187 RVA: 0x002ED799 File Offset: 0x002EB999
		public virtual bool HandleEvent(GetHostileWalkRadiusEvent E)
		{
			return true;
		}

		// Token: 0x06007DBC RID: 32188 RVA: 0x002ED79C File Offset: 0x002EB99C
		public virtual bool HandleEvent(GetHostilityRecognitionLimitsEvent E)
		{
			return true;
		}

		// Token: 0x06007DBD RID: 32189 RVA: 0x002ED79F File Offset: 0x002EB99F
		public virtual bool HandleEvent(GetIdealTemperatureEvent E)
		{
			return true;
		}

		// Token: 0x06007DBE RID: 32190 RVA: 0x002ED7A2 File Offset: 0x002EB9A2
		public virtual bool HandleEvent(GetIntrinsicValueEvent E)
		{
			return true;
		}

		// Token: 0x06007DBF RID: 32191 RVA: 0x002ED7A5 File Offset: 0x002EB9A5
		public virtual bool HandleEvent(GetIntrinsicWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007DC0 RID: 32192 RVA: 0x002ED7A8 File Offset: 0x002EB9A8
		public virtual bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			return true;
		}

		// Token: 0x06007DC1 RID: 32193 RVA: 0x002ED7AB File Offset: 0x002EB9AB
		public virtual bool HandleEvent(GetInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06007DC2 RID: 32194 RVA: 0x002ED7AE File Offset: 0x002EB9AE
		public virtual bool HandleEvent(GetInventoryCategoryEvent E)
		{
			return true;
		}

		// Token: 0x06007DC3 RID: 32195 RVA: 0x002ED7B1 File Offset: 0x002EB9B1
		public virtual bool HandleEvent(GetItemElementsEvent E)
		{
			return true;
		}

		// Token: 0x06007DC4 RID: 32196 RVA: 0x002ED7B4 File Offset: 0x002EB9B4
		public virtual bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06007DC5 RID: 32197 RVA: 0x002ED7B7 File Offset: 0x002EB9B7
		public virtual bool HandleEvent(GetKineticResistanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x002ED7BA File Offset: 0x002EB9BA
		public virtual bool HandleEvent(GetLevelUpDiceEvent E)
		{
			return true;
		}

		// Token: 0x06007DC7 RID: 32199 RVA: 0x002ED7BD File Offset: 0x002EB9BD
		public virtual bool HandleEvent(GetLevelUpPointsEvent E)
		{
			return true;
		}

		// Token: 0x06007DC8 RID: 32200 RVA: 0x002ED7C0 File Offset: 0x002EB9C0
		public virtual bool HandleEvent(GetLevelUpSkillPointsEvent E)
		{
			return true;
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x002ED7C3 File Offset: 0x002EB9C3
		public virtual bool HandleEvent(GetLostChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DCA RID: 32202 RVA: 0x002ED7C6 File Offset: 0x002EB9C6
		public virtual bool HandleEvent(GetMatterPhaseEvent E)
		{
			return true;
		}

		// Token: 0x06007DCB RID: 32203 RVA: 0x002ED7C9 File Offset: 0x002EB9C9
		public virtual bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007DCC RID: 32204 RVA: 0x002ED7CC File Offset: 0x002EB9CC
		public virtual bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			return true;
		}

		// Token: 0x06007DCD RID: 32205 RVA: 0x002ED7CF File Offset: 0x002EB9CF
		public virtual bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x002ED7D2 File Offset: 0x002EB9D2
		public virtual bool HandleEvent(GetMeleeAttacksEvent E)
		{
			return true;
		}

		// Token: 0x06007DCF RID: 32207 RVA: 0x002ED7D5 File Offset: 0x002EB9D5
		public virtual bool HandleEvent(GetMissileCoverPercentageEvent E)
		{
			return true;
		}

		// Token: 0x06007DD0 RID: 32208 RVA: 0x002ED7D8 File Offset: 0x002EB9D8
		public virtual bool HandleEvent(GetMissileStatusColorEvent E)
		{
			return true;
		}

		// Token: 0x06007DD1 RID: 32209 RVA: 0x002ED7DB File Offset: 0x002EB9DB
		public virtual bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x002ED7DE File Offset: 0x002EB9DE
		public virtual bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			return true;
		}

		// Token: 0x06007DD3 RID: 32211 RVA: 0x002ED7E1 File Offset: 0x002EB9E1
		public virtual bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			return true;
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x002ED7E4 File Offset: 0x002EB9E4
		public virtual bool HandleEvent(GetModRarityWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x002ED7E7 File Offset: 0x002EB9E7
		public virtual bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			return true;
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x002ED7EA File Offset: 0x002EB9EA
		public virtual bool HandleEvent(GetMutationTermEvent E)
		{
			return true;
		}

		// Token: 0x06007DD7 RID: 32215 RVA: 0x002ED7ED File Offset: 0x002EB9ED
		public virtual bool HandleEvent(GetNamingBestowalChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DD8 RID: 32216 RVA: 0x002ED7F0 File Offset: 0x002EB9F0
		public virtual bool HandleEvent(GetNamingChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x002ED7F3 File Offset: 0x002EB9F3
		public virtual bool HandleEvent(GetNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007DDA RID: 32218 RVA: 0x002ED7F6 File Offset: 0x002EB9F6
		public virtual bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			return true;
		}

		// Token: 0x06007DDB RID: 32219 RVA: 0x002ED7F9 File Offset: 0x002EB9F9
		public virtual bool HandleEvent(GetOverloadChargeEvent E)
		{
			return true;
		}

		// Token: 0x06007DDC RID: 32220 RVA: 0x002ED7FC File Offset: 0x002EB9FC
		public virtual bool HandleEvent(GetPartyLeaderFollowDistanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DDD RID: 32221 RVA: 0x002ED7FF File Offset: 0x002EB9FF
		public virtual bool HandleEvent(GetPointsOfInterestEvent E)
		{
			return true;
		}

		// Token: 0x06007DDE RID: 32222 RVA: 0x002ED802 File Offset: 0x002EBA02
		public virtual bool HandleEvent(GetPowerLoadLevelEvent E)
		{
			return true;
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x002ED805 File Offset: 0x002EBA05
		public virtual bool HandleEvent(GetPrecognitionRestoreGameStateEvent E)
		{
			return true;
		}

		// Token: 0x06007DE0 RID: 32224 RVA: 0x002ED808 File Offset: 0x002EBA08
		public virtual bool HandleEvent(GetPreferredLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06007DE1 RID: 32225 RVA: 0x002ED80B File Offset: 0x002EBA0B
		public virtual bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06007DE2 RID: 32226 RVA: 0x002ED80E File Offset: 0x002EBA0E
		public virtual bool HandleEvent(GetProjectileObjectEvent E)
		{
			return true;
		}

		// Token: 0x06007DE3 RID: 32227 RVA: 0x002ED811 File Offset: 0x002EBA11
		public virtual bool HandleEvent(GetPropertyModDescription E)
		{
			return true;
		}

		// Token: 0x06007DE4 RID: 32228 RVA: 0x002ED814 File Offset: 0x002EBA14
		public virtual bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x06007DE5 RID: 32229 RVA: 0x002ED817 File Offset: 0x002EBA17
		public virtual bool HandleEvent(GetPsychicGlimmerEvent E)
		{
			return true;
		}

		// Token: 0x06007DE6 RID: 32230 RVA: 0x002ED81A File Offset: 0x002EBA1A
		public virtual bool HandleEvent(GetRandomBuyChimericBodyPartRollsEvent E)
		{
			return true;
		}

		// Token: 0x06007DE7 RID: 32231 RVA: 0x002ED81D File Offset: 0x002EBA1D
		public virtual bool HandleEvent(GetRandomBuyMutationCountEvent E)
		{
			return true;
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x002ED820 File Offset: 0x002EBA20
		public virtual bool HandleEvent(GetRealityStabilizationPenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06007DE9 RID: 32233 RVA: 0x002ED823 File Offset: 0x002EBA23
		public virtual bool HandleEvent(GetRebukeLevelEvent E)
		{
			return true;
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x002ED826 File Offset: 0x002EBA26
		public virtual bool HandleEvent(GetRecoilersEvent E)
		{
			return true;
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x002ED829 File Offset: 0x002EBA29
		public virtual bool HandleEvent(GetReplaceCellInteractionsEvent E)
		{
			return true;
		}

		// Token: 0x06007DEC RID: 32236 RVA: 0x002ED82C File Offset: 0x002EBA2C
		public virtual bool HandleEvent(GetRespiratoryAgentPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x002ED82F File Offset: 0x002EBA2F
		public virtual bool HandleEvent(GetRitualSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x002ED832 File Offset: 0x002EBA32
		public virtual bool HandleEvent(GetRunningBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x002ED835 File Offset: 0x002EBA35
		public virtual bool HandleEvent(GetScanTypeEvent E)
		{
			return true;
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x002ED838 File Offset: 0x002EBA38
		public virtual bool HandleEvent(GetShieldBlockPreferenceEvent E)
		{
			return true;
		}

		// Token: 0x06007DF1 RID: 32241 RVA: 0x002ED83B File Offset: 0x002EBA3B
		public virtual bool HandleEvent(GetShieldSlamDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x002ED83E File Offset: 0x002EBA3E
		public virtual bool HandleEvent(GetShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x002ED841 File Offset: 0x002EBA41
		public virtual bool HandleEvent(GetSkillEffectChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x002ED844 File Offset: 0x002EBA44
		public virtual bool HandleEvent(GetSlotsRequiredEvent E)
		{
			return true;
		}

		// Token: 0x06007DF5 RID: 32245 RVA: 0x002ED847 File Offset: 0x002EBA47
		public virtual bool HandleEvent(GetSlottedInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06007DF6 RID: 32246 RVA: 0x002ED84A File Offset: 0x002EBA4A
		public virtual bool HandleEvent(GetSocialSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x002ED84D File Offset: 0x002EBA4D
		public virtual bool HandleEvent(GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x002ED850 File Offset: 0x002EBA50
		public virtual bool HandleEvent(GetSpaceTimeAnomalyEmergencePermillageChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DF9 RID: 32249 RVA: 0x002ED853 File Offset: 0x002EBA53
		public virtual bool HandleEvent(GetSpecialEffectChanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x002ED856 File Offset: 0x002EBA56
		public virtual bool HandleEvent(GetSpringinessEvent E)
		{
			return true;
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x002ED859 File Offset: 0x002EBA59
		public virtual bool HandleEvent(GetSprintDurationEvent E)
		{
			return true;
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x002ED85C File Offset: 0x002EBA5C
		public virtual bool HandleEvent(GetStorableDramsEvent E)
		{
			return true;
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x002ED85F File Offset: 0x002EBA5F
		public virtual bool HandleEvent(GetSwimmingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x002ED862 File Offset: 0x002EBA62
		public virtual bool HandleEvent(GetThrowProfileEvent E)
		{
			return true;
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x002ED865 File Offset: 0x002EBA65
		public virtual bool HandleEvent(GetThrownWeaponFlexPhaseProviderEvent E)
		{
			return true;
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x002ED868 File Offset: 0x002EBA68
		public virtual bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x002ED86B File Offset: 0x002EBA6B
		public virtual bool HandleEvent(GetThrownWeaponRangeEvent E)
		{
			return true;
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x002ED86E File Offset: 0x002EBA6E
		public virtual bool HandleEvent(GetTinkeringBonusEvent E)
		{
			return true;
		}

		// Token: 0x06007E03 RID: 32259 RVA: 0x002ED871 File Offset: 0x002EBA71
		public virtual bool HandleEvent(GetToHitModifierEvent E)
		{
			return true;
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x002ED874 File Offset: 0x002EBA74
		public virtual bool HandleEvent(GetTonicCapacityEvent E)
		{
			return true;
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x002ED877 File Offset: 0x002EBA77
		public virtual bool HandleEvent(GetTonicDosageEvent E)
		{
			return true;
		}

		// Token: 0x06007E06 RID: 32262 RVA: 0x002ED87A File Offset: 0x002EBA7A
		public virtual bool HandleEvent(GetTonicDurationEvent E)
		{
			return true;
		}

		// Token: 0x06007E07 RID: 32263 RVA: 0x002ED87D File Offset: 0x002EBA7D
		public virtual bool HandleEvent(GetTradePerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06007E08 RID: 32264 RVA: 0x002ED880 File Offset: 0x002EBA80
		public virtual bool HandleEvent(GetTransitiveLocationEvent E)
		{
			return true;
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x002ED883 File Offset: 0x002EBA83
		public virtual bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x002ED886 File Offset: 0x002EBA86
		public virtual bool HandleEvent(GetUtilityScoreEvent E)
		{
			return true;
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x002ED889 File Offset: 0x002EBA89
		public virtual bool HandleEvent(GetWadingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x002ED88C File Offset: 0x002EBA8C
		public virtual bool HandleEvent(GetWaterRitualCostEvent E)
		{
			return true;
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x002ED88F File Offset: 0x002EBA8F
		public virtual bool HandleEvent(GetWaterRitualLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x002ED892 File Offset: 0x002EBA92
		public virtual bool HandleEvent(GetWaterRitualReputationAmountEvent E)
		{
			return true;
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x002ED895 File Offset: 0x002EBA95
		public virtual bool HandleEvent(GetWaterRitualSecretWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x002ED898 File Offset: 0x002EBA98
		public virtual bool HandleEvent(GetWaterRitualSellSecretBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x002ED89B File Offset: 0x002EBA9B
		public virtual bool HandleEvent(GetWeaponHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06007E12 RID: 32274 RVA: 0x002ED89E File Offset: 0x002EBA9E
		public virtual bool HandleEvent(GetWeaponMeleeAttacksEvent E)
		{
			return true;
		}

		// Token: 0x06007E13 RID: 32275 RVA: 0x002ED8A1 File Offset: 0x002EBAA1
		public virtual bool HandleEvent(GetWeaponMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06007E14 RID: 32276 RVA: 0x002ED8A4 File Offset: 0x002EBAA4
		public virtual bool HandleEvent(GetZoneEvent E)
		{
			return true;
		}

		// Token: 0x06007E15 RID: 32277 RVA: 0x002ED8A7 File Offset: 0x002EBAA7
		public virtual bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			return true;
		}

		// Token: 0x06007E16 RID: 32278 RVA: 0x002ED8AA File Offset: 0x002EBAAA
		public virtual bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			return true;
		}

		// Token: 0x06007E17 RID: 32279 RVA: 0x002ED8AD File Offset: 0x002EBAAD
		public virtual bool HandleEvent(GiveDramsEvent E)
		{
			return true;
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x002ED8B0 File Offset: 0x002EBAB0
		public virtual bool HandleEvent(GlimmerChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x002ED8B3 File Offset: 0x002EBAB3
		public virtual bool HandleEvent(GravitationEvent E)
		{
			return true;
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x002ED8B6 File Offset: 0x002EBAB6
		public virtual bool HandleEvent(HasBeenReadEvent E)
		{
			return true;
		}

		// Token: 0x06007E1B RID: 32283 RVA: 0x002ED8B9 File Offset: 0x002EBAB9
		public virtual bool HandleEvent(HasBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x002ED8BC File Offset: 0x002EBABC
		public virtual bool HandleEvent(HasFlammableEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x002ED8BF File Offset: 0x002EBABF
		public virtual bool HandleEvent(HasFlammableEquipmentOrInventoryEvent E)
		{
			return true;
		}

		// Token: 0x06007E1E RID: 32286 RVA: 0x002ED8C2 File Offset: 0x002EBAC2
		public virtual bool HandleEvent(HealsNaturallyEvent E)
		{
			return true;
		}

		// Token: 0x06007E1F RID: 32287 RVA: 0x002ED8C5 File Offset: 0x002EBAC5
		public virtual bool HandleEvent(IAICommandListEvent E)
		{
			return true;
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x002ED8C8 File Offset: 0x002EBAC8
		public virtual bool HandleEvent(IAIItemCommandListEvent E)
		{
			return true;
		}

		// Token: 0x06007E21 RID: 32289 RVA: 0x002ED8CB File Offset: 0x002EBACB
		public virtual bool HandleEvent(IAIMoveCommandListEvent E)
		{
			return true;
		}

		// Token: 0x06007E22 RID: 32290 RVA: 0x002ED8CE File Offset: 0x002EBACE
		public virtual bool HandleEvent(IActOnItemEvent E)
		{
			return true;
		}

		// Token: 0x06007E23 RID: 32291 RVA: 0x002ED8D1 File Offset: 0x002EBAD1
		public virtual bool HandleEvent(IActualEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x002ED8D4 File Offset: 0x002EBAD4
		public virtual bool HandleEvent(IAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x002ED8D7 File Offset: 0x002EBAD7
		public virtual bool HandleEvent(IAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007E26 RID: 32294 RVA: 0x002ED8DA File Offset: 0x002EBADA
		public virtual bool HandleEvent(IBootSequenceEvent E)
		{
			return true;
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x002ED8DD File Offset: 0x002EBADD
		public virtual bool HandleEvent(IChargeConsumptionEvent E)
		{
			return true;
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x002ED8E0 File Offset: 0x002EBAE0
		public virtual bool HandleEvent(IChargeEvent E)
		{
			return true;
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x002ED8E3 File Offset: 0x002EBAE3
		public virtual bool HandleEvent(IChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x002ED8E6 File Offset: 0x002EBAE6
		public virtual bool HandleEvent(IChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06007E2B RID: 32299 RVA: 0x002ED8E9 File Offset: 0x002EBAE9
		public virtual bool HandleEvent(IConsumeEvent E)
		{
			return true;
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x002ED8EC File Offset: 0x002EBAEC
		public virtual bool HandleEvent(IConversationMinEvent E)
		{
			return true;
		}

		// Token: 0x06007E2D RID: 32301 RVA: 0x002ED8EF File Offset: 0x002EBAEF
		public virtual bool HandleEvent(IDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x002ED8F2 File Offset: 0x002EBAF2
		public virtual bool HandleEvent(IDeathEvent E)
		{
			return true;
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x002ED8F5 File Offset: 0x002EBAF5
		public virtual bool HandleEvent(IDerivationEvent E)
		{
			return true;
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x002ED8F8 File Offset: 0x002EBAF8
		public virtual bool HandleEvent(IDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x002ED8FB File Offset: 0x002EBAFB
		public virtual bool HandleEvent(IEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x002ED8FE File Offset: 0x002EBAFE
		public virtual bool HandleEvent(IExamineEvent E)
		{
			return true;
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x002ED901 File Offset: 0x002EBB01
		public virtual bool HandleEvent(IFinalChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x002ED904 File Offset: 0x002EBB04
		public virtual bool HandleEvent(IHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x002ED907 File Offset: 0x002EBB07
		public virtual bool HandleEvent(IInitialChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x002ED90A File Offset: 0x002EBB0A
		public virtual bool HandleEvent(IInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x002ED90D File Offset: 0x002EBB0D
		public virtual bool HandleEvent(ILimbRegenerationEvent E)
		{
			return true;
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x002ED910 File Offset: 0x002EBB10
		public virtual bool HandleEvent(ILiquidEvent E)
		{
			return true;
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x002ED913 File Offset: 0x002EBB13
		public virtual bool HandleEvent(IMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x002ED916 File Offset: 0x002EBB16
		public virtual bool HandleEvent(IMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x002ED919 File Offset: 0x002EBB19
		public virtual bool HandleEvent(IMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x002ED91C File Offset: 0x002EBB1C
		public virtual bool HandleEvent(INavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x002ED91F File Offset: 0x002EBB1F
		public virtual bool HandleEvent(IObjectCellInteractionEvent E)
		{
			return true;
		}

		// Token: 0x06007E3E RID: 32318 RVA: 0x002ED922 File Offset: 0x002EBB22
		public virtual bool HandleEvent(IObjectCreationEvent E)
		{
			return true;
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x002ED925 File Offset: 0x002EBB25
		public virtual bool HandleEvent(IObjectInventoryInteractionEvent E)
		{
			return true;
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x002ED928 File Offset: 0x002EBB28
		public virtual bool HandleEvent(IQuestEvent E)
		{
			return true;
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x002ED92B File Offset: 0x002EBB2B
		public virtual bool HandleEvent(IRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x002ED92E File Offset: 0x002EBB2E
		public virtual bool HandleEvent(IReplicationEvent E)
		{
			return true;
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x002ED931 File Offset: 0x002EBB31
		public virtual bool HandleEvent(ISaveEvent E)
		{
			return true;
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x002ED934 File Offset: 0x002EBB34
		public virtual bool HandleEvent(IShallowZoneEvent E)
		{
			return true;
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x002ED937 File Offset: 0x002EBB37
		public virtual bool HandleEvent(IShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x002ED93A File Offset: 0x002EBB3A
		public virtual bool HandleEvent(ITravelEvent E)
		{
			return true;
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x002ED93D File Offset: 0x002EBB3D
		public virtual bool HandleEvent(IValueEvent E)
		{
			return true;
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x002ED940 File Offset: 0x002EBB40
		public virtual bool HandleEvent(IWeightEvent E)
		{
			return true;
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x002ED943 File Offset: 0x002EBB43
		public virtual bool HandleEvent(IXPEvent E)
		{
			return true;
		}

		// Token: 0x06007E4A RID: 32330 RVA: 0x002ED946 File Offset: 0x002EBB46
		public virtual bool HandleEvent(IZoneEvent E)
		{
			return true;
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x002ED949 File Offset: 0x002EBB49
		public virtual bool HandleEvent(IZoneOnlyEvent E)
		{
			return true;
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x002ED94C File Offset: 0x002EBB4C
		public virtual bool HandleEvent(IdleQueryEvent E)
		{
			return true;
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x002ED94F File Offset: 0x002EBB4F
		public virtual bool HandleEvent(ImplantAddedEvent E)
		{
			return true;
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x002ED952 File Offset: 0x002EBB52
		public virtual bool HandleEvent(ImplantRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x002ED955 File Offset: 0x002EBB55
		public virtual bool HandleEvent(ImplantedEvent E)
		{
			return true;
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x002ED958 File Offset: 0x002EBB58
		public virtual bool HandleEvent(InduceVomitingEvent E)
		{
			return true;
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x002ED95B File Offset: 0x002EBB5B
		public virtual bool HandleEvent(InductionChargeEvent E)
		{
			return true;
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x002ED95E File Offset: 0x002EBB5E
		public virtual bool HandleEvent(InteriorZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x002ED961 File Offset: 0x002EBB61
		public virtual bool HandleEvent(InterruptAutowalkEvent E)
		{
			return true;
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x002ED964 File Offset: 0x002EBB64
		public virtual bool HandleEvent(InventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x002ED967 File Offset: 0x002EBB67
		public virtual bool HandleEvent(IsAdaptivePenetrationActiveEvent E)
		{
			return true;
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x002ED96A File Offset: 0x002EBB6A
		public virtual bool HandleEvent(IsAfflictionEvent E)
		{
			return true;
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x002ED96D File Offset: 0x002EBB6D
		public virtual bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			return true;
		}

		// Token: 0x06007E58 RID: 32344 RVA: 0x002ED970 File Offset: 0x002EBB70
		public virtual bool HandleEvent(IsExplosiveEvent E)
		{
			return true;
		}

		// Token: 0x06007E59 RID: 32345 RVA: 0x002ED973 File Offset: 0x002EBB73
		public virtual bool HandleEvent(IsMutantEvent E)
		{
			return true;
		}

		// Token: 0x06007E5A RID: 32346 RVA: 0x002ED976 File Offset: 0x002EBB76
		public virtual bool HandleEvent(IsOverloadableEvent E)
		{
			return true;
		}

		// Token: 0x06007E5B RID: 32347 RVA: 0x002ED979 File Offset: 0x002EBB79
		public virtual bool HandleEvent(IsRepairableEvent E)
		{
			return true;
		}

		// Token: 0x06007E5C RID: 32348 RVA: 0x002ED97C File Offset: 0x002EBB7C
		public virtual bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return true;
		}

		// Token: 0x06007E5D RID: 32349 RVA: 0x002ED97F File Offset: 0x002EBB7F
		public virtual bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			return true;
		}

		// Token: 0x06007E5E RID: 32350 RVA: 0x002ED982 File Offset: 0x002EBB82
		public virtual bool HandleEvent(IsTrueKinEvent E)
		{
			return true;
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x002ED985 File Offset: 0x002EBB85
		public virtual bool HandleEvent(IsVehicleOperationalEvent E)
		{
			return true;
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x002ED988 File Offset: 0x002EBB88
		public virtual bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			return true;
		}

		// Token: 0x06007E61 RID: 32353 RVA: 0x002ED98B File Offset: 0x002EBB8B
		public virtual bool HandleEvent(JoinedPartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x002ED98E File Offset: 0x002EBB8E
		public virtual bool HandleEvent(JumpedEvent E)
		{
			return true;
		}

		// Token: 0x06007E63 RID: 32355 RVA: 0x002ED991 File Offset: 0x002EBB91
		public virtual bool HandleEvent(KilledEvent E)
		{
			return true;
		}

		// Token: 0x06007E64 RID: 32356 RVA: 0x002ED994 File Offset: 0x002EBB94
		public virtual bool HandleEvent(KilledPlayerEvent E)
		{
			return true;
		}

		// Token: 0x06007E65 RID: 32357 RVA: 0x002ED997 File Offset: 0x002EBB97
		public virtual bool HandleEvent(LateBeforeApplyDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007E66 RID: 32358 RVA: 0x002ED99A File Offset: 0x002EBB9A
		public virtual bool HandleEvent(LeaveCellEvent E)
		{
			return true;
		}

		// Token: 0x06007E67 RID: 32359 RVA: 0x002ED99D File Offset: 0x002EBB9D
		public virtual bool HandleEvent(LeavingCellEvent E)
		{
			return true;
		}

		// Token: 0x06007E68 RID: 32360 RVA: 0x002ED9A0 File Offset: 0x002EBBA0
		public virtual bool HandleEvent(LeftCellEvent E)
		{
			return true;
		}

		// Token: 0x06007E69 RID: 32361 RVA: 0x002ED9A3 File Offset: 0x002EBBA3
		public virtual bool HandleEvent(LiquidMixedEvent E)
		{
			return true;
		}

		// Token: 0x06007E6A RID: 32362 RVA: 0x002ED9A6 File Offset: 0x002EBBA6
		public virtual bool HandleEvent(LoadAmmoEvent E)
		{
			return true;
		}

		// Token: 0x06007E6B RID: 32363 RVA: 0x002ED9A9 File Offset: 0x002EBBA9
		public virtual bool HandleEvent(MakeTemporaryEvent E)
		{
			return true;
		}

		// Token: 0x06007E6C RID: 32364 RVA: 0x002ED9AC File Offset: 0x002EBBAC
		public virtual bool HandleEvent(MentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06007E6D RID: 32365 RVA: 0x002ED9AF File Offset: 0x002EBBAF
		public virtual bool HandleEvent(MissilePenetrateEvent E)
		{
			return true;
		}

		// Token: 0x06007E6E RID: 32366 RVA: 0x002ED9B2 File Offset: 0x002EBBB2
		public virtual bool HandleEvent(MissileTraversingCellEvent E)
		{
			return true;
		}

		// Token: 0x06007E6F RID: 32367 RVA: 0x002ED9B5 File Offset: 0x002EBBB5
		public virtual bool HandleEvent(ModificationAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06007E70 RID: 32368 RVA: 0x002ED9B8 File Offset: 0x002EBBB8
		public virtual bool HandleEvent(ModifyAttackingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06007E71 RID: 32369 RVA: 0x002ED9BB File Offset: 0x002EBBBB
		public virtual bool HandleEvent(ModifyBitCostEvent E)
		{
			return true;
		}

		// Token: 0x06007E72 RID: 32370 RVA: 0x002ED9BE File Offset: 0x002EBBBE
		public virtual bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06007E73 RID: 32371 RVA: 0x002ED9C1 File Offset: 0x002EBBC1
		public virtual bool HandleEvent(ModifyOriginatingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06007E74 RID: 32372 RVA: 0x002ED9C4 File Offset: 0x002EBBC4
		public virtual bool HandleEvent(MovementModeChangedEvent E)
		{
			return true;
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x002ED9C7 File Offset: 0x002EBBC7
		public virtual bool HandleEvent(MutationsSubjectToEMPEvent E)
		{
			return true;
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x002ED9CA File Offset: 0x002EBBCA
		public virtual bool HandleEvent(NeedPartSupportEvent E)
		{
			return true;
		}

		// Token: 0x06007E77 RID: 32375 RVA: 0x002ED9CD File Offset: 0x002EBBCD
		public virtual bool HandleEvent(NeedsReloadEvent E)
		{
			return true;
		}

		// Token: 0x06007E78 RID: 32376 RVA: 0x002ED9D0 File Offset: 0x002EBBD0
		public virtual bool HandleEvent(NeutronFluxPourExplodesEvent E)
		{
			return true;
		}

		// Token: 0x06007E79 RID: 32377 RVA: 0x002ED9D3 File Offset: 0x002EBBD3
		public virtual bool HandleEvent(NotifyTargetImmuneEvent E)
		{
			return true;
		}

		// Token: 0x06007E7A RID: 32378 RVA: 0x002ED9D6 File Offset: 0x002EBBD6
		public virtual bool HandleEvent(ObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06007E7B RID: 32379 RVA: 0x002ED9D9 File Offset: 0x002EBBD9
		public virtual bool HandleEvent(ObjectEnteredCellEvent E)
		{
			return true;
		}

		// Token: 0x06007E7C RID: 32380 RVA: 0x002ED9DC File Offset: 0x002EBBDC
		public virtual bool HandleEvent(ObjectEnteringCellEvent E)
		{
			return true;
		}

		// Token: 0x06007E7D RID: 32381 RVA: 0x002ED9DF File Offset: 0x002EBBDF
		public virtual bool HandleEvent(ObjectGoingProneEvent E)
		{
			return true;
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x002ED9E2 File Offset: 0x002EBBE2
		public virtual bool HandleEvent(ObjectLeavingCellEvent E)
		{
			return true;
		}

		// Token: 0x06007E7F RID: 32383 RVA: 0x002ED9E5 File Offset: 0x002EBBE5
		public virtual bool HandleEvent(ObjectStartedFlyingEvent E)
		{
			return true;
		}

		// Token: 0x06007E80 RID: 32384 RVA: 0x002ED9E8 File Offset: 0x002EBBE8
		public virtual bool HandleEvent(ObjectStoppedFlyingEvent E)
		{
			return true;
		}

		// Token: 0x06007E81 RID: 32385 RVA: 0x002ED9EB File Offset: 0x002EBBEB
		public virtual bool HandleEvent(OkayToDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007E82 RID: 32386 RVA: 0x002ED9EE File Offset: 0x002EBBEE
		public virtual bool HandleEvent(OnDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06007E83 RID: 32387 RVA: 0x002ED9F1 File Offset: 0x002EBBF1
		public virtual bool HandleEvent(OnDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x06007E84 RID: 32388 RVA: 0x002ED9F4 File Offset: 0x002EBBF4
		public virtual bool HandleEvent(OnQuestAddedEvent E)
		{
			return true;
		}

		// Token: 0x06007E85 RID: 32389 RVA: 0x002ED9F7 File Offset: 0x002EBBF7
		public virtual bool HandleEvent(OwnerAfterInventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06007E86 RID: 32390 RVA: 0x002ED9FA File Offset: 0x002EBBFA
		public virtual bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06007E87 RID: 32391 RVA: 0x002ED9FD File Offset: 0x002EBBFD
		public virtual bool HandleEvent(OwnerGetShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06007E88 RID: 32392 RVA: 0x002EDA00 File Offset: 0x002EBC00
		public virtual bool HandleEvent(OwnerGetUnknownShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x002EDA03 File Offset: 0x002EBC03
		public virtual bool HandleEvent(PartAddedEvent E)
		{
			return true;
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x002EDA06 File Offset: 0x002EBC06
		public virtual bool HandleEvent(PartRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x002EDA09 File Offset: 0x002EBC09
		public virtual bool HandleEvent(PartSupportEvent E)
		{
			return true;
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x002EDA0C File Offset: 0x002EBC0C
		public virtual bool HandleEvent(PathAsBurrowerEvent E)
		{
			return true;
		}

		// Token: 0x06007E8D RID: 32397 RVA: 0x002EDA0F File Offset: 0x002EBC0F
		public virtual bool HandleEvent(PhysicalContactEvent E)
		{
			return true;
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x002EDA12 File Offset: 0x002EBC12
		public virtual bool HandleEvent(PointDefenseInterceptEvent E)
		{
			return true;
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x002EDA15 File Offset: 0x002EBC15
		public virtual bool HandleEvent(PollForHealingLocationEvent E)
		{
			return true;
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x002EDA18 File Offset: 0x002EBC18
		public virtual bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			return true;
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x002EDA1B File Offset: 0x002EBC1B
		public virtual bool HandleEvent(PowerUpdatedEvent E)
		{
			return true;
		}

		// Token: 0x06007E92 RID: 32402 RVA: 0x002EDA1E File Offset: 0x002EBC1E
		public virtual bool HandleEvent(PreferDefaultBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06007E93 RID: 32403 RVA: 0x002EDA21 File Offset: 0x002EBC21
		public virtual bool HandleEvent(PreferTargetEvent E)
		{
			return true;
		}

		// Token: 0x06007E94 RID: 32404 RVA: 0x002EDA24 File Offset: 0x002EBC24
		public virtual bool HandleEvent(PreventSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06007E95 RID: 32405 RVA: 0x002EDA27 File Offset: 0x002EBC27
		public virtual bool HandleEvent(PrimePowerSystemsEvent E)
		{
			return true;
		}

		// Token: 0x06007E96 RID: 32406 RVA: 0x002EDA2A File Offset: 0x002EBC2A
		public virtual bool HandleEvent(ProducesLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06007E97 RID: 32407 RVA: 0x002EDA2D File Offset: 0x002EBC2D
		public virtual bool HandleEvent(ProjectileMovingEvent E)
		{
			return true;
		}

		// Token: 0x06007E98 RID: 32408 RVA: 0x002EDA30 File Offset: 0x002EBC30
		public virtual bool HandleEvent(QueryBroadcastDrawEvent E)
		{
			return true;
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x002EDA33 File Offset: 0x002EBC33
		public virtual bool HandleEvent(QueryChargeEvent E)
		{
			return true;
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x002EDA36 File Offset: 0x002EBC36
		public virtual bool HandleEvent(QueryChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x002EDA39 File Offset: 0x002EBC39
		public virtual bool HandleEvent(QueryChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06007E9C RID: 32412 RVA: 0x002EDA3C File Offset: 0x002EBC3C
		public virtual bool HandleEvent(QueryDrawEvent E)
		{
			return true;
		}

		// Token: 0x06007E9D RID: 32413 RVA: 0x002EDA3F File Offset: 0x002EBC3F
		public virtual bool HandleEvent(QueryEquippableListEvent E)
		{
			return true;
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x002EDA42 File Offset: 0x002EBC42
		public virtual bool HandleEvent(QueryInductionChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x002EDA45 File Offset: 0x002EBC45
		public virtual bool HandleEvent(QueryRechargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x002EDA48 File Offset: 0x002EBC48
		public virtual bool HandleEvent(QuerySlotListEvent E)
		{
			return true;
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x002EDA4B File Offset: 0x002EBC4B
		public virtual bool HandleEvent(QuestFinishedEvent E)
		{
			return true;
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x002EDA4E File Offset: 0x002EBC4E
		public virtual bool HandleEvent(QuestStartedEvent E)
		{
			return true;
		}

		// Token: 0x06007EA3 RID: 32419 RVA: 0x002EDA51 File Offset: 0x002EBC51
		public virtual bool HandleEvent(QuestStepFinishedEvent E)
		{
			return true;
		}

		// Token: 0x06007EA4 RID: 32420 RVA: 0x002EDA54 File Offset: 0x002EBC54
		public virtual bool HandleEvent(RadiatesHeatAdjacentEvent E)
		{
			return true;
		}

		// Token: 0x06007EA5 RID: 32421 RVA: 0x002EDA57 File Offset: 0x002EBC57
		public virtual bool HandleEvent(RadiatesHeatEvent E)
		{
			return true;
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x002EDA5A File Offset: 0x002EBC5A
		public virtual bool HandleEvent(RealityStabilizeEvent E)
		{
			return true;
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x002EDA5D File Offset: 0x002EBC5D
		public virtual bool HandleEvent(RechargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x002EDA60 File Offset: 0x002EBC60
		public virtual bool HandleEvent(RefreshTileEvent E)
		{
			return true;
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x002EDA63 File Offset: 0x002EBC63
		public virtual bool HandleEvent(RegenerateDefaultEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x002EDA66 File Offset: 0x002EBC66
		public virtual bool HandleEvent(RegenerateLimbEvent E)
		{
			return true;
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x002EDA69 File Offset: 0x002EBC69
		public virtual bool HandleEvent(RemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x002EDA6C File Offset: 0x002EBC6C
		public virtual bool HandleEvent(RepaintedEvent E)
		{
			return true;
		}

		// Token: 0x06007EAD RID: 32429 RVA: 0x002EDA6F File Offset: 0x002EBC6F
		public virtual bool HandleEvent(RepairCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x06007EAE RID: 32430 RVA: 0x002EDA72 File Offset: 0x002EBC72
		public virtual bool HandleEvent(RepairedEvent E)
		{
			return true;
		}

		// Token: 0x06007EAF RID: 32431 RVA: 0x002EDA75 File Offset: 0x002EBC75
		public virtual bool HandleEvent(ReplaceInContextEvent E)
		{
			return true;
		}

		// Token: 0x06007EB0 RID: 32432 RVA: 0x002EDA78 File Offset: 0x002EBC78
		public virtual bool HandleEvent(ReplaceThrownWeaponEvent E)
		{
			return true;
		}

		// Token: 0x06007EB1 RID: 32433 RVA: 0x002EDA7B File Offset: 0x002EBC7B
		public virtual bool HandleEvent(ReplicaCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06007EB2 RID: 32434 RVA: 0x002EDA7E File Offset: 0x002EBC7E
		public virtual bool HandleEvent(ReputationChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007EB3 RID: 32435 RVA: 0x002EDA81 File Offset: 0x002EBC81
		public virtual bool HandleEvent(RespiresEvent E)
		{
			return true;
		}

		// Token: 0x06007EB4 RID: 32436 RVA: 0x002EDA84 File Offset: 0x002EBC84
		public virtual bool HandleEvent(SecretVisibilityChangedEvent E)
		{
			return true;
		}

		// Token: 0x06007EB5 RID: 32437 RVA: 0x002EDA87 File Offset: 0x002EBC87
		public virtual bool HandleEvent(ShieldSlamPerformedEvent E)
		{
			return true;
		}

		// Token: 0x06007EB6 RID: 32438 RVA: 0x002EDA8A File Offset: 0x002EBC8A
		public virtual bool HandleEvent(ShotCompleteEvent E)
		{
			return true;
		}

		// Token: 0x06007EB7 RID: 32439 RVA: 0x002EDA8D File Offset: 0x002EBC8D
		public virtual bool HandleEvent(ShouldAttackToReachTargetEvent E)
		{
			return true;
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x002EDA90 File Offset: 0x002EBC90
		public virtual bool HandleEvent(ShouldDescribeStatBonusEvent E)
		{
			return true;
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x002EDA93 File Offset: 0x002EBC93
		public virtual bool HandleEvent(SlamEvent E)
		{
			return true;
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x002EDA96 File Offset: 0x002EBC96
		public virtual bool HandleEvent(StackCountChangedEvent E)
		{
			return true;
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x002EDA99 File Offset: 0x002EBC99
		public virtual bool HandleEvent(StartTradeEvent E)
		{
			return true;
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x002EDA9C File Offset: 0x002EBC9C
		public virtual bool HandleEvent(StatChangeEvent E)
		{
			return true;
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x002EDA9F File Offset: 0x002EBC9F
		public virtual bool HandleEvent(StockedEvent E)
		{
			return true;
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x002EDAA2 File Offset: 0x002EBCA2
		public virtual bool HandleEvent(StripContentsEvent E)
		{
			return true;
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x002EDAA5 File Offset: 0x002EBCA5
		public virtual bool HandleEvent(SubjectToGravityEvent E)
		{
			return true;
		}

		// Token: 0x06007EC0 RID: 32448 RVA: 0x002EDAA8 File Offset: 0x002EBCA8
		public virtual bool HandleEvent(SuspendingEvent E)
		{
			return true;
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x002EDAAB File Offset: 0x002EBCAB
		public virtual bool HandleEvent(SyncMutationLevelsEvent E)
		{
			return true;
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x002EDAAE File Offset: 0x002EBCAE
		public virtual bool HandleEvent(SyncRenderEvent E)
		{
			return true;
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x002EDAB1 File Offset: 0x002EBCB1
		public virtual bool HandleEvent(SynchronizeExistenceEvent E)
		{
			return true;
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x002EDAB4 File Offset: 0x002EBCB4
		public virtual bool HandleEvent(TakeOnRoleEvent E)
		{
			return true;
		}

		// Token: 0x06007EC5 RID: 32453 RVA: 0x002EDAB7 File Offset: 0x002EBCB7
		public virtual bool HandleEvent(TakenEvent E)
		{
			return true;
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x002EDABA File Offset: 0x002EBCBA
		public virtual bool HandleEvent(TestChargeEvent E)
		{
			return true;
		}

		// Token: 0x06007EC7 RID: 32455 RVA: 0x002EDABD File Offset: 0x002EBCBD
		public virtual bool HandleEvent(ThawedEvent E)
		{
			return true;
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x002EDAC0 File Offset: 0x002EBCC0
		public virtual bool HandleEvent(TookDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007EC9 RID: 32457 RVA: 0x002EDAC3 File Offset: 0x002EBCC3
		public virtual bool HandleEvent(TookEnvironmentalDamageEvent E)
		{
			return true;
		}

		// Token: 0x06007ECA RID: 32458 RVA: 0x002EDAC6 File Offset: 0x002EBCC6
		public virtual bool HandleEvent(TookEvent E)
		{
			return true;
		}

		// Token: 0x06007ECB RID: 32459 RVA: 0x002EDAC9 File Offset: 0x002EBCC9
		public virtual bool HandleEvent(TransparentToEMPEvent E)
		{
			return true;
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x002EDACC File Offset: 0x002EBCCC
		public virtual bool HandleEvent(TravelSpeedEvent E)
		{
			return true;
		}

		// Token: 0x06007ECD RID: 32461 RVA: 0x002EDACF File Offset: 0x002EBCCF
		public virtual bool HandleEvent(TriggersMakersMarkCreationEvent E)
		{
			return true;
		}

		// Token: 0x06007ECE RID: 32462 RVA: 0x002EDAD2 File Offset: 0x002EBCD2
		public virtual bool HandleEvent(TryRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06007ECF RID: 32463 RVA: 0x002EDAD5 File Offset: 0x002EBCD5
		public virtual bool HandleEvent(UnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06007ED0 RID: 32464 RVA: 0x002EDAD8 File Offset: 0x002EBCD8
		public virtual bool HandleEvent(UnimplantedEvent E)
		{
			return true;
		}

		// Token: 0x06007ED1 RID: 32465 RVA: 0x002EDADB File Offset: 0x002EBCDB
		public virtual bool HandleEvent(UseChargeEvent E)
		{
			return true;
		}

		// Token: 0x06007ED2 RID: 32466 RVA: 0x002EDADE File Offset: 0x002EBCDE
		public virtual bool HandleEvent(UseDramsEvent E)
		{
			return true;
		}

		// Token: 0x06007ED3 RID: 32467 RVA: 0x002EDAE1 File Offset: 0x002EBCE1
		public virtual bool HandleEvent(UseEnergyEvent E)
		{
			return true;
		}

		// Token: 0x06007ED4 RID: 32468 RVA: 0x002EDAE4 File Offset: 0x002EBCE4
		public virtual bool HandleEvent(UseHealingLocationEvent E)
		{
			return true;
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x002EDAE7 File Offset: 0x002EBCE7
		public virtual bool HandleEvent(UsingChargeEvent E)
		{
			return true;
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x002EDAEA File Offset: 0x002EBCEA
		public virtual bool HandleEvent(VaporizedEvent E)
		{
			return true;
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x002EDAED File Offset: 0x002EBCED
		public virtual bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return true;
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x002EDAF0 File Offset: 0x002EBCF0
		public virtual bool HandleEvent(WasDerivedFromEvent E)
		{
			return true;
		}

		// Token: 0x06007ED9 RID: 32473 RVA: 0x002EDAF3 File Offset: 0x002EBCF3
		public virtual bool HandleEvent(WasReplicatedEvent E)
		{
			return true;
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x002EDAF6 File Offset: 0x002EBCF6
		public virtual bool HandleEvent(WaterRitualStartEvent E)
		{
			return true;
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x002EDAF9 File Offset: 0x002EBCF9
		public virtual bool HandleEvent(WorshipPerformedEvent E)
		{
			return true;
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x002EDAFC File Offset: 0x002EBCFC
		public virtual bool HandleEvent(ZoneActivatedEvent E)
		{
			return true;
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x002EDAFF File Offset: 0x002EBCFF
		public virtual bool HandleEvent(ZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06007EDE RID: 32478 RVA: 0x002EDB02 File Offset: 0x002EBD02
		public virtual bool HandleEvent(ZoneDeactivatedEvent E)
		{
			return true;
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x002EDB05 File Offset: 0x002EBD05
		public virtual bool HandleEvent(ZoneThawedEvent E)
		{
			return true;
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06007EE0 RID: 32480 RVA: 0x002EDB08 File Offset: 0x002EBD08
		public IEventBinder Binder
		{
			get
			{
				return ProceduralCookingTriggeredAction.EventBinder.Instance;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06007EE1 RID: 32481 RVA: 0x002EDB0F File Offset: 0x002EBD0F
		public virtual bool WantFieldReflection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x002EDB12 File Offset: 0x002EBD12
		public virtual void Init(GameObject target)
		{
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x002EDB14 File Offset: 0x002EBD14
		public virtual string GetDescription()
		{
			return "[action takes place]";
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x002EDB1B File Offset: 0x002EBD1B
		public virtual string GetTemplatedDescription()
		{
			return this.GetDescription();
		}

		// Token: 0x06007EE5 RID: 32485 RVA: 0x002EDB23 File Offset: 0x002EBD23
		public virtual void Apply(GameObject go)
		{
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x002EDB25 File Offset: 0x002EBD25
		public virtual void Remove(GameObject go)
		{
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x002EDB27 File Offset: 0x002EBD27
		public virtual string GetNotification()
		{
			return this.GetDescription();
		}

		// Token: 0x06007EE8 RID: 32488 RVA: 0x002EDB2F File Offset: 0x002EBD2F
		public virtual bool WantEvent(int ID, int cascade)
		{
			return false;
		}

		// Token: 0x06007EE9 RID: 32489 RVA: 0x002EDB32 File Offset: 0x002EBD32
		public virtual bool HandleEvent(MinEvent E)
		{
			return true;
		}

		// Token: 0x06007EEA RID: 32490 RVA: 0x002EDB35 File Offset: 0x002EBD35
		public virtual void Write(SerializationWriter Writer)
		{
		}

		// Token: 0x06007EEB RID: 32491 RVA: 0x002EDB37 File Offset: 0x002EBD37
		public virtual void Read(SerializationReader Reader)
		{
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x002EDB3C File Offset: 0x002EBD3C
		public virtual ProceduralCookingTriggeredAction DeepCopy()
		{
			Type type = base.GetType();
			ProceduralCookingTriggeredAction proceduralCookingTriggeredAction = (ProceduralCookingTriggeredAction)Activator.CreateInstance(type);
			foreach (FieldInfo fieldInfo in type.GetCachedFields())
			{
				if ((fieldInfo.Attributes & (FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)) == FieldAttributes.PrivateScope)
				{
					fieldInfo.SetValue(proceduralCookingTriggeredAction, fieldInfo.GetValue(this));
				}
			}
			return proceduralCookingTriggeredAction;
		}

		// Token: 0x02001C2A RID: 7210
		public class EventBinder : IEventBinder
		{
			// Token: 0x0600F058 RID: 61528 RVA: 0x0054A2F5 File Offset: 0x005484F5
			public override void WriteBind(SerializationWriter Writer, IEventHandler Handler, int ID)
			{
				Writer.WriteTokenized(Handler.GetType());
			}

			// Token: 0x0600F059 RID: 61529 RVA: 0x0054A304 File Offset: 0x00548504
			public override IEventHandler ReadBind(SerializationReader Reader, int ID)
			{
				Type type = Reader.ReadTokenizedType();
				GameObject player = The.Player;
				ProceduralCookingEffectWithTrigger proceduralCookingEffectWithTrigger = ((player != null) ? player.GetEffectDescendedFrom<ProceduralCookingEffectWithTrigger>() : null);
				if (proceduralCookingEffectWithTrigger != null)
				{
					foreach (ProceduralCookingTriggeredAction proceduralCookingTriggeredAction in proceduralCookingEffectWithTrigger.triggeredActions)
					{
						if (proceduralCookingTriggeredAction.GetType() == type)
						{
							return proceduralCookingTriggeredAction;
						}
					}
				}
				return null;
			}

			// Token: 0x04006F60 RID: 28512
			public static readonly ProceduralCookingTriggeredAction.EventBinder Instance = new ProceduralCookingTriggeredAction.EventBinder();
		}
	}
}
