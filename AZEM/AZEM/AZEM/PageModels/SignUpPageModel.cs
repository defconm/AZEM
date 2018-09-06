using AZEM.Model;

namespace AZEM.PageModels
{
    class SignUpPageModel : FreshMvvm.FreshBasePageModel
    {
        public SignUpModel SignUpUser { get; set; }
        public override void Init(object initData)
        {
            base.Init(initData);
            SignUpUser = new SignUpModel();
        }
    }
}
