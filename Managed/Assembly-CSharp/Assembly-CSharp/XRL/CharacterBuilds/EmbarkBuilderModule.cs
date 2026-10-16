using System;

namespace XRL.CharacterBuilds
{
	/// <summary>
	///     AbstractEmbarkBuilderModule with a specific data type.
	/// </summary>
	/// <typeparam name="T">An EmbarkBuilderModuleData type.</typeparam>
	// Token: 0x0200058B RID: 1419
	public abstract class EmbarkBuilderModule<T> : AbstractEmbarkBuilderModule where T : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003D07 RID: 15623 RVA: 0x0012BA6C File Offset: 0x00129C6C
		public override Type getDataType()
		{
			return typeof(T);
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x0012BA78 File Offset: 0x00129C78
		// (set) Token: 0x06003D09 RID: 15625 RVA: 0x0012BA8A File Offset: 0x00129C8A
		public T data
		{
			get
			{
				return this.getData() as T;
			}
			set
			{
				this.setData(value);
			}
		}
	}
}
