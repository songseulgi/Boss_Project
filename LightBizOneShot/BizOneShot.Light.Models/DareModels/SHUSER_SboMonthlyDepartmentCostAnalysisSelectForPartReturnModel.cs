// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading;

namespace BizOneShot.Light.Models.DareModels
{
    public class SHUSER_SboMonthlyDepartmentCostAnalysisSelectForPartReturnModel
    {
        public String SET_YEAR { get; set; }
        public String SET_MONTH { get; set; }
        public Decimal? TOTAL { get; set; }
        public Decimal? MAN_AMT { get; set; }
        public Decimal? SALES_AMT { get; set; }
        public Decimal? ETC_AMT { get; set; }
    }

}
