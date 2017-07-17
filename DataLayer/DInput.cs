using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DInput
    {
        private int _IdInput;
        
        private int _IdStaff;
        
        private int _IdSupplier;
        
        private DateTime _DateInput;

        private string _TypeDoc;
        
        private string _SerieDoc;
                
        private string _Corelativ;
                
        private decimal _Vat;

        private string _CurrentStatus;
               
        private string _SearchText;
               
        private string _SearchText2;

        
        public int IdInput
        {
            get { return _IdInput; }
            set { _IdInput = value; }
        }

        public int IdStaff
        {
            get { return _IdStaff; }
            set { _IdStaff = value; }
        }

        public int IdSupplier
        {
            get { return _IdSupplier; }
            set { _IdSupplier = value; }
        }

        public DateTime DateInput
        {
            get { return _DateInput; }
            set { _DateInput = value; }
        }

        public string TypeDoc
        {
            get { return _TypeDoc; }
            set { _TypeDoc = value; }
        }

        public string SerieDoc
        {
            get { return _SerieDoc; }
            set { _SerieDoc = value; }
        }

        public string Corelativ
        {
            get { return _Corelativ; }
            set { _Corelativ = value; }
        }

        public decimal Vat
        {
            get { return _Vat; }
            set { _Vat = value; }
        }

        public string CurrentStatus
        {
            get { return _CurrentStatus; }
            set { _CurrentStatus = value; }
        }

        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }

        public string SearchText2
        {
            get { return _SearchText2; }
            set { _SearchText2 = value; }
        }

        public DInput()
        {

        }

        public DInput(int idStaff, int idSupplier, DateTime dateInput, string typeDoc, string serie, string corelativ,
            decimal vat, string currentStatus)
        {
            this.IdStaff = idStaff;
            this.IdSupplier = IdSupplier;
            this.DateInput = dateInput;
            this.TypeDoc = typeDoc;
            this.SerieDoc = serie;
            this.Corelativ = corelativ;
            this.Vat = vat;
            this.CurrentStatus = currentStatus;
        }
    }
}
