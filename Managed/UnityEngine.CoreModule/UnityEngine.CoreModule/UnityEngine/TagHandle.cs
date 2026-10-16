using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020002B1 RID: 689
	[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	public struct TagHandle
	{
		// Token: 0x060021B8 RID: 8632 RVA: 0x00041B44 File Offset: 0x0003FD44
		public static TagHandle GetExistingTag(string tagName)
		{
			return new TagHandle
			{
				_tagIndex = TagHandle.ExtractTagThrowing(tagName)
			};
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x00041B67 File Offset: 0x0003FD67
		public override string ToString()
		{
			return TagHandle.TagToString(this._tagIndex);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x00041B74 File Offset: 0x0003FD74
		[NativeThrows]
		[FreeFunction]
		[NativeHeader("Runtime/Export/Scripting/GameObject.bindings.h")]
		private unsafe static uint ExtractTagThrowing(string tagName)
		{
			uint num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tagName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tagName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = TagHandle.ExtractTagThrowing_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x00041BCC File Offset: 0x0003FDCC
		private static string TagToString(uint tagIndex)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				TagHandle.TagToString_Injected(tagIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060021BC RID: 8636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint ExtractTagThrowing_Injected(ref ManagedSpanWrapper tagName);

		// Token: 0x060021BD RID: 8637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TagToString_Injected(uint tagIndex, out ManagedSpanWrapper ret);

		// Token: 0x040009DC RID: 2524
		private uint _tagIndex;
	}
}
