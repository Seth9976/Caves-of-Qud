using System;

// Token: 0x0200018F RID: 399
public class MonoPInvokeCallbackAttribute : Attribute
{
	// Token: 0x06000E3F RID: 3647 RVA: 0x0001C5AE File Offset: 0x0001A7AE
	public MonoPInvokeCallbackAttribute(Type t)
	{
		this.type = t;
	}

	// Token: 0x04000363 RID: 867
	private Type type;
}
