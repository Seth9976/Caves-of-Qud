using System;

namespace System.Diagnostics
{
	// Token: 0x0200021C RID: 540
	internal class FilterElement : TypedElement
	{
		// Token: 0x06000FA4 RID: 4004 RVA: 0x00045760 File Offset: 0x00043960
		public FilterElement()
			: base(typeof(TraceFilter))
		{
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00045772 File Offset: 0x00043972
		public TraceFilter GetRuntimeObject()
		{
			TraceFilter traceFilter = (TraceFilter)base.BaseGetRuntimeObject();
			traceFilter.initializeData = base.InitData;
			return traceFilter;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004578B File Offset: 0x0004398B
		internal TraceFilter RefreshRuntimeObject(TraceFilter filter)
		{
			if (Type.GetType(this.TypeName) != filter.GetType() || base.InitData != filter.initializeData)
			{
				this._runtimeObject = null;
				return this.GetRuntimeObject();
			}
			return filter;
		}
	}
}
