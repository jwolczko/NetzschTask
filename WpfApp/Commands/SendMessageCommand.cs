using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Windows.Input;
using WpfApp.ViewModels;

namespace WpfApp.Commands
{
    public class SendMessageCommand : ICommand
    {
        private MainWindowViewModel _mainWindowViewModel;
        public SendMessageCommand(MainWindowViewModel mainWindowViewModel ) 
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
            //return !string.IsNullOrWhiteSpace(_mainWindowViewModel.MessageToSend);
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (_mainWindowViewModel == null || string.IsNullOrWhiteSpace(_mainWindowViewModel.MessageToSend))
                    return;

                await _mainWindowViewModel.HubConnection.SendAsync("SendMessage", _mainWindowViewModel.MessageToSend).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _mainWindowViewModel.RecievedMessage = ex.Message;
            }
        }
    }
}
