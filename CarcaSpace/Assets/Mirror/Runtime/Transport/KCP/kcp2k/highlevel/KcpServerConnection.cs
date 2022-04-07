using System.Net;
using System.Net.Sockets;

namespace kcp2k
{
    public class KcpServerConnection : KcpConnection
    {
<<<<<<< HEAD
        // Constructor & Send functions can be overwritten for where-allocation:
        // https://github.com/vis2k/where-allocation
        public KcpServerConnection(Socket socket, EndPoint remoteEndPoint, bool noDelay, uint interval = Kcp.INTERVAL, int fastResend = 0, bool congestionWindow = true, uint sendWindowSize = Kcp.WND_SND, uint receiveWindowSize = Kcp.WND_RCV, int timeout = DEFAULT_TIMEOUT, uint maxRetransmits = Kcp.DEADLINK)
        {
            this.socket = socket;
            this.remoteEndPoint = remoteEndPoint;
            SetupKcp(noDelay, interval, fastResend, congestionWindow, sendWindowSize, receiveWindowSize, timeout, maxRetransmits);
=======
        public KcpServerConnection(Socket socket, EndPoint remoteEndpoint, bool noDelay, uint interval = Kcp.INTERVAL, int fastResend = 0, bool congestionWindow = true, uint sendWindowSize = Kcp.WND_SND, uint receiveWindowSize = Kcp.WND_RCV)
        {
            this.socket = socket;
            this.remoteEndpoint = remoteEndpoint;
            SetupKcp(noDelay, interval, fastResend, congestionWindow, sendWindowSize, receiveWindowSize);
>>>>>>> origin/alpha_merge
        }

        protected override void RawSend(byte[] data, int length)
        {
<<<<<<< HEAD
            socket.SendTo(data, 0, length, SocketFlags.None, remoteEndPoint);
=======
            socket.SendTo(data, 0, length, SocketFlags.None, remoteEndpoint);
>>>>>>> origin/alpha_merge
        }
    }
}
