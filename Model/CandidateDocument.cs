using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bts.Model;

[Table("t_r_candidate_document")]
[Index(nameof(CandidateId), nameof(FileId), nameof(DocumentTypeId), IsUnique = true, Name = "candidate_document_ck")]
internal class CandidateDocument : BaseModel
{

    [Column("candidate_id")]
    public int CandidateId { get; set; }

    [ForeignKey(nameof(CandidateId))]
    public User Candidate { get; set; }

    [Column("file_id")]
    public int FileId { get; set; }

    [ForeignKey(nameof(FileId))]
    public BTSFile File { get; set; }

    [Column("document_type_id")]
    public int DocumentTypeId { get; set; }

    [ForeignKey(nameof(DocumentTypeId))]
    public DocumentType DocumentType { get; set; }
}
