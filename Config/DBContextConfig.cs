using Bts.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bts.Config;

internal class DBContextConfig : DbContext
{
    public DbSet<AcceptanceStatus> AcceptanceStatuses { get; set; }
    public DbSet<BTSFile> BTSFiles { get; set; }
    public DbSet<CandidateAnswer> CandidateAnswers { get; set; }
    public DbSet<CandidateDocument> CandidateDocuments { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamPackage> ExamPackages { get; set; }
    public DbSet<MultipleChoiceOption> MultipleChoiceOptions { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string host = "LAPTOP-4GBC7O34\\SQLEXPRESS";
        const string db = "b_ts";
        const string connString = $"Server={host}; Initial Catalog={db}; Integrated Security=True; TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connString);
        //optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }
}
