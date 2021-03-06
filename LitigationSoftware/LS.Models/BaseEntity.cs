﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
    public class BaseEntity
    {
        #region Properties
        public int Id { get; set; }
        public bool Active { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        #endregion

        #region Serialization
        public bool ShouldSerializeAddedBy()
        {
            return AddedBy.HasValue;
        }

        public bool ShouldSerializeAddedDate()
        {
            return AddedDate.HasValue;
        }

        public bool ShouldSerializeModifiedBy()
        {
            return ModifiedBy.HasValue;
        }

        public bool ShouldSerializeModifiedDate()
        {
            return ModifiedDate.HasValue;
        }

        public bool ShouldSerializeDeletedBy()
        {
            return DeletedBy.HasValue;
        }

        public bool ShouldSerializeDeletedDate()
        {
            return DeletedDate.HasValue;
        }
        #endregion
    }
}
