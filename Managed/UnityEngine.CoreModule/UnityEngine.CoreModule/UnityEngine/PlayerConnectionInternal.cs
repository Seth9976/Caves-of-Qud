using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000229 RID: 553
	[NativeHeader("Runtime/Export/PlayerConnection/PlayerConnectionInternal.bindings.h")]
	internal class PlayerConnectionInternal : IPlayerEditorConnectionNative
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x00039EC8 File Offset: 0x000380C8
		void IPlayerEditorConnectionNative.SendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			PlayerConnectionInternal.SendMessage(messageId.ToString("N"), data, playerId);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00039F08 File Offset: 0x00038108
		bool IPlayerEditorConnectionNative.TrySendMessage(Guid messageId, byte[] data, int playerId)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("messageId must not be empty");
			}
			return PlayerConnectionInternal.TrySendMessage(messageId.ToString("N"), data, playerId);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00039F48 File Offset: 0x00038148
		void IPlayerEditorConnectionNative.Poll()
		{
			PlayerConnectionInternal.PollInternal();
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00039F51 File Offset: 0x00038151
		void IPlayerEditorConnectionNative.RegisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.RegisterInternal(messageId.ToString("N"));
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00039F66 File Offset: 0x00038166
		void IPlayerEditorConnectionNative.UnregisterInternal(Guid messageId)
		{
			PlayerConnectionInternal.UnregisterInternal(messageId.ToString("N"));
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00039F7B File Offset: 0x0003817B
		void IPlayerEditorConnectionNative.Initialize()
		{
			PlayerConnectionInternal.Initialize();
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00039F84 File Offset: 0x00038184
		bool IPlayerEditorConnectionNative.IsConnected()
		{
			return PlayerConnectionInternal.IsConnected();
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00039F9B File Offset: 0x0003819B
		void IPlayerEditorConnectionNative.DisconnectAll()
		{
			PlayerConnectionInternal.DisconnectAll();
		}

		// Token: 0x06001E25 RID: 7717
		[FreeFunction("PlayerConnection_Bindings::IsConnected")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsConnected();

		// Token: 0x06001E26 RID: 7718
		[FreeFunction("PlayerConnection_Bindings::Initialize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Initialize();

		// Token: 0x06001E27 RID: 7719 RVA: 0x00039FA4 File Offset: 0x000381A4
		[FreeFunction("PlayerConnection_Bindings::RegisterInternal")]
		private unsafe static void RegisterInternal(string messageId)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(messageId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = messageId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				PlayerConnectionInternal.RegisterInternal_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00039FF8 File Offset: 0x000381F8
		[FreeFunction("PlayerConnection_Bindings::UnregisterInternal")]
		private unsafe static void UnregisterInternal(string messageId)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(messageId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = messageId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				PlayerConnectionInternal.UnregisterInternal_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0003A04C File Offset: 0x0003824C
		[FreeFunction("PlayerConnection_Bindings::SendMessage")]
		private unsafe static void SendMessage(string messageId, byte[] data, int playerId)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(messageId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = messageId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Span<byte> span = new Span<byte>(data);
				fixed (byte* ptr2 = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span.Length);
					PlayerConnectionInternal.SendMessage_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, playerId);
				}
			}
			finally
			{
				char* ptr = null;
				byte* ptr2 = null;
			}
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0003A0CC File Offset: 0x000382CC
		[FreeFunction("PlayerConnection_Bindings::TrySendMessage")]
		private unsafe static bool TrySendMessage(string messageId, byte[] data, int playerId)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(messageId, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = messageId.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Span<byte> span = new Span<byte>(data);
				fixed (byte* ptr2 = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span.Length);
					flag = PlayerConnectionInternal.TrySendMessage_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, playerId);
				}
			}
			finally
			{
				char* ptr = null;
				byte* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x06001E2B RID: 7723
		[FreeFunction("PlayerConnection_Bindings::PollInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PollInternal();

		// Token: 0x06001E2C RID: 7724
		[FreeFunction("PlayerConnection_Bindings::DisconnectAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisconnectAll();

		// Token: 0x06001E2E RID: 7726
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterInternal_Injected(ref ManagedSpanWrapper messageId);

		// Token: 0x06001E2F RID: 7727
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterInternal_Injected(ref ManagedSpanWrapper messageId);

		// Token: 0x06001E30 RID: 7728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMessage_Injected(ref ManagedSpanWrapper messageId, ref ManagedSpanWrapper data, int playerId);

		// Token: 0x06001E31 RID: 7729
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySendMessage_Injected(ref ManagedSpanWrapper messageId, ref ManagedSpanWrapper data, int playerId);

		// Token: 0x0200022A RID: 554
		[Flags]
		public enum MulticastFlags
		{
			// Token: 0x040008F8 RID: 2296
			kRequestImmediateConnect = 1,
			// Token: 0x040008F9 RID: 2297
			kSupportsProfile = 2,
			// Token: 0x040008FA RID: 2298
			kCustomMessage = 4,
			// Token: 0x040008FB RID: 2299
			kUseAlternateIP = 8
		}
	}
}
