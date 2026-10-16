using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.World.WorldBuilders;

namespace XRL.World
{
	// Token: 0x0200092B RID: 2347
	[Serializable]
	public abstract class DynamicQuestContext : IComposite
	{
		// Token: 0x06005A11 RID: 23057
		public abstract HistoricEntity originEntity();

		// Token: 0x06005A12 RID: 23058
		public abstract string originZoneId();

		// Token: 0x06005A13 RID: 23059
		public abstract string getQuestOriginZone();

		// Token: 0x06005A14 RID: 23060
		public abstract string questTargetZone(int minDistance, int maxDistance);

		// Token: 0x06005A15 RID: 23061
		public abstract GeneratedLocationInfo getNearbyUndiscoveredLocation();

		// Token: 0x06005A16 RID: 23062
		public abstract DynamicQuestReward getQuestReward();

		// Token: 0x06005A17 RID: 23063
		public abstract string getQuestGiverZone();

		// Token: 0x06005A18 RID: 23064
		public abstract Func<GameObject, bool> getQuestGiverFilter();

		// Token: 0x06005A19 RID: 23065
		public abstract Func<GameObject, bool> getQuestActorFilter();

		// Token: 0x06005A1A RID: 23066
		public abstract List<string> getSacredThings();

		// Token: 0x06005A1B RID: 23067
		public abstract DynamicQuestDeliveryTarget getQuestDeliveryTarget();

		// Token: 0x06005A1C RID: 23068
		public abstract GameObject getQuestRemoteInteractable();

		// Token: 0x06005A1D RID: 23069
		public abstract GameObject getQuestGenericRemoteInteractable();

		// Token: 0x06005A1E RID: 23070
		public abstract GameObject getQuestDeliveryItem();

		// Token: 0x06005A1F RID: 23071
		public abstract string getQuestItemNameMutation(string input);

		// Token: 0x06005A20 RID: 23072
		public abstract string assassinationTargetId();

		// Token: 0x06005A21 RID: 23073
		public abstract GameObject getALostItem();

		// Token: 0x06005A22 RID: 23074 RVA: 0x001B4C4C File Offset: 0x001B2E4C
		public virtual void Write(SerializationWriter Writer)
		{
		}

		// Token: 0x06005A23 RID: 23075 RVA: 0x001B4C4E File Offset: 0x001B2E4E
		public virtual void Read(SerializationReader Reader)
		{
		}

		// Token: 0x04002C99 RID: 11417
		public int questNumber;

		// Token: 0x04002C9A RID: 11418
		public int questChainNumber;

		// Token: 0x04002C9B RID: 11419
		public int tier = 1;
	}
}
