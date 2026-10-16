using System;
using Qud.API;
using XRL.Core;
using XRL.Messages;
using XRL.World;
using XRL.World.Anatomy;
using XRL.World.Conversations;

namespace XRL
{
	// Token: 0x020004B6 RID: 1206
	[Serializable]
	public abstract class IGameSystem : IEventHandler, IComposite
	{
		// Token: 0x0600325E RID: 12894 RVA: 0x000FDC4C File Offset: 0x000FBE4C
		public virtual bool HandleEvent(AIAfterMissileEvent E)
		{
			return true;
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000FDC4F File Offset: 0x000FBE4F
		public virtual bool HandleEvent(AIAfterThrowEvent E)
		{
			return true;
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000FDC52 File Offset: 0x000FBE52
		public virtual bool HandleEvent(AIBoredEvent E)
		{
			return true;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000FDC55 File Offset: 0x000FBE55
		public virtual bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000FDC58 File Offset: 0x000FBE58
		public virtual bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000FDC5B File Offset: 0x000FBE5B
		public virtual bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000FDC5E File Offset: 0x000FBE5E
		public virtual bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000FDC61 File Offset: 0x000FBE61
		public virtual bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000FDC64 File Offset: 0x000FBE64
		public virtual bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000FDC67 File Offset: 0x000FBE67
		public virtual bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000FDC6A File Offset: 0x000FBE6A
		public virtual bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000FDC6D File Offset: 0x000FBE6D
		public virtual bool HandleEvent(AIHelpBroadcastEvent E)
		{
			return true;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000FDC70 File Offset: 0x000FBE70
		public virtual bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return true;
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000FDC73 File Offset: 0x000FBE73
		public virtual bool HandleEvent(ActorGetNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000FDC76 File Offset: 0x000FBE76
		public virtual bool HandleEvent(AddedToInventoryEvent E)
		{
			return true;
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000FDC79 File Offset: 0x000FBE79
		public virtual bool HandleEvent(AdjustTotalWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000FDC7C File Offset: 0x000FBE7C
		public virtual bool HandleEvent(AdjustValueEvent E)
		{
			return true;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000FDC7F File Offset: 0x000FBE7F
		public virtual bool HandleEvent(AdjustVisibilityRadiusEvent E)
		{
			return true;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000FDC82 File Offset: 0x000FBE82
		public virtual bool HandleEvent(AdjustWeightEvent E)
		{
			return true;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000FDC85 File Offset: 0x000FBE85
		public virtual bool HandleEvent(AfterAddActiveObjectEvent E)
		{
			return true;
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000FDC88 File Offset: 0x000FBE88
		public virtual bool HandleEvent(AfterAddOpinionEvent E)
		{
			return true;
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000FDC8B File Offset: 0x000FBE8B
		public virtual bool HandleEvent(AfterAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000FDC8E File Offset: 0x000FBE8E
		public virtual bool HandleEvent(AfterAfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000FDC91 File Offset: 0x000FBE91
		public virtual bool HandleEvent(AfterBasicBestowalEvent E)
		{
			return true;
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000FDC94 File Offset: 0x000FBE94
		public virtual bool HandleEvent(AfterChangePartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000FDC97 File Offset: 0x000FBE97
		public virtual bool HandleEvent(AfterConsumeEvent E)
		{
			return true;
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000FDC9A File Offset: 0x000FBE9A
		public virtual bool HandleEvent(AfterContentsTakenEvent E)
		{
			return true;
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000FDC9D File Offset: 0x000FBE9D
		public virtual bool HandleEvent(AfterConversationEvent E)
		{
			return true;
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000FDCA0 File Offset: 0x000FBEA0
		public virtual bool HandleEvent(AfterDieEvent E)
		{
			return true;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000FDCA3 File Offset: 0x000FBEA3
		public virtual bool HandleEvent(AfterDismemberEvent E)
		{
			return true;
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000FDCA6 File Offset: 0x000FBEA6
		public virtual bool HandleEvent(AfterElementBestowalEvent E)
		{
			return true;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000FDCA9 File Offset: 0x000FBEA9
		public virtual bool HandleEvent(AfterExamineCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000FDCAC File Offset: 0x000FBEAC
		public virtual bool HandleEvent(AfterExamineFailureEvent E)
		{
			return true;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000FDCAF File Offset: 0x000FBEAF
		public virtual bool HandleEvent(AfterGameLoadedEvent E)
		{
			return true;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000FDCB2 File Offset: 0x000FBEB2
		public virtual bool HandleEvent(AfterInventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000FDCB5 File Offset: 0x000FBEB5
		public virtual bool HandleEvent(AfterLevelGainedEvent E)
		{
			return true;
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000FDCB8 File Offset: 0x000FBEB8
		public virtual bool HandleEvent(AfterMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000FDCBB File Offset: 0x000FBEBB
		public virtual bool HandleEvent(AfterMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000FDCBE File Offset: 0x000FBEBE
		public virtual bool HandleEvent(AfterMoveFailedEvent E)
		{
			return true;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000FDCC1 File Offset: 0x000FBEC1
		public virtual bool HandleEvent(AfterObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000FDCC4 File Offset: 0x000FBEC4
		public virtual bool HandleEvent(AfterPetEvent E)
		{
			return true;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000FDCC7 File Offset: 0x000FBEC7
		public virtual bool HandleEvent(AfterPilotChangeEvent E)
		{
			return true;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000FDCCA File Offset: 0x000FBECA
		public virtual bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			return true;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000FDCCD File Offset: 0x000FBECD
		public virtual bool HandleEvent(AfterPseudoRelicGeneratedEvent E)
		{
			return true;
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000FDCD0 File Offset: 0x000FBED0
		public virtual bool HandleEvent(AfterReadBookEvent E)
		{
			return true;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000FDCD3 File Offset: 0x000FBED3
		public virtual bool HandleEvent(AfterRelicGeneratedEvent E)
		{
			return true;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000FDCD6 File Offset: 0x000FBED6
		public virtual bool HandleEvent(AfterRemoveActiveObjectEvent E)
		{
			return true;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000FDCD9 File Offset: 0x000FBED9
		public virtual bool HandleEvent(AfterRemoveSkillEvent E)
		{
			return true;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000FDCDC File Offset: 0x000FBEDC
		public virtual bool HandleEvent(AfterReputationChangeEvent E)
		{
			return true;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000FDCDF File Offset: 0x000FBEDF
		public virtual bool HandleEvent(AfterShieldBlockEvent E)
		{
			return true;
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000FDCE2 File Offset: 0x000FBEE2
		public virtual bool HandleEvent(AfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000FDCE5 File Offset: 0x000FBEE5
		public virtual bool HandleEvent(AfterTravelEvent E)
		{
			return true;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000FDCE8 File Offset: 0x000FBEE8
		public virtual bool HandleEvent(AfterZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000FDCEB File Offset: 0x000FBEEB
		public virtual bool HandleEvent(AllowInventoryStackEvent E)
		{
			return true;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000FDCEE File Offset: 0x000FBEEE
		public virtual bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return true;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000FDCF1 File Offset: 0x000FBEF1
		public virtual bool HandleEvent(AllowPolypPluckingEvent E)
		{
			return true;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000FDCF4 File Offset: 0x000FBEF4
		public virtual bool HandleEvent(AllowTradeWithNoInventoryEvent E)
		{
			return true;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000FDCF7 File Offset: 0x000FBEF7
		public virtual bool HandleEvent(AnimateEvent E)
		{
			return true;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000FDCFA File Offset: 0x000FBEFA
		public virtual bool HandleEvent(AnyAutoCollectDramsEvent E)
		{
			return true;
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000FDCFD File Offset: 0x000FBEFD
		public virtual bool HandleEvent(AnyRegenerableLimbsEvent E)
		{
			return true;
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000FDD00 File Offset: 0x000FBF00
		public virtual bool HandleEvent(ApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000FDD03 File Offset: 0x000FBF03
		public virtual bool HandleEvent(AttackerBeforeTemperatureChangeEvent E)
		{
			return true;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000FDD06 File Offset: 0x000FBF06
		public virtual bool HandleEvent(AttackerDealingDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000FDD09 File Offset: 0x000FBF09
		public virtual bool HandleEvent(AttackerDealtDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000FDD0C File Offset: 0x000FBF0C
		public virtual bool HandleEvent(AttemptConversationEvent E)
		{
			return true;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000FDD0F File Offset: 0x000FBF0F
		public virtual bool HandleEvent(AttemptToLandEvent E)
		{
			return true;
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000FDD12 File Offset: 0x000FBF12
		public virtual bool HandleEvent(AutoexploreObjectEvent E)
		{
			return true;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000FDD15 File Offset: 0x000FBF15
		public virtual bool HandleEvent(AwardXPEvent E)
		{
			return true;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000FDD18 File Offset: 0x000FBF18
		public virtual bool HandleEvent(AwardedXPEvent E)
		{
			return true;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000FDD1B File Offset: 0x000FBF1B
		public virtual bool HandleEvent(AwardingXPEvent E)
		{
			return true;
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000FDD1E File Offset: 0x000FBF1E
		public virtual bool HandleEvent(BeforeAITakingActionEvent E)
		{
			return true;
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000FDD21 File Offset: 0x000FBF21
		public virtual bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			return true;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000FDD24 File Offset: 0x000FBF24
		public virtual bool HandleEvent(BeforeAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000FDD27 File Offset: 0x000FBF27
		public virtual bool HandleEvent(BeforeAfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000FDD2A File Offset: 0x000FBF2A
		public virtual bool HandleEvent(BeforeApplyDamageEvent E)
		{
			return true;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000FDD2D File Offset: 0x000FBF2D
		public virtual bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000FDD30 File Offset: 0x000FBF30
		public virtual bool HandleEvent(BeforeConsumeEvent E)
		{
			return true;
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000FDD33 File Offset: 0x000FBF33
		public virtual bool HandleEvent(BeforeConversationEvent E)
		{
			return true;
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000FDD36 File Offset: 0x000FBF36
		public virtual bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000FDD39 File Offset: 0x000FBF39
		public virtual bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000FDD3C File Offset: 0x000FBF3C
		public virtual bool HandleEvent(BeforeDetonateEvent E)
		{
			return true;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000FDD3F File Offset: 0x000FBF3F
		public virtual bool HandleEvent(BeforeDieEvent E)
		{
			return true;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000FDD42 File Offset: 0x000FBF42
		public virtual bool HandleEvent(BeforeDismemberEvent E)
		{
			return true;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000FDD45 File Offset: 0x000FBF45
		public virtual bool HandleEvent(BeforeFireMissileWeaponsEvent E)
		{
			return true;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000FDD48 File Offset: 0x000FBF48
		public virtual bool HandleEvent(BeforeInteriorCollapseEvent E)
		{
			return true;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000FDD4B File Offset: 0x000FBF4B
		public virtual bool HandleEvent(BeforeLevelGainedEvent E)
		{
			return true;
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000FDD4E File Offset: 0x000FBF4E
		public virtual bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000FDD51 File Offset: 0x000FBF51
		public virtual bool HandleEvent(BeforeMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000FDD54 File Offset: 0x000FBF54
		public virtual bool HandleEvent(BeforeMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000FDD57 File Offset: 0x000FBF57
		public virtual bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000FDD5A File Offset: 0x000FBF5A
		public virtual bool HandleEvent(BeforePilotChangeEvent E)
		{
			return true;
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000FDD5D File Offset: 0x000FBF5D
		public virtual bool HandleEvent(BeforePlayMusicEvent E)
		{
			return true;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000FDD60 File Offset: 0x000FBF60
		public virtual bool HandleEvent(BeforeProjectileHitEvent E)
		{
			return true;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000FDD63 File Offset: 0x000FBF63
		public virtual bool HandleEvent(BeforePullDownEvent E)
		{
			return true;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000FDD66 File Offset: 0x000FBF66
		public virtual bool HandleEvent(BeforeRemoveSkillEvent E)
		{
			return true;
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000FDD69 File Offset: 0x000FBF69
		public virtual bool HandleEvent(BeforeRenderEvent E)
		{
			return true;
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000FDD6C File Offset: 0x000FBF6C
		public virtual bool HandleEvent(BeforeSetFeelingEvent E)
		{
			return true;
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000FDD6F File Offset: 0x000FBF6F
		public virtual bool HandleEvent(BeforeShieldBlockEvent E)
		{
			return true;
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000FDD72 File Offset: 0x000FBF72
		public virtual bool HandleEvent(BeforeSlamEvent E)
		{
			return true;
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000FDD75 File Offset: 0x000FBF75
		public virtual bool HandleEvent(BeforeTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000FDD78 File Offset: 0x000FBF78
		public virtual bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			return true;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000FDD7B File Offset: 0x000FBF7B
		public virtual bool HandleEvent(BeforeTookDamageEvent E)
		{
			return true;
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000FDD7E File Offset: 0x000FBF7E
		public virtual bool HandleEvent(BeforeTravelDownEvent E)
		{
			return true;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000FDD81 File Offset: 0x000FBF81
		public virtual bool HandleEvent(BeforeUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000FDD84 File Offset: 0x000FBF84
		public virtual bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000FDD87 File Offset: 0x000FBF87
		public virtual bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000FDD8A File Offset: 0x000FBF8A
		public virtual bool HandleEvent(BeginConversationEvent E)
		{
			return true;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000FDD8D File Offset: 0x000FBF8D
		public virtual bool HandleEvent(BeginMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000FDD90 File Offset: 0x000FBF90
		public virtual bool HandleEvent(BeginMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000FDD93 File Offset: 0x000FBF93
		public virtual bool HandleEvent(BeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000FDD96 File Offset: 0x000FBF96
		public virtual bool HandleEvent(BeingConsumedEvent E)
		{
			return true;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000FDD99 File Offset: 0x000FBF99
		public virtual bool HandleEvent(BlasphemyPerformedEvent E)
		{
			return true;
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000FDD9C File Offset: 0x000FBF9C
		public virtual bool HandleEvent(BlocksRadarEvent E)
		{
			return true;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000FDD9F File Offset: 0x000FBF9F
		public virtual bool HandleEvent(BodyPositionChangedEvent E)
		{
			return true;
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000FDDA2 File Offset: 0x000FBFA2
		public virtual bool HandleEvent(BootSequenceAbortedEvent E)
		{
			return true;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000FDDA5 File Offset: 0x000FBFA5
		public virtual bool HandleEvent(BootSequenceDoneEvent E)
		{
			return true;
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000FDDA8 File Offset: 0x000FBFA8
		public virtual bool HandleEvent(BootSequenceInitializedEvent E)
		{
			return true;
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000FDDAB File Offset: 0x000FBFAB
		public virtual bool HandleEvent(CanAcceptObjectEvent E)
		{
			return true;
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000FDDAE File Offset: 0x000FBFAE
		public virtual bool HandleEvent(CanApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000FDDB1 File Offset: 0x000FBFB1
		public virtual bool HandleEvent(CanBeDismemberedEvent E)
		{
			return true;
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000FDDB4 File Offset: 0x000FBFB4
		public virtual bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return true;
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000FDDB7 File Offset: 0x000FBFB7
		public virtual bool HandleEvent(CanBeMagneticallyManipulatedEvent E)
		{
			return true;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000FDDBA File Offset: 0x000FBFBA
		public virtual bool HandleEvent(CanBeModdedEvent E)
		{
			return true;
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000FDDBD File Offset: 0x000FBFBD
		public virtual bool HandleEvent(CanBeNamedEvent E)
		{
			return true;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000FDDC0 File Offset: 0x000FBFC0
		public virtual bool HandleEvent(CanBeReplicatedEvent E)
		{
			return true;
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000FDDC3 File Offset: 0x000FBFC3
		public virtual bool HandleEvent(CanBeSlottedEvent E)
		{
			return true;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000FDDC6 File Offset: 0x000FBFC6
		public virtual bool HandleEvent(CanBeTradedEvent E)
		{
			return true;
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000FDDC9 File Offset: 0x000FBFC9
		public virtual bool HandleEvent(CanBeUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000FDDCC File Offset: 0x000FBFCC
		public virtual bool HandleEvent(CanChangeMovementModeEvent E)
		{
			return true;
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000FDDCF File Offset: 0x000FBFCF
		public virtual bool HandleEvent(CanDrinkEvent E)
		{
			return true;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000FDDD2 File Offset: 0x000FBFD2
		public virtual bool HandleEvent(CanEnterInteriorEvent E)
		{
			return true;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000FDDD5 File Offset: 0x000FBFD5
		public virtual bool HandleEvent(CanFallEvent E)
		{
			return true;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000FDDD8 File Offset: 0x000FBFD8
		public virtual bool HandleEvent(CanFireAllMissileWeaponsEvent E)
		{
			return true;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000FDDDB File Offset: 0x000FBFDB
		public virtual bool HandleEvent(CanGiveDirectionsEvent E)
		{
			return true;
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000FDDDE File Offset: 0x000FBFDE
		public virtual bool HandleEvent(CanHaveConversationEvent E)
		{
			return true;
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000FDDE1 File Offset: 0x000FBFE1
		public virtual bool HandleEvent(CanJoinPartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000FDDE4 File Offset: 0x000FBFE4
		public virtual bool HandleEvent(CanMissilePassForcefieldEvent E)
		{
			return true;
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000FDDE7 File Offset: 0x000FBFE7
		public virtual bool HandleEvent(CanReceiveEmpathyEvent E)
		{
			return true;
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000FDDEA File Offset: 0x000FBFEA
		public virtual bool HandleEvent(CanReceiveTelepathyEvent E)
		{
			return true;
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000FDDED File Offset: 0x000FBFED
		public virtual bool HandleEvent(CanRefreshAbilityEvent E)
		{
			return true;
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000FDDF0 File Offset: 0x000FBFF0
		public virtual bool HandleEvent(CanSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000FDDF3 File Offset: 0x000FBFF3
		public virtual bool HandleEvent(CanStartConversationEvent E)
		{
			return true;
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000FDDF6 File Offset: 0x000FBFF6
		public virtual bool HandleEvent(CanTemperatureReturnToAmbientEvent E)
		{
			return true;
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000FDDF9 File Offset: 0x000FBFF9
		public virtual bool HandleEvent(CanTradeEvent E)
		{
			return true;
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000FDDFC File Offset: 0x000FBFFC
		public virtual bool HandleEvent(CanTravelEvent E)
		{
			return true;
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000FDDFF File Offset: 0x000FBFFF
		public virtual bool HandleEvent(CarryingCapacityChangedEvent E)
		{
			return true;
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000FDE02 File Offset: 0x000FC002
		public virtual bool HandleEvent(CellChangedEvent E)
		{
			return true;
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000FDE05 File Offset: 0x000FC005
		public virtual bool HandleEvent(CellDepletedEvent E)
		{
			return true;
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000FDE08 File Offset: 0x000FC008
		public virtual bool HandleEvent(ChargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000FDE0B File Offset: 0x000FC00B
		public virtual bool HandleEvent(ChargeUsedEvent E)
		{
			return true;
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000FDE0E File Offset: 0x000FC00E
		public virtual bool HandleEvent(CheckAnythingToCleanEvent E)
		{
			return true;
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000FDE11 File Offset: 0x000FC011
		public virtual bool HandleEvent(CheckAnythingToCleanWithEvent E)
		{
			return true;
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000FDE14 File Offset: 0x000FC014
		public virtual bool HandleEvent(CheckAnythingToCleanWithNearbyEvent E)
		{
			return true;
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000FDE17 File Offset: 0x000FC017
		public virtual bool HandleEvent(CheckAttackableEvent E)
		{
			return true;
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000FDE1A File Offset: 0x000FC01A
		public virtual bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return true;
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000FDE1D File Offset: 0x000FC01D
		public virtual bool HandleEvent(CheckGasCanAffectEvent E)
		{
			return true;
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000FDE20 File Offset: 0x000FC020
		public virtual bool HandleEvent(CheckLoadAmmoEvent E)
		{
			return true;
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000FDE23 File Offset: 0x000FC023
		public virtual bool HandleEvent(CheckOverburdenedOnStrengthUpdateEvent E)
		{
			return true;
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000FDE26 File Offset: 0x000FC026
		public virtual bool HandleEvent(CheckPaintabilityEvent E)
		{
			return true;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000FDE29 File Offset: 0x000FC029
		public virtual bool HandleEvent(CheckReadyToFireEvent E)
		{
			return true;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000FDE2C File Offset: 0x000FC02C
		public virtual bool HandleEvent(CheckRealityDistortionAccessibilityEvent E)
		{
			return true;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000FDE2F File Offset: 0x000FC02F
		public virtual bool HandleEvent(CheckRealityDistortionAdvisabilityEvent E)
		{
			return true;
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000FDE32 File Offset: 0x000FC032
		public virtual bool HandleEvent(CheckRealityDistortionUsabilityEvent E)
		{
			return true;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000FDE35 File Offset: 0x000FC035
		public virtual bool HandleEvent(CheckSpawnMergeEvent E)
		{
			return true;
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000FDE38 File Offset: 0x000FC038
		public virtual bool HandleEvent(CheckTileChangeEvent E)
		{
			return true;
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000FDE3B File Offset: 0x000FC03B
		public virtual bool HandleEvent(CheckUsesChargeWhileEquippedEvent E)
		{
			return true;
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000FDE3E File Offset: 0x000FC03E
		public virtual bool HandleEvent(CleanItemsEvent E)
		{
			return true;
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000FDE41 File Offset: 0x000FC041
		public virtual bool HandleEvent(CollectBroadcastChargeEvent E)
		{
			return true;
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000FDE44 File Offset: 0x000FC044
		public virtual bool HandleEvent(CommandEvent E)
		{
			return true;
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000FDE47 File Offset: 0x000FC047
		public virtual bool HandleEvent(CommandReloadEvent E)
		{
			return true;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000FDE4A File Offset: 0x000FC04A
		public virtual bool HandleEvent(CommandReplaceCellEvent E)
		{
			return true;
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000FDE4D File Offset: 0x000FC04D
		public virtual bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			return true;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000FDE50 File Offset: 0x000FC050
		public virtual bool HandleEvent(CommandSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000FDE53 File Offset: 0x000FC053
		public virtual bool HandleEvent(CommandTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000FDE56 File Offset: 0x000FC056
		public virtual bool HandleEvent(ConfigureMissileVisualEffectEvent E)
		{
			return true;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000FDE59 File Offset: 0x000FC059
		public virtual bool HandleEvent(ContainsAnyBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000FDE5C File Offset: 0x000FC05C
		public virtual bool HandleEvent(ContainsBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000FDE5F File Offset: 0x000FC05F
		public virtual bool HandleEvent(ContainsEvent E)
		{
			return true;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000FDE62 File Offset: 0x000FC062
		public virtual bool HandleEvent(DamageConstantAdjustedEvent E)
		{
			return true;
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000FDE65 File Offset: 0x000FC065
		public virtual bool HandleEvent(DamageDieSizeAdjustedEvent E)
		{
			return true;
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000FDE68 File Offset: 0x000FC068
		public virtual bool HandleEvent(DeathEvent E)
		{
			return true;
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000FDE6B File Offset: 0x000FC06B
		public virtual bool HandleEvent(DecorateDefaultEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000FDE6E File Offset: 0x000FC06E
		public virtual bool HandleEvent(DefendMeleeHitEvent E)
		{
			return true;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000FDE71 File Offset: 0x000FC071
		public virtual bool HandleEvent(DefenderMissileHitEvent E)
		{
			return true;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000FDE74 File Offset: 0x000FC074
		public virtual bool HandleEvent(DerivationCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000FDE77 File Offset: 0x000FC077
		public virtual bool HandleEvent(DidInitialEquipEvent E)
		{
			return true;
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000FDE7A File Offset: 0x000FC07A
		public virtual bool HandleEvent(DidReequipEvent E)
		{
			return true;
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000FDE7D File Offset: 0x000FC07D
		public virtual bool HandleEvent(DropOnDeathEvent E)
		{
			return true;
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000FDE80 File Offset: 0x000FC080
		public virtual bool HandleEvent(DroppedEvent E)
		{
			return true;
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000FDE83 File Offset: 0x000FC083
		public virtual bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000FDE86 File Offset: 0x000FC086
		public virtual bool HandleEvent(EarlyBeforeDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000FDE89 File Offset: 0x000FC089
		public virtual bool HandleEvent(EffectAppliedEvent E)
		{
			return true;
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000FDE8C File Offset: 0x000FC08C
		public virtual bool HandleEvent(EffectForceAppliedEvent E)
		{
			return true;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000FDE8F File Offset: 0x000FC08F
		public virtual bool HandleEvent(EffectRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000FDE92 File Offset: 0x000FC092
		public virtual bool HandleEvent(EmbarkEvent E)
		{
			return true;
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000FDE95 File Offset: 0x000FC095
		public virtual bool HandleEvent(EncounterChanceEvent E)
		{
			return true;
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000FDE98 File Offset: 0x000FC098
		public virtual bool HandleEvent(EncumbranceChangedEvent E)
		{
			return true;
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000FDE9B File Offset: 0x000FC09B
		public virtual bool HandleEvent(EndActionEvent E)
		{
			return true;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000FDE9E File Offset: 0x000FC09E
		public virtual bool HandleEvent(EndTurnEvent E)
		{
			return true;
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000FDEA1 File Offset: 0x000FC0A1
		public virtual bool HandleEvent(EnterCellEvent E)
		{
			return true;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000FDEA4 File Offset: 0x000FC0A4
		public virtual bool HandleEvent(EnteredCellEvent E)
		{
			return true;
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000FDEA7 File Offset: 0x000FC0A7
		public virtual bool HandleEvent(EnteringCellEvent E)
		{
			return true;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000FDEAA File Offset: 0x000FC0AA
		public virtual bool HandleEvent(EnteringZoneEvent E)
		{
			return true;
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000FDEAD File Offset: 0x000FC0AD
		public virtual bool HandleEvent(EnvironmentalUpdateEvent E)
		{
			return true;
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000FDEB0 File Offset: 0x000FC0B0
		public virtual bool HandleEvent(EquippedEvent E)
		{
			return true;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000FDEB3 File Offset: 0x000FC0B3
		public virtual bool HandleEvent(EquipperEquippedEvent E)
		{
			return true;
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000FDEB6 File Offset: 0x000FC0B6
		public virtual bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000FDEB9 File Offset: 0x000FC0B9
		public virtual bool HandleEvent(ExamineFailureEvent E)
		{
			return true;
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000FDEBC File Offset: 0x000FC0BC
		public virtual bool HandleEvent(ExamineSuccessEvent E)
		{
			return true;
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000FDEBF File Offset: 0x000FC0BF
		public virtual bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			return true;
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000FDEC2 File Offset: 0x000FC0C2
		public virtual bool HandleEvent(ExtremitiesMovedEvent E)
		{
			return true;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000FDEC5 File Offset: 0x000FC0C5
		public virtual bool HandleEvent(FellDownEvent E)
		{
			return true;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000FDEC8 File Offset: 0x000FC0C8
		public virtual bool HandleEvent(FindObjectByIdEvent E)
		{
			return true;
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000FDECB File Offset: 0x000FC0CB
		public virtual bool HandleEvent(FinishChargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000FDECE File Offset: 0x000FC0CE
		public virtual bool HandleEvent(FinishRechargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000FDED1 File Offset: 0x000FC0D1
		public virtual bool HandleEvent(FlushWeightCacheEvent E)
		{
			return true;
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000FDED4 File Offset: 0x000FC0D4
		public virtual bool HandleEvent(ForceApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000FDED7 File Offset: 0x000FC0D7
		public virtual bool HandleEvent(FrozeEvent E)
		{
			return true;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000FDEDA File Offset: 0x000FC0DA
		public virtual bool HandleEvent(GaveDirectionsEvent E)
		{
			return true;
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000FDEDD File Offset: 0x000FC0DD
		public virtual bool HandleEvent(GeneralAmnestyEvent E)
		{
			return true;
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000FDEE0 File Offset: 0x000FC0E0
		public virtual bool HandleEvent(GenericCommandEvent E)
		{
			return true;
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000FDEE3 File Offset: 0x000FC0E3
		public virtual bool HandleEvent(GenericDeepNotifyEvent E)
		{
			return true;
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000FDEE6 File Offset: 0x000FC0E6
		public virtual bool HandleEvent(GenericDeepQueryEvent E)
		{
			return true;
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000FDEE9 File Offset: 0x000FC0E9
		public virtual bool HandleEvent(GenericDeepRatingEvent E)
		{
			return true;
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000FDEEC File Offset: 0x000FC0EC
		public virtual bool HandleEvent(GenericNotifyEvent E)
		{
			return true;
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000FDEEF File Offset: 0x000FC0EF
		public virtual bool HandleEvent(GenericQueryEvent E)
		{
			return true;
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000FDEF2 File Offset: 0x000FC0F2
		public virtual bool HandleEvent(GenericRatingEvent E)
		{
			return true;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000FDEF5 File Offset: 0x000FC0F5
		public virtual bool HandleEvent(GetActivationPhaseEvent E)
		{
			return true;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000FDEF8 File Offset: 0x000FC0F8
		public virtual bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000FDEFB File Offset: 0x000FC0FB
		public virtual bool HandleEvent(GetAmbientLightEvent E)
		{
			return true;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000FDEFE File Offset: 0x000FC0FE
		public virtual bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			return true;
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000FDF01 File Offset: 0x000FC101
		public virtual bool HandleEvent(GetApparentSpeciesEvent E)
		{
			return true;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000FDF04 File Offset: 0x000FC104
		public virtual bool HandleEvent(GetAttackerHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000FDF07 File Offset: 0x000FC107
		public virtual bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000FDF0A File Offset: 0x000FC10A
		public virtual bool HandleEvent(GetAutoCollectDramsEvent E)
		{
			return true;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000FDF0D File Offset: 0x000FC10D
		public virtual bool HandleEvent(GetAutoEquipPriorityEvent E)
		{
			return true;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000FDF10 File Offset: 0x000FC110
		public virtual bool HandleEvent(GetAvailableComputePowerEvent E)
		{
			return true;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000FDF13 File Offset: 0x000FC113
		public virtual bool HandleEvent(GetBandagePerformanceEvent E)
		{
			return true;
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000FDF16 File Offset: 0x000FC116
		public virtual bool HandleEvent(GetBleedLiquidEvent E)
		{
			return true;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000FDF19 File Offset: 0x000FC119
		public virtual bool HandleEvent(GetCarriedWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000FDF1C File Offset: 0x000FC11C
		public virtual bool HandleEvent(GetCleaningItemsEvent E)
		{
			return true;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000FDF1F File Offset: 0x000FC11F
		public virtual bool HandleEvent(GetCleaningItemsNearbyEvent E)
		{
			return true;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000FDF22 File Offset: 0x000FC122
		public virtual bool HandleEvent(GetCleaveAmountEvent E)
		{
			return true;
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000FDF25 File Offset: 0x000FC125
		public virtual bool HandleEvent(GetCleaveMaxPenaltyEvent E)
		{
			return true;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000FDF28 File Offset: 0x000FC128
		public virtual bool HandleEvent(GetCompanionLimitEvent E)
		{
			return true;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000FDF2B File Offset: 0x000FC12B
		public virtual bool HandleEvent(GetCompanionStatusEvent E)
		{
			return true;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000FDF2E File Offset: 0x000FC12E
		public virtual bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000FDF31 File Offset: 0x000FC131
		public virtual bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000FDF34 File Offset: 0x000FC134
		public virtual bool HandleEvent(GetContentsEvent E)
		{
			return true;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000FDF37 File Offset: 0x000FC137
		public virtual bool HandleEvent(GetContextEvent E)
		{
			return true;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000FDF3A File Offset: 0x000FC13A
		public virtual bool HandleEvent(GetCookingActionsEvent E)
		{
			return true;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000FDF3D File Offset: 0x000FC13D
		public virtual bool HandleEvent(GetCooldownEvent E)
		{
			return true;
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000FDF40 File Offset: 0x000FC140
		public virtual bool HandleEvent(GetCriticalThresholdEvent E)
		{
			return true;
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000FDF43 File Offset: 0x000FC143
		public virtual bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000FDF46 File Offset: 0x000FC146
		public virtual bool HandleEvent(GetDebugInternalsEvent E)
		{
			return true;
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000FDF49 File Offset: 0x000FC149
		public virtual bool HandleEvent(GetDefenderHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000FDF4C File Offset: 0x000FC14C
		public virtual bool HandleEvent(GetDefenderMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000FDF4F File Offset: 0x000FC14F
		public virtual bool HandleEvent(GetDifficultyEvaluationEvent E)
		{
			return true;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000FDF52 File Offset: 0x000FC152
		public virtual bool HandleEvent(GetDiseaseOnsetEvent E)
		{
			return true;
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000FDF55 File Offset: 0x000FC155
		public virtual bool HandleEvent(GetDisplayNameEvent E)
		{
			return true;
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000FDF58 File Offset: 0x000FC158
		public virtual bool HandleEvent(GetDisplayNamePenetrationColorEvent E)
		{
			return true;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000FDF5B File Offset: 0x000FC15B
		public virtual bool HandleEvent(GetDisplayStatBonusEvent E)
		{
			return true;
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000FDF5E File Offset: 0x000FC15E
		public virtual bool HandleEvent(GetElectricalConductivityEvent E)
		{
			return true;
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000FDF61 File Offset: 0x000FC161
		public virtual bool HandleEvent(GetEnergyCostEvent E)
		{
			return true;
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000FDF64 File Offset: 0x000FC164
		public virtual bool HandleEvent(GetExtraPhysicalFeaturesEvent E)
		{
			return true;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000FDF67 File Offset: 0x000FC167
		public virtual bool HandleEvent(GetExtrinsicValueEvent E)
		{
			return true;
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000FDF6A File Offset: 0x000FC16A
		public virtual bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			return true;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000FDF6D File Offset: 0x000FC16D
		public virtual bool HandleEvent(GetFactionRankEvent E)
		{
			return true;
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000FDF70 File Offset: 0x000FC170
		public virtual bool HandleEvent(GetFeelingEvent E)
		{
			return true;
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000FDF73 File Offset: 0x000FC173
		public virtual bool HandleEvent(GetFirefightingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000FDF76 File Offset: 0x000FC176
		public virtual bool HandleEvent(GetFixedMissileSpreadEvent E)
		{
			return true;
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000FDF79 File Offset: 0x000FC179
		public virtual bool HandleEvent(GetFreeDramsEvent E)
		{
			return true;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000FDF7C File Offset: 0x000FC17C
		public virtual bool HandleEvent(GetGameObjectSortEvent E)
		{
			return true;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000FDF7F File Offset: 0x000FC17F
		public virtual bool HandleEvent(GetGenderEvent E)
		{
			return true;
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000FDF82 File Offset: 0x000FC182
		public virtual bool HandleEvent(GetHostileWalkRadiusEvent E)
		{
			return true;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000FDF85 File Offset: 0x000FC185
		public virtual bool HandleEvent(GetHostilityRecognitionLimitsEvent E)
		{
			return true;
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000FDF88 File Offset: 0x000FC188
		public virtual bool HandleEvent(GetIdealTemperatureEvent E)
		{
			return true;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000FDF8B File Offset: 0x000FC18B
		public virtual bool HandleEvent(GetIntrinsicValueEvent E)
		{
			return true;
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000FDF8E File Offset: 0x000FC18E
		public virtual bool HandleEvent(GetIntrinsicWeightEvent E)
		{
			return true;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000FDF91 File Offset: 0x000FC191
		public virtual bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			return true;
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000FDF94 File Offset: 0x000FC194
		public virtual bool HandleEvent(GetInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000FDF97 File Offset: 0x000FC197
		public virtual bool HandleEvent(GetInventoryCategoryEvent E)
		{
			return true;
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000FDF9A File Offset: 0x000FC19A
		public virtual bool HandleEvent(GetItemElementsEvent E)
		{
			return true;
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000FDF9D File Offset: 0x000FC19D
		public virtual bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000FDFA0 File Offset: 0x000FC1A0
		public virtual bool HandleEvent(GetKineticResistanceEvent E)
		{
			return true;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000FDFA3 File Offset: 0x000FC1A3
		public virtual bool HandleEvent(GetLevelUpDiceEvent E)
		{
			return true;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000FDFA6 File Offset: 0x000FC1A6
		public virtual bool HandleEvent(GetLevelUpPointsEvent E)
		{
			return true;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000FDFA9 File Offset: 0x000FC1A9
		public virtual bool HandleEvent(GetLevelUpSkillPointsEvent E)
		{
			return true;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000FDFAC File Offset: 0x000FC1AC
		public virtual bool HandleEvent(GetLostChanceEvent E)
		{
			return true;
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000FDFAF File Offset: 0x000FC1AF
		public virtual bool HandleEvent(GetMatterPhaseEvent E)
		{
			return true;
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000FDFB2 File Offset: 0x000FC1B2
		public virtual bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			return true;
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000FDFB5 File Offset: 0x000FC1B5
		public virtual bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			return true;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000FDFB8 File Offset: 0x000FC1B8
		public virtual bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			return true;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000FDFBB File Offset: 0x000FC1BB
		public virtual bool HandleEvent(GetMeleeAttacksEvent E)
		{
			return true;
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000FDFBE File Offset: 0x000FC1BE
		public virtual bool HandleEvent(GetMissileCoverPercentageEvent E)
		{
			return true;
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000FDFC1 File Offset: 0x000FC1C1
		public virtual bool HandleEvent(GetMissileStatusColorEvent E)
		{
			return true;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000FDFC4 File Offset: 0x000FC1C4
		public virtual bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000FDFC7 File Offset: 0x000FC1C7
		public virtual bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			return true;
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000FDFCA File Offset: 0x000FC1CA
		public virtual bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			return true;
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000FDFCD File Offset: 0x000FC1CD
		public virtual bool HandleEvent(GetModRarityWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000FDFD0 File Offset: 0x000FC1D0
		public virtual bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			return true;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000FDFD3 File Offset: 0x000FC1D3
		public virtual bool HandleEvent(GetMutationTermEvent E)
		{
			return true;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000FDFD6 File Offset: 0x000FC1D6
		public virtual bool HandleEvent(GetNamingBestowalChanceEvent E)
		{
			return true;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000FDFD9 File Offset: 0x000FC1D9
		public virtual bool HandleEvent(GetNamingChanceEvent E)
		{
			return true;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000FDFDC File Offset: 0x000FC1DC
		public virtual bool HandleEvent(GetNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000FDFDF File Offset: 0x000FC1DF
		public virtual bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			return true;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000FDFE2 File Offset: 0x000FC1E2
		public virtual bool HandleEvent(GetOverloadChargeEvent E)
		{
			return true;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000FDFE5 File Offset: 0x000FC1E5
		public virtual bool HandleEvent(GetPartyLeaderFollowDistanceEvent E)
		{
			return true;
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000FDFE8 File Offset: 0x000FC1E8
		public virtual bool HandleEvent(GetPointsOfInterestEvent E)
		{
			return true;
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000FDFEB File Offset: 0x000FC1EB
		public virtual bool HandleEvent(GetPowerLoadLevelEvent E)
		{
			return true;
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000FDFEE File Offset: 0x000FC1EE
		public virtual bool HandleEvent(GetPrecognitionRestoreGameStateEvent E)
		{
			return true;
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000FDFF1 File Offset: 0x000FC1F1
		public virtual bool HandleEvent(GetPreferredLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000FDFF4 File Offset: 0x000FC1F4
		public virtual bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000FDFF7 File Offset: 0x000FC1F7
		public virtual bool HandleEvent(GetProjectileObjectEvent E)
		{
			return true;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000FDFFA File Offset: 0x000FC1FA
		public virtual bool HandleEvent(GetPropertyModDescription E)
		{
			return true;
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000FDFFD File Offset: 0x000FC1FD
		public virtual bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000FE000 File Offset: 0x000FC200
		public virtual bool HandleEvent(GetPsychicGlimmerEvent E)
		{
			return true;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000FE003 File Offset: 0x000FC203
		public virtual bool HandleEvent(GetRandomBuyChimericBodyPartRollsEvent E)
		{
			return true;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000FE006 File Offset: 0x000FC206
		public virtual bool HandleEvent(GetRandomBuyMutationCountEvent E)
		{
			return true;
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000FE009 File Offset: 0x000FC209
		public virtual bool HandleEvent(GetRealityStabilizationPenetrationEvent E)
		{
			return true;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000FE00C File Offset: 0x000FC20C
		public virtual bool HandleEvent(GetRebukeLevelEvent E)
		{
			return true;
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000FE00F File Offset: 0x000FC20F
		public virtual bool HandleEvent(GetRecoilersEvent E)
		{
			return true;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000FE012 File Offset: 0x000FC212
		public virtual bool HandleEvent(GetReplaceCellInteractionsEvent E)
		{
			return true;
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000FE015 File Offset: 0x000FC215
		public virtual bool HandleEvent(GetRespiratoryAgentPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000FE018 File Offset: 0x000FC218
		public virtual bool HandleEvent(GetRitualSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000FE01B File Offset: 0x000FC21B
		public virtual bool HandleEvent(GetRunningBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000FE01E File Offset: 0x000FC21E
		public virtual bool HandleEvent(GetScanTypeEvent E)
		{
			return true;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000FE021 File Offset: 0x000FC221
		public virtual bool HandleEvent(GetShieldBlockPreferenceEvent E)
		{
			return true;
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000FE024 File Offset: 0x000FC224
		public virtual bool HandleEvent(GetShieldSlamDamageEvent E)
		{
			return true;
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000FE027 File Offset: 0x000FC227
		public virtual bool HandleEvent(GetShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000FE02A File Offset: 0x000FC22A
		public virtual bool HandleEvent(GetSkillEffectChanceEvent E)
		{
			return true;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000FE02D File Offset: 0x000FC22D
		public virtual bool HandleEvent(GetSlotsRequiredEvent E)
		{
			return true;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000FE030 File Offset: 0x000FC230
		public virtual bool HandleEvent(GetSlottedInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000FE033 File Offset: 0x000FC233
		public virtual bool HandleEvent(GetSocialSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000FE036 File Offset: 0x000FC236
		public virtual bool HandleEvent(GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent E)
		{
			return true;
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000FE039 File Offset: 0x000FC239
		public virtual bool HandleEvent(GetSpaceTimeAnomalyEmergencePermillageChanceEvent E)
		{
			return true;
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000FE03C File Offset: 0x000FC23C
		public virtual bool HandleEvent(GetSpecialEffectChanceEvent E)
		{
			return true;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000FE03F File Offset: 0x000FC23F
		public virtual bool HandleEvent(GetSpringinessEvent E)
		{
			return true;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000FE042 File Offset: 0x000FC242
		public virtual bool HandleEvent(GetSprintDurationEvent E)
		{
			return true;
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000FE045 File Offset: 0x000FC245
		public virtual bool HandleEvent(GetStorableDramsEvent E)
		{
			return true;
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000FE048 File Offset: 0x000FC248
		public virtual bool HandleEvent(GetSwimmingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000FE04B File Offset: 0x000FC24B
		public virtual bool HandleEvent(GetThrowProfileEvent E)
		{
			return true;
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000FE04E File Offset: 0x000FC24E
		public virtual bool HandleEvent(GetThrownWeaponFlexPhaseProviderEvent E)
		{
			return true;
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000FE051 File Offset: 0x000FC251
		public virtual bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000FE054 File Offset: 0x000FC254
		public virtual bool HandleEvent(GetThrownWeaponRangeEvent E)
		{
			return true;
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000FE057 File Offset: 0x000FC257
		public virtual bool HandleEvent(GetTinkeringBonusEvent E)
		{
			return true;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000FE05A File Offset: 0x000FC25A
		public virtual bool HandleEvent(GetToHitModifierEvent E)
		{
			return true;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000FE05D File Offset: 0x000FC25D
		public virtual bool HandleEvent(GetTonicCapacityEvent E)
		{
			return true;
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000FE060 File Offset: 0x000FC260
		public virtual bool HandleEvent(GetTonicDosageEvent E)
		{
			return true;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000FE063 File Offset: 0x000FC263
		public virtual bool HandleEvent(GetTonicDurationEvent E)
		{
			return true;
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000FE066 File Offset: 0x000FC266
		public virtual bool HandleEvent(GetTradePerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000FE069 File Offset: 0x000FC269
		public virtual bool HandleEvent(GetTransitiveLocationEvent E)
		{
			return true;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000FE06C File Offset: 0x000FC26C
		public virtual bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000FE06F File Offset: 0x000FC26F
		public virtual bool HandleEvent(GetUtilityScoreEvent E)
		{
			return true;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000FE072 File Offset: 0x000FC272
		public virtual bool HandleEvent(GetWadingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000FE075 File Offset: 0x000FC275
		public virtual bool HandleEvent(GetWaterRitualCostEvent E)
		{
			return true;
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000FE078 File Offset: 0x000FC278
		public virtual bool HandleEvent(GetWaterRitualLiquidEvent E)
		{
			return true;
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000FE07B File Offset: 0x000FC27B
		public virtual bool HandleEvent(GetWaterRitualReputationAmountEvent E)
		{
			return true;
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000FE07E File Offset: 0x000FC27E
		public virtual bool HandleEvent(GetWaterRitualSecretWeightEvent E)
		{
			return true;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000FE081 File Offset: 0x000FC281
		public virtual bool HandleEvent(GetWaterRitualSellSecretBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000FE084 File Offset: 0x000FC284
		public virtual bool HandleEvent(GetWeaponHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000FE087 File Offset: 0x000FC287
		public virtual bool HandleEvent(GetWeaponMeleeAttacksEvent E)
		{
			return true;
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000FE08A File Offset: 0x000FC28A
		public virtual bool HandleEvent(GetWeaponMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000FE08D File Offset: 0x000FC28D
		public virtual bool HandleEvent(GetZoneEvent E)
		{
			return true;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000FE090 File Offset: 0x000FC290
		public virtual bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			return true;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000FE093 File Offset: 0x000FC293
		public virtual bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			return true;
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000FE096 File Offset: 0x000FC296
		public virtual bool HandleEvent(GiveDramsEvent E)
		{
			return true;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x000FE099 File Offset: 0x000FC299
		public virtual bool HandleEvent(GlimmerChangeEvent E)
		{
			return true;
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000FE09C File Offset: 0x000FC29C
		public virtual bool HandleEvent(GravitationEvent E)
		{
			return true;
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000FE09F File Offset: 0x000FC29F
		public virtual bool HandleEvent(HasBeenReadEvent E)
		{
			return true;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000FE0A2 File Offset: 0x000FC2A2
		public virtual bool HandleEvent(HasBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000FE0A5 File Offset: 0x000FC2A5
		public virtual bool HandleEvent(HasFlammableEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000FE0A8 File Offset: 0x000FC2A8
		public virtual bool HandleEvent(HasFlammableEquipmentOrInventoryEvent E)
		{
			return true;
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000FE0AB File Offset: 0x000FC2AB
		public virtual bool HandleEvent(HealsNaturallyEvent E)
		{
			return true;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000FE0AE File Offset: 0x000FC2AE
		public virtual bool HandleEvent(IAICommandListEvent E)
		{
			return true;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000FE0B1 File Offset: 0x000FC2B1
		public virtual bool HandleEvent(IAIItemCommandListEvent E)
		{
			return true;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000FE0B4 File Offset: 0x000FC2B4
		public virtual bool HandleEvent(IAIMoveCommandListEvent E)
		{
			return true;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000FE0B7 File Offset: 0x000FC2B7
		public virtual bool HandleEvent(IActOnItemEvent E)
		{
			return true;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000FE0BA File Offset: 0x000FC2BA
		public virtual bool HandleEvent(IActualEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000FE0BD File Offset: 0x000FC2BD
		public virtual bool HandleEvent(IAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000FE0C0 File Offset: 0x000FC2C0
		public virtual bool HandleEvent(IAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000FE0C3 File Offset: 0x000FC2C3
		public virtual bool HandleEvent(IBootSequenceEvent E)
		{
			return true;
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000FE0C6 File Offset: 0x000FC2C6
		public virtual bool HandleEvent(IChargeConsumptionEvent E)
		{
			return true;
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000FE0C9 File Offset: 0x000FC2C9
		public virtual bool HandleEvent(IChargeEvent E)
		{
			return true;
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000FE0CC File Offset: 0x000FC2CC
		public virtual bool HandleEvent(IChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000FE0CF File Offset: 0x000FC2CF
		public virtual bool HandleEvent(IChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000FE0D2 File Offset: 0x000FC2D2
		public virtual bool HandleEvent(IConsumeEvent E)
		{
			return true;
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000FE0D5 File Offset: 0x000FC2D5
		public virtual bool HandleEvent(IConversationMinEvent E)
		{
			return true;
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000FE0D8 File Offset: 0x000FC2D8
		public virtual bool HandleEvent(IDamageEvent E)
		{
			return true;
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000FE0DB File Offset: 0x000FC2DB
		public virtual bool HandleEvent(IDeathEvent E)
		{
			return true;
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000FE0DE File Offset: 0x000FC2DE
		public virtual bool HandleEvent(IDerivationEvent E)
		{
			return true;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000FE0E1 File Offset: 0x000FC2E1
		public virtual bool HandleEvent(IDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000FE0E4 File Offset: 0x000FC2E4
		public virtual bool HandleEvent(IEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000FE0E7 File Offset: 0x000FC2E7
		public virtual bool HandleEvent(IExamineEvent E)
		{
			return true;
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000FE0EA File Offset: 0x000FC2EA
		public virtual bool HandleEvent(IFinalChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000FE0ED File Offset: 0x000FC2ED
		public virtual bool HandleEvent(IHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000FE0F0 File Offset: 0x000FC2F0
		public virtual bool HandleEvent(IInitialChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000FE0F3 File Offset: 0x000FC2F3
		public virtual bool HandleEvent(IInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000FE0F6 File Offset: 0x000FC2F6
		public virtual bool HandleEvent(ILimbRegenerationEvent E)
		{
			return true;
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000FE0F9 File Offset: 0x000FC2F9
		public virtual bool HandleEvent(ILiquidEvent E)
		{
			return true;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000FE0FC File Offset: 0x000FC2FC
		public virtual bool HandleEvent(IMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000FE0FF File Offset: 0x000FC2FF
		public virtual bool HandleEvent(IMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000FE102 File Offset: 0x000FC302
		public virtual bool HandleEvent(IMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000FE105 File Offset: 0x000FC305
		public virtual bool HandleEvent(INavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000FE108 File Offset: 0x000FC308
		public virtual bool HandleEvent(IObjectCellInteractionEvent E)
		{
			return true;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000FE10B File Offset: 0x000FC30B
		public virtual bool HandleEvent(IObjectCreationEvent E)
		{
			return true;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000FE10E File Offset: 0x000FC30E
		public virtual bool HandleEvent(IObjectInventoryInteractionEvent E)
		{
			return true;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000FE111 File Offset: 0x000FC311
		public virtual bool HandleEvent(IQuestEvent E)
		{
			return true;
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000FE114 File Offset: 0x000FC314
		public virtual bool HandleEvent(IRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000FE117 File Offset: 0x000FC317
		public virtual bool HandleEvent(IReplicationEvent E)
		{
			return true;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000FE11A File Offset: 0x000FC31A
		public virtual bool HandleEvent(ISaveEvent E)
		{
			return true;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000FE11D File Offset: 0x000FC31D
		public virtual bool HandleEvent(IShallowZoneEvent E)
		{
			return true;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000FE120 File Offset: 0x000FC320
		public virtual bool HandleEvent(IShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000FE123 File Offset: 0x000FC323
		public virtual bool HandleEvent(ITravelEvent E)
		{
			return true;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000FE126 File Offset: 0x000FC326
		public virtual bool HandleEvent(IValueEvent E)
		{
			return true;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000FE129 File Offset: 0x000FC329
		public virtual bool HandleEvent(IWeightEvent E)
		{
			return true;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000FE12C File Offset: 0x000FC32C
		public virtual bool HandleEvent(IXPEvent E)
		{
			return true;
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000FE12F File Offset: 0x000FC32F
		public virtual bool HandleEvent(IZoneEvent E)
		{
			return true;
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000FE132 File Offset: 0x000FC332
		public virtual bool HandleEvent(IZoneOnlyEvent E)
		{
			return true;
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000FE135 File Offset: 0x000FC335
		public virtual bool HandleEvent(IdleQueryEvent E)
		{
			return true;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000FE138 File Offset: 0x000FC338
		public virtual bool HandleEvent(ImplantAddedEvent E)
		{
			return true;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000FE13B File Offset: 0x000FC33B
		public virtual bool HandleEvent(ImplantRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000FE13E File Offset: 0x000FC33E
		public virtual bool HandleEvent(ImplantedEvent E)
		{
			return true;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000FE141 File Offset: 0x000FC341
		public virtual bool HandleEvent(InduceVomitingEvent E)
		{
			return true;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000FE144 File Offset: 0x000FC344
		public virtual bool HandleEvent(InductionChargeEvent E)
		{
			return true;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000FE147 File Offset: 0x000FC347
		public virtual bool HandleEvent(InteriorZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000FE14A File Offset: 0x000FC34A
		public virtual bool HandleEvent(InterruptAutowalkEvent E)
		{
			return true;
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000FE14D File Offset: 0x000FC34D
		public virtual bool HandleEvent(InventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000FE150 File Offset: 0x000FC350
		public virtual bool HandleEvent(IsAdaptivePenetrationActiveEvent E)
		{
			return true;
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000FE153 File Offset: 0x000FC353
		public virtual bool HandleEvent(IsAfflictionEvent E)
		{
			return true;
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000FE156 File Offset: 0x000FC356
		public virtual bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			return true;
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000FE159 File Offset: 0x000FC359
		public virtual bool HandleEvent(IsExplosiveEvent E)
		{
			return true;
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000FE15C File Offset: 0x000FC35C
		public virtual bool HandleEvent(IsMutantEvent E)
		{
			return true;
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000FE15F File Offset: 0x000FC35F
		public virtual bool HandleEvent(IsOverloadableEvent E)
		{
			return true;
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000FE162 File Offset: 0x000FC362
		public virtual bool HandleEvent(IsRepairableEvent E)
		{
			return true;
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000FE165 File Offset: 0x000FC365
		public virtual bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return true;
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000FE168 File Offset: 0x000FC368
		public virtual bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			return true;
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000FE16B File Offset: 0x000FC36B
		public virtual bool HandleEvent(IsTrueKinEvent E)
		{
			return true;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000FE16E File Offset: 0x000FC36E
		public virtual bool HandleEvent(IsVehicleOperationalEvent E)
		{
			return true;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000FE171 File Offset: 0x000FC371
		public virtual bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			return true;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000FE174 File Offset: 0x000FC374
		public virtual bool HandleEvent(JoinedPartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000FE177 File Offset: 0x000FC377
		public virtual bool HandleEvent(JumpedEvent E)
		{
			return true;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000FE17A File Offset: 0x000FC37A
		public virtual bool HandleEvent(KilledEvent E)
		{
			return true;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000FE17D File Offset: 0x000FC37D
		public virtual bool HandleEvent(KilledPlayerEvent E)
		{
			return true;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000FE180 File Offset: 0x000FC380
		public virtual bool HandleEvent(LateBeforeApplyDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000FE183 File Offset: 0x000FC383
		public virtual bool HandleEvent(LeaveCellEvent E)
		{
			return true;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000FE186 File Offset: 0x000FC386
		public virtual bool HandleEvent(LeavingCellEvent E)
		{
			return true;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000FE189 File Offset: 0x000FC389
		public virtual bool HandleEvent(LeftCellEvent E)
		{
			return true;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000FE18C File Offset: 0x000FC38C
		public virtual bool HandleEvent(LiquidMixedEvent E)
		{
			return true;
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000FE18F File Offset: 0x000FC38F
		public virtual bool HandleEvent(LoadAmmoEvent E)
		{
			return true;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000FE192 File Offset: 0x000FC392
		public virtual bool HandleEvent(MakeTemporaryEvent E)
		{
			return true;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000FE195 File Offset: 0x000FC395
		public virtual bool HandleEvent(MentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000FE198 File Offset: 0x000FC398
		public virtual bool HandleEvent(MissilePenetrateEvent E)
		{
			return true;
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000FE19B File Offset: 0x000FC39B
		public virtual bool HandleEvent(MissileTraversingCellEvent E)
		{
			return true;
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000FE19E File Offset: 0x000FC39E
		public virtual bool HandleEvent(ModificationAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000FE1A1 File Offset: 0x000FC3A1
		public virtual bool HandleEvent(ModifyAttackingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000FE1A4 File Offset: 0x000FC3A4
		public virtual bool HandleEvent(ModifyBitCostEvent E)
		{
			return true;
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000FE1A7 File Offset: 0x000FC3A7
		public virtual bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000FE1AA File Offset: 0x000FC3AA
		public virtual bool HandleEvent(ModifyOriginatingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000FE1AD File Offset: 0x000FC3AD
		public virtual bool HandleEvent(MovementModeChangedEvent E)
		{
			return true;
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000FE1B0 File Offset: 0x000FC3B0
		public virtual bool HandleEvent(MutationsSubjectToEMPEvent E)
		{
			return true;
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000FE1B3 File Offset: 0x000FC3B3
		public virtual bool HandleEvent(NeedPartSupportEvent E)
		{
			return true;
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000FE1B6 File Offset: 0x000FC3B6
		public virtual bool HandleEvent(NeedsReloadEvent E)
		{
			return true;
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000FE1B9 File Offset: 0x000FC3B9
		public virtual bool HandleEvent(NeutronFluxPourExplodesEvent E)
		{
			return true;
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000FE1BC File Offset: 0x000FC3BC
		public virtual bool HandleEvent(NotifyTargetImmuneEvent E)
		{
			return true;
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000FE1BF File Offset: 0x000FC3BF
		public virtual bool HandleEvent(ObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000FE1C2 File Offset: 0x000FC3C2
		public virtual bool HandleEvent(ObjectEnteredCellEvent E)
		{
			return true;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000FE1C5 File Offset: 0x000FC3C5
		public virtual bool HandleEvent(ObjectEnteringCellEvent E)
		{
			return true;
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000FE1C8 File Offset: 0x000FC3C8
		public virtual bool HandleEvent(ObjectGoingProneEvent E)
		{
			return true;
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000FE1CB File Offset: 0x000FC3CB
		public virtual bool HandleEvent(ObjectLeavingCellEvent E)
		{
			return true;
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000FE1CE File Offset: 0x000FC3CE
		public virtual bool HandleEvent(ObjectStartedFlyingEvent E)
		{
			return true;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000FE1D1 File Offset: 0x000FC3D1
		public virtual bool HandleEvent(ObjectStoppedFlyingEvent E)
		{
			return true;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000FE1D4 File Offset: 0x000FC3D4
		public virtual bool HandleEvent(OkayToDamageEvent E)
		{
			return true;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000FE1D7 File Offset: 0x000FC3D7
		public virtual bool HandleEvent(OnDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000FE1DA File Offset: 0x000FC3DA
		public virtual bool HandleEvent(OnDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000FE1DD File Offset: 0x000FC3DD
		public virtual bool HandleEvent(OnQuestAddedEvent E)
		{
			return true;
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000FE1E0 File Offset: 0x000FC3E0
		public virtual bool HandleEvent(OwnerAfterInventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000FE1E3 File Offset: 0x000FC3E3
		public virtual bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000FE1E6 File Offset: 0x000FC3E6
		public virtual bool HandleEvent(OwnerGetShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000FE1E9 File Offset: 0x000FC3E9
		public virtual bool HandleEvent(OwnerGetUnknownShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000FE1EC File Offset: 0x000FC3EC
		public virtual bool HandleEvent(PartAddedEvent E)
		{
			return true;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000FE1EF File Offset: 0x000FC3EF
		public virtual bool HandleEvent(PartRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000FE1F2 File Offset: 0x000FC3F2
		public virtual bool HandleEvent(PartSupportEvent E)
		{
			return true;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000FE1F5 File Offset: 0x000FC3F5
		public virtual bool HandleEvent(PathAsBurrowerEvent E)
		{
			return true;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000FE1F8 File Offset: 0x000FC3F8
		public virtual bool HandleEvent(PhysicalContactEvent E)
		{
			return true;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000FE1FB File Offset: 0x000FC3FB
		public virtual bool HandleEvent(PointDefenseInterceptEvent E)
		{
			return true;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000FE1FE File Offset: 0x000FC3FE
		public virtual bool HandleEvent(PollForHealingLocationEvent E)
		{
			return true;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000FE201 File Offset: 0x000FC401
		public virtual bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			return true;
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000FE204 File Offset: 0x000FC404
		public virtual bool HandleEvent(PowerUpdatedEvent E)
		{
			return true;
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000FE207 File Offset: 0x000FC407
		public virtual bool HandleEvent(PreferDefaultBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000FE20A File Offset: 0x000FC40A
		public virtual bool HandleEvent(PreferTargetEvent E)
		{
			return true;
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000FE20D File Offset: 0x000FC40D
		public virtual bool HandleEvent(PreventSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000FE210 File Offset: 0x000FC410
		public virtual bool HandleEvent(PrimePowerSystemsEvent E)
		{
			return true;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000FE213 File Offset: 0x000FC413
		public virtual bool HandleEvent(ProducesLiquidEvent E)
		{
			return true;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000FE216 File Offset: 0x000FC416
		public virtual bool HandleEvent(ProjectileMovingEvent E)
		{
			return true;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000FE219 File Offset: 0x000FC419
		public virtual bool HandleEvent(QueryBroadcastDrawEvent E)
		{
			return true;
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000FE21C File Offset: 0x000FC41C
		public virtual bool HandleEvent(QueryChargeEvent E)
		{
			return true;
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000FE21F File Offset: 0x000FC41F
		public virtual bool HandleEvent(QueryChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000FE222 File Offset: 0x000FC422
		public virtual bool HandleEvent(QueryChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000FE225 File Offset: 0x000FC425
		public virtual bool HandleEvent(QueryDrawEvent E)
		{
			return true;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000FE228 File Offset: 0x000FC428
		public virtual bool HandleEvent(QueryEquippableListEvent E)
		{
			return true;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000FE22B File Offset: 0x000FC42B
		public virtual bool HandleEvent(QueryInductionChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000FE22E File Offset: 0x000FC42E
		public virtual bool HandleEvent(QueryRechargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000FE231 File Offset: 0x000FC431
		public virtual bool HandleEvent(QuerySlotListEvent E)
		{
			return true;
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000FE234 File Offset: 0x000FC434
		public virtual bool HandleEvent(QuestFinishedEvent E)
		{
			return true;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000FE237 File Offset: 0x000FC437
		public virtual bool HandleEvent(QuestStartedEvent E)
		{
			return true;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000FE23A File Offset: 0x000FC43A
		public virtual bool HandleEvent(QuestStepFinishedEvent E)
		{
			return true;
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000FE23D File Offset: 0x000FC43D
		public virtual bool HandleEvent(RadiatesHeatAdjacentEvent E)
		{
			return true;
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000FE240 File Offset: 0x000FC440
		public virtual bool HandleEvent(RadiatesHeatEvent E)
		{
			return true;
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000FE243 File Offset: 0x000FC443
		public virtual bool HandleEvent(RealityStabilizeEvent E)
		{
			return true;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000FE246 File Offset: 0x000FC446
		public virtual bool HandleEvent(RechargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000FE249 File Offset: 0x000FC449
		public virtual bool HandleEvent(RefreshTileEvent E)
		{
			return true;
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000FE24C File Offset: 0x000FC44C
		public virtual bool HandleEvent(RegenerateDefaultEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000FE24F File Offset: 0x000FC44F
		public virtual bool HandleEvent(RegenerateLimbEvent E)
		{
			return true;
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000FE252 File Offset: 0x000FC452
		public virtual bool HandleEvent(RemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000FE255 File Offset: 0x000FC455
		public virtual bool HandleEvent(RepaintedEvent E)
		{
			return true;
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000FE258 File Offset: 0x000FC458
		public virtual bool HandleEvent(RepairCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000FE25B File Offset: 0x000FC45B
		public virtual bool HandleEvent(RepairedEvent E)
		{
			return true;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000FE25E File Offset: 0x000FC45E
		public virtual bool HandleEvent(ReplaceInContextEvent E)
		{
			return true;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000FE261 File Offset: 0x000FC461
		public virtual bool HandleEvent(ReplaceThrownWeaponEvent E)
		{
			return true;
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000FE264 File Offset: 0x000FC464
		public virtual bool HandleEvent(ReplicaCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000FE267 File Offset: 0x000FC467
		public virtual bool HandleEvent(ReputationChangeEvent E)
		{
			return true;
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000FE26A File Offset: 0x000FC46A
		public virtual bool HandleEvent(RespiresEvent E)
		{
			return true;
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000FE26D File Offset: 0x000FC46D
		public virtual bool HandleEvent(SecretVisibilityChangedEvent E)
		{
			return true;
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000FE270 File Offset: 0x000FC470
		public virtual bool HandleEvent(ShieldSlamPerformedEvent E)
		{
			return true;
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000FE273 File Offset: 0x000FC473
		public virtual bool HandleEvent(ShotCompleteEvent E)
		{
			return true;
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000FE276 File Offset: 0x000FC476
		public virtual bool HandleEvent(ShouldAttackToReachTargetEvent E)
		{
			return true;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000FE279 File Offset: 0x000FC479
		public virtual bool HandleEvent(ShouldDescribeStatBonusEvent E)
		{
			return true;
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000FE27C File Offset: 0x000FC47C
		public virtual bool HandleEvent(SlamEvent E)
		{
			return true;
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000FE27F File Offset: 0x000FC47F
		public virtual bool HandleEvent(StackCountChangedEvent E)
		{
			return true;
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000FE282 File Offset: 0x000FC482
		public virtual bool HandleEvent(StartTradeEvent E)
		{
			return true;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000FE285 File Offset: 0x000FC485
		public virtual bool HandleEvent(StatChangeEvent E)
		{
			return true;
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000FE288 File Offset: 0x000FC488
		public virtual bool HandleEvent(StockedEvent E)
		{
			return true;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000FE28B File Offset: 0x000FC48B
		public virtual bool HandleEvent(StripContentsEvent E)
		{
			return true;
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000FE28E File Offset: 0x000FC48E
		public virtual bool HandleEvent(SubjectToGravityEvent E)
		{
			return true;
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000FE291 File Offset: 0x000FC491
		public virtual bool HandleEvent(SuspendingEvent E)
		{
			return true;
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000FE294 File Offset: 0x000FC494
		public virtual bool HandleEvent(SyncMutationLevelsEvent E)
		{
			return true;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000FE297 File Offset: 0x000FC497
		public virtual bool HandleEvent(SyncRenderEvent E)
		{
			return true;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000FE29A File Offset: 0x000FC49A
		public virtual bool HandleEvent(SynchronizeExistenceEvent E)
		{
			return true;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000FE29D File Offset: 0x000FC49D
		public virtual bool HandleEvent(TakeOnRoleEvent E)
		{
			return true;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000FE2A0 File Offset: 0x000FC4A0
		public virtual bool HandleEvent(TakenEvent E)
		{
			return true;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000FE2A3 File Offset: 0x000FC4A3
		public virtual bool HandleEvent(TestChargeEvent E)
		{
			return true;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000FE2A6 File Offset: 0x000FC4A6
		public virtual bool HandleEvent(ThawedEvent E)
		{
			return true;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000FE2A9 File Offset: 0x000FC4A9
		public virtual bool HandleEvent(TookDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000FE2AC File Offset: 0x000FC4AC
		public virtual bool HandleEvent(TookEnvironmentalDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000FE2AF File Offset: 0x000FC4AF
		public virtual bool HandleEvent(TookEvent E)
		{
			return true;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000FE2B2 File Offset: 0x000FC4B2
		public virtual bool HandleEvent(TransparentToEMPEvent E)
		{
			return true;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000FE2B5 File Offset: 0x000FC4B5
		public virtual bool HandleEvent(TravelSpeedEvent E)
		{
			return true;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000FE2B8 File Offset: 0x000FC4B8
		public virtual bool HandleEvent(TriggersMakersMarkCreationEvent E)
		{
			return true;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000FE2BB File Offset: 0x000FC4BB
		public virtual bool HandleEvent(TryRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000FE2BE File Offset: 0x000FC4BE
		public virtual bool HandleEvent(UnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000FE2C1 File Offset: 0x000FC4C1
		public virtual bool HandleEvent(UnimplantedEvent E)
		{
			return true;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000FE2C4 File Offset: 0x000FC4C4
		public virtual bool HandleEvent(UseChargeEvent E)
		{
			return true;
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000FE2C7 File Offset: 0x000FC4C7
		public virtual bool HandleEvent(UseDramsEvent E)
		{
			return true;
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000FE2CA File Offset: 0x000FC4CA
		public virtual bool HandleEvent(UseEnergyEvent E)
		{
			return true;
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000FE2CD File Offset: 0x000FC4CD
		public virtual bool HandleEvent(UseHealingLocationEvent E)
		{
			return true;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000FE2D0 File Offset: 0x000FC4D0
		public virtual bool HandleEvent(UsingChargeEvent E)
		{
			return true;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000FE2D3 File Offset: 0x000FC4D3
		public virtual bool HandleEvent(VaporizedEvent E)
		{
			return true;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000FE2D6 File Offset: 0x000FC4D6
		public virtual bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return true;
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000FE2D9 File Offset: 0x000FC4D9
		public virtual bool HandleEvent(WasDerivedFromEvent E)
		{
			return true;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000FE2DC File Offset: 0x000FC4DC
		public virtual bool HandleEvent(WasReplicatedEvent E)
		{
			return true;
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000FE2DF File Offset: 0x000FC4DF
		public virtual bool HandleEvent(WaterRitualStartEvent E)
		{
			return true;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000FE2E2 File Offset: 0x000FC4E2
		public virtual bool HandleEvent(WorshipPerformedEvent E)
		{
			return true;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000FE2E5 File Offset: 0x000FC4E5
		public virtual bool HandleEvent(ZoneActivatedEvent E)
		{
			return true;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000FE2E8 File Offset: 0x000FC4E8
		public virtual bool HandleEvent(ZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000FE2EB File Offset: 0x000FC4EB
		public virtual bool HandleEvent(ZoneDeactivatedEvent E)
		{
			return true;
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000FE2EE File Offset: 0x000FC4EE
		public virtual bool HandleEvent(ZoneThawedEvent E)
		{
			return true;
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000FE2F1 File Offset: 0x000FC4F1
		public IEventBinder Binder
		{
			get
			{
				return IGameSystem.EventBinder.Instance;
			}
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000FE2F8 File Offset: 0x000FC4F8
		public void RegisterEvent(int EventID, int Order = 0, bool Serialize = false)
		{
			this.Game.RegisterEvent(this, EventID, Order, Serialize);
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000FE309 File Offset: 0x000FC509
		public void UnregisterEvent(int EventID)
		{
			this.Game.UnregisterEvent(this, EventID);
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000FE318 File Offset: 0x000FC518
		public XRLGame Game
		{
			get
			{
				return XRLCore.Core.Game;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000FE324 File Offset: 0x000FC524
		public GameObject Player
		{
			get
			{
				return XRLCore.Core.Game.Player._Body;
			}
		}

		/// <summary>Register to events from the <see cref="T:XRL.XRLGame" />.</summary>
		/// <param name="Game">The current game instance.</param>
		/// <param name="Registrar">An event registrar with this <see cref="T:XRL.IGameSystem" /> and <see cref="T:XRL.XRLGame" /> provisioned as defaults.</param>
		// Token: 0x0600349A RID: 13466 RVA: 0x000FE33A File Offset: 0x000FC53A
		public virtual void Register(XRLGame Game, IEventRegistrar Registrar)
		{
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000FE33C File Offset: 0x000FC53C
		public virtual void ApplyRegistrar(XRLGame Game)
		{
			this.Register(Game, EventRegistrar.Get(Game, this));
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000FE34C File Offset: 0x000FC54C
		public virtual void ApplyUnregistrar(XRLGame Game)
		{
			this.Register(Game, EventUnregistrar.Get(Game, this));
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000FE35C File Offset: 0x000FC55C
		public virtual void FireEvent(Event E)
		{
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000FE35E File Offset: 0x000FC55E
		public virtual bool HandleEvent(MinEvent E)
		{
			return true;
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000FE361 File Offset: 0x000FC561
		public virtual void BeforeSave()
		{
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000FE363 File Offset: 0x000FC563
		public virtual void AfterSave()
		{
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000FE365 File Offset: 0x000FC565
		public virtual void AfterLoad(XRLGame game)
		{
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000FE367 File Offset: 0x000FC567
		public virtual void OnAdded()
		{
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000FE369 File Offset: 0x000FC569
		public virtual void OnRemoved()
		{
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060034A4 RID: 13476 RVA: 0x000FE36B File Offset: 0x000FC56B
		public virtual bool IsValid
		{
			get
			{
				return !this.Removed;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000FE376 File Offset: 0x000FC576
		public virtual bool WantFieldReflection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000FE379 File Offset: 0x000FC579
		public virtual void Write(SerializationWriter Writer)
		{
			this.SaveGame(Writer);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000FE382 File Offset: 0x000FC582
		public virtual void Read(SerializationReader Reader)
		{
			this.LoadGame(Reader);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000FE38B File Offset: 0x000FC58B
		public void AddPlayerMessage(string Message, string Color = null, bool Capitalize = true)
		{
			MessageQueue.AddPlayerMessage(Message, Color, Capitalize);
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000FE395 File Offset: 0x000FC595
		public void AddPlayerMessage(string Message, char Color, bool Capitalize = true)
		{
			MessageQueue.AddPlayerMessage(Message, Color, Capitalize);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000FE39F File Offset: 0x000FC59F
		[Obsolete("Use 'The.Game.RegisterEvent(this, GetAmbientLightEvent.ID)'.")]
		public virtual int GetDaylightRadius(int radius)
		{
			return radius;
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000FE3A2 File Offset: 0x000FC5A2
		[Obsolete("Use 'The.Game.RegisterEvent(this, EmbarkEvent.ID)', EventID == \"BootGame\".")]
		public virtual void PlayerEmbarking()
		{
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000FE3A4 File Offset: 0x000FC5A4
		[Obsolete("Use 'The.Game.RegisterEvent(this, EndSegmentEvent.ID)'.")]
		public virtual void BeginSegment()
		{
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000FE3A6 File Offset: 0x000FC5A6
		[Obsolete("Use 'The.Game.RegisterEvent(this, EndTurnEvent.ID)'.")]
		public virtual void EndTurn()
		{
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000FE3A8 File Offset: 0x000FC5A8
		[Obsolete("Use 'The.Game.RegisterEvent(this, QuestStartedEvent.ID)'.")]
		public virtual void QuestStarted(Quest Quest)
		{
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000FE3AA File Offset: 0x000FC5AA
		[Obsolete("Use 'The.Game.RegisterEvent(this, QuestFinishedEvent.ID)'.")]
		public virtual void QuestCompleted(Quest Quest)
		{
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000FE3AC File Offset: 0x000FC5AC
		[Obsolete("Use 'The.Game.RegisterEvent(this, SecretVisibilityChangedEvent.ID)'.")]
		public virtual void LocationDiscovered(string locationName)
		{
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000FE3AE File Offset: 0x000FC5AE
		[Obsolete("Use 'The.Player.RegisterEvent(this, WaterRitualStartEvent.ID)'.")]
		public virtual void WaterRitualPerformed(GameObject go)
		{
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000FE3B0 File Offset: 0x000FC5B0
		[Obsolete("Use 'The.Game.RegisterEvent(this, AfterConversationEvent.ID)'.")]
		public virtual void PlayerAfterConversation(GameObject speaker, Conversation conversation)
		{
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000FE3B2 File Offset: 0x000FC5B2
		[Obsolete("Use 'The.Player.RegisterEvent(this, EnteredCellEvent.ID)'.")]
		public virtual void PlayerEnteredCell(Cell cell)
		{
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000FE3B4 File Offset: 0x000FC5B4
		[Obsolete("Use 'The.Player.RegisterEvent(this, EnteredCellEvent.ID)'.")]
		public virtual void PlayerEnteredWorldMapCell(ObjectEnteredCellEvent E)
		{
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000FE3B6 File Offset: 0x000FC5B6
		[Obsolete("Use 'The.Player.RegisterEvent(this, TookEvent.ID)'.")]
		public virtual void PlayerTook(GameObject go)
		{
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000FE3B8 File Offset: 0x000FC5B8
		[Obsolete("Use 'The.Game.RegisterEvent(this, SecretVisibilityChangedEvent.ID)'.")]
		public virtual void AfterSecretRevealed(IBaseJournalEntry secret)
		{
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000FE3BA File Offset: 0x000FC5BA
		[Obsolete("Use 'The.Player.RegisterEvent(this, EquippedEvent.ID)'.")]
		public virtual void PlayerEquipped(GameObject go, BodyPart part)
		{
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000FE3BC File Offset: 0x000FC5BC
		[Obsolete("Use 'The.Player.RegisterEvent(this, InventoryActionEvent.ID)'.")]
		public virtual bool PlayerActivating(GameObject go)
		{
			return true;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000FE3BF File Offset: 0x000FC5BF
		[Obsolete("Use 'The.Player.RegisterEvent(this, AddedToInventoryEvent.ID)'.")]
		public virtual void PlayerObjectAddedToInventory(GameObject go)
		{
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000FE3C1 File Offset: 0x000FC5C1
		[Obsolete("Use 'The.Game.RegisterEvent(this, AfterReputationChangeEvent.ID)'.")]
		public virtual void PlayerReputationChanged(string faction, int oldValue, int newValue, string type, string because)
		{
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000FE3C3 File Offset: 0x000FC5C3
		[Obsolete("Use 'The.Game.RegisterEvent(this, ZoneBuiltEvent.ID)'.")]
		public virtual void NewZoneGenerated(Zone zone)
		{
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000FE3C5 File Offset: 0x000FC5C5
		[Obsolete("Use 'The.Game.RegisterEvent(this, ZoneActivatedEvent.ID)'.")]
		public virtual void ZoneActivated(Zone zone)
		{
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000FE3C7 File Offset: 0x000FC5C7
		[Obsolete("Use 'The.Player.RegisterEvent(this, AwardXPEvent.ID)'.")]
		public virtual bool AwardingXP(ref GameObject Actor, ref int Amount, ref int Tier, ref int Minimum, ref int Maximum, ref GameObject Kill, ref GameObject InfluencedBy, ref GameObject PassedUpFrom, ref GameObject PassedDownFrom, ref string Deed)
		{
			return true;
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000FE3CA File Offset: 0x000FC5CA
		[Obsolete("Use 'RegisterEvent(MinEvent.ID, Order: EventOrder.VERY_EARLY)'.")]
		public virtual int GetPriority()
		{
			return 0;
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000FE3CD File Offset: 0x000FC5CD
		[Obsolete("Override IGameSystem.Write().")]
		public virtual void SaveGame(SerializationWriter Writer)
		{
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000FE3CF File Offset: 0x000FC5CF
		[Obsolete("Override IGameSystem.Read().")]
		public virtual void LoadGame(SerializationReader Reader)
		{
		}

		// Token: 0x04001AB6 RID: 6838
		[NonSerialized]
		public bool Removed;

		// Token: 0x02001917 RID: 6423
		public class EventBinder : IEventBinder
		{
			// Token: 0x0600E249 RID: 57929 RVA: 0x00521D99 File Offset: 0x0051FF99
			public override void WriteBind(SerializationWriter Writer, IEventHandler Handler, int ID)
			{
				Writer.WriteTokenized(Handler.GetType());
			}

			// Token: 0x0600E24A RID: 57930 RVA: 0x00521DA7 File Offset: 0x0051FFA7
			public override IEventHandler ReadBind(SerializationReader Reader, int ID)
			{
				return The.Game.GetSystem(Reader.ReadTokenizedType());
			}

			// Token: 0x040064AB RID: 25771
			public static readonly IGameSystem.EventBinder Instance = new IGameSystem.EventBinder();
		}
	}
}
