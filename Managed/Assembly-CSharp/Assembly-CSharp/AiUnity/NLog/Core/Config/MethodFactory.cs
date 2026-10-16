using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000294 RID: 660
	internal class MethodFactory<TClassAttributeType, TMethodAttributeType> : INamedItemFactory<MethodInfo, MethodInfo>, IFactory where TClassAttributeType : Attribute where TMethodAttributeType : DisplayNameAttribute
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0007B059 File Offset: 0x00079259
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0007B060 File Offset: 0x00079260
		public IDictionary<string, MethodInfo> AllRegisteredItems
		{
			get
			{
				return this.nameToMethodInfo;
			}
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0007B068 File Offset: 0x00079268
		public void ScanTypes(Type[] types, string prefix)
		{
			foreach (Type type in types)
			{
				try
				{
					this.RegisterType(type, prefix);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					MethodFactory<TClassAttributeType, TMethodAttributeType>.Logger.Error("Failed to add type '" + type.FullName + "': {0}", new object[] { ex });
				}
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0007B0DC File Offset: 0x000792DC
		public void RegisterType(Type type, string itemNamePrefix)
		{
			if (type.IsDefined(typeof(TClassAttributeType), false))
			{
				foreach (MethodInfo methodInfo in type.GetMethods())
				{
					foreach (TMethodAttributeType tmethodAttributeType in (TMethodAttributeType[])methodInfo.GetCustomAttributes(typeof(TMethodAttributeType), false))
					{
						this.RegisterDefinition(itemNamePrefix + tmethodAttributeType.DisplayName, methodInfo);
					}
				}
			}
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0007B161 File Offset: 0x00079361
		public void Clear()
		{
			this.nameToMethodInfo.Clear();
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0007B16E File Offset: 0x0007936E
		public void RegisterDefinition(string name, MethodInfo methodInfo)
		{
			this.nameToMethodInfo[name] = methodInfo;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0007B17D File Offset: 0x0007937D
		public bool TryCreateInstance(string name, out MethodInfo result)
		{
			return this.nameToMethodInfo.TryGetValue(name, out result);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0007B18C File Offset: 0x0007938C
		public MethodInfo CreateInstance(string name)
		{
			MethodInfo methodInfo;
			if (this.TryCreateInstance(name, out methodInfo))
			{
				return methodInfo;
			}
			throw new NLogConfigurationException("Unknown function: '" + name + "'");
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0007B1BB File Offset: 0x000793BB
		public bool TryGetDefinition(string name, out MethodInfo result)
		{
			return this.nameToMethodInfo.TryGetValue(name, out result);
		}

		// Token: 0x04000E26 RID: 3622
		private readonly Dictionary<string, MethodInfo> nameToMethodInfo = new Dictionary<string, MethodInfo>();
	}
}
