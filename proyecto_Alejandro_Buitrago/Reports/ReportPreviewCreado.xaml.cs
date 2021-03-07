using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.Clientes;

namespace proyecto_Alejandro_Buitrago.Reports
{
    /// <summary>
    /// Lógica de interacción para ReportPreviewCreado.xaml
    /// </summary>
    public partial class ReportPreviewCreado : Window
    {
        public ReportPreviewCreado()
        {
            InitializeComponent();
        }

        public bool GenerarInforme(string nFactura2)
        {
            DataTable informe = ClientesDBHandler.ObtenerNFactura(nFactura2);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DatosFormulario";
            rds.Value = informe;
            myReportView.LocalReport.ReportPath = "../../Reports/InformeGenerado.rdlc";
            myReportView.LocalReport.DataSources.Add(rds);
            myReportView.RefreshReport();

            bool ok = false;
            if(informe.Rows.Count > 0)
            {
                ok = true;
            }
            return ok;
        }
    }
}
