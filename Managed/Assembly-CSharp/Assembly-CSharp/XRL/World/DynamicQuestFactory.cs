using System;

namespace XRL.World
{
	// Token: 0x0200092C RID: 2348
	public static class DynamicQuestFactory
	{
		// Token: 0x06005A25 RID: 23077 RVA: 0x001B4C5F File Offset: 0x001B2E5F
		public static BaseDynamicQuestTemplate fabricateQuestTemplate(string type, DynamicQuestContext context)
		{
			Type type2 = ModManager.ResolveType(type, false, false, true);
			if (type2 == null)
			{
				throw new ArgumentException("couldn't resolve quest type " + type);
			}
			return DynamicQuestFactory.fabricateQuestTemplate(type2, context);
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x001B4C8C File Offset: 0x001B2E8C
		public static BaseDynamicQuestTemplate fabricateQuestTemplate(Type type, DynamicQuestContext context)
		{
			BaseDynamicQuestTemplate baseDynamicQuestTemplate = Activator.CreateInstance(type) as BaseDynamicQuestTemplate;
			if (baseDynamicQuestTemplate == null)
			{
				throw new ArgumentException("type should be a BaseDynamicQuestTemplate derived type");
			}
			DynamicQuestsGameState instance = DynamicQuestsGameState.Instance;
			long nextID = instance.NextID;
			instance.NextID = nextID + 1L;
			baseDynamicQuestTemplate.id = nextID;
			baseDynamicQuestTemplate.init(context);
			return baseDynamicQuestTemplate;
		}
	}
}
