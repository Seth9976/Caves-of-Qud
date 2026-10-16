using System;

// Token: 0x02000110 RID: 272
public class LTEvent
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x000457AC File Offset: 0x000439AC
	public LTEvent(int id, object data)
	{
		this.id = id;
		this.data = data;
	}

	// Token: 0x04000736 RID: 1846
	public int id;

	// Token: 0x04000737 RID: 1847
	public object data;
}
