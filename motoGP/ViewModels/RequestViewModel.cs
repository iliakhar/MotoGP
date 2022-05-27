using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using motoGP.Models;
using ReactiveUI;
using System.Reactive;
using System.Data;
using System.Data.SQLite;

namespace motoGP.ViewModels
{
    public class RequestViewModel : ViewModelBase
    {

        public RequestViewModel(DataSet tableSet, List<string> strRequests)
        {
            strRequests = new List<string> { "111", "222", "333" };
            ReqNames = new ObservableCollection<string>();
            for (int i = 0; i < strRequests.Count; i++)
                ReqNames.Add("Request " + i.ToString());
            UsebleTablesAndReq = new ObservableCollection<string> { "" };
            //UsebleTablesAndReq.Union(ReqNames);
            foreach(var it in ReqNames)
               UsebleTablesAndReq.Add(it);
            Agreg = new ObservableCollection<string> { "", "AVG", "SUM", "MIN", "MAX", "COUNT" };
            SQLiteConnection sql_con;
            StrReq = strRequests;
            Send = ReactiveCommand.Create(() => StrReq);
            Cancel = ReactiveCommand.Create(() => { });

            //SelectInd = 0;
            TableName = new ObservableCollection<string> { "" };
            atributes = new List<List<string>> { new List<string> { "" } };
            DataTable table;
            for (int i = 0; i < tableSet.Tables.Count; i++)
            {
                table = tableSet.Tables[i];
                TableName.Add(table.TableName);
                atributes.Add(new List<string> { "" });
                foreach (DataColumn col in table.Columns)
                {
                    atributes[i+1].Add(col.ColumnName);
                }
            }

            UsebleTables = new ObservableCollection<string> { "" };
            
            SelPr = new SelectProperty();
            JoinPr = new JoinProperty();
            FromPr = new FromProperty();
            WhPr = new WhereProperty();
            JoinEn = WhereEn = true;
            SelectEn = true;
            whereStr = "";
        }
        public ReactiveCommand<Unit, List<string>> Send { get; set; }
        public ReactiveCommand<Unit, Unit> Cancel { get; set; }
        private List<string> strReq;
        public List<string> StrReq
        {
            get => strReq;
            set => this.RaiseAndSetIfChanged(ref strReq, value);
        }

        private ObservableCollection<string> currAtr, usebleTables, usebleTablesAndReq, reqNames;
        public ObservableCollection<string> CurrAtr
        {
            get => currAtr;
            set => this.RaiseAndSetIfChanged(ref currAtr, value);
        }
        public ObservableCollection<string> ReqNames
        {
            get => reqNames;
            set => this.RaiseAndSetIfChanged(ref reqNames, value);
        }

        public ObservableCollection<string> UsebleTables
        {
            get => usebleTables;
            set => this.RaiseAndSetIfChanged(ref usebleTables, value);
        }
        public ObservableCollection<string> UsebleTablesAndReq
        {
            get => usebleTablesAndReq;
            set => this.RaiseAndSetIfChanged(ref usebleTablesAndReq, value);
        }


        string curReq, fromStr, whereStr;
        public string CurReq
        {
            get => curReq;
            set { this.RaiseAndSetIfChanged(ref curReq, value); }
        }
        int lastFromInd;

        public void AddFrom()
        {
            if (TableName[FromPr.TableInd] != "")
            {
                string name = TableName[FromPr.TableInd];
                if (fromStr == null)
                {
                    fromStr = "FROM " + name;
                    UsebleTables.Add(name);
                    UsebleTablesAndReq.Add(name);
                }
                else if (!(fromStr.Contains(" " + name + ",") || fromStr.Contains(" " + name + " ") ||
                    (fromStr.LastIndexOf(" " + name) + name.Length + 1) == fromStr.Length))
                {
                    fromStr += ", " + name;
                    UsebleTables.Add(name);
                    UsebleTablesAndReq.Add(name);
                }
                CurReq = fromStr;
                LastFromAtr = new ObservableCollection<string>(atributes[FromPr.TableInd]);
                lastFromInd = FromPr.TableInd;
            }
            
        }
        public void AddJoin()
        {
            if(JoinPr.TableInd!=0 && JoinPr.AtrIndFirst!=0 && JoinPr.AtrIndSec != 0
                && TableName[JoinPr.TableInd]!= TableName[FromPr.TableInd])
            {
                fromStr+=" INNER JOIN "+TableName[JoinPr.TableInd];
                fromStr += " ON " + TableName[lastFromInd] + "." + LastFromAtr[JoinPr.AtrIndFirst];
                fromStr += " = " + TableName[JoinPr.TableInd] + "." + JoinAtr[JoinPr.AtrIndSec]+"\n";
                CurReq = fromStr;
            }
        }

        public void Input()
        {
            string txt = "";
            if (WhPr.TableInd == 0) return;
            if (WhPr.TableInd <= ReqNames.Count)
            {
                txt += " ( " + StrReq[WhPr.TableInd - 1] + " ) ";
            }
            else
            {
                txt += " " + UsebleTablesAndReq[WhPr.TableInd] + "." + WhPr.WhereAtr[WhPr.AtrInd];
            }
            if (WhPr.SignInd == 0) return;
            txt += " " + WhPr.Signs[WhPr.SignInd];
            if(WhPr.SecOperand == "") return;
            txt += WhPr.SecOperand;
            if (WhPr.ConnectInd != 0) txt += " " + WhPr.ConectWord[WhPr.ConnectInd];
            WhPr.Text += " " + txt;
        }

        public void AddWhere()
        {
            if (WhPr.Text == "") return;
            whereStr += "\nWHERE " + WhPr.Text;
            if (WhPr.GroupTbInd != 0 && WhPr.GroupInd != 0)
                whereStr += " GROUP BY " + UsebleTables[WhPr.GroupTbInd] + "." + WhPr.GroupTb[WhPr.GroupInd];
            CurReq = fromStr + whereStr;
        }


        /// <summary>
        /// /////////////////////////////////////////////////
        /// </summary>
        private ObservableCollection<string> joinAtr, lastFromAtr;
        public ObservableCollection<string> JoinAtr
        {
            get => joinAtr;
            set => this.RaiseAndSetIfChanged(ref joinAtr, value);
        }
        

        public ObservableCollection<string> LastFromAtr
        {
            get => lastFromAtr;
            set => this.RaiseAndSetIfChanged(ref lastFromAtr, value);
        }
        
        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>
        public SelectProperty selPr;
        public SelectProperty SelPr
        {
            get => selPr;
            set => this.RaiseAndSetIfChanged(ref selPr, value);
        }
        public JoinProperty joinPr;
        public JoinProperty JoinPr
        {
            get => joinPr;
            set => this.RaiseAndSetIfChanged(ref joinPr, value);
        }
        public FromProperty fromPr;
        public FromProperty FromPr
        {
            get => fromPr;
            set => this.RaiseAndSetIfChanged(ref fromPr, value);
        }
        public WhereProperty whPr;
        public WhereProperty WhPr
        {
            get => whPr;
            set => this.RaiseAndSetIfChanged(ref whPr, value);
        }
        ///////////////////////////////////////

        bool selectEn, joinEn, whereEn;
        public bool SelectEn
        {
            get => selectEn;
            set { this.RaiseAndSetIfChanged(ref selectEn, value); }
        }
        public bool JoinEn
        {
            get => joinEn;
            set { this.RaiseAndSetIfChanged(ref joinEn, value); }
        }
        public bool WhereEn
        {
            get => whereEn;
            set { this.RaiseAndSetIfChanged(ref whereEn, value); }
        }
        ///////////////////////////////////////
        public ObservableCollection<string> TableName { get; set; }
        public List<List<string>> atributes;
        
        public ObservableCollection<string> Agreg { get; set; }

        
    }
}
