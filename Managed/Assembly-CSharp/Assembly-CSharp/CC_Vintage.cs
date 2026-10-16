using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Vintage")]
public class CC_Vintage : CC_LookupFilter
{
	// Token: 0x06000095 RID: 149 RVA: 0x00004D78 File Offset: 0x00002F78
	protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.filter == CC_Vintage.Filter.None)
		{
			this.lookupTexture = null;
		}
		else
		{
			this.lookupTexture = Resources.Load<Texture2D>("Instagram/" + this.filter.ToString());
		}
		base.OnRenderImage(source, destination);
	}

	// Token: 0x040000CB RID: 203
	public CC_Vintage.Filter filter;

	// Token: 0x02001555 RID: 5461
	public enum Filter
	{
		// Token: 0x040055BC RID: 21948
		None,
		// Token: 0x040055BD RID: 21949
		F1977,
		// Token: 0x040055BE RID: 21950
		Aden,
		// Token: 0x040055BF RID: 21951
		Amaro,
		// Token: 0x040055C0 RID: 21952
		Brannan,
		// Token: 0x040055C1 RID: 21953
		Crema,
		// Token: 0x040055C2 RID: 21954
		Earlybird,
		// Token: 0x040055C3 RID: 21955
		Hefe,
		// Token: 0x040055C4 RID: 21956
		Hudson,
		// Token: 0x040055C5 RID: 21957
		Inkwell,
		// Token: 0x040055C6 RID: 21958
		Kelvin,
		// Token: 0x040055C7 RID: 21959
		LoFi,
		// Token: 0x040055C8 RID: 21960
		Ludwig,
		// Token: 0x040055C9 RID: 21961
		Mayfair,
		// Token: 0x040055CA RID: 21962
		Nashville,
		// Token: 0x040055CB RID: 21963
		Perpetua,
		// Token: 0x040055CC RID: 21964
		Rise,
		// Token: 0x040055CD RID: 21965
		Sierra,
		// Token: 0x040055CE RID: 21966
		Slumber,
		// Token: 0x040055CF RID: 21967
		Sutro,
		// Token: 0x040055D0 RID: 21968
		Toaster,
		// Token: 0x040055D1 RID: 21969
		Valencia,
		// Token: 0x040055D2 RID: 21970
		Walden,
		// Token: 0x040055D3 RID: 21971
		Willow,
		// Token: 0x040055D4 RID: 21972
		XProII
	}
}
