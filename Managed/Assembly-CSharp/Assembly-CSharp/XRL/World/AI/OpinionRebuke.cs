using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B4E RID: 2894
	[GenerateSerializationPartial]
	public class OpinionRebuke : OpinionMollify
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060074A7 RID: 29863 RVA: 0x002BE6E6 File Offset: 0x002BC8E6
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x002BE6E9 File Offset: 0x002BC8E9
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x060074A9 RID: 29865 RVA: 0x002BE703 File Offset: 0x002BC903
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x002BE71D File Offset: 0x002BC91D
		public override string GetText(GameObject Actor)
		{
			return "Rebuked me.";
		}
	}
}
