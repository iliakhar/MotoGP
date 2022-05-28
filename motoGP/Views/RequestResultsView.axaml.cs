using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Data;
using System.Data;
using Avalonia.Interactivity;
using System;
using motoGP.ViewModels;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace motoGP.Views
{
    public partial class RequestResultsView : UserControl
    {
        public RequestResultsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            dGrid = this.FindControl<global::Avalonia.Controls.DataGrid>("dGrid");
        }

        public void OnSelect(object sender, SelectionChangedEventArgs args)
        {
            var context = DataContext as RequestResultsViewModel;
            if (context != null && context.TableInd >= 0) 
            {
                string sql = context.StrReq[context.TableInd];
                string connectionStr = "Data Source=MotoGP.sqlite3;Mode=ReadWrite";
                SQLiteConnection sql_con;
                

                using (sql_con = new SQLiteConnection(connectionStr))
                {
                    try
                    {
                        sql_con.Open();
                        SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                        DataTable tablesNames = new DataTable();
                        DataTable table = new DataTable();
                        table.Load(command.ExecuteReader());
                        //context.Tables.Tables.Add(table);
                        ChangeTable(table);

                    }
                    catch{ }
                    
                }
            }
                
        }
        void ChangeTable(DataTable table)
        {
            int i = 0;
            var context = DataContext as RequestResultsViewModel;
            int Index = i;
            for (int ind = dGrid.Columns.Count - 1; ind >= 0; ind--)
            {
                dGrid.Columns.RemoveAt(ind);
            }
            /* dGrid.Columns.Clear();*/
            dGrid.Items = table.Rows;
            foreach (DataColumn col in table.Columns)
            {
                dGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = col.ColumnName,
                    Binding = new Binding($"ItemArray[{i++}]"),
                    IsReadOnly = true
                });
            }

        }

    }
}
