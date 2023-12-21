using Bts.Config;
using Bts.Constant;
using Bts.Helper;
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
    IAcceptanceStatusRepo _acceptanceStatusRepo;
    IUserRepo _userRepo;
    SessionHelper _sessionHelper;

    public ExamService
    (
        IExamRepo examRepo,
        IExamPackageRepo examPackageRepo,
        ICandidateAnswerRepo candidateAnswerRepo,
        IQuestionRepo questionRepo,
        IMultipleChoiceOptionRepo optionRepo,
        SessionHelper sessionHelper,
        IAcceptanceStatusRepo acceptanceStatusRepo,
        IUserRepo userRepo
    )
    {
        _examRepo = examRepo;
        _examPackageRepo = examPackageRepo;
        _candidateAnswerRepo = candidateAnswerRepo;
        _questionRepo = questionRepo;
        _optionRepo = optionRepo;
        _sessionHelper = sessionHelper;
        _acceptanceStatusRepo = acceptanceStatusRepo;
        _userRepo = userRepo;
    }

    public ExamPackage CreateExam(ExamPackage examPackage)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            examPackage.Exam.CreatedBy = _sessionHelper.UserId;
            var newExam = _examRepo.CreateNewExam(examPackage.Exam, context);

            examPackage.ExamId = newExam.Id;
            examPackage.CreatedBy = _sessionHelper.UserId;
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
                questionList = questionList
                            .OrderBy(question => question.OptionList == null)
                            .ToList();
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
            questionList = questionList
                            .OrderBy(question => question.OptionList == null)
                            .ToList();
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

    public void StartExam(ExamPackage examPackage)
    {
        using (var context = new DBContextConfig())
        {
            examPackage.IsSubmitted = false;
            examPackage.UpdatedBy = _sessionHelper.UserId;
            _examPackageRepo.UpdateExamPackage(examPackage, context);
        }
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
                candidateAnswer.CreatedBy = _sessionHelper.UserId;
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

            var multipleChoiceScore = (double)((double)nOptionCorrect / nMultipleChoiceQuestion) * 100.0;
            examPackage.ReviewerScore = multipleChoiceScore;

            var needsReviewStatus = _acceptanceStatusRepo.GetAcceptanceStatusByCode(AcceptanceStatusCode.NeedsReview, context);
            examPackage.Exam.AcceptanceStatusId = needsReviewStatus.Id;
            examPackage.Exam.UpdatedBy = _sessionHelper.UserId;
            _examRepo.UpdateAcceptanceStatusOnExam(examPackage.Exam, context);

            examPackage.IsSubmitted = true;
            examPackage.UpdatedBy = _sessionHelper.UserId;
            _examPackageRepo.UpdateExamPackage(examPackage, context);

            var candidate = _userRepo.GetUserById(_sessionHelper.UserId, context);
            candidate.IsActive = false;
            candidate.UpdatedBy = _sessionHelper.UserId;
            _userRepo.UpdateUserIsActive(candidate, context);

            trx.Commit();
        }
    }

    public void InsertScoreNotesAcceptanceStatusOnExamPackage(ExamPackage examPackage, double essayScore)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            examPackage.Exam.UpdatedBy = _sessionHelper.UserId;
            _examRepo.UpdateAcceptanceStatusOnExam(examPackage.Exam, context);

            var multipleChoiceScore = examPackage.ReviewerScore;
            var finalScore = (multipleChoiceScore + essayScore) / 2.0d;

            examPackage.ReviewerScore = finalScore;
            examPackage.UpdatedBy = _sessionHelper.UserId;
            _examPackageRepo.UpdateExamPackage(examPackage, context);

            trx.Commit();
        }
    }

    public List<AcceptanceStatus> GetAcceptanceStatusList()
    {
        var acceptanceStatusList = new List<AcceptanceStatus>();
        using (var context = new DBContextConfig())
        {
            acceptanceStatusList = _acceptanceStatusRepo.GetAcceptanceStatusList(context);
        }
        return acceptanceStatusList;
    }
}
