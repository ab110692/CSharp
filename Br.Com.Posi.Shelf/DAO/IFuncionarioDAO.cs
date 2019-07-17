using Br.Com.Posi.Connection;
using Br.Com.Posi.Shelf.Model;
using System;


namespace Br.Com.Posi.Shelf.DAO
{
    public interface IFuncionarioDAO : IDAO<Funcionario>
    {
        /// <summary>
        /// verifica se o usuario e a senha existe.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Funcionario Find(String user,String pass);

        /// <summary>
        /// Vertifica se o usuario existe.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Funcionario VerificaUsuario(String user);
    }
}
