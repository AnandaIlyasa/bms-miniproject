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
        foreach (Question question in questionList)
        {
            if (question.Image != null)
            {
                var questionImage = _fileRepo.CreateNewFile(question.Image);
                question.Image.Id = questionImage.Id;
            }
            var insertedQuestion = _questionRepo.CreateNewQuestion(question);

            foreach (var option in question.OptionList)
            {
                if (option.OptionImage != null)
                {
                    var optionImage = _fileRepo.CreateNewFile(option.OptionImage);
                    option.OptionImage.Id = optionImage.Id;
                }
                if (insertedQuestion != null)
                {
                    option.Question = insertedQuestion;
                }
                _optionRepo.CreateNewOption(option);
            }
        }
    }

    public List<Question> GetQuestionListByCandidate(int candidateId)
    {
        return new List<Question>();
    }

    public List<Question> GetQuestionListByPackage(int packageId)
    {
        var questionList = _questionRepo.GetQuestionListByPackage(packageId);
        return questionList;
    }
}
