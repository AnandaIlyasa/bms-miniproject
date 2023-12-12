using Bts.IService;
using Bts.Model;

namespace Bts.Service;

internal class QuestionService : IQuestionService
{
    public void CreateQuestionList(List<Question> questionList)
    {

    }

    public List<Question> GetQuestionListByCandidate(int candidateId)
    {
        return new List<Question>();
    }

    public List<Question> GetQuestionListByPackage(int packageId)
    {
        return new List<Question>();
    }
}
