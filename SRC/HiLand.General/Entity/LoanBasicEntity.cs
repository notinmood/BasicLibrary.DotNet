﻿using System;
using System.Collections.Generic;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Framework.FoundationLayer;
using HiLand.Framework.FoundationLayer.Attributes;
using HiLand.General.BLL;
using HiLand.General.DAL;
using HiLand.General.Enums;
using HiLand.Utility.Data;
using HiLand.Utility.Finance;

namespace HiLand.General.Entity
{
    /// <summary>
    /// 贷款信息实体
    /// </summary>
    public class LoanBasicEntity : BaseModel<LoanBasicEntity>
    {
        #region 实体信息
        private int loanID;
        public int LoanID
        {
            get { return loanID; }
            set { loanID = value; }
        }

        private Guid loanGuid = Guid.Empty;
        [DBFieldAttribute(IsBusinessPrimaryKey = true)]
        public Guid LoanGuid
        {
            get { return loanGuid; }
            set { loanGuid = value; }
        }

        private LoanTypes loanType;
        public LoanTypes LoanType
        {
            get { return loanType; }
            set { loanType = value; }
        }

        private decimal loanAmount;
        public decimal LoanAmount
        {
            get { return loanAmount; }
            set { loanAmount = value; }
        }

        private PaymentTermTypes loanTermType;
        public PaymentTermTypes LoanTermType
        {
            get { return loanTermType; }
            set { loanTermType = value; }
        }

        private decimal loanInterest;
        public decimal LoanInterest
        {
            get { return loanInterest; }
            set { loanInterest = value; }
        }

        private int loanTermCount;
        public int LoanTermCount
        {
            get { return loanTermCount; }
            set { loanTermCount = value; }
        }

        private string loanPurpose = String.Empty;
        public string LoanPurpose
        {
            get { return loanPurpose; }
            set { loanPurpose = value; }
        }

        private int loanUserID = 0;
        public int LoanUserID
        {
            get { return loanUserID; }
            set { loanUserID = value; }
        }

        private string loanOwnerAddon = string.Empty;
        private string loanOwnerDisplay = string.Empty;
        private string loanOwnerKey = string.Empty;
        public string LoanOwnerAddon
        {
            get
            {
                return this.loanOwnerAddon;
            }
            set
            {
                this.loanOwnerAddon = value;
            }
        }

        public string LoanOwnerDisplay
        {
            get
            {
                if (this.loanOwnerDisplay == string.Empty)
                {
                    LoanOwnerTypes loanOwnerType = this.LoanOwnerType;
                    if ((loanOwnerType != LoanOwnerTypes.Person) && (loanOwnerType == LoanOwnerTypes.Enterprise))
                    {
                        EnterpriseEntity entity = BaseBLL<EnterpriseBLL, EnterpriseEntity, EnterpriseDAL, IDAL<EnterpriseEntity>>.Instance.Get(GuidHelper.TryConvert(this.LoanOwnerKey));
                        this.loanOwnerDisplay = entity.CompanyName;
                    }
                    else
                    {
                        BusinessUser user = BusinessUserBLL.Get(GuidHelper.TryConvert(this.LoanOwnerKey));
                        this.loanOwnerDisplay = user.UserNameDisplay;
                    }
                }
                return this.loanOwnerDisplay;
            }
        }

        public string LoanOwnerKey
        {
            get
            {
                return this.loanOwnerKey;
            }
            set
            {
                this.loanOwnerKey = value;
            }
        }

        private LoanOwnerTypes loanOwnerType = LoanOwnerTypes.Person;

        public LoanOwnerTypes LoanOwnerType
        {
            get
            {
                return this.loanOwnerType;
            }
            set
            {
                this.loanOwnerType = value;
            }
        }

        private DateTime loanDate = DateTimeHelper.Min;
        public DateTime LoanDate
        {
            get { return loanDate; }
            set { loanDate = value; }
        }

        private LoanStatuses loanStatus;
        public LoanStatuses LoanStatus
        {
            get { return loanStatus; }
            set { loanStatus = value; }
        }

        private Guid checkUserID = Guid.Empty;
        public Guid CheckUserID
        {
            get { return checkUserID; }
            set { checkUserID = value; }
        }

        private DateTime checkDate = DateTimeHelper.Min;
        public DateTime CheckDate
        {
            get { return checkDate; }
            set { checkDate = value; }
        }

        private Guid readUserID = Guid.Empty;
        public Guid ReadUserID
        {
            get { return readUserID; }
            set { readUserID = value; }
        }

        private DateTime readDate = DateTimeHelper.Min;
        public DateTime ReadDate
        {
            get { return readDate; }
            set { readDate = value; }
        }

        #region 扩展属性
        private List<BusinessUser> usersInEnterprise = null;
        /// <summary>
        /// 当前贷款对应企业内的所有人员
        /// </summary>
        private List<BusinessUser> UsersInEnterprise
        {
            get
            {
                if (usersInEnterprise == null)
                {
                    usersInEnterprise = BusinessUserBLL.GetUsersByDepartment(this.LoanUserID);
                }

                return usersInEnterprise;
            }
        }

        private BusinessUser loanUserReal = null;
        /// <summary>
        /// 贷款人信息（如果是企业用户，显示第一个企业负责人）
        /// </summary>
        public BusinessUser LoanUserReal
        {
            get
            {
                if (this.loanUserReal == null)
                {
                    if (usersInEnterprise == null || usersInEnterprise.Count == 0)
                    {
                        loanUserReal = BusinessUser.Empty;
                    }
                    else
                    {
                        loanUserReal = usersInEnterprise[0];
                    }
                }

                return this.loanUserReal;
            }
        }

        private string loanUserRealName = string.Empty;
        /// <summary>
        /// 贷款人的名字（如果是企业用户，显示第一个企业负责人）
        /// </summary>
        public string LoanUserRealName
        {
            get
            {
                if (string.IsNullOrEmpty(this.loanUserRealName))
                {
                    loanUserRealName = LoanUserReal.UserNameDisplay;
                }

                return loanUserRealName;
            }
            set { loanUserRealName = value; }
        }

        private Guid loanUserRealID = Guid.Empty;
        /// <summary>
        /// 贷款人的Guid（如果是企业用户，显示第一个企业负责人）
        /// </summary>
        public Guid LoanUserRealID
        {
            get
            {
                if (loanUserRealID == Guid.Empty)
                {
                    loanUserRealID = LoanUserReal.UserGuid;
                }

                return loanUserRealID;
            }
            set { loanUserRealID = value; }
        }
        #endregion

        #endregion
    }
}