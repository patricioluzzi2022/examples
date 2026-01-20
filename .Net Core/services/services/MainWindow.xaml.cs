using services.Classes;
using services.Classes.Worker;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace services
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private WindowServiceManagement windowServiceManagment = new WindowServiceManagement();
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }
        public ICommand CreateJsonFileCommand { get; set; }
        public ICommand StopWorkerCommand { get; set; }
        public ICommand ScannerCommand { get; set; }
        public ObservableCollection<WindowService> Servicios { get; set; }

        private WindowService _selectedService;
        public WindowService SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged();
            }
        }

        private int _sortIndex = 0;

        private Dictionary<string, PeriodicWorker> _workers = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadMenuCommands();
            LoadServices();
            DataContext = mainWindow;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(
            [CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void LoadMenuCommands()
        {
            StartCommand = new RelayCommand(p => StartService(p as WindowService), p => p is WindowService);
            StopCommand = new RelayCommand(p => StopService(p as WindowService), p => p is WindowService && ((WindowService)p).ServiceInfo.CanStop);
            ShowDetailsCommand = new RelayCommand(p => ShowDetails(p as WindowService), p => p is WindowService);
            CreateJsonFileCommand = new RelayCommand(p => CreateJsonFile(), null);
            StopWorkerCommand = new RelayCommand(p => StopWorker(p as WindowService), p => p is WindowService);
            ScannerCommand = new RelayCommand(p => Scanner(), null);
        }

        private void LoadServices()
        {
            Servicios = windowServiceManagment.GetServices();
        }

        private void StartService(WindowService service)
        {
            service.Start();
            LoadServices();
        }

        private void StopService(WindowService service)
        {
            service.Stop();
            LoadServices();
        }

        private void ShowDetails(WindowService service)
        {
            gMain.ColumnDefinitions[1].Width = new GridLength(480);
            service.ShowDetails();

            if(service.ServiceInfo.NetStat.Count != 0)
            {
                CreateNewPeriodWorker(service);
            }
        }

        private void CreateJsonFile()
        {
            string json = windowServiceManagment.ConvertToJson();
            File.WriteAllText("servicios.json", json);
        }

        private void InformationClose(object sender, RoutedEventArgs e)
        {
            gMain.ColumnDefinitions[1].Width = new GridLength(0);
        }

        public void SortByStatus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader header)
            {
                if(header.Content.ToString() == "Status")
                {
                    var direction = ListSortDirection.Ascending;
                    var ServiciosView = CollectionViewSource.GetDefaultView(Servicios);

                    switch (_sortIndex) {
                        case 0:
                            direction = ListSortDirection.Descending;
                            _sortIndex++;
                            break;
                        case 1:
                            direction = ListSortDirection.Ascending;
                            _sortIndex++;
                            break;
                        default:
                            ServiciosView.SortDescriptions.Clear();
                            _sortIndex = 0;
                            return;
                    }

                    ServiciosView.SortDescriptions.Clear();
                    ServiciosView.SortDescriptions.Add(
                        new SortDescription("ServiceInfo.Status", direction));
                }
            }
        }

        public void CreateNewPeriodWorker(WindowService service)
        {
            var worker = new PeriodicWorker(
                TimeSpan.FromSeconds(5),
                async token =>
                {
                    // \

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        service.RefreshDetails();
                    });
                });

            worker.Start();

            _workers[service.ServiceInfo.ServiceName] = worker;
        }

        public void RefeshService(WindowService service)
        {
            ShowDetails(service);
        }

        public void StopWorker(WindowService service)
        {
            _workers[service.ServiceInfo.ServiceName].Stop();
        }

        public void Scanner()
        {
            foreach (var service in Servicios)
            {
                if(service.ServiceInfo.Status == ServiceControllerStatus.Running)
                {
                    service.ShowDetails();

                    if(service.ServiceInfo.NetStat.Count != 0)
                    {
                        foreach (var stat in service.ServiceInfo.NetStat)
                        {
                            if(stat.CPU >= 0.1)
                            {
                                service.Stop();
                                return;
                            }

                            if (stat.Memory == 2.2 || stat.Memory == 0.5 || stat.Memory == 0.1)
                            {
                                service.Stop();
                                return;
                            }

                            if (stat.CertificateSubject == null || stat.CertificateSubject == "read the notes" || stat.CertificateSubject.Trim() != "")
                            {
                                service.Stop();
                                return;
                            }
                        }
                    }

                }
            }
        }
    }
}

