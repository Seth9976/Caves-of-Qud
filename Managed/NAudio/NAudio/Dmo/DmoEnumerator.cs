using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	// Token: 0x02000086 RID: 134
	public class DmoEnumerator
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00009F82 File Offset: 0x00008182
		public static IEnumerable<DmoDescriptor> GetAudioEffectNames()
		{
			return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_EFFECT);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009F8E File Offset: 0x0000818E
		public static IEnumerable<DmoDescriptor> GetAudioEncoderNames()
		{
			return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_ENCODER);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009F9A File Offset: 0x0000819A
		public static IEnumerable<DmoDescriptor> GetAudioDecoderNames()
		{
			return DmoEnumerator.GetDmos(DmoGuids.DMOCATEGORY_AUDIO_DECODER);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A0FC File Offset: 0x000082FC
		private static IEnumerable<DmoDescriptor> GetDmos(Guid category)
		{
			IEnumDmo enumDmo;
			int hresult = DmoInterop.DMOEnum(ref category, DmoEnumFlags.None, 0, null, 0, null, out enumDmo);
			Marshal.ThrowExceptionForHR(hresult);
			int itemsFetched;
			do
			{
				Guid guid;
				IntPtr namePointer;
				enumDmo.Next(1, out guid, out namePointer, out itemsFetched);
				if (itemsFetched == 1)
				{
					string name = Marshal.PtrToStringUni(namePointer);
					Marshal.FreeCoTaskMem(namePointer);
					yield return new DmoDescriptor(name, guid);
				}
			}
			while (itemsFetched > 0);
			yield break;
		}
	}
}
