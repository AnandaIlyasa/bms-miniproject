using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class ExamService : IExamService
{
    IExamRepo _examRepo;
    IExamPackageRepo _examPackageRepo;
    ICandidateAnswerRepo _candidateAnswerRepo;

    public ExamService(IExamRepo examRepo, IExamPackageRepo examPackageRepo, ICandidateAnswerRepo candidateAnswerRepo)
    {
        _examRepo = examRepo;
        _examPackageRepo = examPackageRepo;
        _candidateAnswerRepo = candidateAnswerRepo;
    }

    public ExamPackage CreateExam(ExamPackage examPackage)
    {
        var newExam = _examRepo.CreateNewExam(examPackage.Exam);
        examPackage.Exam.Id = newExam.Id;
        var newExamPackage = _examPackageRepo.CreateNewExamPackage(examPackage);
        return newExamPackage;
    }

    public Exam GetExamByCandidate(int candidateId)
    {
        var exam = _examRepo.GetExamByCandidate(candidateId);
        return exam;
    }

    public Exam GetExamById(int examId)
    {
        var exam = _examRepo.GetExamById(examId);
        return exam;
    }

    public List<Exam> GetExamList()
    {
        var repoList = _examRepo.GetExamList();
        return repoList;
    }

    public List<Exam> GetExamListByReviewer(int idReviewer)
    {
        var examList = _examRepo.GetExamListByReviewer(idReviewer);
        return examList;
    }

    public void SubmitExam(List<CandidateAnswer> candidateAnswerList, ExamPackage examPackage)
    {
        foreach (var candidateAnswer in candidateAnswerList)
        {
            _candidateAnswerRepo.CreateNewCandidateAnswer(candidateAnswer);
        }
        _examPackageRepo.UpdateExamPackage(examPackage);
    }
}
