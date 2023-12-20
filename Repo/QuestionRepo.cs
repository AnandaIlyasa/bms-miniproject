using Bts.Config;
using Bts.IRepo;
using Bts.Model;
using Microsoft.EntityFrameworkCore;

namespace Bts.Repo;

internal class QuestionRepo : IQuestionRepo
{
    public Question CreateNewQuestion(Question question, DBContextConfig context)
    {
        context.Questions.Add(question);
        context.SaveChanges();
        return question;
    }

    public List<Question> GetQuestionListByPackage(int packageId, DBContextConfig context)
    {
        var questionList = context.Questions
                            .Where(q => q.PackageId == packageId)
                            .Include(q => q.Image)
                            .ToList();
        return questionList;
    }
}
