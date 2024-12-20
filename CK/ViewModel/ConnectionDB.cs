namespace CK.ViewModel
{
    public class ConnectionDB
    {
        public string AxdbConnection = string.Format("Server=192.168.1.210;User ID=sa;Password=P@ssw0rd;Database=AXDB;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string RichCutConnection = string.Format("Server=192.168.111.2;User ID=sa;Password=P@ssw0rd;Database=Richcut;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string RmsConnection = string.Format("Server=192.168.1.156;User ID=saApp;Password=P@ssw0rd;Database=DATA_CENTER;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string RmsBeforeConnection = string.Format("Server=192.168.1.156;User ID=saApp;Password=P@ssw0rd;Database=DATA_CENTER_Prev_Yrs;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string TopSoftConnection = string.Format("Server=192.168.1.208\\NEW;User ID=sa;Password=P@ssw0rd;Database=TopSoft;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        public string EasySoftConnection = string.Format("Server=192.168.1.76\\sql2016;User ID=mohamed;Password=P@ssw0rd12345;Database=Easysoft;Connect Timeout=10200;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
    }
}
