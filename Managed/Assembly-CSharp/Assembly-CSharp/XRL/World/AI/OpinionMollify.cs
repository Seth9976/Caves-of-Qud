using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B4A RID: 2890
	[GenerateSerializationPartial]
	public class OpinionMollify : IOpinionSubject
	{
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600748D RID: 29837 RVA: 0x002BE587 File Offset: 0x002BC787
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600748E RID: 29838 RVA: 0x002BE58A File Offset: 0x002BC78A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x0600748F RID: 29839 RVA: 0x002BE5A4 File Offset: 0x002BC7A4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06007490 RID: 29840 RVA: 0x002BE5BE File Offset: 0x002BC7BE
		public override int BaseValue
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06007491 RID: 29841 RVA: 0x002BE5C1 File Offset: 0x002BC7C1
		public override float Limit
		{
			get
			{
				return 1000f;
			}
		}

		// Token: 0x06007492 RID: 29842 RVA: 0x002BE5C8 File Offset: 0x002BC7C8
		public override void Initialize(GameObject Actor, GameObject Subject)
		{
			int num = Actor.Brain.GetFeeling(Subject) * -1;
			if ((float)num > this.Magnitude)
			{
				this.Magnitude = (float)num;
			}
		}

		// Token: 0x06007493 RID: 29843 RVA: 0x002BE5F6 File Offset: 0x002BC7F6
		public override string GetText(GameObject Actor)
		{
			return "Mollified.";
		}
	}
}
