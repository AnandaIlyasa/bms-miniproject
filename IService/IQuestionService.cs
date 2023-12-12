using Bts.Model;

namespace Bts.IService;

internal interface IQuestionService
{
    List<Question> GetQuestionListByPackage(int packageId);
    void CreateQuestionList(List<Question> questionList);
    List<Question> GetQuestionListByCandidate(int candidateId);
}
