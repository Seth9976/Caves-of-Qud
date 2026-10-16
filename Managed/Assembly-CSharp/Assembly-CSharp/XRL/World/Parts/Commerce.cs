using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Parts
{
	// Token: 0x02000DBF RID: 3519
	[GeneratePoolingPartial]
	[GenerateSerializationPartial]
	[Serializable]
	public class Commerce : IPart
	{
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06008D2F RID: 36143 RVA: 0x003235E1 File Offset: 0x003217E1
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return Commerce.CommercePool;
			}
		}

		// Token: 0x06008D30 RID: 36144 RVA: 0x003235E8 File Offset: 0x003217E8
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override void Reset()
		{
			base.Reset();
			this.Value = 1.0;
		}

		// Token: 0x06008D31 RID: 36145 RVA: 0x003235FF File Offset: 0x003217FF
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.Value);
		}

		// Token: 0x06008D32 RID: 36146 RVA: 0x0032360D File Offset: 0x0032180D
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Value = Reader.ReadDouble();
		}

		// Token: 0x04003B33 RID: 15155
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool CommercePool = new IPartPool();

		// Token: 0x04003B34 RID: 15156
		public double Value = 1.0;
	}
}
