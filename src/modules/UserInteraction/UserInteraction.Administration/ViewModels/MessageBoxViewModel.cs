﻿using System.Windows.Forms;
using Anapher.Wpf.Swan.ViewInterface;
using Orcus.Administration.Library.Clients;
using Orcus.Administration.Library.Extensions;
using Orcus.Administration.Library.StatusBar;
using Prism.Commands;
using Prism.Mvvm;
using Unclassified.TxLib;
using UserInteraction.Administration.Extensions;
using UserInteraction.Administration.Rest;
using UserInteraction.Dtos.MessageBox;

namespace UserInteraction.Administration.ViewModels
{
    public class MessageBoxViewModel : BindableBase
    {
        private readonly IWindow _window;
        private readonly IShellStatusBar _statusBar;
        private readonly IPackageRestClient _restClient;
        private string _caption;
        private MsgBxButtons _messageBoxButtons;
        private MsgBxIcon _messageBoxIcon;
        private DelegateCommand _sendCommand;
        private DelegateCommand _testCommand;
        private string _text;

        public MessageBoxViewModel(ITargetedRestClient restClient, IWindow window, IShellStatusBar statusBar)
        {
            _window = window;
            _statusBar = statusBar;
            _restClient = restClient.CreateLocal();
        }

        public MsgBxIcon MessageBoxIcon
        {
            get => _messageBoxIcon;
            set => SetProperty(ref _messageBoxIcon, value);
        }

        public MsgBxButtons MessageBoxButtons
        {
            get => _messageBoxButtons;
            set => SetProperty(ref _messageBoxButtons, value);
        }

        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public DelegateCommand TestCommand
        {
            get
            {
                return _testCommand ?? (_testCommand = new DelegateCommand(() =>
                {
                    MessageBox.Show(Text, Caption, (MessageBoxButtons) MessageBoxButtons,
                        SystemIconToMessageBoxIcon(MessageBoxIcon));
                }));
            }
        }

        public DelegateCommand SendCommand
        {
            get
            {
                return _sendCommand ?? (_sendCommand = new DelegateCommand(async () =>
                {
                    var dto = new OpenMessageBoxDto
                    {
                        Caption = Caption,
                        Text = Text,
                        Icon = MessageBoxIcon,
                        Buttons = MessageBoxButtons
                    };

                    var result = await MessageBoxResource.OpenAsync(dto, _restClient)
                        .DisplayOnStatusBar(_statusBar, Tx.T("UserInteraction:MessageBox.SendingMessage"),
                            StatusBarAnimation.Send).OnErrorShowMessageBox(_window);
                    if (!result.Failed)
                    {
                        _statusBar.ShowSuccess(Tx.T("UserInteraction:MessageBox.MessageBoxClosed", "result", result.Result.ToString()));
                    }
                }));
            }
        }

        private static MessageBoxIcon SystemIconToMessageBoxIcon(MsgBxIcon icon)
        {
            switch (icon)
            {
                case MsgBxIcon.Error:
                    return System.Windows.Forms.MessageBoxIcon.Error;
                case MsgBxIcon.Info:
                    return System.Windows.Forms.MessageBoxIcon.Information;
                case MsgBxIcon.Warning:
                    return System.Windows.Forms.MessageBoxIcon.Warning;
                case MsgBxIcon.Question:
                    return System.Windows.Forms.MessageBoxIcon.Question;
                default:
                    return System.Windows.Forms.MessageBoxIcon.None;
            }
        }
    }
}