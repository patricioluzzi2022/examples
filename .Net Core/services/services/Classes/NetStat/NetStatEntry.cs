using System;
using System.Collections.Generic;
using System.Text;

namespace services.Classes.NetStat
{
    public class NetStatEntry
    {
        public string Protocol { get; set; }
        public string LocalAddress { get; set; }
        public string ForeignAddress { get; set; }
        public string State { get; set; }
        public int ProcessId { get; set; }
        public double CPU { get; set; }
        public double Memory { get; set; }
        public string CertificateSubject { get; set; }
        public string Note { get; set; }



    }
}
