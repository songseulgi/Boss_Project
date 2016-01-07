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

namespace BizOneShot.Light.Models.WebModels
{
    // SC_FORM_FILE
    public class ScFormFile
    {
        public int FileSn { get; set; } // FILE_SN (Primary key). 파일식별자
        public int FormSn { get; set; } // FORM_SN. 서식식별자(순번)

        // Foreign keys
        public virtual ScFileInfo ScFileInfo { get; set; } // FK_SC_FILE_INFO_TO_SC_FORM_FILE
        public virtual ScForm ScForm { get; set; } // FK_SC_FORM_TO_SC_FORM_FILE
    }

}
