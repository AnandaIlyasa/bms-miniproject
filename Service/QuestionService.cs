using Bts.Config;
using Bts.Helper;
using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class QuestionService : IQuestionService
{
    readonly IQuestionRepo _questionRepo;
    readonly IFileRepo _fileRepo;
    readonly IMultipleChoiceOptionRepo _optionRepo;
    SessionHelper _sessionHelper;

    public QuestionService(IQuestionRepo questionRepo, IFileRepo fileRepo, IMultipleChoiceOptionRepo optionRepo, SessionHelper sessionHelper)
    {
        _questionRepo = questionRepo;
        _fileRepo = fileRepo;
        _optionRepo = optionRepo;
        _sessionHelper = sessionHelper;
    }

    public void CreateQuestionList(List<Question> questionList)
    {
        using (var context = new DBContextConfig())
        {
            var trx = context.Database.BeginTransaction();

            foreach (Question question in questionList)
            {
                if (question.Image != null)
                {
                    question.Image.CreatedBy = _sessionHelper.UserId;
                    var questionImage = _fileRepo.CreateNewFile(question.Image, context);
                    question.ImageId = questionImage.Id;
                }
                question.CreatedBy = _sessionHelper.UserId;
                var insertedQuestion = _questionRepo.CreateNewQuestion(question, context);

                if (question.OptionList != null && question.OptionList.Count > 0)
                {
                    foreach (var option in question.OptionList)
                    {
                        if (option.OptionImage != null)
                        {
                            option.OptionImage.CreatedBy = _sessionHelper.UserId;
                            var optionImage = _fileRepo.CreateNewFile(option.OptionImage, context);
                            option.OptionImageId = optionImage.Id;
                        }
                        if (insertedQuestion != null)
                        {
                            option.QuestionId = insertedQuestion.Id;
                        }
                        option.CreatedBy = _sessionHelper.UserId;
                        _optionRepo.CreateNewOption(option, context);
                    }
                }
            }

            trx.Commit();
        }
    }

    public List<Question> GetQuestionListByPackage(int packageId)
    {
        var questionList = new List<Question>();
        using (var context = new DBContextConfig())
        {
            questionList = _questionRepo.GetQuestionListByPackage(packageId, context);
            foreach (var question in questionList)
            {
                var optionList = _optionRepo.GetOptionListByQuestion(question.Id, context);
                question.OptionList = optionList;
            }
        }
        var groupingQuery = from question in questionList
                            group question by question.OptionList.Count > 0 into questionGroup
                            orderby questionGroup.Key
                            select questionGroup;

        var groupedQuestionList = new List<Question>();
        foreach (var questionGroup in groupingQuery)
        {
            foreach (var question in questionGroup)
            {
                groupedQuestionList.Add(question);
            }
        }
        return groupedQuestionList;
    }
}
