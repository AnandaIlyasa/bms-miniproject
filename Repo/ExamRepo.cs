namespace Bts.Repo;

using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

internal class ExamRepo : IExamRepo
{
    public Exam CreateNewExam(Exam exam, DBContextConfig context)
    {
        context.Exams.Add(exam);
        context.SaveChanges();
        return exam;
    }

    public Exam? GetExamByCandidate(int candidateId, DBContextConfig context)
    {
        var examNotExist = context.Exams
                        .Where(e => e.CandidateId == candidateId)
                        .IsNullOrEmpty();
        if (examNotExist)
        {
            return null;
        }

        var exam = context.Exams
            .Where(e => e.CandidateId == candidateId)
            .Include(e => e.Reviewer)
            .Include(e => e.Candidate)
            .Include(e => e.AcceptanceStatus)
            .First();
        return exam;
    }

    public Exam GetExamById(int examId, DBContextConfig context)
    {
        var exam = context.Exams
                    .Where(e => e.Id == examId)
                    .Include(e => e.Reviewer)
                    .Include(e => e.Candidate)
                    .Include(e => e.AcceptanceStatus)
                    .First();

        return exam;
    }

    public List<Exam> GetExamListByReviewer(int reviewerId, DBContextConfig context)
    {
        var examList = context.Exams
            .Where(e => e.ReviewerId == reviewerId)
            .Include(e => e.Candidate)
            .Include(e => e.AcceptanceStatus)
            .ToList();

        return examList;
    }

    public List<Exam> GetExamList(DBContextConfig context)
    {
        var examList = context.Exams
                        .FromSql($@"SELECT 
                                        e.*,
                                        can.full_name,
                                        acs.status_name 
                                    FROM 
                                        t_r_exam e 
                                    JOIN 
                                        t_m_user can ON e.candidate_id = can.id 
                                    LEFT JOIN 
                                        t_m_acceptance_status acs ON e.acceptance_status_id = acs.id 
                                    JOIN 
                                        t_r_exam_package ep ON e.id = ep.exam_id 
                                    JOIN 
                                        t_m_package p ON ep.package_id = p.id")
                        .Include(e => e.Candidate)
                        .Include(e => e.Reviewer)
                        .Include(e => e.AcceptanceStatus)
                        .ToList();

        return examList;
    }
}
