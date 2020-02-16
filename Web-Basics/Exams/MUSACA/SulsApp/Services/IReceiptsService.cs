namespace SulsApp.Services
{
    using SulsApp.ViewModels.Receipt;
    using System.Collections.Generic;

    public interface IReceiptsService
    {
        ICollection<ReceiptViewModel> GetReceiptsByUserId(string userId);

        ICollection<ReceiptViewModel> GetAllReceipts();

        void Create(ReceiptViewModel model);
    }
}
