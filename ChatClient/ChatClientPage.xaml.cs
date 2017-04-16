using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebSocket.Portable;
using WebSocket.Portable.Interfaces;
using Websockets;
using Xamarin.Forms;

namespace ChatClient
{
	public partial class ChatClientPage : ContentPage
	{
		private IWebSocketConnection client;
		public ChatClientPage()
		{
			InitializeComponent();
			Connect();
		}

		private void Connect()
		{
			client = Websockets.WebSocketFactory.Create();
			client.OnLog += Connection_OnLog;
			client.OnError += Connection_OnError;
			client.OnMessage += Connection_OnMessage;
			client.OnOpened += Connection_OnOpened;
			client.Open("wss://submeeting.herokuapp.com/websocket");
		}

		private void BtnClieckd(object sender, EventArgs e) {
			client.Send(this.name.Text + ": " + this.message.Text);
		}

		private void Connection_OnOpened()
		{
			Debug.WriteLine("Connection_OnOpened");
		}

		private void Connection_OnMessage(String obj)
		{
			var label = new Label{ Text = obj.ToString() };
			this.sl.Children.Add(label);
		}

		private void Connection_OnError(string obj)
		{
			Debug.WriteLine("ERROR " + obj);
		}

		private void Connection_OnLog(string obj)
		{
			Debug.WriteLine(obj);
		}
	}
}
