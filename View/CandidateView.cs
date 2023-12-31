﻿namespace Bts.View;

using Bts.Utils;
using Bts.Model;
using Bts.IService;

internal class CandidateView
{
    readonly IDocumentService _documentService;
    readonly IExamService _examService;
    User _candidateUser;

    public CandidateView(IDocumentService documentService, IExamService examService)
    {
        _documentService = documentService;
        _examService = examService;
    }

    public void MainMenu(User user)
    {
        _candidateUser = user;

        var exam = _examService.GetExamByCandidate(_candidateUser.Id);
        if (exam == null || exam.ExamPackage.IsSubmitted == true || exam.IsActive == false)
        {
            Console.WriteLine("\nYou have no active exam, check your email for exam schedule or contact the HR!");
            return;
        }

        var candidateDocumentHelper = _documentService.GetCandidateDocumentList(user.Id);
        var candidateDocumentList = candidateDocumentHelper.CandidateDocumentList;
        if (candidateDocumentHelper.AllDocumentIsUploaded)
        {
            StartExam(exam);
            return;
        }

        while (true)
        {
            var documentTypeList = _documentService.GetDocumentTypeList();
            var remainingDocumentTypeList = documentTypeList
                                            .Where(dt => candidateDocumentList.Exists(cd => cd.DocumentType.TypeName == dt.TypeName) == false)
                                            .ToList();
            if (remainingDocumentTypeList.Count == 0)
            {
                _documentService.UploadCandidateDocument(candidateDocumentList);
                Console.WriteLine("\nAll documents successfully uploaded!");
                StartExam(exam);
                break;
            }

            Console.WriteLine("\nYou must upload all these documents first");
            var number = 1;
            foreach (var documentType in remainingDocumentTypeList)
            {
                Console.WriteLine($"{number}. {documentType.TypeName}");
                number++;
            }
            var selectedOpt = Utils.GetNumberInputUtil(1, number - 1);

            var selectedDocumentType = remainingDocumentTypeList[selectedOpt - 1];
            var documentFilename = Utils.GetStringInputUtil(selectedDocumentType.TypeName + " file name");
            var documentExtension = Utils.GetStringInputUtil(selectedDocumentType.TypeName + " file extension");
            var file = new BTSFile()
            {
                FileContent = documentFilename,
                FileExtension = documentExtension,
            };
            var candidateDocument = new CandidateDocument()
            {
                File = file,
                CandidateId = user.Id,
                DocumentTypeId = selectedDocumentType.Id,
                DocumentType = selectedDocumentType,
            };
            candidateDocumentList.Add(candidateDocument);
        }
    }

    void StartExam(Exam exam)
    {
        Console.WriteLine("\nPlease be ready before starting the exam!");
        Console.WriteLine("1. Start Exam - " + exam.ExamPackage.Package.PackageName);
        Utils.GetNumberInputUtil(1, 1, "Start");

        var examStartTime = DateTime.Now;
        var examEndTime = examStartTime.AddMinutes(exam.ExamPackage.Duration);
        var answerList = new List<CandidateAnswer>();
        exam.ExamPackage.ExamStartDateTime = examStartTime;
        _examService.StartExam(exam.ExamPackage);
        while (true)
        {
            var timeRemaining = examEndTime - DateTime.Now;
            Console.WriteLine($"\n---- {exam.ExamPackage.Package.PackageName} (Time remaining: {timeRemaining.Minutes} minutes) ----");
            Console.WriteLine("Candidate: " + exam.Candidate.FullName + "\n");

            var number = 1;
            foreach (var question in exam.QuestionList)
            {
                var questionImage = question.Image;
                var questionText = question.QuestionContent;
                var questionString = questionImage?.FileContent == null ? questionText : questionImage?.FileContent + "." + questionImage?.FileExtension;
                var questionAnswer = answerList.Find(ans => ans.QuestionId == question.Id);
                if (questionAnswer != null)
                {
                    var answer = questionAnswer.AnswerContent == null ? questionAnswer.ChoiceOption?.OptionChar : questionAnswer.AnswerContent;
                    Console.WriteLine($"{number}. {questionString} (Your answer: {answer})");
                }
                else
                {
                    Console.WriteLine($"{number}. {questionString}");
                }

                var optionList = question.OptionList;
                if (optionList == null || optionList.Count == 0)
                {
                    number++;
                    continue;
                }
                else
                {
                    foreach (var option in optionList)
                    {
                        var optChar = option.OptionChar;
                        var optText = option.OptionText;
                        var optImage = option.OptionImage;
                        var optString = optImage?.FileContent == null ? optText : optImage?.FileContent + "." + optImage?.FileExtension;
                        Console.WriteLine($"   {optChar}) {optString}");
                    }
                }
                number++;
            }
            Console.WriteLine(number + ". Finish and Submit");
            var selectedOpt = Utils.GetNumberInputUtil(1, number, "Select question number to answer");

            if (selectedOpt == number)
            {
                Console.WriteLine("\nYou have finished the exam!");
                exam.ExamPackage.IsSubmitted = true;
                exam.ExamPackage.ExamId = exam.Id;
                _examService.SubmitExam(answerList, exam.ExamPackage);
                break;
            }
            else
            {
                var selectedQuestion = exam.QuestionList[selectedOpt - 1];
                var existingAnswer = answerList.Find(ans => ans.QuestionId == selectedQuestion.Id);
                if (selectedQuestion.OptionList == null || selectedQuestion.OptionList.Count == 0)
                {
                    var candidateAnswer = Utils.GetStringInputUtil("Your answer");
                    if (existingAnswer == null)
                    {
                        var answer = new CandidateAnswer()
                        {
                            QuestionId = selectedQuestion.Id,
                            ExamPackageId = exam.ExamPackage.Id,
                            AnswerContent = candidateAnswer,
                        };
                        answerList.Add(answer);
                    }
                    else
                    {
                        existingAnswer.AnswerContent = candidateAnswer;
                    }
                }
                else
                {
                    var questionString = selectedQuestion.QuestionContent == null ? selectedQuestion.Image?.FileContent + "." + selectedQuestion.Image?.FileExtension : selectedQuestion.QuestionContent;
                    Console.WriteLine("\n" + questionString);
                    var no = 1;
                    foreach (var option in selectedQuestion.OptionList)
                    {
                        var optChar = option.OptionChar;
                        var optText = option.OptionText;
                        var optImage = option.OptionImage;
                        var optString = optImage?.FileContent == null ? optText : optImage?.FileContent + "." + optImage?.FileExtension;
                        Console.WriteLine($"{no}. {optChar}) {optString}");
                        no++;
                    }
                    var selectedOptionNo = Utils.GetNumberInputUtil(1, no - 1, "Select your answer");

                    var selectedOption = selectedQuestion.OptionList[selectedOptionNo - 1];

                    if (existingAnswer == null)
                    {
                        var answer = new CandidateAnswer()
                        {
                            QuestionId = selectedQuestion.Id,
                            ExamPackageId = exam.ExamPackage.Id,
                            ChoiceOptionId = selectedOption.Id,
                            ChoiceOption = selectedOption,
                        };
                        answerList.Add(answer);
                    }
                    else
                    {
                        existingAnswer.ChoiceOption = selectedOption;
                    }
                }
            }
        }
    }
}
