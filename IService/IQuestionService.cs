using Bts.Model;

namespace Bts.IService;

internal interface IQuestionService
{
    List<Question> GetQuestionListByPackage(int packageId);
    List<Question> GetQuestionListByCandidate(int candidateId);
    void CreateQuestionList(List<Question> questionList);
}
