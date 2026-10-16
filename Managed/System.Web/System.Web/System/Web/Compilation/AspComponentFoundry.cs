using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000615 RID: 1557
	internal class AspComponentFoundry
	{
		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x000B071B File Offset: 0x000AE91B
		private Dictionary<string, AspComponent> Components
		{
			get
			{
				if (this.components == null)
				{
					this.components = new Dictionary<string, AspComponent>(StringComparer.OrdinalIgnoreCase);
				}
				return this.components;
			}
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000B073C File Offset: 0x000AE93C
		public AspComponentFoundry()
		{
			this.foundries = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			Assembly assembly = typeof(AspComponentFoundry).Assembly;
			this.RegisterFoundry("asp", assembly, "System.Web.UI.WebControls");
			this.RegisterFoundry("", "object", typeof(ObjectTag));
			this.RegisterConfigControls();
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x000B07A0 File Offset: 0x000AE9A0
		public AspComponent GetComponent(string tagName)
		{
			if (tagName == null || tagName.Length == 0)
			{
				return null;
			}
			AspComponent aspComponent;
			if (this.components != null && this.components.TryGetValue(tagName, out aspComponent))
			{
				return aspComponent;
			}
			int num = tagName.IndexOf(':');
			string text;
			string text2;
			if (num > -1)
			{
				if (num == 0)
				{
					throw new Exception("Empty TagPrefix is not valid.");
				}
				if (num + 1 == tagName.Length)
				{
					return null;
				}
				text = tagName.Substring(0, num);
				text2 = tagName.Substring(num + 1);
			}
			else
			{
				text = string.Empty;
				text2 = tagName;
			}
			object obj = this.foundries[text];
			if (obj == null)
			{
				return null;
			}
			AspComponentFoundry.Foundry foundry = obj as AspComponentFoundry.Foundry;
			if (foundry != null)
			{
				return this.CreateComponent(foundry, tagName, text, text2);
			}
			ArrayList arrayList = obj as ArrayList;
			if (arrayList == null)
			{
				return null;
			}
			Exception ex = null;
			foreach (object obj2 in arrayList)
			{
				AspComponentFoundry.Foundry foundry2 = (AspComponentFoundry.Foundry)obj2;
				try
				{
					AspComponent aspComponent2 = this.CreateComponent(foundry2, tagName, text, text2);
					if (aspComponent2 != null)
					{
						return aspComponent2;
					}
				}
				catch (Exception ex)
				{
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return null;
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x000B08D8 File Offset: 0x000AEAD8
		private AspComponent CreateComponent(AspComponentFoundry.Foundry foundry, string tagName, string prefix, string tag)
		{
			string text;
			string text2;
			Type type = foundry.GetType(tag, out text, out text2);
			if (type == null)
			{
				return null;
			}
			AspComponent aspComponent = new AspComponent(type, text2, prefix, text, foundry.FromConfig);
			this.Components.Add(tagName, aspComponent);
			return aspComponent;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x000B091B File Offset: 0x000AEB1B
		public void RegisterFoundry(string foundryName, Assembly assembly, string nameSpace)
		{
			this.RegisterFoundry(foundryName, assembly, nameSpace, false);
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000B0928 File Offset: 0x000AEB28
		public void RegisterFoundry(string foundryName, Assembly assembly, string nameSpace, bool fromConfig)
		{
			this.InternalRegister(foundryName, new AspComponentFoundry.AssemblyFoundry(assembly, nameSpace)
			{
				FromConfig = fromConfig
			}, fromConfig);
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000B094F File Offset: 0x000AEB4F
		public void RegisterFoundry(string foundryName, string tagName, Type type)
		{
			this.RegisterFoundry(foundryName, tagName, type, false);
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000B095C File Offset: 0x000AEB5C
		public void RegisterFoundry(string foundryName, string tagName, Type type, bool fromConfig)
		{
			this.InternalRegister(foundryName, new AspComponentFoundry.TagNameFoundry(tagName, type)
			{
				FromConfig = fromConfig
			}, fromConfig);
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000B0983 File Offset: 0x000AEB83
		public void RegisterFoundry(string foundryName, string tagName, string source)
		{
			this.RegisterFoundry(foundryName, tagName, source, false);
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000B0990 File Offset: 0x000AEB90
		public void RegisterFoundry(string foundryName, string tagName, string source, bool fromConfig)
		{
			this.InternalRegister(foundryName, new AspComponentFoundry.TagNameFoundry(tagName, source)
			{
				FromConfig = fromConfig
			}, fromConfig);
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000B09B8 File Offset: 0x000AEBB8
		public void RegisterAssemblyFoundry(string foundryName, string assemblyName, string nameSpace, bool fromConfig)
		{
			this.InternalRegister(foundryName, new AspComponentFoundry.AssemblyFoundry(assemblyName, nameSpace)
			{
				FromConfig = fromConfig
			}, fromConfig);
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000B09E0 File Offset: 0x000AEBE0
		private void RegisterConfigControls()
		{
			PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
			if (pagesSection == null)
			{
				return;
			}
			TagPrefixCollection controls = pagesSection.Controls;
			if (controls == null || controls.Count == 0)
			{
				return;
			}
			IList codeAssemblies = BuildManager.CodeAssemblies;
			bool flag = codeAssemblies != null && codeAssemblies.Count > 0;
			foreach (object obj in controls)
			{
				TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)obj;
				if (!string.IsNullOrEmpty(tagPrefixInfo.TagName))
				{
					this.RegisterFoundry(tagPrefixInfo.TagPrefix, tagPrefixInfo.TagName, tagPrefixInfo.Source, true);
				}
				else
				{
					if (string.IsNullOrEmpty(tagPrefixInfo.Assembly))
					{
						if (!flag)
						{
							continue;
						}
						using (IEnumerator enumerator2 = codeAssemblies.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								Assembly assembly = obj2 as Assembly;
								if (!(assembly == null))
								{
									this.RegisterFoundry(tagPrefixInfo.TagPrefix, assembly, tagPrefixInfo.Namespace, true);
								}
							}
							continue;
						}
					}
					if (!string.IsNullOrEmpty(tagPrefixInfo.Namespace))
					{
						this.RegisterAssemblyFoundry(tagPrefixInfo.TagPrefix, tagPrefixInfo.Assembly, tagPrefixInfo.Namespace, true);
					}
				}
			}
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x000B0B4C File Offset: 0x000AED4C
		private void InternalRegister(string foundryName, AspComponentFoundry.Foundry foundry, bool fromConfig)
		{
			object obj = this.foundries[foundryName];
			AspComponentFoundry.Foundry foundry2 = null;
			if (obj is AspComponentFoundry.CompoundFoundry)
			{
				((AspComponentFoundry.CompoundFoundry)obj).Add(foundry);
				return;
			}
			if (obj == null || obj is ArrayList || (obj is AspComponentFoundry.AssemblyFoundry && foundry is AspComponentFoundry.AssemblyFoundry))
			{
				foundry2 = foundry;
			}
			else if (obj != null)
			{
				AspComponentFoundry.CompoundFoundry compoundFoundry = new AspComponentFoundry.CompoundFoundry(foundryName);
				compoundFoundry.Add((AspComponentFoundry.Foundry)obj);
				compoundFoundry.Add(foundry);
				foundry2 = foundry;
				foundry2.FromConfig = fromConfig;
			}
			if (foundry2 == null)
			{
				return;
			}
			if (obj == null)
			{
				this.foundries[foundryName] = foundry2;
				return;
			}
			ArrayList arrayList = obj as ArrayList;
			if (arrayList == null)
			{
				arrayList = new ArrayList(2);
				arrayList.Add(obj);
				this.foundries[foundryName] = arrayList;
			}
			if (foundry2 is AspComponentFoundry.AssemblyFoundry)
			{
				for (int i = 0; i < arrayList.Count; i++)
				{
					if (arrayList[i] is AspComponentFoundry.AssemblyFoundry)
					{
						arrayList.Insert(i, foundry2);
						return;
					}
				}
				arrayList.Add(foundry2);
				return;
			}
			arrayList.Insert(0, foundry2);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000B0C3C File Offset: 0x000AEE3C
		public bool LookupFoundry(string foundryName)
		{
			return this.foundries.Contains(foundryName);
		}

		// Token: 0x040023E5 RID: 9189
		private Hashtable foundries;

		// Token: 0x040023E6 RID: 9190
		private Dictionary<string, AspComponent> components;

		// Token: 0x02000616 RID: 1558
		private abstract class Foundry
		{
			// Token: 0x17001537 RID: 5431
			// (get) Token: 0x060042F9 RID: 17145 RVA: 0x000B0C4A File Offset: 0x000AEE4A
			// (set) Token: 0x060042FA RID: 17146 RVA: 0x000B0C52 File Offset: 0x000AEE52
			public bool FromConfig
			{
				get
				{
					return this._fromConfig;
				}
				set
				{
					this._fromConfig = value;
				}
			}

			// Token: 0x060042FB RID: 17147
			public abstract Type GetType(string componentName, out string source, out string ns);

			// Token: 0x040023E7 RID: 9191
			private bool _fromConfig;
		}

		// Token: 0x02000617 RID: 1559
		private class TagNameFoundry : AspComponentFoundry.Foundry
		{
			// Token: 0x17001538 RID: 5432
			// (get) Token: 0x060042FD RID: 17149 RVA: 0x000B0C5B File Offset: 0x000AEE5B
			public bool FromWebConfig
			{
				get
				{
					return this.source != null;
				}
			}

			// Token: 0x060042FE RID: 17150 RVA: 0x000B0C66 File Offset: 0x000AEE66
			public TagNameFoundry(string tagName, string source)
			{
				this.tagName = tagName;
				this.source = source;
			}

			// Token: 0x060042FF RID: 17151 RVA: 0x000B0C7C File Offset: 0x000AEE7C
			public TagNameFoundry(string tagName, Type type)
			{
				this.tagName = tagName;
				this.type = type;
			}

			// Token: 0x06004300 RID: 17152 RVA: 0x000B0C92 File Offset: 0x000AEE92
			public override Type GetType(string componentName, out string source, out string ns)
			{
				source = null;
				ns = null;
				if (string.Compare(componentName, this.tagName, true, Helpers.InvariantCulture) != 0)
				{
					return null;
				}
				source = this.source;
				return this.LoadType();
			}

			// Token: 0x06004301 RID: 17153 RVA: 0x000B0CC0 File Offset: 0x000AEEC0
			private Type LoadType()
			{
				if (this.type != null)
				{
					return this.type;
				}
				HttpContext httpContext = HttpContext.Current;
				string text;
				string text2;
				if (VirtualPathUtility.IsAppRelative(this.source))
				{
					text = this.source;
					text2 = httpContext.Request.MapPath(this.source);
				}
				else
				{
					text = VirtualPathUtility.ToAppRelative(this.source);
					text2 = this.source;
				}
				if ((this.type = CachingCompiler.GetTypeFromCache(text2)) != null)
				{
					return this.type;
				}
				this.type = BuildManager.GetCompiledType(text);
				if (this.type != null)
				{
					AspGenerator.AddTypeToCache(null, text2, this.type);
					BuildManager.AddToReferencedAssemblies(this.type.Assembly);
				}
				return this.type;
			}

			// Token: 0x17001539 RID: 5433
			// (get) Token: 0x06004302 RID: 17154 RVA: 0x000B0D7E File Offset: 0x000AEF7E
			public string TagName
			{
				get
				{
					return this.tagName;
				}
			}

			// Token: 0x040023E8 RID: 9192
			private string tagName;

			// Token: 0x040023E9 RID: 9193
			private Type type;

			// Token: 0x040023EA RID: 9194
			private string source;
		}

		// Token: 0x02000618 RID: 1560
		private class AssemblyFoundry : AspComponentFoundry.Foundry
		{
			// Token: 0x06004303 RID: 17155 RVA: 0x000B0D86 File Offset: 0x000AEF86
			public AssemblyFoundry(Assembly assembly, string nameSpace)
			{
				this.assembly = assembly;
				this.nameSpace = nameSpace;
				if (assembly != null)
				{
					this.assemblyName = assembly.FullName;
					return;
				}
				this.assemblyName = null;
			}

			// Token: 0x06004304 RID: 17156 RVA: 0x000B0DB9 File Offset: 0x000AEFB9
			public AssemblyFoundry(string assemblyName, string nameSpace)
			{
				this.assembly = null;
				this.nameSpace = nameSpace;
				this.assemblyName = assemblyName;
			}

			// Token: 0x06004305 RID: 17157 RVA: 0x000B0DD8 File Offset: 0x000AEFD8
			public override Type GetType(string componentName, out string source, out string ns)
			{
				source = null;
				ns = this.nameSpace;
				if (this.assembly == null && this.assemblyName != null)
				{
					this.assembly = this.GetAssemblyByName(this.assemblyName, true);
				}
				string text = this.nameSpace + "." + componentName;
				if (this.assembly != null)
				{
					return this.assembly.GetType(text, false, true);
				}
				IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
				if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
				{
					foreach (object obj in topLevelAssemblies)
					{
						Assembly assembly = (Assembly)obj;
						if (!(assembly == null))
						{
							Type type = assembly.GetType(text, false, true);
							if (type != null)
							{
								return type;
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06004306 RID: 17158 RVA: 0x000B0EC8 File Offset: 0x000AF0C8
			private Assembly GetAssemblyByName(string name, bool throwOnMissing)
			{
				if (this.assemblyCache == null)
				{
					this.assemblyCache = new Dictionary<string, Assembly>();
				}
				if (this.assemblyCache.ContainsKey(name))
				{
					return this.assemblyCache[name];
				}
				Assembly assembly = null;
				Exception ex = null;
				if (name.IndexOf(',') != -1)
				{
					try
					{
						assembly = Assembly.Load(name);
					}
					catch (Exception ex)
					{
					}
				}
				if (assembly == null)
				{
					try
					{
						assembly = Assembly.LoadWithPartialName(name);
					}
					catch (Exception ex)
					{
					}
				}
				if (!(assembly == null))
				{
					this.assemblyCache.Add(name, assembly);
					return assembly;
				}
				if (throwOnMissing)
				{
					throw new HttpException("Assembly " + name + " not found", ex);
				}
				return null;
			}

			// Token: 0x040023EB RID: 9195
			private string nameSpace;

			// Token: 0x040023EC RID: 9196
			private Assembly assembly;

			// Token: 0x040023ED RID: 9197
			private string assemblyName;

			// Token: 0x040023EE RID: 9198
			private Dictionary<string, Assembly> assemblyCache;
		}

		// Token: 0x02000619 RID: 1561
		private class CompoundFoundry : AspComponentFoundry.Foundry
		{
			// Token: 0x06004307 RID: 17159 RVA: 0x000B0F84 File Offset: 0x000AF184
			public CompoundFoundry(string tagPrefix)
			{
				this.tagPrefix = tagPrefix;
				this.tagnames = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			}

			// Token: 0x06004308 RID: 17160 RVA: 0x000B0FA4 File Offset: 0x000AF1A4
			public void Add(AspComponentFoundry.Foundry foundry)
			{
				if (foundry is AspComponentFoundry.AssemblyFoundry)
				{
					this.assemblyFoundry = (AspComponentFoundry.AssemblyFoundry)foundry;
					return;
				}
				AspComponentFoundry.TagNameFoundry tagNameFoundry = (AspComponentFoundry.TagNameFoundry)foundry;
				string tagName = tagNameFoundry.TagName;
				if (!this.tagnames.Contains(tagName))
				{
					this.tagnames.Add(tagName, foundry);
					return;
				}
				if (tagNameFoundry.FromWebConfig)
				{
					return;
				}
				throw new ApplicationException(string.Format("{0}:{1} already registered.", this.tagPrefix, tagName));
			}

			// Token: 0x06004309 RID: 17161 RVA: 0x000B1010 File Offset: 0x000AF210
			public override Type GetType(string componentName, out string source, out string ns)
			{
				source = null;
				ns = null;
				AspComponentFoundry.Foundry foundry = this.tagnames[componentName] as AspComponentFoundry.Foundry;
				if (foundry != null)
				{
					return foundry.GetType(componentName, out source, out ns);
				}
				if (this.assemblyFoundry != null)
				{
					try
					{
						return this.assemblyFoundry.GetType(componentName, out source, out ns);
					}
					catch
					{
					}
				}
				throw new ApplicationException(string.Format("Type {0} not registered for prefix {1}", componentName, this.tagPrefix));
			}

			// Token: 0x040023EF RID: 9199
			private AspComponentFoundry.AssemblyFoundry assemblyFoundry;

			// Token: 0x040023F0 RID: 9200
			private Hashtable tagnames;

			// Token: 0x040023F1 RID: 9201
			private string tagPrefix;
		}
	}
}
