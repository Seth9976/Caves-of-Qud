using System;

namespace XRL.CharacterBuilds
{
	/// <summary>
	///     AbstractQudEmbarkBuilderModule with a specific data type.
	/// </summary>
	/// <typeparam name="T">An EmbarkBuilderModuleData type.</typeparam>
	// Token: 0x02000593 RID: 1427
	public abstract class QudEmbarkBuilderModule<T> : AbstractQudEmbarkBuilderModule where T : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003D3E RID: 15678 RVA: 0x0012C240 File Offset: 0x0012A440
		public override Type getDataType()
		{
			return typeof(T);
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x0012C24C File Offset: 0x0012A44C
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x0012C25E File Offset: 0x0012A45E
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
