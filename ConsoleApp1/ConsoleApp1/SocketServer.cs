using Autofac.Annotation;
using HPSocket;
using HPSocket.Tcp;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    [Component("SocketClient", AutofacScope = AutofacScope.SingleInstance, AutoActivate = true, InitMethod = nameof(SocketClient.Start))]
    public class SocketClient
    {
        [Autowired]
        private ILogger<SocketClient> logger;

        public void Start()
        {
            ITcpClient tcpClient = new TcpClient();
            // 缓冲区大小,4kb
            tcpClient.SocketBufferSize = 4096;
            tcpClient.Async = true;
            tcpClient.Address = "172.16.9.75";
            tcpClient.Port = 5013;
            tcpClient.OnConnect += TcpClient_OnConnect; ;
            tcpClient.OnClose += TcpClient_OnClose; ;
            tcpClient.OnReceive += TcpClient_OnReceive; ;
            tcpClient.KeepAliveInterval = 3000;
            tcpClient.KeepAliveTime = 3000;
            tcpClient.Async = true;
            tcpClient.Connect();
        }

        private HandleResult TcpClient_OnReceive(IClient sender, byte[] data)
        {
            logger.LogDebug("TcpClient_OnReceive");
            return HandleResult.Ok;
        }

        private HandleResult TcpClient_OnClose(IClient sender, SocketOperation socketOperation, int errorCode)
        {
            logger.LogDebug($"TcpClient_OnClose#socketOperation:{socketOperation}");
            return HandleResult.Ok;
        }

        private HandleResult TcpClient_OnConnect(IClient sender)
        {
            logger.LogDebug("TcpClient_OnConnect");
            return HandleResult.Ok;
        }
    }
}
