namespace SulsApp.ViewModels.Users
{
    using SulsApp.ViewModels.Receipt;
    using System.Collections.Generic;

    public class ProfileViewModel
    {
        public UserViewModel User { get; set; }

        public ICollection<ReceiptViewModel> Receipts { get; set; }
    }
}
