using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

[Table("t_m_package")]
[Index(nameof(PackageCode), IsUnique = true, Name = "package_bk")]
internal class Package : BaseModel
{
    [Column("package_code"), MaxLength(10)]
    public string PackageCode { get; set; }

    [Column("package_name"), MaxLength(30)]
    public string PackageName { get; set; }
}
