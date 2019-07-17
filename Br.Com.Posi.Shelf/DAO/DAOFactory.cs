namespace Br.Com.Posi.Shelf.DAO
{
    public static class DAOFactory
    {

        public static IFuncionarioDAO InitFuncionarioDAO()
        {
            return new FuncionarioDAOImpl();
        }

        public static IFuncionarioDadosPessoaisDAO InitFuncionarioDadosPessoais()
        {
            return new FuncionarioDadosPessoaisDAOImpl();
        }

        public static IClienteDAO InitClienteDAO()
        {
            return new ClienteDAOImpl();
        }

        public static IPerfilDAO InitPerfilDAO()
        {
            return new PerfilDAOImpl();
        }

        public static ISetorDAO InitSetorDAO()
        {
            return new SetorDAOImpl();
        }

        public static ITelefoneDAO InitTelefoneDAO()
        {
            return new TelefoneDAOImpl();
        }

        public static IRedeDAO InitRedeDAO()
        {
            return new RedeDAOImpl();
        }

        public static IAntiVirusDAO InitAntivirusDAO()
        {
            return new AntiVirusDAOImpl();
        }

        public static IContratoDAO InitContratoDAO()
        {
            return new ContratoDAOImpl();
        }

        public static ICategoriaDAO InitCategoriaDAO()
        {
            return new CategoriaDAOImpl();
        }

        public static ISubCategoriaDAO InitSubCategoriaDAO()
        {
            return new SubCategoriaDAOImpl();
        }

        public static IItemDAO InitItemDAO()
        {
            return new ItemDAOImpl();
        }

        public static IAplicativoDAO InitAplicativoDAO()
        {
            return new AplicativoDAOImpl();
        }

        public static IVersaoDAO InitVersaoDAO()
        {
            return new VersaoDAOImpl();
        }

        public static IMSWindowsDAO InitMSWindowsDAO()
        {
            return new MSWindowsDAOImpl();
        }

        public static IComputadorDAO InitComputadorDAO()
        {
            return new ComputadorDAOImpl();
        }

        public static IProtocoloDAO InitProtocoloDAO()
        {
            return new ProtocoloDAOImpl();
        }

        public static IAtendimentoDAO InitAtendimentoDAO()
        {
            return new AtendimentoDAOImpl();
        }

        public static IAtendimentoDetalhadoDAO InitAtendimentoDetalhadoDAO()
        {
            return new AtendimentoDetalhadoDAOImpl();
        }

        public static IProblemaDAO InitProblemaDAO()
        {
            return new ProblemaDAOImpl();
        }
    }
}
