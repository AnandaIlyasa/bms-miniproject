namespace Bts.View;

using Bts.Utils;
using Bts.Model;
using Bts.IService;

internal class CandidateView
{
    readonly IDocumentService _documentService;
    User _candidateUser;

    public CandidateView(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public void MainMenu(User user)
    {
        _candidateUser = user;
        var candidateDocumentList = _documentService.GetCandidateDocumentList(user.Id);
        var documentTypeList = _documentService.GetDocumentTypeList();
        var allDocumentIsUploaded = true;
        foreach (var documentType in documentTypeList)
        {
            if (candidateDocumentList.Exists(cd => cd.DocumentType.TypeName == documentType.TypeName) == false)
            {
                allDocumentIsUploaded = false;
                break;
            }
        }

        if (allDocumentIsUploaded)
        {
            StartExam();
            return;
        }

        while (true)
        {
            var remainingDocumentTypeList = documentTypeList
                                            .Where(dt => candidateDocumentList.Exists(cd => cd.DocumentType.TypeName == dt.TypeName) == false)
                                            .ToList();
            if (remainingDocumentTypeList.Count == 0)
            {
                _documentService.UploadCandidateDocument(candidateDocumentList);
                Console.WriteLine("\nAll documents successfully uploaded!");
                StartExam();
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
                CreatedBy = _candidateUser.Id,
                CreatedAt = DateTime.Now,
                Ver = 0,
                IsActive = true,
            };
            var candidateDocument = new CandidateDocument()
            {
                File = file,
                Candidate = user,
                DocumentType = selectedDocumentType,
                CreatedBy = _candidateUser.Id,
                CreatedAt = DateTime.Now,
                Ver = 0,
                IsActive = true,
            };
            candidateDocumentList.Add(candidateDocument);
        }
    }

    void StartExam()
    {
        Console.WriteLine("\nPlease be ready before starting the exam!");
        Console.WriteLine("1. Start Exam");
        Utils.GetNumberInputUtil(1, 1, "Start");
        while (true)
        {
            Console.WriteLine("\n---- JAVA-01 (Time remaining: 54 minutes) ----");
            Console.WriteLine("Candidate name : Budiman");
            Console.WriteLine("1. Manakah prinsip OOP ke-1");
            Console.WriteLine("A. Inheritance");
            Console.WriteLine("B. Encapsulation");
            Console.WriteLine("C. Abstraction");
            Console.WriteLine("D. Polymorphism");
            Console.WriteLine("2. Sebutkan prinsip OOP ke-2");
            Console.WriteLine("3. Finish and Submit");
            var selectedOpt = Utils.GetNumberInputUtil(1, 3, "Select question number to answer");

            if (selectedOpt == 1)
            {
                var candidateOpt = Utils.GetStringInputUtil("Your option");
            }
            else if (selectedOpt == 2)
            {
                var candidateAnswer = Utils.GetStringInputUtil("Your answer");
            }
            else
            {
                Console.WriteLine("\nYou have finished the exam!");
                break;
            }
        }
    }
}
