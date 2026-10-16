using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B41 RID: 2881
	[GenerateSerializationPartial]
	public class OpinionChaosSpiel : IOpinionSubject
	{
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06007454 RID: 29780 RVA: 0x002BE252 File Offset: 0x002BC452
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007455 RID: 29781 RVA: 0x002BE255 File Offset: 0x002BC455
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007456 RID: 29782 RVA: 0x002BE26F File Offset: 0x002BC46F
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06007457 RID: 29783 RVA: 0x002BE289 File Offset: 0x002BC489
		public override int BaseValue
		{
			get
			{
				return -100;
			}
		}

		// Token: 0x06007458 RID: 29784 RVA: 0x002BE28D File Offset: 0x002BC48D
		public override string GetText(GameObject Actor)
		{
			return "Ruined the First Council of Omonporch.";
		}
	}
}
