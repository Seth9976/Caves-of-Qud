using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B4F RID: 2895
	[GenerateSerializationPartial]
	public class OpinionSummon : IOpinionSubject
	{
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060074AC RID: 29868 RVA: 0x002BE72C File Offset: 0x002BC92C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x002BE72F File Offset: 0x002BC92F
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x060074AE RID: 29870 RVA: 0x002BE749 File Offset: 0x002BC949
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060074AF RID: 29871 RVA: 0x002BE763 File Offset: 0x002BC963
		public override int BaseValue
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x060074B0 RID: 29872 RVA: 0x002BE767 File Offset: 0x002BC967
		public override string GetText(GameObject Actor)
		{
			return "Summoned me.";
		}
	}
}
