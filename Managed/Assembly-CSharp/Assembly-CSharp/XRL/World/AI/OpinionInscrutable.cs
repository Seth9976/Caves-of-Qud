using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B48 RID: 2888
	[GenerateSerializationPartial]
	public class OpinionInscrutable : IOpinionSubject
	{
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06007480 RID: 29824 RVA: 0x002BE4B1 File Offset: 0x002BC6B1
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007481 RID: 29825 RVA: 0x002BE4B4 File Offset: 0x002BC6B4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007482 RID: 29826 RVA: 0x002BE4CE File Offset: 0x002BC6CE
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06007483 RID: 29827 RVA: 0x002BE4E8 File Offset: 0x002BC6E8
		public override int BaseValue
		{
			get
			{
				return -500;
			}
		}

		// Token: 0x06007484 RID: 29828 RVA: 0x002BE4EF File Offset: 0x002BC6EF
		public override string GetText(GameObject Actor)
		{
			return "Inscrutable impulse.";
		}
	}
}
