using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Conditions;
using AiUnity.NLog.Core.Filters;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.LayoutRenderers;
using AiUnity.NLog.Core.Layouts;
using AiUnity.NLog.Core.Targets;
using AiUnity.NLog.Core.Time;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x0200028B RID: 651
	public class ConfigurationItemFactory
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x0007A0EB File Offset: 0x000782EB
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0007A100 File Offset: 0x00078300
		public ConfigurationItemFactory(params Assembly[] assemblies)
		{
			this.CreateInstance = new ConfigurationItemCreator(FactoryHelper.CreateInstance);
			this.targets = new Factory<Target, TargetAttribute>(this);
			this.filters = new Factory<Filter, FilterAttribute>(this);
			this.layoutRenderers = new Factory<LayoutRenderer, LayoutRendererAttribute>(this);
			this.layouts = new Factory<Layout, LayoutAttribute>(this);
			this.conditionMethods = new MethodFactory<ConditionMethodsAttribute, ConditionMethodAttribute>();
			this.ambientProperties = new Factory<LayoutRenderer, AmbientPropertyAttribute>(this);
			this.timeSources = new Factory<TimeSource, TimeSourceAttribute>(this);
			this.allFactories = new List<object> { this.targets, this.filters, this.layoutRenderers, this.layouts, this.conditionMethods, this.ambientProperties, this.timeSources };
			foreach (Assembly assembly in assemblies)
			{
				this.RegisterItemsFromAssembly(assembly);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0007A1F2 File Offset: 0x000783F2
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0007A1F9 File Offset: 0x000783F9
		public static ConfigurationItemFactory Default { get; set; } = ConfigurationItemFactory.BuildDefaultFactory();

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0007A201 File Offset: 0x00078401
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x0007A209 File Offset: 0x00078409
		public ConfigurationItemCreator CreateInstance { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0007A212 File Offset: 0x00078412
		public INamedItemFactory<Target, Type> Targets
		{
			get
			{
				return this.targets;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0007A21A File Offset: 0x0007841A
		public INamedItemFactory<Filter, Type> Filters
		{
			get
			{
				return this.filters;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0007A222 File Offset: 0x00078422
		public INamedItemFactory<LayoutRenderer, Type> LayoutRenderers
		{
			get
			{
				return this.layoutRenderers;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0007A22A File Offset: 0x0007842A
		public INamedItemFactory<Layout, Type> Layouts
		{
			get
			{
				return this.layouts;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0007A232 File Offset: 0x00078432
		public INamedItemFactory<LayoutRenderer, Type> AmbientProperties
		{
			get
			{
				return this.ambientProperties;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0007A23A File Offset: 0x0007843A
		public INamedItemFactory<TimeSource, Type> TimeSources
		{
			get
			{
				return this.timeSources;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0007A242 File Offset: 0x00078442
		public INamedItemFactory<MethodInfo, MethodInfo> ConditionMethods
		{
			get
			{
				return this.conditionMethods;
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0007A24A File Offset: 0x0007844A
		public void RegisterItemsFromAssembly(Assembly assembly)
		{
			this.RegisterItemsFromAssembly(assembly, string.Empty);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0007A258 File Offset: 0x00078458
		public void RegisterItemsFromAssembly(Assembly assembly, string itemNamePrefix)
		{
			ConfigurationItemFactory.Logger.Debug("ScanAssembly('{0}')", new object[] { assembly.FullName });
			Type[] array = assembly.SafeGetTypes();
			foreach (object obj in this.allFactories)
			{
				((IFactory)obj).ScanTypes(array, itemNamePrefix);
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0007A2D0 File Offset: 0x000784D0
		public void Clear()
		{
			foreach (object obj in this.allFactories)
			{
				((IFactory)obj).Clear();
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0007A320 File Offset: 0x00078520
		public void RegisterType(Type type, string itemNamePrefix)
		{
			foreach (object obj in this.allFactories)
			{
				((IFactory)obj).RegisterType(type, itemNamePrefix);
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0007A374 File Offset: 0x00078574
		private static ConfigurationItemFactory BuildDefaultFactory()
		{
			List<string> searchAssemblyNames = new List<string>
			{
				"Assembly-CSharp",
				Assembly.GetExecutingAssembly().GetName().Name
			};
			ConfigurationItemFactory configurationItemFactory = new ConfigurationItemFactory((from a in AppDomain.CurrentDomain.GetAssemblies()
				where searchAssemblyNames.Any<string>((string t) => a.FullName.StartsWith(t))
				select a).ToList<Assembly>().ToArray<Assembly>());
			configurationItemFactory.RegisterExtendedItems();
			return configurationItemFactory;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0007A3E4 File Offset: 0x000785E4
		private void RegisterExtendedItems()
		{
			string text = typeof(NLogger).AssemblyQualifiedName;
			string text2 = "NLog,";
			string text3 = "NLog.Extended,";
			int num = text.IndexOf(text2, StringComparison.OrdinalIgnoreCase);
			if (num >= 0)
			{
				text = ", " + text3 + text.Substring(num + text2.Length);
				string @namespace = typeof(FileTarget).Namespace;
				this.targets.RegisterNamedType("AspNetTrace", @namespace + ".AspNetTraceTarget" + text);
				this.targets.RegisterNamedType("MSMQ", @namespace + ".MessageQueueTarget" + text);
				this.targets.RegisterNamedType("AspNetBufferingWrapper", @namespace + ".Wrappers.AspNetBufferingTargetWrapper" + text);
				string namespace2 = typeof(MessageLayoutRenderer).Namespace;
				this.layoutRenderers.RegisterNamedType("appsetting", namespace2 + ".AppSettingLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-application", namespace2 + ".AspNetApplicationValueLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-request", namespace2 + ".AspNetRequestValueLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-sessionid", namespace2 + ".AspNetSessionIDLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-session", namespace2 + ".AspNetSessionValueLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-user-authtype", namespace2 + ".AspNetUserAuthTypeLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-user-identity", namespace2 + ".AspNetUserIdentityLayoutRenderer" + text);
			}
		}

		// Token: 0x04000E0B RID: 3595
		private readonly IList<object> allFactories;

		// Token: 0x04000E0C RID: 3596
		private readonly Factory<Target, TargetAttribute> targets;

		// Token: 0x04000E0D RID: 3597
		private readonly Factory<Filter, FilterAttribute> filters;

		// Token: 0x04000E0E RID: 3598
		private readonly Factory<LayoutRenderer, LayoutRendererAttribute> layoutRenderers;

		// Token: 0x04000E0F RID: 3599
		private readonly Factory<Layout, LayoutAttribute> layouts;

		// Token: 0x04000E10 RID: 3600
		private readonly MethodFactory<ConditionMethodsAttribute, ConditionMethodAttribute> conditionMethods;

		// Token: 0x04000E11 RID: 3601
		private readonly Factory<LayoutRenderer, AmbientPropertyAttribute> ambientProperties;

		// Token: 0x04000E12 RID: 3602
		private readonly Factory<TimeSource, TimeSourceAttribute> timeSources;
	}
}
