using Bts.Model;

namespace Bts.IRepo;

internal interface IQuestionRepo
{
    List<Question> GetQuestionListByPackage(int packageId);
    Question CreateNewQuestion(Question question);
}
