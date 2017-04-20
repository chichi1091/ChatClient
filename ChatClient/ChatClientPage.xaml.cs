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
		private IWebSocketConnection connection;
		public ChatClientPage()
		{
			InitializeComponent();

			connection = Websockets.WebSocketFactory.Create();
			connection.OnLog += Connection_OnLog;
			connection.OnError += Connection_OnError;
			connection.OnMessage += Connection_OnMessage;
			connection.OnOpened += Connection_OnOpened;
			connection.Open("wss://submeeting.herokuapp.com/websocket");
		}

		private void BtnClieckd(object sender, EventArgs e) {
			connection.Send(this.name.Text + ": " + this.message.Text);
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
