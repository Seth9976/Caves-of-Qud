using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000569 RID: 1385
	internal sealed class NetEventSource : EventSource
	{
		// Token: 0x06002CAE RID: 11438 RVA: 0x000988F2 File Offset: 0x00096AF2
		[NonEvent]
		public static void Enter(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x0009891C File Offset: 0x00096B1C
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0})", NetEventSource.Format(arg0)));
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x00098946 File Offset: 0x00096B46
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1})", NetEventSource.Format(arg0), NetEventSource.Format(arg1)));
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x00098976 File Offset: 0x00096B76
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, object arg2, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1}, {2})", NetEventSource.Format(arg0), NetEventSource.Format(arg1), NetEventSource.Format(arg2)));
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000989AD File Offset: 0x00096BAD
		[Event(1, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Enter(string thisOrContextObject, string memberName, string parameters)
		{
			base.WriteEvent(1, thisOrContextObject, memberName ?? "(?)", parameters);
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000989C2 File Offset: 0x00096BC2
		[NonEvent]
		public static void Exit(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000989EC File Offset: 0x00096BEC
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(arg0).ToString());
			}
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x00098A11 File Offset: 0x00096C11
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, object arg1, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("{0}, {1}", NetEventSource.Format(arg0), NetEventSource.Format(arg1)));
			}
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x00098A41 File Offset: 0x00096C41
		[Event(2, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Exit(string thisOrContextObject, string memberName, string result)
		{
			base.WriteEvent(2, thisOrContextObject, memberName ?? "(?)", result);
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x00098A56 File Offset: 0x00096C56
		[NonEvent]
		public static void Info(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x00098A80 File Offset: 0x00096C80
		[NonEvent]
		public static void Info(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x00098AA5 File Offset: 0x00096CA5
		[Event(4, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void Info(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(4, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x00098ABA File Offset: 0x00096CBA
		[NonEvent]
		public static void Error(object thisOrContextObject, FormattableString formattableString, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(formattableString));
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x00098ADA File Offset: 0x00096CDA
		[NonEvent]
		public static void Error(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x00098AFF File Offset: 0x00096CFF
		[Event(5, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		private void ErrorMessage(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(5, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x00098B14 File Offset: 0x00096D14
		[NonEvent]
		public static void Fail(object thisOrContextObject, FormattableString formattableString, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(formattableString));
			}
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x00098B34 File Offset: 0x00096D34
		[NonEvent]
		public static void Fail(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x00098B59 File Offset: 0x00096D59
		[Event(6, Level = EventLevel.Critical, Keywords = (EventKeywords)2L)]
		private void CriticalFailure(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(6, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x00098B6E File Offset: 0x00096D6E
		[NonEvent]
		public static void DumpBuffer(object thisOrContextObject, byte[] buffer, [CallerMemberName] string memberName = null)
		{
			NetEventSource.DumpBuffer(thisOrContextObject, buffer, 0, buffer.Length, memberName);
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x00098B7C File Offset: 0x00096D7C
		[NonEvent]
		public static void DumpBuffer(object thisOrContextObject, byte[] buffer, int offset, int count, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				if (offset < 0 || offset > buffer.Length - count)
				{
					NetEventSource.Fail(thisOrContextObject, FormattableStringFactory.Create("Invalid {0} Args. Length={1}, Offset={2}, Count={3}", new object[] { "DumpBuffer", buffer.Length, offset, count }), memberName);
					return;
				}
				count = Math.Min(count, 1024);
				byte[] array = buffer;
				if (offset != 0 || count != buffer.Length)
				{
					array = new byte[count];
					Buffer.BlockCopy(buffer, offset, array, 0, count);
				}
				NetEventSource.Log.DumpBuffer(NetEventSource.IdOf(thisOrContextObject), memberName, array);
			}
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x00098C1C File Offset: 0x00096E1C
		[NonEvent]
		public unsafe static void DumpBuffer(object thisOrContextObject, IntPtr bufferPtr, int count, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				byte[] array = new byte[Math.Min(count, 1024)];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				Buffer.MemoryCopy((void*)bufferPtr, (void*)ptr, (long)array.Length, (long)array.Length);
				array2 = null;
				NetEventSource.Log.DumpBuffer(NetEventSource.IdOf(thisOrContextObject), memberName, array);
			}
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x00098C81 File Offset: 0x00096E81
		[Event(7, Level = EventLevel.Verbose, Keywords = (EventKeywords)2L)]
		private void DumpBuffer(string thisOrContextObject, string memberName, byte[] buffer)
		{
			this.WriteEvent(7, thisOrContextObject, memberName ?? "(?)", buffer);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x00098C96 File Offset: 0x00096E96
		[NonEvent]
		public static void Associate(object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(first), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x00098CBC File Offset: 0x00096EBC
		[NonEvent]
		public static void Associate(object thisOrContextObject, object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x00098CE2 File Offset: 0x00096EE2
		[Event(3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L, Message = "[{2}]<-->[{3}]")]
		private void Associate(string thisOrContextObject, string memberName, string first, string second)
		{
			this.WriteEvent(3, thisOrContextObject, memberName ?? "(?)", first, second);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x00098CF9 File Offset: 0x00096EF9
		[Conditional("DEBUG_NETEVENTSOURCE_MISUSE")]
		private static void DebugValidateArg(object arg)
		{
			bool isEnabled = NetEventSource.IsEnabled;
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG_NETEVENTSOURCE_MISUSE")]
		private static void DebugValidateArg(FormattableString arg)
		{
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x00098D01 File Offset: 0x00096F01
		public new static bool IsEnabled
		{
			get
			{
				return NetEventSource.Log.IsEnabled();
			}
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x00098D10 File Offset: 0x00096F10
		[NonEvent]
		public static string IdOf(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			return value.GetType().Name + "#" + NetEventSource.GetHashCode(value).ToString();
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x00098D49 File Offset: 0x00096F49
		[NonEvent]
		public static int GetHashCode(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return value.GetHashCode();
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x00098D58 File Offset: 0x00096F58
		[NonEvent]
		public static object Format(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			string text = null;
			if (text != null)
			{
				return text;
			}
			Array array = value as Array;
			if (array != null)
			{
				return string.Format("{0}[{1}]", array.GetType().GetElementType(), ((Array)value).Length);
			}
			ICollection collection = value as ICollection;
			if (collection != null)
			{
				return string.Format("{0}({1})", collection.GetType().Name, collection.Count);
			}
			SafeHandle safeHandle = value as SafeHandle;
			if (safeHandle != null)
			{
				return string.Format("{0}:{1}(0x{2:X})", safeHandle.GetType().Name, safeHandle.GetHashCode(), safeHandle.DangerousGetHandle());
			}
			if (value is IntPtr)
			{
				return string.Format("0x{0:X}", value);
			}
			string text2 = value.ToString();
			if (text2 == null || text2 == value.GetType().FullName)
			{
				return NetEventSource.IdOf(value);
			}
			return value;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x00098E44 File Offset: 0x00097044
		[NonEvent]
		private static string Format(FormattableString s)
		{
			switch (s.ArgumentCount)
			{
			case 0:
				return s.Format;
			case 1:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)));
			case 2:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)), NetEventSource.Format(s.GetArgument(1)));
			case 3:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)), NetEventSource.Format(s.GetArgument(1)), NetEventSource.Format(s.GetArgument(2)));
			default:
			{
				object[] arguments = s.GetArguments();
				object[] array = new object[arguments.Length];
				for (int i = 0; i < arguments.Length; i++)
				{
					array[i] = NetEventSource.Format(arguments[i]);
				}
				return string.Format(s.Format, array);
			}
			}
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x00098F18 File Offset: 0x00097118
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3, string arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				if (arg4 == null)
				{
					arg4 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text4 = arg4)
							{
								char* ptr4 = text4;
								if (ptr4 != null)
								{
									ptr4 += RuntimeHelpers.OffsetToStringData / 2;
								}
								EventSource.EventData* ptr5;
								checked
								{
									ptr5 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
								}
								*ptr5 = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr),
									Size = (arg1.Length + 1) * 2
								};
								ptr5[1] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr2),
									Size = (arg2.Length + 1) * 2
								};
								ptr5[2] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr3),
									Size = (arg3.Length + 1) * 2
								};
								ptr5[3] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr4),
									Size = (arg4.Length + 1) * 2
								};
								base.WriteEventCore(eventId, 4, ptr5);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000990A4 File Offset: 0x000972A4
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, byte[] arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = Array.Empty<byte>();
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						byte[] array;
						byte* ptr3;
						if ((array = arg3) == null || array.Length == 0)
						{
							ptr3 = null;
						}
						else
						{
							ptr3 = &array[0];
						}
						int num = arg3.Length;
						EventSource.EventData* ptr4;
						checked
						{
							ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
						}
						*ptr4 = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr),
							Size = (arg1.Length + 1) * 2
						};
						ptr4[1] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr2),
							Size = (arg2.Length + 1) * 2
						};
						ptr4[2] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)(&num)),
							Size = 4
						};
						ptr4[3] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr3),
							Size = num
						};
						base.WriteEventCore(eventId, 4, ptr4);
						array = null;
					}
				}
			}
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x00099208 File Offset: 0x00097408
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3, int arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					*ptr2 = new EventSource.EventData
					{
						DataPointer = (IntPtr)((void*)ptr),
						Size = (arg1.Length + 1) * 2
					};
					ptr2[1] = new EventSource.EventData
					{
						DataPointer = (IntPtr)((void*)(&arg2)),
						Size = 4
					};
					ptr2[2] = new EventSource.EventData
					{
						DataPointer = (IntPtr)((void*)(&arg3)),
						Size = 4
					};
					ptr2[3] = new EventSource.EventData
					{
						DataPointer = (IntPtr)((void*)(&arg4)),
						Size = 4
					};
					base.WriteEventCore(eventId, 4, ptr2);
				}
			}
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x0009930C File Offset: 0x0009750C
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, int arg2, string arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg3)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						EventSource.EventData* ptr3;
						checked
						{
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
						}
						*ptr3 = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr),
							Size = (arg1.Length + 1) * 2
						};
						ptr3[1] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)(&arg2)),
							Size = 4
						};
						ptr3[2] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr2),
							Size = (arg3.Length + 1) * 2
						};
						base.WriteEventCore(eventId, 3, ptr3);
					}
				}
			}
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x00099410 File Offset: 0x00097610
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, int arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						EventSource.EventData* ptr3;
						checked
						{
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
						}
						*ptr3 = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr),
							Size = (arg1.Length + 1) * 2
						};
						ptr3[1] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr2),
							Size = (arg2.Length + 1) * 2
						};
						ptr3[2] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)(&arg3)),
							Size = 4
						};
						base.WriteEventCore(eventId, 3, ptr3);
					}
				}
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x00099510 File Offset: 0x00097710
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3, int arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							EventSource.EventData* ptr4;
							checked
							{
								ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
							}
							*ptr4 = new EventSource.EventData
							{
								DataPointer = (IntPtr)((void*)ptr),
								Size = (arg1.Length + 1) * 2
							};
							ptr4[1] = new EventSource.EventData
							{
								DataPointer = (IntPtr)((void*)ptr2),
								Size = (arg2.Length + 1) * 2
							};
							ptr4[2] = new EventSource.EventData
							{
								DataPointer = (IntPtr)((void*)ptr3),
								Size = (arg3.Length + 1) * 2
							};
							ptr4[3] = new EventSource.EventData
							{
								DataPointer = (IntPtr)((void*)(&arg4)),
								Size = 4
							};
							base.WriteEventCore(eventId, 4, ptr4);
						}
					}
				}
			}
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x0009966E File Offset: 0x0009786E
		[Event(10, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void AcquireDefaultCredential(string packageName, global::Interop.SspiCli.CredentialUse intent)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(10, new object[] { packageName, intent });
			}
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x00099693 File Offset: 0x00097893
		[NonEvent]
		public void AcquireCredentialsHandle(string packageName, global::Interop.SspiCli.CredentialUse intent, object authdata)
		{
			if (base.IsEnabled())
			{
				this.AcquireCredentialsHandle(packageName, intent, NetEventSource.IdOf(authdata));
			}
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000996AB File Offset: 0x000978AB
		[Event(11, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void AcquireCredentialsHandle(string packageName, global::Interop.SspiCli.CredentialUse intent, string authdata)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(11, packageName, (int)intent, authdata);
			}
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000996C0 File Offset: 0x000978C0
		[NonEvent]
		public void InitializeSecurityContext(SafeFreeCredentials credential, SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags)
		{
			if (base.IsEnabled())
			{
				this.InitializeSecurityContext(NetEventSource.IdOf(credential), NetEventSource.IdOf(context), targetName, inFlags);
			}
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000996DF File Offset: 0x000978DF
		[Event(12, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		private void InitializeSecurityContext(string credential, string context, string targetName, global::Interop.SspiCli.ContextFlags inFlags)
		{
			this.WriteEvent(12, credential, context, targetName, (int)inFlags);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000996EE File Offset: 0x000978EE
		[NonEvent]
		public void AcceptSecurityContext(SafeFreeCredentials credential, SafeDeleteContext context, global::Interop.SspiCli.ContextFlags inFlags)
		{
			if (base.IsEnabled())
			{
				this.AcceptSecurityContext(NetEventSource.IdOf(credential), NetEventSource.IdOf(context), inFlags);
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x0009970B File Offset: 0x0009790B
		[Event(15, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		private void AcceptSecurityContext(string credential, string context, global::Interop.SspiCli.ContextFlags inFlags)
		{
			this.WriteEvent(15, credential, context, (int)inFlags);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x00099718 File Offset: 0x00097918
		[Event(16, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void OperationReturnedSomething(string operation, global::Interop.SECURITY_STATUS errorCode)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(16, new object[] { operation, errorCode });
			}
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x0009973D File Offset: 0x0009793D
		[Event(13, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void SecurityContextInputBuffer(string context, int inputBufferSize, int outputBufferSize, global::Interop.SECURITY_STATUS errorCode)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(13, context, inputBufferSize, outputBufferSize, (int)errorCode);
			}
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x00099754 File Offset: 0x00097954
		[Event(14, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void SecurityContextInputBuffers(string context, int inputBuffersSize, int outputBufferSize, global::Interop.SECURITY_STATUS errorCode)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(14, context, inputBuffersSize, outputBufferSize, (int)errorCode);
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x0009976B File Offset: 0x0009796B
		[Event(8, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void EnumerateSecurityPackages(string securityPackage)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(8, securityPackage ?? "");
			}
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x00099786 File Offset: 0x00097986
		[Event(9, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void SspiPackageNotFound(string packageName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(9, packageName ?? "");
			}
		}

		// Token: 0x04001839 RID: 6201
		public static readonly NetEventSource Log = new NetEventSource();

		// Token: 0x0400183A RID: 6202
		private const string MissingMember = "(?)";

		// Token: 0x0400183B RID: 6203
		private const string NullInstance = "(null)";

		// Token: 0x0400183C RID: 6204
		private const string StaticMethodObject = "(static)";

		// Token: 0x0400183D RID: 6205
		private const string NoParameters = "";

		// Token: 0x0400183E RID: 6206
		private const int MaxDumpSize = 1024;

		// Token: 0x0400183F RID: 6207
		private const int EnterEventId = 1;

		// Token: 0x04001840 RID: 6208
		private const int ExitEventId = 2;

		// Token: 0x04001841 RID: 6209
		private const int AssociateEventId = 3;

		// Token: 0x04001842 RID: 6210
		private const int InfoEventId = 4;

		// Token: 0x04001843 RID: 6211
		private const int ErrorEventId = 5;

		// Token: 0x04001844 RID: 6212
		private const int CriticalFailureEventId = 6;

		// Token: 0x04001845 RID: 6213
		private const int DumpArrayEventId = 7;

		// Token: 0x04001846 RID: 6214
		private const int EnumerateSecurityPackagesId = 8;

		// Token: 0x04001847 RID: 6215
		private const int SspiPackageNotFoundId = 9;

		// Token: 0x04001848 RID: 6216
		private const int AcquireDefaultCredentialId = 10;

		// Token: 0x04001849 RID: 6217
		private const int AcquireCredentialsHandleId = 11;

		// Token: 0x0400184A RID: 6218
		private const int InitializeSecurityContextId = 12;

		// Token: 0x0400184B RID: 6219
		private const int SecurityContextInputBufferId = 13;

		// Token: 0x0400184C RID: 6220
		private const int SecurityContextInputBuffersId = 14;

		// Token: 0x0400184D RID: 6221
		private const int AcceptSecuritContextId = 15;

		// Token: 0x0400184E RID: 6222
		private const int OperationReturnedSomethingId = 16;

		// Token: 0x0400184F RID: 6223
		private const int NextAvailableEventId = 17;

		// Token: 0x0200056A RID: 1386
		public class Keywords
		{
			// Token: 0x04001850 RID: 6224
			public const EventKeywords Default = (EventKeywords)1L;

			// Token: 0x04001851 RID: 6225
			public const EventKeywords Debug = (EventKeywords)2L;

			// Token: 0x04001852 RID: 6226
			public const EventKeywords EnterExit = (EventKeywords)4L;
		}
	}
}
