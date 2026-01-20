using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Text.Json;

namespace services.Classes
{
    class WindowServiceManagement
    {
        protected List<WindowService> services;
        public WindowServiceManagement()
        {
            services = ServiceController.GetServices()
                .OrderBy(s => s.DisplayName)
                .Select(s => new WindowService(s)).ToList();
        }

        public ObservableCollection<WindowService> GetServices()
        {
            ObservableCollection<WindowService> result = new ObservableCollection<WindowService>();
            foreach (var s in services)
                result.Add(s);
            return result;
        }

        public string ConvertToJson() {
            return JsonSerializer.Serialize(
                services,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });
        }


    }
}
