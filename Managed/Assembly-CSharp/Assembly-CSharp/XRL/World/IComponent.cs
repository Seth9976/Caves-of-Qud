using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ConsoleLib.Console;
using Genkit;
using Newtonsoft.Json;
using UnityEngine;
using XRL.Core;
using XRL.Messages;
using XRL.Rules;
using XRL.Serialization;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200063E RID: 1598
	[Serializable]
	public abstract class IComponent<T> : IEventHandler
	{
		// Token: 0x060042A7 RID: 17063 RVA: 0x0016158C File Offset: 0x0015F78C
		public virtual bool HandleEvent(AIAfterMissileEvent E)
		{
			return true;
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x0016158F File Offset: 0x0015F78F
		public virtual bool HandleEvent(AIAfterThrowEvent E)
		{
			return true;
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x00161592 File Offset: 0x0015F792
		public virtual bool HandleEvent(AIBoredEvent E)
		{
			return true;
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x00161595 File Offset: 0x0015F795
		public virtual bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x00161598 File Offset: 0x0015F798
		public virtual bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x0016159B File Offset: 0x0015F79B
		public virtual bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x0016159E File Offset: 0x0015F79E
		public virtual bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x001615A1 File Offset: 0x0015F7A1
		public virtual bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x001615A4 File Offset: 0x0015F7A4
		public virtual bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x001615A7 File Offset: 0x0015F7A7
		public virtual bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			return true;
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x001615AA File Offset: 0x0015F7AA
		public virtual bool HandleEvent(AIGetRetreatAbilityListEvent E)
		{
			return true;
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x001615AD File Offset: 0x0015F7AD
		public virtual bool HandleEvent(AIHelpBroadcastEvent E)
		{
			return true;
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x001615B0 File Offset: 0x0015F7B0
		public virtual bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return true;
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x001615B3 File Offset: 0x0015F7B3
		public virtual bool HandleEvent(ActorGetNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x001615B6 File Offset: 0x0015F7B6
		public virtual bool HandleEvent(AddedToInventoryEvent E)
		{
			return true;
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x001615B9 File Offset: 0x0015F7B9
		public virtual bool HandleEvent(AdjustTotalWeightEvent E)
		{
			return true;
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x001615BC File Offset: 0x0015F7BC
		public virtual bool HandleEvent(AdjustValueEvent E)
		{
			return true;
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x001615BF File Offset: 0x0015F7BF
		public virtual bool HandleEvent(AdjustVisibilityRadiusEvent E)
		{
			return true;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x001615C2 File Offset: 0x0015F7C2
		public virtual bool HandleEvent(AdjustWeightEvent E)
		{
			return true;
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x001615C5 File Offset: 0x0015F7C5
		public virtual bool HandleEvent(AfterAddActiveObjectEvent E)
		{
			return true;
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x001615C8 File Offset: 0x0015F7C8
		public virtual bool HandleEvent(AfterAddOpinionEvent E)
		{
			return true;
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x001615CB File Offset: 0x0015F7CB
		public virtual bool HandleEvent(AfterAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x001615CE File Offset: 0x0015F7CE
		public virtual bool HandleEvent(AfterAfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x001615D1 File Offset: 0x0015F7D1
		public virtual bool HandleEvent(AfterBasicBestowalEvent E)
		{
			return true;
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x001615D4 File Offset: 0x0015F7D4
		public virtual bool HandleEvent(AfterChangePartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x001615D7 File Offset: 0x0015F7D7
		public virtual bool HandleEvent(AfterConsumeEvent E)
		{
			return true;
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x001615DA File Offset: 0x0015F7DA
		public virtual bool HandleEvent(AfterContentsTakenEvent E)
		{
			return true;
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x001615DD File Offset: 0x0015F7DD
		public virtual bool HandleEvent(AfterConversationEvent E)
		{
			return true;
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x001615E0 File Offset: 0x0015F7E0
		public virtual bool HandleEvent(AfterDieEvent E)
		{
			return true;
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x001615E3 File Offset: 0x0015F7E3
		public virtual bool HandleEvent(AfterDismemberEvent E)
		{
			return true;
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x001615E6 File Offset: 0x0015F7E6
		public virtual bool HandleEvent(AfterElementBestowalEvent E)
		{
			return true;
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x001615E9 File Offset: 0x0015F7E9
		public virtual bool HandleEvent(AfterExamineCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x001615EC File Offset: 0x0015F7EC
		public virtual bool HandleEvent(AfterExamineFailureEvent E)
		{
			return true;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x001615EF File Offset: 0x0015F7EF
		public virtual bool HandleEvent(AfterGameLoadedEvent E)
		{
			return true;
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x001615F2 File Offset: 0x0015F7F2
		public virtual bool HandleEvent(AfterInventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x001615F5 File Offset: 0x0015F7F5
		public virtual bool HandleEvent(AfterLevelGainedEvent E)
		{
			return true;
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x001615F8 File Offset: 0x0015F7F8
		public virtual bool HandleEvent(AfterMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x001615FB File Offset: 0x0015F7FB
		public virtual bool HandleEvent(AfterMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x001615FE File Offset: 0x0015F7FE
		public virtual bool HandleEvent(AfterMoveFailedEvent E)
		{
			return true;
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x00161601 File Offset: 0x0015F801
		public virtual bool HandleEvent(AfterObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x00161604 File Offset: 0x0015F804
		public virtual bool HandleEvent(AfterPetEvent E)
		{
			return true;
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x00161607 File Offset: 0x0015F807
		public virtual bool HandleEvent(AfterPilotChangeEvent E)
		{
			return true;
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x0016160A File Offset: 0x0015F80A
		public virtual bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			return true;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0016160D File Offset: 0x0015F80D
		public virtual bool HandleEvent(AfterPseudoRelicGeneratedEvent E)
		{
			return true;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x00161610 File Offset: 0x0015F810
		public virtual bool HandleEvent(AfterReadBookEvent E)
		{
			return true;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x00161613 File Offset: 0x0015F813
		public virtual bool HandleEvent(AfterRelicGeneratedEvent E)
		{
			return true;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x00161616 File Offset: 0x0015F816
		public virtual bool HandleEvent(AfterRemoveActiveObjectEvent E)
		{
			return true;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x00161619 File Offset: 0x0015F819
		public virtual bool HandleEvent(AfterRemoveSkillEvent E)
		{
			return true;
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0016161C File Offset: 0x0015F81C
		public virtual bool HandleEvent(AfterReputationChangeEvent E)
		{
			return true;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0016161F File Offset: 0x0015F81F
		public virtual bool HandleEvent(AfterShieldBlockEvent E)
		{
			return true;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x00161622 File Offset: 0x0015F822
		public virtual bool HandleEvent(AfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x00161625 File Offset: 0x0015F825
		public virtual bool HandleEvent(AfterTravelEvent E)
		{
			return true;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x00161628 File Offset: 0x0015F828
		public virtual bool HandleEvent(AfterZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x0016162B File Offset: 0x0015F82B
		public virtual bool HandleEvent(AllowInventoryStackEvent E)
		{
			return true;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x0016162E File Offset: 0x0015F82E
		public virtual bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return true;
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x00161631 File Offset: 0x0015F831
		public virtual bool HandleEvent(AllowPolypPluckingEvent E)
		{
			return true;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x00161634 File Offset: 0x0015F834
		public virtual bool HandleEvent(AllowTradeWithNoInventoryEvent E)
		{
			return true;
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x00161637 File Offset: 0x0015F837
		public virtual bool HandleEvent(AnimateEvent E)
		{
			return true;
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x0016163A File Offset: 0x0015F83A
		public virtual bool HandleEvent(AnyAutoCollectDramsEvent E)
		{
			return true;
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x0016163D File Offset: 0x0015F83D
		public virtual bool HandleEvent(AnyRegenerableLimbsEvent E)
		{
			return true;
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x00161640 File Offset: 0x0015F840
		public virtual bool HandleEvent(ApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x00161643 File Offset: 0x0015F843
		public virtual bool HandleEvent(AttackerBeforeTemperatureChangeEvent E)
		{
			return true;
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x00161646 File Offset: 0x0015F846
		public virtual bool HandleEvent(AttackerDealingDamageEvent E)
		{
			return true;
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x00161649 File Offset: 0x0015F849
		public virtual bool HandleEvent(AttackerDealtDamageEvent E)
		{
			return true;
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x0016164C File Offset: 0x0015F84C
		public virtual bool HandleEvent(AttemptConversationEvent E)
		{
			return true;
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x0016164F File Offset: 0x0015F84F
		public virtual bool HandleEvent(AttemptToLandEvent E)
		{
			return true;
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x00161652 File Offset: 0x0015F852
		public virtual bool HandleEvent(AutoexploreObjectEvent E)
		{
			return true;
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x00161655 File Offset: 0x0015F855
		public virtual bool HandleEvent(AwardXPEvent E)
		{
			return true;
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x00161658 File Offset: 0x0015F858
		public virtual bool HandleEvent(AwardedXPEvent E)
		{
			return true;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x0016165B File Offset: 0x0015F85B
		public virtual bool HandleEvent(AwardingXPEvent E)
		{
			return true;
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0016165E File Offset: 0x0015F85E
		public virtual bool HandleEvent(BeforeAITakingActionEvent E)
		{
			return true;
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x00161661 File Offset: 0x0015F861
		public virtual bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			return true;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x00161664 File Offset: 0x0015F864
		public virtual bool HandleEvent(BeforeAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x00161667 File Offset: 0x0015F867
		public virtual bool HandleEvent(BeforeAfterThrownEvent E)
		{
			return true;
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0016166A File Offset: 0x0015F86A
		public virtual bool HandleEvent(BeforeApplyDamageEvent E)
		{
			return true;
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0016166D File Offset: 0x0015F86D
		public virtual bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x00161670 File Offset: 0x0015F870
		public virtual bool HandleEvent(BeforeConsumeEvent E)
		{
			return true;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x00161673 File Offset: 0x0015F873
		public virtual bool HandleEvent(BeforeConversationEvent E)
		{
			return true;
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x00161676 File Offset: 0x0015F876
		public virtual bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x00161679 File Offset: 0x0015F879
		public virtual bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x0016167C File Offset: 0x0015F87C
		public virtual bool HandleEvent(BeforeDetonateEvent E)
		{
			return true;
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x0016167F File Offset: 0x0015F87F
		public virtual bool HandleEvent(BeforeDieEvent E)
		{
			return true;
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x00161682 File Offset: 0x0015F882
		public virtual bool HandleEvent(BeforeDismemberEvent E)
		{
			return true;
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x00161685 File Offset: 0x0015F885
		public virtual bool HandleEvent(BeforeFireMissileWeaponsEvent E)
		{
			return true;
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x00161688 File Offset: 0x0015F888
		public virtual bool HandleEvent(BeforeInteriorCollapseEvent E)
		{
			return true;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x0016168B File Offset: 0x0015F88B
		public virtual bool HandleEvent(BeforeLevelGainedEvent E)
		{
			return true;
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x0016168E File Offset: 0x0015F88E
		public virtual bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x00161691 File Offset: 0x0015F891
		public virtual bool HandleEvent(BeforeMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x00161694 File Offset: 0x0015F894
		public virtual bool HandleEvent(BeforeMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x00161697 File Offset: 0x0015F897
		public virtual bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x0016169A File Offset: 0x0015F89A
		public virtual bool HandleEvent(BeforePilotChangeEvent E)
		{
			return true;
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x0016169D File Offset: 0x0015F89D
		public virtual bool HandleEvent(BeforePlayMusicEvent E)
		{
			return true;
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x001616A0 File Offset: 0x0015F8A0
		public virtual bool HandleEvent(BeforeProjectileHitEvent E)
		{
			return true;
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x001616A3 File Offset: 0x0015F8A3
		public virtual bool HandleEvent(BeforePullDownEvent E)
		{
			return true;
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x001616A6 File Offset: 0x0015F8A6
		public virtual bool HandleEvent(BeforeRemoveSkillEvent E)
		{
			return true;
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x001616A9 File Offset: 0x0015F8A9
		public virtual bool HandleEvent(BeforeRenderEvent E)
		{
			return true;
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x001616AC File Offset: 0x0015F8AC
		public virtual bool HandleEvent(BeforeSetFeelingEvent E)
		{
			return true;
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x001616AF File Offset: 0x0015F8AF
		public virtual bool HandleEvent(BeforeShieldBlockEvent E)
		{
			return true;
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x001616B2 File Offset: 0x0015F8B2
		public virtual bool HandleEvent(BeforeSlamEvent E)
		{
			return true;
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x001616B5 File Offset: 0x0015F8B5
		public virtual bool HandleEvent(BeforeTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x001616B8 File Offset: 0x0015F8B8
		public virtual bool HandleEvent(BeforeTemperatureChangeEvent E)
		{
			return true;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x001616BB File Offset: 0x0015F8BB
		public virtual bool HandleEvent(BeforeTookDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x001616BE File Offset: 0x0015F8BE
		public virtual bool HandleEvent(BeforeTravelDownEvent E)
		{
			return true;
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x001616C1 File Offset: 0x0015F8C1
		public virtual bool HandleEvent(BeforeUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x001616C4 File Offset: 0x0015F8C4
		public virtual bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x001616C7 File Offset: 0x0015F8C7
		public virtual bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x001616CA File Offset: 0x0015F8CA
		public virtual bool HandleEvent(BeginConversationEvent E)
		{
			return true;
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x001616CD File Offset: 0x0015F8CD
		public virtual bool HandleEvent(BeginMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x001616D0 File Offset: 0x0015F8D0
		public virtual bool HandleEvent(BeginMentalDefendEvent E)
		{
			return true;
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x001616D3 File Offset: 0x0015F8D3
		public virtual bool HandleEvent(BeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x001616D6 File Offset: 0x0015F8D6
		public virtual bool HandleEvent(BeingConsumedEvent E)
		{
			return true;
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x001616D9 File Offset: 0x0015F8D9
		public virtual bool HandleEvent(BlasphemyPerformedEvent E)
		{
			return true;
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x001616DC File Offset: 0x0015F8DC
		public virtual bool HandleEvent(BlocksRadarEvent E)
		{
			return true;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x001616DF File Offset: 0x0015F8DF
		public virtual bool HandleEvent(BodyPositionChangedEvent E)
		{
			return true;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x001616E2 File Offset: 0x0015F8E2
		public virtual bool HandleEvent(BootSequenceAbortedEvent E)
		{
			return true;
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x001616E5 File Offset: 0x0015F8E5
		public virtual bool HandleEvent(BootSequenceDoneEvent E)
		{
			return true;
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x001616E8 File Offset: 0x0015F8E8
		public virtual bool HandleEvent(BootSequenceInitializedEvent E)
		{
			return true;
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x001616EB File Offset: 0x0015F8EB
		public virtual bool HandleEvent(CanAcceptObjectEvent E)
		{
			return true;
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x001616EE File Offset: 0x0015F8EE
		public virtual bool HandleEvent(CanApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x001616F1 File Offset: 0x0015F8F1
		public virtual bool HandleEvent(CanBeDismemberedEvent E)
		{
			return true;
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x001616F4 File Offset: 0x0015F8F4
		public virtual bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return true;
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x001616F7 File Offset: 0x0015F8F7
		public virtual bool HandleEvent(CanBeMagneticallyManipulatedEvent E)
		{
			return true;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x001616FA File Offset: 0x0015F8FA
		public virtual bool HandleEvent(CanBeModdedEvent E)
		{
			return true;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x001616FD File Offset: 0x0015F8FD
		public virtual bool HandleEvent(CanBeNamedEvent E)
		{
			return true;
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x00161700 File Offset: 0x0015F900
		public virtual bool HandleEvent(CanBeReplicatedEvent E)
		{
			return true;
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x00161703 File Offset: 0x0015F903
		public virtual bool HandleEvent(CanBeSlottedEvent E)
		{
			return true;
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x00161706 File Offset: 0x0015F906
		public virtual bool HandleEvent(CanBeTradedEvent E)
		{
			return true;
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x00161709 File Offset: 0x0015F909
		public virtual bool HandleEvent(CanBeUnequippedEvent E)
		{
			return true;
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x0016170C File Offset: 0x0015F90C
		public virtual bool HandleEvent(CanChangeMovementModeEvent E)
		{
			return true;
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x0016170F File Offset: 0x0015F90F
		public virtual bool HandleEvent(CanDrinkEvent E)
		{
			return true;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x00161712 File Offset: 0x0015F912
		public virtual bool HandleEvent(CanEnterInteriorEvent E)
		{
			return true;
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x00161715 File Offset: 0x0015F915
		public virtual bool HandleEvent(CanFallEvent E)
		{
			return true;
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x00161718 File Offset: 0x0015F918
		public virtual bool HandleEvent(CanFireAllMissileWeaponsEvent E)
		{
			return true;
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x0016171B File Offset: 0x0015F91B
		public virtual bool HandleEvent(CanGiveDirectionsEvent E)
		{
			return true;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0016171E File Offset: 0x0015F91E
		public virtual bool HandleEvent(CanHaveConversationEvent E)
		{
			return true;
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x00161721 File Offset: 0x0015F921
		public virtual bool HandleEvent(CanJoinPartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x00161724 File Offset: 0x0015F924
		public virtual bool HandleEvent(CanMissilePassForcefieldEvent E)
		{
			return true;
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x00161727 File Offset: 0x0015F927
		public virtual bool HandleEvent(CanReceiveEmpathyEvent E)
		{
			return true;
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x0016172A File Offset: 0x0015F92A
		public virtual bool HandleEvent(CanReceiveTelepathyEvent E)
		{
			return true;
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x0016172D File Offset: 0x0015F92D
		public virtual bool HandleEvent(CanRefreshAbilityEvent E)
		{
			return true;
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x00161730 File Offset: 0x0015F930
		public virtual bool HandleEvent(CanSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x00161733 File Offset: 0x0015F933
		public virtual bool HandleEvent(CanStartConversationEvent E)
		{
			return true;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x00161736 File Offset: 0x0015F936
		public virtual bool HandleEvent(CanTemperatureReturnToAmbientEvent E)
		{
			return true;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x00161739 File Offset: 0x0015F939
		public virtual bool HandleEvent(CanTradeEvent E)
		{
			return true;
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x0016173C File Offset: 0x0015F93C
		public virtual bool HandleEvent(CanTravelEvent E)
		{
			return true;
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x0016173F File Offset: 0x0015F93F
		public virtual bool HandleEvent(CarryingCapacityChangedEvent E)
		{
			return true;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x00161742 File Offset: 0x0015F942
		public virtual bool HandleEvent(CellChangedEvent E)
		{
			return true;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x00161745 File Offset: 0x0015F945
		public virtual bool HandleEvent(CellDepletedEvent E)
		{
			return true;
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x00161748 File Offset: 0x0015F948
		public virtual bool HandleEvent(ChargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x0016174B File Offset: 0x0015F94B
		public virtual bool HandleEvent(ChargeUsedEvent E)
		{
			return true;
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x0016174E File Offset: 0x0015F94E
		public virtual bool HandleEvent(CheckAnythingToCleanEvent E)
		{
			return true;
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x00161751 File Offset: 0x0015F951
		public virtual bool HandleEvent(CheckAnythingToCleanWithEvent E)
		{
			return true;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x00161754 File Offset: 0x0015F954
		public virtual bool HandleEvent(CheckAnythingToCleanWithNearbyEvent E)
		{
			return true;
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x00161757 File Offset: 0x0015F957
		public virtual bool HandleEvent(CheckAttackableEvent E)
		{
			return true;
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x0016175A File Offset: 0x0015F95A
		public virtual bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return true;
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x0016175D File Offset: 0x0015F95D
		public virtual bool HandleEvent(CheckGasCanAffectEvent E)
		{
			return true;
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x00161760 File Offset: 0x0015F960
		public virtual bool HandleEvent(CheckLoadAmmoEvent E)
		{
			return true;
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x00161763 File Offset: 0x0015F963
		public virtual bool HandleEvent(CheckOverburdenedOnStrengthUpdateEvent E)
		{
			return true;
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x00161766 File Offset: 0x0015F966
		public virtual bool HandleEvent(CheckPaintabilityEvent E)
		{
			return true;
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x00161769 File Offset: 0x0015F969
		public virtual bool HandleEvent(CheckReadyToFireEvent E)
		{
			return true;
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x0016176C File Offset: 0x0015F96C
		public virtual bool HandleEvent(CheckRealityDistortionAccessibilityEvent E)
		{
			return true;
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x0016176F File Offset: 0x0015F96F
		public virtual bool HandleEvent(CheckRealityDistortionAdvisabilityEvent E)
		{
			return true;
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x00161772 File Offset: 0x0015F972
		public virtual bool HandleEvent(CheckRealityDistortionUsabilityEvent E)
		{
			return true;
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x00161775 File Offset: 0x0015F975
		public virtual bool HandleEvent(CheckSpawnMergeEvent E)
		{
			return true;
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x00161778 File Offset: 0x0015F978
		public virtual bool HandleEvent(CheckTileChangeEvent E)
		{
			return true;
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x0016177B File Offset: 0x0015F97B
		public virtual bool HandleEvent(CheckUsesChargeWhileEquippedEvent E)
		{
			return true;
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x0016177E File Offset: 0x0015F97E
		public virtual bool HandleEvent(CleanItemsEvent E)
		{
			return true;
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x00161781 File Offset: 0x0015F981
		public virtual bool HandleEvent(CollectBroadcastChargeEvent E)
		{
			return true;
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x00161784 File Offset: 0x0015F984
		public virtual bool HandleEvent(CommandEvent E)
		{
			return true;
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x00161787 File Offset: 0x0015F987
		public virtual bool HandleEvent(CommandReloadEvent E)
		{
			return true;
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x0016178A File Offset: 0x0015F98A
		public virtual bool HandleEvent(CommandReplaceCellEvent E)
		{
			return true;
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x0016178D File Offset: 0x0015F98D
		public virtual bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			return true;
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x00161790 File Offset: 0x0015F990
		public virtual bool HandleEvent(CommandSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x00161793 File Offset: 0x0015F993
		public virtual bool HandleEvent(CommandTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x00161796 File Offset: 0x0015F996
		public virtual bool HandleEvent(ConfigureMissileVisualEffectEvent E)
		{
			return true;
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x00161799 File Offset: 0x0015F999
		public virtual bool HandleEvent(ContainsAnyBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x0016179C File Offset: 0x0015F99C
		public virtual bool HandleEvent(ContainsBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x0016179F File Offset: 0x0015F99F
		public virtual bool HandleEvent(ContainsEvent E)
		{
			return true;
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x001617A2 File Offset: 0x0015F9A2
		public virtual bool HandleEvent(DamageConstantAdjustedEvent E)
		{
			return true;
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x001617A5 File Offset: 0x0015F9A5
		public virtual bool HandleEvent(DamageDieSizeAdjustedEvent E)
		{
			return true;
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x001617A8 File Offset: 0x0015F9A8
		public virtual bool HandleEvent(DeathEvent E)
		{
			return true;
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x001617AB File Offset: 0x0015F9AB
		public virtual bool HandleEvent(DecorateDefaultEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x001617AE File Offset: 0x0015F9AE
		public virtual bool HandleEvent(DefendMeleeHitEvent E)
		{
			return true;
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x001617B1 File Offset: 0x0015F9B1
		public virtual bool HandleEvent(DefenderMissileHitEvent E)
		{
			return true;
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x001617B4 File Offset: 0x0015F9B4
		public virtual bool HandleEvent(DerivationCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x001617B7 File Offset: 0x0015F9B7
		public virtual bool HandleEvent(DidInitialEquipEvent E)
		{
			return true;
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x001617BA File Offset: 0x0015F9BA
		public virtual bool HandleEvent(DidReequipEvent E)
		{
			return true;
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x001617BD File Offset: 0x0015F9BD
		public virtual bool HandleEvent(DropOnDeathEvent E)
		{
			return true;
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x001617C0 File Offset: 0x0015F9C0
		public virtual bool HandleEvent(DroppedEvent E)
		{
			return true;
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x001617C3 File Offset: 0x0015F9C3
		public virtual bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			return true;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x001617C6 File Offset: 0x0015F9C6
		public virtual bool HandleEvent(EarlyBeforeDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x001617C9 File Offset: 0x0015F9C9
		public virtual bool HandleEvent(EffectAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x001617CC File Offset: 0x0015F9CC
		public virtual bool HandleEvent(EffectForceAppliedEvent E)
		{
			return true;
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x001617CF File Offset: 0x0015F9CF
		public virtual bool HandleEvent(EffectRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x001617D2 File Offset: 0x0015F9D2
		public virtual bool HandleEvent(EmbarkEvent E)
		{
			return true;
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x001617D5 File Offset: 0x0015F9D5
		public virtual bool HandleEvent(EncounterChanceEvent E)
		{
			return true;
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x001617D8 File Offset: 0x0015F9D8
		public virtual bool HandleEvent(EncumbranceChangedEvent E)
		{
			return true;
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x001617DB File Offset: 0x0015F9DB
		public virtual bool HandleEvent(EndActionEvent E)
		{
			return true;
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x001617DE File Offset: 0x0015F9DE
		public virtual bool HandleEvent(EndTurnEvent E)
		{
			return true;
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x001617E1 File Offset: 0x0015F9E1
		public virtual bool HandleEvent(EnterCellEvent E)
		{
			return true;
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x001617E4 File Offset: 0x0015F9E4
		public virtual bool HandleEvent(EnteredCellEvent E)
		{
			return true;
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x001617E7 File Offset: 0x0015F9E7
		public virtual bool HandleEvent(EnteringCellEvent E)
		{
			return true;
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x001617EA File Offset: 0x0015F9EA
		public virtual bool HandleEvent(EnteringZoneEvent E)
		{
			return true;
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x001617ED File Offset: 0x0015F9ED
		public virtual bool HandleEvent(EnvironmentalUpdateEvent E)
		{
			return true;
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x001617F0 File Offset: 0x0015F9F0
		public virtual bool HandleEvent(EquippedEvent E)
		{
			return true;
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x001617F3 File Offset: 0x0015F9F3
		public virtual bool HandleEvent(EquipperEquippedEvent E)
		{
			return true;
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x001617F6 File Offset: 0x0015F9F6
		public virtual bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x001617F9 File Offset: 0x0015F9F9
		public virtual bool HandleEvent(ExamineFailureEvent E)
		{
			return true;
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x001617FC File Offset: 0x0015F9FC
		public virtual bool HandleEvent(ExamineSuccessEvent E)
		{
			return true;
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x001617FF File Offset: 0x0015F9FF
		public virtual bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			return true;
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x00161802 File Offset: 0x0015FA02
		public virtual bool HandleEvent(ExtremitiesMovedEvent E)
		{
			return true;
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x00161805 File Offset: 0x0015FA05
		public virtual bool HandleEvent(FellDownEvent E)
		{
			return true;
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x00161808 File Offset: 0x0015FA08
		public virtual bool HandleEvent(FindObjectByIdEvent E)
		{
			return true;
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x0016180B File Offset: 0x0015FA0B
		public virtual bool HandleEvent(FinishChargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x0016180E File Offset: 0x0015FA0E
		public virtual bool HandleEvent(FinishRechargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x00161811 File Offset: 0x0015FA11
		public virtual bool HandleEvent(FlushWeightCacheEvent E)
		{
			return true;
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x00161814 File Offset: 0x0015FA14
		public virtual bool HandleEvent(ForceApplyEffectEvent E)
		{
			return true;
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x00161817 File Offset: 0x0015FA17
		public virtual bool HandleEvent(FrozeEvent E)
		{
			return true;
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x0016181A File Offset: 0x0015FA1A
		public virtual bool HandleEvent(GaveDirectionsEvent E)
		{
			return true;
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x0016181D File Offset: 0x0015FA1D
		public virtual bool HandleEvent(GeneralAmnestyEvent E)
		{
			return true;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x00161820 File Offset: 0x0015FA20
		public virtual bool HandleEvent(GenericCommandEvent E)
		{
			return true;
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00161823 File Offset: 0x0015FA23
		public virtual bool HandleEvent(GenericDeepNotifyEvent E)
		{
			return true;
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x00161826 File Offset: 0x0015FA26
		public virtual bool HandleEvent(GenericDeepQueryEvent E)
		{
			return true;
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x00161829 File Offset: 0x0015FA29
		public virtual bool HandleEvent(GenericDeepRatingEvent E)
		{
			return true;
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x0016182C File Offset: 0x0015FA2C
		public virtual bool HandleEvent(GenericNotifyEvent E)
		{
			return true;
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x0016182F File Offset: 0x0015FA2F
		public virtual bool HandleEvent(GenericQueryEvent E)
		{
			return true;
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x00161832 File Offset: 0x0015FA32
		public virtual bool HandleEvent(GenericRatingEvent E)
		{
			return true;
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x00161835 File Offset: 0x0015FA35
		public virtual bool HandleEvent(GetActivationPhaseEvent E)
		{
			return true;
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x00161838 File Offset: 0x0015FA38
		public virtual bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x0016183B File Offset: 0x0015FA3B
		public virtual bool HandleEvent(GetAmbientLightEvent E)
		{
			return true;
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x0016183E File Offset: 0x0015FA3E
		public virtual bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			return true;
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x00161841 File Offset: 0x0015FA41
		public virtual bool HandleEvent(GetApparentSpeciesEvent E)
		{
			return true;
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x00161844 File Offset: 0x0015FA44
		public virtual bool HandleEvent(GetAttackerHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x00161847 File Offset: 0x0015FA47
		public virtual bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x0016184A File Offset: 0x0015FA4A
		public virtual bool HandleEvent(GetAutoCollectDramsEvent E)
		{
			return true;
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x0016184D File Offset: 0x0015FA4D
		public virtual bool HandleEvent(GetAutoEquipPriorityEvent E)
		{
			return true;
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x00161850 File Offset: 0x0015FA50
		public virtual bool HandleEvent(GetAvailableComputePowerEvent E)
		{
			return true;
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x00161853 File Offset: 0x0015FA53
		public virtual bool HandleEvent(GetBandagePerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x00161856 File Offset: 0x0015FA56
		public virtual bool HandleEvent(GetBleedLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x00161859 File Offset: 0x0015FA59
		public virtual bool HandleEvent(GetCarriedWeightEvent E)
		{
			return true;
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x0016185C File Offset: 0x0015FA5C
		public virtual bool HandleEvent(GetCleaningItemsEvent E)
		{
			return true;
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x0016185F File Offset: 0x0015FA5F
		public virtual bool HandleEvent(GetCleaningItemsNearbyEvent E)
		{
			return true;
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x00161862 File Offset: 0x0015FA62
		public virtual bool HandleEvent(GetCleaveAmountEvent E)
		{
			return true;
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x00161865 File Offset: 0x0015FA65
		public virtual bool HandleEvent(GetCleaveMaxPenaltyEvent E)
		{
			return true;
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x00161868 File Offset: 0x0015FA68
		public virtual bool HandleEvent(GetCompanionLimitEvent E)
		{
			return true;
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x0016186B File Offset: 0x0015FA6B
		public virtual bool HandleEvent(GetCompanionStatusEvent E)
		{
			return true;
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x0016186E File Offset: 0x0015FA6E
		public virtual bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x00161871 File Offset: 0x0015FA71
		public virtual bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x00161874 File Offset: 0x0015FA74
		public virtual bool HandleEvent(GetContentsEvent E)
		{
			return true;
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x00161877 File Offset: 0x0015FA77
		public virtual bool HandleEvent(GetContextEvent E)
		{
			return true;
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x0016187A File Offset: 0x0015FA7A
		public virtual bool HandleEvent(GetCookingActionsEvent E)
		{
			return true;
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x0016187D File Offset: 0x0015FA7D
		public virtual bool HandleEvent(GetCooldownEvent E)
		{
			return true;
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x00161880 File Offset: 0x0015FA80
		public virtual bool HandleEvent(GetCriticalThresholdEvent E)
		{
			return true;
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x00161883 File Offset: 0x0015FA83
		public virtual bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x00161886 File Offset: 0x0015FA86
		public virtual bool HandleEvent(GetDebugInternalsEvent E)
		{
			return true;
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x00161889 File Offset: 0x0015FA89
		public virtual bool HandleEvent(GetDefenderHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x0016188C File Offset: 0x0015FA8C
		public virtual bool HandleEvent(GetDefenderMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x0016188F File Offset: 0x0015FA8F
		public virtual bool HandleEvent(GetDifficultyEvaluationEvent E)
		{
			return true;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x00161892 File Offset: 0x0015FA92
		public virtual bool HandleEvent(GetDiseaseOnsetEvent E)
		{
			return true;
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x00161895 File Offset: 0x0015FA95
		public virtual bool HandleEvent(GetDisplayNameEvent E)
		{
			return true;
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x00161898 File Offset: 0x0015FA98
		public virtual bool HandleEvent(GetDisplayNamePenetrationColorEvent E)
		{
			return true;
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x0016189B File Offset: 0x0015FA9B
		public virtual bool HandleEvent(GetDisplayStatBonusEvent E)
		{
			return true;
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x0016189E File Offset: 0x0015FA9E
		public virtual bool HandleEvent(GetElectricalConductivityEvent E)
		{
			return true;
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x001618A1 File Offset: 0x0015FAA1
		public virtual bool HandleEvent(GetEnergyCostEvent E)
		{
			return true;
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x001618A4 File Offset: 0x0015FAA4
		public virtual bool HandleEvent(GetExtraPhysicalFeaturesEvent E)
		{
			return true;
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x001618A7 File Offset: 0x0015FAA7
		public virtual bool HandleEvent(GetExtrinsicValueEvent E)
		{
			return true;
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x001618AA File Offset: 0x0015FAAA
		public virtual bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			return true;
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x001618AD File Offset: 0x0015FAAD
		public virtual bool HandleEvent(GetFactionRankEvent E)
		{
			return true;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x001618B0 File Offset: 0x0015FAB0
		public virtual bool HandleEvent(GetFeelingEvent E)
		{
			return true;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x001618B3 File Offset: 0x0015FAB3
		public virtual bool HandleEvent(GetFirefightingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x001618B6 File Offset: 0x0015FAB6
		public virtual bool HandleEvent(GetFixedMissileSpreadEvent E)
		{
			return true;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x001618B9 File Offset: 0x0015FAB9
		public virtual bool HandleEvent(GetFreeDramsEvent E)
		{
			return true;
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x001618BC File Offset: 0x0015FABC
		public virtual bool HandleEvent(GetGameObjectSortEvent E)
		{
			return true;
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x001618BF File Offset: 0x0015FABF
		public virtual bool HandleEvent(GetGenderEvent E)
		{
			return true;
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x001618C2 File Offset: 0x0015FAC2
		public virtual bool HandleEvent(GetHostileWalkRadiusEvent E)
		{
			return true;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x001618C5 File Offset: 0x0015FAC5
		public virtual bool HandleEvent(GetHostilityRecognitionLimitsEvent E)
		{
			return true;
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x001618C8 File Offset: 0x0015FAC8
		public virtual bool HandleEvent(GetIdealTemperatureEvent E)
		{
			return true;
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x001618CB File Offset: 0x0015FACB
		public virtual bool HandleEvent(GetIntrinsicValueEvent E)
		{
			return true;
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x001618CE File Offset: 0x0015FACE
		public virtual bool HandleEvent(GetIntrinsicWeightEvent E)
		{
			return true;
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x001618D1 File Offset: 0x0015FAD1
		public virtual bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			return true;
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x001618D4 File Offset: 0x0015FAD4
		public virtual bool HandleEvent(GetInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x001618D7 File Offset: 0x0015FAD7
		public virtual bool HandleEvent(GetInventoryCategoryEvent E)
		{
			return true;
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x001618DA File Offset: 0x0015FADA
		public virtual bool HandleEvent(GetItemElementsEvent E)
		{
			return true;
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x001618DD File Offset: 0x0015FADD
		public virtual bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x001618E0 File Offset: 0x0015FAE0
		public virtual bool HandleEvent(GetKineticResistanceEvent E)
		{
			return true;
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x001618E3 File Offset: 0x0015FAE3
		public virtual bool HandleEvent(GetLevelUpDiceEvent E)
		{
			return true;
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x001618E6 File Offset: 0x0015FAE6
		public virtual bool HandleEvent(GetLevelUpPointsEvent E)
		{
			return true;
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x001618E9 File Offset: 0x0015FAE9
		public virtual bool HandleEvent(GetLevelUpSkillPointsEvent E)
		{
			return true;
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x001618EC File Offset: 0x0015FAEC
		public virtual bool HandleEvent(GetLostChanceEvent E)
		{
			return true;
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x001618EF File Offset: 0x0015FAEF
		public virtual bool HandleEvent(GetMatterPhaseEvent E)
		{
			return true;
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x001618F2 File Offset: 0x0015FAF2
		public virtual bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			return true;
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x001618F5 File Offset: 0x0015FAF5
		public virtual bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			return true;
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x001618F8 File Offset: 0x0015FAF8
		public virtual bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			return true;
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x001618FB File Offset: 0x0015FAFB
		public virtual bool HandleEvent(GetMeleeAttacksEvent E)
		{
			return true;
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x001618FE File Offset: 0x0015FAFE
		public virtual bool HandleEvent(GetMissileCoverPercentageEvent E)
		{
			return true;
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x00161901 File Offset: 0x0015FB01
		public virtual bool HandleEvent(GetMissileStatusColorEvent E)
		{
			return true;
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x00161904 File Offset: 0x0015FB04
		public virtual bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x00161907 File Offset: 0x0015FB07
		public virtual bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			return true;
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x0016190A File Offset: 0x0015FB0A
		public virtual bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			return true;
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x0016190D File Offset: 0x0015FB0D
		public virtual bool HandleEvent(GetModRarityWeightEvent E)
		{
			return true;
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x00161910 File Offset: 0x0015FB10
		public virtual bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			return true;
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x00161913 File Offset: 0x0015FB13
		public virtual bool HandleEvent(GetMutationTermEvent E)
		{
			return true;
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x00161916 File Offset: 0x0015FB16
		public virtual bool HandleEvent(GetNamingBestowalChanceEvent E)
		{
			return true;
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x00161919 File Offset: 0x0015FB19
		public virtual bool HandleEvent(GetNamingChanceEvent E)
		{
			return true;
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x0016191C File Offset: 0x0015FB1C
		public virtual bool HandleEvent(GetNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x0016191F File Offset: 0x0015FB1F
		public virtual bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			return true;
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x00161922 File Offset: 0x0015FB22
		public virtual bool HandleEvent(GetOverloadChargeEvent E)
		{
			return true;
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x00161925 File Offset: 0x0015FB25
		public virtual bool HandleEvent(GetPartyLeaderFollowDistanceEvent E)
		{
			return true;
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x00161928 File Offset: 0x0015FB28
		public virtual bool HandleEvent(GetPointsOfInterestEvent E)
		{
			return true;
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x0016192B File Offset: 0x0015FB2B
		public virtual bool HandleEvent(GetPowerLoadLevelEvent E)
		{
			return true;
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x0016192E File Offset: 0x0015FB2E
		public virtual bool HandleEvent(GetPrecognitionRestoreGameStateEvent E)
		{
			return true;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x00161931 File Offset: 0x0015FB31
		public virtual bool HandleEvent(GetPreferredLiquidEvent E)
		{
			return true;
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x00161934 File Offset: 0x0015FB34
		public virtual bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x00161937 File Offset: 0x0015FB37
		public virtual bool HandleEvent(GetProjectileObjectEvent E)
		{
			return true;
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x0016193A File Offset: 0x0015FB3A
		public virtual bool HandleEvent(GetPropertyModDescription E)
		{
			return true;
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x0016193D File Offset: 0x0015FB3D
		public virtual bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x00161940 File Offset: 0x0015FB40
		public virtual bool HandleEvent(GetPsychicGlimmerEvent E)
		{
			return true;
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x00161943 File Offset: 0x0015FB43
		public virtual bool HandleEvent(GetRandomBuyChimericBodyPartRollsEvent E)
		{
			return true;
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x00161946 File Offset: 0x0015FB46
		public virtual bool HandleEvent(GetRandomBuyMutationCountEvent E)
		{
			return true;
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x00161949 File Offset: 0x0015FB49
		public virtual bool HandleEvent(GetRealityStabilizationPenetrationEvent E)
		{
			return true;
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x0016194C File Offset: 0x0015FB4C
		public virtual bool HandleEvent(GetRebukeLevelEvent E)
		{
			return true;
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x0016194F File Offset: 0x0015FB4F
		public virtual bool HandleEvent(GetRecoilersEvent E)
		{
			return true;
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x00161952 File Offset: 0x0015FB52
		public virtual bool HandleEvent(GetReplaceCellInteractionsEvent E)
		{
			return true;
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x00161955 File Offset: 0x0015FB55
		public virtual bool HandleEvent(GetRespiratoryAgentPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x00161958 File Offset: 0x0015FB58
		public virtual bool HandleEvent(GetRitualSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x0016195B File Offset: 0x0015FB5B
		public virtual bool HandleEvent(GetRunningBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x0016195E File Offset: 0x0015FB5E
		public virtual bool HandleEvent(GetScanTypeEvent E)
		{
			return true;
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x00161961 File Offset: 0x0015FB61
		public virtual bool HandleEvent(GetShieldBlockPreferenceEvent E)
		{
			return true;
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x00161964 File Offset: 0x0015FB64
		public virtual bool HandleEvent(GetShieldSlamDamageEvent E)
		{
			return true;
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x00161967 File Offset: 0x0015FB67
		public virtual bool HandleEvent(GetShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x0016196A File Offset: 0x0015FB6A
		public virtual bool HandleEvent(GetSkillEffectChanceEvent E)
		{
			return true;
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x0016196D File Offset: 0x0015FB6D
		public virtual bool HandleEvent(GetSlotsRequiredEvent E)
		{
			return true;
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x00161970 File Offset: 0x0015FB70
		public virtual bool HandleEvent(GetSlottedInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x00161973 File Offset: 0x0015FB73
		public virtual bool HandleEvent(GetSocialSifrahSetupEvent E)
		{
			return true;
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x00161976 File Offset: 0x0015FB76
		public virtual bool HandleEvent(GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent E)
		{
			return true;
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x00161979 File Offset: 0x0015FB79
		public virtual bool HandleEvent(GetSpaceTimeAnomalyEmergencePermillageChanceEvent E)
		{
			return true;
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x0016197C File Offset: 0x0015FB7C
		public virtual bool HandleEvent(GetSpecialEffectChanceEvent E)
		{
			return true;
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x0016197F File Offset: 0x0015FB7F
		public virtual bool HandleEvent(GetSpringinessEvent E)
		{
			return true;
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x00161982 File Offset: 0x0015FB82
		public virtual bool HandleEvent(GetSprintDurationEvent E)
		{
			return true;
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x00161985 File Offset: 0x0015FB85
		public virtual bool HandleEvent(GetStorableDramsEvent E)
		{
			return true;
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x00161988 File Offset: 0x0015FB88
		public virtual bool HandleEvent(GetSwimmingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x0016198B File Offset: 0x0015FB8B
		public virtual bool HandleEvent(GetThrowProfileEvent E)
		{
			return true;
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x0016198E File Offset: 0x0015FB8E
		public virtual bool HandleEvent(GetThrownWeaponFlexPhaseProviderEvent E)
		{
			return true;
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x00161991 File Offset: 0x0015FB91
		public virtual bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x00161994 File Offset: 0x0015FB94
		public virtual bool HandleEvent(GetThrownWeaponRangeEvent E)
		{
			return true;
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x00161997 File Offset: 0x0015FB97
		public virtual bool HandleEvent(GetTinkeringBonusEvent E)
		{
			return true;
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x0016199A File Offset: 0x0015FB9A
		public virtual bool HandleEvent(GetToHitModifierEvent E)
		{
			return true;
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x0016199D File Offset: 0x0015FB9D
		public virtual bool HandleEvent(GetTonicCapacityEvent E)
		{
			return true;
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x001619A0 File Offset: 0x0015FBA0
		public virtual bool HandleEvent(GetTonicDosageEvent E)
		{
			return true;
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x001619A3 File Offset: 0x0015FBA3
		public virtual bool HandleEvent(GetTonicDurationEvent E)
		{
			return true;
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x001619A6 File Offset: 0x0015FBA6
		public virtual bool HandleEvent(GetTradePerformanceEvent E)
		{
			return true;
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x001619A9 File Offset: 0x0015FBA9
		public virtual bool HandleEvent(GetTransitiveLocationEvent E)
		{
			return true;
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x001619AC File Offset: 0x0015FBAC
		public virtual bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x001619AF File Offset: 0x0015FBAF
		public virtual bool HandleEvent(GetUtilityScoreEvent E)
		{
			return true;
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x001619B2 File Offset: 0x0015FBB2
		public virtual bool HandleEvent(GetWadingPerformanceEvent E)
		{
			return true;
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x001619B5 File Offset: 0x0015FBB5
		public virtual bool HandleEvent(GetWaterRitualCostEvent E)
		{
			return true;
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x001619B8 File Offset: 0x0015FBB8
		public virtual bool HandleEvent(GetWaterRitualLiquidEvent E)
		{
			return true;
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x001619BB File Offset: 0x0015FBBB
		public virtual bool HandleEvent(GetWaterRitualReputationAmountEvent E)
		{
			return true;
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x001619BE File Offset: 0x0015FBBE
		public virtual bool HandleEvent(GetWaterRitualSecretWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x001619C1 File Offset: 0x0015FBC1
		public virtual bool HandleEvent(GetWaterRitualSellSecretBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x001619C4 File Offset: 0x0015FBC4
		public virtual bool HandleEvent(GetWeaponHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x001619C7 File Offset: 0x0015FBC7
		public virtual bool HandleEvent(GetWeaponMeleeAttacksEvent E)
		{
			return true;
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x001619CA File Offset: 0x0015FBCA
		public virtual bool HandleEvent(GetWeaponMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x001619CD File Offset: 0x0015FBCD
		public virtual bool HandleEvent(GetZoneEvent E)
		{
			return true;
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x001619D0 File Offset: 0x0015FBD0
		public virtual bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			return true;
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x001619D3 File Offset: 0x0015FBD3
		public virtual bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			return true;
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x001619D6 File Offset: 0x0015FBD6
		public virtual bool HandleEvent(GiveDramsEvent E)
		{
			return true;
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x001619D9 File Offset: 0x0015FBD9
		public virtual bool HandleEvent(GlimmerChangeEvent E)
		{
			return true;
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x001619DC File Offset: 0x0015FBDC
		public virtual bool HandleEvent(GravitationEvent E)
		{
			return true;
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x001619DF File Offset: 0x0015FBDF
		public virtual bool HandleEvent(HasBeenReadEvent E)
		{
			return true;
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x001619E2 File Offset: 0x0015FBE2
		public virtual bool HandleEvent(HasBlueprintEvent E)
		{
			return true;
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x001619E5 File Offset: 0x0015FBE5
		public virtual bool HandleEvent(HasFlammableEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x001619E8 File Offset: 0x0015FBE8
		public virtual bool HandleEvent(HasFlammableEquipmentOrInventoryEvent E)
		{
			return true;
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x001619EB File Offset: 0x0015FBEB
		public virtual bool HandleEvent(HealsNaturallyEvent E)
		{
			return true;
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x001619EE File Offset: 0x0015FBEE
		public virtual bool HandleEvent(IAICommandListEvent E)
		{
			return true;
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x001619F1 File Offset: 0x0015FBF1
		public virtual bool HandleEvent(IAIItemCommandListEvent E)
		{
			return true;
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x001619F4 File Offset: 0x0015FBF4
		public virtual bool HandleEvent(IAIMoveCommandListEvent E)
		{
			return true;
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x001619F7 File Offset: 0x0015FBF7
		public virtual bool HandleEvent(IActOnItemEvent E)
		{
			return true;
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x001619FA File Offset: 0x0015FBFA
		public virtual bool HandleEvent(IActualEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x001619FD File Offset: 0x0015FBFD
		public virtual bool HandleEvent(IAddSkillEvent E)
		{
			return true;
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x00161A00 File Offset: 0x0015FC00
		public virtual bool HandleEvent(IAdjacentNavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x00161A03 File Offset: 0x0015FC03
		public virtual bool HandleEvent(IBootSequenceEvent E)
		{
			return true;
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x00161A06 File Offset: 0x0015FC06
		public virtual bool HandleEvent(IChargeConsumptionEvent E)
		{
			return true;
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x00161A09 File Offset: 0x0015FC09
		public virtual bool HandleEvent(IChargeEvent E)
		{
			return true;
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x00161A0C File Offset: 0x0015FC0C
		public virtual bool HandleEvent(IChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x00161A0F File Offset: 0x0015FC0F
		public virtual bool HandleEvent(IChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x00161A12 File Offset: 0x0015FC12
		public virtual bool HandleEvent(IConsumeEvent E)
		{
			return true;
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x00161A15 File Offset: 0x0015FC15
		public virtual bool HandleEvent(IConversationMinEvent E)
		{
			return true;
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x00161A18 File Offset: 0x0015FC18
		public virtual bool HandleEvent(IDamageEvent E)
		{
			return true;
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x00161A1B File Offset: 0x0015FC1B
		public virtual bool HandleEvent(IDeathEvent E)
		{
			return true;
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x00161A1E File Offset: 0x0015FC1E
		public virtual bool HandleEvent(IDerivationEvent E)
		{
			return true;
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x00161A21 File Offset: 0x0015FC21
		public virtual bool HandleEvent(IDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x00161A24 File Offset: 0x0015FC24
		public virtual bool HandleEvent(IEffectCheckEvent E)
		{
			return true;
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x00161A27 File Offset: 0x0015FC27
		public virtual bool HandleEvent(IExamineEvent E)
		{
			return true;
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x00161A2A File Offset: 0x0015FC2A
		public virtual bool HandleEvent(IFinalChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x00161A2D File Offset: 0x0015FC2D
		public virtual bool HandleEvent(IHitDiceEvent E)
		{
			return true;
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x00161A30 File Offset: 0x0015FC30
		public virtual bool HandleEvent(IInitialChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x00161A33 File Offset: 0x0015FC33
		public virtual bool HandleEvent(IInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x00161A36 File Offset: 0x0015FC36
		public virtual bool HandleEvent(ILimbRegenerationEvent E)
		{
			return true;
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x00161A39 File Offset: 0x0015FC39
		public virtual bool HandleEvent(ILiquidEvent E)
		{
			return true;
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x00161A3C File Offset: 0x0015FC3C
		public virtual bool HandleEvent(IMeleeAttackEvent E)
		{
			return true;
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x00161A3F File Offset: 0x0015FC3F
		public virtual bool HandleEvent(IMeleePenetrationEvent E)
		{
			return true;
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x00161A42 File Offset: 0x0015FC42
		public virtual bool HandleEvent(IMentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x00161A45 File Offset: 0x0015FC45
		public virtual bool HandleEvent(INavigationWeightEvent E)
		{
			return true;
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x00161A48 File Offset: 0x0015FC48
		public virtual bool HandleEvent(IObjectCellInteractionEvent E)
		{
			return true;
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x00161A4B File Offset: 0x0015FC4B
		public virtual bool HandleEvent(IObjectCreationEvent E)
		{
			return true;
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x00161A4E File Offset: 0x0015FC4E
		public virtual bool HandleEvent(IObjectInventoryInteractionEvent E)
		{
			return true;
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x00161A51 File Offset: 0x0015FC51
		public virtual bool HandleEvent(IQuestEvent E)
		{
			return true;
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x00161A54 File Offset: 0x0015FC54
		public virtual bool HandleEvent(IRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x00161A57 File Offset: 0x0015FC57
		public virtual bool HandleEvent(IReplicationEvent E)
		{
			return true;
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x00161A5A File Offset: 0x0015FC5A
		public virtual bool HandleEvent(ISaveEvent E)
		{
			return true;
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x00161A5D File Offset: 0x0015FC5D
		public virtual bool HandleEvent(IShallowZoneEvent E)
		{
			return true;
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x00161A60 File Offset: 0x0015FC60
		public virtual bool HandleEvent(IShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x00161A63 File Offset: 0x0015FC63
		public virtual bool HandleEvent(ITravelEvent E)
		{
			return true;
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x00161A66 File Offset: 0x0015FC66
		public virtual bool HandleEvent(IValueEvent E)
		{
			return true;
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x00161A69 File Offset: 0x0015FC69
		public virtual bool HandleEvent(IWeightEvent E)
		{
			return true;
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x00161A6C File Offset: 0x0015FC6C
		public virtual bool HandleEvent(IXPEvent E)
		{
			return true;
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x00161A6F File Offset: 0x0015FC6F
		public virtual bool HandleEvent(IZoneEvent E)
		{
			return true;
		}

		// Token: 0x06004449 RID: 17481 RVA: 0x00161A72 File Offset: 0x0015FC72
		public virtual bool HandleEvent(IZoneOnlyEvent E)
		{
			return true;
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x00161A75 File Offset: 0x0015FC75
		public virtual bool HandleEvent(IdleQueryEvent E)
		{
			return true;
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x00161A78 File Offset: 0x0015FC78
		public virtual bool HandleEvent(ImplantAddedEvent E)
		{
			return true;
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x00161A7B File Offset: 0x0015FC7B
		public virtual bool HandleEvent(ImplantRemovedEvent E)
		{
			return true;
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x00161A7E File Offset: 0x0015FC7E
		public virtual bool HandleEvent(ImplantedEvent E)
		{
			return true;
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x00161A81 File Offset: 0x0015FC81
		public virtual bool HandleEvent(InduceVomitingEvent E)
		{
			return true;
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x00161A84 File Offset: 0x0015FC84
		public virtual bool HandleEvent(InductionChargeEvent E)
		{
			return true;
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x00161A87 File Offset: 0x0015FC87
		public virtual bool HandleEvent(InteriorZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x00161A8A File Offset: 0x0015FC8A
		public virtual bool HandleEvent(InterruptAutowalkEvent E)
		{
			return true;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x00161A8D File Offset: 0x0015FC8D
		public virtual bool HandleEvent(InventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x00161A90 File Offset: 0x0015FC90
		public virtual bool HandleEvent(IsAdaptivePenetrationActiveEvent E)
		{
			return true;
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x00161A93 File Offset: 0x0015FC93
		public virtual bool HandleEvent(IsAfflictionEvent E)
		{
			return true;
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x00161A96 File Offset: 0x0015FC96
		public virtual bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			return true;
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x00161A99 File Offset: 0x0015FC99
		public virtual bool HandleEvent(IsExplosiveEvent E)
		{
			return true;
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x00161A9C File Offset: 0x0015FC9C
		public virtual bool HandleEvent(IsMutantEvent E)
		{
			return true;
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x00161A9F File Offset: 0x0015FC9F
		public virtual bool HandleEvent(IsOverloadableEvent E)
		{
			return true;
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x00161AA2 File Offset: 0x0015FCA2
		public virtual bool HandleEvent(IsRepairableEvent E)
		{
			return true;
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x00161AA5 File Offset: 0x0015FCA5
		public virtual bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return true;
		}

		// Token: 0x0600445B RID: 17499 RVA: 0x00161AA8 File Offset: 0x0015FCA8
		public virtual bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			return true;
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x00161AAB File Offset: 0x0015FCAB
		public virtual bool HandleEvent(IsTrueKinEvent E)
		{
			return true;
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x00161AAE File Offset: 0x0015FCAE
		public virtual bool HandleEvent(IsVehicleOperationalEvent E)
		{
			return true;
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x00161AB1 File Offset: 0x0015FCB1
		public virtual bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			return true;
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x00161AB4 File Offset: 0x0015FCB4
		public virtual bool HandleEvent(JoinedPartyLeaderEvent E)
		{
			return true;
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x00161AB7 File Offset: 0x0015FCB7
		public virtual bool HandleEvent(JumpedEvent E)
		{
			return true;
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x00161ABA File Offset: 0x0015FCBA
		public virtual bool HandleEvent(KilledEvent E)
		{
			return true;
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x00161ABD File Offset: 0x0015FCBD
		public virtual bool HandleEvent(KilledPlayerEvent E)
		{
			return true;
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x00161AC0 File Offset: 0x0015FCC0
		public virtual bool HandleEvent(LateBeforeApplyDamageEvent E)
		{
			return true;
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x00161AC3 File Offset: 0x0015FCC3
		public virtual bool HandleEvent(LeaveCellEvent E)
		{
			return true;
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x00161AC6 File Offset: 0x0015FCC6
		public virtual bool HandleEvent(LeavingCellEvent E)
		{
			return true;
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x00161AC9 File Offset: 0x0015FCC9
		public virtual bool HandleEvent(LeftCellEvent E)
		{
			return true;
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x00161ACC File Offset: 0x0015FCCC
		public virtual bool HandleEvent(LiquidMixedEvent E)
		{
			return true;
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x00161ACF File Offset: 0x0015FCCF
		public virtual bool HandleEvent(LoadAmmoEvent E)
		{
			return true;
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x00161AD2 File Offset: 0x0015FCD2
		public virtual bool HandleEvent(MakeTemporaryEvent E)
		{
			return true;
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x00161AD5 File Offset: 0x0015FCD5
		public virtual bool HandleEvent(MentalAttackEvent E)
		{
			return true;
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x00161AD8 File Offset: 0x0015FCD8
		public virtual bool HandleEvent(MissilePenetrateEvent E)
		{
			return true;
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x00161ADB File Offset: 0x0015FCDB
		public virtual bool HandleEvent(MissileTraversingCellEvent E)
		{
			return true;
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x00161ADE File Offset: 0x0015FCDE
		public virtual bool HandleEvent(ModificationAppliedEvent E)
		{
			return true;
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x00161AE1 File Offset: 0x0015FCE1
		public virtual bool HandleEvent(ModifyAttackingSaveEvent E)
		{
			return true;
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x00161AE4 File Offset: 0x0015FCE4
		public virtual bool HandleEvent(ModifyBitCostEvent E)
		{
			return true;
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x00161AE7 File Offset: 0x0015FCE7
		public virtual bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x00161AEA File Offset: 0x0015FCEA
		public virtual bool HandleEvent(ModifyOriginatingSaveEvent E)
		{
			return true;
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x00161AED File Offset: 0x0015FCED
		public virtual bool HandleEvent(MovementModeChangedEvent E)
		{
			return true;
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x00161AF0 File Offset: 0x0015FCF0
		public virtual bool HandleEvent(MutationsSubjectToEMPEvent E)
		{
			return true;
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x00161AF3 File Offset: 0x0015FCF3
		public virtual bool HandleEvent(NeedPartSupportEvent E)
		{
			return true;
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x00161AF6 File Offset: 0x0015FCF6
		public virtual bool HandleEvent(NeedsReloadEvent E)
		{
			return true;
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x00161AF9 File Offset: 0x0015FCF9
		public virtual bool HandleEvent(NeutronFluxPourExplodesEvent E)
		{
			return true;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x00161AFC File Offset: 0x0015FCFC
		public virtual bool HandleEvent(NotifyTargetImmuneEvent E)
		{
			return true;
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x00161AFF File Offset: 0x0015FCFF
		public virtual bool HandleEvent(ObjectCreatedEvent E)
		{
			return true;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x00161B02 File Offset: 0x0015FD02
		public virtual bool HandleEvent(ObjectEnteredCellEvent E)
		{
			return true;
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x00161B05 File Offset: 0x0015FD05
		public virtual bool HandleEvent(ObjectEnteringCellEvent E)
		{
			return true;
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x00161B08 File Offset: 0x0015FD08
		public virtual bool HandleEvent(ObjectGoingProneEvent E)
		{
			return true;
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x00161B0B File Offset: 0x0015FD0B
		public virtual bool HandleEvent(ObjectLeavingCellEvent E)
		{
			return true;
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x00161B0E File Offset: 0x0015FD0E
		public virtual bool HandleEvent(ObjectStartedFlyingEvent E)
		{
			return true;
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x00161B11 File Offset: 0x0015FD11
		public virtual bool HandleEvent(ObjectStoppedFlyingEvent E)
		{
			return true;
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x00161B14 File Offset: 0x0015FD14
		public virtual bool HandleEvent(OkayToDamageEvent E)
		{
			return true;
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x00161B17 File Offset: 0x0015FD17
		public virtual bool HandleEvent(OnDeathRemovalEvent E)
		{
			return true;
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x00161B1A File Offset: 0x0015FD1A
		public virtual bool HandleEvent(OnDestroyObjectEvent E)
		{
			return true;
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x00161B1D File Offset: 0x0015FD1D
		public virtual bool HandleEvent(OnQuestAddedEvent E)
		{
			return true;
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x00161B20 File Offset: 0x0015FD20
		public virtual bool HandleEvent(OwnerAfterInventoryActionEvent E)
		{
			return true;
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x00161B23 File Offset: 0x0015FD23
		public virtual bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			return true;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x00161B26 File Offset: 0x0015FD26
		public virtual bool HandleEvent(OwnerGetShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x00161B29 File Offset: 0x0015FD29
		public virtual bool HandleEvent(OwnerGetUnknownShortDescriptionEvent E)
		{
			return true;
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x00161B2C File Offset: 0x0015FD2C
		public virtual bool HandleEvent(PartAddedEvent E)
		{
			return true;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x00161B2F File Offset: 0x0015FD2F
		public virtual bool HandleEvent(PartRemovedEvent E)
		{
			return true;
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x00161B32 File Offset: 0x0015FD32
		public virtual bool HandleEvent(PartSupportEvent E)
		{
			return true;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x00161B35 File Offset: 0x0015FD35
		public virtual bool HandleEvent(PathAsBurrowerEvent E)
		{
			return true;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x00161B38 File Offset: 0x0015FD38
		public virtual bool HandleEvent(PhysicalContactEvent E)
		{
			return true;
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x00161B3B File Offset: 0x0015FD3B
		public virtual bool HandleEvent(PointDefenseInterceptEvent E)
		{
			return true;
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x00161B3E File Offset: 0x0015FD3E
		public virtual bool HandleEvent(PollForHealingLocationEvent E)
		{
			return true;
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x00161B41 File Offset: 0x0015FD41
		public virtual bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			return true;
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x00161B44 File Offset: 0x0015FD44
		public virtual bool HandleEvent(PowerUpdatedEvent E)
		{
			return true;
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x00161B47 File Offset: 0x0015FD47
		public virtual bool HandleEvent(PreferDefaultBehaviorEvent E)
		{
			return true;
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x00161B4A File Offset: 0x0015FD4A
		public virtual bool HandleEvent(PreferTargetEvent E)
		{
			return true;
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x00161B4D File Offset: 0x0015FD4D
		public virtual bool HandleEvent(PreventSmartUseEvent E)
		{
			return true;
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x00161B50 File Offset: 0x0015FD50
		public virtual bool HandleEvent(PrimePowerSystemsEvent E)
		{
			return true;
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x00161B53 File Offset: 0x0015FD53
		public virtual bool HandleEvent(ProducesLiquidEvent E)
		{
			return true;
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x00161B56 File Offset: 0x0015FD56
		public virtual bool HandleEvent(ProjectileMovingEvent E)
		{
			return true;
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x00161B59 File Offset: 0x0015FD59
		public virtual bool HandleEvent(QueryBroadcastDrawEvent E)
		{
			return true;
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x00161B5C File Offset: 0x0015FD5C
		public virtual bool HandleEvent(QueryChargeEvent E)
		{
			return true;
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x00161B5F File Offset: 0x0015FD5F
		public virtual bool HandleEvent(QueryChargeProductionEvent E)
		{
			return true;
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x00161B62 File Offset: 0x0015FD62
		public virtual bool HandleEvent(QueryChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x00161B65 File Offset: 0x0015FD65
		public virtual bool HandleEvent(QueryDrawEvent E)
		{
			return true;
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x00161B68 File Offset: 0x0015FD68
		public virtual bool HandleEvent(QueryEquippableListEvent E)
		{
			return true;
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x00161B6B File Offset: 0x0015FD6B
		public virtual bool HandleEvent(QueryInductionChargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x00161B6E File Offset: 0x0015FD6E
		public virtual bool HandleEvent(QueryRechargeStorageEvent E)
		{
			return true;
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x00161B71 File Offset: 0x0015FD71
		public virtual bool HandleEvent(QuerySlotListEvent E)
		{
			return true;
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x00161B74 File Offset: 0x0015FD74
		public virtual bool HandleEvent(QuestFinishedEvent E)
		{
			return true;
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x00161B77 File Offset: 0x0015FD77
		public virtual bool HandleEvent(QuestStartedEvent E)
		{
			return true;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x00161B7A File Offset: 0x0015FD7A
		public virtual bool HandleEvent(QuestStepFinishedEvent E)
		{
			return true;
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x00161B7D File Offset: 0x0015FD7D
		public virtual bool HandleEvent(RadiatesHeatAdjacentEvent E)
		{
			return true;
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x00161B80 File Offset: 0x0015FD80
		public virtual bool HandleEvent(RadiatesHeatEvent E)
		{
			return true;
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x00161B83 File Offset: 0x0015FD83
		public virtual bool HandleEvent(RealityStabilizeEvent E)
		{
			return true;
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x00161B86 File Offset: 0x0015FD86
		public virtual bool HandleEvent(RechargeAvailableEvent E)
		{
			return true;
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x00161B89 File Offset: 0x0015FD89
		public virtual bool HandleEvent(RefreshTileEvent E)
		{
			return true;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x00161B8C File Offset: 0x0015FD8C
		public virtual bool HandleEvent(RegenerateDefaultEquipmentEvent E)
		{
			return true;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x00161B8F File Offset: 0x0015FD8F
		public virtual bool HandleEvent(RegenerateLimbEvent E)
		{
			return true;
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x00161B92 File Offset: 0x0015FD92
		public virtual bool HandleEvent(RemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x00161B95 File Offset: 0x0015FD95
		public virtual bool HandleEvent(RepaintedEvent E)
		{
			return true;
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x00161B98 File Offset: 0x0015FD98
		public virtual bool HandleEvent(RepairCriticalFailureEvent E)
		{
			return true;
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x00161B9B File Offset: 0x0015FD9B
		public virtual bool HandleEvent(RepairedEvent E)
		{
			return true;
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x00161B9E File Offset: 0x0015FD9E
		public virtual bool HandleEvent(ReplaceInContextEvent E)
		{
			return true;
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x00161BA1 File Offset: 0x0015FDA1
		public virtual bool HandleEvent(ReplaceThrownWeaponEvent E)
		{
			return true;
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x00161BA4 File Offset: 0x0015FDA4
		public virtual bool HandleEvent(ReplicaCreatedEvent E)
		{
			return true;
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x00161BA7 File Offset: 0x0015FDA7
		public virtual bool HandleEvent(ReputationChangeEvent E)
		{
			return true;
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x00161BAA File Offset: 0x0015FDAA
		public virtual bool HandleEvent(RespiresEvent E)
		{
			return true;
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x00161BAD File Offset: 0x0015FDAD
		public virtual bool HandleEvent(SecretVisibilityChangedEvent E)
		{
			return true;
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x00161BB0 File Offset: 0x0015FDB0
		public virtual bool HandleEvent(ShieldSlamPerformedEvent E)
		{
			return true;
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x00161BB3 File Offset: 0x0015FDB3
		public virtual bool HandleEvent(ShotCompleteEvent E)
		{
			return true;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x00161BB6 File Offset: 0x0015FDB6
		public virtual bool HandleEvent(ShouldAttackToReachTargetEvent E)
		{
			return true;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x00161BB9 File Offset: 0x0015FDB9
		public virtual bool HandleEvent(ShouldDescribeStatBonusEvent E)
		{
			return true;
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x00161BBC File Offset: 0x0015FDBC
		public virtual bool HandleEvent(SlamEvent E)
		{
			return true;
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x00161BBF File Offset: 0x0015FDBF
		public virtual bool HandleEvent(StackCountChangedEvent E)
		{
			return true;
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x00161BC2 File Offset: 0x0015FDC2
		public virtual bool HandleEvent(StartTradeEvent E)
		{
			return true;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x00161BC5 File Offset: 0x0015FDC5
		public virtual bool HandleEvent(StatChangeEvent E)
		{
			return true;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x00161BC8 File Offset: 0x0015FDC8
		public virtual bool HandleEvent(StockedEvent E)
		{
			return true;
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x00161BCB File Offset: 0x0015FDCB
		public virtual bool HandleEvent(StripContentsEvent E)
		{
			return true;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x00161BCE File Offset: 0x0015FDCE
		public virtual bool HandleEvent(SubjectToGravityEvent E)
		{
			return true;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x00161BD1 File Offset: 0x0015FDD1
		public virtual bool HandleEvent(SuspendingEvent E)
		{
			return true;
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x00161BD4 File Offset: 0x0015FDD4
		public virtual bool HandleEvent(SyncMutationLevelsEvent E)
		{
			return true;
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x00161BD7 File Offset: 0x0015FDD7
		public virtual bool HandleEvent(SyncRenderEvent E)
		{
			return true;
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x00161BDA File Offset: 0x0015FDDA
		public virtual bool HandleEvent(SynchronizeExistenceEvent E)
		{
			return true;
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x00161BDD File Offset: 0x0015FDDD
		public virtual bool HandleEvent(TakeOnRoleEvent E)
		{
			return true;
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x00161BE0 File Offset: 0x0015FDE0
		public virtual bool HandleEvent(TakenEvent E)
		{
			return true;
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x00161BE3 File Offset: 0x0015FDE3
		public virtual bool HandleEvent(TestChargeEvent E)
		{
			return true;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x00161BE6 File Offset: 0x0015FDE6
		public virtual bool HandleEvent(ThawedEvent E)
		{
			return true;
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x00161BE9 File Offset: 0x0015FDE9
		public virtual bool HandleEvent(TookDamageEvent E)
		{
			return true;
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x00161BEC File Offset: 0x0015FDEC
		public virtual bool HandleEvent(TookEnvironmentalDamageEvent E)
		{
			return true;
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x00161BEF File Offset: 0x0015FDEF
		public virtual bool HandleEvent(TookEvent E)
		{
			return true;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x00161BF2 File Offset: 0x0015FDF2
		public virtual bool HandleEvent(TransparentToEMPEvent E)
		{
			return true;
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x00161BF5 File Offset: 0x0015FDF5
		public virtual bool HandleEvent(TravelSpeedEvent E)
		{
			return true;
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x00161BF8 File Offset: 0x0015FDF8
		public virtual bool HandleEvent(TriggersMakersMarkCreationEvent E)
		{
			return true;
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x00161BFB File Offset: 0x0015FDFB
		public virtual bool HandleEvent(TryRemoveFromContextEvent E)
		{
			return true;
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x00161BFE File Offset: 0x0015FDFE
		public virtual bool HandleEvent(UnequippedEvent E)
		{
			return true;
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x00161C01 File Offset: 0x0015FE01
		public virtual bool HandleEvent(UnimplantedEvent E)
		{
			return true;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x00161C04 File Offset: 0x0015FE04
		public virtual bool HandleEvent(UseChargeEvent E)
		{
			return true;
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x00161C07 File Offset: 0x0015FE07
		public virtual bool HandleEvent(UseDramsEvent E)
		{
			return true;
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x00161C0A File Offset: 0x0015FE0A
		public virtual bool HandleEvent(UseEnergyEvent E)
		{
			return true;
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x00161C0D File Offset: 0x0015FE0D
		public virtual bool HandleEvent(UseHealingLocationEvent E)
		{
			return true;
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x00161C10 File Offset: 0x0015FE10
		public virtual bool HandleEvent(UsingChargeEvent E)
		{
			return true;
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x00161C13 File Offset: 0x0015FE13
		public virtual bool HandleEvent(VaporizedEvent E)
		{
			return true;
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x00161C16 File Offset: 0x0015FE16
		public virtual bool HandleEvent(WantsLiquidCollectionEvent E)
		{
			return true;
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x00161C19 File Offset: 0x0015FE19
		public virtual bool HandleEvent(WasDerivedFromEvent E)
		{
			return true;
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x00161C1C File Offset: 0x0015FE1C
		public virtual bool HandleEvent(WasReplicatedEvent E)
		{
			return true;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x00161C1F File Offset: 0x0015FE1F
		public virtual bool HandleEvent(WaterRitualStartEvent E)
		{
			return true;
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x00161C22 File Offset: 0x0015FE22
		public virtual bool HandleEvent(WorshipPerformedEvent E)
		{
			return true;
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x00161C25 File Offset: 0x0015FE25
		public virtual bool HandleEvent(ZoneActivatedEvent E)
		{
			return true;
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x00161C28 File Offset: 0x0015FE28
		public virtual bool HandleEvent(ZoneBuiltEvent E)
		{
			return true;
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x00161C2B File Offset: 0x0015FE2B
		public virtual bool HandleEvent(ZoneDeactivatedEvent E)
		{
			return true;
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x00161C2E File Offset: 0x0015FE2E
		public virtual bool HandleEvent(ZoneThawedEvent E)
		{
			return true;
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060044DE RID: 17630
		public abstract IEventBinder Binder { get; }

		// Token: 0x060044DF RID: 17631
		public abstract T GetComponentBasis();

		// Token: 0x060044E0 RID: 17632 RVA: 0x00161C31 File Offset: 0x0015FE31
		public virtual GameObject GetBasisGameObject()
		{
			return this.GetComponentBasis() as GameObject;
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x00161C43 File Offset: 0x0015FE43
		public virtual Cell GetBasisCell()
		{
			return (this.GetComponentBasis() as Cell) ?? this.FindBasisCell();
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x00161C5F File Offset: 0x0015FE5F
		private Cell FindBasisCell()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return null;
			}
			return basisGameObject.CurrentCell;
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x00161C72 File Offset: 0x0015FE72
		public virtual Cell GetAnyBasisCell()
		{
			return (this.GetComponentBasis() as Cell) ?? this.FindAnyBasisCell();
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x00161C8E File Offset: 0x0015FE8E
		private Cell FindAnyBasisCell()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return null;
			}
			return basisGameObject.GetCurrentCell();
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x00161CA1 File Offset: 0x0015FEA1
		public virtual Zone GetBasisZone()
		{
			return (this.GetComponentBasis() as Zone) ?? this.FindBasisZone();
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x00161CBD File Offset: 0x0015FEBD
		private Zone FindBasisZone()
		{
			Cell basisCell = this.GetBasisCell();
			if (basisCell == null)
			{
				return null;
			}
			return basisCell.ParentZone;
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x00161CD0 File Offset: 0x0015FED0
		public virtual Zone GetAnyBasisZone()
		{
			return (this.GetComponentBasis() as Zone) ?? this.FindAnyBasisZone();
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x00161CEC File Offset: 0x0015FEEC
		private Zone FindAnyBasisZone()
		{
			Cell anyBasisCell = this.GetAnyBasisCell();
			if (anyBasisCell == null)
			{
				return null;
			}
			return anyBasisCell.ParentZone;
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x00161CFF File Offset: 0x0015FEFF
		public virtual bool handleDispatch(MinEvent e)
		{
			MetricsManager.LogError("base IComponent::handleDispatch called for " + e.GetType().Name);
			return true;
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x00161D1C File Offset: 0x0015FF1C
		public static long frameTimerMS
		{
			get
			{
				return XRLCore.FrameTimer.ElapsedMilliseconds;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x00161D28 File Offset: 0x0015FF28
		[JsonIgnore]
		public Cell currentCell
		{
			get
			{
				return this.GetBasisCell();
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x00161D30 File Offset: 0x0015FF30
		public virtual bool IsValid
		{
			get
			{
				return this.GetComponentBasis() != null;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060044ED RID: 17645 RVA: 0x00161D40 File Offset: 0x0015FF40
		public static long currentTurn
		{
			get
			{
				return The.Game.Turns;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x00161D4C File Offset: 0x0015FF4C
		public static long wallTime
		{
			get
			{
				return The.Game.WallTime.ElapsedMilliseconds;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x00161D5D File Offset: 0x0015FF5D
		public static XRLCore TheCore
		{
			get
			{
				return The.Core;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x00161D64 File Offset: 0x0015FF64
		public static XRLGame TheGame
		{
			get
			{
				return The.Game;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x00161D6B File Offset: 0x0015FF6B
		public static GameObject ThePlayer
		{
			get
			{
				return The.Player;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x00161D72 File Offset: 0x0015FF72
		public static Cell ThePlayerCell
		{
			get
			{
				GameObject player = The.Player;
				if (player == null)
				{
					return null;
				}
				return player.CurrentCell;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x00161D84 File Offset: 0x0015FF84
		public static string ThePlayerMythDomain
		{
			get
			{
				return The.Game.GetStringGameState("ThePlayerMythDomain", "glass");
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x00161D9A File Offset: 0x0015FF9A
		public static bool TerseMessages
		{
			get
			{
				return The.Game.Player.Messages.Terse;
			}
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x00161DB5 File Offset: 0x0015FFB5
		public bool hasProperty(string property)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return basisGameObject != null && basisGameObject.HasProperty(property);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x00161DC9 File Offset: 0x0015FFC9
		public void LogInEditor(string s)
		{
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x00161DCB File Offset: 0x0015FFCB
		public void LogWarningInEditor(string s)
		{
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x00161DCD File Offset: 0x0015FFCD
		public void LogErrorInEditor(string s)
		{
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x00161DCF File Offset: 0x0015FFCF
		public void Log(string s)
		{
			MetricsManager.LogInfo(s);
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x00161DD7 File Offset: 0x0015FFD7
		public void LogWarning(string s)
		{
			MetricsManager.LogWarning(s);
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x00161DDF File Offset: 0x0015FFDF
		public void LogError(string s)
		{
			MetricsManager.LogError(s);
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x00161DE7 File Offset: 0x0015FFE7
		public void LogError(string s, Exception ex)
		{
			MetricsManager.LogError(s, ex);
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x00161DF0 File Offset: 0x0015FFF0
		public float ConTarget(GameObject target = null)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return -1f;
			}
			if (target == null)
			{
				target = basisGameObject.Target;
				if (target == null)
				{
					return -1f;
				}
			}
			if (target.IsPlayer())
			{
				return 1f;
			}
			if (!basisGameObject.HasStat("Level"))
			{
				return 1f;
			}
			if (!target.HasStat("Level"))
			{
				return 0f;
			}
			int num = target.Stat("Level", 0);
			if (target.GetTagOrStringProperty("Role", "Minion") == "Minion")
			{
				num /= 4;
			}
			if (basisGameObject.HasStat("Hitpoints"))
			{
				if (basisGameObject.hitpoints < basisGameObject.BaseStat("Hitpoints", 0) / 2)
				{
					num *= 2;
				}
				if (basisGameObject.hitpoints < basisGameObject.BaseStat("Hitpoints", 0) / 4)
				{
					num *= 2;
				}
			}
			return (float)num / (float)basisGameObject.Stat("Level", 0);
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x00161ED3 File Offset: 0x001600D3
		public float ConTarget(Event E)
		{
			return this.ConTarget(E.GetGameObjectParameter("Target"));
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x00161EE8 File Offset: 0x001600E8
		public CombatJuiceEntryWorldSound GetJuiceWorldSound(string Clip, float Volume = 0.5f, float PitchVariance = 0f, float Delay = 0f)
		{
			if (Clip.IsNullOrEmpty() || !Options.Sound)
			{
				return null;
			}
			Cell anyBasisCell = this.GetAnyBasisCell();
			if (anyBasisCell == null)
			{
				return null;
			}
			int num;
			bool flag;
			if (!anyBasisCell.TryGetSoundPropagation(Mathf.RoundToInt(40f * Volume), out num, out flag))
			{
				return null;
			}
			return new CombatJuiceEntryWorldSound(Clip, num, flag, Volume, PitchVariance, Delay, anyBasisCell.Location);
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x00161F40 File Offset: 0x00160140
		public void PlayWorldSound(string Clip, float Volume = 0.5f, float PitchVariance = 0f, bool Combat = false, Cell SourceCell = null, float Delay = 0f, float Pitch = 1f, float CostMultiplier = 1f, int CostMaximum = 2147483647)
		{
			if (Clip.IsNullOrEmpty())
			{
				return;
			}
			if (!Options.Sound)
			{
				return;
			}
			if (SourceCell == null)
			{
				SourceCell = this.GetAnyBasisCell();
				if (SourceCell == null)
				{
					return;
				}
			}
			SourceCell.PlayWorldSound(Clip, Volume, PitchVariance, Combat, Delay, Pitch, CostMultiplier, CostMaximum);
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x00161F83 File Offset: 0x00160183
		public static void PlayUISound(string Clip, float Volume = 1f, bool Combat = false, SoundRequest.SoundEffectType Effect = SoundRequest.SoundEffectType.None)
		{
			if (!Clip.IsNullOrEmpty() && Options.Sound && (!Combat || Options.UseCombatSounds))
			{
				SoundManager.PlaySound(Clip, 0f, Volume, 1f, Effect, 0f);
			}
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x00161FB5 File Offset: 0x001601B5
		public virtual bool FireEvent(Event E)
		{
			return true;
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x00161FB8 File Offset: 0x001601B8
		public bool FireEvent(Event E, IEvent ParentEvent)
		{
			bool flag = this.FireEvent(E);
			if (ParentEvent != null)
			{
				ParentEvent.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x00161FCB File Offset: 0x001601CB
		public bool IsDay()
		{
			return Calendar.IsDay();
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x00161FD2 File Offset: 0x001601D2
		public bool IsNight()
		{
			return !Calendar.IsDay();
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x00161FDC File Offset: 0x001601DC
		public static void AddPlayerMessage(string Message, string Color = null, bool Capitalize = true)
		{
			MessageQueue.AddPlayerMessage(Message, Color, Capitalize);
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00161FE6 File Offset: 0x001601E6
		public static void AddPlayerMessage(string Message, char Color, bool Capitalize = true)
		{
			MessageQueue.AddPlayerMessage(Message, Color, Capitalize);
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06004508 RID: 17672 RVA: 0x00161FF0 File Offset: 0x001601F0
		[JsonIgnore]
		public bool juiceEnabled
		{
			get
			{
				return Options.UseOverlayCombatEffects;
			}
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x00161FF8 File Offset: 0x001601F8
		public bool IsPlayer()
		{
			GameObject player = The.Player;
			return player != null && this.GetBasisGameObject() == player;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600450A RID: 17674 RVA: 0x0016201C File Offset: 0x0016021C
		public bool IsHidden
		{
			get
			{
				GameObject basisGameObject = this.GetBasisGameObject();
				Hidden hidden = ((basisGameObject != null) ? basisGameObject.GetPart<Hidden>() : null);
				return hidden != null && !hidden.Found;
			}
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x0016204A File Offset: 0x0016024A
		public void Reveal()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			Hidden hidden = ((basisGameObject != null) ? basisGameObject.GetPart<Hidden>() : null);
			if (hidden == null)
			{
				return;
			}
			hidden.Reveal();
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x00162068 File Offset: 0x00160268
		public Cell PickDirection(string Label = null, GameObject POV = null)
		{
			return this.PickDirection(true, Label, POV);
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x00162080 File Offset: 0x00160280
		public string PickDirectionS(string Label = null, GameObject POV = null)
		{
			return this.PickDirectionS(true, POV, Label);
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x0016208C File Offset: 0x0016028C
		public string PickDirectionS(bool ForAttack, GameObject POV = null, string Label = null)
		{
			if (POV == null)
			{
				GameObject basisGameObject = this.GetBasisGameObject();
				POV = basisGameObject.Holder ?? basisGameObject;
				if (POV == null)
				{
					return null;
				}
			}
			if (POV.IsSelfControlledPlayer())
			{
				return XRL.UI.PickDirection.ShowPicker(Label);
			}
			GameObject target = POV.Target;
			if (target != null)
			{
				return POV.CurrentCell.GetDirectionFromCell(target.CurrentCell, false);
			}
			return null;
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x001620E4 File Offset: 0x001602E4
		public Cell PickDirection(bool ForAttack, string Label = null, GameObject POV = null)
		{
			if (POV == null)
			{
				GameObject basisGameObject = this.GetBasisGameObject();
				POV = basisGameObject.Holder ?? basisGameObject;
				if (POV == null)
				{
					return null;
				}
			}
			if (POV.IsSelfControlledPlayer())
			{
				string text = XRL.UI.PickDirection.ShowPicker(Label);
				if (text != null)
				{
					return POV.CurrentCell.GetCellFromDirection(text, true);
				}
			}
			else
			{
				GameObject target = POV.Target;
				if (target != null)
				{
					Cell currentCell = POV.CurrentCell;
					return currentCell.GetCellFromDirection(currentCell.GetDirectionFromCell(target.CurrentCell, false), true);
				}
			}
			return null;
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x00162154 File Offset: 0x00160354
		public bool HasPropertyOrTag(string TagName)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return basisGameObject != null && basisGameObject.HasPropertyOrTag(TagName);
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x00162174 File Offset: 0x00160374
		public bool HasTag(string TagName)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return basisGameObject != null && basisGameObject.HasTag(TagName);
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x00162194 File Offset: 0x00160394
		public string GetPropertyOrTag(string Name, string Default = null)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return Default;
			}
			return basisGameObject.GetPropertyOrTag(Name, Default);
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x001621B8 File Offset: 0x001603B8
		public string GetTag(string TagName, string Default = null)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return Default;
			}
			return basisGameObject.GetTag(TagName, Default);
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x001621DC File Offset: 0x001603DC
		public List<Cell> PickCloud(int Radius)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return null;
			}
			List<Cell> adjacentCells = basisGameObject.CurrentCell.GetAdjacentCells(Radius, true, true);
			if (!basisGameObject.IsSelfControlledPlayer())
			{
				Brain brain = basisGameObject.Brain;
				GameObject gameObject = ((brain != null) ? brain.Target : null);
				using (List<Cell>.Enumerator enumerator = adjacentCells.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasRelevantFriendlyExcept(basisGameObject, gameObject))
						{
							return null;
						}
					}
				}
				return adjacentCells;
			}
			return adjacentCells;
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x0016226C File Offset: 0x0016046C
		public List<Cell> PickLine(int Length = 9999, AllowVis VisLevel = AllowVis.OnlyVisible, Predicate<GameObject> Filter = null, bool IgnoreSolid = false, bool IgnoreLOS = false, bool RequireCombat = true, bool BlackoutStops = false, GameObject Attacker = null, GameObject Projectile = null, string Label = null, bool Snap = false, bool Locked = true)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return Event.NewCellList();
			}
			Cell currentCell = basisGameObject.CurrentCell;
			if (currentCell == null)
			{
				return Event.NewCellList();
			}
			Zone parentZone = currentCell.ParentZone;
			if (parentZone == null)
			{
				return Event.NewCellList();
			}
			List<Point> list = new List<Point>();
			if (!basisGameObject.IsSelfControlledPlayer())
			{
				GameObject target = basisGameObject.Target;
				if (target != null)
				{
					if (Filter != null && !Filter(target))
					{
						return null;
					}
					Cell currentCell2 = target.CurrentCell;
					int num = currentCell.X - Length;
					int num2 = currentCell.X + Length;
					int num3 = currentCell.Y - Length;
					int num4 = currentCell.Y + Length;
					parentZone.Constrain(ref num, ref num3, ref num2, ref num4);
					List<Cell> list2 = Event.NewCellList();
					for (int i = num3; i <= num4; i++)
					{
						for (int j = num; j <= num2; j++)
						{
							list2.Clear();
							bool flag = false;
							Zone.Line(currentCell.X, currentCell.Y, j, i, list);
							foreach (Point point in list)
							{
								if ((point.X != currentCell.X || point.Y != currentCell.Y) && point.X > 0 && point.Y > 0 && point.X < parentZone.Width && point.Y < parentZone.Height)
								{
									Cell cell = parentZone.GetCell(point.X, point.Y);
									list2.Add(cell);
									if (cell == currentCell2)
									{
										flag = true;
									}
								}
							}
							if (flag)
							{
								bool flag2 = false;
								int num5 = 0;
								while (num5 < list2.Count && !flag2)
								{
									if (BlackoutStops && list2[num5].IsBlackedOut())
									{
										flag2 = true;
										break;
									}
									if (!IgnoreSolid && list2[num5] != currentCell2 && list2[num5].IsSolidForProjectile(Projectile, Attacker, null, target, null, null, true))
									{
										flag2 = true;
										break;
									}
									flag2 = list2[num5].HasRelevantFriendlyExcept(basisGameObject, target);
									num5++;
								}
								if (!flag2)
								{
									return list2;
								}
							}
						}
					}
				}
				return null;
			}
			Cell cell2 = currentCell;
			if (Snap && basisGameObject.GetTotalConfusion() <= 0)
			{
				GameObject gameObject;
				if ((gameObject = basisGameObject.Target) == null)
				{
					GameObject gameObject2 = basisGameObject;
					bool flag3 = true;
					bool flag4 = !IgnoreSolid;
					gameObject = gameObject2.GetNearestVisibleObject(flag3, RequireCombat ? "Combat" : "Physics", 80, flag4, IgnoreLOS, null);
				}
				GameObject gameObject3 = gameObject;
				if (gameObject3 != null)
				{
					cell2 = gameObject3.CurrentCell ?? cell2;
				}
			}
			Cell cell3 = PickTarget.ShowPicker(PickTarget.PickStyle.Line, Length, 9999, cell2.X, cell2.Y, Locked, VisLevel, null, Filter, null, null, Label, false, true);
			if (cell3 == null)
			{
				return null;
			}
			List<Cell> list3 = Event.NewCellList();
			Zone.Line(currentCell.X, currentCell.Y, cell3.X, cell3.Y, list);
			foreach (Point point2 in list)
			{
				list3.Add(parentZone.GetCell(point2.X, point2.Y));
			}
			return list3;
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x001625D4 File Offset: 0x001607D4
		public List<Cell> PickCone(int Length = 9999, int Angle = 45, AllowVis VisLevel = AllowVis.OnlyVisible, Predicate<GameObject> Filter = null, string Label = null)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject.IsInvalid())
			{
				return null;
			}
			if (!basisGameObject.IsSelfControlledPlayer())
			{
				GameObject target = basisGameObject.Target;
				if (target != null)
				{
					if (Filter != null && !Filter(target))
					{
						return null;
					}
					Cell currentCell = basisGameObject.CurrentCell;
					Cell currentCell2 = target.CurrentCell;
					Zone parentZone = currentCell.ParentZone;
					List<Location2D> cone = XRL.Rules.Geometry.GetCone(currentCell.Location, currentCell2.Location, Length, Angle, null);
					List<Cell> list = new List<Cell>();
					foreach (Location2D location2D in cone)
					{
						list.Add(parentZone.GetCell(location2D.X, location2D.Y));
					}
					list.Remove(currentCell);
					if (list.Contains(currentCell2))
					{
						using (List<Cell>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current.HasRelevantFriendlyExcept(basisGameObject, target))
								{
									return null;
								}
							}
						}
						return list;
					}
				}
				return null;
			}
			Cell currentCell3 = basisGameObject.CurrentCell;
			Cell cell = PickTarget.ShowPicker(PickTarget.PickStyle.Cone, Angle, Length, currentCell3.X, currentCell3.Y, false, VisLevel, null, Filter, null, null, Label, false, true);
			if (cell == null)
			{
				return null;
			}
			List<Location2D> cone2 = XRL.Rules.Geometry.GetCone(Location2D.Get(currentCell3.X, currentCell3.Y), Location2D.Get(cell.X, cell.Y), Length, Angle, null);
			List<Cell> list2 = new List<Cell>();
			foreach (Location2D location2D2 in cone2)
			{
				list2.Add(cell.ParentZone.GetCell(location2D2.X, location2D2.Y));
			}
			return list2;
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x001627D0 File Offset: 0x001609D0
		public List<Cell> PickBurst(int Radius = 1, int Range = 9999, bool Locked = false, AllowVis VisLevel = AllowVis.OnlyVisible, string Label = null)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			List<Cell> list;
			try
			{
				if (basisGameObject == null)
				{
					list = null;
				}
				else if (basisGameObject.IsSelfControlledPlayer())
				{
					Cell currentCell = basisGameObject.CurrentCell;
					if (currentCell == null)
					{
						list = null;
					}
					else
					{
						Cell cell = PickTarget.ShowPicker(PickTarget.PickStyle.Burst, Radius, Range, currentCell.X, currentCell.Y, Locked, VisLevel, null, null, null, null, Label, false, true);
						if (cell == null)
						{
							list = null;
						}
						else
						{
							List<Cell> result = new List<Cell>();
							result.Add(cell);
							List<Cell> list2 = new List<Cell>(8 * Radius);
							Action<Cell> <>9__0;
							for (int i = 0; i < Radius; i++)
							{
								int j = 0;
								int count = result.Count;
								while (j < count)
								{
									Cell cell2 = result[j];
									if (!list2.CleanContains(cell2))
									{
										list2.Add(cell2);
										Cell cell3 = cell2;
										Action<Cell> action;
										if ((action = <>9__0) == null)
										{
											action = (<>9__0 = delegate(Cell AC)
											{
												if (AC != null && !result.Contains(AC))
												{
													result.Add(AC);
												}
											});
										}
										cell3.ForeachAdjacentCell(action, true);
									}
									j++;
								}
							}
							list = result;
						}
					}
				}
				else
				{
					GameObject target = basisGameObject.Target;
					if (target != null)
					{
						Cell currentCell2 = basisGameObject.CurrentCell;
						if (currentCell2 == null)
						{
							return null;
						}
						Zone parentZone = currentCell2.ParentZone;
						if (parentZone == null)
						{
							return null;
						}
						Cell currentCell3 = target.CurrentCell;
						if (currentCell3 == null || currentCell3.ParentZone != parentZone)
						{
							return null;
						}
						int num = currentCell2.X - Range;
						int num2 = currentCell2.X + Range;
						int num3 = currentCell2.Y - Range;
						int num4 = currentCell2.Y + Range;
						parentZone.Constrain(ref num, ref num3, ref num2, ref num4);
						for (int k = num3; k <= num4; k++)
						{
							for (int l = num; l <= num2; l++)
							{
								List<Cell> Cells = new List<Cell>((Radius * 2 + 1) * (Radius * 2 + 1)) { parentZone.GetCell(l, k) };
								List<Cell> list3 = new List<Cell>((Radius * 2 + 1) * (Radius * 2 + 1));
								Action<Cell> <>9__1;
								for (int m = 0; m < Radius; m++)
								{
									int n = 0;
									int count2 = Cells.Count;
									while (n < count2)
									{
										Cell cell4 = Cells[n];
										if (!list3.Contains(cell4))
										{
											list3.Add(cell4);
											Cell cell5 = cell4;
											Action<Cell> action2;
											if ((action2 = <>9__1) == null)
											{
												action2 = (<>9__1 = delegate(Cell AC)
												{
													if (!Cells.Contains(AC))
													{
														Cells.Add(AC);
													}
												});
											}
											cell5.ForeachLocalAdjacentCell(action2);
										}
										n++;
									}
								}
								if (Cells.Contains(currentCell3) && !Cells.Contains(currentCell2))
								{
									using (List<Cell>.Enumerator enumerator = Cells.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											if (enumerator.Current.HasRelevantFriendlyExcept(basisGameObject, target))
											{
												return null;
											}
										}
									}
									return Cells;
								}
							}
						}
					}
					list = null;
				}
			}
			catch (Exception ex)
			{
				if (basisGameObject == null)
				{
					MetricsManager.LogException("PickBurst(null)", ex, "game_exception");
				}
				else if (basisGameObject.IsPlayer())
				{
					MetricsManager.LogException("PickBurst(player)", ex, "game_exception");
				}
				else
				{
					MetricsManager.LogException("PickBurst(" + basisGameObject.DebugName + ")", ex, "game_exception");
				}
				list = null;
			}
			return list;
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x00162B6C File Offset: 0x00160D6C
		public List<Cell> PickCircle(int Radius = 1, int Range = 9999, bool Locked = false, AllowVis VisLevel = AllowVis.OnlyVisible, string Label = null)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject.IsSelfControlledPlayer())
			{
				Cell currentCell = basisGameObject.CurrentCell;
				if (currentCell == null)
				{
					return null;
				}
				Cell cell = PickTarget.ShowPicker(PickTarget.PickStyle.Circle, Radius, Range, currentCell.X, currentCell.Y, Locked, VisLevel, null, null, null, null, Label, false, true);
				if (cell == null)
				{
					return null;
				}
				List<Cell> list = new List<Cell>();
				list.Add(cell);
				int x = cell.X;
				int y = cell.Y;
				int num = cell.X - Radius;
				int num2 = cell.X + Radius;
				int num3 = cell.Y - Radius;
				int num4 = cell.Y + Radius;
				cell.ParentZone.Constrain(ref num, ref num3, ref num2, ref num4);
				for (int i = num3; i <= num4; i++)
				{
					for (int j = num; j <= num2; j++)
					{
						if (Math.Sqrt((double)((j - x) * (j - x) + (i - y) * (i - y))) <= (double)Radius)
						{
							list.Add(cell.ParentZone.GetCell(j, i));
						}
					}
				}
				return list;
			}
			else
			{
				Cell cell2;
				if (basisGameObject == null)
				{
					cell2 = null;
				}
				else
				{
					GameObject target = basisGameObject.Target;
					cell2 = ((target != null) ? target.CurrentCell : null);
				}
				Cell cell3 = cell2;
				if (cell3 != null)
				{
					List<Cell> localAdjacentCells = cell3.GetLocalAdjacentCells(1, false);
					if (!localAdjacentCells.Contains(cell3))
					{
						localAdjacentCells.Add(cell3);
					}
					return localAdjacentCells;
				}
				return null;
			}
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x00162CC0 File Offset: 0x00160EC0
		public List<Cell> PickField(int Cells, GameObject Actor = null, string What = null, bool ReturnNullForAbort = false, bool RequireVisibility = false)
		{
			GameObject gameObject = Actor ?? this.GetBasisGameObject();
			if (gameObject == null)
			{
				return null;
			}
			if (!gameObject.IsSelfControlledPlayer())
			{
				return null;
			}
			if (What.IsNullOrEmpty())
			{
				return PickTarget.ShowFieldPicker(Cells, 1, gameObject.CurrentCell.X, gameObject.CurrentCell.Y, "Wall", false, ReturnNullForAbort, false, true, RequireVisibility);
			}
			return PickTarget.ShowFieldPicker(Cells, 1, gameObject.CurrentCell.X, gameObject.CurrentCell.Y, What, false, ReturnNullForAbort, false, true, RequireVisibility);
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x00162D40 File Offset: 0x00160F40
		public List<Cell> PickFieldAdjacent(int Cells, GameObject Actor = null, string What = null, bool ReturnNullForAbort = false, bool RequireVisibility = false)
		{
			GameObject gameObject = Actor ?? this.GetBasisGameObject();
			if (gameObject == null)
			{
				return null;
			}
			if (!gameObject.IsSelfControlledPlayer())
			{
				return null;
			}
			if (What.IsNullOrEmpty())
			{
				return PickTarget.ShowFieldPicker(Cells, 1, gameObject.CurrentCell.X, gameObject.CurrentCell.Y, "Wall", true, ReturnNullForAbort, false, true, RequireVisibility);
			}
			return PickTarget.ShowFieldPicker(Cells, 1, gameObject.CurrentCell.X, gameObject.CurrentCell.Y, What, true, ReturnNullForAbort, false, true, RequireVisibility);
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x00162DC0 File Offset: 0x00160FC0
		public Cell PickDestinationCell(int Range = 9999, AllowVis VisLevel = AllowVis.OnlyVisible, bool Locked = true, bool IgnoreSolid = false, bool IgnoreLOS = false, bool RequireCombat = true, PickTarget.PickStyle Style = PickTarget.PickStyle.EmptyCell, string Label = null, bool Snap = false, Predicate<GameObject> ExtraVisibility = null)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject.IsSelfControlledPlayer())
			{
				Cell cell = basisGameObject.CurrentCell;
				if (Snap && basisGameObject.GetTotalConfusion() <= 0)
				{
					GameObject gameObject;
					if ((gameObject = basisGameObject.Target) == null)
					{
						GameObject gameObject2 = basisGameObject;
						bool flag = true;
						bool flag2 = !IgnoreSolid;
						gameObject = gameObject2.GetNearestVisibleObject(flag, RequireCombat ? "Combat" : "Physics", 80, flag2, IgnoreLOS, ExtraVisibility);
					}
					GameObject gameObject3 = gameObject;
					if (gameObject3 != null)
					{
						cell = gameObject3.CurrentCell ?? cell;
					}
				}
				if (cell != null)
				{
					return PickTarget.ShowPicker(Style, 1, Range, cell.X, cell.Y, Locked, VisLevel, ExtraVisibility, null, null, null, Label, false, true);
				}
			}
			else
			{
				GameObject target = basisGameObject.Target;
				if (target != null)
				{
					return target.CurrentCell;
				}
			}
			return null;
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x00162E7C File Offset: 0x0016107C
		public bool DoIHaveAMissileWeapon()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return false;
			}
			List<GameObject> missileWeapons = basisGameObject.GetMissileWeapons(null, null);
			if (missileWeapons != null && missileWeapons.Count > 0)
			{
				foreach (GameObject gameObject in missileWeapons)
				{
					MissileWeapon part = gameObject.GetPart<MissileWeapon>();
					if (part != null && part.ReadyToFire())
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x00162F00 File Offset: 0x00161100
		public static Dictionary<string, int> MapFromString(string raw)
		{
			string[] array = raw.Split(',', StringSplitOptions.None);
			Dictionary<string, int> dictionary = new Dictionary<string, int>(array.Length);
			foreach (string text in array)
			{
				string[] array3 = text.Split(':', StringSplitOptions.None);
				if (array3.Length != 2)
				{
					throw new Exception("bad element in string map: " + text);
				}
				string text2 = array3[0];
				int num = Convert.ToInt32(array3[1]);
				dictionary.Add(text2, num);
			}
			return dictionary;
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x00162F6C File Offset: 0x0016116C
		public bool LiquidAvailable(string LiquidID, int Amount = 1, bool impureOkay = true)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return basisGameObject != null && basisGameObject.GetFreeDrams(LiquidID, null, null, null, impureOkay) >= Amount;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x00162F98 File Offset: 0x00161198
		public bool ConsumeLiquid(string LiquidID, int Amount = 1, bool impureOkay = true)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return false;
			}
			if (impureOkay)
			{
				return basisGameObject.UseImpureDrams(Amount, LiquidID, null, null, null, null, false);
			}
			return basisGameObject.UseDrams(Amount, LiquidID, null, null, null, null, false);
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x00162FCF File Offset: 0x001611CF
		public static void EmitMessage(GameObject Source, string Msg, char Color = ' ', bool FromDialog = false, bool UsePopup = false, bool AlwaysVisible = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			Messaging.EmitMessage(Source, Msg, Color, FromDialog, UsePopup, AlwaysVisible, ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x00162FE2 File Offset: 0x001611E2
		public void EmitMessage(GameObject Source, StringBuilder Msg, char Color = ' ', bool FromDialog = false, bool UsePopup = false, bool AlwaysVisible = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			IComponent<T>.EmitMessage(Source, Msg.ToString(), Color, FromDialog, UsePopup, AlwaysVisible, ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x00162FFB File Offset: 0x001611FB
		public void EmitMessage(string Msg, char Color = ' ', bool FromDialog = false, bool UsePopup = false, bool AlwaysVisible = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			IComponent<T>.EmitMessage(this.GetBasisGameObject(), Msg, Color, FromDialog, UsePopup, AlwaysVisible, ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x00163013 File Offset: 0x00161213
		public void EmitMessage(StringBuilder Msg, char Color = ' ', bool FromDialog = false, bool UsePopup = false, bool AlwaysVisible = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			this.EmitMessage(Msg.ToString(), Color, FromDialog, UsePopup, AlwaysVisible, ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x0016302B File Offset: 0x0016122B
		protected static string ConsequentialColor(GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			return ColorCoding.ConsequentialColor(ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x00163034 File Offset: 0x00161234
		protected static char ConsequentialColorChar(GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			return ColorCoding.ConsequentialColorChar(ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0016303D File Offset: 0x0016123D
		protected static string ConsequentialColorize(string Text, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, char Default = ' ')
		{
			return ColorCoding.ConsequentialColorize(Text, ColorAsGoodFor, ColorAsBadFor, Default);
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x00163048 File Offset: 0x00161248
		public static void XDidY(GameObject Actor, string Verb, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, GameObject SubjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidY(Actor, Verb, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, SubjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x0016307C File Offset: 0x0016127C
		public static void XDidY(string SubjectOverride, string Verb, string Extra = null, string EndMark = null, GameObject Actor = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, GameObject SubjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidY(Actor, Verb, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, SubjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x001630B0 File Offset: 0x001612B0
		public void DidX(string Verb, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, GameObject SubjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidY(this.GetBasisGameObject(), Verb, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, SubjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x001630E8 File Offset: 0x001612E8
		public static void XDidYToZ(GameObject Actor, string Verb, string Preposition, GameObject Object, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteObject = false, bool IndefiniteObjectForOthers = false, bool PossessiveObject = false, GameObject SubjectPossessedBy = null, GameObject ObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidYToZ(Actor, Verb, Preposition, Object, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteObject, IndefiniteObjectForOthers, PossessiveObject, SubjectPossessedBy, ObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x00163128 File Offset: 0x00161328
		public static void XDidYToZ(string SubjectOverride, string Verb, string Preposition, GameObject Object, string Extra = null, string EndMark = null, GameObject Actor = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteObject = false, bool IndefiniteObjectForOthers = false, bool PossessiveObject = false, GameObject SubjectPossessedBy = null, GameObject ObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidYToZ(Actor, Verb, Preposition, Object, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteObject, IndefiniteObjectForOthers, PossessiveObject, SubjectPossessedBy, ObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x00163168 File Offset: 0x00161368
		public static void XDidYToZ(GameObject Actor, string Verb, GameObject Object, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteObject = false, bool IndefiniteObjectForOthers = false, bool PossessiveObject = false, GameObject SubjectPossessedBy = null, GameObject ObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidYToZ(Actor, Verb, null, Object, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteObject, IndefiniteObjectForOthers, PossessiveObject, SubjectPossessedBy, ObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x001631A8 File Offset: 0x001613A8
		public static void XDidYToZ(string SubjectOverride, string Verb, GameObject Object, string Extra = null, string EndMark = null, GameObject Actor = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteObject = false, bool IndefiniteObjectForOthers = false, bool PossessiveObject = false, GameObject SubjectPossessedBy = null, GameObject ObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidYToZ(Actor, Verb, null, Object, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteObject, IndefiniteObjectForOthers, PossessiveObject, SubjectPossessedBy, ObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x001631E8 File Offset: 0x001613E8
		public void DidXToY(string Verb, string Preposition, GameObject Object, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteObject = false, bool IndefiniteObjectForOthers = false, bool PossessiveObject = false, GameObject SubjectPossessedBy = null, GameObject ObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidYToZ(this.GetBasisGameObject(), Verb, Preposition, Object, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteObject, IndefiniteObjectForOthers, PossessiveObject, SubjectPossessedBy, ObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x0016322C File Offset: 0x0016142C
		public void DidXToY(string Verb, GameObject Object, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteObject = false, bool IndefiniteObjectForOthers = false, bool PossessiveObject = false, GameObject SubjectPossessedBy = null, GameObject ObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.XDidYToZ(this.GetBasisGameObject(), Verb, null, Object, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteObject, IndefiniteObjectForOthers, PossessiveObject, SubjectPossessedBy, ObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x00163270 File Offset: 0x00161470
		public static void WDidXToYWithZ(GameObject Actor, string Verb, string DirectPreposition, GameObject DirectObject, string IndirectPreposition, GameObject IndirectObject, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteDirectObject = false, bool IndefiniteIndirectObject = false, bool IndefiniteDirectObjectForOthers = false, bool IndefiniteIndirectObjectForOthers = false, bool PossessiveDirectObject = false, bool PossessiveIndirectObject = false, GameObject SubjectPossessedBy = null, GameObject DirectObjectPossessedBy = null, GameObject IndirectObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.WDidXToYWithZ(Actor, Verb, DirectPreposition, DirectObject, IndirectPreposition, IndirectObject, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteDirectObject, IndefiniteIndirectObject, IndefiniteDirectObjectForOthers, IndefiniteIndirectObjectForOthers, PossessiveDirectObject, PossessiveIndirectObject, SubjectPossessedBy, DirectObjectPossessedBy, IndirectObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x001632BC File Offset: 0x001614BC
		public static void WDidXToYWithZ(string SubjectOverride, string Verb, string DirectPreposition, GameObject DirectObject, string IndirectPreposition, GameObject IndirectObject, string Extra = null, string EndMark = null, GameObject Actor = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteDirectObject = false, bool IndefiniteIndirectObject = false, bool IndefiniteDirectObjectForOthers = false, bool IndefiniteIndirectObjectForOthers = false, bool PossessiveDirectObject = false, bool PossessiveIndirectObject = false, GameObject SubjectPossessedBy = null, GameObject DirectObjectPossessedBy = null, GameObject IndirectObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.WDidXToYWithZ(Actor, Verb, DirectPreposition, DirectObject, IndirectPreposition, IndirectObject, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteDirectObject, IndefiniteIndirectObject, IndefiniteDirectObjectForOthers, IndefiniteIndirectObjectForOthers, PossessiveDirectObject, PossessiveIndirectObject, SubjectPossessedBy, DirectObjectPossessedBy, IndirectObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x00163308 File Offset: 0x00161508
		public static void WDidXToYWithZ(GameObject Actor, string Verb, GameObject DirectObject, string IndirectPreposition, GameObject IndirectObject, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteDirectObject = false, bool IndefiniteIndirectObject = false, bool IndefiniteDirectObjectForOthers = false, bool IndefiniteIndirectObjectForOthers = false, bool PossessiveDirectObject = false, bool PossessiveIndirectObject = false, GameObject SubjectPossessedBy = null, GameObject DirectObjectPossessedBy = null, GameObject IndirectObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.WDidXToYWithZ(Actor, Verb, null, DirectObject, IndirectPreposition, IndirectObject, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteDirectObject, IndefiniteIndirectObject, IndefiniteDirectObjectForOthers, IndefiniteIndirectObjectForOthers, PossessiveDirectObject, PossessiveIndirectObject, SubjectPossessedBy, DirectObjectPossessedBy, IndirectObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x00163354 File Offset: 0x00161554
		public static void WDidXToYWithZ(string SubjectOverride, string Verb, GameObject DirectObject, string IndirectPreposition, GameObject IndirectObject, string Extra = null, string EndMark = null, GameObject Actor = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteDirectObject = false, bool IndefiniteIndirectObject = false, bool IndefiniteDirectObjectForOthers = false, bool IndefiniteIndirectObjectForOthers = false, bool PossessiveDirectObject = false, bool PossessiveIndirectObject = false, GameObject SubjectPossessedBy = null, GameObject DirectObjectPossessedBy = null, GameObject IndirectObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.WDidXToYWithZ(Actor, Verb, null, DirectObject, IndirectPreposition, IndirectObject, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteDirectObject, IndefiniteIndirectObject, IndefiniteDirectObjectForOthers, IndefiniteIndirectObjectForOthers, PossessiveDirectObject, PossessiveIndirectObject, SubjectPossessedBy, DirectObjectPossessedBy, IndirectObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x001633A0 File Offset: 0x001615A0
		public void DidXToYWithZ(string Verb, string DirectPreposition, GameObject DirectObject, string IndirectPreposition, GameObject IndirectObject, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteDirectObject = false, bool IndefiniteIndirectObject = false, bool IndefiniteDirectObjectForOthers = false, bool IndefiniteIndirectObjectForOthers = false, bool PossessiveDirectObject = false, bool PossessiveIndirectObject = false, GameObject SubjectPossessedBy = null, GameObject DirectObjectPossessedBy = null, GameObject IndirectObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.WDidXToYWithZ(this.GetBasisGameObject(), Verb, DirectPreposition, DirectObject, IndirectPreposition, IndirectObject, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteDirectObject, IndefiniteIndirectObject, IndefiniteDirectObjectForOthers, IndefiniteIndirectObjectForOthers, PossessiveDirectObject, PossessiveIndirectObject, SubjectPossessedBy, DirectObjectPossessedBy, IndirectObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x001633F0 File Offset: 0x001615F0
		public void DidXToYWithZ(string Verb, GameObject DirectObject, string IndirectPreposition, GameObject IndirectObject, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteDirectObject = false, bool IndefiniteIndirectObject = false, bool IndefiniteDirectObjectForOthers = false, bool IndefiniteIndirectObjectForOthers = false, bool PossessiveDirectObject = false, bool PossessiveIndirectObject = false, GameObject SubjectPossessedBy = null, GameObject DirectObjectPossessedBy = null, GameObject IndirectObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			Messaging.WDidXToYWithZ(this.GetBasisGameObject(), Verb, null, DirectObject, IndirectPreposition, IndirectObject, Extra, EndMark, SubjectOverride, Color, ColorAsGoodFor, ColorAsBadFor, UseFullNames, IndefiniteSubject, IndefiniteDirectObject, IndefiniteIndirectObject, IndefiniteDirectObjectForOthers, IndefiniteIndirectObjectForOthers, PossessiveDirectObject, PossessiveIndirectObject, SubjectPossessedBy, DirectObjectPossessedBy, IndirectObjectPossessedBy, Source, DescribeSubjectDirection, DescribeSubjectDirectionLate, AlwaysVisible, FromDialog, UsePopup, UseVisibilityOf);
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x0016343E File Offset: 0x0016163E
		public bool IsBroken()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return basisGameObject != null && basisGameObject.IsBroken();
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x00163451 File Offset: 0x00161651
		public bool IsRusted()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return basisGameObject != null && basisGameObject.IsRusted();
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x00163464 File Offset: 0x00161664
		public bool IsEMPed()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return basisGameObject != null && basisGameObject.IsEMPed();
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x00163477 File Offset: 0x00161677
		public static bool Visible(GameObject Object)
		{
			return Object != null && Object.IsVisible();
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x00163484 File Offset: 0x00161684
		public bool Visible()
		{
			return IComponent<T>.Visible(this.GetBasisGameObject());
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x00163491 File Offset: 0x00161691
		[JsonIgnore]
		public ActivatedAbilities MyActivatedAbilities
		{
			get
			{
				GameObject basisGameObject = this.GetBasisGameObject();
				if (basisGameObject == null)
				{
					return null;
				}
				return basisGameObject.ActivatedAbilities;
			}
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x001634A4 File Offset: 0x001616A4
		public ActivatedAbilities GetMyActivatedAbilities(GameObject who = null)
		{
			GameObject gameObject = who ?? this.GetBasisGameObject();
			if (gameObject == null)
			{
				return null;
			}
			return gameObject.ActivatedAbilities;
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x001634BC File Offset: 0x001616BC
		public ActivatedAbilityEntry MyActivatedAbility(Guid ID, GameObject who = null)
		{
			GameObject gameObject = who ?? this.GetBasisGameObject();
			if (gameObject == null)
			{
				return null;
			}
			return gameObject.GetActivatedAbility(ID);
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x001634D8 File Offset: 0x001616D8
		public Guid AddMyActivatedAbility(string Name, string Command, string Class, string Description = null, string Icon = "\a", string DisabledMessage = null, bool Toggleable = false, bool DefaultToggleState = false, bool ActiveToggle = false, bool IsAttack = false, bool IsRealityDistortionBased = false, bool IsWorldMapUsable = false, bool Silent = false, bool AIDisable = false, bool AlwaysAllowToggleOff = true, bool AffectedByWillpower = true, bool TickPerTurn = false, int Cooldown = -1, GameObject who = null, string CommandForDescription = null, Renderable UITileDefault = null, Renderable UITileToggleOn = null, Renderable UITileDisabled = null, Renderable UITileCoolingDown = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return Guid.Empty;
				}
			}
			return who.AddActivatedAbility(Name, Command, Class, Description, Icon, DisabledMessage, Toggleable, DefaultToggleState, ActiveToggle, IsAttack, IsRealityDistortionBased, IsWorldMapUsable, Silent, AIDisable, AlwaysAllowToggleOff, AffectedByWillpower, TickPerTurn, false, Cooldown, CommandForDescription, UITileDefault, UITileToggleOn, UITileDisabled, UITileCoolingDown);
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x00163530 File Offset: 0x00161730
		public bool RemoveMyActivatedAbility(ref Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.RemoveActivatedAbility(ref ID);
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x00163549 File Offset: 0x00161749
		public bool DescribeMyActivatedAbility(Guid ID, Action<Templates.StatCollector> statCollector, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.DescribeActivatedAbility(ID, statCollector);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x00163563 File Offset: 0x00161763
		public bool DescribeMyActivatedAbility(Guid ID, Templates.StatCollector values, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.DescribeActivatedAbility(ID, values);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x0016357D File Offset: 0x0016177D
		public bool EnableMyActivatedAbility(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.EnableActivatedAbility(ID);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x00163596 File Offset: 0x00161796
		public bool DisableMyActivatedAbility(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.DisableActivatedAbility(ID);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x001635AF File Offset: 0x001617AF
		public bool ToggleMyActivatedAbility(Guid ID, GameObject who = null, bool Silent = false, bool? SetState = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.ToggleActivatedAbility(ID, Silent, SetState);
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x001635CB File Offset: 0x001617CB
		public bool IsMyActivatedAbilityToggledOn(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.IsActivatedAbilityToggledOn(ID);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x001635E4 File Offset: 0x001617E4
		public bool IsMyActivatedAbilityCoolingDown(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.IsActivatedAbilityCoolingDown(ID);
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x001635FD File Offset: 0x001617FD
		public int GetMyActivatedAbilityCooldown(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return -1;
				}
			}
			return who.GetActivatedAbilityCooldown(ID);
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x00163616 File Offset: 0x00161816
		public int GetMyActivatedAbilityCooldownTurns(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return -1;
				}
			}
			return who.GetActivatedAbilityCooldownRounds(ID);
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x0016362F File Offset: 0x0016182F
		public string GetMyActivatedAbilityCooldownDescription(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return "";
				}
			}
			return who.GetActivatedAbilityCooldownDescription(ID);
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x0016364C File Offset: 0x0016184C
		public bool CooldownMyActivatedAbility(Guid ID, int Turns, GameObject who = null, string tags = null, bool Involuntary = false)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.CooldownActivatedAbility(ID, Turns, tags, Involuntary);
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x0016366A File Offset: 0x0016186A
		public bool TakeMyActivatedAbilityOffCooldown(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.TakeActivatedAbilityOffCooldown(ID);
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x00163683 File Offset: 0x00161883
		public bool IsMyActivatedAbilityUsable(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.IsActivatedAbilityUsable(ID);
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x0016369C File Offset: 0x0016189C
		public bool IsMyActivatedAbilityAIUsable(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.IsActivatedAbilityAIUsable(ID);
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x001636B5 File Offset: 0x001618B5
		public bool IsMyActivatedAbilityAIDisabled(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.IsActivatedAbilityAIDisabled(ID);
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x001636CE File Offset: 0x001618CE
		public bool IsMyActivatedAbilityVoluntarilyUsable(Guid ID, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			if (!who.IsPlayer())
			{
				return this.IsMyActivatedAbilityAIUsable(ID, null);
			}
			return this.IsMyActivatedAbilityUsable(ID, null);
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x001636F9 File Offset: 0x001618F9
		public bool SetMyActivatedAbilityDisplayName(Guid ID, string DisplayName, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.SetActivatedAbilityDisplayName(ID, DisplayName);
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x00163713 File Offset: 0x00161913
		public bool SetMyActivatedAbilityDisabledMessage(Guid ID, string DisabledMessage, GameObject who = null)
		{
			if (who == null)
			{
				who = this.GetBasisGameObject();
				if (who == null)
				{
					return false;
				}
			}
			return who.SetActivatedAbilityDisabledMessage(ID, DisabledMessage);
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x00163730 File Offset: 0x00161930
		public void FlushWeightCaches()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject != null)
			{
				basisGameObject.FlushWeightCaches();
				basisGameObject.FlushContextWeightCaches();
			}
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x00163753 File Offset: 0x00161953
		public void FlushNavigationCaches()
		{
			Cell anyBasisCell = this.GetAnyBasisCell();
			if (anyBasisCell == null)
			{
				return;
			}
			anyBasisCell.FlushNavigationCache();
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x00163768 File Offset: 0x00161968
		public void FlushLocalNavigationCaches()
		{
			Cell anyBasisCell = this.GetAnyBasisCell();
			if (anyBasisCell != null)
			{
				anyBasisCell.FlushNavigationCache();
				foreach (Cell cell in anyBasisCell.GetLocalAdjacentCells())
				{
					cell.FlushNavigationCache();
				}
			}
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x001637C8 File Offset: 0x001619C8
		public void FlushZoneNavigationCaches()
		{
			Zone anyBasisZone = this.GetAnyBasisZone();
			if (anyBasisZone == null)
			{
				return;
			}
			anyBasisZone.FlushNavigationCaches();
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x001637DA File Offset: 0x001619DA
		public void FlushTransientCaches()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return;
			}
			basisGameObject.FlushTransientCache();
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x001637EC File Offset: 0x001619EC
		public int StepValue(int Value, int Step = 5)
		{
			int num = Value % Step;
			if (num == 0)
			{
				return Value;
			}
			if (num >= Step / 2)
			{
				return Value - num + Step;
			}
			return Value - num;
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x00163811 File Offset: 0x00161A11
		public virtual int GetEpistemicStatus()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return 2;
			}
			return basisGameObject.GetEpistemicStatus();
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x00163824 File Offset: 0x00161A24
		public void AddLight(int r, LightLevel Level = LightLevel.Light, bool Force = false)
		{
			Cell basisCell = this.GetBasisCell();
			if (basisCell != null)
			{
				Zone parentZone = basisCell.ParentZone;
				if (parentZone == null)
				{
					return;
				}
				parentZone.AddLight(basisCell.X, basisCell.Y, r, Level, Force);
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x0016385A File Offset: 0x00161A5A
		[JsonIgnore]
		public bool OnWorldMap
		{
			get
			{
				Cell anyBasisCell = this.GetAnyBasisCell();
				return anyBasisCell != null && anyBasisCell.OnWorldMap();
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x0016386D File Offset: 0x00161A6D
		[JsonIgnore]
		public bool IsWorldMapActive
		{
			get
			{
				Zone activeZone = The.ActiveZone;
				return activeZone != null && activeZone.IsWorldMap();
			}
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x0016387F File Offset: 0x00161A7F
		public static int PowerLoadBonus(int Load, int Baseline = 100, int Divisor = 150)
		{
			if (Load > Baseline)
			{
				return (Load - Baseline) / Divisor;
			}
			return 0;
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x0016388C File Offset: 0x00161A8C
		public virtual int MyPowerLoadBonus(int Load = -2147483648, int Baseline = 100, int Divisor = 150)
		{
			if (Load == -2147483648)
			{
				GameObject basisGameObject = this.GetBasisGameObject();
				Load = ((basisGameObject != null) ? basisGameObject.GetPowerLoadLevel() : 100);
			}
			return IComponent<T>.PowerLoadBonus(Load, Baseline, Divisor);
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x001638B3 File Offset: 0x00161AB3
		public virtual int MyPowerLoadLevel()
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			if (basisGameObject == null)
			{
				return 100;
			}
			return basisGameObject.GetPowerLoadLevel();
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x001638C8 File Offset: 0x00161AC8
		public bool PerformMentalAttack(Mental.Attack Handler, GameObject Attacker, GameObject Defender, GameObject Source = null, string Command = null, string Dice = null, int Type = 0, int Magnitude = -2147483648, int Penetrations = -2147483648, int AttackModifier = 0, int DefenseModifier = 0)
		{
			return Mental.PerformAttack(Handler, Attacker, Defender, Source ?? this.GetBasisGameObject(), Command, Dice, Type, Magnitude, Penetrations, AttackModifier, DefenseModifier);
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x001638F7 File Offset: 0x00161AF7
		public static void ConstrainTier(ref int tier)
		{
			Tier.Constrain(ref tier);
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x001638FF File Offset: 0x00161AFF
		public static int ConstrainTier(int tier)
		{
			return Tier.Constrain(tier);
		}

		/// <remarks>
		/// The result of this is cached per <see cref="T:XRL.World.GameObject" /> until transient caches are flushed.
		/// Prefer returning a value unlikely to change during the lifetime of the component. 
		/// </remarks>
		// Token: 0x06004562 RID: 17762 RVA: 0x00163907 File Offset: 0x00161B07
		public virtual bool WantTurnTick()
		{
			return false;
		}

		/// <summary>Called when in-game time passes, either from turns passing or world map travel.</summary>
		/// <param name="TimeTick">The time tick of the game world, i.e. the value of <see cref="F:XRL.XRLGame.TimeTicks" />.</param>
		/// <param name="Amount">The interval in turns from the last call to TurnTick.</param>
		// Token: 0x06004563 RID: 17763 RVA: 0x0016390A File Offset: 0x00161B0A
		public virtual void TurnTick(long TimeTick, int Amount)
		{
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x0016390C File Offset: 0x00161B0C
		[Obsolete("Use WantTurnTick")]
		public virtual bool WantTenTurnTick()
		{
			return false;
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x0016390F File Offset: 0x00161B0F
		[Obsolete("Use TurnTick(long TimeTick, int Amount)")]
		public virtual void TenTurnTick(long TurnNumber)
		{
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x00163911 File Offset: 0x00161B11
		[Obsolete("Use WantTurnTick")]
		public virtual bool WantHundredTurnTick()
		{
			return false;
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x00163914 File Offset: 0x00161B14
		[Obsolete("Use TurnTick(long TimeTick, int Amount)")]
		public virtual void HundredTurnTick(long TurnNumber)
		{
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x00163916 File Offset: 0x00161B16
		public virtual bool WantEvent(int ID, int Cascade)
		{
			return false;
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x00163919 File Offset: 0x00161B19
		public virtual bool HandleEvent(MinEvent E)
		{
			return true;
		}

		/// <inheritdoc cref="M:XRL.Collections.EventRegistry.Register(XRL.IEventHandler,System.Int32,System.Int32,System.Boolean)" />
		// Token: 0x0600456A RID: 17770 RVA: 0x0016391C File Offset: 0x00161B1C
		public virtual void RegisterEvent(int EventID, int Order = 0, bool Serialize = false)
		{
		}

		/// <inheritdoc cref="M:XRL.Collections.EventRegistry.Unregister(XRL.IEventHandler,System.Int32)" />
		// Token: 0x0600456B RID: 17771 RVA: 0x0016391E File Offset: 0x00161B1E
		public virtual void UnregisterEvent(int EventID)
		{
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x00163920 File Offset: 0x00161B20
		public virtual bool Render(RenderEvent E)
		{
			return true;
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x00163923 File Offset: 0x00161B23
		public virtual bool OverlayRender(RenderEvent E)
		{
			return true;
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x00163926 File Offset: 0x00161B26
		public virtual bool FinalRender(RenderEvent E)
		{
			return true;
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x00163929 File Offset: 0x00161B29
		[Obsolete("Use FinalRender(RenderEvent)")]
		public virtual bool FinalRender(RenderEvent E, bool bAlt)
		{
			return true;
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x0016392C File Offset: 0x00161B2C
		[Obsolete("Use ISoundRenderer, SoundRendererPart, or SoundRendererEffect")]
		public virtual bool RenderSound(ConsoleChar C, ConsoleChar[,] Buffer)
		{
			return true;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x0016392F File Offset: 0x00161B2F
		public static bool CheckRealityDistortionAdvisability(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int? Threshold = null, int Penetration = 0)
		{
			return CheckRealityDistortionAdvisabilityEvent.Check(Object, Cell, Actor, Device, Mutation, Threshold, Penetration);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x00163940 File Offset: 0x00161B40
		public bool CheckMyRealityDistortionAdvisability(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int? Threshold = null, int Penetration = 0)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return IComponent<T>.CheckRealityDistortionAdvisability(Object ?? basisGameObject, Cell, Actor ?? basisGameObject, Device, Mutation ?? (this as IPart), Threshold, Penetration);
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x00163978 File Offset: 0x00161B78
		public static bool CheckRealityDistortionUsability(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int? Threshold = null, int Penetration = 0)
		{
			return CheckRealityDistortionUsabilityEvent.Check(Object, Cell, Actor, Device, Mutation, Threshold, Penetration);
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x0016398C File Offset: 0x00161B8C
		public bool CheckMyRealityDistortionUsability(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int? Threshold = null, int Penetration = 0)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return IComponent<T>.CheckRealityDistortionUsability(Object ?? basisGameObject, Cell, Actor ?? basisGameObject, Device, Mutation ?? (this as IPart), Threshold, Penetration);
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x001639C4 File Offset: 0x00161BC4
		public static bool CheckRealityDistortionAccessibility(GameObject Object)
		{
			return CheckRealityDistortionAccessibilityEvent.Check(Object, null, null, null, null, 0);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x001639D1 File Offset: 0x00161BD1
		public static bool CheckRealityDistortionAccessibility(Cell Cell)
		{
			return CheckRealityDistortionAccessibilityEvent.Check(null, Cell, null, null, null, 0);
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x001639DE File Offset: 0x00161BDE
		public static bool CheckRealityDistortionAccessibility(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int Penetration = 0)
		{
			return CheckRealityDistortionAccessibilityEvent.Check(Object, Cell, Actor, Device, Mutation, Penetration);
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x001639F0 File Offset: 0x00161BF0
		public bool CheckMyRealityDistortionAccessibility(GameObject Object = null, Cell Cell = null, GameObject Actor = null, GameObject Device = null, IPart Mutation = null, int Penetration = 0)
		{
			GameObject basisGameObject = this.GetBasisGameObject();
			return IComponent<T>.CheckRealityDistortionAccessibility(Object ?? basisGameObject, Cell, Actor ?? basisGameObject, Device, Mutation ?? (this as IPart), Penetration);
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x00163A28 File Offset: 0x00161C28
		[JsonIgnore]
		public virtual string DebugName
		{
			get
			{
				GameObject basisGameObject = this.GetBasisGameObject();
				if (basisGameObject != null)
				{
					return basisGameObject.DebugName + ":" + base.GetType().Name;
				}
				return base.GetType().Name;
			}
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x00163A68 File Offset: 0x00161C68
		public virtual void Write(T Basis, SerializationWriter Writer)
		{
			foreach (FieldInfo fieldInfo in base.GetType().GetCachedFields())
			{
				if ((fieldInfo.Attributes & (FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)) == FieldAttributes.PrivateScope)
				{
					object value = fieldInfo.GetValue(this);
					Type fieldType = fieldInfo.FieldType;
					if (fieldType == typeof(Cell))
					{
						Writer.Write((Cell)value);
					}
					else if (fieldType == typeof(ActivatedAbilityEntry))
					{
						Writer.Write((value == null) ? Guid.Empty : ((ActivatedAbilityEntry)value).ID);
					}
					else if (fieldType == typeof(GameObject))
					{
						Writer.WriteGameObject((GameObject)value);
					}
					else
					{
						Writer.WriteObject(value);
					}
				}
			}
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x00163B34 File Offset: 0x00161D34
		public virtual void Read(T Basis, SerializationReader Reader)
		{
			FieldInfo fieldInfo = null;
			Type type = null;
			Type type2 = base.GetType();
			try
			{
				foreach (FieldInfo fieldInfo2 in type2.GetCachedFields())
				{
					if ((fieldInfo2.Attributes & (FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)) == FieldAttributes.PrivateScope)
					{
						fieldInfo = fieldInfo2;
						type = fieldInfo2.FieldType;
						FieldSaveVersion fieldSaveVersion;
						if (!FastSerialization.FieldSaveVersionInfo.TryGetValue(fieldInfo2, out fieldSaveVersion) || fieldSaveVersion.minimumSaveVersion <= Reader.FileVersion)
						{
							if (type == typeof(Cell))
							{
								fieldInfo2.SetValue(this, Reader.ReadCell());
							}
							else if (type == typeof(ActivatedAbilityEntry))
							{
								Guid guid = Reader.ReadGuid();
								if (guid != Guid.Empty)
								{
									GameObject gameObject = Basis as GameObject;
									ActivatedAbilities activatedAbilities;
									ActivatedAbilityEntry activatedAbilityEntry;
									if (gameObject != null && gameObject.TryGetPart<ActivatedAbilities>(out activatedAbilities) && activatedAbilities.AbilityByGuid.TryGetValue(guid, out activatedAbilityEntry))
									{
										fieldInfo2.SetValue(this, activatedAbilityEntry);
									}
								}
							}
							else if (type == typeof(GameObject))
							{
								fieldInfo2.SetValue(this, Reader.ReadGameObject(null));
							}
							else
							{
								fieldInfo2.SetValue(this, Reader.ReadObject());
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (type2 != null)
				{
					ex.Data["Type"] = type2;
				}
				if (fieldInfo != null)
				{
					ex.Data["Field"] = fieldInfo;
				}
				if (type != null)
				{
					ex.Data["FieldType"] = type;
				}
				throw;
			}
		}

		/// <summary>Called if there was an error reading this component.</summary>
		/// <returns><c>true</c> if the error was handled; <c>false</c> to skip the rest of this component.</returns>
		// Token: 0x0600457C RID: 17788 RVA: 0x00163CCC File Offset: 0x00161ECC
		public virtual bool ReadError(Exception Exception, SerializationReader Reader, long Start, int Length)
		{
			return false;
		}

		/// <summary>Called if there was at least one unhandled error reading this component's basis.</summary>
		// Token: 0x0600457D RID: 17789 RVA: 0x00163CCF File Offset: 0x00161ECF
		public virtual void BasisError(T Basis, SerializationReader Reader)
		{
		}

		// Token: 0x040020FD RID: 8445
		public static readonly int TurnTickID = MinEvent.RegisterEvent(typeof(IComponent<T>), "TurnTickEvent", null, null);
	}
}
