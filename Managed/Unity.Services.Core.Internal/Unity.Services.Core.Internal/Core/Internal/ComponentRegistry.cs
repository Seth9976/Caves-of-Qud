using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000039 RID: 57
	internal class ComponentRegistry : IComponentRegistry
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002B75 File Offset: 0x00000D75
		[NotNull]
		internal Dictionary<int, IServiceComponent> ComponentTypeHashToInstance { get; }

		// Token: 0x060000E4 RID: 228 RVA: 0x00002B7D File Offset: 0x00000D7D
		public ComponentRegistry()
		{
			this.ComponentTypeHashToInstance = new Dictionary<int, IServiceComponent>();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002B90 File Offset: 0x00000D90
		public ComponentRegistry([NotNull] Dictionary<int, IServiceComponent> componentTypeHashToInstance)
		{
			this.ComponentTypeHashToInstance = componentTypeHashToInstance;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public void RegisterServiceComponent<TComponent>(TComponent component) where TComponent : IServiceComponent
		{
			Type typeFromHandle = typeof(TComponent);
			if (component.GetType() == typeFromHandle)
			{
				throw new ArgumentException("Interface type of component not specified.");
			}
			int hashCode = typeFromHandle.GetHashCode();
			if (this.IsComponentTypeRegistered(hashCode))
			{
				throw new InvalidOperationException("A component with the type " + typeFromHandle.FullName + " has already been registered.");
			}
			this.ComponentTypeHashToInstance[hashCode] = component;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002C18 File Offset: 0x00000E18
		public TComponent GetServiceComponent<TComponent>() where TComponent : IServiceComponent
		{
			Type typeFromHandle = typeof(TComponent);
			IServiceComponent serviceComponent;
			if (!this.ComponentTypeHashToInstance.TryGetValue(typeFromHandle.GetHashCode(), out serviceComponent) || serviceComponent is MissingComponent)
			{
				throw new KeyNotFoundException("There is no component `" + typeFromHandle.Name + "` registered. Are you missing a package?");
			}
			return (TComponent)((object)serviceComponent);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00002C70 File Offset: 0x00000E70
		public bool TryGetServiceComponent<TComponent>(out TComponent component) where TComponent : IServiceComponent
		{
			Type typeFromHandle = typeof(TComponent);
			IServiceComponent serviceComponent;
			bool flag = this.ComponentTypeHashToInstance.TryGetValue(typeFromHandle.GetHashCode(), out serviceComponent) && !(serviceComponent is MissingComponent);
			component = (flag ? ((TComponent)((object)serviceComponent)) : default(TComponent));
			return flag;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002CC8 File Offset: 0x00000EC8
		private bool IsComponentTypeRegistered(int componentTypeHash)
		{
			IServiceComponent serviceComponent;
			return this.ComponentTypeHashToInstance.TryGetValue(componentTypeHash, out serviceComponent) && serviceComponent != null && !(serviceComponent is MissingComponent);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002CF6 File Offset: 0x00000EF6
		public void ResetProvidedComponents(IDictionary<int, IServiceComponent> componentTypeHashToInstance)
		{
			this.ComponentTypeHashToInstance.Clear();
			this.ComponentTypeHashToInstance.MergeAllowOverride(componentTypeHashToInstance);
		}
	}
}
