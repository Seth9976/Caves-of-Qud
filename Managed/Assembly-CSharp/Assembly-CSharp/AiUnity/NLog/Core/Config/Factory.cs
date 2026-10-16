using System;
using System.Collections.Generic;
using System.ComponentModel;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x0200028D RID: 653
	internal class Factory<TBaseType, TAttributeType> : INamedItemFactory<TBaseType, Type>, IFactory where TBaseType : class where TAttributeType : DisplayNameAttribute
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0007A583 File Offset: 0x00078783
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0007A58A File Offset: 0x0007878A
		internal Factory(ConfigurationItemFactory parentFactory)
		{
			this.parentFactory = parentFactory;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0007A5AC File Offset: 0x000787AC
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
					Factory<TBaseType, TAttributeType>.Logger.Error("Failed to add type '" + type.FullName + "': {0}", new object[] { ex });
				}
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0007A620 File Offset: 0x00078820
		public void RegisterType(Type type, string itemNamePrefix)
		{
			TAttributeType[] array = (TAttributeType[])type.GetCustomAttributes(typeof(TAttributeType), false);
			if (array != null)
			{
				foreach (TAttributeType tattributeType in array)
				{
					this.RegisterDefinition(itemNamePrefix + tattributeType.DisplayName, type);
				}
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0007A678 File Offset: 0x00078878
		public void RegisterNamedType(string itemName, string typeName)
		{
			this.items[itemName] = () => Type.GetType(typeName, false);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0007A6AA File Offset: 0x000788AA
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0007A6B8 File Offset: 0x000788B8
		public void RegisterDefinition(string name, Type type)
		{
			this.items[name] = () => type;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0007A6EC File Offset: 0x000788EC
		public bool TryGetDefinition(string itemName, out Type result)
		{
			Factory<TBaseType, TAttributeType>.GetTypeDelegate getTypeDelegate;
			if (!this.items.TryGetValue(itemName, out getTypeDelegate))
			{
				result = null;
				return false;
			}
			bool flag;
			try
			{
				result = getTypeDelegate();
				flag = result != null;
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0007A744 File Offset: 0x00078944
		public bool TryCreateInstance(string itemName, out TBaseType result)
		{
			Type type;
			if (!this.TryGetDefinition(itemName, out type))
			{
				result = default(TBaseType);
				return false;
			}
			result = (TBaseType)((object)this.parentFactory.CreateInstance(type));
			return true;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0007A784 File Offset: 0x00078984
		public TBaseType CreateInstance(string name)
		{
			TBaseType tbaseType;
			if (this.TryCreateInstance(name, out tbaseType))
			{
				return tbaseType;
			}
			throw new ArgumentException(typeof(TBaseType).Name + " cannot be found: '" + name + "'");
		}

		// Token: 0x04000E15 RID: 3605
		private readonly Dictionary<string, Factory<TBaseType, TAttributeType>.GetTypeDelegate> items = new Dictionary<string, Factory<TBaseType, TAttributeType>.GetTypeDelegate>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000E16 RID: 3606
		private ConfigurationItemFactory parentFactory;

		// Token: 0x0200167D RID: 5757
		// (Invoke) Token: 0x0600D836 RID: 55350
		private delegate Type GetTypeDelegate();
	}
}
