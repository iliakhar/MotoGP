using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using motoGP.Models;
using ReactiveUI;
using System.Reactive;
using Microsoft.EntityFrameworkCore;
using motoGP.Views;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace motoGP.ViewModels
{
    public class TablesViewModel : ViewModelBase
    {
        private SQLiteConnection sql_con;
        private DataSet tables;
        public DataView dtView;
        private List<string> strReq;
        public List<string> StrReq
        {
            get => strReq;
            set => this.RaiseAndSetIfChanged(ref strReq, value);
        }
        public DataView DtView
        {
            get => dtView;
            set { this.RaiseAndSetIfChanged(ref dtView, value); }
        }
        int selectRow;
        bool isTableChosen;
        public bool IsTableChosen
        {
            get => isTableChosen;
            set { this.RaiseAndSetIfChanged(ref isTableChosen, value); }
        }

        public int currentTableIndex;
        public int CurrentTableIndex
        {
            get => currentTableIndex;
            set
            {
                IsTableChosen = value < 0 ? false : true;
                this.RaiseAndSetIfChanged(ref currentTableIndex, value);
            }
        }
        public int SelectRow
        {
            get => selectRow;
            private set => this.RaiseAndSetIfChanged(ref selectRow, value);
        }

        public DataSet Tables
        {
            get => tables;
            private set => this.RaiseAndSetIfChanged(ref tables, value);
        }
        //public ObservableCollection<TabModel> Tabs { get; set; }

        public TablesViewModel()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type=\"table\" ORDER BY 1";
            string connectionStr = "Data Source=MotoGP.sqlite3;Mode=ReadWrite";

            using (sql_con = new SQLiteConnection(connectionStr))
            {
                sql_con.Open();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                DataTable tablesNames = new DataTable();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    tablesNames.Load(reader);
                    tables = new DataSet();
                    string subsql = "SELECT * FROM ";
                    foreach (DataRow row in tablesNames.Rows)
                    {
                        string name = row.ItemArray[0].ToString();
                        if (name == "sqlite_sequence") continue;
                        SQLiteCommand sqlTab = new SQLiteCommand(subsql + name, sql_con);
                        DataTable table = new DataTable();
                        table.Load(sqlTab.ExecuteReader());
                        tables.Tables.Add(table);
                    }
                }
            }
            CurrentTableIndex = -1;
        }
        public void AddRow()
        {
            DataRow row = tables.Tables[currentTableIndex].NewRow();
            List<object> arr = new List<object>();
            /*for (int i = 0; i < row.ItemArray.Length; i++)
            {
                arr.Add(i+10);
               
            }*/
            //row.ItemArray = arr.ToArray();
            tables.Tables[currentTableIndex].Rows.Add(row);
        }

        public void DeleteRow()
        {
            //tables.Tables[currentTableIndex].Rows.RemoveAt(selectRow);
            tables.Tables[currentTableIndex].Rows[selectRow].Delete();
            //tables.Tables[currentTableIndex].Rows.
            //tables.Tables[currentTableIndex].AcceptChanges();

            /*tables.Tables[currentTableIndex].Rows.RemoveAt(selectRow);*/

        }

        public void OnSave()
        {
            string connectionStr = "Data Source=MotoGP.sqlite3;Mode=Write";

            using (sql_con = new SQLiteConnection(connectionStr))
            {
                for (int i = 0; i < tables.Tables.Count; i++)
                {

                    try
                    {
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM " + tables.Tables[i].TableName, sql_con);

                        SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(adapter);
                        adapter.Update(tables.Tables[i]);
                        
                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                }
                tables.AcceptChanges();
            }
        }
        ~TablesViewModel()
        {
            sql_con.Close();
        }
    }
}
