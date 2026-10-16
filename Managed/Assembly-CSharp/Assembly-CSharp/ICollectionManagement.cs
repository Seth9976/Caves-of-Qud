using System;

// Token: 0x02000088 RID: 136
public interface ICollectionManagement<T>
{
	// Token: 0x060004DD RID: 1245
	void Add(T el);

	// Token: 0x060004DE RID: 1246
	T Get(int index);

	// Token: 0x060004DF RID: 1247
	void RemoveRange(int index, int count);

	// Token: 0x060004E0 RID: 1248
	void RemoveAt(int index);
}
