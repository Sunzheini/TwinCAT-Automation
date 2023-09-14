using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace TcAutomation
{
    public class MessageFilter : IOleMessageFilter
    {
        public static void Register()
        {
            IOleMessageFilter newFilter = new MessageFilter();
            IOleMessageFilter oldFilter = null;
            int test = CoRegisterMessageFilter(newFilter, out oldFilter);

            if (test != 0)
            {
                Console.WriteLine(string.Format("CoRegisterMessageFilter failed with error : {0}", test));
            }
        }

        public static void Revoke()
        {
            IOleMessageFilter oldFilter = null;
            int test = CoRegisterMessageFilter(null, out oldFilter);
        }

        int IOleMessageFilter.HandleInComingCall(int dwCallType, System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr lpInterfaceInfo)
        {
            //returns the flag SERVERCALL_ISHANDLED. 
            return 0;
        }

        int IOleMessageFilter.RetryRejectedCall(System.IntPtr hTaskCallee, int dwTickCount, int dwRejectType)
        {
            // Thread call was refused, try again. 
            if (dwRejectType == 2)
            // flag = SERVERCALL_RETRYLATER. 
            {
                // retry thread call at once, if return value >=0 & 
                // <100. 
                return 99;
            }
            return -1;
        }

        int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee, int dwTickCount, int dwPendingType)
        {
            //return flag PENDINGMSG_WAITDEFPROCESS. 
            return 2;
        }

        // implement IOleMessageFilter interface. 
        [DllImport("Ole32.dll")]
        private static extern int CoRegisterMessageFilter(IOleMessageFilter newFilter, out IOleMessageFilter oldFilter);
    }

    [ComImport(), Guid("00000016-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    interface IOleMessageFilter
    {

        [PreserveSig]
        int HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo);


        [PreserveSig]
        int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType);


        [PreserveSig]
        int MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType);
    }
}
