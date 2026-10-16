using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x02000138 RID: 312
	internal sealed class TypeDescriptorFilterService : ITypeDescriptorFilterService, IDisposable
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x000100B8 File Offset: 0x0000E2B8
		public TypeDescriptorFilterService(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this._serviceProvider = serviceProvider;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000100D8 File Offset: 0x0000E2D8
		public bool FilterAttributes(IComponent component, IDictionary attributes)
		{
			if (this._serviceProvider == null)
			{
				throw new ObjectDisposedException("TypeDescriptorFilterService");
			}
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			IDesignerHost designerHost = this._serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				IDesigner designer = designerHost.GetDesigner(component);
				if (designer is IDesignerFilter)
				{
					((IDesignerFilter)designer).PreFilterAttributes(attributes);
					((IDesignerFilter)designer).PostFilterAttributes(attributes);
				}
			}
			return true;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00010150 File Offset: 0x0000E350
		public bool FilterEvents(IComponent component, IDictionary events)
		{
			if (this._serviceProvider == null)
			{
				throw new ObjectDisposedException("TypeDescriptorFilterService");
			}
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			IDesignerHost designerHost = this._serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				IDesigner designer = designerHost.GetDesigner(component);
				if (designer is IDesignerFilter)
				{
					((IDesignerFilter)designer).PreFilterEvents(events);
					((IDesignerFilter)designer).PostFilterEvents(events);
				}
			}
			return true;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000101C8 File Offset: 0x0000E3C8
		public bool FilterProperties(IComponent component, IDictionary properties)
		{
			if (this._serviceProvider == null)
			{
				throw new ObjectDisposedException("TypeDescriptorFilterService");
			}
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			IDesignerHost designerHost = this._serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				IDesigner designer = designerHost.GetDesigner(component);
				if (designer is IDesignerFilter)
				{
					((IDesignerFilter)designer).PreFilterProperties(properties);
					((IDesignerFilter)designer).PostFilterProperties(properties);
				}
			}
			return true;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001023D File Offset: 0x0000E43D
		public void Dispose()
		{
			this._serviceProvider = null;
		}

		// Token: 0x04000210 RID: 528
		private IServiceProvider _serviceProvider;
	}
}
