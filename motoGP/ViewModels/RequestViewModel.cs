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
            if(strRequests == null)
                strRequests = new List<string>();
            ReqNames = new ObservableCollection<string>();
            for (int i = 0; i < strRequests.Count; i++)
                ReqNames.Add("Request " + i.ToString());
            UsebleTablesAndReq = new ObservableCollection<string> { "" };
            //UsebleTablesAndReq.Union(ReqNames);
            foreach(var it in ReqNames)
               UsebleTablesAndReq.Add(it);
            
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
            SelectEn = JoinEn = WhereEn = SaveEn = false;
            FromEn = true;
            selStr = whereStr = "";
            corespNames = new List<int> { 0 };
            ReqListInd = -1;
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


        string curReq, fromStr, whereStr, selStr;
        public string CurReq
        {
            get => curReq;
            set { this.RaiseAndSetIfChanged(ref curReq, value); }
        }
        int lastFromInd, reqListInd;
        public List<int> corespNames;
        public int ReqListInd
        {
            get => reqListInd;
            set => this.RaiseAndSetIfChanged(ref reqListInd, value);
        }
        public void AddSel()
        {
            if (SelPr.TableInd == 0) return;
            if (selPr.ColInd!=0)
            {
                if (selPr.ColInd <= 0) return;
                if (selStr.Contains("SELECT"))
                    selStr += ", " + TableName[corespNames[selPr.TableInd]] + "." + selPr.Table[SelPr.ColInd];
                else selStr += "SELECT " + TableName[corespNames[selPr.TableInd]] + "." + selPr.Table[SelPr.ColInd];
            }
            if (selPr.AgrInd > 0 && selPr.AgrTbInd > 0)
            {
                if (selStr.Contains("SELECT"))
                    selStr += ", "+ SelPr.Agreg[SelPr.AgrInd] + "("+ TableName[corespNames[selPr.TableInd]] + "." + selPr.Table[SelPr.AgrTbInd]+")";
                else selStr += "SELECT " + SelPr.Agreg[SelPr.AgrInd] + "(" + TableName[corespNames[selPr.TableInd]] + "." + selPr.Table[SelPr.AgrTbInd] + ")";
            }
            if (selStr != "")
            {
                SaveEn = true;
                CurReq = selStr + "\n" + fromStr + whereStr;
                FromEn = JoinEn = WhereEn = false;
                SelectEn = true;
            }
            else CurReq = fromStr + whereStr;
        }
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
                    corespNames.Add(FromPr.TableInd);
                }
                else if (!(fromStr.Contains(" " + name + ",") || (fromStr.Contains(" " + name + " ")&& !fromStr.Contains("N " + name + " ")) ||
                    (fromStr.LastIndexOf(" " + name) + name.Length + 1) == fromStr.Length))
                {
                    fromStr += ", " + name;
                    UsebleTables.Add(name);
                    UsebleTablesAndReq.Add(name);
                    corespNames.Add(FromPr.TableInd);
                }
                CurReq = fromStr;
                LastFromAtr = new ObservableCollection<string>(atributes[FromPr.TableInd]);
                lastFromInd = FromPr.TableInd;
                SelectEn = JoinEn = WhereEn = true;
            }
            
        }
        public void AddJoin()
        {
            if(JoinPr.TableInd>0 && JoinPr.AtrIndFirst!=0 && JoinPr.AtrIndSec > 0
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
            if (WhPr.isConnect == false) return;
            if (WhPr.TableInd <= 0) return;
            if (WhPr.TableInd <= ReqNames.Count)
            {
                txt += " ( " + StrReq[WhPr.TableInd - 1] + " ) ";
            }
            else
            {
                txt += " " + UsebleTablesAndReq[WhPr.TableInd] + "." + WhPr.WhereAtr[WhPr.AtrInd];
            }
            if (WhPr.SignInd <= 0) return;
            txt += " " + WhPr.Signs[WhPr.SignInd];
            if(WhPr.SecOperand == "" || WhPr.SecOperand == null) return;
            if (WhPr.SecOperand.IndexOf("Request ") == 0)
            {
                bool findReq = false;
                for(int i = 0; i < ReqNames.Count; i++)
                    if (ReqNames[i] == WhPr.SecOperand)
                    {
                        findReq = true;
                        txt += " ( " + StrReq[i] + " ) ";
                    }
                if (!findReq) return;
            }
            else txt += WhPr.SecOperand;
            if (WhPr.ConnectInd != 0) txt += " " + WhPr.ConectWord[WhPr.ConnectInd];
            else WhPr.isConnect = false;
            WhPr.Text += " " + txt;
        }

        public void AddWhere()
        {
            if (WhPr.Text == "" || WhPr.Text == null) return;
            whereStr += "\nWHERE " + WhPr.Text;
            if (WhPr.GroupTbInd > 0 && WhPr.GroupInd > 0)
                whereStr += " GROUP BY " + UsebleTables[WhPr.GroupTbInd] + "." + WhPr.GroupTb[WhPr.GroupInd];
            CurReq = fromStr + whereStr;
            FromEn = JoinEn = WhereEn = false;
            SelectEn = true;
        }

        public void SaveReq()
        {
            SelectEn = JoinEn = WhereEn = SaveEn = false;
            FromEn = true;
            StrReq.Add(CurReq);
            CurReq = "";
            if (ReqNames.Count > 0)
            {
                string lastNumbStr = ReqNames[ReqNames.Count - 1].Substring(ReqNames[ReqNames.Count - 1].IndexOf(" ") + 1);
                int lastNumb = Int32.Parse(lastNumbStr);
                ReqNames.Add("Request " + (lastNumb + 1).ToString());
            }
            else ReqNames = new ObservableCollection<string> { "Request 0" };
            UsebleTablesAndReq.Clear();
            UsebleTables.Clear();
            UsebleTablesAndReq = new ObservableCollection<string> { "" };
            UsebleTables = new ObservableCollection<string> { "" };
            foreach (var it in ReqNames)
                UsebleTablesAndReq.Add(it);
            SelPr = new SelectProperty();
            JoinPr = new JoinProperty();
            FromPr = new FromProperty();
            WhPr = new WhereProperty();
        }
        public void DelReq()
        {
            StrReq.RemoveAt(ReqListInd);
            ReqNames.RemoveAt(ReqListInd);
            if(ReqNames.Count > 0)
                for (int i = 1; i < ReqNames.Count + 1; i++)
                {
                    if(ReqNames[i-1] !=UsebleTablesAndReq[i])
                        UsebleTablesAndReq.RemoveAt(i);
                }
            else UsebleTablesAndReq.RemoveAt(1);
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

        bool selectEn, joinEn, whereEn, fromEn, saveEn;
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
        public bool FromEn
        {
            get => fromEn;
            set { this.RaiseAndSetIfChanged(ref fromEn, value); }
        }
        public bool SaveEn
        {
            get => saveEn;
            set { this.RaiseAndSetIfChanged(ref saveEn, value); }
        }
        ///////////////////////////////////////
        public ObservableCollection<string> TableName { get; set; }
        public List<List<string>> atributes;
        
        

        
    }
}
