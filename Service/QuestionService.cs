using Bts.Config;
using Bts.IRepo;
using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class QuestionService : IQuestionService
{
    readonly IQuestionRepo _questionRepo;
    readonly IFileRepo _fileRepo;
    readonly IMultipleChoiceOptionRepo _optionRepo;

    public QuestionService(IQuestionRepo questionRepo, IFileRepo fileRepo, IMultipleChoiceOptionRepo optionRepo)
    {
        _questionRepo = questionRepo;
        _fileRepo = fileRepo;
        _optionRepo = optionRepo;
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
                    var questionImage = _fileRepo.CreateNewFile(question.Image, context);
                    question.ImageId = questionImage.Id;
                }
                var insertedQuestion = _questionRepo.CreateNewQuestion(question, context);

                if (question.OptionList != null && question.OptionList.Count > 0)
                {
                    foreach (var option in question.OptionList)
                    {
                        if (option.OptionImage != null)
                        {
                            var optionImage = _fileRepo.CreateNewFile(option.OptionImage, context);
                            option.OptionImageId = optionImage.Id;
                        }
                        if (insertedQuestion != null)
                        {
                            option.QuestionId = insertedQuestion.Id;
                        }
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
        return questionList;
    }
}
