using System;
using System.Globalization;

namespace System.Web.Globalization
{
	// Token: 0x02000771 RID: 1905
	public interface IStringLocalizerProvider
	{
		// Token: 0x06004D3D RID: 19773
		string GetLocalizedString(CultureInfo culture, string name, object[] arguments);
	}
}
