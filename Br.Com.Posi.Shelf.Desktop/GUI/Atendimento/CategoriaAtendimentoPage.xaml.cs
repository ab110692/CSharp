using Br.Com.Posi.Shelf.DAO;
using Br.Com.Posi.Shelf.Desktop.GUI.Outro;
using Br.Com.Posi.Shelf.Model;
using Br.Com.Posi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Br.Com.Posi.Shelf.Desktop.GUI.Atendimento
{
    /// <summary>
    /// Interaction logic for CategoriaAtendimentoPage.xaml
    /// </summary>
    public partial class CategoriaAtendimentoPage : Page
    {
        private ICategoriaDAO daoCategoria;
        private TreeViewItem tvItem;
        private List<Categoria> categorias;
        private object objectSelect;

        public CategoriaAtendimentoPage()
        {
            InitializeComponent();

            MyComponentsUtil.IsEnableComponents(false, txtDescricao, btnCadastrarSubCategoria, btnAlterar, btnRemover);

            this.categorias = new List<Categoria>();
            this.daoCategoria = DAOFactory.InitCategoriaDAO();
            this.categorias.AddRange(daoCategoria.GetList());

            this.LoadTreeView();
        }

        private void LoadTreeView()
        {
            treeViewCategoria.Items.Clear();
            foreach (Categoria categoria in categorias)
            {
                tvItem = new TreeViewItem
                {
                    Header = categoria
                };
                treeViewCategoria.Items.Add(tvItem);
                foreach (SubCategoria subCategoria in categoria.SubCategorias)
                {
                    tvItem = new TreeViewItem
                    {
                        Header = subCategoria
                    };
                    (treeViewCategoria.Items[treeViewCategoria.Items.Count - 1] as TreeViewItem).Items.Add(tvItem);
                    foreach (Item item in subCategoria.Items)
                    {
                        tvItem = new TreeViewItem
                        {
                            Header = item
                        };
                        var i = (treeViewCategoria.Items[treeViewCategoria.Items.Count - 1] as TreeViewItem).Items;
                        (i[i.Count - 1] as TreeViewItem).Items.Add(tvItem);
                    }
                }
            }
        }

        private void treeViewCategoria_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            foreach (TreeViewItem categoria in this.treeViewCategoria.Items)
            {
                if (categoria.IsSelected)
                {
                    this.objectSelect = categoria.Header;
                    this.txtDescricao.Text = (objectSelect as Categoria).Nome;

                    this.btnCadastrarSubCategoria.Content = "Nova SubCategoria";
                    this.btnRemover.Content = "Remover";
                    MyComponentsUtil.IsEnableComponents(true, btnCadastrarSubCategoria, btnAlterar, btnRemover);
                    return;
                }
                foreach (TreeViewItem subCategoria in categoria.Items)
                {
                    if (subCategoria.IsSelected)
                    {
                        this.objectSelect = subCategoria.Header;
                        this.txtDescricao.Text = (objectSelect as SubCategoria).Nome;

                        this.btnCadastrarSubCategoria.Content = "Nova SubCategoria";
                        this.btnRemover.Content = "Remover";
                        MyComponentsUtil.IsEnableComponents(true, btnCadastrarSubCategoria, btnAlterar, btnRemover);
                        return;
                    }
                    foreach (TreeViewItem item in subCategoria.Items)
                    {
                        if (item.IsSelected)
                        {
                            this.objectSelect = item.Header;
                            this.txtDescricao.Text = (objectSelect as Item).Nome;

                            this.btnCadastrarSubCategoria.Content = "Nova SubCategoria";
                            this.btnRemover.Content = "Remover";
                            MyComponentsUtil.IsEnableComponents(true, btnCadastrarSubCategoria, btnAlterar, btnRemover);
                            return;
                        }
                    }
                }
            }
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Novo"))
            {
                (sender as Button).Content = "Cadastrar Categoria";
                this.btnRemover.Content = "Cancelar";
                this.txtDescricao.Text = string.Empty;
                MyComponentsUtil.IsEnableComponents(false, btnCadastrarSubCategoria, btnAlterar);
                MyComponentsUtil.IsEnableComponents(true, txtDescricao, btnRemover);
            }
            else if ((sender as Button).Content.Equals("Cadastrar Categoria"))
            {
                if (string.IsNullOrEmpty(this.txtDescricao.Text) || this.categorias.Where(cat => cat.Nome.Equals(this.txtDescricao.Text)).Any())
                {
                    BallonDialog.Show("Descrição inválida ou Existente!", "Alerta");
                    return;
                }
                Categoria c = new Categoria
                {
                    Nome = this.txtDescricao.Text
                };
                c = this.daoCategoria.SaveOrUpdate(c);
                this.categorias.Add(c);
                this.LoadTreeView();
                this.Clear();
                (sender as Button).Content = "Novo";
                MyComponentsUtil.IsEnableComponents(false, txtDescricao, btnCadastrarSubCategoria, btnAlterar, btnRemover);
                BallonDialog.Show($"Categoria: {c.Nome} cadastrado com sucesso!", "Informativo");
            }
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDescricao.Text))
            {
                BallonDialog.Show("Descrição inválida!", "Alerta");
                return;
            }
            try
            {
                if (this.btnCadastrarSubCategoria.Content.Equals("Nova SubCategoria"))
                {
                    this.btnRemover.Content = "Cancelar";
                    this.btnCadastrarSubCategoria.Content = "Cadastrar SubCategoria";
                    this.txtDescricao.Text = string.Empty;
                    MyComponentsUtil.IsEnableComponents(true, txtDescricao);
                }
                else if (this.btnCadastrarSubCategoria.Content.Equals("Cadastrar SubCategoria"))
                {
                    foreach (TreeViewItem sub in treeViewCategoria.Items)
                    {
                        if (sub.IsSelected)
                        {
                            Categoria cat = categorias.Where(ca => ca.Nome.Equals(sub.Header.ToString())).Single();
                            SubCategoria s = new SubCategoria();
                            s.Nome = this.txtDescricao.Text;
                            s.Categoria = cat;
                            cat.SubCategorias.Add(s);
                            daoCategoria.SaveOrUpdate(cat);
                            BallonDialog.Show($"SubCategoria: {s.Nome} cadastrado com sucesso!", "Informativo");
                            this.txtDescricao.Text = ("Cadastrar SubCategoria");
                            MyComponentsUtil.IsEnableComponents(false, txtDescricao, btnCadastrarSubCategoria, btnAlterar, btnRemover);
                            this.LoadTreeView();
                            this.Clear();
                            return;
                        }

                        foreach (TreeViewItem item in sub.Items)
                        {
                            if (item.IsSelected)
                            {
                                Categoria cat = categorias.Where(ca => ca.Nome.Equals(sub.Header.ToString())).Single();
                                SubCategoria s = cat.SubCategorias.Where(subc => subc.Nome.Equals(item.Header.ToString())).Single();
                                Item i = new Item();
                                i.Nome = this.txtDescricao.Text;
                                i.SubCategoria = s;
                                s.Items.Add(i);
                                daoCategoria.SaveOrUpdate(cat);
                                BallonDialog.Show($"Item: {i.Nome} cadastrado com sucesso!", "Informativo");
                                this.txtDescricao.Text = ("Cadastrar SubCategoria");
                                MyComponentsUtil.IsEnableComponents(false, txtDescricao, btnCadastrarSubCategoria, btnAlterar, btnRemover);
                                this.LoadTreeView();
                                this.Clear();
                                return;
                            }
                        }

                    }
                    throw new Exception("Categoria não selecionado");
                }
            }
            catch (Exception ex)
            {
                BallonDialog.Show(ex.Message.ToString(), "Erro");
            }
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDescricao.Text))
            {
                BallonDialog.Show("Descrição inválida!", "Alerta");
                return;
            }
            try
            {
                if (objectSelect is Categoria)
                {
                    Categoria categoria = (objectSelect as Categoria);
                    categoria.Nome = this.txtDescricao.Text;
                    categoria = daoCategoria.SaveOrUpdate(categoria);

                    BallonDialog.Show("Alterado Categoria com sucesso !", "Modificação");
                }
                else if (objectSelect is SubCategoria)
                {
                    SubCategoria subCategoria = (objectSelect as SubCategoria);
                    subCategoria.Nome = this.txtDescricao.Text;
                    subCategoria = daoCategoria.SaveOrUpdate(subCategoria.Categoria).SubCategorias.First();

                    BallonDialog.Show("Alterado SubCategoria com sucesso !", "Modificação");
                }
                else if (objectSelect is Item)
                {
                    Item item = (objectSelect as Item);
                    item.Nome = this.txtDescricao.Text;
                    item = daoCategoria.SaveOrUpdate(item.SubCategoria.Categoria).SubCategorias.First().Items.First();

                    BallonDialog.Show("Alterado Item com sucesso !", "Modificação");
                }

                this.LoadTreeView();
                this.Clear();
                MyComponentsUtil.IsEnableComponents(false, txtDescricao, btnCadastrarSubCategoria, btnAlterar, btnRemover);
            }
            catch (Exception ex)
            {
                BallonDialog.Show("Ocorreu um erro na Alteração: " + ex.Message.ToString(), "Erro");
            }
        }

        private void btnRemover_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Cancelar"))
            {
                this.Clear();
                this.btnNovo.Content = "Novo";
                this.btnRemover.Content = "Remover";
                MyComponentsUtil.IsEnableComponents(false, txtDescricao, btnCadastrarSubCategoria, btnAlterar, btnRemover);
            }
            else if ((sender as Button).Content.Equals("Remover"))
            {
                try
                {
                    if (!MessageDialog.Show(Window.GetWindow(this), "Deseja realmente excluir ?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        return;
                    }

                    foreach (TreeViewItem cat in treeViewCategoria.Items)
                    {
                        if (cat.IsSelected)
                        {
                            Categoria categoria = categorias.Where(ca => ca.Nome.Equals(cat.Header.ToString())).Single();
                            if (daoCategoria.Delete(categoria))
                            {
                                categorias.Remove(categoria);
                                BallonDialog.Show("Removido Categoria com sucesso !", "Remoção");
                            }
                            break;
                        }
                        foreach (TreeViewItem sub in cat.Items)
                        {
                            if (sub.IsSelected)
                            {
                                Categoria categoria = categorias.Where(ca => ca.Nome.Equals(cat.Header.ToString())).Single();
                                SubCategoria s = categoria.SubCategorias.Where(subc => subc.Nome.Equals(sub.Header.ToString())).Single();
                                if (DAOFactory.InitSubCategoriaDAO().Delete(s))
                                {
                                    categoria.SubCategorias.Remove(s);
                                    BallonDialog.Show("Removido SubCategoria com sucesso !", "Remoção");
                                }
                                break;
                            }
                            foreach (TreeViewItem it in sub.Items)
                            {
                                if (it.IsSelected)
                                {
                                    Categoria categoria = categorias.Where(ca => ca.Nome.Equals(cat.Header.ToString())).Single();
                                    SubCategoria s = categoria.SubCategorias.Where(subc => subc.Nome.Equals(sub.Header.ToString())).Single();
                                    Item i = s.Items.Where(ite => ite.Nome.Equals(it.Header.ToString())).First();
                                    if (DAOFactory.InitItemDAO().Delete(i))
                                    {
                                        s.Items.Remove(i);
                                        BallonDialog.Show("Removido Item com sucesso !", "Remoção");
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    this.LoadTreeView();
                    this.Clear();
                    this.btnRemover.Content = "Remover";
                    MyComponentsUtil.IsEnableComponents(false, txtDescricao, btnCadastrarSubCategoria, btnAlterar, btnRemover);
                }
                catch (Exception ex)
                {
                    BallonDialog.Show("Ocorreu um erro na remoção: " + ex.Message.ToString(), "Erro");
                }
            }
        }

        private void Clear()
        {
            this.txtDescricao.Text = string.Empty;
        }

        private void treeViewCategoria_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MenuItem item = new MenuItem();
            item.Items.Add("New");
            item.Items.Add("Rename");
            item.Items.Add("Remove");

            this.AddLogicalChild(item);
        }
    }
}

