using IS_Studio_Miniaturas.Helpers;

namespace IS_Studio_Miniaturas.ViewModels
{
    public class PrincipalViewModel
    {

        private Corel.Interop.VGCore.Application corelApp;
        private CorelHelper _corelHelper;

        //Contrutor
        public PrincipalViewModel()
        {
            _corelHelper = new CorelHelper();
        }

        // Metodos


    }
}