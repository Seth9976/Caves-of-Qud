using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace XRL.CharacterBuilds
{
	// Token: 0x0200058A RID: 1418
	[HasModSensitiveStaticCache]
	public static class EmbarkBuilderConfiguration
	{
		// Token: 0x06003D05 RID: 15621 RVA: 0x0012B938 File Offset: 0x00129B38
		public static void Init()
		{
			EmbarkBuilderConfiguration.activeModules = new List<AbstractEmbarkBuilderModule>();
			EmbarkBuilderConfiguration.modules = new Dictionary<string, AbstractEmbarkBuilderModule>();
			Dictionary<string, Action<XmlDataHelper>> handlers = null;
			handlers = new Dictionary<string, Action<XmlDataHelper>>
			{
				{
					"embarkmodules",
					delegate(XmlDataHelper xml)
					{
						xml.HandleNodes(handlers, null);
					}
				},
				{
					"module",
					new Action<XmlDataHelper>(EmbarkBuilderConfiguration.<Init>g__HandleModule|2_0)
				}
			};
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("EmbarkModules", true, true))
			{
				xmlDataHelper.HandleNodes(handlers, null);
			}
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x0012B9EC File Offset: 0x00129BEC
		[CompilerGenerated]
		internal static void <Init>g__HandleModule|2_0(XmlDataHelper xml)
		{
			string attribute = xml.GetAttribute("Class");
			ModManager.ResolveType(attribute, false, false, true);
			AbstractEmbarkBuilderModule abstractEmbarkBuilderModule = null;
			if (!EmbarkBuilderConfiguration.modules.TryGetValue(attribute, out abstractEmbarkBuilderModule))
			{
				abstractEmbarkBuilderModule = Activator.CreateInstance(ModManager.ResolveType(attribute, false, false, true)) as AbstractEmbarkBuilderModule;
				EmbarkBuilderConfiguration.modules.Add(attribute, abstractEmbarkBuilderModule);
				EmbarkBuilderConfiguration.activeModules.Add(abstractEmbarkBuilderModule);
			}
			if (abstractEmbarkBuilderModule == null)
			{
				MetricsManager.LogError("Unknown embark builder type: " + attribute);
				xml.DoneWithElement();
				return;
			}
			abstractEmbarkBuilderModule.HandleNodes(xml);
		}

		// Token: 0x04001E60 RID: 7776
		public static List<AbstractEmbarkBuilderModule> activeModules;

		// Token: 0x04001E61 RID: 7777
		public static Dictionary<string, AbstractEmbarkBuilderModule> modules;
	}
}
