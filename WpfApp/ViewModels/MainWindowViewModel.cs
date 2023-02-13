using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _messageToSend;
        private string _recievedMessage;
        private ICommand sendMessage;
        private HubConnection _hubConnection;

        public string MessageToSend
        {
            get => _messageToSend;
            set
            {
                SetProperty(ref _messageToSend, value);
                //CommandManager.CanExecuteEvent()
            }
        }

        public string RecievedMessage
        {
            get => _recievedMessage;
            set => SetProperty(ref _recievedMessage, value);
        }

        public ICommand SendMessage
        {
            get => sendMessage;
            set => SetProperty(ref sendMessage, value);
        }

        public HubConnection HubConnection => _hubConnection;

        public MainWindowViewModel()
        {
            SendMessage = new SendMessageCommand(this);
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5270/message")
                .Build();
            _hubConnection.On<string>("RecievedMessage", OnRevieceMessage);

            _hubConnection.StartAsync().Wait();
        }

        private void OnRevieceMessage(string recievedMessage)
        {
            RecievedMessage = recievedMessage;
        }
    }
}
