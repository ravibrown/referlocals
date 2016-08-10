using DataAccess.HelperClasses;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Classes
{
    public class PropStates
    {
        #region "Properties"
        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //SNO
        //private Int64 _SNO = 0;
        
        //public Int64 SNO
        //{
        //    get { return _SNO; }
        //    set { _SNO = value; }
        //}

        //State
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        //State
        //private string _Keyword = string.Empty;
        //public string Keyword
        //{
        //    get { return _Keyword; }
        //    set { _Keyword = value; }
        //}

        //City
        private string _City = string.Empty;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        //Zip
        private string _Zip = string.Empty;
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }

        ////IsApproved
        //private Int64 _IsApproved = (int)HelperEnums.BooleanValues.Both;
        //public Int64 IsApproved
        //{
        //    get { return _IsApproved; }
        //    set { _IsApproved = value; }
        //}

        ////Take
        //private Int64 _Take = 0;
        //public Int64 Take
        //{
        //    get { return _Take; }
        //    set { _Take = value; }
        //}

        ////Index
        //private Int64 _Index = 0;
        //public Int64 Index
        //{
        //    get { return _Index; }
        //    set { _Index = value; }
        //}

        ////CreatedDate
        //private DateTime? _CreatedDate = DateTime.UtcNow;
        //public DateTime? CreatedDate
        //{
        //    get { return _CreatedDate; }
        //    set { _CreatedDate = value; }
        //}

        ////UpdatedDate
        //private DateTime? _UpdatedDate = DateTime.UtcNow;
        //public DateTime? UpdatedDate
        //{
        //    get { return _UpdatedDate; }
        //    set { _UpdatedDate = value; }
        //}

        ////CreatedBy
        //private Int64? _CreatedBy = 0;
        //public Int64? CreatedBy
        //{
        //    get { return _CreatedBy ?? 0; }
        //    set { _CreatedBy = value; }
        //}

        ////IsDeleted
        //private bool _IsDeleted = false;
        //public bool IsDeleted
        //{
        //    get { return _IsDeleted; }
        //    set { _IsDeleted = value; }
        //}

        ////IsApprovedByAdmin
        //private bool _IsApprovedByAdmin = false;
        //public bool IsApprovedByAdmin
        //{
        //    get { return _IsApprovedByAdmin; }
        //    set { _IsApprovedByAdmin = value; }
        //}

        ////DataRecieved
        //private bool _DataRecieved = false;
        //public bool DataRecieved
        //{
        //    get { return _DataRecieved; }
        //    set { _DataRecieved = value; }
        //}

        #endregion
    }
}
