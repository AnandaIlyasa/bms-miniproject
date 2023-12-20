using Bts.Config;
using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class ExamService : IExamService
{
    IExamRepo _examRepo;
    IExamPackageRepo _examPackageRepo;
    ICandidateAnswerRepo _candidateAnswerRepo;
    IQuestionRepo _questionRepo;
    IMultipleChoiceOptionRepo _optionRepo;

    public ExamService(IExamRepo examRepo, IExamPackageRepo examPackageRepo, ICandidateAnswerRepo candidateAnswerRepo, IQuestionRepo questionRepo, IMultipleChoiceOptionRepo optionRepo)
    {
        _examRepo = examRepo;
        _examPackageRepo = examPackageRepo;
        _candidateAnswerRepo = candidateAnswerRepo;
        _questionRepo = questionRepo;
        _optionRepo = optionRepo;
    }

    public ExamPackage CreateExam(ExamPackage examPackage)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            var newExam = _examRepo.CreateNewExam(examPackage.Exam, context);
            examPackage.ExamId = newExam.Id;
            examPackage = _examPackageRepo.CreateNewExamPackage(examPackage, context);

            trx.Commit();
        }
        return examPackage;
    }

    public Exam? GetExamByCandidate(int candidateId)
    {
        Exam? exam = null;
        using (var context = new DBContextConfig())
        {
            exam = _examRepo.GetExamByCandidate(candidateId, context);
            if (exam != null)
            {
                var examPackage = _examPackageRepo.GetExamPackageByExam(exam.Id, context);
                exam.ExamPackage = examPackage;

                var questionList = _questionRepo.GetQuestionListByPackage(examPackage.PackageId, context);
                exam.QuestionList = questionList;
                foreach (var question in questionList)
                {
                    var optionList = _optionRepo.GetOptionListByQuestion(question.Id, context);
                    question.OptionList = optionList;
                }

                var candidateAnswerList = _candidateAnswerRepo.GetCandidateAnswerListByExamPackage(examPackage.Id, context);
                exam.CandidateAnswerList = candidateAnswerList;
            }
        }
        return exam;
    }

    public Exam GetExamById(int examId)
    {
        var exam = new Exam();
        using (var context = new DBContextConfig())
        {
            exam = _examRepo.GetExamById(examId, context);

            var examPackage = _examPackageRepo.GetExamPackageByExam(exam.Id, context);
            exam.ExamPackage = examPackage;

            var questionList = _questionRepo.GetQuestionListByPackage(examPackage.PackageId, context);
            exam.QuestionList = questionList;

            var candidateAnswerList = _candidateAnswerRepo.GetCandidateAnswerListByExamPackage(examPackage.Id, context);
            exam.CandidateAnswerList = candidateAnswerList;
        }
        return exam;
    }

    public List<Exam> GetExamList()
    {
        var examList = new List<Exam>();
        using (var context = new DBContextConfig())
        {
            examList = _examRepo.GetExamList(context);
            foreach (var exam in examList)
            {
                var examPackage = _examPackageRepo.GetExamPackageByExam(exam.Id, context);
                exam.ExamPackage = examPackage;
            }
        }
        examList = examList
                .GroupBy(e => e.Id)
                .Select(e => e.First())
                .ToList();
        return examList;
    }

    public List<Exam> GetExamListByReviewer(int idReviewer)
    {
        var examList = new List<Exam>();
        using (var context = new DBContextConfig())
        {
            examList = _examRepo.GetExamListByReviewer(idReviewer, context);
            foreach (var exam in examList)
            {
                var examPackage = _examPackageRepo.GetExamPackageByExam(exam.Id, context);
                exam.ExamPackage = examPackage;
            }
        }
        return examList;
    }

    public void SubmitExam(List<CandidateAnswer> candidateAnswerList, ExamPackage examPackage)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            var nOptionCorrect = 0;
            foreach (var candidateAnswer in candidateAnswerList)
            {
                if (candidateAnswer.ChoiceOption != null && candidateAnswer.ChoiceOption.IsCorrect)
                {
                    nOptionCorrect++;
                }
            }

            foreach (var candidateAnswer in candidateAnswerList)
            {
                candidateAnswer.ChoiceOption = null;
                _candidateAnswerRepo.CreateNewCandidateAnswer(candidateAnswer, context);
            }

            var nMultipleChoiceQuestion = 0;
            var questionList = _questionRepo.GetQuestionListByPackage(examPackage.PackageId, context);
            foreach (var question in questionList)
            {
                var optionList = _optionRepo.GetOptionListByQuestion(question.Id, context);
                question.OptionList = optionList;
            }
            foreach (var question in questionList)
            {
                if (question.OptionList.Count != 0) nMultipleChoiceQuestion++;
            }

            var multipleChoiceScore = (double)((double)nOptionCorrect / (double)nMultipleChoiceQuestion) * 100.0;
            examPackage.ReviewerScore = multipleChoiceScore;

            _examPackageRepo.UpdateExamPackage(examPackage, context);

            trx.Commit();
        }
    }

    public void UpdateReviewerScoreAndNotesOnExamPackage(ExamPackage examPackage)
    {
        using (var context = new DBContextConfig())
        {
            _examPackageRepo.UpdateExamPackage(examPackage, context);
        }
    }

    // TODO : Update Exam Package when candidate start the exam
}
