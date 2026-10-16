using System;
using XRL.World;

namespace XRL
{
	/// <summary>A tokenized singleton that serializes bindings to a specific implementation of <see cref="T:XRL.IEventHandler" />.</summary>
	// Token: 0x020004DF RID: 1247
	public abstract class IEventBinder : ITokenized, IComposite
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x001044D4 File Offset: 0x001026D4
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060035E6 RID: 13798 RVA: 0x001044D7 File Offset: 0x001026D7
		// (set) Token: 0x060035E7 RID: 13799 RVA: 0x001044DF File Offset: 0x001026DF
		int ITokenized.Token { get; set; }

		// Token: 0x060035E8 RID: 13800 RVA: 0x001044E8 File Offset: 0x001026E8
		public virtual void WriteBind(SerializationWriter Writer, IEventHandler Handler, int ID)
		{
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x001044EA File Offset: 0x001026EA
		public virtual IEventHandler ReadBind(SerializationReader Reader, int ID)
		{
			return null;
		}
	}
}
