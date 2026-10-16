using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020001BB RID: 443
	internal sealed class DataBindingBuilder : CodeBuilder
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x0002FB8F File Offset: 0x0002DD8F
		public DataBindingBuilder(string code, ILocation location)
			: base(code, false, location)
		{
			base.SetControlType(typeof(DataBoundLiteralControl));
		}
	}
}
