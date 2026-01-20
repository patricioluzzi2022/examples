using services.Classes.NetStat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Text;

namespace services.Classes
{
    public class WindowServiceInfo : INotifyPropertyChanged
    {
        public string ServiceName { get; set; }
        public string DisplayName { get; set; }
        private string _description { get; set; }
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private ServiceControllerStatus _status;
        public ServiceControllerStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool CanStop { get; set; }
        private int _processId { get; set; }
        public int ProcessId
        {
            get => _processId;
            set
            {
                if (_processId != value)
                {
                    _processId = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _startMode { get; set; }
        public string StartMode
        {
            get => _startMode;
            set
            {
                if (_startMode != value)
                {
                    _startMode = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _pathName { get; set; }
        public string PathName
        {
            get => _pathName;
            set
            {
                if (_pathName != value)
                {
                    _pathName = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<NetStatEntry> _netStat { get; set; }
        public List<NetStatEntry> NetStat
        {
            get => _netStat;
            set
            {
                if (_netStat != value)
                {
                    _netStat = value;
                    OnPropertyChanged();
                }
            }
        }

        private NetStatEntry _selectedNetStatEntry;
        public NetStatEntry SelectedNetStatEntry
        {
            get => _selectedNetStatEntry;
            set
            {
                _selectedNetStatEntry = value;
                OnPropertyChanged();
            }
        }
        private DateTime? _lastUpdate { get; set; }
        public DateTime? LastUpdate
        {
            get => _lastUpdate;
            set
            {
                if (_lastUpdate != value)
                {
                    _lastUpdate = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
