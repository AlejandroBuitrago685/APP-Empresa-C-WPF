using Microsoft.Reporting.WinForms;
using proyecto_Alejandro_Buitrago.ProjectDB.SQLData.Clientes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace proyecto_Alejandro_Buitrago.Reports
{
    /// <summary>
    /// Lógica de interacción para InformeCIF.xaml
    /// </summary>
    public partial class InformeCIF : Window
    {
        public InformeCIF()
        {
            InitializeComponent();
        }

        public bool ObtenerPorCIF(string cif)
        {
            DataTable informe = ClientesDBHandler.ObtenerPorCif(cif);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DatosInforme";
            rds.Value = informe;
            myReportView.LocalReport.ReportPath = "../../Reports/InformePorCif.rdlc";
            myReportView.LocalReport.DataSources.Add(rds);
            myReportView.RefreshReport();

            bool ok = false;
            if (informe.Rows.Count > 0)
            {
                ok = true;
            }
            return ok;
        }

        public bool ObtenerPorFactura(string factura)
        {
            DataTable informe = ClientesDBHandler.ObtenerNFactura(factura);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DatosInforme";
            rds.Value = informe;
            myReportView.LocalReport.ReportPath = "../../Reports/InformePorFactura.rdlc";
            myReportView.LocalReport.DataSources.Add(rds);
            myReportView.RefreshReport();

            bool ok = false;
            if (informe.Rows.Count > 0)
            {
                ok = true;
            }
            return ok;
        }

        public bool ObtenerPorFechas(DateTime dateTime1, DateTime dateTime2)
        {
            DataTable informe = ClientesDBHandler.ObtenerRangoFechas(dateTime1, dateTime2);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DatosInforme";
            rds.Value = informe;
            myReportView.LocalReport.ReportPath = "../../Reports/InformePorFechas.rdlc";
            myReportView.LocalReport.DataSources.Add(rds);
            myReportView.RefreshReport();

            bool ok = false;
            if (informe.Rows.Count > 0)
            {
                ok = true;
            }
            return ok;
        }
    }
}
