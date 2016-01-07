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
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using BizOneShot.Light.Models.WebModels;
using System.Threading;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace BizOneShot.Light.Dao.WebConfiguration
{
    // SC_FORM
    internal partial class ScFormConfiguration : EntityTypeConfiguration<ScForm>
    {
        public ScFormConfiguration()
            : this("dbo")
        {
        }
 
        public ScFormConfiguration(string schema)
        {
            ToTable(schema + ".SC_FORM");
            HasKey(x => x.FormSn);

            Property(x => x.FormSn).HasColumnName("FORM_SN").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Subject).HasColumnName("SUBJECT").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Contents).HasColumnName("CONTENTS").IsOptional().HasColumnType("nvarchar").HasMaxLength(2000);
            Property(x => x.FormType).HasColumnName("FORM_TYPE").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Status).HasColumnName("STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.RegId).HasColumnName("REG_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UpdId).HasColumnName("UPD_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.UpdDt).HasColumnName("UPD_DT").IsOptional().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
