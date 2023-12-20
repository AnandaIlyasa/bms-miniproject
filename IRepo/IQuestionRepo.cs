using Bts.Config;
using Bts.Model;

namespace Bts.IRepo;

internal interface IQuestionRepo
{
    List<Question> GetQuestionListByPackage(int packageId, DBContextConfig context);
    Question CreateNewQuestion(Question question, DBContextConfig context);
}
