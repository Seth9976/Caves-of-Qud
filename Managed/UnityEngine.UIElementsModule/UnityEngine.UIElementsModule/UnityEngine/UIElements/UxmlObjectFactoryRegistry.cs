using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000584 RID: 1412
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Obsolete("UxmlObjectFactoryRegistry is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	internal class UxmlObjectFactoryRegistry
	{
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x00098970 File Offset: 0x00096B70
		internal static Dictionary<string, List<IBaseUxmlObjectFactory>> factories
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				bool flag = UxmlObjectFactoryRegistry.s_Factories == null;
				if (flag)
				{
					UxmlObjectFactoryRegistry.s_Factories = new Dictionary<string, List<IBaseUxmlObjectFactory>>();
					UxmlObjectFactoryRegistry.RegisterEngineFactories();
					UxmlObjectFactoryRegistry.RegisterUserFactories();
				}
				return UxmlObjectFactoryRegistry.s_Factories;
			}
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000989AC File Offset: 0x00096BAC
		protected static void RegisterFactory(IBaseUxmlObjectFactory factory)
		{
			List<IBaseUxmlObjectFactory> list;
			bool flag = UxmlObjectFactoryRegistry.factories.TryGetValue(factory.uxmlQualifiedName, out list);
			if (flag)
			{
				foreach (IBaseUxmlObjectFactory baseUxmlObjectFactory in list)
				{
					bool flag2 = baseUxmlObjectFactory.GetType() == factory.GetType();
					if (flag2)
					{
						throw new ArgumentException("A factory for the type " + factory.GetType().FullName + " was already registered");
					}
				}
				list.Add(factory);
			}
			else
			{
				list = new List<IBaseUxmlObjectFactory> { factory };
				UxmlObjectFactoryRegistry.s_Factories.Add(factory.uxmlQualifiedName, list);
			}
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x00098A74 File Offset: 0x00096C74
		private static void RegisterEngineFactories()
		{
			IBaseUxmlObjectFactory[] array = new IBaseUxmlObjectFactory[]
			{
				new Columns.UxmlObjectFactory(),
				new Column.UxmlObjectFactory(),
				new SortColumnDescriptions.UxmlObjectFactory(),
				new SortColumnDescription.UxmlObjectFactory()
			};
			foreach (IBaseUxmlObjectFactory baseUxmlObjectFactory in array)
			{
				UxmlObjectFactoryRegistry.RegisterFactory(baseUxmlObjectFactory);
			}
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x00098AC8 File Offset: 0x00096CC8
		private static void RegisterUserFactories()
		{
			HashSet<string> hashSet = new HashSet<string>(ScriptingRuntime.GetAllUserAssemblies());
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				bool flag = !hashSet.Contains(assembly.GetName().Name + ".dll") || assembly.GetName().Name == "UnityEngine.UIElementsModule";
				if (!flag)
				{
					Type[] types = assembly.GetTypes();
					foreach (Type type in types)
					{
						bool flag2 = !typeof(IBaseUxmlObjectFactory).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract || type.IsGenericType;
						if (!flag2)
						{
							IBaseUxmlObjectFactory baseUxmlObjectFactory = (IBaseUxmlObjectFactory)Activator.CreateInstance(type);
							UxmlObjectFactoryRegistry.RegisterFactory(baseUxmlObjectFactory);
						}
					}
				}
			}
		}

		// Token: 0x04001304 RID: 4868
		internal const string uieCoreModule = "UnityEngine.UIElementsModule";

		// Token: 0x04001305 RID: 4869
		private static Dictionary<string, List<IBaseUxmlObjectFactory>> s_Factories;
	}
}
